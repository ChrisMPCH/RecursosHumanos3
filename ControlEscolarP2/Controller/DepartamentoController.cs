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
    public class DepartamentoController
    {
        private static readonly Logger _logger = LogManager.GetLogger("RecursosHumanos.Controller.DepartamentoController");
        private readonly DepartamentoDataAccess _departamentoDataAccess;
        private static readonly AuditoriasController _auditoriasController= new AuditoriasController();


        public DepartamentoController()
        {
            try
            {
                _departamentoDataAccess = new DepartamentoDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear la conexión a la base de datos");
                throw;
            }
        }

        public List<Departamento> ObtenerTodosLosDepartamentos(bool soloActivos = true)
        {
            try
            {
                var departamentos = _departamentoDataAccess.ObtenerTodosLosDepartamentos(soloActivos);
                _logger.Info($"Se obtuvieron {departamentos.Count} departamentos correctamente");
                return departamentos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de departamentos");
                throw;
            }
        }

        public Departamento? ObtenerDetalleDepartamento(int idDepartamento)
        {
            try
            {
                _logger.Debug($"Solicitando detalle del departamento con ID: {idDepartamento}");
                return _departamentoDataAccess.ObtenerDepartamentoPorId(idDepartamento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener los detalles del departamento con ID: {idDepartamento}");
                throw;
            }
        }

        public (int id, string mensaje) RegistrarDepartamento(Departamento departamento)
        {
            try
            {
                int idDepartamento = _departamentoDataAccess.InsertarDepartamento(departamento);

                if (idDepartamento <= 0)
                {
                    return (-4, "Error al registrar el departamento en la base de datos");
                }
                _auditoriasController.RegistrarAuditoriaGenerica(2, 1, (short)idDepartamento);

                _logger.Info($"Departamento registrado exitosamente con ID: {idDepartamento}");
                return (idDepartamento, "Departamento registrado exitosamente");
            }
            catch (ArgumentException ex)
            {
                _logger.Warn($"Error de validación al registrar el departamento: {ex.Message}");
                return (-1, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Warn($"Error de negocio al registrar el departamento: {ex.Message}");
                return (-2, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar el departamento");
                return (-5, $"Error inesperado: {ex.Message}");
            }
        }

        public (bool exito, string mensaje) ActualizarDepartamento(Departamento departamento)
        {
            try
            {
                if (departamento == null)
                {
                    return (false, "No se proporcionaron datos del departamento");
                }

                Departamento? departamentoExistente = _departamentoDataAccess.ObtenerDepartamentoPorId(departamento.IdDepartamento);
                if (departamentoExistente == null)
                {
                    return (false, $"No se encontró el departamento con ID {departamento.IdDepartamento}");
                }

                _logger.Info($"Actualizando departamento con ID: {departamento.IdDepartamento}");

                bool resultado = _departamentoDataAccess.ActualizarDepartamento(departamento);

                if (!resultado)
                {
                    _logger.Error($"Error al actualizar el departamento con ID {departamento.IdDepartamento}");
                    return (false, "Error al actualizar el departamento en la base de datos");
                }

                _logger.Info($"Departamento con ID {departamento.IdDepartamento} actualizado exitosamente");
                return (true, "Departamento actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error inesperado al actualizar departamento con ID: {departamento.IdDepartamento}");
                return (false, "Error inesperado al actualizar el departamento");
            }
        }
        public (bool exito, string mensaje) EliminarDepartamentoLogico(int idDepartamento)
        {
            try
            {
                if (idDepartamento <= 0)
                {
                    return (false, "El ID del departamento debe ser un número positivo.");
                }

                Departamento? departamentoExistente = _departamentoDataAccess.ObtenerDepartamentoPorId(idDepartamento);
                if (departamentoExistente == null)
                {
                    return (false, $"No se encontró el departamento con ID {idDepartamento}.");
                }

                if (!departamentoExistente.Estatus)
                {
                    return (false, "El departamento ya está inactivo.");
                }

                bool resultado = _departamentoDataAccess.EliminarDepartamentoLogico(idDepartamento);

                if (!resultado)
                {
                    _logger.Error($"Error al eliminar lógicamente el departamento con ID {idDepartamento}");
                    return (false, "Error al eliminar lógicamente el departamento en la base de datos.");
                }

                _logger.Info($"Departamento con ID {idDepartamento} eliminado lógicamente.");
                return (true, "Departamento eliminado lógicamente con éxito.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error inesperado al eliminar lógicamente el departamento con ID: {idDepartamento}");
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public bool ExportarDepartamentosExcel(bool estatus)
        {
            try
            {
                var departamentos = _departamentoDataAccess.ObtenerTodosLosDepartamentos(); // Método para obtener la lista de departamentos

                // Filtro dinámico en función del parámetro estatus
                Func<Departamento, bool> filtro = d => d.Estatus == estatus;

                var nombre = $"Departamentos_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                // Ruta
                string rutaArchivo = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.Desktop), "exportados", nombre);

                if (!Directory.Exists(Path.GetDirectoryName(rutaArchivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));

                // Exportar
                bool resultado = ExcelExporter.ExportToExcel(departamentos, rutaArchivo, "Departamentos", filtro);

                if (resultado)
                {
                    _logger.Info($"Archivo exportado correctamente a {rutaArchivo}");
                    MessageBox.Show("La exportación de departamentos a Excel se completó exitosamente.", "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo.");
                    MessageBox.Show("La exportación de departamentos a Excel ha fallado.", "Exportación incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar departamentos a Excel");
                return false;
            }
        }


    }
}
