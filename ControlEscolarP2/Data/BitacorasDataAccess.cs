using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NLog;
using RecursosHumanos.Utilities;
using RecursosHumanos.Model;

namespace RecursosHumanos.Data
{
    public class BitacorasDataAccess
    {
        // Logger para registrar errores y eventos
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.BitacorasDataAccess");

        // Acceso a base de datos PostgreSQL
        private readonly PostgreSQLDataAccess _dbAccess;

        // Constructor: inicializa conexiones
        public BitacorasDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar BitacorasDataAccess");
                throw;
            }
        }

        /// <summary>
        /// Obtiene las auditorías filtradas según los parámetros proporcionados.
        /// </summary>
        /// <param name="idTipo">Filtrar por id_tipo</param>
        /// <param name="idAccion">Filtrar por id_accion</param>
        /// <param name="fechaInicio">Filtrar por fecha de inicio</param>
        /// <param name="fechaFin">Filtrar por fecha de fin</param>
        /// <param name="ipEquipo">Filtrar por ip_equipo</param>
        /// <param name="nombreEquipo">Filtrar por nombre_equipo</param>
        /// <param name="idUsuario">Filtrar por id_usuario</param>
        /// <param name="estatus">Filtrar por estatus</param>
        /// <returns>Lista de auditorías filtradas</returns>
        public List<Auditoria> ObtenerAuditoriasPorFiltros(int? idTipo = null, int? idAccion = null,
                                                          DateTime? fechaInicio = null, DateTime? fechaFin = null,
                                                          string ipEquipo = null, string nombreEquipo = null,
                                                          int? idUsuario = null, int? estatus = null)
        {
            List<Auditoria> auditorias = new List<Auditoria>();

            try
            {
                // Construcción de la consulta SQL dinámica con filtros
                string query = "SELECT a.id_auditoria, a.id_tipo, a.id_accion, a.fecha_movimiento, " +
                               "a.ip_equipo, a.nombre_equipo, a.detalle, a.id_usuario, a.estatus, u.usuario " +
                               "FROM audits.auditoria a " +
                               "INNER JOIN administration.usuario u ON a.id_usuario = u.id_usuario " +
                               "WHERE 1 = 1";  // Esto es para que sea más fácil agregar filtros dinámicos

                // Lista de parámetros
                var parametros = new List<NpgsqlParameter>();

                // Agregar filtros dinámicos
                if (idTipo.HasValue)
                {
                    query += " AND a.id_tipo = @IdTipo";
                    parametros.Add(_dbAccess.CreateParameter("@IdTipo", idTipo.Value));
                }
                if (idAccion.HasValue)
                {
                    query += " AND a.id_accion = @IdAccion";
                    parametros.Add(_dbAccess.CreateParameter("@IdAccion", idAccion.Value));
                }
                if (fechaInicio.HasValue)
                {
                    query += " AND a.fecha_movimiento >= @FechaInicio";
                    parametros.Add(_dbAccess.CreateParameter("@FechaInicio", fechaInicio.Value));
                }
                if (fechaFin.HasValue)
                {
                    query += " AND a.fecha_movimiento <= @FechaFin";
                    parametros.Add(_dbAccess.CreateParameter("@FechaFin", fechaFin.Value));
                }
                if (!string.IsNullOrEmpty(ipEquipo))
                {
                    query += " AND a.ip_equipo LIKE @IpEquipo";
                    parametros.Add(_dbAccess.CreateParameter("@IpEquipo", "%" + ipEquipo + "%"));
                }
                if (!string.IsNullOrEmpty(nombreEquipo))
                {
                    query += " AND a.nombre_equipo LIKE @NombreEquipo";
                    parametros.Add(_dbAccess.CreateParameter("@NombreEquipo", "%" + nombreEquipo + "%"));
                }
                if (idUsuario.HasValue)
                {
                    query += " AND a.id_usuario = @IdUsuario";
                    parametros.Add(_dbAccess.CreateParameter("@IdUsuario", idUsuario.Value));
                }
                if (estatus.HasValue)
                {
                    query += " AND a.estatus = @Estatus";
                    parametros.Add(_dbAccess.CreateParameter("@Estatus", estatus.Value));
                }

                query += " ORDER BY a.fecha_movimiento DESC";  // Ordenar por fecha de movimiento

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, parametros.ToArray());

                foreach (DataRow row in resultado.Rows)
                {
                    // Construir la auditoría
                    Auditoria auditoria = new Auditoria
                    {
                        Id_Auditoria = Convert.ToInt32(row["id_auditoria"]),
                        Id_Tipo = Convert.ToInt16(row["id_tipo"]),
                        Id_Accion = Convert.ToInt16(row["id_accion"]),
                        Fecha_Movimiento = Convert.ToDateTime(row["fecha_movimiento"]),
                        Ip_Equipo = row["ip_equipo"].ToString() ?? "",
                        Nombre_Equipo = row["nombre_equipo"].ToString() ?? "",
                        Detalle = row["detalle"].ToString() ?? "",
                        Id_Usuario = Convert.ToInt32(row["id_usuario"]),
                        Estatus = Convert.ToInt16(row["estatus"]),
                        UsuarioResponsable = new Usuario
                        {
                            UsuarioNombre = row["usuario"].ToString() ?? ""
                        } // Usuario que realizó la auditoría
                    };

                    auditorias.Add(auditoria);
                }

                _logger.Info($"Se obtuvieron {auditorias.Count} auditorías.");
                return auditorias;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las auditorías con los filtros especificados.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Inserta una nueva auditoría en la base de datos.
        /// </summary>
        /// <param name="auditoria">Objeto Auditoria con los datos a insertar</param>
        /// <returns>ID de la nueva auditoría o -1 si falla</returns>
        public int InsertarAuditoria(Auditoria auditoria)
        {
            try
            {
                // Preparar la consulta para insertar la auditoría
                string query = @"
                    INSERT INTO audits.auditoria 
                    (id_tipo, id_accion, movimiento, fecha_movimiento, ip_equipo, nombre_equipo, detalle, id_usuario, estatus)
                    VALUES 
                    (@IdTipo, @IdAccion, @Movimiento, @FechaMovimiento, @IpEquipo, @NombreEquipo, @Detalle, @IdUsuario, @Estatus)
                    RETURNING id_auditoria;";

                // Crear los parámetros
                var parametros = new[]
                {
                    _dbAccess.CreateParameter("@IdTipo", auditoria.Id_Tipo),
                    _dbAccess.CreateParameter("@IdAccion", auditoria.Id_Accion),
                    _dbAccess.CreateParameter("@movimiento", auditoria.Movimiento),
                    _dbAccess.CreateParameter("@FechaMovimiento", auditoria.Fecha_Movimiento),
                    _dbAccess.CreateParameter("@IpEquipo", auditoria.Ip_Equipo),
                    _dbAccess.CreateParameter("@NombreEquipo", auditoria.Nombre_Equipo),
                    _dbAccess.CreateParameter("@Detalle", auditoria.Detalle),
                    _dbAccess.CreateParameter("@IdUsuario", auditoria.Id_Usuario),
                    _dbAccess.CreateParameter("@Estatus", auditoria.Estatus)
                };

                // Ejecutar la inserción
                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);

                int idGenerado = Convert.ToInt32(resultado);
                _logger.Info($"Auditoría insertada correctamente con ID {idGenerado}");

                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar auditoría.");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        /// <summary>
        /// Obtiene una auditoría por su ID.
        /// </summary>
        /// <param name="idAuditoria">ID de la auditoría</param>
        /// <returns>Auditoria correspondiente al ID</returns>
        internal Auditoria ObtenerAuditoriaPorId(int idAuditoria)
        {
            try
            {
                // Consulta para obtener la auditoría por ID
                string query = @"
            SELECT a.id_auditoria, a.id_tipo, a.id_accion, a.movimiento, a.fecha_movimiento, 
                   a.ip_equipo, a.nombre_equipo, a.detalle, a.id_usuario, a.estatus, u.usuario 
            FROM audits.auditoria a 
            INNER JOIN administration.usuario u ON a.id_usuario = u.id_usuario 
            WHERE a.id_auditoria = @IdAuditoria";  // Filtrar por ID de auditoría

                // Crear los parámetros
                var parametros = new[]
                {
            _dbAccess.CreateParameter("@IdAuditoria", idAuditoria)  // Parámetro de auditoría ID
        };

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, parametros);

                if (resultado.Rows.Count == 0)
                {
                    // Si no se encuentra la auditoría, devolver null
                    return null;
                }

                // Construir la auditoría con todos los datos, incluyendo 'movimiento'
                DataRow row = resultado.Rows[0];
                Auditoria auditoria = new Auditoria
                {
                    Id_Auditoria = Convert.ToInt32(row["id_auditoria"]),
                    Id_Tipo = Convert.ToInt16(row["id_tipo"]),
                    Id_Accion = Convert.ToInt16(row["id_accion"]),
                    Movimiento = Convert.ToInt16(row["movimiento"]),  // Aquí se agrega 'movimiento'
                    Fecha_Movimiento = Convert.ToDateTime(row["fecha_movimiento"]),
                    Ip_Equipo = row["ip_equipo"].ToString() ?? "",
                    Nombre_Equipo = row["nombre_equipo"].ToString() ?? "",
                    Detalle = row["detalle"].ToString() ?? "",
                    Id_Usuario = Convert.ToInt32(row["id_usuario"]),
                    Estatus = Convert.ToInt16(row["estatus"]),
                    UsuarioResponsable = new Usuario
                    {
                        UsuarioNombre = row["usuario"].ToString() ?? ""  // Usuario que realizó la auditoría
                    }
                };

                return auditoria;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener auditoría por ID {idAuditoria}");
                throw;  // Rethrow the exception for higher level handling
            }
            finally
            {
                _dbAccess.Disconnect();  // Always close the database connection in the finally block
            }
        }

    }
}
