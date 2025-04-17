using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
     class Contrato
    {
        public int  Id_Contrato { get; set; }
        public int Id_Empleado { get; set; }

        public int Id_TipoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraEntrada { get; set; } //timeSpan solo guarda la hora
        public TimeSpan HoraSalida { get; set; }
        public double Sueldo { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; } 
        
        public Contrato(int id_Contrato, int id_Empleado, int id_TipoContrato, 
            DateTime fechaInicio, DateTime fechaFin, TimeSpan horaEntrada, 
            TimeSpan horaSalida, double sueldo, string descripcion, bool estatus)
        {
            Id_Contrato = id_Contrato;
            Id_Empleado = id_Empleado;
            Id_TipoContrato = id_TipoContrato;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
            Sueldo = sueldo;
            Descripcion = descripcion;
            Estatus = estatus;
        }

        public Contrato()
        {
            Id_Contrato = 0;
            Id_Empleado = 0;
            Id_TipoContrato = 0;
            FechaInicio = DateTime.Now;
            FechaFin = DateTime.Now;
            HoraEntrada = new TimeSpan(0, 0, 0);
            HoraSalida = new TimeSpan(0, 0, 0);
            Sueldo = 0.0;
            Descripcion = string.Empty;
            Estatus = true;
        }


    }
}
