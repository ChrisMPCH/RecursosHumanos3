using System;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using NLog;
using Npgsql;
using System.Data;

namespace RecursosHumanos.Data
{
    public class RolesDataAccess
    {
        // Logger para registrar eventos, errores e información de esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.RolesDataAccess");

        // Instancia del acceso a la base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Constructor: inicializa la conexión a PostgreSQL
        public RolesDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Error al inicializar RolesDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Inserta un nuevo rol en la base de datos
        /// </summary>
        public int InsertarRol(Rol rol)
        {
            try
            {
                string query = @"
                    INSERT INTO administration.roles (codigo, nombre, descripcion, estatus)
                    VALUES (@Codigo, @Nombre, @Descripcion, @Estatus)
                    RETURNING id_rol";

                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@Codigo", rol.Codigo),
                    _dbAccess.CreateParameter("@Nombre", rol.Nombre),
                    _dbAccess.CreateParameter("@Descripcion", rol.Descripcion),
                    _dbAccess.CreateParameter("@Estatus", rol.Estatus)
                };

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);
                int idGenerado = Convert.ToInt32(resultado);

                _logger.Info($"Rol insertado correctamente con ID {idGenerado}");
                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar rol: {rol.Nombre}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Actualiza un rol existente
        /// </summary>
        public bool ActualizarRol(Rol rol)
        {
            try
            {
                string query = @"
                    UPDATE administration.roles
                    SET codigo = @Codigo,
                        nombre = @Nombre,
                        descripcion = @Descripcion,
                        estatus = @Estatus
                    WHERE id_rol = @Id";

                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@Id", rol.Id_Rol),
                    _dbAccess.CreateParameter("@Codigo", rol.Codigo),
                    _dbAccess.CreateParameter("@Nombre", rol.Nombre),
                    _dbAccess.CreateParameter("@Descripcion", rol.Descripcion),
                    _dbAccess.CreateParameter("@Estatus", rol.Estatus)
                };

                _dbAccess.Connect();
                int filas = _dbAccess.ExecuteNonQuery(query, parametros);

                _logger.Info($"Rol actualizado ID {rol.Id_Rol}, filas afectadas: {filas}");
                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar rol ID {rol.Id_Rol}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Elimina un rol por ID
        /// </summary>
        public bool EliminarRol(int id)
        {
            try
            {
                string query = "UPDATE administration.roles SET estatus = 0 WHERE id_rol = @Id";
                var parametro = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();
                int filas = _dbAccess.ExecuteNonQuery(query, parametro);

                _logger.Info($"Rol eliminado ID {id}, filas afectadas: {filas}");
                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar rol ID {id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene todos los roles registrados
        /// </summary>
        public List<Rol> ObtenerTodosLosRoles()
        {
            List<Rol> roles = new List<Rol>();

            try
            {
                string query = "SELECT * FROM administration.roles ORDER BY id_rol";

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    Rol rol = new Rol
                    {
                        Id_Rol = Convert.ToInt32(row["id_rol"]),
                        Codigo = row["codigo"].ToString() ?? "",
                        Nombre = row["nombre"].ToString() ?? "",
                        Descripcion = row["descripcion"].ToString() ?? "",
                        Estatus = Convert.ToInt16(row["estatus"])
                    };

                    roles.Add(rol);
                }

                _logger.Info($"Se encontraron {roles.Count} roles.");
                return roles;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener lista de roles");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------Existe()
        /// <summary>
        /// Verifica si ya existe un rol con el mismo nombre
        /// </summary>
        public bool ExisteNombreRol(string nombre)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM administration.roles WHERE nombre = @Nombre";
                var parametro = _dbAccess.CreateParameter("@Nombre", nombre);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametro);
                int count = Convert.ToInt32(resultado);
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar existencia del rol por nombre: {nombre}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si ya existe un rol con el mismo código
        /// </summary>
        public bool ExisteCodigoRol(string codigo)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM administration.roles WHERE codigo = @Codigo";
                var parametro = _dbAccess.CreateParameter("@Codigo", codigo);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametro);
                int count = Convert.ToInt32(resultado);
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar existencia del rol por código: {codigo}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}