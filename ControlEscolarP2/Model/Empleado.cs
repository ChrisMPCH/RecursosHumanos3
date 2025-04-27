using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
    public class Empleado
    {
        public int Id_Empleado { get; set; }
        public int Id_Persona { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public DateTime? Fecha_Baja { get; set; }
        public int Id_Departamento { get; set; }
        public int Id_Puesto { get; set; }
        public string Matricula { get; set; }
        public string Motivo { get; set; }
        public short Estatus { get; set; }

        public Persona DatosPersonales { get; set; }

        public Empleado()
        {
            Matricula = string.Empty;
            Motivo = string.Empty;
            Fecha_Ingreso = DateTime.Now;
            Estatus = 1;
            DatosPersonales = new Persona();
        }

        public Empleado(string matricula, string motivo, Persona datosPersonales)
        {
            Matricula = matricula;
            Motivo = motivo;
            Fecha_Ingreso = DateTime.Now;
            Estatus = 1;
            DatosPersonales = datosPersonales;
        }

        public Empleado(int idEmpleado, int idPersona, DateTime fechaIngreso, DateTime? fechaBaja,
                        int idDepartamento, int idPuesto, string matricula, string motivo, short estatus,
                        Persona datosPersonales)
        {
            Id_Empleado = idEmpleado;
            Id_Persona = idPersona;
            Fecha_Ingreso = fechaIngreso;
            Fecha_Baja = fechaBaja;
            Id_Departamento = idDepartamento;
            Id_Puesto = idPuesto;
            Matricula = matricula;
            Motivo = motivo;
            Estatus = estatus;
            DatosPersonales = datosPersonales;
        }
    }
}

