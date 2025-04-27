using NLog;
using RecursosHumanos.Model;
using RecursosHumanos.Data;
using System;
using System.Collections.Generic;


namespace RecursosHumanos.Controller
{
    /// <summary>
    /// Controlador encargado de gestionar operaciones relacionadas con los empleados.
    /// </summary>
    public class EmpleadosController
    {
        private static readonly Logger _logger = LogManager.GetLogger("RecursosHumanos.Controller.EmpleadoController");
        private readonly EmpleadosDataAccess _empleadosAccess;

        /// <summary>
        /// Constructor que inicializa la clase de acceso a datos de empleados.
        /// </summary>
        public EmpleadosController()
        {
            _empleadosAccess = new EmpleadosDataAccess();
        }

        /// <summary>
        /// Registra un nuevo empleado en la base de datos.
        /// </summary>
        /// <param name="empleado">El objeto Empleado a registrar.</param>
        /// <returns>Una tupla con el resultado: éxito, idEmpleado y mensaje.</returns>
        public (bool exito, int idEmpleado, string mensaje) RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                if (_empleadosAccess.ExisteMatricula(empleado.Matricula))
                    return (false, 0, "La matrícula ya existe.");

                int idEmpleado = _empleadosAccess.InsertarEmpleado(empleado);

                return idEmpleado > 0
                    ? (true, idEmpleado, "Empleado registrado correctamente.")
                    : (false, 0, "No se pudo registrar el empleado.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al registrar empleado: {empleado.Matricula}");
                return (false, 0, "Error inesperado al registrar el empleado.");
            }
        }

        /// <summary>
        /// Obtiene la lista completa de empleados registrados.
        /// </summary>
        /// <returns>Lista de empleados.</returns>
        public List<Empleado> ObtenerEmpleados()
        {
            try
            {
                return _empleadosAccess.ObtenerTodosLosEmpleados();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de empleados");
                return new List<Empleado>();
            }
        }

        /// <summary>
        /// Da de baja (desactiva) a un empleado según su ID.
        /// </summary>
        /// <param name="idEmpleado">ID del empleado a dar de baja.</param>
        /// <returns>Tupla indicando si fue exitoso y un mensaje.</returns>
        public (bool exito, string mensaje) EliminarEmpleado(int idEmpleado)
        {
            try
            {
                bool eliminado = _empleadosAccess.DarDeBajaEmpleado(idEmpleado);
                return eliminado
                    ? (true, "Empleado dado de baja correctamente.")
                    : (false, "No se pudo dar de baja el empleado.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar empleado ID {idEmpleado}");
                return (false, "Error inesperado al eliminar empleado.");
            }
        }

        /// <summary>
        /// Actualiza la información de un empleado existente.
        /// </summary>
        /// <param name="empleado">Empleado con los datos actualizados.</param>
        /// <returns>True si se actualizó correctamente, false en caso contrario.</returns>
        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                bool actualizado = _empleadosAccess.ActualizarEmpleado(empleado);
                if (!actualizado)
                {
                    _logger.Warn($"No se pudo actualizar el empleado con ID {empleado.Id_Empleado}");
                }

                return actualizado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar empleado con ID {empleado.Id_Empleado}");
                return false;
            }
        }

        /// <summary>
        /// Obtiene un empleado específico por su ID.
        /// </summary>
        /// <param name="idEmpleado">ID del empleado.</param>
        /// <returns>El objeto Empleado si se encuentra, null si no existe o hay error.</returns>
        public Empleado? ObtenerEmpleadoPorId(int idEmpleado)
        {
            try
            {
                return _empleadosAccess.ObtenerEmpleadoPorId(idEmpleado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener empleado con ID {idEmpleado}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene un empleado por su número de matrícula.
        /// </summary>
        /// <param name="matricula">Matrícula del empleado.</param>
        /// <returns>El objeto Empleado si se encuentra, null si no existe o hay error.</returns>
        public Empleado? ObtenerEmpleadoPorMatricula(string matricula)
        {
            try
            {
                return _empleadosAccess.ObtenerEmpleadoPorMatricula(matricula);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener empleado con matrícula {matricula}");
                return null;
            }
        }
    }
}
