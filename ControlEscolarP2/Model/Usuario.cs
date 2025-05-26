using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace RecursosHumanos.Model
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Rol { get; set; }
        public string UsuarioNombre { get; set; }
        public string Contrasenia { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Ultimo_Acceso { get; set; }
        public short Estatus { get; set; }

        public Persona DatosPersonales { get; set; }
        public Rol Rol { get; internal set; }

        public int EstatusPermiso { get; set; }

        public Usuario()
        {
            UsuarioNombre = string.Empty;
            Contrasenia = string.Empty;
            Fecha_Creacion = DateTime.Now;
            Fecha_Ultimo_Acceso = DateTime.Now;
            Estatus = 1;
            DatosPersonales = new Persona();
        }

        public Usuario(string usuario, string contrasenia, Persona persona, int estatusPermiso)
        {
            UsuarioNombre = usuario;
            Contrasenia = contrasenia;
            Fecha_Creacion = DateTime.Now;
            Fecha_Ultimo_Acceso = DateTime.Now;
            Estatus = 1;
            DatosPersonales = persona;
            EstatusPermiso = estatusPermiso;
        }

        public Usuario(int idUsuario, int idPersona, int idRol, string usuario, string contrasenia,
                       DateTime fechaCreacion, DateTime fechaUltimoAcceso, short estatus, Persona persona, int estatusPermiso)
        {
            Id_Usuario = idUsuario;
            Id_Persona = idPersona;
            Id_Rol = idRol;
            UsuarioNombre = usuario;
            Contrasenia = contrasenia;
            Fecha_Creacion = fechaCreacion;
            Fecha_Ultimo_Acceso = fechaUltimoAcceso;
            Estatus = estatus;
            DatosPersonales = persona;
            EstatusPermiso = estatusPermiso;
        }
    }
}
