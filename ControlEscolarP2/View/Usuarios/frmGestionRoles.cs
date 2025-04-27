using RecursosHumanos.Bussines;
using RecursosHumanos.Controller;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
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
    public partial class frmGestionRoles : Form
    {
        private RolesController _rolesController;
        private PermisosController _permisosController;


        public frmGestionRoles()
        {
            InitializeComponent();
            _rolesController = new RolesController();
            _permisosController = new PermisosController();
            InicializarVentana();
        }

        private void InicializarVentana()
        {
            InicializarCampos();
            IniciarTablaRolesU();
            // Configurar el modo de edición del DataGridView
            dataGridPermisos.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void IniciarTablaRolesU()
        {
            ConfigurarAnchoColumnasRoles(300);
            FormLoad();
            Formas.ConfigurarEstiloDataGridView(dataGridRolesU);
        }

        private void ConfigurarAnchoColumnasRoles(int ancho)
        {
            foreach (DataGridViewColumn columna in dataGridRolesU.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private void ConfigurarAnchoColumnasPermisos(int ancho)
        {
            foreach (DataGridViewColumn columna in dataGridPermisos.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private void HabilitarEditar()
        {
            txtDescripcion.ReadOnly = false;
            btnGuardarEdicion.Enabled = true;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = true;
            btnBuscar.Enabled = false;

            dataGridPermisos.ScrollBars = ScrollBars.Both;
            dataGridPermisos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridPermisos.AutoSize = false;
            dataGridPermisos.ReadOnly = false;

        }

        private bool EditarRol()
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

                // Obtener ID guardado en el Tag del textbox
                if (txtRolCodigo.Tag == null)
                {
                    MessageBox.Show("No se ha cargado un rol para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                int idRol = Convert.ToInt32(txtRolCodigo.Tag);

                Rol rolEditado = new Rol
                {
                    Id_Rol = idRol,
                    Codigo = txtRolCodigo.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estatus = 1
                };

                List<int> permisosSeleccionados = ObtenerPermisosSeleccionados();
                if (permisosSeleccionados.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un permiso.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                RolesController controller = new RolesController();
                var resultado = controller.ActualizarRolConPermisos(rolEditado, permisosSeleccionados);

                if (resultado.exito)
                {
                    MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestablecerTodo();
                    return true;
                }
                else
                {
                    MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al guardar los cambios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtRolCodigo, "Ingrese codigo del Rol");
            Formas.ConfigurarTextBox(txtDescripcion, "Describa lo que realizara el rol");
        }

        public bool DatosVaciosRoles()
        {
            if (txtRolCodigo.Text == "Ingrese nombre del Rol" || txtRolCodigo.Text == "")
            {
                MessageBox.Show("Ingrese el nombre del Rol", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (!RolesNegocio.EsRolCodigoValido(txtRolCodigo.Text.Trim()))
            {
                MessageBox.Show("Rol inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!RolesNegocio.EsDescripcionValida(txtDescripcion.Text.Trim()))
            {
                MessageBox.Show("Descripción inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Text == "Buscar")
            {
                if (txtRolCodigo.Text == "Ingrese codigo del Rol" || txtRolCodigo.Text == "")
                {
                    MessageBox.Show("Ingrese el rol del usuario.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (BuscarRolPorCodigo(txtRolCodigo.Text.Trim()))
                {
                    btnBuscar.Text = "Editar";
                    btnCancelar.Text = "Cerrar";
                    btnCancelar.Enabled = true;
                    btnEliminar.Enabled = true;
                    txtDescripcion.ReadOnly = true;
                    txtRolCodigo.Enabled = false;

                    CargarPermisosEnTabla();                                // 1. Carga los permisos
                    AgregarCheckBoxColumna();                               // 2. Asegúrate que la columna existe
                    Formas.ConfigurarEstiloDataGridView(dataGridPermisos);  // 3. Aplica estilos
                    MarcarPermisosAsignados((int)txtRolCodigo.Tag);         // 4. MARCA permisos ✅

                    dataGridPermisos.ReadOnly = true;
                }

            }
            else if (btnBuscar.Text == "Editar")
            {
                HabilitarEditar();
                btnCancelar.Text = "Cancelar";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtRolCodigo.Tag == null)
            {
                MessageBox.Show("Debe buscar un rol antes de poder eliminarlo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idRol = Convert.ToInt32(txtRolCodigo.Tag);
            var resultado = _rolesController.EliminarRol(idRol);

            if (resultado.exito)
            {
                MessageBox.Show(resultado.mensaje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestablecerTodo();
            }
            else
            {
                MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            if(EditarRol())
            {
                RestablecerTodo();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            
            RestablecerTodo();
        }

        private void RestablecerTodo()
        {
            InicializarCampos();
            IniciarTablaRolesU();
            RestablecerTablaPermisos();
            txtDescripcion.ReadOnly = true;
            txtRolCodigo.ForeColor = Color.FromArgb(125, 137, 149);
            btnGuardarEdicion.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            btnBuscar.Text = "Buscar";
            btnBuscar.Enabled = true;
            txtRolCodigo.Enabled = true;
        }

        private void RestablecerTablaPermisos()
        {
            dataGridPermisos.DataSource = null;
            dataGridPermisos.Rows.Clear();
            dataGridPermisos.Columns.Clear();
            dataGridPermisos.Refresh();
        }

        //--------------------------------------------------------------------------------Llenado tabla
        private void FormLoad()
        {
            CargarRolesEnTabla();
        }

        /// <summary>
        /// Carga los roles en el dataGridRolesU
        /// </summary>
        private void CargarRolesEnTabla()
        {
            try
            {
                var listaRoles = _rolesController.ObtenerRoles();
                dataGridRolesU.DataSource = listaRoles;

                // Opcional: ocultar columnas o cambiar encabezados
                dataGridRolesU.Columns["Id_Rol"].Visible = false;
                dataGridRolesU.Columns["Codigo"].HeaderText = "Código";
                dataGridRolesU.Columns["Nombre"].HeaderText = "Nombre del Rol";
                dataGridRolesU.Columns["Descripcion"].HeaderText = "Descripción";
                dataGridRolesU.Columns["Estatus"].HeaderText = "Estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------Ver permisos del ROl

        /// <summary>
        /// Busca un rol por su código y lo carga en los campos de texto
        /// </summary>
        /// <param name="codigo"></param>
        private bool BuscarRolPorCodigo(string codigo)
        {
            try
            {
                RolesController controller = new RolesController();
                Rol? rol = controller.ObtenerRolPorCodigo(codigo);

                if (rol == null)
                {
                    MessageBox.Show("No se encontró ningún rol con ese código.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Llenar los campos
                txtDescripcion.Text = rol.Descripcion;

                // Guardar ID del rol
                txtRolCodigo.Tag = rol.Id_Rol;

                // Marcar los permisos que tiene ese rol
                MarcarPermisosAsignados(rol.Id_Rol);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar rol: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
        /// Carga los permisos en el DataGridView
        /// </summary>
        private void CargarPermisosEnTabla()
        {
            try
            {
                var listaPermisos = _permisosController.ObtenerPermisosCorto();
                dataGridPermisos.DataSource = listaPermisos;

                // Asegúrate de ocultar/mostrar lo que quieras
                dataGridPermisos.Columns["Id_Permiso"].Visible = false;
                dataGridPermisos.Columns["Nombre"].Visible = false;
                dataGridPermisos.Columns["Codigo"].HeaderText = "Codigo";
                dataGridPermisos.Columns["Descripcion"].HeaderText = "Descripción";

                if (dataGridPermisos.Columns.Contains("Codigo"))
                    dataGridPermisos.Columns["Codigo"].Visible = true;

                if (dataGridPermisos.Columns.Contains("Estatus"))
                    dataGridPermisos.Columns["Estatus"].Visible = false;

                ConfigurarAnchoColumnasPermisos(300);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los permisos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Marca los permisos asignados a un rol en el DataGridView
        /// </summary>
        /// <param name="idRol"></param>
        private void MarcarPermisosAsignados(int idRol)
        {
            try
            {
                RolesPermisosDataAccess data = new RolesPermisosDataAccess();
                var permisosAsignados = data.ObtenerPermisosPorRol(idRol);

                foreach (DataGridViewRow row in dataGridPermisos.Rows)
                {
                    int idPermisoFila = Convert.ToInt32(row.Cells["Id_Permiso"].Value);
                    bool tienePermiso = permisosAsignados.Any(p => p.Id_Permiso == idPermisoFila);
                    row.Cells["Seleccionar"].Value = tienePermiso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al marcar permisos del rol: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------Editar permisos de un rol
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

    }
}
