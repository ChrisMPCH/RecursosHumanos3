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
        public (bool exito, string mensaje) RegistrarEmpleado(Empleado empleado)
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
                    _auditoriasController.RegistrarAuditoriaGenerica(1, 1, (short)idGenerado); // módulo 2 = Empleados, acción 1 = insertar
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
        public (bool exito, string mensaje) EliminarEmpleado(int idEmpleado)
        {
            try
            {
                bool eliminado = _empleadosAccess.EliminarUsuario(idEmpleado);

                if (eliminado)
                {
                    _auditoriasController.RegistrarAuditoriaGenerica(1, 0, (short)idEmpleado); // módulo 2 = empleados, acción 0 = eliminar
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
        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                bool actualizado = _empleadosAccess.ActualizarEmpleado(empleado);
                if (actualizado)
                {
                    _auditoriasController.RegistrarAuditoriaGenerica(1, 2, (short)empleado.Id_Empleado); // acción 2 = actualizar
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

        public bool ExportarEmpleadosExcel()
        {
            try
            {
                // Obtener la lista de empleados (ajusta este método si es necesario)
                var empleados = ObtenerEmpleados();

                var nombre = $"Empleados_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                // Ruta del archivo
                string rutaArchivo = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "exportados",
                    nombre
                );

                // Crear directorio si no existe
                if (!Directory.Exists(Path.GetDirectoryName(rutaArchivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));

                // Exportar sin filtro
                bool resultado = ExcelExporter.ExportToExcel(empleados, rutaArchivo, "Empleados");

                if (resultado)
                {
                    _logger.Info($"Archivo exportado correctamente a {rutaArchivo}");
                    MessageBox.Show("La exportación a Excel se completó exitosamente.","Exportación Completada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    _logger.Warn("No se pudo exportar el archivo.");
                    MessageBox.Show("La exportación a Excel ha fallado.","Exportación incompleta",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al exportar empleados a Excel");
                return false;
            }
        }

    }
}
