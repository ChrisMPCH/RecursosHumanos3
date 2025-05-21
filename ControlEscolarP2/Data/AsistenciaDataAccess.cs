using System.Data;
using NLog;
using Npgsql;
using RecursosHumanos.Data;
using RecursosHumanos.Models;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.DataAccess
{
    public class AsistenciaDataAccess
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Data.AsistenciaDataAccess");

        private readonly PostgreSQLDataAccess _dbAccess;

        public AsistenciaDataAccess()
        {
            try
            {
                _dbAccess = PostgreSQLDataAccess.GetInstance();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar el acceso a datos de AsistenciaDataAccess");
                throw;
            }
        }

        public bool InsertarAsistenciaPorMatricula(string matricula, DateTime fecha, TimeSpan horaActual)
        {
            try
            {
                _dbAccess.Connect();

                string queryEmpleado = @"SELECT id_empleado FROM human_resours.empleado WHERE matricula = @matricula";
                var parametrosEmpleado = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@matricula", matricula)
                };

                object resultadoEmpleado = _dbAccess.ExecuteScalar(queryEmpleado, parametrosEmpleado);

                if (resultadoEmpleado == null)
                {
                    _logger.Warn($"No se encontró empleado con matrícula {matricula}");
                    return false;
                }

                int idEmpleado = Convert.ToInt32(resultadoEmpleado);

                if (ExisteRegistroDelDiaInterno(_dbAccess, idEmpleado, fecha))
                {
                    _logger.Warn($"El empleado {idEmpleado} ya tiene asistencia para el día {fecha.Date}");
                    return false;
                }

                string queryInsertar = @"INSERT INTO human_resours.asistencia 
                                         (fecha_asistencia, hora_entrada, id_empleado, estatus) 
                                         VALUES (@fecha, @hora, @empleado, @estatus)";

                short estatus = 1; // 1 = Asistencia

                var parametrosInsertar = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@fecha", fecha.Date),
                    _dbAccess.CreateParameter("@hora", horaActual),
                    _dbAccess.CreateParameter("@empleado", idEmpleado),
                    _dbAccess.CreateParameter("@estatus", estatus)
                };

                int filas = _dbAccess.ExecuteNonQuery(queryInsertar, parametrosInsertar);
                _logger.Info($"Registro de entrada creado para empleado {idEmpleado}. Filas afectadas: {filas}");

                return filas > 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public bool ExisteRegistroDelDia(int idEmpleado, DateTime fecha)
        {
            try
            {
                _dbAccess.Connect();
                return ExisteRegistroDelDiaInterno(_dbAccess, idEmpleado, fecha);
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        private static bool ExisteRegistroDelDiaInterno(PostgreSQLDataAccess db, int idEmpleado, DateTime fecha)
        {
            string query = @"SELECT COUNT(*) FROM human_resours.asistencia 
                             WHERE id_empleado = @empleado AND fecha_asistencia = @fecha";

            var parametros = new NpgsqlParameter[]
            {
                db.CreateParameter("@empleado", idEmpleado),
                db.CreateParameter("@fecha", fecha.Date)
            };

            object result = db.ExecuteScalar(query, parametros);
            return Convert.ToInt32(result) > 0;
        }

        public bool ExisteRegistroDelDia(string matricula, DateTime fecha)
        {
            try
            {
                _dbAccess.Connect();

                string queryEmpleado = @"SELECT id_empleado FROM human_resours.empleado WHERE matricula = @matricula";
                var parametrosEmpleado = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@matricula", matricula)
                };

                object resultadoEmpleado = _dbAccess.ExecuteScalar(queryEmpleado, parametrosEmpleado);

                if (resultadoEmpleado == null)
                {
                    _logger.Warn($"No se encontró empleado con matrícula {matricula}");
                    return false;
                }

                int idEmpleado = Convert.ToInt32(resultadoEmpleado);

                return ExisteRegistroDelDiaInterno(_dbAccess, idEmpleado, fecha);
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public TimeSpan CalcularHorasTrabajadas(string matricula, DateTime fecha)
        {
            string query = @"SELECT a.hora_entrada AS hora_entrada_asistencia, 
                            c.hora_entrada AS hora_entrada_contrato, 
                            c.hora_salida AS hora_salida_contrato
                     FROM human_resours.asistencia a
                     JOIN human_resours.empleado e ON a.id_empleado = e.id_empleado
                     JOIN human_resours.contrato c ON e.id_empleado = c.id_empleado
                     WHERE e.matricula = @matricula AND a.fecha_asistencia = @fecha";

            var parametros = new NpgsqlParameter[]
            {
        _dbAccess.CreateParameter("@matricula", matricula),
        _dbAccess.CreateParameter("@fecha", fecha.Date)
            };

            DataTable tabla = _dbAccess.ExecuteQuery_Reader(query, parametros);

            if (tabla.Rows.Count == 0)
            {
                throw new InvalidOperationException("No se encontró entrada para el empleado en la fecha indicada.");
            }

            var row = tabla.Rows[0];
            TimeSpan horaEntradaAsistencia = (TimeSpan)row["hora_entrada_asistencia"];
            TimeSpan horaSalidaActual = DateTime.Now.TimeOfDay;

            // Calcular horas trabajadas reales
            TimeSpan horasTrabajadas = horaSalidaActual - horaEntradaAsistencia;

            // Calcular horas contratadas
            TimeSpan horaEntradaContrato = (TimeSpan)row["hora_entrada_contrato"];
            TimeSpan horaSalidaContrato = (TimeSpan)row["hora_salida_contrato"];
            TimeSpan horasContratadas = horaSalidaContrato - horaEntradaContrato;

            // Puedes validar si cumplió o no aquí
            if (horasTrabajadas >= horasContratadas)
            {
                _logger.Info($"Empleado {matricula} cumplió las horas contratadas ({horasContratadas.TotalHours} hrs).");
            }
            else
            {
                _logger.Warn($"Empleado {matricula} NO cumplió las horas contratadas. Trabajó {horasTrabajadas.TotalHours} hrs de {horasContratadas.TotalHours} hrs requeridas.");
            }

            return horasTrabajadas;
        }

        public bool RegistrarSalidaPorMatricula(string matricula, DateTime fecha, TimeSpan horaSalidaActual, string observaciones = "")
        {
            try
            {
                _dbAccess.Connect();

                // Paso 1: Obtener id_empleado
                string queryEmpleado = @"SELECT id_empleado FROM human_resours.empleado WHERE matricula = @matricula";
                var parametrosEmpleado = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@matricula", matricula)
                };

                object resultadoEmpleado = _dbAccess.ExecuteScalar(queryEmpleado, parametrosEmpleado);

                if (resultadoEmpleado == null)
                {
                    _logger.Warn($"No se encontró empleado con matrícula {matricula}");
                    return false;
                }

                int idEmpleado = Convert.ToInt32(resultadoEmpleado);

                // Paso 2: Buscar el registro de asistencia
                string query = @"SELECT a.id_asistencia, a.hora_entrada, 
                                c.hora_entrada AS hora_entrada_contrato, 
                                c.hora_salida AS hora_salida_contrato
                         FROM human_resours.asistencia a
                         JOIN human_resours.contrato c ON a.id_empleado = c.id_empleado
                         WHERE a.id_empleado = @idEmpleado AND a.fecha_asistencia = @fecha";

                var parametros = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@idEmpleado", idEmpleado),
            _dbAccess.CreateParameter("@fecha", fecha.Date)
                };

                DataTable tabla = _dbAccess.ExecuteQuery_Reader(query, parametros);

                if (tabla.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontró asistencia de entrada para {matricula} el {fecha.Date}");
                    return false;
                }

                var row = tabla.Rows[0];
                int idAsistencia = Convert.ToInt32(row["id_asistencia"]);

                // Paso 3: Calcular si cumplió horario completo
                TimeSpan horaEntradaAsistencia = (TimeSpan)row["hora_entrada"];
                TimeSpan horaEntradaContrato = (TimeSpan)row["hora_entrada_contrato"];
                TimeSpan horaSalidaContrato = (TimeSpan)row["hora_salida_contrato"];

                TimeSpan horasTrabajadas = horaSalidaActual - horaEntradaAsistencia;
                TimeSpan horasContratadas = horaSalidaContrato - horaEntradaContrato;

                bool horarioCompleto = horasTrabajadas >= horasContratadas;

                // Paso 4: Actualizar incluyendo horario_completo
                string queryUpdate = @"UPDATE human_resours.asistencia 
                               SET hora_salida = @salida, 
                                   observaciones = @obs,
                                   horario_completo = @horarioCompleto
                               WHERE id_asistencia = @id";

                var parametrosUpdate = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@salida", horaSalidaActual),
            _dbAccess.CreateParameter("@obs", observaciones ?? ""),
            _dbAccess.CreateParameter("@horarioCompleto", horarioCompleto),
            _dbAccess.CreateParameter("@id", idAsistencia)
                };

                int filas = _dbAccess.ExecuteNonQuery(queryUpdate, parametrosUpdate);

                return filas > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar la salida.");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public int ContarDiasTrabajados(int idEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                _dbAccess.Connect();

                string query = @"
            SELECT COUNT(DISTINCT fecha_asistencia) 
            FROM human_resours.asistencia 
            WHERE id_empleado = @idEmpleado 
            AND fecha_asistencia BETWEEN @fechaInicio AND @fechaFin 
            AND estatus = 1
            AND horario_completo = TRUE";

                var parametros = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@idEmpleado", idEmpleado),
            _dbAccess.CreateParameter("@fechaInicio", fechaInicio.Date),
            _dbAccess.CreateParameter("@fechaFin", fechaFin.Date)
                };

                object? resultado = _dbAccess.ExecuteScalar(query, parametros);

                if (resultado == null || resultado == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al contar los días trabajados");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public (int asistenciasHoy, int empleadosActivos) ContarAsistenciasHoy()
        {
            try
            {
                // Contar asistencias de hoy (solo registros activos)  
                string queryAsistencias = @"  
    SELECT COUNT(*)   
    FROM human_resours.asistencia  
    WHERE Fecha_Asistencia = CURRENT_DATE  
    AND Estatus = true";

                // Contar empleados activos  
                string queryEmpleados = @"  
    SELECT COUNT(*)   
    FROM human_resours.empleado  
    WHERE estatus = true";

                _dbAccess.Connect();

                // Contar asistencias de hoy  
                object? asistenciasResultado = _dbAccess.ExecuteScalar(queryAsistencias);
                int asistenciasHoy = Convert.ToInt32(asistenciasResultado);

                // Contar empleados activos  
                object? empleadosResultado = _dbAccess.ExecuteScalar(queryEmpleados);
                int empleadosActivos = Convert.ToInt32(empleadosResultado);

                _logger.Debug($"Se encontraron {asistenciasHoy} asistencias hoy y {empleadosActivos} empleados activos.");
                return (asistenciasHoy, empleadosActivos);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al contar las asistencias de hoy");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public List<Asistencia> ObtenerAsistencias()
        {
            List<Asistencia> lista = new List<Asistencia>();

            string query = @"
        SELECT a.id_asistencia, a.fecha_asistencia, a.hora_entrada, a.hora_salida,
               e.matricula,
               p.nombre || ' ' || p.ap_paterno || ' ' || p.ap_materno AS nombre_empleado
        FROM human_resours.asistencia a
        INNER JOIN human_resours.empleado e ON a.id_empleado = e.id_empleado
        INNER JOIN human_resours.persona p ON e.id_persona = p.id_persona
        WHERE a.estatus = 1
        ORDER BY a.fecha_asistencia DESC;";

            try
            {
                _dbAccess.Connect();

                DataTable tabla = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in tabla.Rows)
                {
                    lista.Add(new Asistencia
                    {
                        IdAsistencia = Convert.ToInt32(row["id_asistencia"]),
                        FechaAsistencia = Convert.ToDateTime(row["fecha_asistencia"]),
                        HoraEntrada = (TimeSpan)row["hora_entrada"],
                        HoraSalida = row["hora_salida"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)row["hora_salida"],
                        Matricula = row["matricula"].ToString(),
                        NombreEmpleado = row["nombre_empleado"].ToString()
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener asistencias con datos del empleado");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

    }
}
