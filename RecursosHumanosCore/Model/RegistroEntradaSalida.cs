using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanosCore.Model
{
    class RegistroEntradaSalida
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public string Observaciones { get; set; }
        public bool EsDiaTrabajado { get; set; } // true si entró en horario permitido
    }
}
