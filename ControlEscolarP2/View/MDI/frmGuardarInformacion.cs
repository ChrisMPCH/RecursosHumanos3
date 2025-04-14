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
            Form frmGuardarInf = new frmGuardarInformacion();
            Formas.abrirPanelForm(frmGuardarInf, frmRegistroPersonas.pnlCambiante);
            frmRegistroPersonas.InicializarCampos();
        }
    }
}
