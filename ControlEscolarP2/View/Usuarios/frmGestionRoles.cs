using RecursosHumanos.Bussines;
using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursosHumanos.View
{
    public partial class frmGestionRoles : Form
    {
        public frmGestionRoles()
        {
            InitializeComponent();
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            InicializarCampos();
            IniciarTabla();
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridRolesU);
            Formas.ConfigurarEstiloDataGridView(dataGridPermisos);
            ConfigurarAnchoColumnasRoles(300);
            FormLoadRoles();
        }

        private void ConfigurarAnchoColumnasRoles(int ancho)
        {
            foreach (DataGridViewColumn columna in dataGridRolesU.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private void ConfigurarAnchoColumnasPermisos(int ancho)
        {
            foreach (DataGridViewColumn columna in dataGridPermisos.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private bool GenerarRol()
        {
            if (!DatosVaciosRoles())
            {
                return false;
            }
            if (!DatosCorrectosRoles())
            {
                return false;
            }
            return true;
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtRol, "Ingrese codigo del Rol");
            Formas.ConfigurarTextBox(txtDescripcion, "Describa lo que realizara el rol");
        }

        public bool DatosVaciosRoles()
        {
            if (txtRol.Text == "Ingrese nombre del Rol" || txtRol.Text == "")
            {
                MessageBox.Show("Ingrese el nombre del Rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtDescripcion.Text == "Describa lo que realizara el rol" || txtDescripcion.Text == "")
            {
                MessageBox.Show("Describa lo que realizara el rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool DatosCorrectosRoles()
        {
            if (!RolesNegocio.EsRolCodigoValido(txtRol.Text.Trim()))
            {
                MessageBox.Show("Rol inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!RolesNegocio.EsDescripcionValida(txtDescripcion.Text.Trim()))
            {
                MessageBox.Show("Descripción inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GenerarRol())
            {
                MessageBox.Show("Rol generado correctamente", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarCampos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtRol.Text == "Ingrese codigo del Rol" || txtRol.Text == "")
            {
                MessageBox.Show("Ingrese el rol del usuario.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Habilitar todos los elementos para edición/eliminación
            FormLoadPermiso();
            ConfigurarAnchoColumnasPermisos(300);

            txtDescripcion.Enabled = true;
            btnGuardarEdicion.Enabled = true;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rol eliminado correctamente", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RestablecerTodo();
        }

        private void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rol editado correctamente", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RestablecerTodo();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            RestablecerTodo();
        }

        private void RestablecerTodo()
        {
            InicializarCampos();
            IniciarTabla();
            txtDescripcion.Enabled = false;
            txtRol.ForeColor = Color.FromArgb(125, 137, 149);
            btnGuardarEdicion.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            VaciarDatosPermisos();
        }

        //--------------------------------------------------------------------------------Llenado tabla
        private void FormLoadPermiso()
        {
            // Lista de usuarios con datos simulados
            List<Permiso> listaPermisos = new List<Permiso>
            {
                new Permiso { Codigo = "1ddd", Descripcion = "Altas", Estatus = "Activo", Check = true },
                new Permiso { Codigo = "2ddd", Descripcion = "Bajas", Estatus = "Activo", Check = true },
                new Permiso { Codigo = "3ddd", Descripcion = "Consultas", Estatus = "Activo", Check = true },
                new Permiso { Codigo = "4ddd", Descripcion = "Actualizaciones", Estatus = "Activo", Check = true }
            };

            // Asignar la lista al DataGridView
            dataGridPermisos.DataSource = listaPermisos;
        }

        private void FormLoadRoles()
        {
            // Lista de roles con datos simulados  
            List<Roles> listaRoles = new List<Roles>
           {
               new Roles { Codigo = "1rrr", Descripcion = "Administrador", Estatus = "Activo" },
               new Roles { Codigo = "2rrr", Descripcion = "Usuario", Estatus = "Activo" },
               new Roles { Codigo = "3rrr", Descripcion = "Invitado", Estatus = "Inactivo" },
               new Roles { Codigo = "4rrr", Descripcion = "Supervisor", Estatus = "Activo" }
           };

            // Asignar la lista al DataGridView  
            dataGridRolesU.DataSource = listaRoles;
        }
        private void VaciarDatosPermisos()
        {
            dataGridPermisos.DataSource = null;
        }

        public class Permiso
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public string Estatus { get; set; }
            public bool Check { get; set; }
        }

        public class Roles
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public string Estatus { get; set; }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
