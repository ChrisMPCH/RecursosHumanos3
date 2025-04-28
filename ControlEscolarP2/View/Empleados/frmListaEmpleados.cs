using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private DepartamentoDataAccess _departamentoDataAccess = new DepartamentoDataAccess();
        private PuestoDataAccess _puestoDataAccess = new PuestoDataAccess();

        // Diccionarios para almacenar los departamentos y puestos
        private Dictionary<int, string> departamentos = new Dictionary<int, string>();
        private Dictionary<int, string> puestos = new Dictionary<int, string>();

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
            // Obtener los departamentos desde la base de datos
            List<Departamento> departamentosList = _departamentoDataAccess.ObtenerTodosLosDepartamentos();

            // Rellenar el diccionario con los resultados de la base de datos
            foreach (var departamento in departamentosList)
            {
                departamentos.Add(departamento.IdDepartamento, departamento.NombreDepartamento);
            }

            // Verificar si existen departamentos antes de continuar
            if (departamentos.Count > 0)
            {
                // Asignar los valores al ComboBox
                cmbDepartamento.DataSource = new BindingSource(departamentos, null);
                cmbDepartamento.DisplayMember = "Value"; // Lo que se muestra
                cmbDepartamento.ValueMember = "Key"; // Lo que se guarda como SelectedValue
                cmbDepartamento.SelectedIndex = 0; // Seleccionar el primer elemento
            }
            else
            {
                MessageBox.Show("No se encontraron departamentos.");
                // Puedes deshabilitar el ComboBox si no hay departamentos
                cmbDepartamento.Enabled = false;
            }
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
            dgvEmpleados.Columns.Clear(); // Limpiar las columnas anteriores

            // Poblar los puestos (también puedes usar un diccionario similar al de departamentos)
            List<Puesto> listaPuestos = _puestoDataAccess.ObtenerTodosLosPuestos();
            foreach (var puesto in listaPuestos)
            {
                puestos[puesto.IdPuesto] = puesto.NombrePuesto;
            }

            // Asignar los datos a la DataGridView
            dgvEmpleados.DataSource = empleados.Select(e => new
            {
                // Mostrar la Matrícula y Nombre Completo utilizando los métodos de la clase Empleado
                Matricula = e.Matricula,
                Nombre = e.ObtenerNombreCompleto(), // Obtener nombre completo del empleado

                // Mapear el ID del Departamento a su nombre (puedes usar el diccionario de departamentos)
                Departamento = departamentos.ContainsKey(e.Id_Departamento) ? departamentos[e.Id_Departamento] : "Desconocido",

                // Mapear el ID del Puesto a su nombre 
                Puesto = puestos.ContainsKey(e.Id_Puesto) ? puestos[e.Id_Puesto] : "Desconocido",

                // Obtener el Estatus como texto (Activo/Inactivo)
                Estatus = e.ObtenerEstatusTexto()
            }).ToList();
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
                (string.IsNullOrEmpty(estatusFiltro) || (e.Estatus == 1 && estatusFiltro == "activo") || (e.Estatus == 0 && estatusFiltro == "inactivo"))
            ).Select(e => new
            {
                Matrícula = e.Matricula,
                Nombre = $"{e.DatosPersonales.Nombre} {e.DatosPersonales.Ap_Paterno}",
                // Mapear el ID del Departamento a su nombre
                Departamento = departamentos.ContainsKey(e.Id_Departamento) ? departamentos[e.Id_Departamento] : "Desconocido",
                // Mapear el ID del Puesto a su nombre
                Puesto = puestos.ContainsKey(e.Id_Puesto) ? puestos[e.Id_Puesto] : "Desconocido",
                Estatus = e.Estatus == 1 ? "Activo" : "Inactivo" // Mapear estatus
            }).ToList();

            dgvEmpleados.Columns.Clear(); // <-- limpia columnas antes
            dgvEmpleados.DataSource = filtrados;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpia el campo de nombre
            txtNombre.Clear();
            // Reinicia la selección del combo de departamentos al índice 0
            cmbDepartamento.SelectedIndex = 0;
            // Reinicia la selección del combo de estatus al índice 0
            cmbEstatus.SelectedIndex = 0;
            // Carga todos los empleados sin filtros
            CargarEmpleados();
        }
    }
}
