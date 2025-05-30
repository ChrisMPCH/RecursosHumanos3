using Guna.UI2.WinForms;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Models;
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
    public partial class frmListadoPuestos : Form
    {

        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo

        public frmListadoPuestos(Guna2GradientPanel pnlCambiante)
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
            Formas.ConfigurarEstiloDataGridView(dgvPuestos);
        }


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Limpiar el panel de frmListadoPuestos al hacer clic en Regresar
            Formas.limpiarPanel(pnlCambiante);
        }

        private void FormLoad()
        {
            CargarPuestosEnTabla();
        }
        List<Puesto> listaPuestos;

        private void CargarPuestosEnTabla()
        {
            try
            {
                PuestoController controller = new PuestoController();
                listaPuestos = controller.ObtenerTodosLosPuestos(soloActivos: false); // Mostrar todos, activos e inactivos

                // Transformar la lista para personalizar cómo se muestran los datos
                var datosMostrar = listaPuestos.Select(p => new
                {
                    ID = p.IdPuesto,
                    Nombre = p.NombrePuesto,
                    Descripcion = p.DescripcionPuesto,
                    Estatus = p.Estatus ? "Activo" : "Inactivo"
                }).ToList();

                dgvPuestos.DataSource = datosMostrar;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar puestos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExportarPuestos_Click(object sender, EventArgs e)
        {
            PuestoController controller = new PuestoController();

            var (exito, mensaje) = controller.ExportarPuestosExcel(true);

            if (exito)
            {
                MessageBox.Show(mensaje, "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(mensaje, "Exportación Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
