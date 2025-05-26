using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
    public class Contrato
    {
        public int Id_Contrato { get; set; }
        public string Matricula { get; set; }
        public int Id_TipoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public double Sueldo { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }



        // Propiedades adicionales solo para reportes
        public string NombreTipoContrato { get; set; }
        public string NombreEmpleado { get; set; }           // Solo reportes
        public string NombreDepartamento { get; set; }        // Solo reportes

        public Contrato(string id_Empleado, int id_TipoContrato,
            DateTime fechaInicio, DateTime fechaFin, TimeSpan horaEntrada,
            TimeSpan horaSalida, double sueldo, string descripcion, bool estatus)
        {
            Matricula = id_Empleado;
            Id_TipoContrato = id_TipoContrato;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
            Sueldo = sueldo;
            Descripcion = descripcion;
            Estatus = estatus;

            // Inicialización segura
            NombreTipoContrato = "";
            NombreEmpleado = "";
            NombreDepartamento = "";
        }

        public Contrato()
        {
            Id_Contrato = 0;
            Matricula = "";
            Id_TipoContrato = 0;
            FechaInicio = DateTime.Now;
            FechaFin = DateTime.Now;
            HoraEntrada = new TimeSpan(0, 0, 0);
            HoraSalida = new TimeSpan(0, 0, 0);
            Sueldo = 0.0;
            Descripcion = string.Empty;
            Estatus = true;
            NombreTipoContrato = "";
            NombreEmpleado = "";
            NombreDepartamento = "";
        }

        public int NumeroHoras
        {
            get
            {
                return (int)(HoraSalida - HoraEntrada).TotalHours;
            }
        }
    }


}
