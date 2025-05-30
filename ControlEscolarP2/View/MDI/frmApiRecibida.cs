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
            ConfigurarColumnas(); // Agregar columnas personalizadas
        }
        private void ConfigurarColumnas()
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

            try
            {
                var nominas = await _apiService.ObtenerNominasPorEmpleadoAsync(matricula);

                if (nominas.Count == 0)
                {
                    MessageBox.Show("No se encontraron nóminas para esta matrícula.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Configurar las columnas para esta vista si es diferente, o limpiar la tabla actual
                tblEmpleado.Rows.Clear();

                // Opcional: configurar columnas específicas para nóminas si quieres
                // Aquí ejemplo simple:
                tblEmpleado.Columns.Clear();
                tblEmpleado.Columns.Add("IdNomina", "ID Nómina");
                tblEmpleado.Columns.Add("FechaInicio", "Fecha Inicio");
                tblEmpleado.Columns.Add("FechaFin", "Fecha Fin");
                tblEmpleado.Columns.Add("EstadoPago", "Estado Pago");
                tblEmpleado.Columns.Add("MontoTotal", "Monto Total");
                tblEmpleado.Columns.Add("MontoLetras", "Monto en Letras");

                tblEmpleado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (var n in nominas)
                {
                    tblEmpleado.Rows.Add(
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
