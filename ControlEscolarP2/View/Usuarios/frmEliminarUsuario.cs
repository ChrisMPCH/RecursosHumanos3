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
    public partial class frmEliminarUsuario : Form
    {
        private int? idUsuarioEncontrado = null;

        public frmEliminarUsuario()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            Formas.ConfigurarTextBox(txtUsuario, "Ingrese nombre de usuario");
            DesbloquearCampos(false);
        }

        public void PoblarCombos()
        {
            PoblaComboEstatus();
            PoblaComboGenero();
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
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            UsuariosController controller = new UsuariosController();
            var listaUsuarios = controller.ObtenerUsuarios();
            var usuario = listaUsuarios.FirstOrDefault(u => u.UsuarioNombre.Equals(txtUsuario.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                MessageBox.Show("Usuario no encontrado.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            PoblarCombos();

            // Guardar el ID
            idUsuarioEncontrado = usuario.Id_Usuario;

            // Llenar campos con info de la persona
            txtNombre.Text = usuario.DatosPersonales.Nombre;
            txtApellidoP.Text = usuario.DatosPersonales.Ap_Paterno;
            txtApellidoM.Text = usuario.DatosPersonales.Ap_Materno;
            txtRFC.Text = usuario.DatosPersonales.RFC;
            txtCURP.Text = usuario.DatosPersonales.CURP;
            txtDireccion.Text = usuario.DatosPersonales.Direccion;
            txtTelefono.Text = usuario.DatosPersonales.Telefono;
            txtCorreo.Text = usuario.DatosPersonales.Email;
            dtaNacimiento.Value = usuario.DatosPersonales.Fecha_Nacimiento;
            cbRoles.SelectedValue = usuario.Rol;
            cbxGenero.SelectedIndex = usuario.DatosPersonales.Genero.ToUpper().StartsWith("H") ? 1 : 0;
            cbxEstatus.SelectedIndex = usuario.Estatus == 1 ? 0 : 1;

            DesbloquearCampos(false);
            return true;
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
                    { 1, "Hombre" },
                    { 2, "Mujer" }
                };

            //Asignar los valores al comboBox
            cbxGenero.DataSource = new BindingSource(list_puestos, null);
            cbxGenero.DisplayMember = "Value"; //lo que se mestra
            cbxGenero.ValueMember = "Key"; //lo que se guarda como SelectedValue
            cbxGenero.SelectedIndex = 1;
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
            dtpFechaBaja.Enabled = accion;
        }

        private void btnBuscarU_Click(object sender, EventArgs e)
        {
            BuscarUsuario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Formas.ConfigurarTextBox(txtUsuario, "Ingrese nombre de usuario");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioEncontrado == null)
            {
                MessageBox.Show("Debes buscar primero un usuario válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UsuariosController controller = new UsuariosController();
            var resultado = controller.EliminarUsuario(idUsuarioEncontrado.Value);

            if (resultado.exito)
            {
                MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Formas.ConfigurarTextBox(txtUsuario, "Ingrese nombre de usuario");
                idUsuarioEncontrado = null;
            }
            else
            {
                MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
