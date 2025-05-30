using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanosCore.Utilities;
using NLog;
using Npgsql;

namespace RecursosHumanosCore.Data
{
    public class PostgreSQLDataAccess
    {
        //Logger usando el logginManeger
        public static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.PostgreSQLDataAccess");

        //Cadena de conexión desde App.config
        private static string _connectionString;

        public NpgsqlConnection _connection;
        public static PostgreSQLDataAccess? _instance;


        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        // Intenta obtener desde ConfigurationManager (Windows Forms)
                        _connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"]?.ConnectionString;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn(ex, "No se pudo obtener la cadena de conexión desde ConfigurationManager");
                    }
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"]?.ConnectionString;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn(ex, "No se pudo obtener la cadena de conexión desde ConfigurationManager");
                    }
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        private PostgreSQLDataAccess()
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                {
                    throw new InvalidOperationException("La cadena de conexión no está configurada. Asegúrate de establecer PostgreSQLDataAccess.ConnectionString antes de usar la clase.");
                }

                _connection = new NpgsqlConnection(ConnectionString);
                _logger.Info("Instancia de acceso a datos creada correctamente");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error al inicializar el acceso a la base de datos");
                throw;
            }
        }


        public static PostgreSQLDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PostgreSQLDataAccess();
            }
            return _instance;
        }

        public bool Connect()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                    _logger.Info("Conexión a la base de datos establecida correctamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al conectar a la base de datos");
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                    _logger.Info("Conexión a la base de datos cerrada correctamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al cerrar la conexión a la base de datos");
                throw;
            }
        }

        public DataTable ExecuteQuery_Reader(string query, params NpgsqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                _logger.Debug($"Ejecutando consulta: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        _logger.Debug($"Consulta ejecutada correctamente: {query}");
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar una consulta en la base de datos:{query} ");
                throw;
            }
        }

        //prepra con los parametros
        public NpgsqlCommand CreateCommand(string query, NpgsqlParameter[] parameters)
        {
            NpgsqlCommand command = new NpgsqlCommand(query, _connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
                foreach (var param in parameters)
                {
                    _logger.Trace($"Parámetro: {param.ParameterName} = {param.Value ?? "NULL"}");
                }
            }
            return command;
        }

        public int ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando operación: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {
                    int result = command.ExecuteNonQuery();
                    _logger.Debug($"Operación ejecutada exitosamente. Filas afectadas: {result}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar la operación: {query}");
                throw;
            }
        }

        public object? ExecuteScalar(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                _logger.Debug($"Ejecutando consulta escalar: {query}");
                using (NpgsqlCommand command = CreateCommand(query, parameters))
                {
                    object? result = command.ExecuteScalar();
                    _logger.Debug($"Consulta escalar ejecutada correctamente. ID afectado: {result}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al ejecutar una consulta escalar en la base de datos: {query}");
                throw;
            }
        }

        public NpgsqlParameter CreateParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value ?? DBNull.Value);
        }
    }
}

