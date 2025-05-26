using System;
using RecursosHumanos.Models;
using RecursosHumanos.Data;
using RecursosHumanos.DataAccess;
using NLog;
using RecursosHumanos.Controller;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.Controllers
{
    public class AusenciaController
    {
        private readonly AusenciaDataAccess _ausenciaDataAccess;
        private readonly EmpleadosDataAccess _empleadosDataAccess;
        private static readonly AuditoriasController _auditoriasController = new AuditoriasController();
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.AusenciaController");


        public AusenciaController()
        {
            _ausenciaDataAccess = new AusenciaDataAccess();
            _empleadosDataAccess = new EmpleadosDataAccess(); 
        }

        public bool RegistrarAusenciaPorRetardo(int idEmpleado, DateTime fecha, out string mensaje)
        {
            mensaje = "";

            // Validar si ya existe ausencia registrada hoy
            bool yaExiste = _ausenciaDataAccess.ExisteAusenciaHoy(idEmpleado, fecha);
            if (yaExiste)
            {
                mensaje = "Ya existe una ausencia registrada para hoy.";
                return false;
            }

            // Insertar ausencia
            Ausencia nuevaAusencia = new Ausencia
            {
                FechaAusencias = fecha,
                MotivoAusencia = "Retardo mayor al permitido",
                IdEmpleado = idEmpleado,
                Estatus = 1
            };

            bool resultado = _ausenciaDataAccess.InsertarAusencia(nuevaAusencia);

            if (resultado)
            {
                _auditoriasController.RegistrarAuditoriaGenerica(6, 1, (short)idEmpleado);
                mensaje = "Ausencia registrada correctamente.";
                return true;
            }
            else
            {
                mensaje = "No se pudo registrar la ausencia.";
                return false;
            }
        }
        public bool TieneAusenciaRegistradaHoy(string matricula)
        {
            // Buscar empleado por matrícula usando su controller
            Empleado empleado = _empleadosDataAccess.ObtenerEmpleadoPorMatricula(matricula);

            if (empleado == null)
                return false;

            // Validar si tiene ausencia hoy usando capa de DataAccess
            return _ausenciaDataAccess.ExisteAusenciaHoy(empleado.Id_Empleado, DateTime.Now);
        }

        public List<Ausencia> ObtenerAusencias()
        {
            try
            {
                return _ausenciaDataAccess.ObtenerAusencias();
            }
            catch (Exception ex)
            {
                // Aquí puedes usar NLog o el logger que tengas configurado
                _logger.Error(ex, "Error al obtener la lista de ausencias.");
                return new List<Ausencia>();
            }
        }

        public bool ExportarAusenciasExcel()
        {
            try
            {

                var ausencias = ObtenerAusencias();

                var nombre = $"Ausencias_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                // Ruta del archivo
                string rutaArchivo = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "exportados",
                    nombre
                );

                // Crear directorio si no existe
                if (!Directory.Exists(Path.GetDirectoryName(rutaArchivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));

                // Exportar sin filtro
                bool resultado = ExcelExporter.ExportToExcel(ausencias, rutaArchivo, "Ausencias");

                if (resultado)
                {
                    _logger.Info($"Archivo exportado correctamente a {rutaArchivo}");
                    MessageBox.Show("La exportación a Excel se completó exitosamente.", "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo.");
                    MessageBox.Show("La exportación a Excel ha fallado.", "Exportación incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar empleados a Excel");
                return false;
            }
        }
    }
}
