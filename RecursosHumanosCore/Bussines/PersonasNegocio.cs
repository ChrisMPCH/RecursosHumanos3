using RecursosHumanosCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecursosHumanosCore.Bussines
{
    public class PersonasNegocio
    {
        public static bool EsRFCValido(String rfc)
        {
            return Validaciones.EsRFCValido(rfc);
        }
        public static bool EsCURPValido(String curp)
        {
            return Validaciones.EsCURPValido(curp);
        }
        public static bool EsTelefonoValido(String telefono)
        {
            return Validaciones.EsTelefonoValido(telefono);
        }
        public static bool EsEmailValido(String email)
        {
            return Validaciones.EsCorreoValido(email);
        }
        public static bool EsGeneroValido(int genero)
        {
            return genero == 1 | genero == 2;
        }
    }
}
