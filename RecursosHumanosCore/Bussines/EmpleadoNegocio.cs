using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.Model;

namespace RecursosHumanosCore.Bussines
{
    public class EmpleadoNegocio
    {
        public static bool EsNoMatriculaValido(String matricula)
        {
            string patron = @"^(E)-\d{4}-\d{3,5}$";
            return Regex.IsMatch(matricula, patron);
        }

        // Método para validar que la fecha de entrada es menor que la de salida
        public static bool ValidarFechas(DateTime fechaEntrada, DateTime fechaSalida)
        {
            return fechaEntrada <= fechaSalida;
        }

        //Método para validar horario laboral
        public static bool ValidarHorario(DateTime horaEntrada, DateTime horaSalida)
        {
            return horaEntrada < horaSalida;
        }

    }
}
