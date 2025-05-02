using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using NLog;
using System;
using System.Collections.Generic;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.Controller
{
    public class LoginController
    {
        private readonly UsuarioDataAccess _usuariosAccess;

        public LoginController()
        {
            _usuariosAccess = new UsuarioDataAccess();
        }

        /// <summary>
        /// Método para manejar el proceso de login.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <param name="contrasenia">Contraseña del usuario</param>
        /// <returns>Usuario logueado o null si las credenciales son incorrectas</returns>
        public Usuario? Login(string nombreUsuario, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasenia))
            {
                throw new ArgumentException("El nombre de usuario y la contraseña no pueden estar vacíos.");
            }

            return _usuariosAccess.Login(nombreUsuario, contrasenia);
        }

        /// <summary>
        /// Actualiza la fecha de último acceso del usuario.
        /// </summary>
        /// <param name="usuario">Usuario a actualizar</param>
        public void ActualizarFechaUltimoAcceso(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
            }

            usuario.Fecha_Ultimo_Acceso = DateTime.Now;
            _usuariosAccess.ActualizarUsuario(usuario);
        }

        /// <summary>
        /// Obtiene los permisos del usuario según su ID.
        /// </summary>
        /// <param name="idUsuario">ID del usuario</param>
        /// <returns>Lista de IDs de permisos</returns>
        public List<int> ObtenerPermisosUsuario(int idUsuario)
        {
            return _usuariosAccess.ObtenerPermisosUsuario(idUsuario);
        }
    }
}
