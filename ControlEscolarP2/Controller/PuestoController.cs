using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using RecursosHumanos.Models;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.Controller
{
    public class PuestoController
    {
        private static readonly Logger _logger = LogManager.GetLogger("RecursosHumanos.Controller.PuestoController");
        private readonly PuestoDataAccess _puestoDataAccess;

        public PuestoController()
        {
            try
            {
                _puestoDataAccess = new PuestoDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear la conexión a la base de datos");
                throw;
            }
        }

        public (int id, string mensaje) RegistrarPuesto(Puesto puesto)
        {
            try
            {
                int idPuesto = _puestoDataAccess.InsertarPuesto(puesto);
                if (idPuesto <= 0)
                    return (-4, "No se pudo registrar el puesto en la base de datos.");
                AuditoriasController auditoriasController = new AuditoriasController();
                auditoriasController.RegistrarAuditoriaGenerica(2, 1, (short)idPuesto);
                _logger.Info($"Puesto registrado exitosamente con ID: {idPuesto}");
                return (idPuesto, "Puesto registrado exitosamente");
            }
            catch (ArgumentException ex)
            {
                _logger.Warn($"Error de validación al registrar el puesto: {ex.Message}");
                return (-1, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Warn($"Error de negocio al registrar el puesto: {ex.Message}");
                return (-2, ex.Message);
            }
            catch (Exception ex)
            {
                return (-5, $"Error inesperado: {ex.Message}");
            }
        }

        public Puesto? ObtenerDetallePuesto(int idPuesto)
        {
            try
            {
                _logger.Debug($"Solicitando detalle del puesto con ID: {idPuesto}");
                return _puestoDataAccess.ObtenerPuestoPorId(idPuesto);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener los detalles del puesto con ID: {idPuesto}");
                throw;
            }
        }


        public (bool exito, string mensaje) ActualizarPuesto(Puesto puesto)
        {
            try
            {
                if (puesto == null)
                {
                    return (false, "No se proporcionaron datos del puesto");
                }

                Puesto? puestoExistente = _puestoDataAccess.ObtenerPuestoPorId(puesto.IdPuesto);
                if (puestoExistente == null)
                {
                    return (false, $"No se encontró el puesto con ID {puesto.IdPuesto}");
                }

                _logger.Info($"Actualizando puesto con ID: {puesto.IdPuesto}");

                bool resultado = _puestoDataAccess.ActualizarPuesto(puesto);

                if (!resultado)
                {
                    _logger.Error($"Error al actualizar el puesto con ID {puesto.IdPuesto}");
                    return (false, "Error al actualizar el puesto en la base de datos");
                }

                _logger.Info($"Puesto con ID {puesto.IdPuesto} actualizado exitosamente");
                return (true, "Puesto actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error inesperado al actualizar puesto con ID: {puesto.IdPuesto}");
                return (false, "Error inesperado al actualizar el puesto");
            }
        }
        public List<Puesto> ObtenerTodosLosPuestos(bool soloActivos = true)
        {
            try
            {
                var puestos = _puestoDataAccess.ObtenerTodosLosPuestos(soloActivos);
                if (puestos == null)
                {
                    _logger.Error("ObtenerTodosLosPuestos retornó null");
                    return new List<Puesto>(); // Retornar una lista vacía en lugar de null
                }
                _logger.Info($"Se obtuvieron {puestos.Count} puestos correctamente");
                return puestos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de puestos");
                throw;
            }
        }
        public (bool exito, string mensaje) EliminarPuestoLogico(int idPuesto)
        {
            try
            {
                if (idPuesto <= 0)
                {
                    return (false, "El ID del departamento debe ser un número positivo.");
                }

                Puesto? PuestoExistente = _puestoDataAccess.ObtenerPuestoPorId(idPuesto);
                if (PuestoExistente == null)
                {
                    return (false, $"No se encontró el Puesto con ID {idPuesto}.");
                }

                if (!PuestoExistente.Estatus)
                {
                    return (false, "El departamento ya está inactivo.");
                }

                bool resultado = _puestoDataAccess.EliminarPuestoLogico(idPuesto);

                if (!resultado)
                {
                    _logger.Error($"Error al eliminar lógicamente el Puesto con ID {idPuesto}");
                    return (false, "Error al eliminar lógicamente el Puesto en la base de datos.");
                }

                _logger.Info($"Departamento con ID {idPuesto} eliminado lógicamente.");
                return (true, "Departamento eliminado lógicamente con éxito.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error inesperado al eliminar lógicamente el departamento con ID: {idPuesto}");
                return (false, $"Error inesperado: {ex.Message}");
            }
        }


        public bool ExportarPuestosExcel(bool estatus)
        {
            try
            {
                var puestos = _puestoDataAccess.ObtenerTodosLosPuestos(); // Método para obtener la lista de puestos

                // Filtro dinámico en función del parámetro estatus
                Func<Puesto, bool> filtro = p => p.Estatus == estatus;

                var nombre = $"Puestos_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                // Ruta
                string rutaArchivo = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.Desktop), "exportados", nombre);

                if (!Directory.Exists(Path.GetDirectoryName(rutaArchivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));

                // Exportar
                bool resultado = ExcelExporter.ExportToExcel(puestos, rutaArchivo, "Puestos", filtro);

                if (resultado)
                {
                    _logger.Info($"Archivo exportado correctamente a {rutaArchivo}");
                    MessageBox.Show("La exportación de puestos a Excel se completó exitosamente.", "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo.");
                    MessageBox.Show("La exportación de puestos a Excel ha fallado.", "Exportación incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar puestos a Excel");
                return false;
            }
        }

    }
}
