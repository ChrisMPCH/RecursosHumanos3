using NLog;
using RecursosHumanos.Model;
using RecursosHumanos.Data;

namespace RecursosHumanos.Controller
{
    public class EmpleadoController
    {
        private static readonly Logger _logger = LogManager.GetLogger("RecursosHumanos.Controller.EmpleadoController");
        private readonly EmpleadosDataAccess _empleadosDataAccess;

        public EmpleadoController()
        {
            try
            {
                _empleadosDataAccess = new EmpleadosDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar EmpleadoController");
                throw;
            }
        }

        public (int idEmpleado, string mensaje) RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                int idEmpleado = _empleadosDataAccess.InsertarEmpleado(empleado); 

                if (idEmpleado <= 0)
                {
                    _logger.Error($"Error al registrar el empleado con matrícula: {empleado.Matricula}");
                    return (-1, "No se pudo registrar el empleado.");
                }

                _logger.Info($"Empleado registrado exitosamente con ID: {idEmpleado}");
                return (idEmpleado, "Empleado registrado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error inesperado al registrar empleado con matrícula: {empleado.Matricula}");
                return (-2, $"Error inesperado: {ex.Message}");
            }
        }
    }
}
