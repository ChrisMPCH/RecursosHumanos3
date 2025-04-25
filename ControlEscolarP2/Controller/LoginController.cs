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
        private readonly PersonasDataAccess _personasAccess;
        private readonly UsuarioDataAccess _usuariosAccess;

        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.LoginController");



        public LoginController(PersonasDataAccess personasDataAccess, UsuarioDataAccess usuarioDataAccess)
        {
            _personasAccess = personasDataAccess;
            _usuariosAccess = usuarioDataAccess;
        }

        public LoginController()
        {

        }

        /// <summary>
        /// Inicia sesión con un usuario existente.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasenia"></param>
        /// <returns></returns>
        public Usuario? Login(string nombreUsuario, string contrasenia)
        {
            try
            {
                return _usuariosAccess.Login(nombreUsuario, contrasenia);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al intentar iniciar sesión");
                return null;
            }
        }

        /// <summary>
        /// Obtiene los permisos del usuario según su ID
        /// </summary>
        /// <param name="idUsuario">ID del usuario que inició sesión</param>
        /// <returns>Lista de IDs de permisos que tiene el usuario</returns>
        public List<int> ObtenerPermisosUsuario(int idUsuario)
        {
            // Obtén los permisos del usuario desde el DataAccess
            return _usuariosAccess.ObtenerPermisosUsuario(idUsuario);
        }


    }
}
