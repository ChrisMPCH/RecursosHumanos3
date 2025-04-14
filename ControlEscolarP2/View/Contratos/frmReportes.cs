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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static RecursosHumanos.View.frmListaUsuarios;

namespace RecursosHumanos.View
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }
        private void frmReportes_Load_1(object sender, EventArgs e)
        {
            InicializaVentanaReportes();
            IniciarTabla();

        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridUsuarios); // Configurar el estilo del DataGridView
            ConfigurarColumnas(); // Agregar columnas personalizadas
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
            PoblaComboAntiguedad();
            PoblaComboAsistencia();
            PoblaComboDepartamento();
            PoblaComboEstadoaboral();
            dtpFechaIngreso.Value = DateTime.Now;
        }

        private void PoblaComboAsistencia()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_asistencia = new Dictionary<int, string>
            {
                { 1, "Asistio" },
                { 0, "Null" },
                { 2, "No asistio" }
            };

            // Asignar el diccionario al ComboBox
            cbxAsistencia1.DataSource = new BindingSource(list_asistencia, null);
            cbxAsistencia1.DisplayMember = "Value";  // Lo que se muestra
            cbxAsistencia1.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxAsistencia1.SelectedValue = 1;

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

      
        private void PoblaComboAntiguedad()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_antiguedad = new Dictionary<int, string>
            {
                { 1, "1 año" },
                { 0, "Null" },
                { 2, "2 años" },
                {3, "3 años" },
                {4, "4 años" },
                {5, "5 años" }
            };

            // Asignar el diccionario al ComboBox
            cbxAntiguedad1.DataSource = new BindingSource(list_antiguedad, null);
            cbxAntiguedad1.DisplayMember = "Value";  // Lo que se muestra
            cbxAntiguedad1.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxAntiguedad1.SelectedValue = 1;

        }

      

        private void PoblaComboEstadoaboral()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_estadoL = new Dictionary<int, string>
            {
                { 1, "Activo" },
                { 0, "Null" },
                { 2, "Inactivo" }
            };

            // Asignar el diccionario al ComboBox
            cbxEstadoLaboral1.DataSource = new BindingSource(list_estadoL, null);
            cbxEstadoLaboral1.DisplayMember = "Value";  // Lo que se muestra
            cbxEstadoLaboral1.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxEstadoLaboral1.SelectedValue = 1;

        }
        private void cbxEstadoLaboral_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxEstadoLaboral1.SelectedValue != null && int.TryParse(cbxEstadoLaboral1.SelectedValue.ToString(), out int selectedValue))
            {
                if (selectedValue == 2 || selectedValue == 0 || selectedValue == 1)
                {
                    lblEstadoLaboral.Visible = true;
                }
                else
                {
                    lblEstadoLaboral.Visible = false;
                }
            }
        }
     
        private bool GenerarReporte()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, seleccione un tipo de reporte.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
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
            if (cbxAsistencia1.Text == "" || cbxDepartamento1.Text == "" || cbxAntiguedad1.Text == "" || cbxEstadoLaboral1.Text == "" || dtpFechaIngreso.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnAceptar1_Click(object sender, EventArgs e)
        {
            if (BuscarTipoReporte())
            {
                MessageBox.Show("Reporte seleccionado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGenerar1_Click(object sender, EventArgs e)
        {
         
            if (GenerarReporte())
            {
                MessageBox.Show("El reporte se generara correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
    }
    }
}


