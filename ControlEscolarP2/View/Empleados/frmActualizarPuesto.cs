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
    public partial class frmActualizarPuesto : Form
    {
        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo
        public frmActualizarPuesto(Guna2GradientPanel pnlCambiante)
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
            Formas.ConfigurarTextBox(txtIdPuesto, "Ingrese el puesto que desea actualizar");
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre");
            Formas.ConfigurarTextBox(txtDescripcion, "Ingrese descripción");
        }



        private bool DatosVacios()
        {
            if (txtIdPuesto.Text == "" || txtIdPuesto.Text == "Ingrese el puesto que desea actualizar" ||
            txtNombre.Text == "" || txtNombre.Text == "Ingrese nombre" ||
            txtDescripcion.Text == "" || txtDescripcion.Text == "Ingrese descripción")
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
            if (GuardarPuesto())
            {
                MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool GuardarPuesto()
        {
            if (DatosVacios())
            {
                return false;
            }

            PuestoController controller = new PuestoController();

            Puesto puesto = new Puesto
            {
                IdPuesto= int.Parse(txtIdPuesto.Text.Trim()),
                NombrePuesto = txtNombre.Text.Trim(),
                DescripcionPuesto = txtDescripcion.Text.Trim(),
                Estatus = (bool)cbxEstatus.SelectedValue
            };

            var (exito, mensaje) = controller.ActualizarPuesto(puesto);

            if (exito)
            {
                MessageBox.Show("Puesto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            frmListadoPuestos formListado = new frmListadoPuestos(pnlCambiante);
            Formas.abrirPanelForm(formListado, pnlCambiante);
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!BuscarPuesto())
            {
                return;
            }
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
        private void DesbloquearCampos(bool desbloquear)
        {
            txtNombre.Enabled = desbloquear;
            txtDescripcion.Enabled = desbloquear;
            cbxEstatus.Enabled = desbloquear;
        }
        private bool BuscarPuesto()
        {
            if (!DatosVacios())
            {
                return false;
            }
            PuestoController controller = new PuestoController();
            var puesto = controller.ObtenerDetallePuesto(int.Parse(txtIdPuesto.Text.Trim()));

            if (puesto == null)
            {
                MessageBox.Show("Puesto no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Mostrar datos en los campos
            txtNombre.Text = puesto.NombrePuesto;
            txtDescripcion.Text = puesto.DescripcionPuesto;
            cbxEstatus.SelectedValue = puesto.Estatus;

            DesbloquearCampos(true);

            return true;
        }


    }
}
   
