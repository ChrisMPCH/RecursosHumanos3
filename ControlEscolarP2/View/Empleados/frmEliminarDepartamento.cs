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
    public partial class frmEliminarDepartamento : Form
    {
        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo
        public frmEliminarDepartamento(Guna2GradientPanel pnlCambiante)
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
            Formas.ConfigurarTextBox(txtDeprtamento, "Ingrese el departamento que desea eliminar");
        }



        private bool DatosVacios()
        {
            if (txtDeprtamento.Text == "" || txtDeprtamento.Text == "Ingrese el departamento que desea eliminar")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmListadoDepartamentos formListado = new frmListadoDepartamentos(pnlCambiante);
            Formas.abrirPanelForm(formListado, pnlCambiante);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (EliminarDepartamento())
            {
                MessageBox.Show("Datos eliminados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool EliminarDepartamento()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (!BuscarDepartamento())
            {
                MessageBox.Show("No se puede eliminar porque el departamento no existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool BuscarDepartamento()
        {
            if (string.IsNullOrWhiteSpace(txtDeprtamento.Text))
            {
                MessageBox.Show("Ingrese un departamento para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
