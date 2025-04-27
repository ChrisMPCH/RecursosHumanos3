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
    public partial class frmRegistroUsuario : Form
    {
        public frmRegistroUsuario()
        {
            InitializeComponent();
            InicializarCampos();
        }
        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre de usuario");
            Formas.ConfigurarTextBox(txtContrasenia, "Ingresar contraseña");
            Formas.ConfigurarTextBox(txtContraseniaCon, "Ingresar contraseña");
            txtContrasenia.UseSystemPasswordChar = true; // Oculta la contraseña por defecto

            PoblaComboRol();
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

        public bool DatosVaciosUsuario()
        {
            if (txtNombre.Text == "Ingrese nombre de usuario" || txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre del usuario", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtContrasenia.Text == "Ingresar contraseña" || txtContrasenia.Text == "")
            {
                MessageBox.Show("Ingrese la contraseña del usuario", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtContraseniaCon.Text == "Ingresar contraseña" || txtContraseniaCon.Text == "")
            {
                MessageBox.Show("Confirme su contraseña", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbRoles.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un rol para el usuario", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool DatosCorrectosUsuario()
        {
            if (!UsuarioNegocio.EsNombreUsuarioValido(txtNombre.Text.Trim()))
            {
                MessageBox.Show("Nombre de usuario inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!UsuarioNegocio.EsContraseñaValido(txtContrasenia.Text.Trim()))
            {
                MessageBox.Show("Contraseña inválida, sugerimos una contraseña con 8 o mas caracteres.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!UsuarioNegocio.EsConfirmarContraseñaValido(txtContrasenia.Text.Trim(), txtContraseniaCon.Text.Trim()))
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool GenerarUsuario()
        {
            var idPersona = frmRegistroPersonas.IdPersonaRegistrada!;

            if (idPersona <= 0)
            {
                MessageBox.Show("Faltan los datos de la persona.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!DatosVaciosUsuario())
            {
                return false;
            }
            if (!DatosCorrectosUsuario())
            {
                return false;
            }

            UsuariosController controller = new UsuariosController();

            Usuario nuevoUsuario = new Usuario
            {
                UsuarioNombre = txtNombre.Text.Trim(),
                Contrasenia = txtContrasenia.Text.Trim(),
                Id_Persona = idPersona,
                Id_Rol = (int)cbRoles.SelectedValue,
                Fecha_Creacion = DateTime.Now,
                Fecha_Ultimo_Acceso = DateTime.Now,
                Estatus = 1
            };

            var (exito, mensaje) = controller.RegistrarUsuario(nuevoUsuario);
            if (exito)
            {
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar y volver a panel anterior
                frmRegistroPersonas.InicializarCampos();
                frmRegistroPersonas.DesbloquearCampos(true);

                InicializarCampos();

                MDIRecursosHumanos.DesbloquearBotonesMenu();

                Form frmGuardarInf = new frmGuardarInformacion();
                Formas.abrirPanelForm(frmGuardarInf, frmRegistroPersonas.pnlCambiante);
                return true;
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            PersonasController personasController = new PersonasController();
            var exito = personasController.CancelarRegistroPersona(frmRegistroPersonas.IdPersonaRegistrada);
            if (!exito)
            {
                MessageBox.Show("No se canceló el registro, no se pudo eliminar la persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmRegistroPersonas.IdPersonaRegistrada = 0;
            frmRegistroPersonas.DesbloquearCampos(true);
            MessageBox.Show("Se canceló el registro y se eliminó la persona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            InicializarCampos();
            MDIRecursosHumanos.DesbloquearBotonesMenu();

            Form frmGuardarInf = new frmGuardarInformacion();
            Formas.abrirPanelForm(frmGuardarInf, frmRegistroPersonas.pnlCambiante);
        }

        private void btnUsuario_Click_1(object sender, EventArgs e)
        {
            GenerarUsuario();
        }   

        private void pcVerContraseña_Click(object sender, EventArgs e)
        {
            txtContrasenia.UseSystemPasswordChar = !txtContrasenia.UseSystemPasswordChar;
        }

        private void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            ofdArchivo.Title = "Seleccionar archivo de Excel";
            ofdArchivo.Filter = "Archivos de Excel (*.xlsx;*.xls)|*.xlsx;*.xls";
            ofdArchivo.InitialDirectory = "C:\\";
            ofdArchivo.FilterIndex = 1;
            ofdArchivo.RestoreDirectory = true;

            if (ofdArchivo.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofdArchivo.FileName;
                string extension = Path.GetExtension(filePath).ToLower();

                if (extension == ".xlsx" || extension == ".xls")
                {
                    MessageBox.Show("Archivo válido: " + filePath, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un archivo de Excel válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
