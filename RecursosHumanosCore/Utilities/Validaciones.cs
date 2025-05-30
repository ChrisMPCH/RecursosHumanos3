using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecursosHumanosCore.Utilities
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
        public static bool EsRFCValido(string rfc)
        {
            if (string.IsNullOrWhiteSpace(rfc))
                return false;
            // Elimina los espacios en blanco y convierte a mayúsculas
            rfc = rfc.Trim().ToUpper();
            // Verificación de RFC de Persona Física
            string patronPersonaFisica = @"^[A-ZÑ&]{4}\d{6}[A-Z0-9]{3}$";
            // Verificación de RFC de Persona Moral
            string patronPersonaMoral = @"^[A-ZÑ&]{3}\d{6}[A-Z0-9]{3}$";
            return Regex.IsMatch(rfc, patronPersonaFisica) || Regex.IsMatch(rfc, patronPersonaMoral);
        }
        public static bool EsNombreValido(string nombre)
        {
            string patron = @"^[A-Z][a-z]+(\s[A-Z][a-z]+)*$";
            return Regex.IsMatch(nombre, patron);
        }
    }
}
