using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        {

        /// <summary>
        /// Obtiene un empleado y su persona asociada por ID
        /// </summary>
        public Empleado? ObtenerEmpleadoPorId(int id)
        {
            try
            {

                {
                }


                {
                    {
                }

            }
            catch (Exception ex)
            {
            }

        }
    }
}
