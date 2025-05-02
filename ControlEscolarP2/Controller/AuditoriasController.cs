using System;
using System.Collections.Generic;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using NLog;
using RecursosHumanos.Utilities;
using System.Net.NetworkInformation;
using System.Net;
using RecursosHumanos.View;

namespace RecursosHumanos.Controller
{
    public class AuditoriasController
    {
        public readonly BitacorasDataAccess _bitacorasDataAccess;
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.AuditoriasController");

        // Constructor que inicializa el acceso a datos de Bitacoras
        public AuditoriasController()
        {
            _bitacorasDataAccess = new BitacorasDataAccess();
        }

        /// <summary>
        /// Método para registrar una nueva auditoría
        /// </summary>
        /// <param name="auditoria">Objeto Auditoria con los datos a insertar</param>
        /// <returns>Resultado de la operación (exito, mensaje)</returns>
        public (bool exito, string mensaje) RegistrarAuditoria(Auditoria auditoria)
        {
            try
            {
                int idGenerado = _bitacorasDataAccess.InsertarAuditoria(auditoria);

                if (idGenerado > 0)
                {
                    return (true, $"Auditoría registrada correctamente con ID {idGenerado}");
                }
                else
                {
                    return (false, "Error al registrar la auditoría.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar la auditoría");
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Método para obtener todas las auditorías con filtros opcionales
        /// </summary>
        /// <param name="movimiento">Filtrar por movimiento (opcional)</param>
        /// <param name="idTipo">Filtrar por id_tipo (opcional)</param>
        /// <param name="idAccion">Filtrar por id_accion (opcional)</param>
        /// <param name="fechaInicio">Filtrar por fecha de inicio (opcional)</param>
        /// <param name="fechaFin">Filtrar por fecha de fin (opcional)</param>
        /// <param name="ipEquipo">Filtrar por ip_equipo (opcional)</param>
        /// <param name="nombreEquipo">Filtrar por nombre_equipo (opcional)</param>
        /// <param name="idUsuario">Filtrar por id_usuario (opcional)</param>
        /// <param name="estatus">Filtrar por estatus (opcional)</param>
        /// <returns>Lista de auditorías que cumplen con los filtros</returns>
        public List<Auditoria> ObtenerAuditorias(int? idTipo = null, int? idAccion = null,
                                                 DateTime? fechaInicio = null, DateTime? fechaFin = null,
                                                 string ipEquipo = null, string nombreEquipo = null,
                                                 int? idUsuario = null, int? estatus = null)
        {
            try
            {
                // Llamar al método de BitacorasDataAccess para obtener auditorías con los filtros
                List<Auditoria> auditorias = _bitacorasDataAccess.ObtenerAuditoriasPorFiltros(
                    idTipo, idAccion, fechaInicio, fechaFin, ipEquipo, nombreEquipo, idUsuario, estatus);

                return auditorias;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las auditorías con los filtros especificados.");
                return new List<Auditoria>(); // Retornar una lista vacía en caso de error
            }
        }

        /// <summary>
        /// Método para obtener una auditoría por su ID
        /// </summary>
        /// <param name="idAuditoria"></param>
        /// <returns></returns>
        public Auditoria ObtenerAuditoriaPorId(int idAuditoria)
        {
            try
            {
                Auditoria auditoria = _bitacorasDataAccess.ObtenerAuditoriaPorId(idAuditoria);
                return auditoria;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener la auditoría con ID {idAuditoria}");
                return null; // Retornar null en caso de error
            }
        }

        /// <summary>
        /// Super método para registrar auditorías de alta, baja o actualización de cualquier entidad.
        /// </summary>
        /// <param name="idEntidad">ID de la entidad afectada (Empleado, Contrato, Usuario, etc.)</param>
        /// <param name="idAccion">Tipo de acción (Alta, Baja, Actualización)</param>
        /// <param name="detalle">Detalle de la acción realizada</param>
        /// <param name="idUsuario">ID del usuario responsable de la acción</param>
        /// <param name="estatus">Estatus de la auditoría</param>
        /// <param name="usuarioResponsable">Usuario que realizó la acción</param>
        /// <returns>Resultado de la operación (exito, mensaje)</returns>
        /// idEntidad
        ///{ 1, "Empleado" },
        ///{ 2, "Contrato" },
        ///{ 3, "Usuario" },
        ///{ 4, "Roles" },
        ///{ 5, "Permisos" },
        ///{ 6, "Otros" }
        ///idAccion
        ///{ 1, "Alta" },
        ///{ 2, "Actualizacion" },
        ///{ 0, "Baja" }
        ///movimiento es el id del registro afectado

        public (bool exito, string mensaje) RegistrarAuditoriaGenerica(int idEntidad, int idAccion, short movimiento)
        {
            try
            {
                // Obtener el nombre y la IP del equipo si no se proporcionan
                var (nombre, ip) = ObtenerNombreYIPEquipo();
                int idUsuario = frmLogin.usuarioLogueado.Id_Usuario; // Obtener el ID del usuario logueado

                // Obtener el usuario responsable
                UsuariosController usuariosController = new UsuariosController();

                Auditoria auditoria = new Auditoria() {
                    Id_Tipo = (short)idEntidad,
                    Id_Accion = (short)idAccion,
                    Movimiento = movimiento,
                    Fecha_Movimiento = DateTime.Now,
                    Ip_Equipo = ip,
                    Nombre_Equipo = nombre,
                    Detalle = generadorDetalle(idEntidad, idAccion),
                    Id_Usuario = idUsuario,
                    Estatus = 1,
                    UsuarioResponsable = usuariosController.ObtenerUsuarioPorId(idUsuario)
                };

                // Registrar la auditoría en la base de datos
                int idGenerado = _bitacorasDataAccess.InsertarAuditoria(auditoria);

                // Si la inserción fue exitosa, devolver un mensaje de éxito
                if (idGenerado > 0)
                {
                    return (true, $"Auditoría registrada correctamente con ID {idGenerado}");
                }
                else
                {
                    return (false, "Error al registrar la auditoría.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar la auditoría");
                return (false, $"Error: {ex.Message}");
            }
        }

        internal static string generadorDetalle(int entidad, int accion)
        {
            string detalle = string.Empty;
            switch (entidad)
            {
                case 1: // Empleado
                    switch (accion)
                    {
                        case 1: // Alta
                            detalle = "Alta de empleado";
                            break;
                        case 2: // Baja
                            detalle = "Baja de empleado";
                            break;
                        case 3: // Actualización
                            detalle = "Actualización de empleado";
                            break;
                        default:
                            detalle = "Acción no reconocida de empleado";
                            break;
                    }
                    break;
                case 2: // Contrato
                    switch (accion)
                    {
                        case 1: // Alta
                            detalle = "Alta de contrato";
                            break;
                        case 2: // Baja
                            detalle = "Baja de contrato";
                            break;
                        case 3: // Actualización
                            detalle = "Actualización de contrato";
                            break;
                        default:
                            detalle = "Acción no reconocida de contrato";
                            break;
                    }
                    break;
                case 3: // Usuario
                    switch (accion)
                    {
                        case 1: // Alta
                            detalle = "Alta de usuario";
                            break;
                        case 2: // Baja
                            detalle = "Baja de usuario";
                            break;
                        case 3: // Actualización
                            detalle = "Actualización de usuario";
                            break;
                        default:
                            detalle = "Acción no reconocida de usuario";
                            break;
                    }
                    break;
                case 4: // Roles
                    switch (accion)
                    {
                        case 1: // Alta
                            detalle = "Alta de rol";
                            break;
                        case 2: // Baja
                            detalle = "Baja de rol";
                            break;
                        case 3: // Actualización
                            detalle = "Actualización de rol";
                            break;
                        default:
                            detalle = "Acción no reconocida de rol";
                            break;
                    }
                    break;
                case 5: // Permisos
                    switch (accion)
                    {
                        case 1: // Alta
                            detalle = "Alta de permiso";
                            break;
                        case 2: // Baja
                            detalle = "Baja de permiso";
                            break;
                        case 3: // Actualización
                            detalle = "Actualización de permiso";
                            break;
                        default:
                            detalle = "Acción no reconocida de permiso";
                            break;
                    }
                    break;
                case 6: // Otros
                    switch (accion)
                    {
                        case 1: // Alta
                            detalle = "Alta de otros";
                            break;
                        case 2: // Baja
                            detalle = "Baja de otros";
                            break;
                        case 3: // Actualización
                            detalle = "Actualización de otros";
                            break;
                        default:
                            detalle = "Acción no reconocida de otros";
                            break;
                    }
                    break;
                default:
                    detalle = "Entidad no reconocida";
                    break;
            }
            return detalle;
        }

        /// <summary>
        /// Método para obtener el nombre y la IP del equipo local.
        /// </summary>
        /// <returns></returns>
        public static (string nombreEquipo, string ipEquipo) ObtenerNombreYIPEquipo()
        {
            try
            {
                // Obtener el nombre del equipo
                string nombreEquipo = Dns.GetHostName();

                // Obtener las direcciones IP del equipo
                string ipEquipo = string.Empty;

                // Obtener las interfaces de red disponibles
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Filtrar las interfaces de red activas
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        // Obtener la dirección IP de la interfaz de red
                        IPInterfaceProperties properties = ni.GetIPProperties();
                        foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                        {
                            // Verificar que sea una dirección IPv4
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                ipEquipo = ip.Address.ToString();
                                break;
                            }
                        }
                    }
                    // Si encontramos una dirección IP, podemos salir del ciclo
                    if (!string.IsNullOrEmpty(ipEquipo))
                        break;
                }

                return (nombreEquipo, ipEquipo);
            }
            catch (Exception ex)
            {
                // Manejar errores
                Console.WriteLine($"Error al obtener la información del equipo: {ex.Message}");
                return (string.Empty, string.Empty);
            }
        }
        public List<(int idUsuario, string detalle, DateTime fechaMovimiento)> ObtenerUltimasAuditorias(int limite = 3)
        {
            try
            {
                return _bitacorasDataAccess.ObtenerUltimasAuditorias(limite);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las auditorías recientes");
                return new List<(int, string, DateTime)>();
            }
        }
    }
}
