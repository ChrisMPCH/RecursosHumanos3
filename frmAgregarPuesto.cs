using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using RecursosHumanos.Bussines;
using RecursosHumanos.Controller;
using RecursosHumanos.Models;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmAgregarPuesto : Form
    {
        private Guna2GradientPanel pnlCambiante; // Variable para almacenar el panel

        // Constructor que recibe la referencia del panel para poder limpiarlo
        public frmAgregarPuesto(Guna2GradientPanel pnlCambiante)
        {
            InitializeComponent();
            this.pnlCambiante = pnlCambiante; // Guardamos la referencia del panel
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            InicializarCampos();
        }

        private void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre");
            Formas.ConfigurarTextBox(txtDescripcion, "Ingrese descripción del puesto");
        }


        private bool DatosVacios()
        {
            if (txtNombre.Text == "" || txtNombre.Text == "Ingrese nombre" ||
            txtDescripcion.Text == "" || txtDescripcion.Text == "Ingrese descripción del puesto")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GenerarPuesto())
            {
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool GenerarPuesto()
        {
            if (!DatosVacios())
            {
                return false;
            }

            PuestoController controller = new PuestoController();

            Puesto nuevoPuesto = new Puesto
            {
                NombrePuesto = txtNombre.Text.Trim(),
                DescripcionPuesto = txtDescripcion.Text.Trim(),
                Estatus=true
            };

            var (idPuesto, mensaje) = controller.RegistrarPuesto(nuevoPuesto);
            if (idPuesto > 0)
            {
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MDIRecursosHumanos mid = new MDIRecursosHumanos();
                mid.BloquearBotonesMenu();

                InicializarCampos();
                DesbloquearCampos(true);
                Form frmGuardarInf = new frmGuardarInformacion();
                Formas.abrirPanelForm(frmGuardarInf, pnlCambiante);
                return true;
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmListadoPuestos formListado = new frmListadoPuestos(pnlCambiante);
            Formas.abrirPanelForm(formListado, pnlCambiante);
        }

        private void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            ofdArchivo.Title = "Seleccionar archivo de Excel";
            ofdArchivo.Filter = "Archivos de Excel (*.xlsx;*.xls)|*.xlsx;*.xls";
            ofdArchivo.InitialDirectory = "C:\\";
            ofdArchivo.FilterIndex = 1;
            ofdArchivo.RestoreDirectory = true;

            if (ofdArchivo.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofdArchivo.FileName;
                string extension = Path.GetExtension(filePath).ToLower();

                if (extension == ".xlsx" || extension == ".xls")
                {
                    MessageBox.Show("Archivo válido: " + filePath, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un archivo de Excel válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void DesbloquearCampos(bool desbloquear)
        {
            txtNombre.Enabled = desbloquear;
            txtDescripcion.Enabled = desbloquear;
        }
    }
}