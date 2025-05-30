using RecursosHumanosCore.Bussines;
using RecursosHumanosCore.Controller;
using RecursosHumanosCore.Model;
using RecursosHumanos.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursosHumanos.View
{
    public partial class frmGestionCreacionRoles : Form
    {
        private PermisosController _permisosController;

        public frmGestionCreacionRoles()
        {
            InitializeComponent();
            _permisosController = new PermisosController();
            InicializarVentana();
            VerificarPermisos();
        }

        private void InicializarVentana()
        {
            InicializarCampos();
            IniciarTabla();
            // Configurar el modo de edición del DataGridView
            dataGridPermisos.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void IniciarTabla()
        {
            ConfigurarAnchoColumnas(300);
            FormLoad();
            Formas.ConfigurarEstiloDataGridView(dataGridPermisos);
        }

        private void ConfigurarAnchoColumnas(int ancho)
        {
            foreach (DataGridViewColumn columna in dataGridPermisos.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private bool GenerarRol()
        {
            try
            {
                if (!DatosVaciosRoles())
                {
                    return false;
                }
                if (!DatosCorrectosRoles())
                {
                    return false;
                }

                // Crear objeto Rol con datos del formulario
                Rol nuevoRol = new Rol
                {
                    Codigo = txtRolCodigo.Text.Trim(),
                    Nombre = txtRolNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estatus = 1 // Activo por defecto
                };

                // Obtener permisos seleccionados del DataGridView
                List<int> permisosSeleccionados = ObtenerPermisosSeleccionados();

                if (permisosSeleccionados.Count == 0)
                {
                    MessageBox.Show("Seleccione al menos un permiso para el rol.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Llamar al controlador para guardar el rol y asignar permisos
                RolesController rolesController = new RolesController();
                var (exito, mensaje) = rolesController.CrearRolConPermisos(nuevoRol, permisosSeleccionados);

                // Mostrar el resultado
                if (exito)
                {
                    MessageBox.Show(mensaje, "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InicializarCampos();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error al crear rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return exito;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al intentar guardar el rol. Por favor, intente nuevamente o contacte al administrador.", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtRolNombre, "Ingrese nombre del Rol");
            Formas.ConfigurarTextBox(txtRolCodigo, "Ingrese codigo del Rol");
            Formas.ConfigurarTextBox(txtDescripcion, "Describa lo que realizara el rol");
        }

        public bool DatosVaciosRoles()
        {
            if (txtRolNombre.Text == "Ingrese nombre del Rol" || txtRolCodigo.Text == "")
            {
                MessageBox.Show("Ingrese nombre del Rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtRolCodigo.Text == "Ingrese codigo del Rol" || txtRolCodigo.Text == "")
            {
                MessageBox.Show("Ingrese codigo del Rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtDescripcion.Text == "Describa lo que realizara el rol" || txtDescripcion.Text == "")
            {
                MessageBox.Show("Describa lo que realizara el rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool DatosCorrectosRoles()
        {
            if (!RolesNegocio.EsRolNombreValido(txtRolNombre.Text.Trim()))
            {
                MessageBox.Show("Nombre del Rol inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!RolesNegocio.EsRolCodigoValido(txtRolCodigo.Text.Trim()))
            {
                MessageBox.Show("Codigo del Rol inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!RolesNegocio.EsDescripcionValida(txtDescripcion.Text.Trim()))
            {
                MessageBox.Show("Descripción inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GenerarRol())
            {
                InicializarCampos();
            }
        }

        //--------------------------------------------------------------------------------Llenado tabla
        private void FormLoad()
        {
            CargarPermisosEnTabla();
            AgregarCheckBoxColumna();
        }

        private void CargarPermisosEnTabla()
        {
            try
            {
                var listaPermisos = _permisosController.ObtenerPermisos();
                dataGridPermisos.DataSource = listaPermisos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los permisos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Agrega una columna de tipo CheckBox al DataGridView para seleccionar permisos
        /// </summary>
        private void AgregarCheckBoxColumna()
        {
            if (!dataGridPermisos.Columns.Contains("Seleccionar"))
            {
                DataGridViewCheckBoxColumn checkBoxCol = new DataGridViewCheckBoxColumn
                {
                    Name = "Seleccionar",
                    HeaderText = "Seleccionar",
                    Width = 50,
                    FlatStyle = FlatStyle.Standard,
                    TrueValue = true,
                    FalseValue = false,
                    IndeterminateValue = false
                };
                // Aplicar estilos al checkbox desde código
                checkBoxCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                checkBoxCol.DefaultCellStyle.BackColor = Color.White;
                checkBoxCol.DefaultCellStyle.ForeColor = Color.Black;

                checkBoxCol.ReadOnly = false;

                dataGridPermisos.Columns.Insert(0, checkBoxCol);
            }
        }

        /// <summary>
        /// Obtiene la lista de IDs de permisos seleccionados en el DataGridView
        /// </summary>
        /// <returns>Lista de IDs de permisos</returns>
        private List<int> ObtenerPermisosSeleccionados()
        {
            List<int> permisosIds = new List<int>();

            try
            {
                foreach (DataGridViewRow fila in dataGridPermisos.Rows)
                {
                    // Validar si la fila no está vacía ni nueva
                    if (!fila.IsNewRow)
                    {
                        // Verifica si la columna "Seleccionar" está marcada
                        bool seleccionado = Convert.ToBoolean(fila.Cells["Seleccionar"].Value ?? false);

                        if (seleccionado)
                        {
                            // Obtener el ID del permiso
                            int idPermiso = Convert.ToInt32(fila.Cells["Id_Permiso"].Value);
                            permisosIds.Add(idPermiso);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los permisos seleccionados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return permisosIds;
        }

        /// <summary>
        /// Verifica los permisos del usuario para habilitar o deshabilitar los botones de registro.
        /// </summary>
        private void VerificarPermisos()
        {
            var permisosUsuario = MDIRecursosHumanos.permisosUsuario;

            if (!permisosUsuario.Contains(27)) // CreaR rol
            {
                btnGuardar.Enabled = false;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            PermisosController permisosController = new PermisosController();

            var (exito, mensaje) = permisosController.ExportarPermisosExcel();

            if (exito)
            {
                MessageBox.Show(mensaje, "Exportación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(mensaje, "Exportación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
