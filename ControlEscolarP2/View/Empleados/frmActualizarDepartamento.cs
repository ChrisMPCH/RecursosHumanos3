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
using RecursosHumanos.Controller;
using RecursosHumanos.Models;
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
            PoblarComboBoxEstatus();

        }

        private void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtIdDepartamento, "Ingrese el departamento que desea actualizar");
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
            if (txtIdDepartamento.Text == "" || txtIdDepartamento.Text == "Ingrese el departamento que desea actualizar" ||
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
                return false;
            }

            if (!DatosValidos())
            {
                return false;
            }

            DepartamentoController controller = new DepartamentoController();

            Departamento departamento = new Departamento
            {
                IdDepartamento = int.Parse(txtIdDepartamento.Text.Trim()),
                NombreDepartamento = txtNombre.Text.Trim(),
                Ubicacion = txtUbicacion.Text.Trim(),
                TelefonoDepartamento = txtTelefono.Text.Trim(),
                EmailDepartamento = txtCorreo.Text.Trim(),
                Estatus= (bool) cbxEstatus.SelectedValue
            };

            var (exito, mensaje) = controller.ActualizarDepartamento(departamento);

            if (exito)
            {
                MessageBox.Show("Departamento actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarCampos();
                DesbloquearCampos(false);
                return true;
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!BuscarDepartamento())
            {
                return;
            }
        }


        private bool BuscarDepartamento()
        {
            if (!DatosVaciosDepartamento())
            {
                return false;
            }

            if (!DatosCorrectosDepartamento())
            {
                return false;
            }

            DepartamentoController controller = new DepartamentoController();
            var departamento = controller.ObtenerDetalleDepartamento(int.Parse(txtIdDepartamento.Text.Trim()));

            if (departamento == null)
            {
                MessageBox.Show("Departamento no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Mostrar datos en los campos
            txtNombre.Text = departamento.NombreDepartamento;
            txtUbicacion.Text = departamento.Ubicacion;
            txtTelefono.Text = departamento.TelefonoDepartamento;
            txtCorreo.Text = departamento.EmailDepartamento;
            cbxEstatus.SelectedValue = departamento.Estatus;

            DesbloquearCampos(true);

            return true;
        }

        private bool DatosVaciosDepartamento()
        {
            if (string.IsNullOrWhiteSpace(txtIdDepartamento.Text))
            {
                MessageBox.Show("Ingrese un ID de departamento para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool DatosCorrectosDepartamento()
        {
            if (!int.TryParse(txtIdDepartamento.Text.Trim(), out int id) || id <= 0)
            {
                MessageBox.Show("El ID del departamento debe ser un número entero positivo.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void DesbloquearCampos(bool desbloquear)
        {
            txtNombre.Enabled = desbloquear;
            txtUbicacion.Enabled = desbloquear;
            txtTelefono.Enabled = desbloquear;
            txtCorreo.Enabled = desbloquear;
            cbxEstatus.Enabled = desbloquear;
        }
        private void PoblarComboBoxEstatus()
        {
            var opcionesEstatus = new List<KeyValuePair<string, bool>>
    {
        new KeyValuePair<string, bool>("Activo", true),
        new KeyValuePair<string, bool>("Inactivo", false)
    };

            cbxEstatus.DataSource = opcionesEstatus;
            cbxEstatus.DisplayMember = "Key";
            cbxEstatus.ValueMember = "Value";
            cbxEstatus.SelectedIndex = 0; // Establecer "Activo" como predeterminado
        }


    }
}
