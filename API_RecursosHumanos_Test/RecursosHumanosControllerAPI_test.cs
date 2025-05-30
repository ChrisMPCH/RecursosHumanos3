using Microsoft.AspNetCore.Mvc;
using RecursosHumanosCore.Controller;
using Microsoft.AspNetCore.Mvc;
using RecursosHumanosCore.Model;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.DataAccess;
using RecursosHumanosCore.Controllers;


namespace API_RecursosHumanos_Test
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursosHumanosControllerAPI_test: ControllerBase
    {
        
            private readonly UsuariosController _usuariosController;
            private readonly EmpleadosDataAccess _empleadosDataAccess;
            private readonly ContratoController _contratosController;
            private readonly AsistenciaDataAccess _asistenciaDataAccess;
            private readonly AusenciaController _ausenciasController;

        private readonly ILogger<RecursosHumanosControllerAPI_test> _logger;

            public RecursosHumanosControllerAPI_test(ContratoController contratoController, UsuariosController usuariosController, EmpleadosDataAccess empleadosDataAccess, AsistenciaDataAccess asistenciaDataAccess, AusenciaController ausenciaController, ILogger<RecursosHumanosControllerAPI_test> logger)
            {
                _usuariosController = usuariosController;
                _empleadosDataAccess = empleadosDataAccess;
                _contratosController = contratoController;
                _asistenciaDataAccess = asistenciaDataAccess;
                _ausenciasController = ausenciaController;
            _logger = logger;
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

        //Obtener información de días trabajados
        [HttpGet("obtenerInfo")]
        public IActionResult ObtenerInformacionDiasTrabajados([FromQuery] string matricula, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                Empleado empleado = _empleadosDataAccess.ObtenerEmpleadoPorMatricula(matricula);
                if (empleado == null)
                    return NotFound($"Empleado con matrícula {matricula} no encontrado.");

                var contrato = _contratosController.ObtenerContratoActivoPorMatricula(matricula);
                if (contrato == null)
                    return BadRequest("El empleado no tiene un contrato activo.");

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

                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener información de días trabajados para el empleado {matricula}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
