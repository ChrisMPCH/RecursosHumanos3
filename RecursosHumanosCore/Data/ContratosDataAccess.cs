using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanosCore.Utilities;
using RecursosHumanosCore.Model;
using NLog;
using Npgsql;
using System.Data;
using RecursosHumanosCore.Bussines;

namespace RecursosHumanosCore.Data
{
     public class ContratosDataAccess
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
        public int InsertarContrato(Contrato contrato, int idEmpleado) //FUNCIONA
        {
            try
            {
                string query = @"
            INSERT INTO human_resours.contrato 
                (id_empleado, id_tipocontrato, fecha_inicio, fecha_fin, hora_entrada, hora_salida, salario, descripcion, estatus)
            VALUES 
                (@IdEmpleado, @IdTipoContrato, @FechaInicio, @FechaFin, @HoraEntrada, @HoraSalida, @Salario, @Descripcion, @Estatus)
            RETURNING id_contrato;";

                var parametrosContrato = new[]
                {
            _dbAccess.CreateParameter("@IdEmpleado", idEmpleado),
            _dbAccess.CreateParameter("@IdTipoContrato", contrato.Id_TipoContrato),
            _dbAccess.CreateParameter("@FechaInicio", contrato.FechaInicio),
            _dbAccess.CreateParameter("@FechaFin", contrato.FechaFin),
            _dbAccess.CreateParameter("@HoraEntrada", contrato.HoraEntrada),
            _dbAccess.CreateParameter("@HoraSalida", contrato.HoraSalida),
            _dbAccess.CreateParameter("@Salario", contrato.Sueldo),
            _dbAccess.CreateParameter("@Descripcion", contrato.Descripcion),
            _dbAccess.CreateParameter("@Estatus", contrato.Estatus ? 1 : 0)
        };

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, parametrosContrato);

                if (resultado == null || resultado == DBNull.Value)
                {
                    _logger.Error("El resultado del INSERT fue null. Posiblemente el INSERT falló o el RETURNING no se ejecutó.");
                    return 0;
                }

                int idContrato = Convert.ToInt32(resultado);
                _logger.Info($"Contrato insertado correctamente con ID {idContrato} (id_empleado = {idEmpleado})");
                return idContrato;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al insertar contrato");
                return -5;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        //Método para obtener todos los contratos
       
        public List<Contrato> ObtenerContratosPorMatricula(string matricula)
        {
            List<Contrato> contratos = new List<Contrato>();

            try
            {
                const string query = @"
            SELECT
                c.id_contrato,
                e.matricula,
                c.id_tipocontrato,
                c.fecha_inicio,
                c.fecha_fin,
                c.hora_entrada,
                c.hora_salida,
                c.salario,
                c.descripcion,
                c.estatus,
                tc.nombre AS tipo_contrato
            FROM human_resours.contrato c
            INNER JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado
            INNER JOIN human_resours.tipocontrato tc ON c.id_tipocontrato = tc.id_tipocontrato
            WHERE e.matricula = @matricula
            ORDER BY c.fecha_inicio DESC;
        ";

                var parametros = new[]
                {
            new NpgsqlParameter("@matricula", matricula)
        };

                _dbAccess.Connect();
                DataTable result = _dbAccess.ExecuteQuery_Reader(query, parametros);

                foreach (DataRow row in result.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        Id_Contrato = Convert.ToInt32(row["id_contrato"]),
                        Matricula = row["matricula"]?.ToString() ?? "",
                        Id_TipoContrato = Convert.ToInt32(row["id_tipocontrato"]),
                        FechaInicio = Convert.ToDateTime(row["fecha_inicio"]),
                        FechaFin = Convert.ToDateTime(row["fecha_fin"]),
                        HoraEntrada = TimeSpan.Parse(row["hora_entrada"].ToString()),
                        HoraSalida = TimeSpan.Parse(row["hora_salida"].ToString()),
                        Sueldo = Convert.ToDouble(row["salario"]),
                        Descripcion = row["descripcion"]?.ToString() ?? "",
                        Estatus = Convert.ToInt16(row["estatus"]) == 1,
                        NombreTipoContrato = row["tipo_contrato"]?.ToString() ?? ""
                    };

                    contratos.Add(contrato);
                }

                return contratos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener contratos por matrícula: {matricula}");
                throw;
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
            SELECT 
    c.id_contrato,
    e.matricula,  
    c.id_tipocontrato,
    c.fecha_inicio,
    c.fecha_fin,
    c.hora_entrada,
    c.hora_salida,
    c.salario,
    c.descripcion,
    c.estatus,
    t.nombre AS nombre_tipocontrato
FROM human_resours.contrato c
INNER JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado  
INNER JOIN human_resours.tipocontrato t ON c.id_tipocontrato = t.id_tipocontrato
WHERE c.id_contrato = @IdContrato;";

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
        public bool ActualizarContrato(Contrato contrato, int idEmpleado)
        {
            try
            {
                _logger.Debug($"Actualizando contrato con ID {contrato.Id_Contrato} para el empleado ID {idEmpleado}");

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
        WHERE id_contrato = @IdContrato;";

                _dbAccess.Connect();

                // Crear parámetros
                NpgsqlParameter paramIdContrato = _dbAccess.CreateParameter("@IdContrato", contrato.Id_Contrato);
                NpgsqlParameter paramIdEmpleado = _dbAccess.CreateParameter("@IdEmpleado", idEmpleado);
                NpgsqlParameter paramIdTipoContrato = _dbAccess.CreateParameter("@IdTipoContrato", contrato.Id_TipoContrato);
                NpgsqlParameter paramFechaInicio = _dbAccess.CreateParameter("@FechaInicio", contrato.FechaInicio);
                NpgsqlParameter paramFechaFin = _dbAccess.CreateParameter("@FechaFin", contrato.FechaFin);
                NpgsqlParameter paramHoraEntrada = _dbAccess.CreateParameter("@HoraEntrada", contrato.HoraEntrada);
                NpgsqlParameter paramHoraSalida = _dbAccess.CreateParameter("@HoraSalida", contrato.HoraSalida);
                NpgsqlParameter paramSalario = _dbAccess.CreateParameter("@Salario", contrato.Sueldo);
                NpgsqlParameter paramDescripcion = _dbAccess.CreateParameter("@Descripcion", contrato.Descripcion);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", contrato.Estatus ? 1 : 0);

                // Ejecutar actualización
                int filasAfectadas = _dbAccess.ExecuteNonQuery(queryContrato,
                    paramIdEmpleado, paramIdTipoContrato, paramFechaInicio, paramFechaFin,
                    paramHoraEntrada, paramHoraSalida, paramSalario, paramDescripcion, paramEstatus,
                    paramIdContrato);

                if (filasAfectadas <= 0)
                {
                    _logger.Warn($"No se encontró el contrato con ID {contrato.Id_Contrato} para actualizar.");
                    return false;
                }

                _logger.Info($"Contrato con ID {contrato.Id_Contrato} actualizado correctamente.");
                return true;
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
        public List<Contrato> ObtenerContratosFiltrados(string matricula, int tipoContrato, int estatus, int departamento, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<Contrato> contratos = new List<Contrato>();

            try
            {
                StringBuilder query = new StringBuilder(@"
SELECT 
    c.id_contrato,
    e.matricula,
    CONCAT(p.nombre, ' ', p.ap_paterno, ' ', p.ap_materno) AS nombre_empleado,
    d.nombre_departamento,
    tc.nombre AS nombre_tipo_contrato,
    c.id_tipocontrato,
    c.fecha_inicio,
    c.fecha_fin,
    c.hora_entrada,
    c.hora_salida,
    c.salario,
    c.descripcion,
    c.estatus AS estatus_contrato
FROM human_resours.contrato c
INNER JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado
INNER JOIN human_resours.persona p ON e.id_persona = p.id_persona
INNER JOIN human_resours.departamento d ON e.id_departamento = d.id_departamento
INNER JOIN human_resours.tipocontrato tc ON c.id_tipocontrato = tc.id_tipocontrato
WHERE 1=1");

                List<NpgsqlParameter> parametros = new List<NpgsqlParameter>();

                if (!string.IsNullOrWhiteSpace(matricula) && matricula != "Ingresa la matricula")
                {
                    query.Append(" AND e.matricula = @matricula");
                    parametros.Add(_dbAccess.CreateParameter("@matricula", matricula));
                }

                if (tipoContrato > 0)
                {
                    query.Append(" AND c.id_tipocontrato = @tipoContrato");
                    parametros.Add(_dbAccess.CreateParameter("@tipoContrato", tipoContrato));
                }

                // ✅ Solo se aplica si se seleccionó "Activo" (1) o "Inactivo" (0)
                if (estatus == 0 || estatus == 1)
                {
                    query.Append(" AND c.estatus = @estatus");
                    parametros.Add(_dbAccess.CreateParameter("@estatus", estatus));
                }

                if (departamento > 0)
                {
                    query.Append(" AND d.id_departamento = @departamento");
                    parametros.Add(_dbAccess.CreateParameter("@departamento", departamento));
                }

               


                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query.ToString(), parametros.ToArray());

                foreach (DataRow row in resultado.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        Id_Contrato = Convert.ToInt32(row["id_contrato"]),
                        Matricula = row["matricula"]?.ToString() ?? "",
                        NombreEmpleado = row["nombre_empleado"]?.ToString() ?? "",
                        NombreDepartamento = row["nombre_departamento"]?.ToString() ?? "",
                        NombreTipoContrato = row["nombre_tipo_contrato"]?.ToString() ?? "",
                        Id_TipoContrato = Convert.ToInt32(row["id_tipocontrato"]),
                        FechaInicio = Convert.ToDateTime(row["fecha_inicio"]),
                        FechaFin = Convert.ToDateTime(row["fecha_fin"]),
                        HoraEntrada = TimeSpan.Parse(row["hora_entrada"].ToString()),
                        HoraSalida = TimeSpan.Parse(row["hora_salida"].ToString()),
                        Sueldo = Convert.ToDouble(row["salario"]),
                        Descripcion = row["descripcion"]?.ToString() ?? "",
                        Estatus = Convert.ToInt32(row["estatus_contrato"]) == 1
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
            ValidarEstatusPorFecha(contratos);
            return contratos;

        }

        private void ValidarEstatusPorFecha(List<Contrato> contratos)
        {
            foreach (var contrato in contratos)
            {
                if (contrato.FechaFin < DateTime.Now.Date)
                {
                    contrato.Estatus = false;
                }
            }
        }



        //Obteniene si un contrato esta activo
        public bool TieneContratoActivo(string matricula)
        {
            try
            {
                string query = @"
            SELECT COUNT(*) 
            FROM human_resours.contrato c
            INNER JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado
            WHERE e.matricula = @matricula 
              AND c.estatus = 1 
              AND c.fecha_fin >= @hoy";  

                NpgsqlParameter paramMatricula = _dbAccess.CreateParameter("@matricula", matricula);
                NpgsqlParameter paramHoy = _dbAccess.CreateParameter("@hoy", DateTime.Now.Date);  // Fecha actual

                _dbAccess.Connect();
                object? resultado = _dbAccess.ExecuteScalar(query, paramMatricula, paramHoy);

                int cantidad = Convert.ToInt32(resultado);
                bool tieneContrato = cantidad > 0;

                _logger.Debug($"Contrato activo (fecha y estatus) para {matricula}: {(tieneContrato ? "Sí" : "No")}");
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
        public (int totalContratos, int contratosActivos) ContarContratos()
        {
            try
            {
                // Contar total de contratos
                string queryTotal = @"
                SELECT COUNT(*) 
                FROM human_resours.contrato
                WHERE 1=1";

                // Contar contratos activos
                string queryActivos = @"
                SELECT COUNT(*) 
                FROM human_resours.contrato
                WHERE estatus = 1";

                _dbAccess.Connect();

                // Contar total de contratos
                object? totalResultado = _dbAccess.ExecuteScalar(queryTotal);
                int totalContratos = Convert.ToInt32(totalResultado);

                // Contar contratos activos
                object? activosResultado = _dbAccess.ExecuteScalar(queryActivos);
                int contratosActivos = Convert.ToInt32(activosResultado);

                _logger.Debug($"Se encontraron {totalContratos} contratos en total, de los cuales {contratosActivos} están activos.");
                return (totalContratos, contratosActivos);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al contar los contratos");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public List<Contrato> ObtenerContratosAPI(string matricula, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<Contrato> contratos = new List<Contrato>();

            try
            {
                StringBuilder query = new StringBuilder(@"
                    SELECT  
                        e.matricula,
                        CONCAT(p.nombre, ' ', p.ap_paterno, ' ', p.ap_materno) AS nombre_empleado,
                        p.email,
                        p.telefono,
                        d.nombre_departamento,
                        pu.nombre_puesto,
                        e.estatus AS estatus_empleado,
                        tc.nombre AS tipo_contrato,
                        c.salario,
                        c.fecha_inicio,
                        c.fecha_fin,
                        c.hora_entrada,
                        c.hora_salida
                    FROM human_resours.contrato c
                    JOIN human_resours.empleado e ON c.id_empleado = e.id_empleado
                    JOIN human_resours.persona p ON e.id_persona = p.id_persona
                    JOIN human_resours.departamento d ON e.id_departamento = d.id_departamento
                    JOIN human_resours.puesto pu ON e.id_puesto = pu.id_puesto
                    JOIN human_resours.tipocontrato tc ON c.id_tipocontrato = tc.id_tipocontrato
                    WHERE 1=1");

                List<NpgsqlParameter> parametros = new List<NpgsqlParameter>();

                if (!string.IsNullOrWhiteSpace(matricula) && matricula != "Ingresa la matricula")
                {
                    query.Append(" AND e.matricula = @matricula");
                    parametros.Add(_dbAccess.CreateParameter("@matricula", matricula));
                }

                if (fechaInicio.HasValue)
                {
                    query.Append(" AND c.fecha_inicio >= @fechaInicio");
                    parametros.Add(_dbAccess.CreateParameter("@fechaInicio", fechaInicio.Value));
                }

                if (fechaFin.HasValue)
                {
                    query.Append(" AND c.fecha_fin <= @fechaFin");
                    parametros.Add(_dbAccess.CreateParameter("@fechaFin", fechaFin.Value));
                }

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query.ToString(), parametros.ToArray());

                foreach (DataRow row in resultado.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        Matricula = row["matricula"]?.ToString() ?? "",
                        NombreEmpleado = row["nombre_empleado"]?.ToString() ?? "",
                        Correo = row["email"]?.ToString() ?? "",
                        Telefono = row["telefono"]?.ToString() ?? "",
                        NombreDepartamento = row["nombre_departamento"]?.ToString() ?? "",
                        NombrePuesto = row["nombre_puesto"]?.ToString() ?? "",
                        EstatusEmpleado = Convert.ToInt32(row["estatus_empleado"]) == 1 ? "Activo" : "Inactivo",
                        NombreTipoContrato = row["tipo_contrato"]?.ToString() ?? "",
                        Sueldo = Convert.ToDouble(row["salario"]),
                        FechaInicio = Convert.ToDateTime(row["fecha_inicio"]),
                        FechaFin = row["fecha_fin"] != DBNull.Value ? Convert.ToDateTime(row["fecha_fin"]) : DateTime.MinValue,
                        HoraEntrada = row["hora_entrada"] != DBNull.Value ? TimeSpan.Parse(row["hora_entrada"].ToString()) : TimeSpan.Zero,
                        HoraSalida = row["hora_salida"] != DBNull.Value ? TimeSpan.Parse(row["hora_salida"].ToString()) : TimeSpan.Zero
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




    }
}
