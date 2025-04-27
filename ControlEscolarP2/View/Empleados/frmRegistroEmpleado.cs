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
using RecursosHumanos.Controller;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmRegistroEmpleado : Form
    {
        public frmRegistroEmpleado()
        {
            InitializeComponent();
            InicializarVentana();
        }


        private void InicializarVentana()
        {
            PoblaComboDepartamento();
            PoblaComboPuesto();
            PoblaComboEstatus();
            //fecha de hoy
            dtpFechaIngreso.Value = DateTime.Now;
            InicializarCampos();
        }


        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula, "Ingrese matrícula");
        }

        private void PoblaComboEstatus()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                { 1, "Activo" },
                { 0, "Inactivo" }
            };

            //Asignar los valores al comboBox
            cbxEstatus.DataSource = new BindingSource(list_estatus, null);
            cbxEstatus.DisplayMember = "Value"; //lo que se mestra
            cbxEstatus.ValueMember = "Key"; //lo que se guarda como SelectedValue

            cbxEstatus.SelectedIndex = 1;
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

        private void PoblaComboPuesto()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_puestos = new Dictionary<int, string>
            {
                { 1, "Puesto 1" },
                { 2, "Puesto 2" },
                { 3, "Puesto 3" }
            };

            //Asignar los valores al comboBox
            cbxPuesto.DataSource = new BindingSource(list_puestos, null);
            cbxPuesto.DisplayMember = "Value"; //lo que se mestra
            cbxPuesto.ValueMember = "Key"; //lo que se guarda como SelectedValue

            cbxPuesto.SelectedIndex = 1;

        }

        private bool DatosValidos()
        {

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool DatosVacios()
        {
            if (txtMatricula.Text == "" || txtMatricula.Text == "Ingrese matrícula" || cbxDepartamento.Text == ""
                || cbxPuesto.Text == "" || cbxEstatus.Text == "" || dtpFechaIngreso.Text == "")
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
            if (GuardarEmpleado())
            {
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool GuardarEmpleado()
        {
            if (!frmRegistroPersonas.GenerarPersona())
            {
                MessageBox.Show("Faltan los datos de la persona.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!DatosValidos())
            {
                return false;
            }

            try
            {
                Empleado nuevoEmpleado = new Empleado
                {
                    //DatosPersonales = frmRegistroPersonas.PersonaGenerada, // cuando se genere una persona
                    Matricula = txtMatricula.Text.Trim(),
                    Fecha_Ingreso = dtpFechaIngreso.Value,
                    Fecha_Baja = null,
                    Id_Departamento = Convert.ToInt32(cbxDepartamento.SelectedValue),
                    Id_Puesto = Convert.ToInt32(cbxPuesto.SelectedValue),
                    Motivo = null,
                    Estatus = Convert.ToInt16(cbxEstatus.SelectedValue)
                };

                var controlador = new EmpleadoController();
                var (idEmpleado, mensaje) = controlador.RegistrarEmpleado(nuevoEmpleado);

                if (idEmpleado <= 0)
                {
                    MessageBox.Show(mensaje, "Error al registrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form frmGuardarInf = new frmGuardarInformacion();
            Formas.abrirPanelForm(frmGuardarInf, frmRegistroPersonas.pnlCambiante);
        }

        private void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            ofdArchivo.Title = "Seleccionar archivo de Excel";
            ofdArchivo.Filter = "Archivos de Excel (*.xlsx;*.xls)|*.xlsx;*.xls";
            ofdArchivo.InitialDirectory = "C:\\";//carpeta inicial
            ofdArchivo.FilterIndex = 1; //selecciona el primer filtro por defecto
            ofdArchivo.RestoreDirectory = true; //mantiene la ultima ruta utilizada

            //showdialog espera una respuesta
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
    }
}
