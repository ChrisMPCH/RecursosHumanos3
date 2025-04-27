using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecursosHumanos.Utilities
{
    internal class Validaciones
    {
        public static bool EsCorreoValido(string correo)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patron);
        }
        public static bool EsCURPValido(string curp)
        {
            string patron = @"^[A-Z]{4}\d{6}[HM][A-Z]{5}[A-Z0-9]{2}$";
            return Regex.IsMatch(curp, patron);
        }
        public static bool EsTelefonoValido(string telefono)
        {
            string patron = @"^\d{10}$";
            return Regex.IsMatch(telefono, patron);
        }
        public static bool EsCodigoPostalValido(string cp)
        {
            string patron = @"^\d{5}$";
            return Regex.IsMatch(cp, patron);
        }
        public static bool EsRFCValido(string rfc)
        {
            string patron = @"^[A-Z]{4}\d{6}[A-Z0-9]{3}$";
            return Regex.IsMatch(rfc, patron);
        }
        public static bool EsNombreValido(string nombre)
        {
            string patron = @"^[A-Z][a-z]+(\s[A-Z][a-z]+)*$";
            return Regex.IsMatch(nombre, patron);
        }
        public static bool EsApellidoValido(string apellido)
        {
            string patron = @"^[A-Z][a-z]+(\s[A-Z][a-z]+)*$";
            return Regex.IsMatch(apellido, patron);
        }
        public static bool EsNumeroValido(string numero)
        {
            string patron = @"^\d+$";
            return Regex.IsMatch(numero, patron);
        }
        public static bool EsSalarioValido(string salario)
        {
            string patron = @"^\d+(\.\d{1,2})?$";
            return Regex.IsMatch(salario, patron);
        }
        public static bool EsFechaValida(string fecha)
        {
            DateTime fechaConvertida;
            return DateTime.TryParse(fecha, out fechaConvertida);
        }

    }
}
