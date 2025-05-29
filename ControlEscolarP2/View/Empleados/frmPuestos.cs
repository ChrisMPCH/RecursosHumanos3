using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecursosHumanosCore.Bussines;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmPuestos : Form
    {
        public frmPuestos()
        {
            InitializeComponent();
            InicializarVentana();
            VerificarPermisos();
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

        /// <summary>
        /// Verifica los permisos del usuario para habilitar o deshabilitar los botones de registro.
        /// </summary>
        private void VerificarPermisos()
        {
            var permisosUsuario = MDIRecursosHumanos.permisosUsuario;

            if (permisosUsuario.Contains(4) || permisosUsuario.Contains(35)) // Agregar puesto  
            {
                btnAgregar.Enabled = true;
            }
            if (permisosUsuario.Contains(5) || permisosUsuario.Contains(35)) // Editar puesto  
            {
                btnActualizar.Enabled = true;
            }
            if (permisosUsuario.Contains(6) || permisosUsuario.Contains(35)) // Eliminar puesto  
            {
                btnEliminar.Enabled = true;
            }
        }

        private void pnFondo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlCambiante_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}