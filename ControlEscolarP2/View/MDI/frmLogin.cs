using Guna.UI2.AnimatorNS;
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
    public partial class frmLogin : Form
    {
        public static List<int> permisosUsuario = new List<int>();
        public static LoginController controller = new LoginController();
        public static Usuario? usuarioLogueado = null;

        public frmLogin()
        {
            InitializeComponent();
            InicializarCampos();
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtUsuario, "Ingrese su usuario");
            Formas.ConfigurarTextBox(txtContrasenia, "Ingrese su contraseña");
            txtContrasenia.UseSystemPasswordChar = true; // Oculta la contraseña por defecto
        }

        private bool VerificarUsuario()
        {
            if (!DatosVaciosLogin())
            {
                return false;
            }
            if (!DatosCorrectosLogin())
            {
                return false;
            }

            string usuario = txtUsuario.Text.Trim();
            string contrasenia = txtContrasenia.Text.Trim();

            usuarioLogueado = controller.Login(usuario, contrasenia);

            if (usuarioLogueado != null)
            {
                MessageBox.Show($"Bienvenido, {usuarioLogueado.DatosPersonales.Nombre}", "Login exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Obtener los permisos del usuario
                List<int> idPermisosUsuario = controller.ObtenerPermisosUsuario(usuarioLogueado.Id_Usuario);

                // Pasar los permisos para ternerlos en la interfaz principal
                permisosUsuario = idPermisosUsuario;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return true;
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private bool DatosVaciosLogin()
        {
            if (txtUsuario.Text == "Ingrese su usuario" || txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese el nombre del usuario", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtContrasenia.Text == "Ingrese su contraseña" || txtContrasenia.Text == "")
            {
                MessageBox.Show("Ingrese la contraseña del usuario", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool DatosCorrectosLogin()
        {
            if (!UsuarioNegocio.EsNombreUsuarioValido(txtUsuario.Text.Trim()))
            {
                MessageBox.Show("Nombre de usuario inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!UsuarioNegocio.EsContraseñaValido(txtContrasenia.Text.Trim()))
            {
                MessageBox.Show("Contraseña inválida, sugerimos una contraseña con 8 o mas caracteres.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (VerificarUsuario())
            {
                // Actualizar fecha de acceso del usuario logueado
                if (usuarioLogueado != null)
                {
                    usuarioLogueado.Fecha_Ultimo_Acceso = DateTime.Now;
                    controller.ActualizarFechaUltimoAcceso(usuarioLogueado);
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pcVerContraseña_Click(object sender, EventArgs e)
        { 
            txtContrasenia.UseSystemPasswordChar = !txtContrasenia.UseSystemPasswordChar;
        }

        //-------------------------------------------------------------------------------Permisos
        
    }
}
