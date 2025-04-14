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
    public partial class frmActualizarUsuario : Form
    {
        public frmActualizarUsuario()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            PoblarCombos();
            InicializarCampos();
        }
        public void PoblarCombos()
        {
            PoblaComboEstatus();
            PoblaComboGenero();
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtUsuario, "Ingrese nombre de usuario");
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre(s)");
            Formas.ConfigurarTextBox(txtApellidoP, "Ingrese apellido paterno");
            Formas.ConfigurarTextBox(txtApellidoM, "Ingrese apellido materno");
            Formas.ConfigurarTextBox(txtRFC, "Ingrese RFC");
            Formas.ConfigurarTextBox(txtCURP, "Ingrese CURP");
            Formas.ConfigurarTextBox(txtCorreo, "Ingrese correo electrónico");
            Formas.ConfigurarTextBox(txtDireccion, "Ingrese direccion");
            Formas.ConfigurarTextBox(txtTelefono, "Ingrese teléfono");
            dtpFechaIngreso.Enabled = false;
        }

        private void PoblaComboEstatus()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                { 1, "Activo" },
                { 2, "Inactivo" }
            };

            //Asignar los valores al comboBox
            cbxEstatus.DataSource = new BindingSource(list_estatus, null);
            cbxEstatus.DisplayMember = "Value"; //lo que se mestra
            cbxEstatus.ValueMember = "Key"; //lo que se guarda como SelectedValue

            cbxEstatus.SelectedIndex = 1;
        }

        private void PoblaComboGenero()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_puestos = new Dictionary<int, string>
            {
                { 1, "Femenino" },
                { 2, "Masculino" }
            };

            //Asignar los valores al comboBox
            cbxGenero.DataSource = new BindingSource(list_puestos, null);
            cbxGenero.DisplayMember = "Value"; //lo que se mestra
            cbxGenero.ValueMember = "Key"; //lo que se guarda como SelectedValue
            cbxGenero.SelectedIndex = 1;
        }
        private bool GuardarUsuario()
        {
            if (DatosVacios())
            {
                return false;
            }
            if (!DatosValidos())
            {
                return false;
            }
            return true;
        }

        private bool DatosValidos()
        {
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtUsuario.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsEmailValido(txtCorreo.Text.Trim()))
            {
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsTelefonoValido(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("Teléfono inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsCURPValido(txtCURP.Text.Trim()))
            {
                MessageBox.Show("CURP inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsRFCValido(txtRFC.Text.Trim())) // Se corrigió el error aquí
            {
                MessageBox.Show("RFC inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsGeneroValido((int)cbxGenero.SelectedValue))
            {
                MessageBox.Show("Género inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool DatosVacios()
        {
            if (txtNombre.Text == "Ingrese nombre(s)" || txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre de la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtDireccion.Text == "Ingrese direccion" || txtDireccion.Text == "")
            {
                MessageBox.Show("Ingrese la dirección del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtCorreo.Text == "Ingrese correo electrónico" || txtCorreo.Text == "")
            {
                MessageBox.Show("Ingrese el correo electrónico del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtTelefono.Text == "Ingrese teléfono" || txtTelefono.Text == "")
            {
                MessageBox.Show("Ingrese el teléfono del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtCURP.Text == "Ingrese CURP" || txtCURP.Text == "")
            {
                MessageBox.Show("Ingrese el CURP del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtRFC.Text == "Ingrese RFC" || txtRFC.Text == "")
            {
                MessageBox.Show("Ingrese el RFC del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtApellidoM.Text == "Ingrese apellido materno" || txtApellidoM.Text == "")
            {
                MessageBox.Show("Ingrese el apellido materno del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtApellidoP.Text == "Ingrese apellido paterno" || txtApellidoP.Text == "")
            {
                MessageBox.Show("Ingrese el apellido paterno del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (cbxGenero.SelectedValue == null || (int)cbxGenero.SelectedValue == 0)
            {
                MessageBox.Show("Seleccione el género del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }

        private bool DatosVaciosUsuario()
        {
            if (txtUsuario.Text == "Ingrese nombre de usuario" || txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese el nombre de Usuario", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool DatosCorrectosUsuario()
        {
            if (!UsuarioNegocio.EsNombreUsuarioValido(txtUsuario.Text.Trim()))
            {
                MessageBox.Show("Nombre inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool BuscarUsuario()
        {
            if (!DatosVaciosUsuario())
            {
                return false;
            }
            if (!DatosCorrectosUsuario())
            {
                return false;
            }
            return true;
        }

        private void DesbloquearCampos(bool accion)
        {
            txtNombre.Enabled = accion;
            txtDireccion.Enabled = accion;
            txtCorreo.Enabled = accion;
            txtTelefono.Enabled = accion;
            txtCURP.Enabled = accion;
            txtRFC.Enabled = accion;
            txtApellidoM.Enabled = accion;
            txtApellidoP.Enabled = accion;
            cbxGenero.Enabled = accion;
            cbxEstatus.Enabled = accion;
            dtaNacimiento.Enabled = accion;
        }

        private void btnBuscarU_Click(object sender, EventArgs e)
        {
            if (BuscarUsuario())
            {
                MessageBox.Show("Usuario encontrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PoblarCombos();
                DesbloquearCampos(true);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (GuardarUsuario())
            {
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            DesbloquearCampos(false);

        }

    }
}
