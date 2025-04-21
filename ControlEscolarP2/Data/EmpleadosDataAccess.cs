using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NLog;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.Data
{
    public class EmpleadosDataAccess
    {
        // Logger para esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.EmpleadosDataAccess");

        // Acceso a PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Acceso a datos de persona (relación con empleado)
        private readonly PersonasDataAccess _personasData;

        // Constructor
        public EmpleadosDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
                _personasData = new PersonasDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar EmpleadosDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Inserta un nuevo empleado junto con su persona relacionada
        /// </summary>
        public int InsertarEmpleado(Empleado empleado)
        {
            try
            {
                // Inserta primero a la persona
                int idPersona = _personasData.InsertarPersona(empleado.DatosPersonales);
                if (idPersona <= 0)
                {
                    _logger.Error($"No se pudo insertar la persona del empleado: {empleado.Matricula}");
                    return -1;
                }

                // Asignar el ID de la persona al objeto empleado
                empleado.Id_Persona = idPersona;

                // Query para insertar el empleado
                string query = @"
                    INSERT INTO human_resours.empleado
                    (id_persona, fecha_ingreso, fecha_baja, id_departamento, id_puesto, matricula, motivo, estatus)
                    VALUES
                    (@IdPersona, @FechaIngreso, @FechaBaja, @IdDepartamento, @IdPuesto, @Matricula, @Motivo, @Estatus)
                    RETURNING id_empleado;";

                // Parámetros para la consulta
                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@IdPersona", empleado.Id_Persona),
                    _dbAccess.CreateParameter("@FechaIngreso", empleado.Fecha_Ingreso),
                    _dbAccess.CreateParameter("@FechaBaja", empleado.Fecha_Baja ?? (object)DBNull.Value),
                    _dbAccess.CreateParameter("@IdDepartamento", empleado.Id_Departamento),
                    _dbAccess.CreateParameter("@IdPuesto", empleado.Id_Puesto),
                    _dbAccess.CreateParameter("@Matricula", empleado.Matricula),
                    _dbAccess.CreateParameter("@Motivo", empleado.Motivo),
                    _dbAccess.CreateParameter("@Estatus", empleado.Estatus)
                };

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);

                int idGenerado = Convert.ToInt32(resultado);
                _logger.Info($"Empleado insertado correctamente con ID {idGenerado}");
                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar empleado con matrícula {empleado.Matricula}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Actualiza los datos de un empleado y su persona asociada
        /// </summary>
        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                _logger.Debug($"Actualizando empleado ID {empleado.Id_Empleado} con persona ID {empleado.Id_Persona}");

                // Primero actualizar la persona
                bool personaActualizada = _personasData.ActualizarPersona(empleado.DatosPersonales);
                if (!personaActualizada)
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {empleado.Id_Persona}");
                    return false;
                }

                // Query para actualizar empleado
                string query = @"
                    UPDATE human_resours.empleado
                    SET fecha_ingreso = @FechaIngreso,
                        fecha_baja = @FechaBaja,
                        id_departamento = @IdDepartamento,
                        id_puesto = @IdPuesto,
                        matricula = @Matricula,
                        motivo = @Motivo,
                        estatus = @Estatus
                    WHERE id_empleado = @IdEmpleado;";

                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@IdEmpleado", empleado.Id_Empleado),
                    _dbAccess.CreateParameter("@FechaIngreso", empleado.Fecha_Ingreso),
                    _dbAccess.CreateParameter("@FechaBaja", empleado.Fecha_Baja ?? (object)DBNull.Value),
                    _dbAccess.CreateParameter("@IdDepartamento", empleado.Id_Departamento),
                    _dbAccess.CreateParameter("@IdPuesto", empleado.Id_Puesto),
                    _dbAccess.CreateParameter("@Matricula", empleado.Matricula),
                    _dbAccess.CreateParameter("@Motivo", empleado.Motivo),
                    _dbAccess.CreateParameter("@Estatus", empleado.Estatus)
                };

                _dbAccess.Connect();
                int filas = _dbAccess.ExecuteNonQuery(query, parametros);

                if (filas > 0)
                {
                    _logger.Info($"Empleado con ID {empleado.Id_Empleado} actualizado correctamente");
                    return true;
                }

                _logger.Warn($"No se encontró empleado con ID {empleado.Id_Empleado} para actualizar");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar empleado con ID {empleado.Id_Empleado}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene un empleado y su persona asociada por ID
        /// </summary>
        public Empleado? ObtenerEmpleadoPorId(int id)
        {
            try
            {
                string query = @"
                    SELECT e.*, p.*
                    FROM human_resours.empleado e
                    INNER JOIN human_resours.persona p ON e.id_persona = p.id_persona
                    WHERE e.id_empleado = @Id";

                var param = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, param);

                if (resultado.Rows.Count == 0)
                {
                    _logger.Warn($"Empleado con ID {id} no encontrado");
                    return null;
                }

                DataRow row = resultado.Rows[0];

                // Crea la persona
                Persona persona = new Persona
                {
                    Id_Persona = Convert.ToInt32(row["id_persona"]),
                    Nombre = row["nombre"].ToString() ?? "",
                    Ap_Paterno = row["ap_paterno"].ToString() ?? "",
                    Ap_Materno = row["ap_materno"].ToString() ?? "",
                    RFC = row["rfc"].ToString() ?? "",
                    CURP = row["curp"].ToString() ?? "",
                    Direccion = row["direccion"].ToString() ?? "",
                    Telefono = row["telefono"].ToString() ?? "",
                    Email = row["email"].ToString() ?? "",
                    Fecha_Nacimiento = Convert.ToDateTime(row["fecha_nacimiento"]),
                    Genero = row["genero"].ToString() ?? "",
                    Estatus = Convert.ToInt16(row["estatus"])
                };

                // Crea el empleado
                Empleado empleado = new Empleado(
                    Convert.ToInt32(row["id_empleado"]),
                    Convert.ToInt32(row["id_persona"]),
                    Convert.ToDateTime(row["fecha_ingreso"]),
                    row["fecha_baja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_baja"]) : null,
                    Convert.ToInt32(row["id_departamento"]),
                    Convert.ToInt32(row["id_puesto"]),
                    row["matricula"].ToString() ?? "",
                    row["motivo"].ToString() ?? "",
                    Convert.ToInt16(row["estatus"]),
                    persona
                );

                return empleado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener empleado con ID {id}");
                return null;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Devuelve todos los empleados con sus datos personales
        /// </summary>
        public List<Empleado> ObtenerTodosLosEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                string query = @"
                    SELECT e.*, p.*
                    FROM human_resours.empleado e
                    INNER JOIN human_resours.persona p ON e.id_persona = p.id_persona
                    ORDER BY e.id_empleado";

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    Persona persona = new Persona
                    {
                        Id_Persona = Convert.ToInt32(row["id_persona"]),
                        Nombre = row["nombre"].ToString() ?? "",
                        Ap_Paterno = row["ap_paterno"].ToString() ?? "",
                        Ap_Materno = row["ap_materno"].ToString() ?? "",
                        RFC = row["rfc"].ToString() ?? "",
                        CURP = row["curp"].ToString() ?? "",
                        Direccion = row["direccion"].ToString() ?? "",
                        Telefono = row["telefono"].ToString() ?? "",
                        Email = row["email"].ToString() ?? "",
                        Fecha_Nacimiento = Convert.ToDateTime(row["fecha_nacimiento"]),
                        Genero = row["genero"].ToString() ?? "",
                        Estatus = Convert.ToInt16(row["estatus"])
                    };

                    Empleado empleado = new Empleado(
                        Convert.ToInt32(row["id_empleado"]),
                        Convert.ToInt32(row["id_persona"]),
                        Convert.ToDateTime(row["fecha_ingreso"]),
                        row["fecha_baja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_baja"]) : null,
                        Convert.ToInt32(row["id_departamento"]),
                        Convert.ToInt32(row["id_puesto"]),
                        row["matricula"].ToString() ?? "",
                        row["motivo"].ToString() ?? "",
                        Convert.ToInt16(row["estatus"]),
                        persona
                    );

                    empleados.Add(empleado);
                }

                _logger.Info($"Se encontraron {empleados.Count} empleados");
                return empleados;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener todos los empleados");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------Existe()

        /// <summary>
        /// Verifica si una matrícula ya existe
        /// </summary>
        public bool ExisteMatricula(string matricula)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM human_resours.empleado WHERE matricula = @Matricula";
                var param = _dbAccess.CreateParameter("@Matricula", matricula);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, param);
                int count = Convert.ToInt32(resultado);

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar matrícula: {matricula}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}
