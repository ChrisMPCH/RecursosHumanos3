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

        public bool ExportarPermisosExcel()
        {
            try
            {
                var permisos = ObtenerPermisos();

                if (permisos == null || permisos.Count == 0)
                {
                    _logger.Warn("No se encontraron permisos para exportar.");
                    MessageBox.Show("No se encontraron permisos para exportar.", "Exportación Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Nombre y ruta del archivo
                var nombreArchivo = $"Permisos_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "exportados", nombreArchivo);

                // Crear carpeta si no existe
                var carpetaDestino = Path.GetDirectoryName(rutaArchivo);
                if (!Directory.Exists(carpetaDestino))
                    Directory.CreateDirectory(carpetaDestino);

                // Exportación usando el método genérico
                bool resultado = ExcelExporter.ExportToExcel(permisos, rutaArchivo, "Permisos");

                if (resultado)
                {
                    _logger.Info($"Permisos exportados correctamente a {rutaArchivo}");
                    MessageBox.Show("La exportación de permisos se completó exitosamente.", "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo de permisos.");
                    MessageBox.Show("No se pudo exportar el archivo.", "Exportación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar permisos a Excel.");
                MessageBox.Show("Ocurrió un error inesperado durante la exportación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
