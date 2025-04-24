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
    public class UsuariosController
    {
        private readonly PersonasDataAccess _personasAccess;
        private readonly UsuarioDataAccess _usuariosAccess;
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.UsuariosController");

        public UsuariosController()
        {
            _usuariosAccess = new UsuarioDataAccess();
            _personasAccess = new PersonasDataAccess();
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public (bool exito, string mensaje) RegistrarUsuario(Usuario usuario)
        {
            try
            {
                if (_usuariosAccess.ExisteNombreUsuario(usuario.UsuarioNombre))
                {
                    return (false, "El nombre de usuario ya existe.");
                }

                int idGenerado = _usuariosAccess.InsertarUsuario(usuario);
                return idGenerado > 0
                    ? (true, "Usuario registrado correctamente.")
                    : (false, "Error al insertar usuario.");
            }
            catch (Exception ex)
            {
                return (false, "Error inesperado: " + ex.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public List<Usuario> ObtenerUsuarios()
        {
            try
            {
                return _usuariosAccess.ObtenerTodosLosUsuarios(); // Ya lo tienes si usaste el login
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener lista de usuarios");
                return new List<Usuario>();
            }
        }
        /// <summary>
        /// Desactiva un usuario en la base de datos.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public (bool exito, string mensaje) EliminarUsuario(int idUsuario)
        {
            try
            {
                bool eliminado = _usuariosAccess.EliminarUsuario(idUsuario);
                return eliminado
                    ? (true, "Usuario eliminado correctamente.")
                    : (false, "No se pudo eliminar el usuario.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar usuario ID {idUsuario}");
                return (false, "Error inesperado al eliminar usuario.");
            }
        }
        /// <summary>
        /// Actualiza los datos del usuario y su persona asociada.
        /// </summary>
        /// <param name="usuario">Objeto usuario con los datos actualizados</param>
        /// <returns>True si se actualizó correctamente</returns>
        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                _logger.Debug($"Actualizando usuario con ID {usuario.Id_Usuario} y persona ID {usuario.Id_Persona}");

                // Actualiza la persona primero
                bool personaActualizada = _personasAccess.ActualizarPersona(usuario.DatosPersonales);
                if (!personaActualizada)
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {usuario.Id_Persona}");
                    return false;
                }

                // Luego actualiza los datos del usuario
                bool usuarioActualizado = _usuariosAccess.ActualizarUsuario(usuario);
                if (!usuarioActualizado)
                {
                    _logger.Warn($"No se pudo actualizar el usuario con ID {usuario.Id_Usuario}");
                    return false;
                }

                _logger.Info($"Usuario con ID {usuario.Id_Usuario} actualizado correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el usuario con ID {usuario.Id_Usuario}");
                return false;
            }
        }

    }
}