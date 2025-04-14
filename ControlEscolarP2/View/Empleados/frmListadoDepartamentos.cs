using Guna.UI2.WinForms;
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
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridEmpleados);
            ConfigurarAnchoColumnas(300);
        }

        private void ConfigurarAnchoColumnas(int ancho)
        {
            foreach (DataGridViewColumn columna in dataGridEmpleados.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Limpiar el panel de frmDepartamentos al hacer clic en Cancelar
            Formas.limpiarPanel(pnlCambiante);
        }
    }
}
