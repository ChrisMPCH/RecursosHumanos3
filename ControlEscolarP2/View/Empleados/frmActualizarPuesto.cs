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
        }

        private void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtPuesto, "Ingrese el puesto que desea actualizar");
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre");
            Formas.ConfigurarTextBox(txtDescripcion, "Ingrese descripción");
        }



        private bool DatosVacios()
        {
            if (txtPuesto.Text == "" || txtPuesto.Text == "Ingrese el puesto que desea actualizar" ||
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

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmListadoPuestos formListado = new frmListadoPuestos(pnlCambiante);
            Formas.abrirPanelForm(formListado, pnlCambiante);
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {

            {
                string nombrePuesto = txtPuesto.Text.Trim(); // Elimina espacios extras

                // Validación 1: Verificar si el usuario ingresó texto
                if (string.IsNullOrWhiteSpace(nombrePuesto))
                {
                    MessageBox.Show("Ingrese un nombre de puesto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación 2: Evitar números y caracteres especiales
                if (!nombrePuesto.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    MessageBox.Show("El nombre del puesto solo debe contener letras y espacios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Puesto validado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}