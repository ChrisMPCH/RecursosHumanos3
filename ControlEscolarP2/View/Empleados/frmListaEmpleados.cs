using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RecursosHumanos.Data;
using RecursosHumanos.Model;

namespace RecursosHumanos.View
{
    public partial class frmListaEmpleados : Form
    {
        private List<Empleado> empleados = new List<Empleado>();
        private readonly EmpleadosDataAccess empleadosDataAccess = new EmpleadosDataAccess();

        public frmListaEmpleados()
        {
            InitializeComponent();
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            PoblarComboDepartamento();
            PoblarComboEstatus();
            IniciarTabla();
            CargarEmpleados(); // Carga todos los empleados al inicio
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dgvEmpleados);
            ConfigurarAnchoColumnas(200);
        }

        private void ConfigurarAnchoColumnas(int ancho)
        {
            foreach (DataGridViewColumn columna in dgvEmpleados.Columns)
            {
                columna.Width = ancho;
            }
        }

        private void PoblarComboDepartamento()
        {
            // Ya predefinido (no se modifica)
            Dictionary<int, string> list_departamentos = new Dictionary<int, string>
            {
                { 1, "Departamento 1" },
                { 2, "Departamento 2" },
                { 3, "Departamento 3" }
            };

            cmbDepartamento.DataSource = new BindingSource(list_departamentos, null);
            cmbDepartamento.DisplayMember = "Value";
            cmbDepartamento.ValueMember = "Key";
            cmbDepartamento.SelectedIndex = 0;
        }

        private void PoblarComboEstatus()
        {
            List<string> estatus = new List<string> { "Activo", "Inactivo" };
            cmbEstatus.DataSource = estatus;
            cmbEstatus.SelectedIndex = 0;
        }

        private void CargarEmpleados()
        {
            empleados = empleadosDataAccess.ObtenerEmpleados();
            dgvEmpleados.DataSource = empleados.Select(e => new
            {
                ID = e.Id_Empleado,
                NombreCompleto = $"{e.DatosPersonales.Nombre} {e.DatosPersonales.Ap_Paterno}",
                Departamento = e.Id_Departamento,
                Puesto = e.Id_Puesto,
                Estatus = e.Estatus
            }).ToList();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEmpleadosConFiltros();
        }

        private void CargarEmpleadosConFiltros()
        {
            string nombreFiltro = txtNombre.Text.Trim().ToLower();

            // Verifica si SelectedItem es null antes de realizar el unboxing
            int departamentoFiltro = 0;  // Valor predeterminado
            if (cmbDepartamento.SelectedItem is KeyValuePair<int, string> selectedDepartamento)
            {
                departamentoFiltro = selectedDepartamento.Key;
            }

            string estatusFiltro = cmbEstatus.SelectedItem?.ToString()?.ToLower() ?? "";

            var filtrados = empleados.Where(e =>
                (string.IsNullOrEmpty(nombreFiltro) ||
                 e.DatosPersonales.Nombre.ToLower().Contains(nombreFiltro) ||
                 e.DatosPersonales.Ap_Paterno.ToLower().Contains(nombreFiltro)) &&
                (departamentoFiltro == 0 || e.Id_Departamento == departamentoFiltro) &&
                (string.IsNullOrEmpty(estatusFiltro) || e.Estatus.ToString().ToLower() == estatusFiltro)
            ).Select(e => new
            {
                ID = e.Id_Empleado,
                NombreCompleto = $"{e.DatosPersonales.Nombre} {e.DatosPersonales.Ap_Paterno}",
                Departamento = e.Id_Departamento,
                Puesto = e.Id_Puesto,
                Estatus = e.Estatus
            }).ToList();

            dgvEmpleados.DataSource = filtrados;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpia el campo de nombre
            txtNombre.Clear();
            // Reinicia la selección del combo de departamentos al índice 0
            cmbDepartamento.SelectedIndex = 0;
            // Reinicia la selección del combo de estatus al índice 0 (puedes usar -1 si quieres dejarlo sin seleccionar)
            cmbEstatus.SelectedIndex = 0;
            // Carga todos los empleados sin filtros
            CargarEmpleados();
        }
    }
}

