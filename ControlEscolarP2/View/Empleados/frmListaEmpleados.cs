using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using RecursosHumanos.Data;


namespace RecursosHumanos.View
   
{
    public partial class frmListaEmpleados : Form
    {
        public frmListaEmpleados()
        {
            InitializeComponent();
            InicializarVentana();

            var empleados = EmpleadosDataAccess.ObtenerEmpleados();
            dgvEmpleados.DataSource = empleados;
        }
        public void InicializarVentana()
        {
            PoblaComboDepartamento();
            IniciarTabla();
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dgvEmpleados);
            ConfigurarAnchoColumnas(300);
        }

        private void ConfigurarAnchoColumnas(int ancho)
        {
            foreach (DataGridViewColumn columna in dgvEmpleados.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private void PoblaComboDepartamento()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_departamentos = new Dictionary<int, string>
            {
                { 1, "Departamento 1" },
                { 2, "Departamento 2" },
                { 3, "Departamento 3" }
            };

            //Asignar los valores al comboBox
            cmbDepartamento.DataSource = new BindingSource(list_departamentos, null);
            cmbDepartamento.DisplayMember = "Value"; //lo que se mestra
            cmbDepartamento.ValueMember = "Key"; //lo que se guarda como SelectedValue

            cmbDepartamento.SelectedIndex = 1;

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbDepartamento.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un departamento para buscar", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarEmpleadosConFiltros();
        }

        private void CargarFiltros()
        {
            var empleados = EmpleadosDataAccess.ObtenerEmpleados();

            var departamentos = empleados.Select(e => e.Departamento).Distinct().ToList();
            departamentos.Insert(0, ""); // Opción vacía para "Todos"
            cmbDepartamento.DataSource = departamentos;

            var estatus = empleados.Select(e => e.Estatus).Distinct().ToList();
            estatus.Insert(0, "");
            cmbEstatus.DataSource = estatus;
        }
        private void CargarEmpleados()
        {
            var empleados = EmpleadosDataAccess.ObtenerEmpleados();
            dgvEmpleados.DataSource = empleados;
        }

        private void frmListaEmpleados_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
            CargarFiltros();
        }

        private void CargarEmpleadosConFiltros()
        {
            string nombre = txtNombre.Text.Trim().ToLower();
            string departamento = cmbDepartamento.SelectedItem?.ToString()?.ToLower() ?? "";
            string estatus = cmbEstatus.SelectedItem?.ToString()?.ToLower() ?? "";

            var empleados = EmpleadosDataAccess.ObtenerEmpleados();

            var filtrados = empleados.Where(e =>
                (string.IsNullOrEmpty(nombre) || e.Nombre.ToLower().Contains(nombre) || e.Apellido.ToLower().Contains(nombre)) &&
                (string.IsNullOrEmpty(departamento) || e.Departamento.ToLower() == departamento) &&
                (string.IsNullOrEmpty(estatus) || e.Estatus.ToLower() == estatus)
            ).ToList();

            dgvEmpleados.DataSource = filtrados;
        }

    }
}
