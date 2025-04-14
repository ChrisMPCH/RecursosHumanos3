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
using static System.Net.Mime.MediaTypeNames;

namespace RecursosHumanos.View
{
    public partial class frmActualizarEmpleado : Form
    {
        public frmActualizarEmpleado()
        {
            InitializeComponent();
            InicializarVentana();
        }


        public void InicializarVentana()
        {
            PoblaComboDepartamento();
            PoblaComboPuesto();
            PoblaComboEstatus();
            PoblaComboGenero();
            InicializarCampos();
            dtpFechaBaja.Value = DateTime.Now;
        }


        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula, "Ingrese matrícula");
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre(s)");
            Formas.ConfigurarTextBox(txtApellidoP, "Ingrese apellido paterno");
            Formas.ConfigurarTextBox(txtApellidoM, "Ingrese apellido materno");
            Formas.ConfigurarTextBox(txtRFC, "Ingrese RFC");
            Formas.ConfigurarTextBox(txtCURP, "Ingrese CURP");
            Formas.ConfigurarTextBox(txtCorreo, "Ingrese correo electrónico");
            Formas.ConfigurarTextBox(txtDireccion, "Ingrese direccion");
            Formas.ConfigurarTextBox(txtTelefono, "Ingrese teléfono");
        }

        private void PoblaComboEstatus()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                { 1, "Activo" },
                { 2, "Inactivo" }
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

        private void PoblaComboGenero()
        {
            //Crear un diccionario con los valores
            Dictionary<int, string> list_puestos = new Dictionary<int, string>
            {
                { 1, "Femenino" },
                { 2, "Masculino" }
            };

            //Asignar los valores al comboBox
            cbxGenero.DataSource = new BindingSource(list_puestos, null);
            cbxGenero.DisplayMember = "Value"; //lo que se mestra
            cbxGenero.ValueMember = "Key"; //lo que se guarda como SelectedValue

            cbxGenero.SelectedIndex = 1;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (BuscarEmpleado())
            {
                MessageBox.Show("Empleado encontrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Empleado no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool BuscarEmpleado()
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                MessageBox.Show("Ingrese una matrícula para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida. Verifique el formato.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GuardarEmpleado())
            {
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (!EmpleadoNegocio.ValidarFechas(dtpFechaIngreso.Value, dtpFechaBaja.Value))
            {
                MessageBox.Show("La fecha de ingreso debe ser menor que la fecha de baja.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private bool GuardarEmpleado()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!DatosValidos())
            {
                return false;
            }
            return true;
        }

        private bool DatosValidos()
        {
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsEmailValido(txtCorreo.Text.Trim()))
            {
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsTelefonoValido(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("Teléfono inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsCURPValido(txtCURP.Text.Trim()))
            {
                MessageBox.Show("CURP inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsRFCValido(txtRFC.Text.Trim())) // Se corrigió el error aquí
            {
                MessageBox.Show("RFC inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!PersonasNegocio.EsGeneroValido((int)cbxGenero.SelectedValue))
            {
                MessageBox.Show("Género inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool DatosVacios()
        {
            if (txtMatricula.Text == "" || txtMatricula.Text == "Ingrese matrícula" ||
             txtNombre.Text == "" || txtNombre.Text == "Ingrese nombre(s)" ||
             txtApellidoP.Text == "" || txtApellidoP.Text == "Ingrese apellido paterno" ||
             txtApellidoM.Text == "" || txtApellidoM.Text == "Ingrese apellido materno" ||
             txtRFC.Text == "" || txtRFC.Text == "Ingrese RFC" ||
             txtCURP.Text == "" || txtCURP.Text == "Ingrese CURP" ||
             txtCorreo.Text == "" || txtCorreo.Text == "Ingrese correo electrónico" ||
             txtDireccion.Text == "" || txtDireccion.Text == "Ingrese direccion" ||
             txtTelefono.Text == "" || txtTelefono.Text == "Ingrese teléfono" ||
             cbxDepartamento.Text == "" || cbxPuesto.Text == "" || cbxEstatus.Text == "" ||
             dtpFechaBaja.Text == "" || cbxGenero.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void lblTelefono_Click(object sender, EventArgs e)
        {

        }
    }
}
