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
using RecursosHumanosCore.Bussines;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Data;
using RecursosHumanosCore.Model;
using RecursosHumanosCore.Models;
using RecursosHumanos.Utilities;
using RecursosHumanosCore.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmAgregarDepartamento : Form
    {
        public static int IdDepartamento { get; set; } //para usarlo en usuarios xd
        public static Departamento? departamentoGenerado;
        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo
        public frmAgregarDepartamento(Guna2GradientPanel pnlCambiante)
        {
            InitializeComponent();
            this.pnlCambiante = pnlCambiante; // Guardamos la referencia del panel
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            InicializarCampos();
        }

        private void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre");
            Formas.ConfigurarTextBox(txtUbicacion, "Ingrese ubicacion");
            Formas.ConfigurarTextBox(txtTelefono, "Ingrese telefono");
            Formas.ConfigurarTextBox(txtCorreo, "Ingrese correo electrónico");
        }

        private bool DatosValidos()
        {
            if (!PersonasNegocio.EsTelefonoValido(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("Teléfono inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsEmailValido(txtCorreo.Text.Trim()))
            {
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool DatosVacios()
        {
            if (txtNombre.Text == "" || txtNombre.Text == "Ingrese nombre" ||
                txtUbicacion.Text == "" || txtUbicacion.Text == "Ingrese ubicacion" ||
                txtTelefono.Text == "" || txtTelefono.Text == "Ingrese telefono" ||
                txtCorreo.Text == "" || txtCorreo.Text == "Ingrese correo electrónico")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GenerarDepartamento())
            {
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool GenerarDepartamento()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DepartamentoController controller = new DepartamentoController();

            Departamento nuevoDepartamento = new Departamento
            {
                NombreDepartamento = txtNombre.Text.Trim(),
                Ubicacion = txtUbicacion.Text.Trim(),
                TelefonoDepartamento = txtTelefono.Text.Trim(),
                EmailDepartamento = txtCorreo.Text.Trim(),
                Estatus = true
            };
            int idUsuario = LoggingManager.UsuarioActual.Id_Usuario;
            var (idDepartamento, mensaje) = controller.RegistrarDepartamento(nuevoDepartamento, idUsuario);

            if (idDepartamento > 0)
            {
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MDIRecursosHumanos.BloquearBotonesMenu();
                InicializarCampos();
                DesbloquearCampos(true);
                Form frmGuardarInf = new frmGuardarInformacion();
                Formas.abrirPanelForm(frmGuardarInf, pnlCambiante);
                return true;
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmListadoDepartamentos formListado = new frmListadoDepartamentos(pnlCambiante);
            Formas.abrirPanelForm(formListado, pnlCambiante);
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

        private void pnlInfoEsmpleado_Paint(object sender, PaintEventArgs e)
        {

        }
        private void DesbloquearCampos(bool desbloquear)
        {
            txtNombre.Enabled = desbloquear;
            txtUbicacion.Enabled = desbloquear;
            txtTelefono.Enabled = desbloquear;
            txtCorreo.Enabled = desbloquear;
        }
    }
}

