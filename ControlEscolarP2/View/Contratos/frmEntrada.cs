using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursosHumanos.Bussines;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmEntrada : Form
    {
        public frmEntrada()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            InicializarCampos();

        }

        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula1, "Ingresa tu matricula");

        }

        private bool HoraEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!EsMatriculaValida())
            {
                return false;
            }

            return true;

        }


        private bool EsMatriculaValida()
        {
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            //if (HoraEntrada())
            //{
            //    MessageBox.Show("Datos cargados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            string mensaje = AsistenciaNegocio.RegistrarEntrada(txtMatricula1.Text.Trim());
            MessageBox.Show(mensaje, "Registro de entrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (HoraEntrada())
            {
                MessageBox.Show("Registro guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAceptarSalida_Click(object sender, EventArgs e)
        {

            //if (HoraEntrada())
            //{
            //    MessageBox.Show("Registro guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            string mensaje = AsistenciaNegocio.RegistrarSalida(txtMatricula1.Text.Trim());
            MessageBox.Show(mensaje, "Registro de salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pnlDatos_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
