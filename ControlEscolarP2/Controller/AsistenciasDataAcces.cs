using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using RecursosHumanos.DataAccess;

public class AsistenciaController
{
    private static readonly Logger _logger = LogManager.GetLogger("RecursosHumanos.Controller.AsistenciaController");
    private readonly AsistenciaDataAccess _asistenciaDataAccess;

    public AsistenciaController()
    {
        try
        {
            _asistenciaDataAccess = new AsistenciaDataAccess();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error al crear la conexión a la base de datos");
            throw;
        }
    }

    public double ObtenerPorcentajeAsistenciasHoy()
    {
        try
        {
            var (asistenciasHoy, empleadosActivos) = _asistenciaDataAccess.ContarAsistenciasHoy();

            if (empleadosActivos == 0)
            {
                _logger.Warn("No hay empleados activos para calcular el porcentaje de asistencias.");
                return 0.0;
            }

            double porcentaje = (double)asistenciasHoy / empleadosActivos * 100;
            _logger.Info($"Porcentaje de asistencias hoy: {porcentaje}%");
            return porcentaje;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error al obtener el porcentaje de asistencias de hoy");
            throw;
        }
    }
}
