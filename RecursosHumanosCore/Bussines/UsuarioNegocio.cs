using RecursosHumanosCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanosCore.Bussines
{
    //Calculo de nomina, calcular impuestos, faltas, empleado activo, 
    public class UsuarioNegocio
    {
        public static bool EsNombreUsuarioValido(string nombreUsuario)
        {
            return nombreUsuario.Length >= 5;
        }
        public static bool EsContraseñaValido(string contraseña)
        {
            return contraseña.Length >= 8;
        }
        public static bool EsConfirmarContraseñaValido(string contraseña, string confirmarContraseña)
        {
            return EsContraseñaValido(contraseña) && contraseña == confirmarContraseña;
        }
    }
}
