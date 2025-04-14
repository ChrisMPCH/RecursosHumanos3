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

namespace RecursosHumanos.View
{
    public partial class frmPuestos : Form
    {
        public frmPuestos()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            iniciarPaneles();
            frmListadoPuestos formLeer = new frmListadoPuestos(pnlCambiante);
            Formas.abrirPanelForm(formLeer, pnlCambiante);
        }

        private void iniciarPaneles()
        {
            pnlCambiante.Controls.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarPuesto
            frmAgregarPuesto formAgregar = new frmAgregarPuesto(pnlCambiante);
            Formas.abrirPanelForm(formAgregar, pnlCambiante);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarDepartamento
            frmEliminarPuesto formEliminar = new frmEliminarPuesto(pnlCambiante);
            Formas.abrirPanelForm(formEliminar, pnlCambiante);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarDepartamento
            frmActualizarPuesto formActualizar = new frmActualizarPuesto(pnlCambiante);
            Formas.abrirPanelForm(formActualizar, pnlCambiante);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarDepartamento
            frmListadoPuestos formLeer = new frmListadoPuestos(pnlCambiante);
            Formas.abrirPanelForm(formLeer, pnlCambiante);
        }

        private void lblInfoUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}