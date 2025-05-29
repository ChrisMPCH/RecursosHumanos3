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
using RecursosHumanosCore.Controllers;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.View
{
    public partial class frmDias_calculados : Form
    {
        private readonly EmpleadosController _empleadosController = new EmpleadosController();
        private readonly AsistenciaController _asistenciaController = new AsistenciaController();

        private int _idEmpleado;
        private int _idPersona;

        public frmDias_calculados()
        {
            InitializeComponent();
            InicializaVentana();
        }

        private void InicializaVentana()
        {
            InicializarCampos();
            // Configuramos el placeholder para el campo de matrícula
            Formas.ConfigurarTextBox(txtMatricula, "Ingresa tu matricula");
        }

        private void InicializarCampos()
        {
            txtNombreCompleto.Text = "";
            txtCurp.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtHorasTrabajadas.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();

            if (string.IsNullOrWhiteSpace(matricula) || matricula == "Ingresa tu matricula")
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

                if (empleado.Estatus == 0) // 0 = Inactivo
                {
                    MessageBox.Show("El empleado está inactivo y no puede registrar asistencia.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                    return;
                }

                _idEmpleado = empleado.Id_Empleado;
                _idPersona = empleado.DatosPersonales.Id_Persona;

                txtNombreCompleto.Text = empleado.DatosPersonales.Nombre;
                txtCurp.Text = empleado.DatosPersonales.RFC;
                txtTelefono.Text = empleado.DatosPersonales.Telefono;
                txtCorreo.Text = empleado.DatosPersonales.Email;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar al empleado:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombreCompleto.Text = "";
            txtCurp.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";

            _idEmpleado = 0;
            _idPersona = 0;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text) || txtMatricula.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EmpleadoNegocio.ValidarFechas(dtpFechaInicio1.Value, dtpFechaFin1.Value))
            {
                MessageBox.Show("La fecha de inicio debe ser menor que la fecha de fin.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DateTime fechaInicio = dtpFechaInicio1.Value;
                DateTime fechaFin = dtpFechaFin1.Value;

                int totalAsistencias = _asistenciaController.ObtenerAsistenciasCompletas(_idEmpleado, fechaInicio, fechaFin);

                txtHorasTrabajadas.Text = totalAsistencias.ToString();

                MessageBox.Show($"Se calcularon correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show($"Ocurrió un error al calcular las asistencias:\n\n{ex.Message}\n\nDetalles técnicos: {errorReal}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool DatosVacios()
        {
            if (txtMatricula.Text == "" || txtMatricula.Text == "Ingresa tu matricula" || dtpFechaInicio1.Text == "" || dtpFechaFin1.Text == "")
            {
                return true;
            }
            return false;
        }

        private void Regresar(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

