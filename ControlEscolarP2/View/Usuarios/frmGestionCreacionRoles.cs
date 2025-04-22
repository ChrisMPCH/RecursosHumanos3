using RecursosHumanos.Bussines;
using RecursosHumanos.Controller;
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
        private PermisosController _permisosController;

        public frmGestionCreacionRoles()
        {
            InitializeComponent();
            _permisosController = new PermisosController();
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
            Formas.ConfigurarTextBox(txtRolNombre, "Ingrese nombre del Rol");
            Formas.ConfigurarTextBox(txtRolCodigo, "Ingrese codigo del Rol");
            Formas.ConfigurarTextBox(txtDescripcion, "Describa lo que realizara el rol");
        }

        public bool DatosVaciosRoles()
        {
            if (txtRolNombre.Text == "Ingrese nombre del Rol" || txtRolCodigo.Text == "")
            {
                MessageBox.Show("Ingrese nombre del Rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtRolCodigo.Text == "Ingrese codigo del Rol" || txtRolCodigo.Text == "")
            {
                MessageBox.Show("Ingrese codigo del Rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (!RolesNegocio.EsRolNombreValido(txtRolNombre.Text.Trim()))
            {
                MessageBox.Show("Nombre del Rol inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!RolesNegocio.EsRolCodigoValido(txtRolCodigo.Text.Trim()))
            {
                MessageBox.Show("Codigo del Rol inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            CargarPermisosEnTabla();
        }

        private void CargarPermisosEnTabla()
        {
            try
            {
                var listaPermisos = _permisosController.ObtenerPermisos();
                dataGridPermisos.DataSource = listaPermisos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los permisos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
