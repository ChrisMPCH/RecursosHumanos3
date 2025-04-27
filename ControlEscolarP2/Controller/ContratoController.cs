using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Npgsql;
using RecursosHumanos.Model;
using RecursosHumanos.Data;
using RecursosHumanos.Bussines;

namespace RecursosHumanos.Controller
{
     class ContratoController
    {
        private static readonly Logger _logger = LogManager.GetLogger("DiseñoForms.Controller.ContratoController");
        private readonly ContratosDataAccess _contratosDataAccess;
        private readonly EmpleadosDataAccess _empleadosDataAccess;

        public ContratoController()
        {
            try
            {
                _contratosDataAccess = new ContratosDataAccess();
                _empleadosDataAccess = new EmpleadosDataAccess();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear la conexion a la base de datos");
                throw;
            }
          
        }

        public List<Contrato> ObtenerTodosLosContratos(bool soloActivos = true, int tipoFecha = 0, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                // Obtener los contratos desde la capa de acceso a datos
                var contratos = _contratosDataAccess.ObtenerTodosLosContratos(soloActivos, tipoFecha, fechaInicio, fechaFin);

                _logger.Info($"Se obtuvieron {contratos.Count} contratos correctamente");
                return contratos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de contratos");
                throw; // Propagar la excepción para que la capa superior la maneje
            }
        }
        public List<Contrato> ObtenerTodosLosContratosPorMatricula(string matricula)
        {
            return _contratosDataAccess.ObtenerContratosPorMatricula(matricula);
        }


        //registrar un nuevo contrato
        public (int id, string mensaje) RegistrarContrato(Contrato contrato, Empleado empleado)
        {
            try
            {
                // Verificar si ya existe un contrato con el mismo ID (por seguridad, aunque lo normal es que no venga definido en inserción)
                if (_contratosDataAccess.ExisteContratoPorId(contrato.Id_Contrato))
                {
                    _logger.Warn($"Intento de registrar contrato duplicado con ID: {contrato.Id_Contrato}");
                    return (-3, $"El contrato con ID {contrato.Id_Contrato} ya existe en el sistema.");
                }

                _logger.Info($"Registrando contrato para empleado ID: {empleado.Id_Persona}");

                int idContrato = _contratosDataAccess.InsertarContrato(contrato, empleado);

                if (idContrato <= 0)
                {
                    return (-4, "Error al registrar el contrato en la base de datos");
                }

                _logger.Info($"Contrato registrado exitosamente con ID: {idContrato}");
                return (idContrato, "Contrato registrado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al registrar contrato para empleado ID: {empleado.Id_Persona}");
                return (-5, $"Error inesperado: {ex.Message}");
            }
        }

        //Obtener detalles de un contrato
        public Contrato? ObtenerDetalleContrato(int idContrato)
        {
            try
            {
                _logger.Debug($"Solicitando detalle del contrato con ID: {idContrato}");
                return _contratosDataAccess.ObtenerContratoPorId(idContrato);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener los detalles del contrato con ID: {idContrato}");
                throw;
            }
        }
        public List<Contrato> ObtenerContratosFiltrados(string matricula, int tipoContrato, int estatus, int departamento, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return _contratosDataAccess.ObtenerContratosFiltrados(matricula, tipoContrato, estatus, departamento, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener contratos filtrados");
                return new List<Contrato>();
            }
        }
        public bool TieneContratoActivo(string matricula)
        {
            return _contratosDataAccess.TieneContratoActivo(matricula);
        }




        //actualizar un contrato
        public (bool exito, string mensaje) ActualizarContrato(Contrato contrato)
        {
            try
            {
                // Validaciones básicas
                if (contrato == null)
                {
                    return (false, "No se proporcionaron datos del contrato");
                }

                if (!EmpleadoNegocio.EsNoMatriculaValido(contrato.Matricula))
                {
                    return (false, "El formato de matrícula no es válido.");
                }


                // Verificar si el contrato existe
                Contrato? contratoExistente = _contratosDataAccess.ObtenerContratoPorId(contrato.Id_Contrato);

                if (contratoExistente == null)
                {
                    return (false, $"No se encontró el contrato con ID {contrato.Id_Contrato}");
                }


                _logger.Info($"Actualizando contrato con ID: {contrato.Id_Contrato} para el empleado Matricula: {contrato.Matricula}");

                bool resultado = _contratosDataAccess.ActualizarContrato(contrato);

                if (!resultado)
                {
                    _logger.Error($"Error al actualizar el contrato con ID {contrato.Id_Contrato}");
                    return (false, "Error al actualizar el contrato en la base de datos");
                }

                _logger.Info($"Contrato con ID {contrato.Id_Contrato} actualizado exitosamente");
                return (true, "Contrato actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error inesperado al actualizar contrato con ID: {contrato.Id_Contrato}");
                return (false, "Error inesperado al actualizar el contrato");
            }
        }


    }
}
