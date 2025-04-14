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
    public partial class frmDias_calculados : Form
    {

        public frmDias_calculados()
        {
            InitializeComponent();
            InicializaVentana();
        }
        // Método para configurar la ventana al inicio
        private void InicializaVentana()
        {
            InicializarCampos(); // Inicializa los campos de texto
        }

        // Método para configurar los TextBox con texto predeterminado
        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula1, "Ingresa tu matricula");
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Cargando datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // Validar matrícula
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que todos los campos estén llenos
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar fechas
            if (!EmpleadoNegocio.ValidarFechas(dtpFechaInicio1.Value, dtpFechaFin1.Value))
            {
                MessageBox.Show("La fecha de inicio debe ser menor que la fecha de fin.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Si todo es válido, generar contrato
            MessageBox.Show("Se han calculado los dias trabajados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool DatosVacios()
        {
            if (txtMatricula1.Text == "" || txtMatricula1.Text == "Ingresa tu matricula" || dtpFechaInicio1.Text == "" || dtpFechaFin1.Text == "" )
            {
                return true;
            }
            return false;
        }

        private void Regresar(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
