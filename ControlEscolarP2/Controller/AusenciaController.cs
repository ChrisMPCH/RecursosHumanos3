using System;
using RecursosHumanos.Models;
using RecursosHumanos.Data;
using RecursosHumanos.DataAccess;
using NLog;
using RecursosHumanos.Controller;

namespace RecursosHumanos.Controllers
{
    public class AusenciaController
    {
        private readonly AusenciaDataAccess _ausenciaDataAccess;
        private readonly EmpleadosDataAccess _empleadosDataAccess;
        private static readonly AuditoriasController _auditoriasController = new AuditoriasController();

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
    }
}
