using RecursosHumanos.Models;
using System;
using System.Data;
using Npgsql;
using RecursosHumanos.Data;
using NLog;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.DataAccess
{
    public class AsistenciaDataAccess
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.AsistenciaDataAccess");
        private readonly PostgreSQLDataAccess _dbAccess;

        // Constructor to initialize _dbAccess  
        public AsistenciaDataAccess()
        {
            _dbAccess = PostgreSQLDataAccess.GetInstance();
        }

        public static bool InsertarAsistencia(Asistencia a)
        {
            var db = PostgreSQLDataAccess.GetInstance();

            string query = @"INSERT INTO human_resours.asistencia (fecha_asistencia, hora_entrada, id_empleado, estatus)  
                        VALUES (@fecha, @hora, @empleado, @estatus)";

            var parametros = new NpgsqlParameter[]
            {
                   db.CreateParameter("@fecha", a.FechaAsistencia),
                   db.CreateParameter("@hora", a.HoraEntrada),
                   db.CreateParameter("@empleado", a.IdEmpleado),
                   db.CreateParameter("@estatus", a.Estatus)
            };

            int filasAfectadas = db.ExecuteNonQuery(query, parametros);

            return filasAfectadas > 0;
        }

        public static bool RegistrarSalida(int idEmpleado, DateTime fecha, TimeSpan horaSalida)
        {
            var db = PostgreSQLDataAccess.GetInstance();

            if (!db.Connect())
            {
                return false;
            }

            string query = @"UPDATE human_resours.asistencia  
                        SET hora_salida = @salida  
                        WHERE id_empleado = @empleado AND fecha_asistencia = @fecha";

            var parametros = new NpgsqlParameter[]
            {
                   db.CreateParameter("@salida", horaSalida),
                   db.CreateParameter("@empleado", idEmpleado),
                   db.CreateParameter("@fecha", fecha)
            };

            int filasAfectadas = db.ExecuteNonQuery(query, parametros);

            db.Disconnect();

            return filasAfectadas > 0;
        }

        public (int asistenciasHoy, int empleadosActivos) ContarAsistenciasHoy()
        {
            try
            {
                // Contar asistencias de hoy (solo registros activos)  
                string queryAsistencias = @"  
    SELECT COUNT(*)   
    FROM human_resours.asistencia  
    WHERE Fecha_Asistencia = CURRENT_DATE  
    AND Estatus = true";

                // Contar empleados activos  
                string queryEmpleados = @"  
    SELECT COUNT(*)   
    FROM human_resours.empleado  
    WHERE estatus = true";

                _dbAccess.Connect();

                // Contar asistencias de hoy  
                object? asistenciasResultado = _dbAccess.ExecuteScalar(queryAsistencias);
                int asistenciasHoy = Convert.ToInt32(asistenciasResultado);

                // Contar empleados activos  
                object? empleadosResultado = _dbAccess.ExecuteScalar(queryEmpleados);
                int empleadosActivos = Convert.ToInt32(empleadosResultado);

                _logger.Debug($"Se encontraron {asistenciasHoy} asistencias hoy y {empleadosActivos} empleados activos.");
                return (asistenciasHoy, empleadosActivos);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al contar las asistencias de hoy");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}