using RecursosHumanos.Controller;
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
    public partial class frmGuardarInformacion : Form
    {
        public frmGuardarInformacion()
        {
            InitializeComponent();
        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            Form frmRegistroUsuario = new frmRegistroUsuario();
            Formas.abrirPanelForm(frmRegistroUsuario, frmRegistroPersonas.pnlCambiante);

        }

        private void btnRegitrarEmpleado_Click(object sender, EventArgs e)
        {
            Form frmRegistroEmpleado = new frmRegistroEmpleado();
            Formas.abrirPanelForm(frmRegistroEmpleado, frmRegistroPersonas.pnlCambiante);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (frmRegistroPersonas.IdPersonaRegistrada == -1)
            {
                frmRegistroPersonas.InicializarCampos();
                return;
            }
            PersonasController personasController = new PersonasController();
            var exito = personasController.CancelarRegistroPersona(frmRegistroPersonas.IdPersonaRegistrada);
            if (!exito)
            {
                MessageBox.Show("No se canceló el registro, no se pudo eliminar la persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmRegistroPersonas.IdPersonaRegistrada = -1;
            MessageBox.Show("Se canceló el registro y se eliminó la persona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmRegistroPersonas.DesbloquearCampos(true);
            frmRegistroPersonas.InicializarCampos();
            MDIRecursosHumanos.DesbloquearBotonesMenu();
        }
    }
}
