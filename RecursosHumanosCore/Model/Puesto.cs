using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanosCore.Models
{
    public class Puesto
    {
        public int IdPuesto { get; set; }
        public string NombrePuesto { get; set; }
        public string DescripcionPuesto { get; set; }
        public bool Estatus { get; set; }

        public Puesto()
        {
            NombrePuesto = "";
            DescripcionPuesto = "";
        }

        public Puesto(int idPuesto, string nombrePuesto, string descripcionPuesto, bool estatus)
        {
            IdPuesto = idPuesto;
            NombrePuesto = nombrePuesto;
            DescripcionPuesto = descripcionPuesto;
            Estatus = estatus;
        }
    }
}
