using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursosHumanos.Utilities;
using RecursosHumanosCore.Bussines;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Model;

namespace RecursosHumanos.View.MDI
{
    public partial class ApiRecibida : Form
    {
        private ContratoController _contratosController = new ContratoController();
        public ApiRecibida()
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();
           
            if (!string.IsNullOrEmpty(matricula) && matricula != "Ingresa la matricula")
            {
                if (!EmpleadoNegocio.EsNoMatriculaValido(matricula))
                {
                    MessageBox.Show("Matricula vacía", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                matricula = null;
            }


            // Evaluar si el filtro de fechas debe aplicarse
            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;

            if (!(dtpFechaInicio1.Value.Date == DateTime.Today && dtpFechaFin1.Value.Date == DateTime.Today))
            {
                fechaInicio = dtpFechaInicio1.Value.Date;
                fechaFin = dtpFechaFin1.Value.Date;
            }


            // Obtener contratos con los filtros
            List<Contrato> contratos = _contratosController.ObtenerContratosAPI(matricula, fechaInicio, fechaFin);

            // Mostrar en tabla
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

            MessageBox.Show(
                contratos.Count > 0
                    ? $"Se encontraron {contratos.Count} contrato(s) con los filtros aplicados."
                    : "No se encontraron contratos con los filtros aplicados.",
                "Resultado del reporte",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
