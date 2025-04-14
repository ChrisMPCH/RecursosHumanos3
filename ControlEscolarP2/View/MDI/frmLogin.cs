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
    public partial class frmLogin : Form
    {
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

        private bool GenerarUsuario()
        {
            if (!DatosVaciosLogin())
            {
                return false;
            }
            if (!DatosCorrectosLogin())
            {
                return false;
            }
            return true;
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
            if (GenerarUsuario())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
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
    }
}
