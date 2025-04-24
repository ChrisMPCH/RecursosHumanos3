using System;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using NLog;
using Npgsql;
using System.Data;

namespace RecursosHumanos.Data
{
    public class PersonasDataAccess
    {
        // Logger para registrar eventos, errores e información de esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.PersonasDataAccess");

        // Instancia del acceso a la base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Constructor: inicializa la conexión a PostgreSQL
        public PersonasDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Error al inicializar PersonasDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Inserta una nueva persona en la base de datos
        /// </summary>
        /// <param name="persona">Objeto Persona a insertar</param>
        /// <returns>ID generado o -1 si falla</returns>
        public int InsertarPersona(Persona persona)
        {
            try
            {
                string query = "INSERT INTO human_resours.persona (nombre, ap_paterno, ap_materno, rfc, curp, direccion, telefono, email, fecha_nacimiento, genero, estatus) " +
                               "VALUES (@Nombre, @Ap_Paterno, @Ap_Materno, @RFC, @CURP, @Direccion, @Telefono, @Email, @FechaNacimiento, @Genero, @Estatus) " +
                               "RETURNING id_persona;";

                // Crear los parámetros para el query
                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@Nombre", persona.Nombre),
                    _dbAccess.CreateParameter("@Ap_Paterno", persona.Ap_Paterno),
                    _dbAccess.CreateParameter("@Ap_Materno", persona.Ap_Materno),
                    _dbAccess.CreateParameter("@RFC", persona.RFC),
                    _dbAccess.CreateParameter("@CURP", persona.CURP),
                    _dbAccess.CreateParameter("@Direccion", persona.Direccion),
                    _dbAccess.CreateParameter("@Telefono", persona.Telefono),
                    _dbAccess.CreateParameter("@Email", persona.Email),
                    _dbAccess.CreateParameter("@FechaNacimiento", persona.Fecha_Nacimiento),
                    _dbAccess.CreateParameter("@Genero", persona.Genero),
                    _dbAccess.CreateParameter("@Estatus", persona.Estatus)
                };

                // Ejecutar consulta
                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);
                int idGenerado = Convert.ToInt32(resultado);

                _logger.Info($"Persona insertada correctamente con ID {idGenerado}");
                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar persona: {persona.Nombre} {persona.Ap_Paterno}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Actualiza los datos de una persona en la base de datos
        /// </summary>
        public bool ActualizarPersona(Persona persona)
        {
            try
            {
                string query = "UPDATE human_resours.persona " +
                               "SET nombre = @Nombre, ap_paterno = @Ap_Paterno, ap_materno = @Ap_Materno, " +
                               "rfc = @RFC, curp = @CURP, direccion = @Direccion, telefono = @Telefono, " +
                               "email = @Email, fecha_nacimiento = @FechaNacimiento, genero = @Genero, estatus = @Estatus " +
                               "WHERE id_persona = @Id";

                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@Id", persona.Id_Persona),
                    _dbAccess.CreateParameter("@Nombre", persona.Nombre),
                    _dbAccess.CreateParameter("@Ap_Paterno", persona.Ap_Paterno),
                    _dbAccess.CreateParameter("@Ap_Materno", persona.Ap_Materno),
                    _dbAccess.CreateParameter("@RFC", persona.RFC),
                    _dbAccess.CreateParameter("@CURP", persona.CURP),
                    _dbAccess.CreateParameter("@Direccion", persona.Direccion),
                    _dbAccess.CreateParameter("@Telefono", persona.Telefono),
                    _dbAccess.CreateParameter("@Email", persona.Email),
                    _dbAccess.CreateParameter("@FechaNacimiento", persona.Fecha_Nacimiento),
                    _dbAccess.CreateParameter("@Genero", persona.Genero),
                    _dbAccess.CreateParameter("@Estatus", persona.Estatus)
                };

                _dbAccess.Connect();
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, parametros);

                if (filasAfectadas > 0)
                {
                    _logger.Info($"Persona con ID {persona.Id_Persona} actualizada correctamente");
                    return true;
                }

                _logger.Warn($"No se encontró persona con ID {persona.Id_Persona}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar la persona con ID {persona.Id_Persona}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Elimina una persona por su ID
        /// </summary>
        public bool EliminarPersona(int id)
        {
            try
            {
                string query = "UPDATE human_resours.persona SET estatus = 0 WHERE id_persona = @Id";
                var param = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();
                int filas = _dbAccess.ExecuteNonQuery(query, param);
                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar la persona con ID {id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Devuelve una persona por su ID
        /// </summary>
        public Persona? ObtenerPersonaPorId(int id)
        {
            try
            {
                string query = "SELECT * FROM human_resours.persona WHERE id_persona = @Id";
                var parametro = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, parametro);

                if (resultado.Rows.Count == 0) return null;

                DataRow row = resultado.Rows[0];
                return new Persona
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
                    Estatus = Convert.ToInt16(row["estatus"])
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener persona con ID {id}");
                return null;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Elimina una persona si no tiene relaciones en otras tablas
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns></returns>
        public bool EliminarPersonaSiNoTieneRelaciones(int idPersona)
        {
            try
            {
                // Verificar si la persona está relacionada a usuarios o empleados
                string verificarQuery = @"
            SELECT 
                (SELECT COUNT(*) FROM administration.usuario WHERE id_persona = @IdPersona) +
                (SELECT COUNT(*) FROM human_resours.empleado WHERE id_persona = @IdPersona) AS TotalRelaciones;";

                var param = _dbAccess.CreateParameter("@IdPersona", idPersona);
                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(verificarQuery, param);
                int relaciones = Convert.ToInt32(resultado);

                if (relaciones > 0)
                {
                    _logger.Warn($"No se puede eliminar la persona ID {idPersona} porque tiene {relaciones} relaciones.");
                    return false;
                }

                // Si no tiene relaciones, eliminarla
                string eliminarQuery = "DELETE FROM human_resours.persona WHERE id_persona = @IdPersona";
                int filas = _dbAccess.ExecuteNonQuery(eliminarQuery, param);

                _logger.Info($"Persona con ID {idPersona} eliminada correctamente.");
                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar persona ID {idPersona}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


        //-----------------------------------------------------------------------------------------------------------------Existe()

        /// <summary>
        /// Verifica si una CURP ya está registrada
        /// </summary>
        public bool ExisteCURP(string curp)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM human_resours.persona WHERE curp = @CURP";
                var parametro = _dbAccess.CreateParameter("@CURP", curp);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametro);
                int count = Convert.ToInt32(resultado);

                return count > 0;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error al verificar si existe CURP: {curp}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si un RFC ya está registrado
        /// </summary>
        public bool ExisteRFC(string rfc)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM human_resours.persona WHERE rfc = @RFC";
                var parametro = _dbAccess.CreateParameter("@RFC", rfc);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametro);
                int count = Convert.ToInt32(resultado);

                return count > 0;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error al verificar si existe RFC: {rfc}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si un número de teléfono ya está registrado
        /// </summary>
        public bool ExisteTelefono(string telefono)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM human_resours.persona WHERE telefono = @Telefono";
                var parametro = _dbAccess.CreateParameter("@Telefono", telefono);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametro);
                int count = Convert.ToInt32(resultado);

                return count > 0;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error al verificar si existe Teléfono: {telefono}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Verifica si un correo ya existe en la base de datos
        /// </summary>
        public bool ExisteEmail(string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM human_resours.persona WHERE email = @Email";
                var parametro = _dbAccess.CreateParameter("@Email", email);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametro);
                int count = Convert.ToInt32(resultado);
                return count > 0;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error al verificar si existe Email: {email}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}
