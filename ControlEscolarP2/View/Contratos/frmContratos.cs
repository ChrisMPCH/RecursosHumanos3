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
    public partial class frmContratos : Form
    {
        public frmContratos()
        {
            InitializeComponent(); // Inicializa los componentes del formulario
        }

        // Evento que se ejecuta al cargar el formulario
        private void frmContratos_Load(object sender, EventArgs e)
        {
            InicializaVentanaContratos(); // Llama al método para inicializar la ventana
            ConfigurarHoraEntradaSalida(); // Configura DateTimePicker para hora
            txtSalario.KeyPress += txtSalario_KeyPress; // Evento para validar solo números en txtSalario
        }

        // Método para configurar la ventana al inicio
        private void InicializaVentanaContratos()
        {
            PoblaComboTipoContrato(); // Pobla el ComboBox de tipo de contrato
            InicializarCampos(); // Inicializa los campos de texto
        }

        // Método para configurar los TextBox con texto predeterminado
        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula1, "Ingresa tu matricula");
            Formas.ConfigurarTextBox(txtDescrpcion, "Ingresa una descripcion");
            Formas.ConfigurarTextBox(txtSalario, "Ingresa el salario");
        }

        // Configurar DateTimePicker para que permita ingresar solo la hora
        private void ConfigurarHoraEntradaSalida()
        {
            dtpHoraEntrada.Format = DateTimePickerFormat.Custom;
            dtpHoraEntrada.CustomFormat = "HH:mm"; // 24 horas (Ejemplo: 14:30)
            dtpHoraEntrada.ShowUpDown = true; // Solo permite cambiar la hora

            dtpHoraSalida1.Format = DateTimePickerFormat.Custom;
            dtpHoraSalida1.CustomFormat = "HH:mm";
            dtpHoraSalida1.ShowUpDown = true;
        }

        // Método para llenar el ComboBox de tipo de contrato con valores predefinidos
        private void PoblaComboTipoContrato()
        {
            Dictionary<int, string> list_tipoCon = new Dictionary<int, string>
            {
                { 1, "Fijo" },
                { 2, "Temporal" }
            };

            cbxTipoContrato1.DataSource = new BindingSource(list_tipoCon, null);
            cbxTipoContrato1.DisplayMember = "Value";
            cbxTipoContrato1.ValueMember = "Key";

            cbxTipoContrato1.SelectedValue = 1;
        }

   

        // Método para verificar si hay campos vacíos
        private bool DatosVacios()
        {
            if (txtMatricula1.Text == "" || txtMatricula1.Text == "Ingresa tu matricula" || txtDescrpcion.Text == "" || txtDescrpcion.Text == "Ingresa una descripcion" || cbxTipoContrato1.Text == ""
                || dtpFechaInicio1.Text == "" || dtpFechaFin1.Text == "" || dtpHoraEntrada.Text == "" || dtpHoraSalida1.Text == "" || txtSalario.Text == "" ||
                txtSalario.Text == "Ingresa el salario")
            {
                return true;
            }
            return false;
        }



        // Evento que se ejecuta al presionar el botón "Generar Contrato"
        private void btnGenerar1_Click(object sender, EventArgs e)
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

            // Validar horario de entrada y salida
            if (!EmpleadoNegocio.ValidarHorario(dtpHoraEntrada.Value, dtpHoraSalida1.Value))
            {
                MessageBox.Show("La hora de entrada debe ser menor que la hora de salida.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si todo es válido, generar contrato
            MessageBox.Show("Contrato generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ;
            }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ;
            }
             MessageBox.Show("Cargando datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        // Método para validar que solo se ingresen números en el campo Salario
        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea cualquier carácter que no sea número
            }
        }

        private void btnCargaMasiva_Click(object sender, EventArgs e)
        {

            ofdArchivo.Title = "Seleccionar archivo de Excel";
            ofdArchivo.Filter = "Archivos de Excel (*.xlsx;*.xls)|*.xlsx;*.xls";
            ofdArchivo.InitialDirectory = "C:\\";
            ofdArchivo.FilterIndex = 1;
            ofdArchivo.RestoreDirectory = true;

            if (ofdArchivo.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofdArchivo.FileName;
                string extension = Path.GetExtension(filePath).ToLower();

                if (extension == ".xlsx" || extension == ".xls")
                {
                    MessageBox.Show("Archivo válido: " + filePath, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un archivo de Excel válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCancelar1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
