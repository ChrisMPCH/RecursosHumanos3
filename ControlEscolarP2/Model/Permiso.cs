using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
    public class Permiso
    {
        public int Id_Permiso { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public short Estatus { get; set; }

        public Permiso()
        {
            Codigo = string.Empty;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Estatus = 1;
        }

        public Permiso(string codigo, string nombre, string descripcion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Estatus = 1;
        }

        public Permiso(int id, string codigo, string nombre, string descripcion, short estatus)
        {
            Id_Permiso = id;
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Estatus = estatus;
        }
    }
}

