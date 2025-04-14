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

namespace RecursosHumanos.View.Usuarios
{
    public partial class frmAuditoria : Form
    {
        public frmAuditoria()
        {
            InitializeComponent();
            InicializarCampos();
            this.Resize += frmAuditoria_Resize;
        }
        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            InicializaVentana();
            IniciarTabla();
            ConfigurarHora();
    
            scAgregar.Dock = DockStyle.Fill;
        }

        private void InicializaVentana()
        {
            PoblaComboTipoMovimiento();
            PoblaComboAccionRealizada();
            PoblaComboAccionRealizadaUsuarios();
            PoblaComboEntidad();
            scAgregar.Panel1Collapsed = true;
        }
        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtUsuario, "Ingrese nombre de usuario");
            Formas.ConfigurarTextBox(txtMovimiento, "Descripción del movimiento");
            Formas.ConfigurarTextBox(txtResponsable, "Ingresa nombre de usuario");
            Formas.ConfigurarTextBox(txtIp, "Ingresa IP de equipo");
            Formas.ConfigurarTextBox(txtNomEquipo, "Ingresa nombre de equipo");
            Formas.ConfigurarTextBox(txtDetalles, "Ingresa detalle del movimiento");
        }

        private void ConfigurarHora()
        {
            dtpHora.Format = DateTimePickerFormat.Custom;
            dtpHora.CustomFormat = "HH:mm"; // 24 horas (Ejemplo: 14:30)
            dtpHora.ShowUpDown = true; // Solo permite cambiar la hora


        }
        private void frmAuditoria_Resize(object sender, EventArgs e)
        {
            // Ajustar el tamaño del panel izquierdo automáticamente
            scAgregar.SplitterDistance = this.Width / 3;
        }

        private void PoblaComboTipoMovimiento()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_TipoMovi = new Dictionary<int, string>
            {
                { 1, "Empleado" },
                { 0, "Null" },
                { 2, "Contrato" }
            };

            // Asignar el diccionario al ComboBox
            cbxMovimiento.DataSource = new BindingSource(list_TipoMovi, null);
            cbxMovimiento.DisplayMember = "Value";  // Lo que se muestra
            cbxMovimiento.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxMovimiento.SelectedValue = 1;

        }

        private void PoblaComboAccionRealizada()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_Accion = new Dictionary<int, string>
            {
                { 1, "Alta" },
                { 0, "Baja" },
                { 2, "Cambio" }
            };

            // Asignar el diccionario al ComboBox
            cbxAccion.DataSource = new BindingSource(list_Accion, null);
            cbxAccion.DisplayMember = "Value";  // Lo que se muestra
            cbxAccion.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxAccion.SelectedValue = 1;

        }

        private void PoblaComboAccionRealizadaUsuarios()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_Accion = new Dictionary<int, string>
            {
                { 1, "Alta" },
                { 0, "Baja" },
                { 2, "Cambio" }
            };

            // Asignar el diccionario al ComboBox
            cbxAccionUsuario.DataSource = new BindingSource(list_Accion, null);
            cbxAccionUsuario.DisplayMember = "Value";  // Lo que se muestra
            cbxAccionUsuario.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxAccionUsuario.SelectedValue = 1;

        }

        private void PoblaComboEntidad()
        {
            // Crear un diccionario con los valores
            Dictionary<int, string> list_Accion = new Dictionary<int, string>
            {
                { 1, "Empleado" },
                { 0, "Contrato" },
                { 2, "Usuario" }
            };

            // Asignar el diccionario al ComboBox
            cbxEntidad.DataSource = new BindingSource(list_Accion, null);
            cbxEntidad.DisplayMember = "Value";  // Lo que se muestra
            cbxEntidad.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxEntidad.SelectedValue = 1;

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.Text == "Ingrese nombre de usuario")
            {
                MessageBox.Show("Por favor, ingrese su usuario.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Cargando resultados.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.Text == "Ingrese nombre de usuario")
            {
                MessageBox.Show("Por favor, ingrese su usuario.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!UsuarioNegocio.EsNombreUsuarioValido(txtUsuario.Text.Trim()))
            {
                MessageBox.Show("Nombre de usuario inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Auditoria lista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridAuditorias); // Configurar el estilo del DataGridView
            ConfigurarColumnasAuditoria(); // Agregar columnas personalizadas
        }

        private void ConfigurarColumnasAuditoria()
        {
            dataGridAuditorias.Columns.Clear();
            dataGridAuditorias.AutoGenerateColumns = false; // Desactiva la generación automática

            // Agregamos las columnas que corresponden a los campos de la tabla Auditoria
            dataGridAuditorias.Columns.Add("ID_Auditoria", "ID");
            dataGridAuditorias.Columns.Add("Movimiento", "ID Afectado");
            dataGridAuditorias.Columns.Add("ID_Tipo", "Tipo de Entidad");
            dataGridAuditorias.Columns.Add("ID_Accion", "Acción");
            dataGridAuditorias.Columns.Add("Fecha_Movimiento", "Fecha y Hora");
            dataGridAuditorias.Columns.Add("IP_Equipo", "IP del Equipo");
            dataGridAuditorias.Columns.Add("Nombre_equipo", "Nombre del Equipo");
            dataGridAuditorias.Columns.Add("Detalle", "Detalle del Movimiento");
            dataGridAuditorias.Columns.Add("ID_Usuario", "ID Usuario");

            dataGridAuditorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            // Validar que todos los campos estén llenos
            if (DatosVacios())
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si todo es válido, generar contrato
            MessageBox.Show("Auditoria guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método para verificar si hay campos vacíos
        private bool DatosVacios()
        {
            if ( txtMovimiento.Text == "" || txtMovimiento.Text == "Descripción del movimiento" || txtResponsable.Text == "" || txtResponsable.Text == "Ingresa nombre de usuario "
                || txtIp.Text == "" || txtIp.Text == "Ingresa IP de equipo" || txtNomEquipo.Text == "" || txtNomEquipo.Text == "Ingresa el nombre de equipo" || txtDetalles.Text == "" || txtDetalles.Text == "Ingresa detalle de movimiento")
            {
                return true;
            }
            return false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (scAgregar.Panel1Collapsed)
            {
                scAgregar.Panel1Collapsed = false;
                btnAgregar.Text = "Ocultar auditoria";

            }
            else
            {
                scAgregar.Panel1Collapsed = true;
                btnAgregar.Text = "Agregar Auditoria";
            }
        }
    }
}
