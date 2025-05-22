using System;
using System.Collections.Generic;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using NLog;

namespace RecursosHumanos.Controller
{
    public class RolesController
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.RolesController");

        private readonly RolesDataAccess _rolesData;
        private readonly RolesPermisosDataAccess _rolesPermisosData;
        private static readonly AuditoriasController _auditoriasController = new AuditoriasController();

        public RolesController()
        {
            try
            {
                _rolesData = new RolesDataAccess();
                _rolesPermisosData = new RolesPermisosDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error al inicializar RolesController");
                throw;
            }
        }
        /// <summary>
        /// Obtiene la lista de todos los roles registrados en la base de datos
        /// </summary>
        /// <returns>Da la lista con los roles de la BD</returns>
        public List<Rol> ObtenerRoles()
        {
            try
            {
                return _rolesData.ObtenerTodosLosRoles();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener roles desde el controlador a la base de datos");
                return new List<Rol>();
            }
        }
        /// <summary>
        /// Crea un nuevo rol con la lista de permisos asignados
        /// </summary>
        /// <param name="rol">Objeto rol a crear</param>
        /// <param name="permisosIds">Lista de IDs de permisos seleccionados</param>
        public (bool exito, string mensaje) CrearRolConPermisos(Rol rol, List<int> permisosIds)
        {
            try
            {
                // Validar existencia por nombre o código
                if (_rolesData.ExisteNombreRol(rol.Nombre))
                    return (false, $"El nombre de rol '{rol.Nombre}' ya existe.");

                if (_rolesData.ExisteCodigoRol(rol.Codigo))
                    return (false, $"El código de rol '{rol.Codigo}' ya existe.");

                // Insertar rol
                int idRol = _rolesData.InsertarRol(rol);
                if (idRol <= 0)
                    return (false, "No se pudo insertar el rol en la base de datos.");

                _logger.Info($"Rol creado con ID {idRol}, asignando permisos...");

                // Insertar permisos en roles_permisos
                foreach (int idPermiso in permisosIds)
                {
                    if (!_rolesPermisosData.ExisteRelacion(idRol, idPermiso))
                    {
                        var relacion = new RolPermiso
                        {
                            Id_Rol = idRol,
                            Id_Permiso = idPermiso
                        };
                        _rolesPermisosData.InsertarRolPermiso(relacion);
                    }
                }
                _auditoriasController.RegistrarAuditoriaGenerica(4,1, (short)idRol);

                return (true, "Rol y permisos asignados correctamente.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear rol con permisos.");
                return (false, "Error inesperado al crear el rol.");
            }
        }
        /// <summary>
        /// Obtiene un rol específico por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>El rol</returns>
        public Rol? ObtenerRolPorCodigo(string codigo)
        {
            try
            {
                return _rolesData.ObtenerRolPorCodigo(codigo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error desde el controlador al buscar rol por código: {codigo}");
                return null;
            }
        }
        /// <summary>
        /// Actualiza un rol existente y sus permisos
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="nuevosPermisos"></param>
        /// <returns></returns>
        public (bool exito, string mensaje) ActualizarRolConPermisos(Rol rol, List<int> nuevosPermisos)
        {
            try
            {
                // Verificar que exista
                var existente = _rolesData.ObtenerRolPorCodigo(rol.Codigo);
                if (existente == null)
                {
                    return (false, "El rol no existe en la base de datos.");
                }

                rol.Id_Rol = existente.Id_Rol;

                // Actualizar datos del rol
                bool actualizado = _rolesData.ActualizarRol(rol);
                if (!actualizado)
                {
                    return (false, "No se pudo actualizar el rol.");
                }

                // Eliminar todas las relaciones anteriores
                var relacionesAnteriores = _rolesPermisosData.ObtenerPermisosPorRol(rol.Id_Rol);
                foreach (var permiso in relacionesAnteriores)
                {
                    _rolesPermisosData.EliminarRolPermisoPorRelacion(rol.Id_Rol, permiso.Id_Permiso);
                }

                // Insertar nuevas relaciones
                foreach (int idPermiso in nuevosPermisos)
                {
                    if (!_rolesPermisosData.ExisteRelacion(rol.Id_Rol, idPermiso))
                    {
                        var nuevaRelacion = new RolPermiso
                        {
                            Id_Rol = rol.Id_Rol,
                            Id_Permiso = idPermiso
                        };
                        _rolesPermisosData.InsertarRolPermiso(nuevaRelacion);
                    }
                }
                _auditoriasController.RegistrarAuditoriaGenerica(4, 3, (short)rol.Id_Rol);

                return (true, "Rol actualizado correctamente con sus permisos.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el rol con permisos.");
                return (false, "Ocurrió un error inesperado.");
            }
        }
        /// <summary>
        /// Elimina un rol de forma lógica (soft delete) y elimina sus permisos asociados
        /// </summary>
        /// <param name="idRol"></param>
        /// <returns></returns>
        public (bool exito, string mensaje) EliminarRol(int idRol)
        {
            try
            {
                bool desactivado = _rolesData.DesactivarRol(idRol);
                if (!desactivado)
                {
                    return (false, "No se pudo desactivar el rol.");
                }

                bool permisosEliminados = _rolesPermisosData.EliminarPermisosDeRol(idRol);
                _auditoriasController.RegistrarAuditoriaGenerica(4, 2, (short)idRol);

                return (true, "Rol eliminado correctamente (soft delete).");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar el rol con ID {idRol}");
                return (false, "Error inesperado al eliminar el rol.");
            }
        }
        /// <summary>
        /// Obtiene la lista de roles activos
        /// </summary>
        /// <returns></returns>
        public List<Rol> ObtenerRolesActivos()
        {
            try
            {
                return _rolesData.ObtenerTodosLosRoles();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error desde RolesController al obtener roles activos");
                return new List<Rol>();
            }
        }

        public bool ExportarRolesExcel()
        {
            try
            {
                var roles = ObtenerRoles();

                if (roles == null || roles.Count == 0)
                {
                    _logger.Warn("No se encontraron roles para exportar.");
                    MessageBox.Show("No se encontraron roles para exportar.", "Exportación Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Nombre y ruta del archivo Excel
                var nombreArchivo = $"Roles_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "exportados", nombreArchivo);

                var carpetaDestino = Path.GetDirectoryName(rutaArchivo);
                if (!Directory.Exists(carpetaDestino))
                    Directory.CreateDirectory(carpetaDestino);

                // Exportación genérica
                bool resultado = ExcelExporter.ExportToExcel(roles, rutaArchivo, "Roles");

                if (resultado)
                {
                    _logger.Info($"Archivo exportado correctamente a {rutaArchivo}");
                    MessageBox.Show("La exportación a Excel se completó exitosamente.", "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo.");
                    MessageBox.Show("No se pudo exportar el archivo.", "Exportación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar roles a Excel.");
                MessageBox.Show("Ocurrió un error inesperado durante la exportación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
