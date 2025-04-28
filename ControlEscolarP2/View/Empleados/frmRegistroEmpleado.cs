using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RecursosHumanos.Bussines;
using RecursosHumanos.Controller;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using RecursosHumanos.Data;
using RecursosHumanos.Models;

namespace RecursosHumanos.View
{
    public partial class frmRegistroEmpleado : Form
    {
        private DepartamentoDataAccess _departamentoDataAccess = new DepartamentoDataAccess();
        private PuestoDataAccess _puestoDataAccess = new PuestoDataAccess();
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
            // Fecha de hoy
            dtpFechaIngreso.Value = DateTime.Now;
            InicializarCampos();
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula, "Ingrese matrícula");
        }

        private void PoblaComboEstatus()
        {
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                { 1, "Activo" },
                { 0, "Inactivo" }
            };

            cbxEstatus.DataSource = new BindingSource(list_estatus, null);
            cbxEstatus.DisplayMember = "Value";
            cbxEstatus.ValueMember = "Key";

            cbxEstatus.SelectedIndex = 1;
        }

        private void PoblaComboDepartamento()
        {
            Dictionary<int, string> list_departamentos = new Dictionary<int, string>();
            List<Departamento> departamentos = _departamentoDataAccess.ObtenerTodosLosDepartamentos();

            foreach (var departamento in departamentos)
            {
                list_departamentos.Add(departamento.IdDepartamento, departamento.NombreDepartamento);
            }

            if (list_departamentos.Count > 0)
            {
                cbxDepartamento.DataSource = new BindingSource(list_departamentos, null);
                cbxDepartamento.DisplayMember = "Value";
                cbxDepartamento.ValueMember = "Key";
                cbxDepartamento.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No se encontraron departamentos.");
                cbxDepartamento.Enabled = false;
            }
        }

        private void PoblaComboPuesto()
        {
            Dictionary<int, string> list_puestos = new Dictionary<int, string>();
            List<Puesto> puestos = _puestoDataAccess.ObtenerTodosLosPuestos();

            foreach (var puesto in puestos)
            {
                list_puestos.Add(puesto.IdPuesto, puesto.NombrePuesto);
            }

            if (list_puestos.Count > 0)
            {
                cbxPuesto.DataSource = new BindingSource(list_puestos, null);
                cbxPuesto.DisplayMember = "Value";
                cbxPuesto.ValueMember = "Key";
                cbxPuesto.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No se encontraron puestos.");
                cbxPuesto.Enabled = false;
            }
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
            return false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GuardarEmpleado())  // Si el guardado es exitoso
            {
                MessageBox.Show("Empleado guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MDIRecursosHumanos.DesbloquearBotonesMenu();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hubo un error al guardar al empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool GuardarEmpleado()
        {
            if (frmRegistroPersonas.IdPersonaRegistrada <= 0)
            {
                MessageBox.Show("Primero debe registrar a una persona.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    Id_Persona = frmRegistroPersonas.IdPersonaRegistrada, // Aquí usamos el ID de la persona registrada
                    Matricula = txtMatricula.Text.Trim(),
                    Fecha_Ingreso = dtpFechaIngreso.Value,
                    Fecha_Baja = null,
                    Id_Departamento = Convert.ToInt32(cbxDepartamento.SelectedValue),
                    Id_Puesto = Convert.ToInt32(cbxPuesto.SelectedValue),
                    Estatus = Convert.ToInt16(cbxEstatus.SelectedValue)
                };

                EmpleadosController empleadosController = new EmpleadosController();
                var (exito, mensaje) = empleadosController.RegistrarEmpleado(nuevoEmpleado);

                if (!exito)
                {
                    MessageBox.Show(mensaje, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
