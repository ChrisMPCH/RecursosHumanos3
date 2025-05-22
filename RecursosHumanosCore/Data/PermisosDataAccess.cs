using System;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using NLog;
using Npgsql;
using System.Data;

namespace RecursosHumanos.Data
{
    public class PermisosDataAccess
    {
        // Logger para registrar eventos, errores e información de esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.PermisosDataAccess");

        // Instancia del acceso a la base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Constructor: inicializa la conexión a PostgreSQL
        public PermisosDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Error al inicializar PermisosDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Devuelve todos los permisos
        /// </summary>
        public List<Permiso> ObtenerTodosLosPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();

            try
            {
                string query = "SELECT * FROM administration.permisos ORDER BY id_permiso";

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    Permiso permiso = new Permiso
                    {
                        Id_Permiso = Convert.ToInt32(row["id_permiso"]),
                        Codigo = row["codigo"].ToString() ?? "",
                        Nombre = row["nombre"].ToString() ?? "",
                        Descripcion = row["descripcion"].ToString() ?? "",
                        Estatus = Convert.ToInt16(row["estatus"])
                    };

                    permisos.Add(permiso);
                }

                _logger.Info($"Se encontraron {permisos.Count} permisos.");
                return permisos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener lista de permisos");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        //-----------------------------------------------------------------------------------------------------------------Existe()

    }
}
