using System;
using Npgsql;
using NLog;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.Models;
using RecursosHumanosCore.Utilities;
using System.Data;

namespace RecursosHumanosCore.DataAccess
{
    public class AusenciaDataAccess
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.AusenciaDataAccess");
        private readonly PostgreSQLDataAccess _dbAccess;

        public AusenciaDataAccess()
        {
            _dbAccess = PostgreSQLDataAccess.GetInstance();
        }

        public bool InsertarAusencia(Ausencia ausencia)
        {
            try
            {
                _dbAccess.Connect();

                string query = @"INSERT INTO human_resours.ausencias 
                                (fecha_ausencias, motivo_ausencia, id_empleado, estatus) 
                                VALUES (@fecha, @motivo, @empleado, @estatus)";

                var parametros = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@fecha", ausencia.FechaAusencias.Date),
                    _dbAccess.CreateParameter("@motivo", ausencia.MotivoAusencia ?? "Retardo mayor al permitido"),
                    _dbAccess.CreateParameter("@empleado", ausencia.IdEmpleado),
                    _dbAccess.CreateParameter("@estatus", ausencia.Estatus)
                };

                int filas = _dbAccess.ExecuteNonQuery(query, parametros);

                _logger.Info($"Se insertó ausencia para empleado {ausencia.IdEmpleado} en fecha {ausencia.FechaAusencias}. Filas afectadas: {filas}");

                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al insertar ausencia.");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public bool ExisteAusenciaHoy(int idEmpleado, DateTime fecha)
        {
            try
            {
                _dbAccess.Connect();

                string query = @"SELECT COUNT(*) FROM human_resours.ausencias 
                         WHERE id_empleado = @empleado 
                         AND fecha_ausencias = @fecha 
                         AND estatus = 1";

                var parametros = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@empleado", idEmpleado),
            _dbAccess.CreateParameter("@fecha", fecha.Date)
                };

                object resultado = _dbAccess.ExecuteScalar(query, parametros);
                int conteo = Convert.ToInt32(resultado);

                _logger.Info($"Validación de ausencia para empleado {idEmpleado} en fecha {fecha.Date}: {conteo} registros encontrados.");

                return conteo > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al validar existencia de ausencia.");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public List<Ausencia> ObtenerAusencias()
        {
            List<Ausencia> ausencias = new List<Ausencia>();

            string query = @"
            SELECT 
        e.matricula,
        p.nombre || ' ' || p.ap_paterno || ' ' || p.ap_materno AS nombre_empleado,
        a.id_ausencias,
        a.fecha_ausencias,
        a.motivo_ausencia,
        a.id_empleado,
        a.estatus
            FROM human_resours.ausencias a
            INNER JOIN human_resours.empleado e ON e.id_empleado = a.id_empleado
            INNER JOIN human_resours.persona p ON p.id_persona = e.id_persona
            WHERE a.estatus = 1
            ORDER BY a.fecha_ausencias DESC;";

            try
            {
                _dbAccess.Connect();

                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    Ausencia ausencia = new Ausencia
                    {
                        IdAusencias = Convert.ToInt32(row["id_ausencias"]),
                        FechaAusencias = Convert.ToDateTime(row["fecha_ausencias"]),
                        MotivoAusencia = row["motivo_ausencia"].ToString() ?? string.Empty,
                        IdEmpleado = Convert.ToInt32(row["id_empleado"]),
                        Estatus = Convert.ToInt16(row["estatus"]),
                        Matricula = row["matricula"].ToString() ?? string.Empty,
                        Nombre = row["nombre_empleado"].ToString() ?? string.Empty
                    };

                    ausencias.Add(ausencia);
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías usar NLog también
                Console.WriteLine($"Error al obtener las ausencias: {ex.Message}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }

            return ausencias;
        }
    }
}
