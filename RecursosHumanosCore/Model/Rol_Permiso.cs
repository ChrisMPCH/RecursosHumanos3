using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Model
{
    public class RolPermiso
    {
        public int Id_Roles_Permisos { get; set; }
        public int Id_Rol { get; set; }
        public int Id_Permiso { get; set; }

        public Rol Rol { get; set; }
        public Permiso Permiso { get; set; }

        public RolPermiso()
        {
            Rol = new Rol();
            Permiso = new Permiso();
        }

        public RolPermiso(Rol rol, Permiso permiso)
        {
            Rol = rol;
            Permiso = permiso;
        }

        public RolPermiso(int id, int idRol, int idPermiso, Rol rol, Permiso permiso)
        {
            Id_Roles_Permisos = id;
            Id_Rol = idRol;
            Id_Permiso = idPermiso;
            Rol = rol;
            Permiso = permiso;
        }
    }
}

