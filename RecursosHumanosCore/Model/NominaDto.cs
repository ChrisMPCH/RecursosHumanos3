using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanosCore.Model
{
    public class NominaDto
    {
        public int idNomina { get; set; }
        public int idEmpleado { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string estadoPago { get; set; }
        public string nombreEmpleado { get; set; }
        public string departamentoEmpleado { get; set; }
        public string rfcEmpleado { get; set; }
        public decimal sueldoBase { get; set; }
        public decimal montoTotal { get; set; }
        public string montoLetras { get; set; }
    }
}
