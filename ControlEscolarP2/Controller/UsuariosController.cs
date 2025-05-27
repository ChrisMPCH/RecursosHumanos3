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
using RecursosHumanos.View;
using System.IO.Packaging;
using System.IO;

namespace RecursosHumanos.Controller
{
    public class UsuariosController
    {
        private readonly PersonasDataAccess _personasAccess;
        private readonly UsuarioDataAccess _usuariosAccess;
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.UsuariosController");
        private static readonly AuditoriasController _auditoriasController = new AuditoriasController();

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
                if (idGenerado > 0)
                {
                    _auditoriasController.RegistrarAuditoriaGenerica(3,1, (short)idGenerado);
                    return (true, $"Usuario registrado correctamente con ID {idGenerado}.");
                }
                 return (false, "Error al insertar usuario.");
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
                if (eliminado)
                {
                    _auditoriasController.RegistrarAuditoriaGenerica(3, 0, (short)idUsuario);
                }
                else
                {
                    _logger.Warn($"No se pudo eliminar el usuario con ID {idUsuario}");
                }
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
                _auditoriasController.RegistrarAuditoriaGenerica(3, 2, (short)usuario.Id_Usuario);

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el usuario con ID {usuario.Id_Usuario}");
                return false;
            }
        }
        /// <summary>  
        /// Obtiene un usuario por su ID.  
        /// </summary>  
        /// <param name="idUsuario"></param>  
        /// <returns></returns>  
        public Usuario? ObtenerUsuarioPorId(int idUsuario)
        {
            try
            {
                _logger.Debug($"Obteniendo usuario con ID {idUsuario}");
                return _usuariosAccess.ObtenerUsuarioPorId(idUsuario);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener usuario con ID {idUsuario}");
                return null;
            }
        }

        public bool ExportarUsuariosExcel(int rol)
        {
            try
            {
                var usuarios = ObtenerUsuarios();

                // Filtro dinámico en función de los parámetros que llegan de la vista
                Func<dynamic, bool> filtro = e => e.Id_Rol >= rol;

                var nombre = $"Usuarios_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                // Ruta
                string rutaArchivo = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.Desktop), "exportados", nombre);

                if (!Directory.Exists(Path.GetDirectoryName(rutaArchivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));

                // Crear una lista de objetos anónimos con los campos que queremos exportar
                var usuariosParaExportar = usuarios.Select(u => new
                {
                    ID_Usuario = u.Id_Usuario,
                    Nombre_Usuario = u.UsuarioNombre,
                    ID_Persona = u.Id_Persona,
                    Nombre = u.DatosPersonales.Nombre,
                    Apellido_Paterno = u.DatosPersonales.Ap_Paterno,
                    Apellido_Materno = u.DatosPersonales.Ap_Materno,
                    Nombre_Completo = $"{u.DatosPersonales.Nombre} {u.DatosPersonales.Ap_Paterno} {u.DatosPersonales.Ap_Materno}",
                    Email = u.DatosPersonales.Email,
                    Telefono = u.DatosPersonales.Telefono,
                    Direccion = u.DatosPersonales.Direccion,
                    Fecha_Nacimiento = u.DatosPersonales.Fecha_Nacimiento,
                    Genero = u.DatosPersonales.Genero,
                    ID_Rol = u.Id_Rol,
                    Nombre_Rol = u.Rol?.Nombre ?? "Sin rol",
                    Codigo_Rol = u.Rol?.Codigo ?? "Sin código",
                    Fecha_Creacion = u.Fecha_Creacion,
                    Ultimo_Acceso = u.Fecha_Ultimo_Acceso,
                    Estatus = u.Estatus == 1 ? "Activo" : "Inactivo"
                }).ToList();

                // Exportar
                bool resultado = ExcelExporter.ExportToExcel(usuariosParaExportar, rutaArchivo, "Usuarios", filtro);

                if (resultado)
                {
                    _logger.Info($"Archivo exportado correctamente a {rutaArchivo}");
                    MessageBox.Show("La exportación a Excel se completó exitosamente.", "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo.");
                    MessageBox.Show("No se pudo exportar el archivo.", "Exportación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar usuarios a Excel.");
                MessageBox.Show("Ocurrió un error inesperado durante la exportación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


    }
}