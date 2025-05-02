using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanos.Controller;
using RecursosHumanos.DataAccess;
using RecursosHumanos.Models;

namespace RecursosHumanos.Bussines
{
    public class AsistenciaNegocio
    {
        private static readonly TimeSpan ToleranciaEntrada = TimeSpan.FromMinutes(30);

        public static string RegistrarEntrada(string matricula)
        {
            // Obtener información del empleado
            var empleado = EmpleadoNegocio.ObtenerEmpleadoPorMatricula(matricula);

            // Si el empleado no existe o está dado de baja, devolver mensaje
            if (empleado == null || empleado.Estatus == 0)
                return "Empleado no encontrado o dado de baja.";

            DateTime hoy = DateTime.Today;
            TimeSpan horaActual = DateTime.Now.TimeOfDay;

            // Crear instancia de ContratoController
            ContratoController contratoController = new ContratoController();

            // Llamar al método ObtenerContratoActivoPorMatricula de ContratoController
            var contrato = contratoController.ObtenerContratoActivoPorMatricula(matricula);

            // Verificar si el contrato existe
            if (contrato == null)
                return "El empleado no tiene un contrato activo.";

            // Calcular la hora permitida (hora de entrada más tolerancia)
            TimeSpan horaPermitida = contrato.HoraEntrada.Add(ToleranciaEntrada);

            // Si la hora actual supera la hora permitida, devolver mensaje de tolerancia excedida
            if (horaActual > horaPermitida)
            {
                return "Tolerancia excedida. Día no trabajado.";
            }

            // Crear un objeto de asistencia
            var asistencia = new Asistencia
            {
                FechaAsistencia = hoy,
                HoraEntrada = DateTime.Now.TimeOfDay,
                IdEmpleado = empleado.Id_Empleado,
                Estatus = 1
            };

            // Registrar la asistencia en la base de datos usando AsistenciaDataAccess
            bool exito = AsistenciaDataAccess.InsertarAsistencia(asistencia);

            return exito ? "Entrada registrada exitosamente." : "Error al registrar entrada.";
        }

        public static string RegistrarSalida(string matricula)
        {
            var empleado = EmpleadoNegocio.ObtenerEmpleadoPorMatricula(matricula);

            if (empleado == null || empleado.Estatus == 0)
                return "Empleado no encontrado o dado de baja.";

            DateTime hoy = DateTime.Today;

            return AsistenciaDataAccess.RegistrarSalida(empleado.Id_Empleado, hoy, DateTime.Now.TimeOfDay)
                ? "Salida registrada exitosamente."
                : "No se pudo registrar la salida.";
        }
    }
}