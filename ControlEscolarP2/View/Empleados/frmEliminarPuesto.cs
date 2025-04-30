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
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmEliminarPuesto : Form
    {
        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo
        public frmEliminarPuesto(Guna2GradientPanel pnlCambiante)
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
            Formas.ConfigurarTextBox(txtIdPuesto, "Ingrese el puesto que desea eliminar");
        }



        private bool DatosVacios()
        {
            if (txtIdPuesto.Text == "" || txtIdPuesto.Text == "Ingrese el puesto que desea eliminar")
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

        private bool EliminarPuesto()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (!BuscarPuesto())
            {
                MessageBox.Show("No se puede eliminar porque el puesto no existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private bool BuscarPuesto()
        {
            if (!DatosVacios())
            {
                return false;
            }

            PuestoController controller = new PuestoController();
            var Puesto = controller.ObtenerDetallePuesto(int.Parse(txtIdPuesto.Text.Trim()));

            if (Puesto == null)
            {
                MessageBox.Show("Puesto no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Mostrar datos en los campos
            lblNombrePuesto.Text = Puesto.NombrePuesto;
            lblDescripcionPuesto.Text = Puesto.DescripcionPuesto;
            DesbloquearCampos(true);

            return true;
        }
        private void DesbloquearCampos(bool desbloquear)
        {
            txtIdPuesto.Enabled = desbloquear;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarIdPuesto())
            {
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este departamento? Esto lo marcará como inactivo.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                PuestoController controller = new PuestoController();
                var (exito, mensaje) = controller.EliminarPuestoLogico(int.Parse(txtIdPuesto.Text.Trim()));

                if (exito)
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InicializarCampos();
                    DesbloquearCampos(false);
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarIdPuesto()
        {
            if (string.IsNullOrWhiteSpace(txtIdPuesto.Text))
            {
                MessageBox.Show("Ingrese un ID de departamento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtIdPuesto.Text.Trim(), out int id) || id <= 0)
            {
                MessageBox.Show("El ID del departamento debe ser un número entero positivo.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
