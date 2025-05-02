using RecursosHumanos.Models;
using System;
using System.Data;
using Npgsql;
using RecursosHumanos.Data;

namespace RecursosHumanos.DataAccess
{
    public class AsistenciaDataAccess
    {
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

    }
}