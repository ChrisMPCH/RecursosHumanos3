using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Bussines
{
    //Calculo de nomina, calcular impuestos, faltas, empleado activo, 
    internal class UsuarioNegocio
    {
        internal static bool EsNombreUsuarioValido(string nombreUsuario)
        {
            return nombreUsuario.Length >= 5;
        }

        internal static bool EsContraseñaValido(string contraseña)
        {
            return contraseña.Length >= 8;
        }

        internal static bool EsConfirmarContraseñaValido(string contraseña, string confirmarContraseña)
        {
            return EsContraseñaValido(contraseña) && contraseña == confirmarContraseña;
        }
    }
}
