using RecursosHumanos.Services;
using RecursosHumanos.Utilities;
using RecursosHumanosCore.Bussines;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursosHumanos.View.MDI
{
    public partial class frmApiRecibida : Form
    {
        private ContratoController _contratosController = new ContratoController();
        private readonly ApiService _apiService = new ApiService();

        public frmApiRecibida()
        {
            InitializeComponent();
            InicializarCampos();
            dtpFechaInicio1.Value = DateTime.Now;
            dtpFechaFin1.Value = DateTime.Now;
            IniciarTabla();
        }

        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula, "Ingresa tu matricula");

        }
        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(tblEmpleado); // Configurar el estilo del DataGridView
            Formas.ConfigurarEstiloDataGridView(tblApiRecibida); // Configurar el estilo del DataGridView
            ConfigurarColumnas(); // Agregar columnas personalizadas
            ConfigurarColumnasE();
        }
        private void ConfigurarColumnas()
        {
            tblApiRecibida.Columns.Clear();
            tblApiRecibida.AutoGenerateColumns = false;

            tblApiRecibida.Columns.Add("Id_Contrato", "ID Contrato");
            tblApiRecibida.Columns.Add("Matricula", "Matrícula");
            tblApiRecibida.Columns.Add("NombreEmpleado", "Empleado");
            tblApiRecibida.Columns.Add("Correo", "Correo");
            tblApiRecibida.Columns.Add("Telefono", "Teléfono");
            tblApiRecibida.Columns.Add("NombreDepartamento", "Departamento");
            tblApiRecibida.Columns.Add("NombrePuesto", "Puesto");
            tblApiRecibida.Columns.Add("FechaInicio", "Fecha Inicio");
            tblApiRecibida.Columns.Add("FechaFin", "Fecha Fin");
            tblApiRecibida.Columns.Add("HoraEntrada", "Hora Entrada");
            tblApiRecibida.Columns.Add("HoraSalida", "Hora Salida");
            tblApiRecibida.Columns.Add("Sueldo", "Salario");
            tblApiRecibida.Columns.Add("Descripcion", "Descripción");
            tblApiRecibida.Columns.Add("Estatus", "Estatus");
            tblApiRecibida.Columns.Add("NombreTipoContrato", "Tipo de Contrato");

            tblApiRecibida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnasE()
        {
            tblEmpleado.Columns.Clear();
            tblEmpleado.AutoGenerateColumns = false;

            tblEmpleado.Columns.Add("Id_Contrato", "ID Contrato");
            tblEmpleado.Columns.Add("Matricula", "Matrícula");
            tblEmpleado.Columns.Add("NombreEmpleado", "Empleado");
            tblEmpleado.Columns.Add("Correo", "Correo");
            tblEmpleado.Columns.Add("Telefono", "Teléfono");
            tblEmpleado.Columns.Add("NombreDepartamento", "Departamento");
            tblEmpleado.Columns.Add("NombrePuesto", "Puesto");
            tblEmpleado.Columns.Add("FechaInicio", "Fecha Inicio");
            tblEmpleado.Columns.Add("FechaFin", "Fecha Fin");
            tblEmpleado.Columns.Add("HoraEntrada", "Hora Entrada");
            tblEmpleado.Columns.Add("HoraSalida", "Hora Salida");
            tblEmpleado.Columns.Add("Sueldo", "Salario");
            tblEmpleado.Columns.Add("Descripcion", "Descripción");
            tblEmpleado.Columns.Add("Estatus", "Estatus");
            tblEmpleado.Columns.Add("NombreTipoContrato", "Tipo de Contrato");

            tblEmpleado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();

            if (string.IsNullOrEmpty(matricula) || matricula == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor ingresa una matrícula válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;
            if (!(dtpFechaInicio1.Value.Date == DateTime.Today && dtpFechaFin1.Value.Date == DateTime.Today))
            {
                fechaInicio = dtpFechaInicio1.Value.Date;
                fechaFin = dtpFechaFin1.Value.Date;
            }

            // Obtener contratos
            List<Contrato> contratos = new List<Contrato>();
            try
            {
                contratos = _contratosController.ObtenerContratosAPI(matricula, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener contratos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tblEmpleado.Rows.Clear();
            foreach (var c in contratos)
            {
                tblEmpleado.Rows.Add(
                    c.Id_Contrato,
                    c.Matricula,
                    c.NombreEmpleado,
                    c.Correo,
                    c.Telefono,
                    c.NombreDepartamento,
                    c.NombrePuesto,
                    c.FechaInicio.ToShortDateString(),
                    c.FechaFin.ToShortDateString(),
                    c.HoraEntrada.ToString(@"hh\:mm"),
                    c.HoraSalida.ToString(@"hh\:mm"),
                    c.Sueldo.ToString("C"),
                    c.Estatus ? "Activo" : "Inactivo",
                    c.NombreTipoContrato
                );
            }

            // Obtener nóminas
            try
            {
                var nominas = await _apiService.ObtenerNominasPorEmpleadoAsync(matricula);

                tblApiRecibida.Rows.Clear();
                tblApiRecibida.Columns.Clear();

                tblApiRecibida.Columns.Add("IdNomina", "ID Nómina");
                tblApiRecibida.Columns.Add("FechaInicio", "Fecha Inicio");
                tblApiRecibida.Columns.Add("FechaFin", "Fecha Fin");
                tblApiRecibida.Columns.Add("EstadoPago", "Estado Pago");
                tblApiRecibida.Columns.Add("MontoTotal", "Monto Total");
                tblApiRecibida.Columns.Add("MontoLetras", "Monto en Letras");

                tblApiRecibida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (var n in nominas)
                {
                    tblApiRecibida.Rows.Add(
                        n.idNomina,
                        n.fechaInicio.ToShortDateString(),
                        n.fechaFin.ToShortDateString(),
                        n.estadoPago,
                        n.montoTotal.ToString("C"),
                        n.montoLetras
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las nóminas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


