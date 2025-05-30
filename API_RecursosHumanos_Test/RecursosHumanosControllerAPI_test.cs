using Microsoft.AspNetCore.Mvc;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Model;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.DataAccess;
using RecursosHumanosCore.Controllers;

namespace API_RecursosHumanos_Test
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursosHumanosControllerAPI_test : ControllerBase
    {
        private readonly UsuariosController _usuariosController;
        private readonly EmpleadosDataAccess _empleadosDataAccess;
        private readonly ContratoController _contratosController;
        private readonly AsistenciaDataAccess _asistenciaDataAccess;
        private readonly AusenciaController _ausenciasController;
        private readonly ILogger<RecursosHumanosControllerAPI_test> _logger;

        public RecursosHumanosControllerAPI_test(
            ILogger<RecursosHumanosControllerAPI_test> logger,
            ContratoController contratoController,
            UsuariosController usuariosController,
            EmpleadosDataAccess empleadosDataAccess,
            AsistenciaDataAccess asistenciaDataAccess,
            AusenciaController ausenciaController)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usuariosController = usuariosController ?? throw new ArgumentNullException(nameof(usuariosController));
            _empleadosDataAccess = empleadosDataAccess ?? throw new ArgumentNullException(nameof(empleadosDataAccess));
            _contratosController = contratoController ?? throw new ArgumentNullException(nameof(contratoController));
            _asistenciaDataAccess = asistenciaDataAccess ?? throw new ArgumentNullException(nameof(asistenciaDataAccess));
            _ausenciasController = ausenciaController ?? throw new ArgumentNullException(nameof(ausenciaController));
        }

        public class WorkDaysInfo
        {
            public string Matricula { get; set; }
            public string NombreEmpleado { get; set; }
            public string EstatusEmpleado { get; set; }
            public string EstatusContrato { get; set; }
            public int DiasTrabajados { get; set; }
            public double Salario { get; set; }
        }

        public class EmpleadoInfo
        {
            public string Matricula { get; set; }
            public string NombreEmpleado { get; set; }
            public string Departamento { get; set; }
            public string Puesto { get; set; }
            public string EstatusEmpleado { get; set; }
            public string EstatusContrato { get; set; }
            public double Salario { get; set; }
            public DateTime FechaIngreso { get; set; }
            public DateTime? FechaBaja { get; set; }
        }

        [HttpGet("obtenerInfo")]
        public async Task<IActionResult> ObtenerInformacionDiasTrabajados(
            [FromQuery] string matricula,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            try
            {
                if (string.IsNullOrEmpty(matricula))
                {
                    return BadRequest("La matrícula es requerida");
                }

                if (fechaInicio > fechaFin)
                {
                    return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin");
                }

                _logger.LogInformation($"Iniciando consulta para matricula={matricula}, fechaInicio={fechaInicio:yyyy-MM-dd}, fechaFin={fechaFin:yyyy-MM-dd}");

                // Obtener información del empleado
                var empleado = _empleadosDataAccess.ObtenerEmpleadoPorMatricula(matricula);
                if (empleado == null)
                {
                    _logger.LogWarning($"Empleado con matrícula {matricula} no encontrado");
                    return NotFound(new { mensaje = $"Empleado con matrícula {matricula} no encontrado" });
                }

                // Obtener contrato activo
                var contrato = _contratosController.ObtenerContratoActivoPorMatricula(matricula);
                if (contrato == null)
                {
                    _logger.LogWarning($"No se encontró contrato activo para el empleado {matricula}");
                    return NotFound(new { mensaje = "El empleado no tiene un contrato activo" });
                }

                // Contar días trabajados
                int diasTrabajados = _asistenciaDataAccess.ContarDiasTrabajados(empleado.Id_Empleado, fechaInicio, fechaFin);

                var info = new WorkDaysInfo
                {
                    Matricula = empleado.Matricula,
                    NombreEmpleado = empleado.Nombre,
                    EstatusEmpleado = empleado.EstatusTexto,
                    EstatusContrato = contrato.Estatus ? "Activo" : "Inactivo",
                    DiasTrabajados = diasTrabajados,
                    Salario = contrato.Sueldo
                };

                _logger.LogInformation($"Consulta exitosa para matricula={matricula}");
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener información de días trabajados para el empleado {matricula}");
                return StatusCode(500, new { 
                    mensaje = "Error interno del servidor",
                    error = ex.Message,
                    detalles = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("obtenerTodosEmpleados")]
        public async Task<IActionResult> ObtenerTodosEmpleados()
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de todos los empleados");

                // Obtener lista de empleados
                var empleados = _empleadosDataAccess.ObtenerTodosLosEmpleados();
                if (empleados == null || !empleados.Any())
                {
                    _logger.LogWarning("No se encontraron empleados");
                    return NotFound(new { mensaje = "No se encontraron empleados" });
                }

                var empleadosInfo = new List<EmpleadoInfo>();

                foreach (var empleado in empleados)
                {
                    var contrato = _contratosController.ObtenerContratoActivoPorMatricula(empleado.Matricula);
                    
                    empleadosInfo.Add(new EmpleadoInfo
                    {
                        Matricula = empleado.Matricula,
                        NombreEmpleado = empleado.Nombre,
                        Departamento = empleado.Departamento,
                        Puesto = empleado.Puesto,
                        EstatusEmpleado = empleado.EstatusTexto,
                        EstatusContrato = contrato?.Estatus == true ? "Activo" : "Inactivo",
                        Salario = contrato?.Sueldo ?? 0,
                        FechaIngreso = empleado.Fecha_Ingreso,
                        FechaBaja = empleado.Fecha_Baja
                    });
                }

                _logger.LogInformation($"Consulta exitosa. Se encontraron {empleadosInfo.Count} empleados");
                return Ok(empleadosInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de empleados");
                return StatusCode(500, new { 
                    mensaje = "Error interno del servidor",
                    error = ex.Message,
                    detalles = ex.InnerException?.Message
                });
            }
        }
    }
}
