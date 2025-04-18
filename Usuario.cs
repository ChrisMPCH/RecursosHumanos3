using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
    internal class Usuario
    {
        /// <summary>
        /// ID generado para el usuario
        /// </summary>
        public int IdUsuario {  get; set; }
        /// <summary>
        /// Id de la persona a que le van a hacer el ususario
        /// </summary>
        public int IdPersona { get; set; }
        /// <summary>
        /// Id del rol a asignar
        /// </summary>
        public int IdRol {  get; set; }
        /// <summary>
        /// Nombre que le pondran al nuevo usuario 
        /// </summary>
        public string NombreUsuario { get; set; }
        /// <summary>
        /// Contraseña de Acceso
        /// </summary>
        public string Contraseña { get; set;} 
        /// <summary>
        /// Fecha de cracion del Usuario
        /// </summary>
        public DateTime? CreacionUsuario {  get; set; }
        /// <summary>
        /// Ultima fecha de inicio de secion 
        /// </summary>
        public DateTime? Fechainiciodesesion { get;set; }
        /// <summary>
        /// Estatus del Usuario "True":Activo "False":Inactivo
        /// </summary>
        public bool EstatusUsuario { get; set;}
        /// <summary>
        /// Todos los datos de la persona a que se le va a hacer el usuario
        /// </summary>
        public Persona DatosPersona { get; set; }
        /// <summary>
        /// Constructor del usuario vacio
        /// </summary>
        public Usuario ()
        {
            NombreUsuario = string.Empty;
            Contraseña = string.Empty;
            CreacionUsuario = DateTime.Now;
            Fechainiciodesesion = DateTime.Now;
            EstatusUsuario = true;
            DatosPersona = new Persona ();
        }



    }
}
