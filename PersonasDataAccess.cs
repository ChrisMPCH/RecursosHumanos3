using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanos.Utilities;
using alumnos_tem2.Model;
using NLog;
using Npgsql;
using RecursosHumanos.Data;

namespace alumnos_tem2.Data
{
    public class PersonasDataAccess
    {
        //logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data*.PersonasDataAccess");
        //instancia del acceso a datos postgree
        private readonly PostgreSQLDataAccess  _dbAccess;
        //contructor
        public PersonasDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Error al instializar PersonasDataAccess");
                throw;
            }
        }
        public int InsertarPersona(Persona persona)
        {
            try
            {
                string query = "INSERT INTO seguricdad.personas (nombre_completo,correo,telefono,fecha_nacimiento,curp,estatus)" +
                                "VALUES /@NombreCompleto,@Correo,@Telefono,@FechaNacimiento,@Curp,@Estatus)" +
                                "RETURNING id";
                //crear parametros
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@NombreCompleto", persona.Nombre);
                NpgsqlParameter paramApPaterno = _dbAccess.CreateParameter("@ApellidoPaterno", persona.ApellidoPaterno);
                NpgsqlParameter paramApMaterno= _dbAccess.CreateParameter("@ApellidoMaterno", persona.ApellidoMaterno);
                NpgsqlParameter paramCorreo = _dbAccess.CreateParameter("@Corre", persona.Correo);
                NpgsqlParameter paramTelefono = _dbAccess.CreateParameter("@Telefono", persona.Telefono);
                NpgsqlParameter paramFechaNac = _dbAccess.CreateParameter("@FechaNacimiento", persona.FechaNacimiento);
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter("@Curp", persona.Curp);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", persona.Estatus);
                NpgsqlParameter paramRFC = _dbAccess.CreateParameter("@Estatus", persona.RFC);
                NpgsqlParameter paramDireccion = _dbAccess.CreateParameter("@Estatus", persona.Direccion);
                NpgsqlParameter paramGenero = _dbAccess.CreateParameter("@Estatus", persona.Genero);
                //establecer conexion
                _dbAccess.Connect();
                //ejecutar insercion y obtiene id generado(scalar sdolo se afceta uno)
                object? resultado = _dbAccess.ExecuteScalar(query, paramNombre,paramApPaterno,paramApMaterno, paramCorreo, paramTelefono, paramFechaNac, paramCurp, paramEstatus,paramRFC,paramDireccion,paramGenero);
                //resultado a entero
                int idGenerado = Convert.ToInt32(resultado);
                _logger.Info($"persona insertada correctamente con ID{idGenerado}");
                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"error al insertar persona{persona.Nombre}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public bool ExisteCURP(string curp)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM seguridad.personas WHERE curp =@Curp";
                NpgsqlParameter paramCurp = _dbAccess.CreateParameter(@"Curp", curp);
                _dbAccess.Connect();
                Object? resultado = _dbAccess.ExecuteScalar(query, paramCurp);
                int count=Convert.ToInt32(resultado);
                return count > 0;
            }
            catch(Exception e)
            {
                _logger.Error(e, $"error al verificar si existe CRUP: {curp}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public bool ActualizarPersona(Persona persona)
        {
            try
            {
                string query = @"
            UPDATE seguridad.personas
            SET nombre_completo = @NombreCompleto, " +
                        "correo = @Correo, " +
                        "telefono = @Telefono, " +
                        "fecha_nacimiento = @FechaNacimiento, " +
                        "curp = @CURP, " +
                        "estatus = @Estatus " +
                    "WHERE id = @Id";

                // Crea los parámetros
                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", persona.IdPersona);
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@NombreCompleto", persona.Nombre);
                NpgsqlParameter paramApPaterno = _dbAccess.CreateParameter("@ApellidoPaterno", persona.ApellidoPaterno);
                NpgsqlParameter paramApMaterno = _dbAccess.CreateParameter("@ApellidoMaterno", persona.ApellidoMaterno);
                NpgsqlParameter paramCorreo = _dbAccess.CreateParameter("@Correo", persona.Correo);
                NpgsqlParameter paramTelefono = _dbAccess.CreateParameter("@Telefono", persona.Telefono);
                NpgsqlParameter paramFechaNac = _dbAccess.CreateParameter("@FechaNacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                NpgsqlParameter paramCURP = _dbAccess.CreateParameter("@CURP", persona.Curp);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", persona.Estatus);
                NpgsqlParameter paramRFC = _dbAccess.CreateParameter("@Estatus", persona.RFC);
                NpgsqlParameter paramDireccion = _dbAccess.CreateParameter("@Estatus", persona.Direccion);
                NpgsqlParameter paramGenero = _dbAccess.CreateParameter("@Estatus", persona.Genero);
                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la actualización
                int filasAfectadas = _dbAccess.ExecuteNonQuery(query, paramId, paramNombre, paramCorreo, paramTelefono, paramFechaNac, paramCURP, paramEstatus);

                bool exito = filasAfectadas > 0;

                if (exito)
                {
                    _logger.Info($"Persona con ID {persona.IdPersona} actualizada correctamente");
                }
                else
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {persona.IdPersona}. No se encontró el registro");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar la persona con ID {persona.IdPersona}");
                return false;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }
    }
}
