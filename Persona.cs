using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecursosHumanos.Model
{
    public class Persona
    {
        private string v;

        //propiedades de la persona
        
        /// <summary>
        /// ID de la Persona
        /// </summary>
        public int IdPersona { get; set; }

        /// <summary>
        /// Nombre de la persona
        /// </summary>
        public string Nombre { get; set; }
       
        /// <summary>
        /// Apellido paternp
        /// </summary>
        public string ApellidoPaterno { get; set; }
        
        /// <summary>
        /// Apellido materno
        /// </summary>
        public string ApellidoMaterno { get; set; }

        /// <summary>
        /// Correo electrónico de la persona
        /// </summary>
        public string Correo { get; set; }
        
        /// <summary>
        /// Número de teléfono de la persona
        /// </summary>
        public string Telefono { get; set; }
        
        /// <summary>
        /// CURP (Clave Única de Registro de Población) de la persona
        /// </summary>
        public string Curp { get; set; }

        /// <summary>
        /// Fecha de nacimiento de la persona
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }//no es obligatoria puede ser nulo

        /// <summary>
        /// Indica si la persona está activa en el sistema
        /// </summary>
        public bool Estatus { get; set; }
        
        /// <summary>
        /// RFC de la persona
        /// </summary>
        public string RFC { get;set; }
        
        /// <summary>
        /// Genero de la persona 'M':Mujer 'H':Hombre 
        /// </summary>
        public string Genero { get;set; }

        /// <summary>
        /// Direccion de la perosona
        /// </summary>
        public string Direccion { get;set; }

        //para recibir objeto con datos vacios, cuando se quiere depositar datos en el objeto
        public Persona()
        {
            Nombre = string.Empty;
            ApellidoPaterno = string.Empty;
            ApellidoMaterno = string.Empty;
            Correo = string.Empty;
            Telefono = string.Empty;
            Curp = string.Empty;
            Estatus = true;
            RFC= string.Empty;
            Genero= string.Empty;
            Direccion= string.Empty;
        }
        
        
    }
}
