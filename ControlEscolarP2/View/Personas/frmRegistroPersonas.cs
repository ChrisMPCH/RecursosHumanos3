using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using RecursosHumanos.Bussines;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmRegistroPersonas : Form
    {

        public frmRegistroPersonas()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            IniciarPaneles();
            PoblaComboGenero();
            InicializarCampos();
        }

        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre");
            Formas.ConfigurarTextBox(txtDireccion, "Ingrese dirección completa");
            Formas.ConfigurarTextBox(txtCorreo, "Ingrese dirección de correo electronico");
            Formas.ConfigurarTextBox(txtTelefono, "Ej. 7291589593");
            Formas.ConfigurarTextBox(txtCURP, "Ingrese CURP");
            Formas.ConfigurarTextBox(txtRFC, "Ingrese RFC");
            Formas.ConfigurarTextBox(txtMaterno, "Ingrese apellido materno");
            Formas.ConfigurarTextBox(txtPaterno, "Ingrese apellido paterno");
        }

        private void IniciarPaneles()
        {
            Form frmGuardarInf = new frmGuardarInformacion();
            Formas.abrirPanelForm(frmGuardarInf, pnlCambiante);
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            Form frmRegistroUsuario = new frmRegistroUsuario();
            Formas.abrirPanelForm(frmRegistroUsuario, pnlCambiante);
        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            if (GenerarPersona())
            {
                MessageBox.Show("Datos de persona capturados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DesbloquearCampos(false);
            }
        }

        public static void DesbloquearCampos(bool accion)
        {
            txtNombre.Enabled = accion;
            txtDireccion.Enabled = accion;
            txtCorreo.Enabled = accion;
            txtTelefono.Enabled = accion;
            txtCURP.Enabled = accion;
            txtRFC.Enabled = accion;
            txtMaterno.Enabled = accion;
            txtPaterno.Enabled = accion;
            cbGenero.Enabled = accion;
        }

        public static bool GenerarPersona()
        {
            if (!DatosVaciosPersona())
            {
                return false;
            }
            if (!DatosCorrectosPersona())
            {
                return false;
            }
            return true;
        }

        public static bool DatosVaciosPersona()
        {
            if (txtNombre.Text == "Ingrese nombre" || txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre de la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPaterno.Text == "Ingrese apellido paterno" || txtPaterno.Text == "")
            {
                MessageBox.Show("Ingrese el apellido paterno de la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtMaterno.Text == "Ingrese apellido materno" || txtMaterno.Text == "")
            {
                MessageBox.Show("Ingrese el apellido materno la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtRFC.Text == "Ingrese RFC" || txtRFC.Text == "")
            {
                MessageBox.Show("Ingrese el RFC la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtCURP.Text == "Ingrese CURP" || txtCURP.Text == "")
            {
                MessageBox.Show("Ingrese el CURP la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtTelefono.Text == "Ej. 7291589593" || txtTelefono.Text == "")
            {
                MessageBox.Show("Ingrese el teléfono la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbGenero.SelectedValue == null || (int)cbGenero.SelectedValue == 0)
            {
                MessageBox.Show("Seleccione el género la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtDireccion.Text == "Ingrese dirección completa" || txtDireccion.Text == "")
            {
                MessageBox.Show("Ingrese la dirección la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtCorreo.Text == "Ingrese dirección de correo electronico" || txtCorreo.Text == "")
            {
                MessageBox.Show("Ingrese el correo electrónico del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool DatosCorrectosPersona()
        {
            if (PersonasNegocio.EsRFCValido(txtRFC.Text.Trim()))
            {
                MessageBox.Show("RFC inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsCURPValido(txtCURP.Text.Trim()))
            {
                MessageBox.Show("CURP inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsTelefonoValido(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("Teléfono inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsGeneroValido((int)cbGenero.SelectedValue))
            {
                MessageBox.Show("Género inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsEmailValido(txtCorreo.Text.Trim()))
            {
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void PoblaComboGenero()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_tipoCon = new Dictionary<int, string>
            {
                { 0,"Seleccione"},
                { 1, "Hombre" },
                { 2, "Mujer" }
            };
            // Asignar el diccionario al ComboBox
            cbGenero.DataSource = new BindingSource(list_tipoCon, null);
            cbGenero.DisplayMember = "Value";  // Lo que se muestra
            cbGenero.ValueMember = "Key";      // Lo que se guarda como SelectedValue
            cbGenero.SelectedValue = 0;
        }

        private void btnPersonaCancelar_Click(object sender, EventArgs e)
        {
            DesbloquearCampos(true);
        }
    }
}
