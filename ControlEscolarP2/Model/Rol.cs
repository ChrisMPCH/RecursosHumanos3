using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
    public class Rol
    {
        public int Id_Rol { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public short Estatus { get; set; }

        public Rol()
        {
            Codigo = string.Empty;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Estatus = 1;
        }

        public Rol(string codigo, string nombre, string descripcion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Estatus = 1;
        }

        public Rol(int id, string codigo, string nombre, string descripcion, short estatus)
        {
            Id_Rol = id;
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Estatus = estatus;
        }
    }
}

