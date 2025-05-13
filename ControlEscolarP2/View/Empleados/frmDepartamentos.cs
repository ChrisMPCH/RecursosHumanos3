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
    public partial class frmDepartamentos : Form
    {
        public frmDepartamentos()
        {
            InitializeComponent();
            InicializarVentana();
            VerificarPermisos();
        }

        public void InicializarVentana()
        {
            iniciarPaneles();
            frmListadoDepartamentos formLeer = new frmListadoDepartamentos(pnlCambiante);
            Formas.abrirPanelForm(formLeer, pnlCambiante);
        }

        private void iniciarPaneles()
        {
            pnlCambiante.Controls.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarDepartamento
            frmAgregarDepartamento formAgregar = new frmAgregarDepartamento(pnlCambiante);
            Formas.abrirPanelForm(formAgregar, pnlCambiante);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarDepartamento
            frmEliminarDepartamento formEliminar = new frmEliminarDepartamento(pnlCambiante);
            Formas.abrirPanelForm(formEliminar, pnlCambiante);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarDepartamento
            frmActualizarDepartamento formActualizar = new frmActualizarDepartamento(pnlCambiante);
            Formas.abrirPanelForm(formActualizar, pnlCambiante);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // Pasamos la referencia del panel pnlCambiante al formulario frmAgregarDepartamento
            frmListadoDepartamentos formLeer = new frmListadoDepartamentos(pnlCambiante);
            Formas.abrirPanelForm(formLeer, pnlCambiante);
        }

        /// <summary>
        /// Verifica los permisos del usuario para habilitar o deshabilitar los botones de registro.
        /// </summary>
        private void VerificarPermisos()
        {
            var permisosUsuario = MDIRecursosHumanos.permisosUsuario;

            if (permisosUsuario.Contains(50)) // Agregar departamento  
            {
                btnAgregar.Enabled = true;
            }
            if (permisosUsuario.Contains(51) || permisosUsuario.Contains(56)) // Editar departamento  
            {
                btnActualizar.Enabled = true;
            }
            if (permisosUsuario.Contains(52) || permisosUsuario.Contains(56)) // Eliminar departamento  
            {
                btnEliminar.Enabled = true;
            }
            if (permisosUsuario.Contains(53) || permisosUsuario.Contains(56)) // Consultar departamento  
            {
                btnConsultar.Enabled = true;
            }
        }

    }
}