using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Npgsql;
using RecursosHumanos.Models;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.Data
{
    public class PuestoDataAccess
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.PuestoDataAccess");
        private readonly PostgreSQLDataAccess _dbAccess = null;

        public PuestoDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar el acceso a datos de PuestoDataAccess");
                throw;
            }
        }

        public int InsertarPuesto(Puesto puesto)
        {
            try
            {
                string query = @"
INSERT INTO human_resours.puesto 
(nombre_puesto, descripcion_puesto, estatus)
VALUES 
(@NombrePuesto, @DescripcionPuesto, @Estatus)
RETURNING id_puesto";

                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@NombrePuesto", puesto.NombrePuesto),
                    _dbAccess.CreateParameter("@DescripcionPuesto", puesto.DescripcionPuesto),
                    _dbAccess.CreateParameter("@Estatus", puesto.Estatus ? 1 : 0)
                };

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parameters);
                int idPuesto = Convert.ToInt32(resultado);
                _logger.Info($"Puesto insertado con ID: {idPuesto}");

                return idPuesto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar el puesto: {puesto.NombrePuesto}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public List<Puesto> ObtenerTodosLosPuestos(bool soloActivos = true)
        {
            List<Puesto> puestos = new List<Puesto>();

            try
            {
                string query = @"
SELECT p.id_puesto, p.nombre_puesto, p.descripcion_puesto, p.estatus
FROM human_resours.puesto p
WHERE 1=1";

                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                if (soloActivos)
                {
                    query += " AND p.estatus = 1";
                }

                query += " ORDER BY p.id_puesto";

                _dbAccess.Connect();
                DataTable result = _dbAccess.ExecuteQuery_Reader(query, parameters.ToArray());

                foreach (DataRow row in result.Rows)
                {
                    Puesto puesto = new Puesto
                    {
                        IdPuesto = Convert.ToInt32(row["id_puesto"]),
                        NombrePuesto = row["nombre_puesto"].ToString() ?? "",
                        DescripcionPuesto = row["descripcion_puesto"].ToString() ?? "",
                        Estatus = Convert.ToInt32(row["estatus"]) == 1
                    };

                    puestos.Add(puesto);
                }

                _logger.Debug($"Se obtuvieron {puestos.Count} registros de puestos");
                return puestos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al intentar obtener la lista de puestos desde la base de datos");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public Puesto? ObtenerPuestoPorId(int idPuesto)
        {
            try
            {
                string query = @"
SELECT p.id_puesto, p.nombre_puesto, p.descripcion_puesto, p.estatus
FROM human_resours.puesto p
WHERE p.id_puesto = @IdPuesto";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@IdPuesto", idPuesto);

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, paramId);

                if (resultado.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontró ningún puesto con ID {idPuesto}");
                    return null;
                }

                DataRow row = resultado.Rows[0];

                Puesto puesto = new Puesto
                {
                    IdPuesto = Convert.ToInt32(row["id_puesto"]),
                    NombrePuesto = row["nombre_puesto"].ToString() ?? "",
                    DescripcionPuesto = row["descripcion_puesto"].ToString() ?? "",
                    Estatus = Convert.ToInt32(row["estatus"]) == 1
                };

                return puesto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener el puesto con ID {idPuesto}");
                return null;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public bool ActualizarPuesto(Puesto puesto)
        {
            try
            {
                _logger.Debug($"Actualizando puesto con ID {puesto.IdPuesto}");

                string query = @"
UPDATE human_resours.puesto
SET nombre_puesto = @NombrePuesto,
    descripcion_puesto = @DescripcionPuesto,
    estatus = @Estatus
WHERE id_puesto = @IdPuesto";

                NpgsqlParameter paramIdPuesto = _dbAccess.CreateParameter("@IdPuesto", puesto.IdPuesto);
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@NombrePuesto", puesto.NombrePuesto);
                NpgsqlParameter paramDescripcion = _dbAccess.CreateParameter("@DescripcionPuesto", puesto.DescripcionPuesto);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", puesto.Estatus ? 1 : 0);

                _dbAccess.Connect();
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query,
                    paramNombre, paramDescripcion, paramEstatus, paramIdPuesto);

                bool exito = filasAfectadas > 0;

                if (!exito)
                {
                    _logger.Warn($"No se pudo actualizar el puesto con ID {puesto.IdPuesto}. No se encontró el registro.");
                }
                else
                {
                    _logger.Debug($"Puesto con ID {puesto.IdPuesto} actualizado correctamente.");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el puesto con ID {puesto.IdPuesto}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public bool EliminarPuestoLogico(int idPuesto)
        {
            try
            {
                _logger.Debug($"Eliminando lógicamente el Puesto con ID {idPuesto}");

                string query = @"
UPDATE human_resours.puesto
SET estatus = 0
WHERE id_Puesto = @IdPuesto";

                NpgsqlParameter paramIdDepartamento = _dbAccess.CreateParameter("@IdPuesto", idPuesto);

                _dbAccess.Connect();
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, paramIdDepartamento);

                bool exito = filasAfectadas > 0;

                if (!exito)
                {
                    _logger.Warn($"No se pudo eliminar lógicamente el puesto con ID {idPuesto}. No se encontró el registro.");
                }
                else
                {
                    _logger.Debug($"Puesto con ID {idPuesto} eliminado lógicamente.");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar lógicamente el departamento con ID {idPuesto}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}
