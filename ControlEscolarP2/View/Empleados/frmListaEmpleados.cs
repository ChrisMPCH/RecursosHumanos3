using RecursosHumanos.Utilities;
using RecursosHumanos.Controller;

namespace RecursosHumanos.View
{
    public partial class frmListaEmpleados : Form
    {
        private List<Empleado> empleados = new List<Empleado>();
        private readonly EmpleadosController empleadosController = new EmpleadosController();
        private readonly DepartamentoController _departamentoController = new DepartamentoController();
        private readonly PuestoController _puestoController = new PuestoController();

        private Dictionary<int, string> departamentos = new Dictionary<int, string>();
        private Dictionary<int, string> puestos = new Dictionary<int, string>();

        public frmListaEmpleados()
        {
            InitializeComponent();
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            CargarDepartamentos();  // Cargamos los departamentos
            CargarPuestos();        // Cargamos los puestos
            PoblarComboDepartamento();  // Rellenamos combo de departamentos
            PoblarComboEstatus();     // Rellenamos combo de estatus
            IniciarTabla();           // Configuramos el DataGridView
            CargarEmpleadosEnTabla(); // Cargamos los empleados
        }

        private void CargarDepartamentos()
        {
            var departamentosList = _departamentoController.ObtenerTodosLosDepartamentos();
            foreach (var departamento in departamentosList)
            {
                departamentos[departamento.IdDepartamento] = departamento.NombreDepartamento;
            }
        }

        private void CargarPuestos()
        {
            var puestosList = _puestoController.ObtenerTodosLosPuestos();
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

        private void PoblarComboDepartamento()
        {
            if (departamentos.Count > 0)
            {
                // Insertar la opción "Todos" al principio de la lista de departamentos
                var departamentosConTodos = new Dictionary<int, string> { { 0, "Todos" } };

                // Agregar los departamentos a la nueva lista
                foreach (var departamento in departamentos)
                {
                    departamentosConTodos.Add(departamento.Key, departamento.Value);
                }

                // Asignar la nueva lista al ComboBox
                cmbDepartamento.DataSource = new BindingSource(departamentosConTodos, null);
                cmbDepartamento.DisplayMember = "Value";
                cmbDepartamento.ValueMember = "Key";
                cmbDepartamento.SelectedIndex = 0; // Selección por defecto es "Todos"
            }
            else
            {
                MessageBox.Show("No se encontraron departamentos.");
                cmbDepartamento.Enabled = false;
            }
        }


        private void PoblarComboEstatus()
        {
            // Agregar la opción "Todos" al combo de estatus
            List<string> estatus = new List<string> { "Todos", "Activo", "Inactivo" };
            cmbEstatus.DataSource = estatus;
            cmbEstatus.SelectedIndex = 0; // Selección por defecto es "Todos"
        }

        private void CargarEmpleadosEnTabla()
        {
            try
            {
                empleados = empleadosController.ObtenerEmpleados();

                if (empleados.Count == 0)
                {
                    MessageBox.Show("No se encontraron empleados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var empleadosMostrar = empleados.Select(e => new
                {
                    Matricula = e.Matricula,
                    Nombre = e.Nombre,
                    Departamento = e.Departamento,
                    Puesto = e.Puesto,
                    Estatus = e.EstatusTexto
                }).ToList();

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
            try
            {
                string nombreFiltro = txtNombre.Text.Trim().ToLower();

                int departamentoFiltro = 0;
                if (cmbDepartamento.SelectedItem is KeyValuePair<int, string> selectedDepartamento)
                {
                    departamentoFiltro = selectedDepartamento.Key;
                }

                string estatusFiltro = cmbEstatus.SelectedItem?.ToString()?.ToLower() ?? "";

                var filtrados = empleados.Where(e =>
                {
                    var nombreCompleto = $"{e.DatosPersonales.Nombre} {e.DatosPersonales.Ap_Paterno} {e.DatosPersonales.Ap_Materno}".ToLower();

                    return (string.IsNullOrEmpty(nombreFiltro) || nombreCompleto.Contains(nombreFiltro)) &&
                           (departamentoFiltro == 0 || e.Id_Departamento == departamentoFiltro) &&
                           (string.IsNullOrEmpty(estatusFiltro) ||
                            (estatusFiltro == "activo" && e.Estatus == 1) ||
                            (estatusFiltro == "inactivo" && e.Estatus == 0) ||
                            (estatusFiltro == "todos"));
                })
                .Select(e => new
                {
                    Matricula = e.Matricula,
                    Nombre = $"{e.DatosPersonales.Nombre} {e.DatosPersonales.Ap_Paterno} {e.DatosPersonales.Ap_Materno}",
                    Departamento = departamentos.ContainsKey(e.Id_Departamento) ? departamentos[e.Id_Departamento] : "Desconocido",
                    Puesto = puestos.ContainsKey(e.Id_Puesto) ? puestos[e.Id_Puesto] : "Desconocido",
                    Estatus = e.Estatus == 1 ? "Activo" : "Inactivo"
                })
                .ToList();

                if (filtrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron empleados con los filtros aplicados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                dgvEmpleados.DataSource = filtrados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar filtros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            cmbDepartamento.SelectedIndex = 0; // Reseteamos a "Todos"
            cmbEstatus.SelectedIndex = 0; // Reseteamos a "Todos"
            CargarEmpleadosEnTabla(); // Recargamos todos los empleados
        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
