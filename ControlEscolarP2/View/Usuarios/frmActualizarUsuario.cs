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
    public partial class frmActualizarUsuario : Form
    {
        

        private int? idUsuario = null;
        private int? idPersona = null;

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
            PoblaComboRol();
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
            Formas.ConfigurarTextBox(txtContrasenia, "Ingrese nueva contraseña");
            Formas.ConfigurarTextBox(txtContraseniaCon, "Confirme nueva contraseña");
            txtContrasenia.UseSystemPasswordChar = true;
            txtContraseniaCon.UseSystemPasswordChar = false;

            dtpFechaCreacion.Enabled = false;
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

            UsuariosController controller = new UsuariosController();

            Persona persona = new Persona
            {
                Id_Persona = idPersona.Value,
                Nombre = txtNombre.Text.Trim(),
                Ap_Paterno = txtApellidoP.Text.Trim(),
                Ap_Materno = txtApellidoM.Text.Trim(),
                RFC = txtRFC.Text.Trim(),
                CURP = txtCURP.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtCorreo.Text.Trim(),
                Fecha_Nacimiento = dtaNacimiento.Value,
                Genero = cbxGenero.Text,
                Estatus = Convert.ToInt16(cbxEstatus.SelectedValue) // Fix: Convert SelectedValue to short
            };

            Usuario usuario = new Usuario
            {
                Id_Usuario = idUsuario.Value,
                Id_Persona = idPersona.Value,
                UsuarioNombre = txtUsuario.Text.Trim(),
                Contrasenia = txtContrasenia.Text.Trim(), // ← Ya permite actualizar
                Id_Rol = (int)cbRoles.SelectedValue,      // ← Ya actualiza el rol
                Fecha_Ultimo_Acceso = DateTime.Now,
                Estatus = (short)(cbxEstatus.SelectedIndex == 0 ? 1 : 0),
                DatosPersonales = persona
            };

            var resultado = controller.ActualizarUsuario(usuario);

            if (resultado)
            {
                MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarCampos();
                DesbloquearCampos(false);
                idUsuario = null;
                idPersona = null;
                return true;
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool DatosValidos()
        {
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

            if (!UsuarioNegocio.EsContraseñaValido(txtContrasenia.Text.Trim()))
            {
                MessageBox.Show("Contraseña inválida. Debe tener al menos 8 caracteres.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!UsuarioNegocio.EsConfirmarContraseñaValido(txtContrasenia.Text.Trim(), txtContraseniaCon.Text.Trim()))
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbRoles.SelectedValue == null || (int)cbRoles.SelectedValue == 0)
            {
                MessageBox.Show("Seleccione un rol para el usuario", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!UsuarioNegocio.EsNombreUsuarioValido(txtUsuario.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                if (!UsuarioNegocio.EsContraseñaValido(txtContrasenia.Text.Trim()))
                {
                    MessageBox.Show("Contraseña inválida. Debe tener al menos 8 caracteres.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!UsuarioNegocio.EsConfirmarContraseñaValido(txtContrasenia.Text.Trim(), txtContraseniaCon.Text.Trim()))
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
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

            PoblarCombos();

            UsuariosController controller = new UsuariosController();
            var usuario = controller.ObtenerUsuarios()
                                    .FirstOrDefault(u => u.UsuarioNombre.Equals(txtUsuario.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                MessageBox.Show("Usuario no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Guardar IDs
            idUsuario = usuario.Id_Usuario;
            idPersona = usuario.Id_Persona;

            // Mostrar datos en los campos
            txtNombre.Text = usuario.DatosPersonales.Nombre;
            txtApellidoP.Text = usuario.DatosPersonales.Ap_Paterno;
            txtApellidoM.Text = usuario.DatosPersonales.Ap_Materno;
            txtRFC.Text = usuario.DatosPersonales.RFC;
            txtCURP.Text = usuario.DatosPersonales.CURP;
            txtDireccion.Text = usuario.DatosPersonales.Direccion;
            txtTelefono.Text = usuario.DatosPersonales.Telefono;
            txtCorreo.Text = usuario.DatosPersonales.Email;

            if (usuario.Fecha_Creacion != DateTime.MinValue)
            {
                dtpFechaCreacion.Value = usuario.Fecha_Creacion;
            }
            else
            {
                dtpFechaCreacion.Value = DateTime.Today;
            }

            dtaNacimiento.Value = usuario.DatosPersonales.Fecha_Nacimiento > dtaNacimiento.MinDate
                ? usuario.DatosPersonales.Fecha_Nacimiento
                : DateTime.Today;

            cbRoles.SelectedValue = usuario.Id_Rol;

            var genero = usuario.DatosPersonales.Genero?.ToLower();
            if (genero == "mujer")
                cbxGenero.SelectedValue = 2;
            else if (genero == "hombre")
                cbxGenero.SelectedValue = 1;
            else
                cbxGenero.SelectedValue = 0; // Por si acaso

            cbxEstatus.SelectedValue = usuario.Estatus;

            txtContrasenia.Text = usuario.Contrasenia;

            DesbloquearCampos(true);
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
            cbRoles.Enabled = accion;
            txtContrasenia.Enabled = accion;
            txtContraseniaCon.Enabled = accion;
        }

        private void btnBuscarU_Click(object sender, EventArgs e)
        {
            BuscarUsuario();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            GuardarUsuario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            DesbloquearCampos(false);
        }

        private void pcVerContraseña_Click(object sender, EventArgs e)
        {
            txtContrasenia.UseSystemPasswordChar = !txtContrasenia.UseSystemPasswordChar;
        }
    }
}
