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
using Guna.UI2.WinForms.Suite;
using RecursosHumanos.Bussines;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using RecursosHumanos.Utilities;
using RecursosHumanos.Controller;

namespace RecursosHumanos.View
{
    public partial class frmRegistroPersonas : Form
    {
        public static int IdPersonaRegistrada { get; set; } = -1;//para usarlo en usuarios xd

        public frmRegistroPersonas()
        {
            InitializeComponent();
            InicializarVentana();
        }

        public void InicializarVentana()
        {
            IniciarPaneles();
            PoblaComboGenero();
            InicializarCampos();
        }

        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtNombre, "Ingrese nombre");
            Formas.ConfigurarTextBox(txtDireccion, "Ingrese dirección completa");
            Formas.ConfigurarTextBox(txtCorreo, "Ingrese dirección de correo electronico");
            Formas.ConfigurarTextBox(txtTelefono, "Ej. 7291589593");
            Formas.ConfigurarTextBox(txtCURP, "Ingrese CURP");
            Formas.ConfigurarTextBox(txtRFC, "Ingrese RFC");
            Formas.ConfigurarTextBox(txtMaterno, "Ingrese apellido materno");
            Formas.ConfigurarTextBox(txtPaterno, "Ingrese apellido paterno");
        }

        private void IniciarPaneles()
        {
            Form frmGuardarInf = new frmGuardarInformacion();
            Formas.abrirPanelForm(frmGuardarInf, pnlCambiante);
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            Form frmRegistroUsuario = new frmRegistroUsuario();
            Formas.abrirPanelForm(frmRegistroUsuario, pnlCambiante);
        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            GenerarPersona();
        }

        public static void DesbloquearCampos(bool accion)
        {
            txtNombre.Enabled = accion;
            txtDireccion.Enabled = accion;
            txtCorreo.Enabled = accion;
            txtTelefono.Enabled = accion;
            txtCURP.Enabled = accion;
            txtRFC.Enabled = accion;
            txtMaterno.Enabled = accion;
            txtPaterno.Enabled = accion;
            cbGenero.Enabled = accion;
        }

        public static bool GenerarPersona()
        {
            try
            {
                if (!DatosVaciosPersona())
                {
                    return false;
                }
                if (!DatosCorrectosPersona())
                {
                    return false;
                }
                
                Persona persona = new Persona
                {
                    Nombre = txtNombre.Text.Trim(),
                    Ap_Paterno = txtPaterno.Text.Trim(),
                    Ap_Materno = txtMaterno.Text.Trim(),
                    RFC = txtRFC.Text.Trim(),
                    CURP = txtCURP.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtCorreo.Text.Trim(),
                    Fecha_Nacimiento = dtpFechaNacimiento.Value,
                    Genero = cbGenero.Text,
                    Estatus = 1
                };

                PersonasController personasController = new PersonasController();
                var(exito, mensaje, idPersona) = personasController.RegistrarPersona(persona);
                //Verificar el resultado
                if (idPersona > 0)
                {
                    MessageBox.Show(mensaje, "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DesbloquearCampos(false); 
                    MDIRecursosHumanos.BloquearBotonesMenu();
                    IdPersonaRegistrada = idPersona;
                }
                else
                {
                    //Mostrar mensaje de error devuelto por el controlador
                    MessageBox.Show(mensaje, "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //Enfocar el campo apropiado basado en el codigo de error
                    switch (idPersona)
                    {
                        case -1: // Error genérico
                            MessageBox.Show("Ocurrió un error al registrar la persona. Intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -2: // CURP duplicado
                            MessageBox.Show("El CURP ingresado ya está registrado. Verifique la información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCURP.Focus();
                            txtCURP.SelectAll();
                            break;
                        case -3: // RFC duplicado
                            MessageBox.Show("El RFC ingresado ya está registrado. Verifique la información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtRFC.Focus();
                            txtRFC.SelectAll();
                            break;
                        case -4: // Teléfono duplicado
                            MessageBox.Show("El número de teléfono ingresado ya está registrado. Verifique la información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtTelefono.Focus();
                            txtTelefono.SelectAll();
                            break;
                        case -5: // Correo duplicado
                            MessageBox.Show("El correo electrónico ingresado ya está registrado. Verifique la información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCorreo.Focus();
                            txtCorreo.SelectAll();
                            break;
                        default: // Otros errores no especificados
                            MessageBox.Show("Ocurrió un error desconocido. Código de error: " + idPersona, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DatosVaciosPersona()
        {
            if (txtNombre.Text == "Ingrese nombre" || txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre de la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPaterno.Text == "Ingrese apellido paterno" || txtPaterno.Text == "")
            {
                MessageBox.Show("Ingrese el apellido paterno de la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtMaterno.Text == "Ingrese apellido materno" || txtMaterno.Text == "")
            {
                MessageBox.Show("Ingrese el apellido materno la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtRFC.Text == "Ingrese RFC" || txtRFC.Text == "")
            {
                MessageBox.Show("Ingrese el RFC la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtCURP.Text == "Ingrese CURP" || txtCURP.Text == "")
            {
                MessageBox.Show("Ingrese el CURP la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtTelefono.Text == "Ej. 7291589593" || txtTelefono.Text == "")
            {
                MessageBox.Show("Ingrese el teléfono la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbGenero.SelectedValue == null || (int)cbGenero.SelectedValue == 0)
            {
                MessageBox.Show("Seleccione el género la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtDireccion.Text == "Ingrese dirección completa" || txtDireccion.Text == "")
            {
                MessageBox.Show("Ingrese la dirección la persona", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtCorreo.Text == "Ingrese dirección de correo electronico" || txtCorreo.Text == "")
            {
                MessageBox.Show("Ingrese el correo electrónico del empleado", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool DatosCorrectosPersona()
        {
            if (PersonasNegocio.EsRFCValido(txtRFC.Text.Trim()))
            {
                MessageBox.Show("RFC inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsCURPValido(txtCURP.Text.Trim()))
            {
                MessageBox.Show("CURP inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsTelefonoValido(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("Teléfono inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsGeneroValido((int)cbGenero.SelectedValue))
            {
                MessageBox.Show("Género inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsEmailValido(txtCorreo.Text.Trim()))
            {
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void PoblaComboGenero()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_tipoCon = new Dictionary<int, string>
            {
                { 0,"Seleccione"},
                { 1, "Hombre" },
                { 2, "Mujer" }
            };
            // Asignar el diccionario al ComboBox
            cbGenero.DataSource = new BindingSource(list_tipoCon, null);
            cbGenero.DisplayMember = "Value";  // Lo que se muestra
            cbGenero.ValueMember = "Key";      // Lo que se guarda como SelectedValue
            cbGenero.SelectedValue = 0;
        }

        private void btnPersonaCancelar_Click(object sender, EventArgs e)
        {
            if (IdPersonaRegistrada == -1)
            {
                InicializarCampos();
                return;
            }
            PersonasController personasController = new PersonasController();
            var exito = personasController.CancelarRegistroPersona(IdPersonaRegistrada);
            if (!exito)
            {
                MessageBox.Show("No se canceló el registro, no se pudo eliminar la persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IdPersonaRegistrada = -1;
            MessageBox.Show("Se canceló el registro y se eliminó la persona.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DesbloquearCampos(true);
            InicializarCampos();
            MDIRecursosHumanos.DesbloquearBotonesMenu();
        }

        //--------------------------------------------------------------------------------Creacion de Persona

    }
}
