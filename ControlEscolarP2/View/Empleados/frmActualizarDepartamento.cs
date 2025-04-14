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
using RecursosHumanos.Bussines;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmActualizarDepartamento : Form
    {
        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo
        public frmActualizarDepartamento(Guna2GradientPanel pnlCambiante)
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
            Formas.ConfigurarTextBox(txtDeprtamento, "Ingrese el departamento que desea actualizar");
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
            if (txtDeprtamento.Text == "" || txtDeprtamento.Text == "Ingrese el departamento que desea actualizar" ||
            txtNombre.Text == "" || txtNombre.Text == "Ingrese nombre" ||
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (GuardarDepartamento())
            {
                MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool GuardarDepartamento()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!DatosValidos())
            {
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmListadoDepartamentos formListado = new frmListadoDepartamentos(pnlCambiante);
            Formas.abrirPanelForm(formListado, pnlCambiante);
        }

        private void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            ofdArchivo.Title = "Seleccionar archivo de Excel";
            ofdArchivo.Filter = "Archivos de excel (.xlsx;.xls)|.xlsx;.xls";
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            {
                string nombreDepartamento = txtDeprtamento.Text.Trim(); // Elimina espacios extras

                // Validación 1: Verificar si el usuario ingresó texto
                if (string.IsNullOrWhiteSpace(nombreDepartamento))
                {
                    MessageBox.Show("Ingrese un nombre de departamento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación 2: Evitar números y caracteres especiales
                if (!nombreDepartamento.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    MessageBox.Show("El nombre del departamento solo debe contener letras y espacios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Departamento validado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}