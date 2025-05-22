using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Models
{
    public class Ausencia
    {
        public int IdAusencias { get; set; }
        public DateTime FechaAusencias { get; set; }
        public string MotivoAusencia { get; set; }
        public int IdEmpleado { get; set; }
        public short Estatus { get; set; }
    }
}

