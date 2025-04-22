using System;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using NLog;
using Npgsql;
using System.Data;

namespace RecursosHumanos.Data
{
    public class RolesPermisosDataAccess
    {
        // Logger para registrar eventos, errores e información de esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.RolesPermisosDataAccess");

        // Instancia del acceso a la base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Acceso a datos de Roles (para cargar la relación)
        private readonly RolesDataAccess _rolesData;

        // Acceso a datos de Permisos (para cargar la relación)
        private readonly PermisosDataAccess _permisosData;

        // Constructor: inicializa la conexión a PostgreSQL
        public RolesPermisosDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Error al inicializar RolesPermisosDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Inserta una nueva relación rol-permiso
        /// </summary>
        public int InsertarRolPermiso(RolPermiso rolPermiso)
        {
            try
            {
                string query = @"
                    INSERT INTO administration.roles_permisos (id_rol, id_permiso)
                    VALUES (@IdRol, @IdPermiso)
                    RETURNING id_roles_permisos";

                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@IdRol", rolPermiso.Id_Rol),
                    _dbAccess.CreateParameter("@IdPermiso", rolPermiso.Id_Permiso)
                };

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);
                int idGenerado = Convert.ToInt32(resultado);

                _logger.Info($"RolPermiso insertado correctamente con ID {idGenerado}");
                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar la relación rol-permiso");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Actualiza una relación existente de rol-permiso
        /// </summary>
        public bool ActualizarRolPermiso(RolPermiso rolPermiso)
        {
            try
            {
                string query = @"
                    UPDATE administration.roles_permisos
                    SET id_rol = @IdRol,
                        id_permiso = @IdPermiso
                    WHERE id_roles_permisos = @Id";

                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@Id", rolPermiso.Id_Roles_Permisos),
                    _dbAccess.CreateParameter("@IdRol", rolPermiso.Id_Rol),
                    _dbAccess.CreateParameter("@IdPermiso", rolPermiso.Id_Permiso)
                };

                _dbAccess.Connect();
                int filas = _dbAccess.ExecuteNonQuery(query, parametros);

                _logger.Info($"RolPermiso actualizado ID {rolPermiso.Id_Roles_Permisos}, filas afectadas: {filas}");
                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar rol-permiso ID {rolPermiso.Id_Roles_Permisos}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Elimina una relación rol-permiso por su ID
        /// </summary>
        public bool EliminarRolPermiso(int id)
        {
            try
            {
                string query = "UPDATE administration.roles_permisos SET estatus = 0 WHERE id_roles_permisos = @Id";
                var param = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();
                int filas = _dbAccess.ExecuteNonQuery(query, param);

                _logger.Info($"RolPermiso eliminado ID {id}, filas afectadas: {filas}");
                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar rol-permiso ID {id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Devuelve todas las relaciones rol-permiso
        /// </summary>
        public List<RolPermiso> ObtenerTodosRolesPermisos()
        {
            List<RolPermiso> lista = new List<RolPermiso>();

            try
            {
                string query = "SELECT * FROM administration.roles_permisos ORDER BY id_roles_permisos";

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    RolPermiso item = new RolPermiso
                    {
                        Id_Roles_Permisos = Convert.ToInt32(row["id_roles_permisos"]),
                        Id_Rol = Convert.ToInt32(row["id_rol"]),
                        Id_Permiso = Convert.ToInt32(row["id_permiso"])
                    };

                    lista.Add(item);
                }

                _logger.Info($"Se encontraron {lista.Count} relaciones rol-permiso.");
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener lista de roles-permisos");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Devuelve todos los permisos asignados a un rol
        /// </summary>
        public List<Permiso> ObtenerPermisosPorRol(int idRol)
        {
            List<Permiso> permisos = new List<Permiso>();

            try
            {
                string query = @"
            SELECT p.*
            FROM administration.permisos p
            INNER JOIN administration.roles_permisos rp ON rp.id_permiso = p.id_permiso
            WHERE rp.id_rol = @IdRol";

                var param = _dbAccess.CreateParameter("@IdRol", idRol);

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, param);

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

                _logger.Info($"Se encontraron {permisos.Count} permisos para el rol ID {idRol}");
                return permisos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener los permisos del rol ID {idRol}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------Existe()

        /// <summary>
        /// Verifica si una relación ya existe (por rol y permiso)
        /// </summary>
        public bool ExisteRelacion(int idRol, int idPermiso)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM administration.roles_permisos 
                    WHERE id_rol = @IdRol AND id_permiso = @IdPermiso";

                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@IdRol", idRol),
                    _dbAccess.CreateParameter("@IdPermiso", idPermiso)
                };

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);
                int count = Convert.ToInt32(resultado);
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar existencia de rol-permiso ({idRol}, {idPermiso})");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}