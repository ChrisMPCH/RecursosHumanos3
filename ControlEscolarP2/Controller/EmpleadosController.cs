using NLog;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;

namespace RecursosHumanos.Controller
{
    /// <summary>
    /// Controlador encargado de gestionar operaciones relacionadas con los empleados.
    /// </summary>
    public class EmpleadosController
    {
        private readonly EmpleadosDataAccess _empleadosAccess;
        private readonly PersonasDataAccess _personasAccess;
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.EmpleadosController");

        /// <summary>
        /// Constructor que inicializa las clases de acceso a datos.
        /// </summary>
        public EmpleadosController()
        {
            _empleadosAccess = new EmpleadosDataAccess();
            _personasAccess = new PersonasDataAccess();
        }

        /// <summary>
        /// Registra un nuevo empleado en la base de datos.
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public (bool exito, int idEmpleado, string mensaje) RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                if (_empleadosAccess.ExisteMatricula(empleado.Matricula))
                {
                    return (false, 0, "La matrícula ya existe.");
                }

                int idGenerado = _empleadosAccess.InsertarEmpleado(empleado);
                return idGenerado > 0
                    ? (true, idGenerado, "Empleado registrado correctamente.")
                    : (false, 0, "Error al insertar empleado.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado al registrar empleado.");
                return (false, 0, "Error inesperado: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los empleados registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public List<Empleado> ObtenerEmpleados()
        {
            try
            {
                return _empleadosAccess.ObtenerTodosLosEmpleados();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener lista de empleados");
                return new List<Empleado>();
            }
        }

        /// <summary>
        /// Da de baja (desactiva) un empleado en la base de datos.
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
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
                _logger.Error(ex, $"Error al dar de baja empleado ID {idEmpleado}");
                return (false, "Error inesperado al dar de baja el empleado.");
            }
        }

        /// <summary>
        /// Actualiza los datos del empleado y su persona asociada.
        /// </summary>
        /// <param name="empleado">Objeto empleado con los datos actualizados</param>
        /// <returns>True si se actualizó correctamente</returns>
        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                _logger.Debug($"Actualizando empleado con ID {empleado.Id_Empleado} y persona ID {empleado.Id_Persona}");

                // Actualiza la persona primero
                bool personaActualizada = _personasAccess.ActualizarPersona(empleado.DatosPersonales);
                if (!personaActualizada)
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {empleado.Id_Persona}");
                    return false;
                }

                // Luego actualiza los datos del empleado
                //bool empleadoActualizado = _empleadosAccess.ActualizarEmpleado(empleado);
                //if (!empleadoActualizado)
                {
                    _logger.Warn($"No se pudo actualizar el empleado con ID {empleado.Id_Empleado}");
                    return false;
                }

                _logger.Info($"Empleado con ID {empleado.Id_Empleado} actualizado correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el empleado con ID {empleado.Id_Empleado}");
                return false;
            }
        }

        /// <summary>  
        /// Obtiene un empleado por su ID.  
        /// </summary>  
        /// <param name="idEmpleado"></param>  
        /// <returns></returns>  
        public Empleado? ObtenerEmpleadoPorId(int idEmpleado)
        {
            try
            {
                _logger.Debug($"Obteniendo empleado con ID {idEmpleado}");
                return _empleadosAccess.ObtenerEmpleadoPorId(idEmpleado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener empleado con ID {idEmpleado}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene un empleado por su matrícula.
        /// </summary>
        /// <param name="matricula">Matrícula del empleado.</param>
        /// <returns>El objeto Empleado si se encuentra, null si no existe o hay error.</returns>
        public Empleado? ObtenerEmpleadoPorMatricula(string matricula)
        {
            try
            {
                _logger.Debug($"Obteniendo empleado con matrícula {matricula}");
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
