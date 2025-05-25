using Guna.UI2.WinForms;
using RecursosHumanos.Controller;
using RecursosHumanos.Model;
using RecursosHumanos.Models;
using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursosHumanos.View
{
    public partial class frmListadoDepartamentos : Form
    {

        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo

        public frmListadoDepartamentos(Guna2GradientPanel pnlCambiante)
        {
            InitializeComponent();
            this.pnlCambiante = pnlCambiante; // Guardamos la referencia del panel
            InicializarVentana();
        }

        public void InicializarVentana()
        {

            IniciarTabla();
            FormLoad();
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dgvDepartamentos);
        }

        private void ConfigurarAnchoColumnas(int ancho)
        {
            foreach (DataGridViewColumn columna in dgvDepartamentos.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Limpiar el panel de frmDepartamentos al hacer clic en Cancelar
            Formas.limpiarPanel(pnlCambiante);
        }
        List<Departamento> listaDepartamentos;
        private void FormLoad()
        {
            CargarDepartamentosEnTabla();
        }

        private void CargarDepartamentosEnTabla()
        {
            try
            {
                DepartamentoController controller = new DepartamentoController();
                listaDepartamentos = controller.ObtenerTodosLosDepartamentos(); // Mostrar todos, activos e inactivos

                // Transformar la lista para personalizar cómo se muestran los datos
                var datosMostrar = listaDepartamentos.Select(d => new
                {
                    ID = d.IdDepartamento,
                    Nombre = d.NombreDepartamento,
                    Ubicacion = d.Ubicacion,
                    Telefono = d.TelefonoDepartamento,
                    Correo = d.EmailDepartamento,
                    Estatus = d.Estatus ? "Activo" : "Inactivo"
                }).ToList();

                dgvDepartamentos.DataSource = datosMostrar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar departamentos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
           DepartamentoController controller = new DepartamentoController();

            // Exportar a Excel
            controller.ExportarDepartamentosExcel(true);
        }
    }
}
