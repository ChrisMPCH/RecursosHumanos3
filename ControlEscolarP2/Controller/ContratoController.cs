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

        public List<Contrato> ObtenerTodosLosContratosPorMatricula(string matricula)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(matricula))
                {
                    _logger.Warn("Matrícula vacía al intentar buscar contratos.");
                    return new List<Contrato>();
                }

                if (!EmpleadoNegocio.EsNoMatriculaValido(matricula))
                {
                    _logger.Warn($"Formato de matrícula inválido: {matricula}");
                    return new List<Contrato>();
                }

                var contratos = _contratosDataAccess.ObtenerContratosPorMatricula(matricula); // ✅ usa el nuevo

                if (contratos == null || contratos.Count == 0)
                {
                    _logger.Info($"No se encontraron contratos para la matrícula {matricula}.");
                    return new List<Contrato>();
                }

                return contratos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al buscar contratos para la matrícula: {matricula}");
                return new List<Contrato>();
            }
        }

        //registrar un nuevo contrato
        //registrar un nuevo contrato
        public (int id, string mensaje) RegistrarContrato(string matricula, Contrato contrato)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(matricula) || contrato == null)
                    return (-1, "La matrícula o el contrato son inválidos.");

                if (!EmpleadoNegocio.EsNoMatriculaValido(matricula))
                    return (-2, "Formato de matrícula inválido.");

                Empleado empleado = _empleadosDataAccess.ObtenerEmpleadoPorMatricula(matricula);

                if (empleado == null || empleado.Estatus == 0)
                    return (-3, "No se encontró un empleado activo con esa matrícula.");

                if (_contratosDataAccess.TieneContratoActivo(matricula))
                    return (-4, "Este empleado ya tiene un contrato activo.");

                contrato.Matricula = matricula;

                int idContrato = _contratosDataAccess.InsertarContrato(contrato, empleado.Id_Empleado);

                if (idContrato <= 0)
                    return (-5, "Error al registrar el contrato en la base de datos.");

                // REGISTRO DE AUDITORÍA
                AuditoriasController auditoriasController = new AuditoriasController();
                auditoriasController.RegistrarAuditoriaGenerica(2, 1, (short)idContrato); // 2 = Contrato, 1 = Alta

                return (idContrato, "Contrato registrado exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al registrar contrato para matrícula: {matricula}");
                return (-6, "Error inesperado: " + ex.Message);
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
        public List<Contrato> ObtenerContratosFiltrados(
     string matricula,
     int tipoContrato,
     int estatus,
     int departamento,
     DateTime? fechaInicio,
     DateTime? fechaFin)
        {
            try
            {
                // Pasa los parámetros directamente al DataAccess (ajustado para nullables y -1)
                return _contratosDataAccess.ObtenerContratosFiltrados(
                    matricula,
                    tipoContrato,
                    estatus,
                    departamento,
                    fechaInicio,
                    fechaFin
                );
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
                if (contrato == null)
                {
                    return (false, "No se proporcionaron datos del contrato.");
                }

                if (!EmpleadoNegocio.EsNoMatriculaValido(contrato.Matricula))
                {
                    return (false, "El formato de matrícula no es válido.");
                }

                // Verificar si el contrato existe
                Contrato? contratoExistente = _contratosDataAccess.ObtenerContratoPorId(contrato.Id_Contrato);
                if (contratoExistente == null)
                {
                    return (false, $"No se encontró el contrato con ID {contrato.Id_Contrato}.");
                }

                // Obtener el ID del empleado desde la matrícula
                var empleado = _empleadosDataAccess.ObtenerEmpleadoPorMatricula(contrato.Matricula);
                if (empleado == null)
                {
                    return (false, "No se encontró el empleado correspondiente a la matrícula.");
                }

                _logger.Info($"Actualizando contrato con ID: {contrato.Id_Contrato} para el empleado ID: {empleado.Id_Empleado}");

                // Llamar a ActualizarContrato con el ID correcto del empleado
                bool resultado = _contratosDataAccess.ActualizarContrato(contrato, empleado.Id_Empleado);

                if (!resultado)
                {
                    _logger.Error($"Error al actualizar el contrato con ID {contrato.Id_Contrato}.");
                    return (false, "Error al actualizar el contrato en la base de datos.");
                }

                _logger.Info($"Contrato con ID {contrato.Id_Contrato} actualizado exitosamente.");
                AuditoriasController auditoriasController = new AuditoriasController();
                auditoriasController.RegistrarAuditoriaGenerica(2, 2, (short)contrato.Id_Contrato); // 2 = Contrato, 3 = Actualización
                return (true, "Contrato actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error inesperado al actualizar contrato con ID: {contrato.Id_Contrato}");
                return (false, "Error inesperado al actualizar el contrato.");
            }
        }

        public Contrato? ObtenerContratoActivoPorMatricula(string matricula)
        {
            var contratos = ObtenerTodosLosContratosPorMatricula(matricula);
            return contratos.FirstOrDefault(c => c.Estatus);
        }
        public double ObtenerPorcentajeContratosActivos()
        {
            try
            {
                var (totalContratos, contratosActivos) = _contratosDataAccess.ContarContratos();

                if (totalContratos == 0)
                {
                    _logger.Warn("No hay contratos registrados para calcular el porcentaje.");
                    return 0.0;
                }

                double porcentaje = (double)contratosActivos / totalContratos * 100;
                _logger.Info($"Porcentaje de contratos activos: {porcentaje}%");
                return porcentaje;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el porcentaje de contratos activos");
                throw;
            }
        }
    }
}
