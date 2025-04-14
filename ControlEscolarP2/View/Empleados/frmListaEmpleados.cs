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
    public partial class frmListaEmpleados : Form
    {
        public frmListaEmpleados()
        {
            InitializeComponent();
            InicializarVentana();
        }
        public void InicializarVentana()
        {
            PoblaComboDepartamento();
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

        private void PoblaComboDepartamento()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_departamentos = new Dictionary<int, string>
            {
                { 1, "Departamento 1" },
                { 2, "Departamento 2" },
                { 3, "Departamento 3" }
            };

            //Asignar los valores al comboBox
            cbxDepartamento.DataSource = new BindingSource(list_departamentos, null);
            cbxDepartamento.DisplayMember = "Value"; //lo que se mestra
            cbxDepartamento.ValueMember = "Key"; //lo que se guarda como SelectedValue

            cbxDepartamento.SelectedIndex = 1;

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbxDepartamento.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un departamento para buscar", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        
    }
}
