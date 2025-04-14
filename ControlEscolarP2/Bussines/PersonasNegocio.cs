using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecursosHumanos.Bussines
{
    class PersonasNegocio
    {
        internal static bool EsNombreValido(String nombre)
        {
           return Validaciones.EsNombreValido(nombre);
        }
        internal static bool EsApellidoValido(String nombre)
        {
            return Validaciones.EsNombreValido(nombre);
        }
        internal static bool EsRFCValido(String rfc)
        {
            return Validaciones.EsRFCValido(rfc);
        }
        internal static bool EsCURPValido(String curp)
        {
            return Validaciones.EsCURPValido(curp);
        }
        internal static bool EsTelefonoValido(String telefono)
        {
            return Validaciones.EsTelefonoValido(telefono);
        }
        internal static bool EsEmailValido(String email)
        {
            return Validaciones.EsCorreoValido(email);
        }
        internal static bool EsFechaNacimientoValido(String fecha)
        {
            return Validaciones.EsFechaValida(fecha);
        }
        internal static bool EsGeneroValido(int genero)
        {
            return genero == 1 | genero == 2;
        }

    }
}
