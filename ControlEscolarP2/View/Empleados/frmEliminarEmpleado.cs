using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursosHumanosCore.Bussines;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmEliminarEmpleado : Form
    {
        public frmEliminarEmpleado()
        {
            InitializeComponent();
            InicializarVentana();
        }



        public void InicializarVentana()
        {
            InicializarCampos();
            dtpFechaBaja.Value = DateTime.Now;
        }



        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula, "Ingrese matrícula");
            Formas.ConfigurarTextBox(txtMotivo, "Ingrese motivo");
        }



        private bool DatosValidos()
        {
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool DatosVacios()
        {
            if (txtMatricula.Text == "" || txtNombre.Text == "" || txtMotivo.Text == "" || dtpFechaBaja.Text == "")
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
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (EliminarEmpleado())
            {
                MessageBox.Show("Datos eliminados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool EliminarEmpleado()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!DatosValidos())
            {
                return false;
            }

            if (!BuscarEmpleado())
            {
                MessageBox.Show("No se puede eliminar porque el empleado no existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btmBuscarM_Click(object sender, EventArgs e)
        {
            if (BuscarEmpleado())
            {
                MessageBox.Show("Empleado encontrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Empleado no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool BuscarEmpleado()
        {
            // Verifica si el campo txtMatricula está vacío o contiene solo espacios
            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                // Si está vacío, muestra el mensaje y retorna false, deteniendo la ejecución
                MessageBox.Show("Por favor, ingrese una matrícula para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Si el campo no está vacío, entonces verifica el formato de la matrícula
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida. Verifique el formato.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // Matrícula válida y no vacía
        }
    }
}
