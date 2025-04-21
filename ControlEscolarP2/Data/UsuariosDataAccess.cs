using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NLog;
using RecursosHumanos.Utilities;
using RecursosHumanos.Model;

namespace RecursosHumanos.Data
{
    public class UsuarioDataAccess
    {
        // Logger para registrar errores y eventos
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.UsuarioDataAccess");

        // Acceso a base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Acceso a datos de personas (para cargar la relación)
        private readonly PersonasDataAccess _personasData;

        // Constructor: inicializa conexiones
        public UsuarioDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
                _personasData = new PersonasDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar UsuarioDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Devuelve la lista de todos los usuarios del sistema
        /// </summary>
        /// <returns>Lista de usuarios con su información personal</returns>
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                string query = @"
                    SELECT u.id_usuario, u.usuario, u.contrasenia, u.id_persona, u.id_rol,
                           u.fecha_creacion, u.fecha_ultimo_acceso, u.estatus,
                           p.nombre, p.ap_paterno, p.ap_materno, p.rfc, p.curp, p.direccion,
                           p.telefono, p.email, p.fecha_nacimiento, p.genero, p.estatus as estatus_persona
                    FROM administration.usuario u
                    INNER JOIN human_resours.persona p ON u.id_persona = p.id_persona
                    ORDER BY u.id_usuario";

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    // Construye el objeto Persona con los datos de la base
                    Persona persona = new Persona
                    {
                        Id_Persona = Convert.ToInt32(row["id_persona"]),
                        Nombre = row["nombre"].ToString() ?? "",
                        Ap_Paterno = row["ap_paterno"].ToString() ?? "",
                        Ap_Materno = row["ap_materno"].ToString() ?? "",
                        RFC = row["rfc"].ToString() ?? "",
                        CURP = row["curp"].ToString() ?? "",
                        Direccion = row["direccion"].ToString() ?? "",
                        Telefono = row["telefono"].ToString() ?? "",
                        Email = row["email"].ToString() ?? "",
                        Fecha_Nacimiento = Convert.ToDateTime(row["fecha_nacimiento"]),
                        Genero = row["genero"].ToString() ?? "",
                        Estatus = Convert.ToInt16(row["estatus_persona"])
                    };

                    // Construye el objeto Usuario con sus datos + la persona asociada
                    Usuario usuario = new Usuario
                    {
                        Id_Usuario = Convert.ToInt32(row["id_usuario"]),
                        UsuarioNombre = row["usuario"].ToString() ?? "",
                        Contrasenia = row["contrasenia"].ToString() ?? "",
                        Id_Persona = Convert.ToInt32(row["id_persona"]),
                        Id_Rol = Convert.ToInt32(row["id_rol"]),
                        Fecha_Creacion = Convert.ToDateTime(row["fecha_creacion"]),
                        Fecha_Ultimo_Acceso = Convert.ToDateTime(row["fecha_ultimo_acceso"]),
                        Estatus = Convert.ToInt16(row["estatus"]),
                        DatosPersonales = persona
                    };

                    usuarios.Add(usuario);
                }

                _logger.Info($"Se obtuvieron {usuarios.Count} usuarios.");
                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de usuarios.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Inserta un nuevo usuario con su persona asociada
        /// </summary>
        /// <param name="usuario">Objeto Usuario con los datos completos</param>
        /// <returns>ID del nuevo usuario o -1 si falla</returns>
        public int InsertarUsuario(Usuario usuario)
        {
            try
            {
                // Primero insertamos la persona asociada
                int idPersona = _personasData.InsertarPersona(usuario.DatosPersonales);

                if (idPersona <= 0)
                {
                    _logger.Error($"No se pudo insertar la persona del usuario: {usuario.UsuarioNombre}");
                    return -1;
                }

                // Asignar el ID de persona generado al objeto usuario
                usuario.Id_Persona = idPersona;

                // Preparar la consulta para insertar el usuario
                string query = @"
                INSERT INTO administration.usuario 
                (id_persona, id_rol, usuario, contrasenia, fecha_creacion, fecha_ultimo_acceso, estatus)
                VALUES 
                (@IdPersona, @IdRol, @Usuario, @Contrasenia, @FechaCreacion, @FechaUltimoAcceso, @Estatus)
                RETURNING id_usuario;";

                // Crear los parámetros
                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@IdPersona", usuario.Id_Persona),
                    _dbAccess.CreateParameter("@IdRol", usuario.Id_Rol),
                    _dbAccess.CreateParameter("@Usuario", usuario.UsuarioNombre),
                    _dbAccess.CreateParameter("@Contrasenia", usuario.Contrasenia),
                    _dbAccess.CreateParameter("@FechaCreacion", usuario.Fecha_Creacion),
                    _dbAccess.CreateParameter("@FechaUltimoAcceso", usuario.Fecha_Ultimo_Acceso),
                    _dbAccess.CreateParameter("@Estatus", usuario.Estatus)
                };

                // Ejecutar la inserción
                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);

                int idGenerado = Convert.ToInt32(resultado);
                _logger.Info($"Usuario insertado correctamente con ID {idGenerado}");

                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar usuario: {usuario.UsuarioNombre}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
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

                // Actualiza primero la persona
                bool actualizacionPersonaExitosa = _personasData.ActualizarPersona(usuario.DatosPersonales);

                if (!actualizacionPersonaExitosa)
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {usuario.Id_Persona}");
                    return false;
                }

                // Actualiza la tabla usuario
                string query = @"
                UPDATE administration.usuario
                SET usuario = @Usuario,
                    contrasenia = @Contrasenia,
                    id_rol = @IdRol,
                    fecha_ultimo_acceso = @FechaUltimoAcceso,
                    estatus = @Estatus
                WHERE id_usuario = @IdUsuario";

                // Parámetros para la consulta
                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@IdUsuario", usuario.Id_Usuario),
                    _dbAccess.CreateParameter("@Usuario", usuario.UsuarioNombre),
                    _dbAccess.CreateParameter("@Contrasenia", usuario.Contrasenia),
                    _dbAccess.CreateParameter("@IdRol", usuario.Id_Rol),
                    _dbAccess.CreateParameter("@FechaUltimoAcceso", usuario.Fecha_Ultimo_Acceso),
                    _dbAccess.CreateParameter("@Estatus", usuario.Estatus)
                };

                _dbAccess.Connect();
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, parametros);

                bool exito = filasAfectadas > 0;

                if (exito)
                {
                    _logger.Info($"Usuario con ID {usuario.Id_Usuario} actualizado correctamente.");
                }
                else
                {
                    _logger.Warn($"No se encontró usuario con ID {usuario.Id_Usuario} para actualizar.");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el usuario con ID {usuario.Id_Usuario}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------Existe()
        
        /// <summary>
        /// Verifica si ya existe un nombre de usuario en la base de datos
        /// </summary>
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM administration.usuario WHERE usuario = @Usuario";
                var parametro = _dbAccess.CreateParameter("@Usuario", nombreUsuario);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametro);
                int count = Convert.ToInt32(resultado);
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar si existe el nombre de usuario: {nombreUsuario}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

    }
}