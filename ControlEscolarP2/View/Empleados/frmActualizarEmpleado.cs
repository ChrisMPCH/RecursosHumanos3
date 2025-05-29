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
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Model;
using RecursosHumanos.Utilities;
using static System.Net.Mime.MediaTypeNames;
using RecursosHumanosCore.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmActualizarEmpleado : Form
    {
        private readonly EmpleadosController _empleadosController = new EmpleadosController();
        private readonly DepartamentoController _departamentoController = new DepartamentoController();
        private readonly PuestoController _puestoController = new PuestoController();

        private Dictionary<int, string> departamentos = new Dictionary<int, string>();
        private Dictionary<int, string> puestos = new Dictionary<int, string>();

        // NUEVO: Variables privadas para guardar los IDs al buscar
        private int _idEmpleado;
        private int _idPersona;

        public frmActualizarEmpleado()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            CargarDepartamentos();
            CargarPuestos();
            PoblarComboDepartamento();
            PoblarComboEstatus();
            PoblarComboPuesto();
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

        private void CargarDepartamentos()
        {
            var departamentosList = _departamentoController.ObtenerTodosLosDepartamentos();
            foreach (var departamento in departamentosList)
            {
                departamentos[departamento.IdDepartamento] = departamento.NombreDepartamento;
            }
        }

        private void CargarPuestos()
        {
            var puestosList = _puestoController.ObtenerTodosLosPuestos();
            foreach (var puesto in puestosList)
            {
                puestos[puesto.IdPuesto] = puesto.NombrePuesto;
            }
        }

        private void PoblarComboDepartamento()
        {
            if (departamentos.Count > 0)
            {
                cbxDepartamento.DataSource = new BindingSource(departamentos, null);
                cbxDepartamento.DisplayMember = "Value";
                cbxDepartamento.ValueMember = "Key";
                cbxDepartamento.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("No se encontraron departamentos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxDepartamento.Enabled = false;
            }
        }

        private void PoblarComboPuesto()
        {
            if (puestos.Count > 0)
            {
                cbxPuesto.DataSource = new BindingSource(puestos, null);
                cbxPuesto.DisplayMember = "Value";
                cbxPuesto.ValueMember = "Key";
                cbxPuesto.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("No se encontraron puestos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxPuesto.Enabled = false;
            }
        }

        private void PoblarComboEstatus()
        {
            List<string> estatus = new List<string> { "Activo", "Inactivo" };
            cbxEstatus.DataSource = estatus;
            cbxEstatus.SelectedIndex = 1;
        }

        private void PoblaComboGenero()
        {
            Dictionary<int, string> list_tipoCon = new Dictionary<int, string>
            {
                { 1, "Hombre" },
                { 2, "Mujer" }
            };
            cbxGenero.DataSource = new BindingSource(list_tipoCon, null);
            cbxGenero.DisplayMember = "Value";
            cbxGenero.ValueMember = "Key";
            cbxGenero.SelectedValue = 1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();

            if (string.IsNullOrWhiteSpace(matricula))
            {
                MessageBox.Show("Por favor, ingrese una matrícula válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Empleado empleado = _empleadosController.ObtenerEmpleadoPorMatricula(matricula);

                if (empleado == null)
                {
                    MessageBox.Show("Empleado no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    return;
                }

                // Asignar IDs internos
                _idEmpleado = empleado.Id_Empleado;
                _idPersona = empleado.DatosPersonales.Id_Persona;

                // Cargar datos personales
                txtNombre.Text = empleado.DatosPersonales.Nombre;
                txtApellidoP.Text = empleado.DatosPersonales.Ap_Paterno;
                txtApellidoM.Text = empleado.DatosPersonales.Ap_Materno;
                txtRFC.Text = empleado.DatosPersonales.RFC;
                txtCURP.Text = empleado.DatosPersonales.CURP;
                txtDireccion.Text = empleado.DatosPersonales.Direccion;
                txtTelefono.Text = empleado.DatosPersonales.Telefono;
                txtCorreo.Text = empleado.DatosPersonales.Email;
                dtpFechaNac.Value = empleado.DatosPersonales.Fecha_Nacimiento;
                cbxGenero.Text = empleado.DatosPersonales.Genero;

                // Cargar datos laborales
                cbxDepartamento.Text = empleado.Departamento;
                cbxPuesto.Text = empleado.Puesto;
                dtpFechaIngreso.Value = empleado.Fecha_Ingreso;
                dtpFechaBaja.Value = empleado.Fecha_Baja ?? DateTime.Today;
                cbxEstatus.Text = empleado.EstatusTexto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar al empleado:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellidoP.Text = "";
            txtApellidoM.Text = "";
            txtRFC.Text = "";
            txtCURP.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            cbxGenero.Text = "";
            dtpFechaNac.Value = DateTime.Today;

            cbxDepartamento.Text = "";
            cbxPuesto.Text = "";
            dtpFechaIngreso.Value = DateTime.Today;
            dtpFechaBaja.Value = DateTime.Today;
            cbxEstatus.Text = "";

            //Limpiar los IDs
            _idEmpleado = 0;
            _idPersona = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DatosVacios())
                {
                    MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!DatosValidos())
                {
                    return;
                }

                if (!EmpleadoNegocio.ValidarFechas(dtpFechaIngreso.Value, dtpFechaBaja.Value))
                {
                    MessageBox.Show("La fecha de ingreso debe ser menor que la fecha de baja.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // NUEVO: Construir objeto Empleado actualizado con IDs
                Empleado empleadoActualizado = new Empleado
                {
                    Id_Empleado = _idEmpleado,  // Asignar ID
                    Matricula = txtMatricula.Text.Trim(),
                    Fecha_Ingreso = dtpFechaIngreso.Value,
                    Fecha_Baja = dtpFechaBaja.Value,
                    Id_Departamento = ((KeyValuePair<int, string>)cbxDepartamento.SelectedItem).Key,
                    Id_Puesto = ((KeyValuePair<int, string>)cbxPuesto.SelectedItem).Key,
                    Estatus = (short)(cbxEstatus.Text == "Activo" ? 1 : 0),
                    DatosPersonales = new Persona
                    {
                        Id_Persona = _idPersona,  // Asignar ID
                        Nombre = txtNombre.Text.Trim(),
                        Ap_Paterno = txtApellidoP.Text.Trim(),
                        Ap_Materno = txtApellidoM.Text.Trim(),
                        RFC = txtRFC.Text.Trim(),
                        CURP = txtCURP.Text.Trim(),
                        Email = txtCorreo.Text.Trim(),
                        Direccion = txtDireccion.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Fecha_Nacimiento = dtpFechaNac.Value,
                        Genero = ((KeyValuePair<int, string>)cbxGenero.SelectedItem).Value
                    }
                };
                int idUsuario = LoggingManager.UsuarioActual.Id_Usuario;
                bool actualizado = _empleadosController.ActualizarEmpleado(empleadoActualizado, idUsuario);


                if (actualizado)
                {
                    MessageBox.Show("Empleado actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al actualizar el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            if (!PersonasNegocio.EsRFCValido(txtRFC.Text.Trim()))
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

    }
}
