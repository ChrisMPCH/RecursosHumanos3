using System;
using System.Collections.Generic;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.Model;
using NLog;
using RecursosHumanosCore.Utilities;
using RecursosHumanosCore.Controller;


namespace RecursosHumanosCore.Controller
{
    public class EmpleadosController
    {
        private readonly EmpleadosDataAccess _empleadosAccess;
        private readonly PersonasDataAccess _personasAccess;
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.EmpleadosController");
        private static readonly AuditoriasController _auditoriasController = new AuditoriasController(); 

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
        public (bool exito, string mensaje) RegistrarEmpleado(Empleado empleado, int idUsuario)
        {
            try
            {
                if (_empleadosAccess.ExisteEmpleadoPorMatricula(empleado.Matricula))
                {
                    return (false, "La matrícula ya existe.");
                }

                // Insertar el empleado con la persona asociada
                int idGenerado = _empleadosAccess.InsertarEmpleado(empleado);

                if (idGenerado > 0)
                {
                    _auditoriasController.RegistrarAuditoriaGenerica(1, 1, (short)idGenerado, idUsuario); // 1 = Empleados, 1 = Insertar
                    return (true, "Empleado registrado correctamente.");
                }

                return (false, "Error al insertar empleado.");
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
        public (bool exito, string mensaje) EliminarEmpleado(int idEmpleado, int idUsuario)
        {
            try
            {
                bool eliminado = _empleadosAccess.EliminarUsuario(idEmpleado);

                if (eliminado)
                {
                    _auditoriasController.RegistrarAuditoriaGenerica(1, 0, (short)idEmpleado, idUsuario); // módulo 2 = empleados, acción 0 = eliminar
                }

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

        /// Actualiza los datos del empleado y su persona asociada.
        /// </summary>
        /// <param name="empleado">Objeto empleado con los datos actualizados</param>
        /// <returns>True si se actualizó correctamente</returns>
        public bool ActualizarEmpleado(Empleado empleado, int idUsuario)
        {
            try
            {
                bool actualizado = _empleadosAccess.ActualizarEmpleado(empleado);
                if (actualizado)
                {
                    _auditoriasController.RegistrarAuditoriaGenerica(1, 2, (short)empleado.Id_Empleado, idUsuario);
                    // acción 2 = actualizar
                }
                return actualizado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado al actualizar empleado.");
                return false;
            }
        }

        /// <summary>
        /// Devuelve un empleado dado su número de matrícula
        /// </summary>
        /// <param name="matricula">Matrícula del empleado</param>
        /// <returns>Empleado o null</returns>
        public Empleado ObtenerEmpleadoPorMatricula(string matricula)
        {
            try
            {
                return _empleadosAccess.ObtenerEmpleadoPorMatricula(matricula);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en lógica de negocio al buscar empleado por matrícula");
                return null;
            }
        }

        public double ObtenerPorcentajeEmpleadosActivos()
        {
            try
            {
                var (totalEmpleados, empleadosActivos) = _empleadosAccess.ContarEmpleados();

                if (totalEmpleados == 0)
                {
                    _logger.Warn("No hay empleados registrados para calcular el porcentaje.");
                    return 0.0;
                }

                double porcentaje = (double)empleadosActivos / totalEmpleados * 100;
                _logger.Info($"Porcentaje de empleados activos: {porcentaje}%");
                return porcentaje;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el porcentaje de empleados activos");
                throw;
            }
        }

        public (bool exito, string mensaje) ExportarEmpleadosExcel()
        {
            try
            {
                var empleados = ObtenerEmpleados();

                var nombre = $"Empleados_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string rutaArchivo = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "exportados",
                    nombre
                );

                if (!Directory.Exists(Path.GetDirectoryName(rutaArchivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));

                bool resultado = ExcelExporter.ExportToExcel(empleados, rutaArchivo, "Empleados");

                if (resultado)
                {
                    _logger.Info($"Archivo exportado correctamente a {rutaArchivo}");
                    return (true, "La exportación a Excel se completó exitosamente.");
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo.");
                    return (false, "La exportación a Excel ha fallado.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar empleados a Excel");
                return (false, "Ocurrió un error al exportar los empleados.");
            }
        }


    }
}
