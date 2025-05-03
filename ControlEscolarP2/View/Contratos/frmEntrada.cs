using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using RecursosHumanos.Bussines;
using RecursosHumanos.Controller;
using RecursosHumanos.Controllers;
using RecursosHumanos.DataAccess;
using RecursosHumanos.Models;
using RecursosHumanos.Utilities;
using Timer = System.Windows.Forms.Timer;

namespace RecursosHumanos.View
{
    public partial class frmEntrada : Form
    {
        private readonly EmpleadosController _empleadosController = new EmpleadosController();
        private readonly ContratoController _contratosController = new ContratoController();
        private readonly AsistenciaController _asistenciasController = new AsistenciaController();
        private readonly AusenciaController _ausenciasController = new AusenciaController();


        //  Variables privadas para guardar los IDs al buscar
        private int _idEmpleado;
        private int _idPersona;

        private Timer _timer;

        public frmEntrada()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            InicializarCampos();
            InicializarFechaYHora();
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula, "Ingresa tu matricula");
        }

        private bool HoraEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text) || txtMatricula.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!EsMatriculaValida())
            {
                return false;
            }

            return true;
        }

        private bool EsMatriculaValida()
        {
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarCampos();  // Limpiar los campos en caso de error
                return false;
            }
            return true;
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
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

                // Validar estatus activo antes de cargar datos
                if (empleado.Estatus == 0) // 0 = Inactivo
                {
                    MessageBox.Show("El empleado está inactivo y no puede registrar asistencia.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                    return;
                }

                // Validar contrato activo
                ContratoController contratosController = new ContratoController();
                if (!contratosController.TieneContratoActivo(matricula))
                {
                    MessageBox.Show("El empleado no tiene un contrato activo y no puede registrar asistencia.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                    return;
                }

                // Asignar IDs internos
                _idEmpleado = empleado.Id_Empleado;
                _idPersona = empleado.DatosPersonales.Id_Persona;

                // Cargar datos personales
                txtNombre.Text = empleado.DatosPersonales.Nombre;
                txtRFC.Text = empleado.DatosPersonales.RFC;
                txtTelefono.Text = empleado.DatosPersonales.Telefono;
                txtDireccion.Text = empleado.DatosPersonales.Direccion;

                // Cargar datos laborales
                txtDepartamento.Text = empleado.Departamento;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar al empleado:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtRFC.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";

            txtDepartamento.Text = "";

            //Limpiar los IDs
            _idEmpleado = 0;
            _idPersona = 0;
        }


        private void InicializarFechaYHora()
        {
            // Configura el DTP de fecha Entrada
            dtpDiaEntrada.Format = DateTimePickerFormat.Custom;
            dtpDiaEntrada.CustomFormat = "dd/MM/yyyy";
            dtpDiaEntrada.Value = DateTime.Now;

            // Configura el DTP de fecha Salida
            dtpDiaSalida.Format = DateTimePickerFormat.Custom;
            dtpDiaSalida.CustomFormat = "dd/MM/yyyy";
            dtpDiaSalida.Value = DateTime.Now;

            // Configura el DTP de hora Entrada
            dtpHoraEntrada.Format = DateTimePickerFormat.Custom;
            dtpHoraEntrada.CustomFormat = "HH:mm:ss";
            dtpHoraEntrada.ShowUpDown = true;
            dtpHoraEntrada.Value = DateTime.Now;

            // Configura el DTP de hora Salida
            dtpHoraSalida.Format = DateTimePickerFormat.Custom;
            dtpHoraSalida.CustomFormat = "HH:mm:ss";
            dtpHoraSalida.ShowUpDown = true;
            dtpHoraSalida.Value = DateTime.Now;

            // Configura el Timer para actualizar la hora cada segundo
            _timer = new Timer();
            _timer.Interval = 1000; // 1 segundo
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dtpHoraEntrada.Value = DateTime.Now; // Actualiza la hora en tiempo real entrada
            dtpHoraSalida.Value = DateTime.Now; // Actualiza la hora en tiempo real salida
        }

        private void btnAceptarEntrada_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();

            if (string.IsNullOrWhiteSpace(matricula))
            {
                MessageBox.Show("Por favor, ingrese una matrícula válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Llamar al Controller para registrar la entrada
                bool resultado = _asistenciasController.RegistrarEntrada(matricula, out string mensaje);

                MessageBox.Show(mensaje, resultado ? "Éxito" : "Advertencia",
                                MessageBoxButtons.OK,
                                resultado ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (resultado)
                    LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al registrar la entrada:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAceptarSalida_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();

            // Verificar que la matrícula no esté vacía
            if (string.IsNullOrWhiteSpace(matricula))
            {
                MessageBox.Show("Por favor, ingrese una matrícula válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Verificar que el empleado exista
                Empleado empleado = _empleadosController.ObtenerEmpleadoPorMatricula(matricula);

                if (empleado == null)
                {
                    MessageBox.Show("Empleado no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    return;
                }

                // Validar si el empleado está activo
                if (empleado.Estatus == 0) // 0 = Inactivo
                {
                    MessageBox.Show("El empleado está inactivo y no puede registrar su salida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                    return;
                }

                // Verificar si el empleado tiene un contrato activo
                ContratoController contratosController = new ContratoController();
                if (!contratosController.TieneContratoActivo(matricula))
                {
                    MessageBox.Show("El empleado no tiene un contrato activo y no puede registrar su salida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                    return;
                }

                // Verificar si ya tiene registrada una ausencia hoy
                if (_ausenciasController.TieneAusenciaRegistradaHoy(matricula))
                {
                    MessageBox.Show("No se puede registrar la salida porque el empleado ya tiene una ausencia registrada hoy.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Registrar la salida del empleado
                bool resultado = _asistenciasController.RegistrarSalida(matricula, out string mensaje);

                // Mostrar el mensaje según el resultado
                MessageBox.Show(mensaje, resultado ? "Éxito" : "Advertencia",
                                MessageBoxButtons.OK,
                                resultado ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (resultado)
                    LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al registrar la salida:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
