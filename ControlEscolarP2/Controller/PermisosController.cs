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
        /// Devuelve la lista de todos los permisos registrados en formato corto
        /// </summary>
        public List<Permiso> ObtenerPermisosCorto()
        {
            try
            {
                var permisos = _permisosData.ObtenerTodosLosPermisos();
                return permisos.Select(p => new Permiso
                {
                    Id_Permiso = p.Id_Permiso,
                    Codigo = p.Codigo,
                    Descripcion = p.Descripcion
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener permisos (corto)");
                return new List<Permiso>();
            }
        }
    }
}
