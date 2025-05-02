using RecursosHumanos.Bussines;
using RecursosHumanos.Controller;
using RecursosHumanos.Model;
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
            InicializarCampos();
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtUsuario, "Ingrese nombre de usuario");
            VaciarCampos();
            DesbloquearCampos(false);
        }

        public void PoblarCombos()
        {
            PoblaComboEstatus();
            PoblaComboGenero();
            PoblaComboRol();
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

            // Llenar campos con info del usuario
            txtNombre.Text = usuario.DatosPersonales.Nombre;
            txtApellidoP.Text = usuario.DatosPersonales.Ap_Paterno;
            txtApellidoM.Text = usuario.DatosPersonales.Ap_Materno;
            txtRFC.Text = usuario.DatosPersonales.RFC;
            txtCURP.Text = usuario.DatosPersonales.CURP;
            txtDireccion.Text = usuario.DatosPersonales.Direccion;
            txtTelefono.Text = usuario.DatosPersonales.Telefono;
            txtCorreo.Text = usuario.DatosPersonales.Email;

            dtaNacimiento.Value = usuario.DatosPersonales.Fecha_Nacimiento > dtaNacimiento.MinDate
                ? usuario.DatosPersonales.Fecha_Nacimiento
                : DateTime.Today;

            // Fecha creación
            if (usuario.Fecha_Creacion >= dtpFechaCreacion.MinDate &&
                usuario.Fecha_Creacion <= dtpFechaCreacion.MaxDate)
            {
                dtpFechaCreacion.Value = usuario.Fecha_Creacion;
            }
            else
            {
                dtpFechaCreacion.Value = DateTime.Today;
            }

            // Fecha último acceso
            if (usuario.Fecha_Ultimo_Acceso >= dtpFechaUltimoAcc.MinDate &&
                usuario.Fecha_Ultimo_Acceso <= dtpFechaUltimoAcc.MaxDate)
            {
                dtpFechaUltimoAcc.Value = usuario.Fecha_Ultimo_Acceso;
            }
            else
            {
                dtpFechaUltimoAcc.Value = DateTime.Today;
            }


            cbRoles.SelectedValue = usuario.Id_Rol;

            var genero = usuario.DatosPersonales.Genero?.ToLower();
            if (genero == "mujer")
                cbxGenero.SelectedValue = 2;
            else if (genero == "hombre")
                cbxGenero.SelectedValue = 1;
            else
                cbxGenero.SelectedValue = 0; // Por si acaso

            cbxEstatus.SelectedValue = usuario.Estatus;

            DesbloquearCampos(false);
            return true;
        }

        private void PoblaComboEstatus()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
                {
                    { 1, "Activo" },
                    { 0, "Inactivo" }
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

        private void PoblaComboRol()
        {
            try
            {
                RolesController controller = new RolesController();
                var roles = controller.ObtenerRolesActivos();

                // Agrega opción por defecto
                roles.Insert(0, new Rol { Id_Rol = 0, Nombre = "Seleccione" });

                cbRoles.DataSource = roles;
                cbRoles.DisplayMember = "Nombre";
                cbRoles.ValueMember = "Id_Rol";
                cbRoles.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            dtpFechaUltimoAcc.Enabled = accion;
            dtpFechaCreacion.Enabled = accion;
            dtpFechaUltimoAcc.Enabled = accion;

        }

        private void btnBuscarU_Click(object sender, EventArgs e)
        {
            BuscarUsuario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            DesbloquearCampos(false);
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

        private void VaciarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellidoP.Text = string.Empty;
            txtApellidoM.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtCURP.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCorreo.Text = string.Empty;

            dtaNacimiento.Value = DateTime.Today;
            dtpFechaUltimoAcc.Value = DateTime.Today;

            cbRoles.SelectedValue = 0;
            idUsuarioEncontrado = null;
        }
    }
}
