using RecursosHumanos.Utilities;
using RecursosHumanos.Controller;
using RecursosHumanos.Controllers;
using RecursosHumanos.Models;

namespace RecursosHumanos.View
{
    public partial class frmListaAsistencias : Form
    {
        private List<Asistencia> asistencias = new List<Asistencia>();
        private readonly AsistenciaController asistenciaController = new AsistenciaController();

        public frmListaAsistencias()
        {
            InitializeComponent();
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            IniciarTabla();           // Configuramos el DataGridView
            CargarAsistencias();      // Cargamos todas las asistencias sin filtro
        }

        private void IniciarTabla()
        {
            // Configuración del estilo
            Formas.ConfigurarEstiloDataGridView(dgvAsistencias);

            // Desactivar auto-generación de columnas
            dgvAsistencias.AutoGenerateColumns = false;

            // Limpiar columnas previas
            dgvAsistencias.Columns.Clear();

            // Agregar columnas personalizadas
            dgvAsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Matrícula",
                DataPropertyName = "Matricula",
                Width = 120
            });
            dgvAsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 250
            });
            dgvAsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha",
                DataPropertyName = "FechaAsistencia",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            dgvAsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Hora Entrada",
                DataPropertyName = "HoraEntrada",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "HH:mm:ss" }
            });
            dgvAsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Hora Salida",
                DataPropertyName = "HoraSalida",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "HH:mm:ss" }
            });
        }

        // Carga todas las asistencias sin aplicar filtros
        private void CargarAsistencias()
        {
            try
            {
                var asistencias = asistenciaController.ObtenerAsistenciasConEmpleado();

                var listaMostrar = asistencias.Select(a => new
                {
                    Matricula = a.Matricula,
                    Nombre = a.NombreEmpleado,
                    FechaAsistencia = a.FechaAsistencia,
                    HoraEntrada = a.HoraEntrada,
                    HoraSalida = a.HoraSalida.HasValue ? a.HoraSalida.Value : (TimeSpan?)null
                }).ToList();

                dgvAsistencias.DataSource = listaMostrar;

                if (listaMostrar.Count == 0)
                    MessageBox.Show("No se encontraron asistencias.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asistencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sobrecarga para evitar parámetro obligatorio en llamada inicial
        private void CargarAsistenciasConFiltros()
        {
            string matriculaFiltro = txtMatricula.Text.Trim();
            DateTime? fechaFiltro = dtpFecha.Checked ? dtpFecha.Value.Date : (DateTime?)null;

            CargarAsistenciasConFiltros(matriculaFiltro, fechaFiltro);
        }

        // Método principal que carga las asistencias con filtro de matrícula y fecha
        private void CargarAsistenciasConFiltros(string matriculaFiltro, DateTime? fechaFiltro)
        {
            try
            {
                var asistencias = asistenciaController.ObtenerAsistenciasConEmpleado();

                var filtradas = asistencias.Where(a =>
                    (string.IsNullOrEmpty(matriculaFiltro) || a.Matricula.ToLower().Contains(matriculaFiltro.ToLower())) &&
                    (!fechaFiltro.HasValue || a.FechaAsistencia.Date == fechaFiltro.Value)
                )
                .Select(a => new
                {
                    Matricula = a.Matricula,
                    Nombre = a.NombreEmpleado,
                    FechaAsistencia = a.FechaAsistencia,
                    HoraEntrada = a.HoraEntrada,
                    HoraSalida = a.HoraSalida.HasValue ? a.HoraSalida.Value : (TimeSpan?)null
                }).ToList();

                dgvAsistencias.DataSource = filtradas;

                if (filtradas.Count == 0)
                    MessageBox.Show("No se encontraron asistencias con los filtros aplicados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asistencias con filtros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarAsistenciasConFiltros();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtMatricula.Clear();
            dtpFecha.Value = DateTime.Today;
            dtpFecha.Checked = false; // Si usas Checked para habilitar o no la fecha
            CargarAsistencias();
        }

      
    }
}
