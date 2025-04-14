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
            // Crear un diccionario con los valores
            Dictionary<int, string> list_roles = new Dictionary<int, string>
            {
                {0, "Seleccione" },
                { 1, "ADMIN" },
                { 2, "RH_MANAGER" },
                { 3, "RH_ANALYST" },
                { 4, "SUPERVISOR" }
            };

            // Asignar el diccionario al ComboBox
            cbRoles.DataSource = new BindingSource(list_roles, null);
            cbRoles.DisplayMember = "Value";  // Lo que se muestra
            cbRoles.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbRoles.SelectedValue = 0;
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

        private bool GenerarUsuario()
        {
            if (!frmRegistroPersonas.GenerarPersona())
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
            return true;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Form frmGuardarInf = new frmGuardarInformacion();
            Formas.abrirPanelForm(frmGuardarInf, frmRegistroPersonas.pnlCambiante);
        }

        private void btnUsuario_Click_1(object sender, EventArgs e)
        {
            if (GenerarUsuario())
            {
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmRegistroPersonas.InicializarCampos();
                frmRegistroPersonas.DesbloquearCampos(true);
                Form frmGuardarInf = new frmGuardarInformacion();
                Formas.abrirPanelForm(frmGuardarInf, frmRegistroPersonas.pnlCambiante);
            }
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
