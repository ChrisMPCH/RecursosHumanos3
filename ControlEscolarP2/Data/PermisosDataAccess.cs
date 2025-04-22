using System;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using NLog;
using Npgsql;

namespace RecursosHumanos.Data
{
    public class PermisosDataAccess
    {
        // Logger para registrar eventos, errores e información de esta clase
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.PermisosDataAccess");

        // Instancia del acceso a la base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Constructor: inicializa la conexión a PostgreSQL
        public PermisosDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Error al inicializar PermisosDataAccess");
                throw;
            }
        }



        //-----------------------------------------------------------------------------------------------------------------Existe()

       
    }
}
