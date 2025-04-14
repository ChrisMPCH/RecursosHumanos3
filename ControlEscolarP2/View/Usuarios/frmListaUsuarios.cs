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
            // Crear un diccionario con los valores
            Dictionary<int, string> list_roles = new Dictionary<int, string>
            {
                { 0, "----------" },
                { 1, "ADMIN" },
                { 2, "RH_MANAGER" },
                { 3, "RH_ANALYST" },
                { 4, "SUPERVISOR" },
            };

            // Asignar el diccionario al ComboBox
            cbRoles.DataSource = new BindingSource(list_roles, null);
            cbRoles.DisplayMember = "Value";  // Lo que se muestra
            cbRoles.ValueMember = "Key";      // Lo que se guarda como SelectedValue

            cbRoles.SelectedValue = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if ((int)cbRoles.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un rol para buscar", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FormLoad();
                return;
            }

            // Obtener el nombre del rol desde el diccionario
            string rolSeleccionado = ((KeyValuePair<int, string>)cbRoles.SelectedItem).Value;

            // Filtrar la lista de usuarios por el rol seleccionado
            var listaConFiltro = listaUsuarios.Where(x => x.Rol == rolSeleccionado).ToList();

            // Asignar la lista filtrada al DataGridView
            dataGridUsuarios.DataSource = listaConFiltro;
        }

        //--------------------------------------------------------------------------------Llenado tabla
        List<Usuario> listaUsuarios;
        private void FormLoad()
        {
            // Lista de usuarios con datos simulados
            listaUsuarios = new List<Usuario>
            {
            new Usuario { Nombre = "Juan Pérez", FechaCreacion = DateTime.Now.AddMonths(-3), UltimoAcceso = DateTime.Now, Estatus = "Activo", Rol = "ADMIN" },
            new Usuario { Nombre = "Ana Gómez", FechaCreacion = DateTime.Now.AddMonths(-2), UltimoAcceso = DateTime.Now.AddDays(-1), Estatus = "Inactivo", Rol = "RH_MANAGER" },
            new Usuario { Nombre = "Carlos López", FechaCreacion = DateTime.Now.AddMonths(-1), UltimoAcceso = DateTime.Now.AddDays(-5), Estatus = "Activo", Rol = "SUPERVISOR" }
            };

            // Asignar la lista al DataGridView
            dataGridUsuarios.DataSource = listaUsuarios;
        }

        public class Usuario
        {
            public string Nombre { get; set; }
            public DateTime FechaCreacion { get; set; }
            public DateTime UltimoAcceso { get; set; }
            public string Estatus { get; set; }
            public string Rol { get; set; }
        }


    }
}
