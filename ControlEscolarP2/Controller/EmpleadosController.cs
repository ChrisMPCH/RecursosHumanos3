using System;
using System.Collections.Generic;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using NLog;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.Controller
{
    public class EmpleadosController
    {
        private readonly EmpleadosDataAccess _empleadosAccess;
        private readonly PersonasDataAccess _personasAccess;
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.EmpleadosController");

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
        public (bool exito, string mensaje) RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                if (_empleadosAccess.ExisteEmpleadoPorMatricula(empleado.Matricula))
                {
                    return (false, "La matrícula ya existe.");
                }

                // Insertar primero la persona asociada al empleado
                int idPersona = _personasAccess.InsertarPersona(empleado.DatosPersonales);
                if (idPersona <= 0)
                {
                    return (false, "Error al insertar la persona asociada.");
                }

                // Asignar el ID de la persona al empleado
                empleado.DatosPersonales.Id_Persona = idPersona;

                // Insertar el empleado con la persona asociada
                int idGenerado = _empleadosAccess.InsertarEmpleado(empleado);
                return idGenerado > 0
                    ? (true, "Empleado registrado correctamente.")
                    : (false, "Error al insertar empleado.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado al registrar empleado.");
                return (false, "Error inesperado: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los empleados registrados en la base de datos.
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
                _logger.Error(ex, "Error al obtener la lista de empleados.");
                return new List<Empleado>();
            }
        }

        /// <summary>
        /// Elimina un empleado de la base de datos.
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        public (bool exito, string mensaje) EliminarEmpleado(int idEmpleado)
        {
            try
            {
                bool eliminado = _empleadosAccess.EliminarUsuario(idEmpleado);
                return eliminado
                    ? (true, "Empleado eliminado correctamente.")
                    : (false, "No se pudo eliminar el empleado.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar empleado con ID {idEmpleado}");
                return (false, "Error inesperado al eliminar empleado.");
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
                // Primero actualiza los datos de la persona asociada
                bool personaActualizada = _personasAccess.ActualizarPersona(empleado.DatosPersonales);
                if (!personaActualizada)
                {
                    _logger.Warn($"No se pudo actualizar la persona con ID {empleado.DatosPersonales.Id_Persona}");
                    return false;
                }

                // Luego actualiza los datos del empleado
                bool empleadoActualizado = _empleadosAccess.ActualizarUsuario(empleado);
                if (!empleadoActualizado)
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
                return _empleadosAccess.ObtenerEmpleadoPorId(idEmpleado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener el empleado con ID {idEmpleado}");
                return null;
            }
        }
    }
}
