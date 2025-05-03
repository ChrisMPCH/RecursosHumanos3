using System;
using Npgsql;
using NLog;
using RecursosHumanos.Data;
using RecursosHumanos.Models;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.DataAccess
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

    }
}
