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
    public partial class frmGestionCreacionRoles : Form
    {
        public frmGestionCreacionRoles()
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
            Formas.ConfigurarEstiloDataGridView(dataGridPermisos);
            ConfigurarAnchoColumnas(300);
            FormLoad();
        }

        private void ConfigurarAnchoColumnas(int ancho)
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
            if (!RolesNegocio.EsRolValido(txtRol.Text.Trim()))
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GenerarRol())
            {
                MessageBox.Show("Rol generado correctamente", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarCampos();
            }
        }


        //--------------------------------------------------------------------------------Llenado tabla
        private void FormLoad()
        {
            // Lista de usuarios con datos simulados
            List<Permiso> listaPermisos = new List<Permiso>
            {
                new Permiso { Codigo = "1ddd", Descripcion = "Altas", Estatus = "Activo", Check = false },
                new Permiso { Codigo = "2ddd", Descripcion = "Bajas", Estatus = "Activo", Check = false },
                new Permiso { Codigo = "3ddd", Descripcion = "Consultas", Estatus = "Activo", Check = false },
                new Permiso { Codigo = "4ddd", Descripcion = "Actualizaciones", Estatus = "Activo", Check = false }
            };

            // Asignar la lista al DataGridView
            dataGridPermisos.DataSource = listaPermisos;
        }

        public class Permiso
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public string Estatus { get; set; }
            public bool Check { get; set; }
        }

    }
}
