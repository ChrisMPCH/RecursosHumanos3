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

namespace RecursosHumanos.View.Usuarios
{
    public partial class frmAuditoria : Form
    {
        public frmAuditoria()
        {
            InitializeComponent();
            InicializarCampos();
            InicializaVentana();

            scAgregar.Dock = DockStyle.Fill;

            IniciarTabla();
            this.Resize += frmAuditoria_Resize;
        }

        private void InicializaVentana()
        {
            PoblaComboTipoMovimiento();
            PoblaComboAccionRealizada();
            PoblaComboAccionRealizadaUsuarios();
            PoblaComboEntidad();
            scAgregar.Panel1Collapsed = false;
        }

        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtUsuario, "Ingrese nombre de usuario");
            Formas.ConfigurarTextBox(txtMovimiento, "Descripción del movimiento");
            Formas.ConfigurarTextBox(txtResponsable, "Ingresa nombre de usuario");
            Formas.ConfigurarTextBox(txtIpB, "Ingresa IP de equipo");
            Formas.ConfigurarTextBox(txtNomEquipoB, "Ingresa nombre de equipo");
            Formas.ConfigurarTextBox(txtDetalles, "Ingresa detalle del movimiento");
            cbxEntidad.Enabled = false; // Deshabilitar el campo ID_Entidad
            cbxAccionUsuario.Enabled = false;  // Deshabilitar el campo ID_Tipo
            txtMovimiento.ReadOnly = true; // Deshabilitar el campo ID_Accion
            txtResponsable.ReadOnly = true; // Deshabilitar el campo ID_Accion
            dtpFechaInicio1.Enabled = false; // Deshabilitar el campo Fecha de Inicio
            txtIp.ReadOnly = true; // Deshabilitar el campo IP_Equipo
            txtNomEquipo.ReadOnly = true; // Deshabilitar el campo Nombre_Equipo
            txtDetalles.ReadOnly = true; // Deshabilitar el campo Detalle
        }

        private void frmAuditoria_Resize(object sender, EventArgs e)
        {
            // Ajustar el tamaño del panel izquierdo automáticamente
            scAgregar.SplitterDistance = this.Width / 4;
        }

        private void PoblaComboTipoMovimiento()
        {
            // Crear un diccionario con los valores de tipos de movimiento
            Dictionary<int, string> list_TipoMovi = new Dictionary<int, string>
        {
        { -1, "Seleccione" },
            { 1, "Empleado" },
        { 2, "Contrato" },
        { 3, "Usuario" },
        { 4, "Roles" },
        { 5, "Perisos" },
        { 6, "Otros" }
        };

            // Asignar el diccionario al ComboBox
            cbxMovimiento.DataSource = new BindingSource(list_TipoMovi, null);
            cbxMovimiento.DisplayMember = "Value";  // Lo que se muestra
            cbxMovimiento.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxMovimiento.SelectedValue = -1;  // Valor por defecto
        }

        private void PoblaComboAccionRealizada()
        {
            // Crear un diccionario con los valores de acciones realizadas
            Dictionary<int, string> list_Accion = new Dictionary<int, string>
        {
                            { -1, "Seleccione" },

        { 1, "Alta" },
        { 2, "Actualizacion" },
        { 0, "Baja" }
        };

            // Asignar el diccionario al ComboBox
            cbxAccion.DataSource = new BindingSource(list_Accion, null);
            cbxAccion.DisplayMember = "Value";  // Lo que se muestra
            cbxAccion.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxAccion.SelectedValue = -1;  // Valor por defecto 
        }

        private void PoblaComboAccionRealizadaUsuarios()
        {
            // Crear un diccionario con las acciones realizadas para los usuarios
            Dictionary<int, string> list_AccionUsuario = new Dictionary<int, string>
            {
            { -1, "Seleccione" },
            { 1, "Alta" },
            { 2, "Actualizacion" },
            { 0, "Baja" }
            };

            // Asignar el diccionario al ComboBox
            cbxAccionUsuario.DataSource = new BindingSource(list_AccionUsuario, null);
            cbxAccionUsuario.DisplayMember = "Value";  // Lo que se muestra
            cbxAccionUsuario.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxAccionUsuario.SelectedValue = -1;  // Valor por defecto
        }

        private void PoblaComboEntidad()
        {
            // Crear un diccionario con los valores de entidades
            Dictionary<int, string> list_Entidad = new Dictionary<int, string>
            {
            { -1, "Seleccione" },
            { 1, "Empleado" },
        { 2, "Contrato" },
        { 3, "Usuario" },
        { 4, "Roles" },
        { 5, "Perisos" },
        { 6, "Otros" }
            };

            // Asignar el diccionario al ComboBox
            cbxEntidad.DataSource = new BindingSource(list_Entidad, null);
            cbxEntidad.DisplayMember = "Value";  // Lo que se muestra
            cbxEntidad.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxEntidad.SelectedValue = -1;  // Valor por defecto

            cbxEntidad.DataSource = new BindingSource(list_Entidad, null);
            cbxEntidad.DisplayMember = "Value";  // Lo que se muestra
            cbxEntidad.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbxEntidad.SelectedValue = -1;  // Valor por defecto

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarTabla(false);
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridAuditorias); // Configurar el estilo del DataGridView
            ConfigurarColumnasAuditoria(); // Agregar columnas personalizadas

            llenarTabla(true);
        }

        private void ConfigurarColumnasAuditoria()
        {
            dataGridAuditorias.Columns.Clear();
            dataGridAuditorias.AutoGenerateColumns = false; // Desactiva la generación automática

            // Agregamos las columnas que corresponden a los campos de la tabla Auditoria
            dataGridAuditorias.Columns.Add("ID_Auditoria", "ID");
            dataGridAuditorias.Columns.Add("ID_Tipo", "Tipo de Entidad");
            dataGridAuditorias.Columns.Add("ID_Accion", "Acción");
            dataGridAuditorias.Columns.Add("Fecha_Movimiento", "Fecha y Hora");
            dataGridAuditorias.Columns.Add("IP_Equipo", "IP del Equipo");
            dataGridAuditorias.Columns.Add("Nombre_equipo", "Nombre del Equipo");
            dataGridAuditorias.Columns.Add("Detalle", "Detalle del Movimiento");
            dataGridAuditorias.Columns.Add("ID_Usuario", "ID Usuario");

            dataGridAuditorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Método llenar la tabla
        private void llenarTabla(bool all)
        {
            // Crear el controlador de auditorías
            var auditoriasController = new AuditoriasController();

            // Obtener los filtros de la vista con validaciones para asegurarse de que solo se apliquen si tienen valores
            int? idTipo = (int?)cbxMovimiento.SelectedValue != -1 ? (int?)cbxMovimiento.SelectedValue : null; // Tipo de movimiento seleccionado

            // Validación para la acción realizada
            int? idAccion = (int?)cbxAccion.SelectedValue != -1 ? (int?)cbxAccion.SelectedValue : null; // Acción realizada seleccionada

            // Inicializar las fechas de inicio y fin como null
            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;

            // Validar si las fechas han sido seleccionadas
            if (dtpFechaInicio.Checked)
            {
                fechaInicio = dtpFechaInicio.Value;
            }

            if (dtpFechaFin.Checked)
            {
                fechaFin = dtpFechaFin.Value;
            }

            // IP de equipo (si no está vacío o con el valor predeterminado)
            string ipEquipo = !string.IsNullOrWhiteSpace(txtIp.Text) && txtIp.Text != "Ingresa IP de equipo" ? txtIp.Text : null;

            // Nombre de equipo (si no está vacío o con el valor predeterminado)
            string nombreEquipo = !string.IsNullOrWhiteSpace(txtNomEquipo.Text) && txtNomEquipo.Text != "Ingresa el nombre de equipo" ? txtNomEquipo.Text : null;

            // ID de usuario (si no está vacío o con el valor predeterminado)
            int? idUsuario = !string.IsNullOrWhiteSpace(txtResponsable.Text) && txtResponsable.Text != "Ingresa nombre de usuario" ? (int?)Convert.ToInt32(txtResponsable.Text) : null;

            // Acción realizada por usuario (estatus)
            int? estatus = 1;

            try
            {
                // Llamar al controlador para obtener las auditorías filtradas
                List<Auditoria> auditorias = new List<Auditoria>();

                if (all)
                {
                    // Obtener todas las auditorías si 'all' es verdadero
                    auditorias = auditoriasController.ObtenerAuditorias();
                }
                else
                {
                    // Obtener auditorías con filtros si 'all' es falso
                    auditorias = auditoriasController.ObtenerAuditorias(idTipo, idAccion, fechaInicio, fechaFin, ipEquipo, nombreEquipo, idUsuario, estatus);
                }

                // Limpiar filas previas
                dataGridAuditorias.Rows.Clear();

                // Si se encuentran auditorías, agregar las filas al DataGridView
                if (auditorias != null && auditorias.Count > 0)
                {
                    foreach (var auditoria in auditorias)
                    {
                        // Ajustar los valores de los campos ID a sus nombres legibles
                        string tipoEntidad = ObtenerTipoEntidad(auditoria.Id_Tipo);
                        string accionRealizada = ObtenerAccionRealizada(auditoria.Id_Accion);

                        // Agregar la fila con los datos ajustados
                        dataGridAuditorias.Rows.Add(
                            auditoria.Id_Auditoria,
                            tipoEntidad,
                            accionRealizada,
                            auditoria.Fecha_Movimiento.ToString("dd/MM/yyyy HH:mm"),
                            auditoria.Ip_Equipo,
                            auditoria.Nombre_Equipo,
                            auditoria.Detalle,
                            auditoria.Id_Usuario
                        );
                    }
                }
                else
                {
                    // Si no se encuentran auditorías, mostrar un mensaje
                    MessageBox.Show("No se encontraron auditorías con los filtros especificados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las auditorías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Métodos para obtener los nombres en lugar de los IDs

        private string ObtenerTipoEntidad(int idTipo)
        {
            // Mapear el tipo de entidad según el ID
            switch (idTipo)
            {
                case 1: return "Empleado";
                case 2: return "Contrato";
                case 3: return "Usuario";
                default: return "Desconocido";
            }
        }

        private string ObtenerAccionRealizada(int idAccion)
        {
            // Mapear la acción realizada según el ID
            switch (idAccion)
            {
                case 1: return "Alta";
                case 2: return "Cambio";
                case 0: return "Baja";
                default: return "Desconocido";
            }
        }

        private void txtIdAuditoria_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Obtener el ID de la auditoría ingresado
                int idAuditoria;
                if (int.TryParse(txtIdAuditoria.Text, out idAuditoria))
                {
                    // Llamar al controlador para obtener la auditoría correspondiente
                    var auditoriasController = new AuditoriasController();
                    var auditoria = auditoriasController.ObtenerAuditoriaPorId(idAuditoria);

                    // Verificar si se encontró la auditoría
                    if (auditoria != null)
                    {
                        // Llenar los campos con los datos de la auditoría
                        txtMovimiento.Text = auditoria.Movimiento.ToString();  // Aquí se llena el campo 'movimiento'

                        cbxEntidad.SelectedValue = (int)auditoria.Id_Tipo;
                        cbxAccionUsuario.SelectedValue = (int)auditoria.Id_Accion;
                        txtResponsable.Text = auditoria.Id_Usuario.ToString();
                        dtpFechaInicio1.Value = auditoria.Fecha_Movimiento;

                        txtIp.Text = auditoria.Ip_Equipo;
                        txtNomEquipo.Text = auditoria.Nombre_Equipo;

                        txtDetalles.Text = auditoria.Detalle;

                        // Puedes agregar más campos según sea necesario
                    }
                    else
                    {
                        // Si no se encuentra la auditoría, mostrar un mensaje
                        MessageBox.Show("Auditoría no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Si el ID de auditoría no es válido, mostrar un mensaje
                    MessageBox.Show("Por favor, ingrese un ID de auditoría válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
