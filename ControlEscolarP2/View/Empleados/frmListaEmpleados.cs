using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using RecursosHumanos.Models;

namespace RecursosHumanos.View
{
    public partial class frmListaEmpleados : Form
    {
        private List<Empleado> empleados = new List<Empleado>();
        private readonly EmpleadosDataAccess empleadosDataAccess = new EmpleadosDataAccess();
        private readonly DepartamentoDataAccess _departamentoDataAccess = new DepartamentoDataAccess();
        private readonly PuestoDataAccess _puestoDataAccess = new PuestoDataAccess();

        private Dictionary<int, string> departamentos = new Dictionary<int, string>();
        private Dictionary<int, string> puestos = new Dictionary<int, string>();

        public frmListaEmpleados()
        {
            InitializeComponent();
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            CargarDepartamentosYPuestos();  // Llamamos al método para cargar departamentos y puestos
            PoblarComboDepartamento();     // Rellenamos los combos de departamento
            PoblarComboEstatus();          // Rellenamos los combos de estatus
            IniciarTabla();                // Configuramos el DataGridView
            CargarEmpleadosEnTabla();      // Cargamos los empleados en el DataGridView
        }


        private void CargarDepartamentosYPuestos()
        {
            var departamentosList = _departamentoDataAccess.ObtenerTodosLosDepartamentos();
            foreach (var departamento in departamentosList)
            {
                departamentos[departamento.IdDepartamento] = departamento.NombreDepartamento;
            }

            var puestosList = _puestoDataAccess.ObtenerTodosLosPuestos();
            foreach (var puesto in puestosList)
            {
                puestos[puesto.IdPuesto] = puesto.NombrePuesto;
            }
        }


        private void IniciarTabla()
        {
            // Configuración del estilo
            Formas.ConfigurarEstiloDataGridView(dgvEmpleados);

            // Desactivar auto-generación de columnas
            dgvEmpleados.AutoGenerateColumns = false;

            // Limpiar columnas previas
            dgvEmpleados.Columns.Clear();

            // Agregar columnas personalizadas
            dgvEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Matrícula",
                DataPropertyName = "Matricula",
                Width = 120
            });
            dgvEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 250
            });
            dgvEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Departamento",
                DataPropertyName = "Departamento",
                Width = 200
            });
            dgvEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Puesto",
                DataPropertyName = "Puesto",
                Width = 200
            });
            dgvEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estatus",
                DataPropertyName = "Estatus",
                Width = 100
            });
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
            if (departamentos.Count > 0)
            {
                cmbDepartamento.DataSource = new BindingSource(departamentos, null);
                cmbDepartamento.DisplayMember = "Value";
                cmbDepartamento.ValueMember = "Key";
                cmbDepartamento.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No se encontraron departamentos.");
                cmbDepartamento.Enabled = false;
            }
        }

        private void PoblarComboEstatus()
        {
            List<string> estatus = new List<string> { "Activo", "Inactivo" };
            cmbEstatus.DataSource = estatus;
            cmbEstatus.SelectedIndex = 0;
        }



        private void CargarEmpleadosEnTabla()
        {
            try
            {
                // Obtener los empleados desde el acceso a datos
                empleados = empleadosDataAccess.ObtenerTodosLosEmpleados();

                if (empleados.Count == 0)
                {
                    MessageBox.Show("No se encontraron empleados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Transformar los datos para mostrarlos en el DataGridView
                var empleadosMostrar = empleados.Select(e => new
                {
                    Matricula = e.Matricula,
                    Nombre = $"{e.DatosPersonales?.Nombre ?? "Desconocido"} {e.DatosPersonales?.Ap_Paterno ?? "Desconocido"}",
                    Departamento = departamentos.ContainsKey(e.Id_Departamento) ? departamentos[e.Id_Departamento] : "Desconocido",
                    Puesto = puestos.ContainsKey(e.Id_Puesto) ? puestos[e.Id_Puesto] : "Desconocido",
                    Estatus = e.Estatus == 1 ? "Activo" : "Inactivo"
                }).ToList();

                // Asignar la lista transformada al DataGridView
                dgvEmpleados.DataSource = empleadosMostrar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEmpleadosConFiltros();
        }

        private void CargarEmpleadosConFiltros()
        {
            string nombreFiltro = txtNombre.Text.Trim().ToLower();

            int departamentoFiltro = 0;
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
                (string.IsNullOrEmpty(estatusFiltro) ||
                 (e.Estatus == 1 && estatusFiltro == "activo") ||
                 (e.Estatus == 0 && estatusFiltro == "inactivo"))
            ).Select(e => new
            {
                Matrícula = e.Matricula,
                Nombre = $"{e.DatosPersonales.Nombre} {e.DatosPersonales.Ap_Paterno}",
                Departamento = departamentos.ContainsKey(e.Id_Departamento) ? departamentos[e.Id_Departamento] : "Desconocido",
                Puesto = puestos.ContainsKey(e.Id_Puesto) ? puestos[e.Id_Puesto] : "Desconocido",
                Estatus = e.Estatus == 1 ? "Activo" : "Inactivo"
            }).ToList();

            dgvEmpleados.DataSource = filtrados;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            cmbDepartamento.SelectedIndex = 0;
            cmbEstatus.SelectedIndex = 0;
            CargarEmpleadosEnTabla();
        }
    }
}
