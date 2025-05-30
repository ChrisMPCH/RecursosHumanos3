using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NLog;
using RecursosHumanosCore.Utilities;
using RecursosHumanosCore.Model;

namespace RecursosHumanosCore.Data
{
    public class EmpleadosDataAccess
    {
        // Logger para registrar errores y eventos
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.EmpleadosDataAccess");

        // Acceso a base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Acceso a datos de personas (para cargar la relación)
        private readonly PersonasDataAccess _personasData;

        // Constructor: inicializa conexiones
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
        /// Devuelve la lista de todos los empleados del sistema
        /// </summary>
        /// <returns>Lista de empleados</returns>
        public List<Empleado> ObtenerTodosLosEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                string query = @"
            SELECT
              e.id_empleado,
              e.id_persona,
              e.fecha_ingreso,
              e.fecha_baja,
              e.id_departamento,
              e.id_puesto,
              e.matricula,
              e.motivo,
              e.estatus,
              p.nombre,
              p.ap_paterno,
              p.ap_materno,
              d.nombre_departamento,
              pu.nombre_puesto
            FROM
              human_resours.empleado e  
            INNER JOIN
              human_resours.persona p ON e.id_persona = p.id_persona
            INNER JOIN
              human_resours.departamento d ON e.id_departamento = d.id_departamento
            INNER JOIN
              human_resours.puesto pu ON e.id_puesto = pu.id_puesto;
        ";

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    Persona persona = new Persona
                    {
                        Id_Persona = Convert.ToInt32(row["id_persona"]),
                        Nombre = row["nombre"]?.ToString() ?? "",
                        Ap_Paterno = row["ap_paterno"]?.ToString() ?? "",
                        Ap_Materno = row["ap_materno"]?.ToString() ?? ""
                    };

                    Empleado empleado = new Empleado
                    {
                        Id_Empleado = Convert.ToInt32(row["id_empleado"]),
                        Id_Persona = Convert.ToInt32(row["id_persona"]),
                        Fecha_Ingreso = Convert.ToDateTime(row["fecha_ingreso"]),
                        Fecha_Baja = row["fecha_baja"] != DBNull.Value ? Convert.ToDateTime(row["fecha_baja"]) : (DateTime?)null,
                        Id_Departamento = Convert.ToInt32(row["id_departamento"]),
                        Id_Puesto = Convert.ToInt32(row["id_puesto"]),
                        Matricula = row["matricula"]?.ToString() ?? "",
                        Motivo = row["motivo"]?.ToString() ?? "",
                        Estatus = Convert.ToInt16(row["estatus"]),
                        DatosPersonales = persona,
                        Nombre = $"{persona.Nombre} {persona.Ap_Paterno} {persona.Ap_Materno}",
                        Departamento = row["nombre_departamento"]?.ToString() ?? "",
                        Puesto = row["nombre_puesto"]?.ToString() ?? "",
                        EstatusTexto = Convert.ToInt16(row["estatus"]) == 1 ? "Activo" : "Inactivo"
                    };

                    empleados.Add(empleado);
                }

                _logger.Info($"Se obtuvieron {empleados.Count} empleados.");
                return empleados;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de empleados.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }



        /// <summary>
        /// Inserta un nuevo empleado con su persona asociada
        /// </summary>
        /// <param name="empleado">Objeto Empleado con los datos completos</param>
        /// <returns>ID del nuevo usuario o -1 si falla</returns>
        public int InsertarEmpleado(Empleado empleado)
        {
            try
            {
                // Primero, insertar el empleado en la tabla "empleado"
                string query = @"
                INSERT INTO human_resours.empleado 
                      (id_persona, fecha_ingreso, fecha_baja, id_departamento, id_puesto, matricula, motivo, estatus)
                VALUES 
                      (@IdPersona, @FechaIngreso, @FechaBaja, @IdDepartamento, @IdPuesto, @Matricula, @Motivo, @Estatus)
                RETURNING id_empleado;";

                // Crear los parámetros para la inserción en la tabla "empleado"
                var parametrosEmpleado = new[]
                {
                    _dbAccess.CreateParameter("@IdPersona", empleado.Id_Persona),
                    _dbAccess.CreateParameter("@FechaIngreso", empleado.Fecha_Ingreso),
                    _dbAccess.CreateParameter("@FechaBaja", empleado.Fecha_Baja.HasValue ? (object)empleado.Fecha_Baja.Value : DBNull.Value), // Permitir NULL si no tiene fecha de baja
                    _dbAccess.CreateParameter("@IdDepartamento", empleado.Id_Departamento),
                    _dbAccess.CreateParameter("@IdPuesto", empleado.Id_Puesto),
                    _dbAccess.CreateParameter("@Matricula", empleado.Matricula),
                    _dbAccess.CreateParameter("@Motivo", empleado.Motivo),
                    _dbAccess.CreateParameter("@Estatus", empleado.Estatus)
                };

                // Ejecutar la inserción
                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametrosEmpleado);

                int idGenerado = Convert.ToInt32(resultado);
                _logger.Info($"Usuario insertado correctamente con ID {idGenerado}");

                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar usuario: {empleado.Matricula}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Actualiza los datos del empleado y su persona asociada.
        /// </summary>
        /// <param name="empleado">Objeto empleado con los datos actualizados</param>
        /// <returns>True si se actualizó correctamente</returns>
        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                _logger.Debug($"Actualizando usuario con ID {empleado.Id_Empleado} y persona ID {empleado.Id_Persona}");

                // Actualiza primero la persona
                bool actualizacionPersonaExitosa = _personasData.ActualizarPersona(empleado.DatosPersonales);

                if (!actualizacionPersonaExitosa)
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {empleado.Id_Persona}");
                    return false;
                }

                // Primero, actualizar los datos en la tabla "empleado"
                string query = @"
                UPDATE human_resours.empleado
                SET fecha_ingreso = @FechaIngreso,
                      fecha_baja = @FechaBaja,
                      id_departamento = @IdDepartamento,
                      id_puesto = @IdPuesto,
                      matricula = @Matricula,
                      motivo = @Motivo,
                      estatus = @Estatus
                 WHERE id_empleado = @IdEmpleado";

                // Crear los parámetros para la consulta "empleado"
                var parametrosEmpleado = new[]
                {
                    _dbAccess.CreateParameter("@IdEmpleado", empleado.Id_Empleado),
                    _dbAccess.CreateParameter("@FechaIngreso", empleado.Fecha_Ingreso),
                    _dbAccess.CreateParameter("@FechaBaja", empleado.Fecha_Baja.HasValue ? (object)empleado.Fecha_Baja.Value : DBNull.Value), // Permitir NULL si no tiene fecha de baja
                    _dbAccess.CreateParameter("@IdDepartamento", empleado.Id_Departamento),
                    _dbAccess.CreateParameter("@IdPuesto", empleado.Id_Puesto),
                    _dbAccess.CreateParameter("@Matricula", empleado.Matricula),
                    _dbAccess.CreateParameter("@Motivo", empleado.Motivo),
                    _dbAccess.CreateParameter("@Estatus", empleado.Estatus)
                };

                _dbAccess.Connect();
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, parametrosEmpleado);

                bool exito = filasAfectadas > 0;

                if (exito)
                {
                    _logger.Info($"Usuario con ID {empleado.Id_Empleado} actualizado correctamente.");
                }
                else
                {
                    _logger.Warn($"No se encontró usuario con ID {empleado.Id_Empleado} para actualizar.");
                }

                _logger.Debug($"Filas afectadas: {filasAfectadas}");
                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el usuario con ID {empleado.Id_Empleado}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Elimina un empleado de la base de datos por su ID
        /// </summary>
        /// <param name="idEmpleado">ID del empleado a eliminar</param>
        /// <returns>True si se eliminó correctamente</returns>
        public bool EliminarUsuario(int idEmpleado)
        {
            try
            {
                // Preparar la consulta para actualizar el estatus del empleado a 0 (eliminado)
                string query = "UPDATE human_resours.empleado SET estatus = 0 WHERE id_empleado = @IdEmpleado";
                var parametros = new[] { _dbAccess.CreateParameter("@IdEmpleado", idEmpleado) };

                _dbAccess.Connect();
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, parametros);

                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar el usuario con ID {idEmpleado}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si existe un empleado con la matrícula proporcionada.
        /// </summary>
        /// <param name="matricula">Matrícula del empleado a verificar</param>
        /// <returns>True si existe un empleado con esa matrícula</returns>
        public bool ExisteEmpleadoPorMatricula(string matricula)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM human_resours.empleado WHERE matricula = @Matricula";
                var parametro = new[] { _dbAccess.CreateParameter("@Matricula", matricula) };

                _dbAccess.Connect();
                object resultado = _dbAccess.ExecuteScalar(query, parametro);

                return Convert.ToInt32(resultado) > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia de la matrícula {matricula}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Busca un empleado por su matrícula, incluyendo datos personales, departamento y puesto.
        /// </summary>
        public Empleado ObtenerEmpleadoPorMatricula(string matricula)
        {
            try
            {
                const string query = @"
                    SELECT
                        e.id_empleado,
                        e.id_persona,
                        e.fecha_ingreso,
                        e.fecha_baja,
                        e.id_departamento,
                        e.id_puesto,
                        e.matricula,
                        e.estatus,
                        p.nombre, p.ap_paterno, p.ap_materno, p.rfc, p.curp,
                        p.direccion, p.telefono, p.email, p.fecha_nacimiento, p.genero,
                        d.nombre_departamento,
                        pu.nombre_puesto
                    FROM human_resours.empleado e
                    INNER JOIN human_resours.persona p ON e.id_persona = p.id_persona
                    INNER JOIN human_resours.departamento d ON e.id_departamento = d.id_departamento
                    INNER JOIN human_resours.puesto pu ON e.id_puesto = pu.id_puesto
                    WHERE e.matricula = @matricula;
                ";

                var parametros = new[]
                {
                    new NpgsqlParameter("@matricula", matricula)
                };

                _dbAccess.Connect();
                DataTable dt = _dbAccess.ExecuteQuery_Reader(query, parametros);

                if (dt.Rows.Count == 0)
                    return null;

                DataRow row = dt.Rows[0];

                var persona = new Persona
                {
                    Id_Persona = Convert.ToInt32(row["id_persona"]),
                    Nombre = row["nombre"]?.ToString() ?? "",
                    Ap_Paterno = row["ap_paterno"]?.ToString() ?? "",
                    Ap_Materno = row["ap_materno"]?.ToString() ?? "",
                    RFC = row["rfc"]?.ToString() ?? "",
                    CURP = row["curp"]?.ToString() ?? "",
                    Direccion = row["direccion"]?.ToString() ?? "",
                    Telefono = row["telefono"]?.ToString() ?? "",
                    Email = row["email"]?.ToString() ?? "",
                    Fecha_Nacimiento = Convert.ToDateTime(row["fecha_nacimiento"]),
                    Genero = row["genero"]?.ToString() ?? ""
                };

                var empleado = new Empleado
                {
                    Id_Empleado = Convert.ToInt32(row["id_empleado"]),
                    Id_Persona = persona.Id_Persona,
                    Fecha_Ingreso = Convert.ToDateTime(row["fecha_ingreso"]),
                    Fecha_Baja = row["fecha_baja"] != DBNull.Value
                                      ? Convert.ToDateTime(row["fecha_baja"])
                                      : (DateTime?)null,
                    Id_Departamento = Convert.ToInt32(row["id_departamento"]),
                    Id_Puesto = Convert.ToInt32(row["id_puesto"]),
                    Matricula = row["matricula"]?.ToString() ?? "",
                    Estatus = Convert.ToInt16(row["estatus"]),
                    DatosPersonales = persona,
                    Nombre = $"{persona.Nombre} {persona.Ap_Paterno} {persona.Ap_Materno}",
                    Departamento = row["nombre_departamento"]?.ToString() ?? "",
                    Puesto = row["nombre_puesto"]?.ToString() ?? "",
                    EstatusTexto = Convert.ToInt16(row["estatus"]) == 1 ? "Activo" : "Inactivo"
                };

                return empleado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener empleado por matrícula.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public (int totalEmpleados, int empleadosActivos) ContarEmpleados()
        {
            try
            {
                string queryTotal = @"
                SELECT COUNT(*) 
                FROM human_resours.empleado
                WHERE 1=1";

                        string queryActivos = @"
                SELECT COUNT(*) 
                FROM human_resours.empleado
                WHERE estatus = 1";

                _dbAccess.Connect();

                // Contar total de empleados
                object? totalResultado = _dbAccess.ExecuteScalar(queryTotal);
                int totalEmpleados = Convert.ToInt32(totalResultado);

                // Contar empleados activos
                object? activosResultado = _dbAccess.ExecuteScalar(queryActivos);
                int empleadosActivos = Convert.ToInt32(activosResultado);

                _logger.Debug($"Se encontraron {totalEmpleados} empleados en total, de los cuales {empleadosActivos} están activos.");
                return (totalEmpleados, empleadosActivos);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al contar los empleados");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

    }
}
