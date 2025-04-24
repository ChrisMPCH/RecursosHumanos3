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
using  RecursosHumanos.Model;
using RecursosHumanos.Controller;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static RecursosHumanos.View.frmListaUsuarios;
using System.Windows.Forms;

namespace RecursosHumanos.View
{
    public partial class frmReportes : Form
    {
        private ContratoController _contratosController = new ContratoController();
        public frmReportes()
        {
            InitializeComponent();
        }
        private void frmReportes_Load_1(object sender, EventArgs e)
        {
            InicializaVentanaReportes();
            IniciarTabla();
            InicializarCampos();


        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridUsuarios); // Configurar el estilo del DataGridView
            ConfigurarColumnas(); // Agregar columnas personalizadas
        }
        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula, "Ingresa la matricula");
        }

        private void ConfigurarColumnas()
        {
            dataGridUsuarios.Columns.Clear();
            dataGridUsuarios.AutoGenerateColumns = false; // Desactiva la generación automática

            // Las columnas deben coincidir EXACTAMENTE con los nombres de las propiedades de Usuario
            dataGridUsuarios.Columns.Add("Matricula", "Matrícula");
            dataGridUsuarios.Columns.Add("Nombre", "Nombre");
            dataGridUsuarios.Columns.Add("Departamento", "Departamento");
            dataGridUsuarios.Columns.Add("Antiguedad", "Antigüedad");
            dataGridUsuarios.Columns.Add("EstadoLaboral", "Estado Laboral");
            dataGridUsuarios.Columns.Add("FechaIngreso", "Fecha de Ingreso");

            dataGridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void InicializaVentanaReportes()
        {
            PoblaComboDepartamento();
            PoblaComboEstatus();
            PoblaComboTipoContrato();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
        }

        private void PoblaComboDepartamento()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_departamento = new Dictionary<int, string>
            {
            { 1, "Asistio" },
            { 0, "Null" },
            { 2, "No asistio" }
            };

            // Asignar el diccionario al ComboBox
            cbxDepartamento1.DataSource = new BindingSource(list_departamento, null);
            cbxDepartamento1.DisplayMember = "Value";  // Lo que se muestra
            cbxDepartamento1.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxDepartamento1.SelectedValue = 1;

        }
        private void PoblaComboEstatus()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_estadoL = new Dictionary<int, string>
            {
                { 1, "Activo" },
                { 0, "Null" },
                { 2, "Inactivo" }
            };

            // Asignar el diccionario al ComboBox
            cbxEstatus.DataSource = new BindingSource(list_estadoL, null);
            cbxEstatus.DisplayMember = "Value";  // Lo que se muestra
            cbxEstatus.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxEstatus.SelectedValue = 1;

        }

        private void PoblaComboTipoContrato()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_tipoC = new Dictionary<int, string>
            {
                { 1, "Temporal" },
                { 0, "Null" },
                { 2, "Indifinido" }
            };
            // Asignar el diccionario al ComboBox
            cbxTipoContrato.DataSource = new BindingSource(list_tipoC, null);
            cbxTipoContrato.DisplayMember = "Value";  // Lo que se muestra
            cbxTipoContrato.ValueMember = "Key";      // Lo que se guarda como SelectedValue
            cbxTipoContrato.SelectedValue = 1;
        }


        private bool BuscarTipoReporte()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, seleccione un tipo de reporte.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool DatosVacios()
        {
            if (cbxDepartamento1.Text == "" || cbxTipoContrato.Text == "" || cbxEstatus.Text == "" || dtpFechaInicio.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       

        private void btnGenerar1_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();

            // Validación correcta solo si se ingresó algo distinto del texto por defecto
            if (!string.IsNullOrEmpty(matricula) && matricula != "Ingresa la matricula")
            {
                if (!EmpleadoNegocio.EsNoMatriculaValido(matricula))
                {
                    MessageBox.Show("La matrícula ingresada no tiene un formato válido.\nEjemplo: E-2023-456", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                // Si el campo estaba vacío o con texto de ayuda, lo tratamos como filtro nulo
                matricula = null;
            }

            int tipoContrato = cbxTipoContrato.SelectedValue != null ? (int)cbxTipoContrato.SelectedValue : 0;
            int estatus = cbxEstatus.SelectedValue != null ? (int)cbxEstatus.SelectedValue : 0;
            int departamento = cbxDepartamento1.SelectedValue != null ? (int)cbxDepartamento1.SelectedValue : 0;
            DateTime fechaInicio = dtpFechaInicio.Value.Date;
            DateTime fechaFin = dtpFechaFin.Value.Date;

            List<Contrato> contratos = new ContratoController().ObtenerContratosFiltrados(
                matricula, tipoContrato, estatus, departamento, fechaInicio, fechaFin);

            dataGridUsuarios.Rows.Clear();
            foreach (var c in contratos)
            {
                dataGridUsuarios.Rows.Add(
                    c.Matricula,
                    (DateTime.Now - c.FechaInicio).Days / 30 + " meses",
                    c.Estatus ? "Activo" : "Inactivo",
                    c.FechaInicio.ToShortDateString(),
                    c.FechaFin.ToShortDateString()
                );
            }

            MessageBox.Show(
                contratos.Count > 0
                    ? $"Se encontraron {contratos.Count} contrato(s) con los filtros aplicados."
                    : "No se encontraron contratos con los filtros aplicados.",
                "Resultado del reporte",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        } 

        private void cbxEstadoLaboral1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxEstatus.SelectedValue != null && int.TryParse(cbxEstatus.SelectedValue.ToString(), out int selectedValue))
            {
                if (selectedValue == 2 || selectedValue == 0 || selectedValue == 1)
                {
                    lblEstatus.Visible = true;
                }
                else
                {
                    lblEstatus.Visible = false;
                }
            }
        }

        
        
    


    }
}


