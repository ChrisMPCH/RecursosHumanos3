using System;
using RecursosHumanos.Models;
using RecursosHumanos.Data;
using RecursosHumanos.Bussines; 
using NLog;
using RecursosHumanos.DataAccess;
using RecursosHumanos.Controller;
using System.Configuration;

namespace RecursosHumanos.Controllers
{
    public class AsistenciaController
    {
        private readonly EmpleadosDataAccess _empleadosDataAccess = new EmpleadosDataAccess();
        private readonly ContratoController _contratosController = new ContratoController();
        private readonly AsistenciaDataAccess _asistenciaDataAccess = new AsistenciaDataAccess();
        private readonly AusenciaController _ausenciasController = new AusenciaController();
        private static readonly AuditoriasController _auditoriasController = new AuditoriasController();

        private static readonly Logger _logger = LogManager.GetLogger("RecursosHumanos.Controller.AsistenciaController");
        public bool ValidarToleranciaEntrada(DateTime horaEsperadaEntrada, DateTime horaRealEntrada)
        {
            // Leer el valor de la tolerancia en minutos desde el app.config
            int toleranciaMinutos = Convert.ToInt32(ConfigurationManager.AppSettings["ToleranciaMinutos"]);

            // Calcular la diferencia de tiempo
            TimeSpan diferencia = horaRealEntrada - horaEsperadaEntrada;

            // Validar si la diferencia está dentro del rango de tolerancia
            return diferencia.TotalMinutes <= toleranciaMinutos && diferencia.TotalMinutes >= 0;
        }

        public bool RegistrarEntrada(string matricula, out string mensaje)
        {
            mensaje = "";

            // 1. Buscar empleado
            Empleado empleado = _empleadosDataAccess.ObtenerEmpleadoPorMatricula(matricula);
            if (empleado == null)
            {
                mensaje = "Empleado no encontrado.";
                return false;
            }

            // 2. Validar contrato activo
            var contrato = _contratosController.ObtenerContratoActivoPorMatricula(matricula);
            if (contrato == null)
            {
                mensaje = "El empleado no tiene un contrato activo.";
                return false;
            }

            // 3. Validar que aún no sea antes de la hora de entrada (nuevo)
            TimeSpan horaActual = DateTime.Now.TimeOfDay;
            TimeSpan horaEntradaPermitida = contrato.HoraEntrada;

            if (horaActual < horaEntradaPermitida)
            {
                mensaje = $"Aún no es hora de registrar la entrada. La hora permitida es a las {horaEntradaPermitida:hh\\:mm}.";
                return false;
            }

            // 4. Validar horario y tolerancia (aquí sí aplica retardo)
            DateTime horaEsperada = DateTime.Today.Add(contrato.HoraEntrada);
            DateTime horaReal = DateTime.Now;

            if (!ValidarToleranciaEntrada(horaEsperada, horaReal))
            {
                // Registrar ausencia solo si no existe ya
                bool ausenciaRegistrada = _ausenciasController.RegistrarAusenciaPorRetardo(empleado.Id_Empleado, DateTime.Today, out string mensajeAusencia);

                if (!ausenciaRegistrada)
                {
                    mensaje = "La hora de entrada excede los minutos de tolerancia permitidos. " + mensajeAusencia;
                }
                else
                {
                    mensaje = "La hora de entrada excede los minutos de tolerancia permitidos. Ausencia registrada correctamente.";
                }
                return false;
            }

            // 5. Registrar asistencia
            bool resultado = _asistenciaDataAccess.InsertarAsistenciaPorMatricula(matricula, DateTime.Today, horaReal.TimeOfDay);

            if (resultado)
            {
                _auditoriasController.RegistrarAuditoriaGenerica(6, 1, (short)empleado.Id_Empleado);
                mensaje = "Entrada registrada correctamente.";
                return true;
            }
            else
            {
                mensaje = "No se pudo registrar la entrada. Es posible que ya exista un registro hoy.";
                return false;
            }
        }

        /// <summary>
        /// Registra la salida de un empleado.
        /// </summary>
        /// <param name="matricula">Matrícula del empleado</param>
        /// <param name="mensaje">Mensaje de salida</param>
        /// <param name="observaciones">Observaciones adicionales</param>
        /// <returns>True si la salida fue registrada correctamente, false si hubo algún error</returns>
        public bool RegistrarSalida(string matricula, out string mensaje, string observaciones = "")
        {
            mensaje = "";

            // 1. Buscar empleado
            Empleado empleado = _empleadosDataAccess.ObtenerEmpleadoPorMatricula(matricula);
            if (empleado == null)
            {
                mensaje = "Empleado no encontrado.";
                return false;
            }

            // 2. Validar contrato activo
            var contrato = _contratosController.ObtenerContratoActivoPorMatricula(matricula);
            if (contrato == null)
            {
                mensaje = "El empleado no tiene un contrato activo.";
                return false;
            }

            // 3. Validar si ya existe un registro de entrada para el día actual
            bool registroEntradaExistente = _asistenciaDataAccess.ExisteRegistroDelDia(matricula, DateTime.Today);
            if (!registroEntradaExistente)
            {
                mensaje = "No se ha registrado una entrada para el empleado hoy.";
                return false;
            }

            // 4. Validar que el empleado haya cumplido con las horas de contrato
            TimeSpan horasTrabajadas = _asistenciaDataAccess.CalcularHorasTrabajadas(matricula, DateTime.Today);

            if (horasTrabajadas.TotalHours < contrato.NumeroHoras)
            {
                mensaje = "El empleado no ha cumplido con las horas de trabajo establecidas en el contrato.";
                return false;
            }

            // 5. Registrar la salida del empleado
            bool resultado = _asistenciaDataAccess.RegistrarSalidaPorMatricula(matricula, DateTime.Today, DateTime.Now.TimeOfDay, observaciones);

            if (resultado)
            {
                _auditoriasController.RegistrarAuditoriaGenerica(6, 2, (short)empleado.Id_Empleado);
                mensaje = "Salida registrada correctamente.";
                return true;
            }
            else
            {
                mensaje = "No se pudo registrar la salida. Verifique si ya existe un registro de entrada.";
                return false;
            }
        }
        /// <summary>
        /// Obtiene la cantidad de días con asistencias completas (entrada y salida) para un empleado en un rango de fechas.
        /// </summary>
        /// <param name="idEmpleado">ID del empleado</param>
        /// <param name="fechaInicio">Fecha de inicio del rango</param>
        /// <param name="fechaFin">Fecha de fin del rango</param>
        /// <returns>Número de asistencias completas</returns>
        public int ObtenerAsistenciasCompletas(int idEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Validar ID de empleado
                if (idEmpleado <= 0)
                    throw new ArgumentException("El ID del empleado no es válido.");

                // Validar rango de fechas
                if (fechaInicio > fechaFin)
                    throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");

                // Llamar al DataAccess
                int asistenciasCompletas = _asistenciaDataAccess.ContarDiasTrabajados(idEmpleado, fechaInicio, fechaFin);

                return asistenciasCompletas;
            }
            catch (ArgumentException ex)
            {
                _logger.Warn(ex, "Validación fallida al obtener asistencias completas");
                throw; // Relanzamos porque es un error esperado por el usuario.
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado al obtener las asistencias completas");
                throw new ApplicationException("Hubo un error al obtener las asistencias completas. Por favor intente nuevamente más tarde.", ex);
            }
        }

        public List<Asistencia> ObtenerAsistenciasConEmpleado()
        {
            try
            {
                return _asistenciaDataAccess.ObtenerAsistencias();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener asistencias con datos del empleado");
                throw;
            }
        }

    }
}
