using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace RecursosHumanos.Model
{
    public class Auditoria
    {

        public int Id_Auditoria { get; set; }
        public short Id_Tipo { get; set; }
        public short Movimiento { get; internal set; }
        public short Id_Accion { get; set; }
        public DateTime Fecha_Movimiento { get; set; }
        public string Ip_Equipo { get; set; }
        public string Nombre_Equipo { get; set; }
        public string Detalle { get; set; }
        public int Id_Usuario { get; set; }
        public short Estatus { get; set; }

        public Usuario UsuarioResponsable { get; set; }

        public Auditoria()
        {
            Ip_Equipo = string.Empty;
            Nombre_Equipo = string.Empty;
            Detalle = string.Empty;
            Fecha_Movimiento = DateTime.Now;
            Estatus = 1;
            UsuarioResponsable = new Usuario();
        }

        public Auditoria(string ipEquipo, string nombreEquipo, string detalle, Usuario usuario)
        {
            Ip_Equipo = ipEquipo;
            Nombre_Equipo = nombreEquipo;
            Detalle = detalle;
            Fecha_Movimiento = DateTime.Now;
            Estatus = 1;
            UsuarioResponsable = usuario;
        }

        public Auditoria(int idAuditoria, int movimiento, short idTipo, short idAccion,
                         DateTime fechaMovimiento, string ipEquipo, string nombreEquipo,
                         string detalle, int idUsuario, short estatus, Usuario usuario)
        {
            Id_Auditoria = idAuditoria;
            Id_Tipo = idTipo;
            Id_Accion = idAccion;
            Movimiento = (short)movimiento;
            Fecha_Movimiento = fechaMovimiento;
            Ip_Equipo = ipEquipo;
            Nombre_Equipo = nombreEquipo;
            Detalle = detalle;
            Id_Usuario = idUsuario;
            Estatus = estatus;
            UsuarioResponsable = usuario;
        }

    }
}


