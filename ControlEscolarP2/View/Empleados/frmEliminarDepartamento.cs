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
            Formas.ConfigurarTextBox(txtIdDepartamento, "Ingrese el departamento que desea eliminar");
        }



        private bool DatosVacios()
        {
            if (txtIdDepartamento.Text == "" || txtIdDepartamento.Text == "Ingrese el departamento que desea eliminar")
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
            if (!BuscarDepartamento())
            {
                return;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarIdDepartamento())
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
                DepartamentoController controller = new DepartamentoController();
                var (exito, mensaje) = controller.EliminarDepartamentoLogico(int.Parse(txtIdDepartamento.Text.Trim()));

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

        private bool ValidarIdDepartamento()
        {
            if (string.IsNullOrWhiteSpace(txtIdDepartamento.Text))
            {
                MessageBox.Show("Ingrese un ID de departamento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtIdDepartamento.Text.Trim(), out int id) || id <= 0)
            {
                MessageBox.Show("El ID del departamento debe ser un número entero positivo.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void DesbloquearCampos(bool desbloquear)
        {
            txtIdDepartamento.Enabled = desbloquear;
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
            lblNombreDepto.Text = departamento.NombreDepartamento;
            lblUbicacionDepto.Text = departamento.Ubicacion;
            lblTelefonoDepto.Text = departamento.TelefonoDepartamento;
            lblCorreoDepto.Text = departamento.EmailDepartamento;
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
    }
}
