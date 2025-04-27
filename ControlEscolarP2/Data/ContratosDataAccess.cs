using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanos.Utilities;
using RecursosHumanos.Model;
using NLog;
using Npgsql;
using System.Data;
using RecursosHumanos.Bussines;

namespace RecursosHumanos.Data
{
     class ContratosDataAccess
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.ContratoDataAccess");
        private readonly PostgreSQLDataAccess _dbAccess = null;
        private readonly EmpleadosDataAccess _empleadosDataAccess;

        //obtener la instancia de acceso a datos
        public ContratosDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
                _empleadosDataAccess = new EmpleadosDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar el acceso a datos de ContratoDataAccess");
                throw;
            }
        }

        //Método para insertar un contrato
        public int InsertarContrato(Contrato contrato, Empleado empleado)
        {
            try
            {
                if (!EmpleadoNegocio.EsNoMatriculaValido(empleado.Matricula))
                {
                    _logger.Warn($"La matrícula {empleado.Matricula} no cumple con el formato esperado.");
                    return -2;
                }

                int idEmpleado = _empleadosDataAccess.InsertarEmpleado(empleado);
                if (idEmpleado <= 0)
                {
                    _logger.Error($"No se pudo insertar el empleado para el contrato con matrícula {empleado.Matricula}");
                    return -1;
                }

                contrato.Matricula = empleado.Matricula;

                string query = @"
                    INSERT INTO human_resours.contrato 
                    (id_empleado, id_tipocontrato, fecha_inicio, fecha_fin, hora_entrada, hora_salida, salario, descripcion, estatus)
                    VALUES 
                    (@idEmpleado, @idTipoContrato, @fechaInicio, @fechaFin, @horaEntrada, @horaSalida, @salario, @descripcion, @estatus)
                    RETURNING id_contrato";

                NpgsqlParameter[] parametros = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@idEmpleado", idEmpleado),
                    _dbAccess.CreateParameter("@idTipoContrato", contrato.Id_TipoContrato),
                    _dbAccess.CreateParameter("@fechaInicio", contrato.FechaInicio),
                    _dbAccess.CreateParameter("@fechaFin", contrato.FechaFin),
                    _dbAccess.CreateParameter("@horaEntrada", contrato.HoraEntrada),
                    _dbAccess.CreateParameter("@horaSalida", contrato.HoraSalida),
                    _dbAccess.CreateParameter("@salario", contrato.Sueldo),
                    _dbAccess.CreateParameter("@descripcion", contrato.Descripcion),
                    _dbAccess.CreateParameter("@estatus", contrato.Estatus ? 1 : 0)
                };

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametros);
                int idContrato = Convert.ToInt32(resultado);
                _logger.Info($"Contrato insertado con ID: {idContrato}");

                return idContrato;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar el contrato del empleado {empleado.Matricula}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public List<Contrato> ObtenerContratosPorMatricula(string matricula)
        {
            List<Contrato> contratos = new List<Contrato>();

            try
            {
                if (!EmpleadoNegocio.EsNoMatriculaValido(matricula))
                {
                    _logger.Warn($"La matrícula {matricula} no cumple con el formato esperado.");
                    return contratos;
                }

                string query = @"
                SELECT c.id_contrato, c.id_empleado, c.id_tipocontrato, c.fecha_inicio, c.fecha_fin,
                       c.hora_entrada, c.hora_salida, c.salario, c.descripcion, c.estatus,
                       e.matricula
                FROM human_resours.contrato c
                INNER JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado
                WHERE e.matricula = @matricula
                ORDER BY c.id_contrato";

                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@matricula", matricula);

                _dbAccess.Connect();
                DataTable result = _dbAccess.ExecuteQuery_Reader(query, paramMatricula);

                foreach (DataRow row in result.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        Id_Contrato = Convert.ToInt32(row["id_contrato"]),
                        Matricula = row["matricula"].ToString() ?? "",
                        Id_TipoContrato = Convert.ToInt32(row["id_tipocontrato"]),
                        FechaInicio = Convert.ToDateTime(row["fecha_inicio"]),
                        FechaFin = Convert.ToDateTime(row["fecha_fin"]),
                        HoraEntrada = TimeSpan.Parse(row["hora_entrada"].ToString()),
                        HoraSalida = TimeSpan.Parse(row["hora_salida"].ToString()),
                        Sueldo = Convert.ToDouble(row["salario"]),
                        Descripcion = row["descripcion"].ToString() ?? "",
                        Estatus = Convert.ToInt32(row["estatus"]) == 1
                    };

                    contratos.Add(contrato);
                }

                _logger.Debug($"Se obtuvieron {contratos.Count} contratos para la matrícula {matricula}");
                return contratos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener los contratos del empleado con matrícula {matricula}");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


        //Método para obtener todos los contratos
        public List<Contrato> ObtenerTodosLosContratos(bool soloActivos = true, int tipoFecha = 0, DateTime? fechaInicio = null, DateTime? fechaFinal = null)
        {
            List<Contrato> contratos = new List<Contrato>();

            try
            {
                string query = @"
            SELECT c.id_contrato, c.id_empleado, c.id_tipocontrato, c.fecha_inicio, c.fecha_fin, 
                   c.hora_entrada, c.hora_salida, c.salario, c.descripcion, c.estatus,
                   t.nombre AS nombre_tipocontrato,
                   CASE 
                       WHEN c.estatus = 1 THEN 'Activo'
                       WHEN c.estatus = 0 THEN 'Inactivo'
                       ELSE 'Desconocido'
                   END AS desc_estatus
            FROM human_resours.contrato c
            INNER JOIN human_resours.tipocontrato t ON c.id_tipocontrato = t.id_tipocontrato
            WHERE 1=1";

                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                if (soloActivos)
                {
                    query += " AND c.estatus = 1";
                }

                if (fechaInicio != null && fechaFinal != null)
                {
                    switch (tipoFecha)
                    {
                        case 1: // Filtrar por fecha de inicio
                            query += " AND c.fecha_inicio BETWEEN @fechaInicio AND @fechaFinal";
                            break;
                        case 2: // Filtrar por fecha de fin
                            query += " AND c.fecha_fin BETWEEN @fechaInicio AND @fechaFinal";
                            break;
                    }
                    parameters.Add(_dbAccess.CreateParameter("@fechaInicio", fechaInicio.Value));
                    parameters.Add(_dbAccess.CreateParameter("@fechaFinal", fechaFinal.Value));
                }

                query += " ORDER BY c.id_contrato";

                _dbAccess.Connect();
                DataTable result = _dbAccess.ExecuteQuery_Reader(query, parameters.ToArray());

                foreach (DataRow row in result.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        Id_Contrato = Convert.ToInt32(row["id_contrato"]),
                        Matricula = row["matricula"].ToString() ?? "",
                        Id_TipoContrato = Convert.ToInt32(row["id_tipocontrato"]),
                        FechaInicio = Convert.ToDateTime(row["fecha_inicio"]),
                        FechaFin = Convert.ToDateTime(row["fecha_fin"]),
                        HoraEntrada = TimeSpan.Parse(row["hora_entrada"].ToString()),
                        HoraSalida = TimeSpan.Parse(row["hora_salida"].ToString()),
                        Sueldo = Convert.ToDouble(row["salario"]),
                        Descripcion = row["descripcion"].ToString() ?? "",
                        Estatus = Convert.ToInt32(row["estatus"]) == 1
                    };

                    contratos.Add(contrato);
                } // no se hace objeto de empleado por que solo necitamos su id

                _logger.Debug($"Se obtuvieron {contratos.Count} registros de contratos");
                return contratos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al intentar obtener la lista de contratos desde la base de datos");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


        //Metodo para verificar si un contrato existe
        public bool ExisteContratoPorId(int idContrato)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM human_resours.contrato WHERE id_contrato = @IdContrato";

                // Crear el parámetro
                NpgsqlParameter paramIdContrato = _dbAccess.CreateParameter("@IdContrato", idContrato);

                // Conectar a la base de datos
                _dbAccess.Connect();

                // Ejecutar consulta
                object? resultado = _dbAccess.ExecuteScalar(query, paramIdContrato);

                int cantidad = Convert.ToInt32(resultado);
                bool existe = cantidad > 0;

                return existe;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia del contrato con ID {idContrato}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


        //Método para obtener un contrato por ID
        public Contrato? ObtenerContratoPorId(int idContrato)
        {
            try
            {
                string query = @"
            SELECT c.id_contrato, c.id_empleado, c.id_tipocontrato, 
                   c.fecha_inicio, c.fecha_fin, c.hora_entrada, c.hora_salida, 
                   c.salario, c.descripcion, c.estatus,
                   t.nombre AS nombre_tipocontrato
            FROM human_resours.contrato c
            INNER JOIN human_resours.tipocontrato t ON c.id_tipocontrato = t.id_tipocontrato
            WHERE c.id_contrato = @IdContrato";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@IdContrato", idContrato);

                _dbAccess.Connect();

                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, paramId);

                if (resultado.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontró ningún contrato con ID {idContrato}");
                    return null;
                }

                DataRow row = resultado.Rows[0];

                Contrato contrato = new Contrato
                {
                    Id_Contrato = Convert.ToInt32(row["id_contrato"]),
                    Matricula = row["matricula"].ToString() ?? "",
                    Id_TipoContrato = Convert.ToInt32(row["id_tipocontrato"]),
                    FechaInicio = Convert.ToDateTime(row["fecha_inicio"]),
                    FechaFin = Convert.ToDateTime(row["fecha_fin"]),
                    HoraEntrada = TimeSpan.Parse(row["hora_entrada"].ToString()),
                    HoraSalida = TimeSpan.Parse(row["hora_salida"].ToString()),
                    Sueldo = Convert.ToDouble(row["salario"]),
                    Descripcion = row["descripcion"].ToString() ?? "",
                    Estatus = Convert.ToInt32(row["estatus"]) == 1
                };

                return contrato;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener el contrato con ID {idContrato}");
                return null;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        //Método para editar un contrato
        public bool ActualizarContrato(Contrato contrato)
        {
            try
            {
                _logger.Debug($"Actualizando contrato con ID {contrato.Id_Contrato} para el empleado con Matricula {contrato.Matricula}");

                string queryContrato = @"
            UPDATE human_resours.contrato
            SET id_empleado = @IdEmpleado,
                id_tipocontrato = @IdTipoContrato,
                fecha_inicio = @FechaInicio,
                fecha_fin = @FechaFin,
                hora_entrada = @HoraEntrada,
                hora_salida = @HoraSalida,
                salario = @Salario,
                descripcion = @Descripcion,
                estatus = @Estatus
            WHERE id_contrato = @IdContrato";

                // Conexión
                _dbAccess.Connect();

                // Crear parámetros
                NpgsqlParameter paramIdContrato = _dbAccess.CreateParameter("@IdContrato", contrato.Id_Contrato);
                NpgsqlParameter paramIdEmpleado = _dbAccess.CreateParameter("@IdEmpleado", contrato.Matricula);
                NpgsqlParameter paramIdTipoContrato = _dbAccess.CreateParameter("@IdTipoContrato", contrato.Id_TipoContrato);
                NpgsqlParameter paramFechaInicio = _dbAccess.CreateParameter("@FechaInicio", contrato.FechaInicio);
                NpgsqlParameter paramFechaFin = _dbAccess.CreateParameter("@FechaFin", contrato.FechaFin);
                NpgsqlParameter paramHoraEntrada = _dbAccess.CreateParameter("@HoraEntrada", contrato.HoraEntrada);
                NpgsqlParameter paramHoraSalida = _dbAccess.CreateParameter("@HoraSalida", contrato.HoraSalida);
                NpgsqlParameter paramSalario = _dbAccess.CreateParameter("@Salario", contrato.Sueldo);
                NpgsqlParameter paramDescripcion = _dbAccess.CreateParameter("@Descripcion", contrato.Descripcion);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", contrato.Estatus ? 1 : 0);

                // Ejecutar la actualización
                int filasAfectadas = _dbAccess.ExecuteNonQuery(queryContrato,
                    paramIdEmpleado, paramIdTipoContrato, paramFechaInicio, paramFechaFin,
                    paramHoraEntrada, paramHoraSalida, paramSalario, paramDescripcion, paramEstatus,
                    paramIdContrato);

                bool exito = filasAfectadas > 0;

                if (!exito)
                {
                    _logger.Warn($"No se pudo actualizar el contrato con ID {contrato.Id_Contrato}. No se encontró el registro.");
                }
                else
                {
                    _logger.Debug($"Contrato con ID {contrato.Id_Contrato} actualizado correctamente.");
                }

                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el contrato con ID {contrato.Id_Contrato}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public List<Contrato> ObtenerContratosFiltrados(string matricula, int tipoContrato, int estatus, int departamento, DateTime fechaInicio, DateTime fechaFin)
        {
            List<Contrato> contratos = new List<Contrato>();

            try
            {
                StringBuilder query = new StringBuilder(@"
            SELECT c.id_contrato, e.matricula, c.id_tipocontrato, c.fecha_inicio, c.fecha_fin,
                   c.hora_entrada, c.hora_salida, c.salario, c.descripcion, c.estatus
            FROM human_resours.contrato c
            INNER JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado
            INNER JOIN human_resours.persona p ON e.id_persona = p.id_persona
            INNER JOIN human_resours.departamento d ON e.id_departamento = d.id_departamento
            WHERE 1=1");

                List<NpgsqlParameter> parametros = new List<NpgsqlParameter>();

                //  Filtro por matrícula exacta
                if (!string.IsNullOrEmpty(matricula))
                {
                    query.Append(" AND e.matricula = @matricula");
                    parametros.Add(_dbAccess.CreateParameter("@matricula", matricula));
                }

                if (tipoContrato != 0)
                {
                    query.Append(" AND c.id_tipocontrato = @tipoContrato");
                    parametros.Add(_dbAccess.CreateParameter("@tipoContrato", tipoContrato));
                }

                if (estatus != 0)
                {
                    query.Append(" AND c.estatus = @estatus");
                    parametros.Add(_dbAccess.CreateParameter("@estatus", estatus == 1 ? 1 : 0));
                }

                if (departamento != 0)
                {
                    query.Append(" AND d.id_departamento = @departamento");
                    parametros.Add(_dbAccess.CreateParameter("@departamento", departamento));
                }

                query.Append(" AND c.fecha_inicio BETWEEN @fechaInicio AND @fechaFin");
                parametros.Add(_dbAccess.CreateParameter("@fechaInicio", fechaInicio));
                parametros.Add(_dbAccess.CreateParameter("@fechaFin", fechaFin));

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query.ToString(), parametros.ToArray());

                foreach (DataRow row in resultado.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        Id_Contrato = Convert.ToInt32(row["id_contrato"]),
                        Matricula = row["matricula"].ToString() ?? "",
                        Id_TipoContrato = Convert.ToInt32(row["id_tipocontrato"]),
                        FechaInicio = Convert.ToDateTime(row["fecha_inicio"]),
                        FechaFin = Convert.ToDateTime(row["fecha_fin"]),
                        HoraEntrada = TimeSpan.Parse(row["hora_entrada"].ToString()),
                        HoraSalida = TimeSpan.Parse(row["hora_salida"].ToString()),
                        Sueldo = Convert.ToDouble(row["salario"]),
                        Descripcion = row["descripcion"].ToString() ?? "",
                        Estatus = Convert.ToInt32(row["estatus"]) == 1
                    };

                    contratos.Add(contrato);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener contratos filtrados");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }

            return contratos;
        }

        public bool TieneContratoActivo(string matricula)
        {
            try
            {
                string query = @"
            SELECT COUNT(*) 
            FROM human_resours.contrato c
            INNER JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado
            WHERE e.matricula = @matricula AND c.estatus = 1";

                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@matricula", matricula);

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, paramMatricula);

                int cantidad = Convert.ToInt32(resultado);
                bool tieneContrato = cantidad > 0;

                _logger.Debug($"Contrato activo para {matricula}: {(tieneContrato ? "Sí" : "No")}");
                return tieneContrato;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar contrato activo para la matrícula {matricula}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }



    }
}
