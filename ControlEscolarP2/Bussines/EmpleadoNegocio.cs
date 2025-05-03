using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RecursosHumanos.Data;
using RecursosHumanos.Model;

namespace RecursosHumanos.Bussines
{
    class EmpleadoNegocio
    {
        internal static bool EsNoMatriculaValido(String matricula)
        {
            string patron = @"^(E)-\d{4}-\d{3,5}$";
            return Regex.IsMatch(matricula, patron);
        }

        // Método para validar que la fecha de entrada es menor que la de salida
        internal static bool ValidarFechas(DateTime fechaEntrada, DateTime fechaSalida)
        {
            return fechaEntrada <= fechaSalida;
        }

        //Método para validar horario laboral
        internal static bool ValidarHorario(DateTime horaEntrada, DateTime horaSalida)
        {
            return horaEntrada < horaSalida;
        }

        public static Empleado? ObtenerEmpleadoPorMatricula(string matricula)
        {
            var empleadosData = new EmpleadosDataAccess();
            return empleadosData.ObtenerEmpleadoPorMatricula(matricula);
        }

        // Método para validar si la hora de entrada está dentro del rango de tolerancia (30 minutos)
        public static bool ValidarToleranciaEntrada(DateTime horaEsperadaEntrada, DateTime horaRealEntrada)
        {
            TimeSpan diferencia = horaRealEntrada - horaEsperadaEntrada;
            return diferencia.TotalMinutes <= 30 && diferencia.TotalMinutes >= 0;
        }


    }
}
