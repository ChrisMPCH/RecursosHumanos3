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
    public partial class frmListaContratos : Form
    {
        public frmListaContratos()
        {
            InitializeComponent();
            IniciarTabla();
            InicializarCampos();
        }
        public void InicializarVentana()
        {
            InicializarCampos();

        }

        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula1, "Ingresa tu matricula");

        }

        private void frmListaContratos_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (BuscarMatricula())
            {
                MessageBox.Show("Cargando datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool BuscarMatricula()
        {
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridContratos); // Configurar el estilo del DataGridView
            ConfigurarColumnas(); // Agregar columnas personalizadas
        }

        private void ConfigurarColumnas()
        {
            dataGridContratos.Columns.Clear();
            dataGridContratos.AutoGenerateColumns = false; // Desactiva la generación automática

            // Las columnas deben coincidir EXACTAMENTE con los nombres de las propiedades de Usuario
            dataGridContratos.Columns.Add("Matricula", "Matrícula");
            dataGridContratos.Columns.Add("Nombre", "Nombre");
            dataGridContratos.Columns.Add("Departamento", "Departamento");
            dataGridContratos.Columns.Add("Antiguedad", "Antigüedad");
            dataGridContratos.Columns.Add("EstadoLaboral", "Estado Laboral");
            dataGridContratos.Columns.Add("Contratos", "Contratos");

            dataGridContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
