using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
    public class Persona
    {
        public int Id_Persona { get; set; }
        public string Nombre { get; set; }
        public string Ap_Paterno { get; set; }
        public string Ap_Materno { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Genero { get; set; }
        public short Estatus { get; set; }

        public Persona()
        {
            Nombre = string.Empty;
            Ap_Paterno = string.Empty;
            Ap_Materno = string.Empty;
            RFC = string.Empty;
            CURP = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            Email = string.Empty;
            Genero = string.Empty;
            Estatus = 1; //activo
        }
        public Persona(string nombre, string apPaterno, string apMaterno, string rfc, string curp, string direccion, string telefono, string email, string genero, short estatus)
        {
            Nombre = nombre;
            Ap_Paterno = apPaterno;
            Ap_Materno = apMaterno;
            RFC = rfc;
            CURP = curp;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Genero = genero;
            Estatus = estatus;
        }

        public Persona(string nombre, string apPaterno, string apMaterno, string rfc, string curp, string direccion, string telefono, string email, DateTime fechaNacimiento, string genero, short estatus)
        {
            Nombre = nombre;
            Ap_Paterno = apPaterno;
            Ap_Materno = apMaterno;
            RFC = rfc;
            CURP = curp;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Fecha_Nacimiento = fechaNacimiento;
            Genero = genero;
            Estatus = estatus;
        }

    }
}

