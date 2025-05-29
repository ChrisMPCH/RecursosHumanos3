using System;
using RecursosHumanosCore.Models;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.DataAccess;
using NLog;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Utilities;

namespace RecursosHumanosCore.Controllers
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

        public bool RegistrarAusenciaPorRetardo(int idEmpleado, int idUsuario, DateTime fecha, out string mensaje)
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
                _auditoriasController.RegistrarAuditoriaGenerica(6, 1, (short)idEmpleado, idUsuario);

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
    }
}
