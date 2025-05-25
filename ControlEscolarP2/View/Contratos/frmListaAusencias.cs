using RecursosHumanos.Utilities;
using RecursosHumanos.Controller;
using RecursosHumanos.Controllers;
using RecursosHumanos.Models;

namespace RecursosHumanos.View
{
    public partial class frmListaAusencias : Form
    {
        private List<Ausencia> ausencias = new List<Ausencia>();
        private readonly AusenciaController ausenciaController = new AusenciaController();


        public frmListaAusencias()
        {
            InitializeComponent();
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            IniciarTabla();           // Configuramos el DataGridView
            CargarAusencias();      // Cargamos todas las ausencias sin filtro
        }

        private void IniciarTabla()
        {
            // Configuración del estilo (igual que antes)
            Formas.ConfigurarEstiloDataGridView(dgvAusencias);

            dgvAusencias.AutoGenerateColumns = false;
            dgvAusencias.Columns.Clear();

            dgvAusencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Matrícula",
                DataPropertyName = "Matricula",
                Width = 120
            });
            dgvAusencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 250
            });
            dgvAusencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Ausencia",
                DataPropertyName = "FechaAusencias",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            dgvAusencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Motivo",
                DataPropertyName = "MotivoAusencia",
                Width = 250
            });
        }


        // Carga todas las ausencias sin aplicar filtros
        private void CargarAusencias()
        {
            try
            {
                var ausencias = ausenciaController.ObtenerAusencias();

                var listaMostrar = ausencias.Select(a => new
                {
                    Matricula = a.Matricula,
                    Nombre = a.Nombre,
                    FechaAusencias = a.FechaAusencias,
                    MotivoAusencia = a.MotivoAusencia
                }).ToList();

                dgvAusencias.DataSource = listaMostrar;

                if (listaMostrar.Count == 0)
                    MessageBox.Show("No se encontraron ausencias.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ausencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Método principal que carga las ausencias con filtro de matrícula y fecha
        private void CargarAusenciasConFiltros()
        {
            string matriculaFiltro = txtMatricula.Text.Trim();
            DateTime? fechaFiltro = dtpFecha.Checked ? dtpFecha.Value.Date : (DateTime?)null;

            try
            {
                var ausencias = ausenciaController.ObtenerAusencias();

                var filtradas = ausencias.Where(a =>
                    (string.IsNullOrEmpty(matriculaFiltro) || a.Matricula.ToLower().Contains(matriculaFiltro.ToLower())) &&
                    (!fechaFiltro.HasValue || a.FechaAusencias.Date == fechaFiltro.Value)
                )
                .Select(a => new
                {
                    Matricula = a.Matricula,
                    Nombre = a.Nombre,
                    FechaAusencias = a.FechaAusencias,
                    MotivoAusencia = a.MotivoAusencia
                }).ToList();

                dgvAusencias.DataSource = filtradas;

                if (filtradas.Count == 0)
                    MessageBox.Show("No se encontraron ausencias con los filtros aplicados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ausencias con filtros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarAusenciasConFiltros();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtMatricula.Clear();
            dtpFecha.Value = DateTime.Today;
            dtpFecha.Checked = false; // Si usas Checked para habilitar o no la fecha
            CargarAusencias();
        }

      
    }
}
