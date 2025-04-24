using RecursosHumanos.Controller;
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
    public partial class frmListaUsuarios : Form
    {
        public frmListaUsuarios()
        {
            InitializeComponent();
            InicializarVentana();
        }
        public void InicializarVentana()
        {
            PoblaComboRol();
            IniciarTabla();
        }

        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridUsuarios);
            ConfigurarAnchoColumnas(300);
            FormLoad();
        }

        private void ConfigurarAnchoColumnas(int ancho)
        {
            foreach (DataGridViewColumn columna in dataGridUsuarios.Columns)
            {
                columna.Width = ancho; // Asignar un ancho fijo a todas las columnas
            }
        }

        private void PoblaComboRol()
        {
            try
            {
                RolesController controller = new RolesController();
                var roles = controller.ObtenerRolesActivos();
                roles.Insert(0, new Rol { Id_Rol = 0, Nombre = "----------" });

                cbRoles.DataSource = roles;
                cbRoles.DisplayMember = "Nombre";
                cbRoles.ValueMember = "Id_Rol";
                cbRoles.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int idRol = (int)cbRoles.SelectedValue;

            if (idRol == 0)
            {
                MessageBox.Show("Seleccione un rol para buscar", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuariosFiltrados = listaUsuarios.Where(x => x.Id_Rol == idRol).Select(u => new
            {
                Nombre = u.UsuarioNombre,
                FechaCreacion = u.Fecha_Creacion,
                UltimoAcceso = u.Fecha_Ultimo_Acceso,
                Estatus = u.Estatus == 1 ? "Activo" : "Inactivo",
                Rol = u.Rol.Nombre
            }).ToList();

            dataGridUsuarios.DataSource = usuariosFiltrados;
        }

        //--------------------------------------------------------------------------------Llenado tabla
        List<Usuario> listaUsuarios;
        private void FormLoad()
        {
            cargarUsuariosTabla();
        }

        private void cargarUsuariosTabla() 
        {
            try
            {
                UsuariosController controller = new UsuariosController();
                listaUsuarios = controller.ObtenerUsuarios();

                // Puedes transformar la lista para que solo muestre campos visibles si quieres
                var datosMostrar = listaUsuarios.Select(u => new
                {
                    Nombre = u.UsuarioNombre,
                    FechaCreacion = u.Fecha_Creacion,
                    UltimoAcceso = u.Fecha_Ultimo_Acceso,
                    Estatus = u.Estatus == 1 ? "Activo" : "Inactivo",
                    Rol = u.Rol.Nombre
                }).ToList();

                dataGridUsuarios.DataSource = datosMostrar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
