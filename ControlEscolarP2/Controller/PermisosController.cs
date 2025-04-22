using System;
using System.Collections.Generic;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using NLog;

namespace RecursosHumanos.Controller
{
    public class PermisosController
    {
        // Logger específico para esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.PermisosController");

        // Acceso a datos de permisos
        private readonly PermisosDataAccess _permisosData;

        /// <summary>
        /// Constructor del controlador de permisos
        /// </summary>
        public PermisosController()
        {
            try
            {
                _permisosData = new PermisosDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar PermisosController");
                throw;
            }
        }

        /// <summary>
        /// Devuelve la lista de todos los permisos registrados
        /// </summary>
        public List<Permiso> ObtenerPermisos()
        {
            try
            {
                var permisos = _permisosData.ObtenerTodosLosPermisos();
                _logger.Info($"Se obtuvieron {permisos.Count} permisos.");
                return permisos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de permisos");
                return new List<Permiso>();
            }
        }

        /// <summary>
        /// Devuelve un permiso específico por su ID
        /// </summary>
        public Permiso? ObtenerPermisoPorId(int id)
        {
            try
            {
                var permisos = _permisosData.ObtenerTodosLosPermisos();
                var permiso = permisos.Find(p => p.Id_Permiso == id);
                if (permiso != null)
                    _logger.Info($"Permiso encontrado: {permiso.Nombre} (ID: {permiso.Id_Permiso})");
                else
                    _logger.Warn($"No se encontró un permiso con ID: {id}");

                return permiso;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener permiso con ID: {id}");
                return null;
            }
        }

        /// <summary>
        /// Busca permisos por nombre o código que contengan el texto
        /// </summary>
        public List<Permiso> BuscarPermisosPorTexto(string texto)
        {
            try
            {
                texto = texto.ToLower();
                var permisos = _permisosData.ObtenerTodosLosPermisos();
                var filtrados = permisos.FindAll(p =>
                    p.Nombre.ToLower().Contains(texto) ||
                    p.Codigo.ToLower().Contains(texto)
                );

                _logger.Info($"Se encontraron {filtrados.Count} permisos que coinciden con: '{texto}'");
                return filtrados;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al buscar permisos por texto: {texto}");
                return new List<Permiso>();
            }
        }
    }
}
