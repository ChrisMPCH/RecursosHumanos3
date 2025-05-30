using Microsoft.AspNetCore.Mvc;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Model;
using System;
using System.Collections.Generic;

namespace API_RecursosHumanos_Test
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursosHumanosControllerAPI_test : ControllerBase
    {
        private readonly UsuariosController _usuariosController;
        private readonly EmpleadosController _empleadosController;
        private readonly ContratoController _contratoController;
        private readonly AsistenciaController _asistenciaController;
        private readonly ILogger<RecursosHumanosControllerAPI_test> _logger;

        public RecursosHumanosControllerAPI_test(
            UsuariosController usuariosController,
            EmpleadosController empleadosController,
            ContratoController contratoController,
            AsistenciaController asistenciaController,
            ILogger<RecursosHumanosControllerAPI_test> logger)
        {
            _usuariosController = usuariosController;
            _empleadosController = empleadosController;
            _contratoController = contratoController;
            _asistenciaController = asistenciaController;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene los días trabajados de un empleado en un rango de fechas
        /// </summary>
        /// <param name="matricula">Matrícula del empleado</param>
        /// <param name="fechaInicio">Fecha de inicio del período</param>
        /// <param name="fechaFin">Fecha de fin del período</param>
        /// <returns>Información de días trabajados</returns>
        [HttpGet("dias-trabajados")]
        public IActionResult GetDiasTrabajados([FromQuery] string matricula, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                // 1. Validar parámetros
                if (string.IsNullOrWhiteSpace(matricula))
                {
                    return BadRequest("La matrícula es requerida.");
                }

                if (fechaInicio > fechaFin)
                {
                    return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin.");
                }

                // 2. Obtener información del empleado
                var empleado = _empleadosController.ObtenerEmpleadoPorMatricula(matricula);
                if (empleado == null)
                {
                    return NotFound($"No se encontró un empleado con la matrícula {matricula}.");
                }

                // 3. Obtener contrato activo
                var contrato = _contratoController.ObtenerContratoActivoPorMatricula(matricula);
                if (contrato == null)
                {
                    return NotFound($"No se encontró un contrato activo para el empleado con matrícula {matricula}.");
                }

                // 4. Obtener días trabajados
                int diasTrabajados = _asistenciaController.ObtenerAsistenciasCompletas(empleado.Id_Empleado, fechaInicio, fechaFin);

                // 5. Construir respuesta
                var respuesta = new DiasTrabajadosDTO
                {
                    Matricula = matricula,
                    NombreEmpleado = empleado.Nombre,
                    EstatusEmpleado = empleado.Estatus == 1 ? "Activo" : "Inactivo",
                    EstatusContrato = contrato.Estatus ? "Activo" : "Inactivo",
                    DiasTrabajados = diasTrabajados,
                    Salario = contrato.Sueldo,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener días trabajados");
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el histórico de pagos de un empleado
        /// </summary>
        /// <param name="matricula">Matrícula del empleado</param>
        /// <returns>Lista de pagos históricos</returns>
        [HttpGet("historico-pagos")]
        public IActionResult GetHistoricoPagos([FromQuery] string matricula)
        {
            try
            {
                // TODO: Implementar la lógica para obtener el histórico de pagos
                var historicoPagos = new List<HistoricoPagosDTO>();

                return Ok(historicoPagos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener histórico de pagos");
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
