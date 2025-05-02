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
using RecursosHumanos.Models;
using RecursosHumanos.Data;

namespace RecursosHumanos.View
{
    public partial class frmReportes : Form
    {
        private ContratoController _contratosController = new ContratoController();
        private readonly DepartamentoController _departamentoController = new DepartamentoController();
        private Dictionary<int, string> departamentos = new Dictionary<int, string>();

        public frmReportes()
        {
            InitializeComponent();
        }
        private void frmReportes_Load_1(object sender, EventArgs e)
        {
            InicializaVentanaReportes();
            IniciarTabla();
            InicializarCampos();
            MostrarContratosSinFiltro();


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
            dataGridUsuarios.AutoGenerateColumns = false;

            dataGridUsuarios.Columns.Add("Id_Contrato", "ID Contrato");
            dataGridUsuarios.Columns.Add("Matricula", "Matrícula");
            dataGridUsuarios.Columns.Add("NombreEmpleado", "Empleado");
            dataGridUsuarios.Columns.Add("NombreDepartamento", "Departamento");
           
            dataGridUsuarios.Columns.Add("FechaInicio", "Fecha Inicio");
            dataGridUsuarios.Columns.Add("FechaFin", "Fecha Fin");
            dataGridUsuarios.Columns.Add("HoraEntrada", "Hora Entrada");
            dataGridUsuarios.Columns.Add("HoraSalida", "Hora Salida");
            dataGridUsuarios.Columns.Add("Sueldo", "Salario");
            dataGridUsuarios.Columns.Add("Descripcion", "Descripción");
            dataGridUsuarios.Columns.Add("Estatus", "Estatus");
            dataGridUsuarios.Columns.Add("NombreTipoContrato", "Tipo de Contrato");

            dataGridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }



        private void InicializaVentanaReportes()
        {
            CargarDepartamentosDesdeBD();
            PoblarComboDepartamento();
            PoblaComboEstatus();
            PoblaComboTipoContrato();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
        }
        private void PoblarComboDepartamento()
        {
            if (departamentos.Count > 0)
            {
                // Agregamos manualmente "Todos" con clave -1
                var departamentosConTodos = new Dictionary<int, string>
        {
            { -1, "Todos" }
        };

                foreach (var item in departamentos)
                {
                    departamentosConTodos.Add(item.Key, item.Value);
                }

                cbxDepartamento1.DataSource = new BindingSource(departamentosConTodos, null);
                cbxDepartamento1.DisplayMember = "Value";
                cbxDepartamento1.ValueMember = "Key";
                cbxDepartamento1.SelectedValue = -1; // "Todos" seleccionado por defecto
            }
            else
            {
                MessageBox.Show("No se encontraron departamentos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxDepartamento1.Enabled = false;
            }
        }

        private void PoblaComboEstatus()
        {
            Dictionary<int, string> list_estadoL = new Dictionary<int, string>
    {
        { -1, "Todos" },    // Esta opción no aplica filtro
        { 1, "Activo" },
        { 0, "Inactivo" }
    };

            cbxEstatus.DataSource = new BindingSource(list_estadoL, null);
            cbxEstatus.DisplayMember = "Value";
            cbxEstatus.ValueMember = "Key";

            cbxEstatus.SelectedValue = -1; // Por defecto sin filtro
        }


        private void PoblaComboTipoContrato()
        {
            Dictionary<int, string> list_tipoCon = new Dictionary<int, string>
    {
        { -1, "Todos" },      // Clave -1 para no aplicar filtro
        { 1, "Fijo" },
        { 2, "Temporal" }
    };

            cbxTipoContrato.DataSource = new BindingSource(list_tipoCon, null);
            cbxTipoContrato.DisplayMember = "Value";
            cbxTipoContrato.ValueMember = "Key";

            cbxTipoContrato.SelectedValue = -1; // Selecciona "Todos" por defecto
        }

        private void MostrarContratosSinFiltro()
        {
            try
            {
                // Obtener todos los contratos sin filtros (usando nullables)
                List<Contrato> contratos = _contratosController.ObtenerContratosFiltrados(
                    null,       // matrícula
                    0,          // tipoContrato (0 = sin filtro)
                    -1,         // estatus (-1 = sin filtro)
                    0,          // departamento (0 = sin filtro)
                    (DateTime?)null, // fechaInicio
                    (DateTime?)null  // fechaFin
                );

                dataGridUsuarios.Rows.Clear();

                foreach (var c in contratos)
                {
                    dataGridUsuarios.Rows.Add(
                        c.Id_Contrato,
                        c.Matricula,
                        c.NombreEmpleado,
                        c.NombreDepartamento,
                        c.FechaInicio.ToShortDateString(),
                        c.FechaFin.ToShortDateString(),
                        c.HoraEntrada.ToString(@"hh\:mm"),
                        c.HoraSalida.ToString(@"hh\:mm"),
                        c.Sueldo.ToString("C2"),
                        c.Descripcion,
                        c.Estatus ? "Activo" : "Inactivo",
                        c.NombreTipoContrato 
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar contratos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void CargarDepartamentosDesdeBD()
        {
            try
            {
                var lista = _departamentoController.ObtenerTodosLosDepartamentos();

                if (lista != null && lista.Count > 0)
                {
                    departamentos = lista.ToDictionary(d => d.IdDepartamento, d => d.NombreDepartamento);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar departamentos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGenerar1_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();

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
                matricula = null;
            }

            // Validaciones de selección
            int tipoContrato = cbxTipoContrato.SelectedValue != null ? (int)cbxTipoContrato.SelectedValue : -1;
            int estatus = cbxEstatus.SelectedValue != null ? (int)cbxEstatus.SelectedValue : -1;
            int departamento = cbxDepartamento1.SelectedValue != null ? (int)cbxDepartamento1.SelectedValue : -1;

            // Evaluar si el filtro de fechas debe aplicarse
            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;

            if (!(dtpFechaInicio.Value.Date == DateTime.Today && dtpFechaFin.Value.Date == DateTime.Today))
            {
                fechaInicio = dtpFechaInicio.Value.Date;
                fechaFin = dtpFechaFin.Value.Date;
            }

         
            // Obtener contratos con los filtros
            List<Contrato> contratos = _contratosController.ObtenerContratosFiltrados(matricula, tipoContrato, estatus, departamento, fechaInicio, fechaFin);

            // Mostrar en tabla
            dataGridUsuarios.Rows.Clear();

            foreach (var c in contratos)
            {
                dataGridUsuarios.Rows.Add(
                    c.Id_Contrato,
                    c.Matricula,
                    c.NombreEmpleado,
                    c.NombreDepartamento,
                    c.FechaInicio.ToShortDateString(),
                    c.FechaFin.ToShortDateString(),
                    c.HoraEntrada.ToString(@"hh\:mm"),
                    c.HoraSalida.ToString(@"hh\:mm"),
                    c.Sueldo.ToString("C"),
                    c.Descripcion,
                    c.Estatus ? "Activo" : "Inactivo",
                    c.NombreTipoContrato
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


