using RecursosHumanos.Utilities;
using RecursosHumanos.View.Contratos;
using RecursosHumanos.View.Usuarios;
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
    // Form principal para la aplicación MDI de Recursos Humanos
    public partial class MDIRecursosHumanos : Form
    {
        public static Form? formActivada = null;
        public static List<int> permisosUsuario = frmLogin.permisosUsuario;


        // Constructor del formulario principal, se inicializan los componentes y el menú MDI
        public MDIRecursosHumanos()
        {
            InitializeComponent(); // Inicializa los controles en el formulario
            MDIRecursosHumanos_Load();
            //VerificarPermisos();
        }

        private void MDIRecursosHumanos_Load()
        {
            horafecha_Tick(); // Llama a la función para mostrar la hora y fecha en el formulario
            inicioMenuMDI(); // Llama a la función para inicializar el estado del menú MDI
        }

        //Elementos para la fecha y hora
        private void horafecha_Tick()
        {
            tiempo.Interval = 1000;
            tiempo.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHoraActual.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        // Función que oculta todos los submenús cuando se inicia la aplicación
        private void inicioMenuMDI()
        {
            subChristopherPanel.Visible = false;
            subFridaPanel.Visible = false;
            subNatalyPanel.Visible = false;
            subVanessaPanel.Visible = false;
            pnlSubRoles.Visible = false;
        }

        // Función para mostrar o esconder un submenú dependiendo de su estado actual
        public static void showSubMenu(Panel subMenu)
        {
            // Si el submenú no está visible, lo mostramos
            if (!subMenu.Visible)
            {
                subMenu.Visible = true; // Muestra el submenú seleccionado
            }
            else
            {
                subMenu.Visible = false; // Si ya está visible, lo ocultamos
            }
        }

        // Función para esconder un submenú dependiendo de su estado actual
        public static void showNoSubMenu(Panel subMenu)
        {
            // Si el submenú no está visible, lo mostramos
            subMenu.Visible = false; // Si ya está visible, lo ocultamos
        }

        private void picMenu_Click(object sender, EventArgs e)
        {
            Formas.CloseOtherForms(this);
        }


        //-----------------------------------------------------------------------Christopher
        #region
        private void btmChristopher_Click(object sender, EventArgs e)
        {
            showSubMenu(subChristopherPanel); // Muestra u oculta el submenú de Usuarios
        }
        private void registrarPersonas_Click(object sender, EventArgs e)
        {
            // Creamos la instancia del formulario hijo
            Form frmRegistroPersonas = new frmRegistroPersonas();

            // Usamos el método para abrir el formulario con las configuraciones estándar
            abrirChildFormMDI(frmRegistroPersonas);
        }

        private void btnLisUsuarios_Click(object sender, EventArgs e)
        {
            Form frmListaUsuarios = new frmListaUsuarios();
            abrirChildFormMDI(frmListaUsuarios);
        }

        private void btnActualizarUsuarios_Click(object sender, EventArgs e)
        {
            Form frmActualizarUsuario = new frmActualizarUsuario();
            abrirChildFormMDI(frmActualizarUsuario);
        }

        private void btnEliminarUsuarios_Click(object sender, EventArgs e)
        {
            Form frmEliminarUsuarios = new frmEliminarUsuario();
            abrirChildFormMDI(frmEliminarUsuarios);
        }

        private void btnRoles_Click_1(object sender, EventArgs e)
        {
            showSubMenu(pnlSubRoles); // Muestra u oculta el submenú de Roles
        }

        private void btnGestionRoles_Click(object sender, EventArgs e)
        {
            Form frmGestionRoles = new frmGestionRoles();
            abrirChildFormMDI(frmGestionRoles);
        }

        private void btnCreacionRoles_Click(object sender, EventArgs e)
        {
            Form frmGestionCreacionRoles = new frmGestionCreacionRoles();
            abrirChildFormMDI(frmGestionCreacionRoles);
        }

        #endregion
        //-----------------------------------------------------------------------Vanessa
        #region
        private void btmVanessa_Click(object sender, EventArgs e)
        {
            showSubMenu(subVanessaPanel); // Muestra u oculta el submenú de Vanessa
        }

        private void RegistroEmpleados_Click(object sender, EventArgs e)
        {
            Form frmRegistroEmpleado = new frmRegistroEmpleado();
            abrirChildFormMDI(frmRegistroEmpleado);
        }

        private void ListaEmpleados_Click(object sender, EventArgs e)
        {
            Form frmListaEmpleados = new frmListaEmpleados();
            abrirChildFormMDI(frmListaEmpleados);
        }

        private void ActualizarEmpleados_Click(object sender, EventArgs e)
        {
            Form frmActualizarEmpleado = new frmActualizarEmpleado();
            abrirChildFormMDI(frmActualizarEmpleado);
        }

        private void btnEliminarEmpleados_Click(object sender, EventArgs e)
        {
            Form frmEliminarEmpleado = new frmEliminarEmpleado();
            abrirChildFormMDI(frmEliminarEmpleado);
        }
        private void btnDepartamentos_Click(object sender, EventArgs e)
        {
            Form frmDepartamentos = new frmDepartamentos();
            abrirChildFormMDI(frmDepartamentos);
        }

        private void btnPuestos_Click(object sender, EventArgs e)
        {
            Form frmPuestos = new frmPuestos();
            abrirChildFormMDI(frmPuestos);
        }


        private void btnContratos_Click_1(object sender, EventArgs e)
        {
            Form frmContratos = new frmContratos();
            abrirChildFormMDI(frmContratos);
        }
        #endregion
        //-----------------------------------------------------------------------Nataly
        #region
        private void btnNataly_Click(object sender, EventArgs e)
        {
            showSubMenu(subNatalyPanel); // Muestra u oculta el submenú de Nataly
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            Form fmrReporte = new frmReportes();
            abrirChildFormMDI(fmrReporte);
        }

        private void btnEntradas_Click(object sender, EventArgs e)
        {
            Form frmEntrada = new frmEntrada();
            abrirChildFormMDI(frmEntrada);
        }

        private void btnSalidas_Click(object sender, EventArgs e)
        {
            Form frmInasistencias = new frmInasistencias();
            abrirChildFormMDI(frmInasistencias);
        }
        private void btnListaContratos_Click(object sender, EventArgs e)
        {
            Form frmActualizarContratos = new frmActualizarContratos();
            abrirChildFormMDI(frmActualizarContratos);
        }

        #endregion
        //-----------------------------------------------------------------------Frida
        #region
        private void btnFrida_Click(object sender, EventArgs e)
        {
            showSubMenu(subFridaPanel); // Muestra u oculta el submenú de Frida
        }



        private void btnContarDias_Click(object sender, EventArgs e)
        {
            Form frmDias_calculados = new frmDias_calculados();
            abrirChildFormMDI(frmDias_calculados);
        }

        #endregion
        //-----------------------------------------------------------------------Funciones de MDI
        private void abrirChildFormMDI(Form formChild)
        {
            if (formActivada != null)
            {
                formActivada.Close(); // Cierra cualquier formulario hijo abierto anteriormente
            }

            formActivada = formChild;

            formChild.TopLevel = false;  // Indica que este formulario es hijo del MDI
            formChild.FormBorderStyle = FormBorderStyle.None;  // Sin bordes
            formChild.Dock = DockStyle.Fill;  // Se ajusta al tamaño del panel
            panelChildForm.Controls.Add(formChild);  // Añade el formulario al panel MDI
            panelChildForm.Tag = formChild;

            formChild.BringToFront();  // Lo trae al frente
            formChild.Show();  // Muestra el formulario
        }

        private void lblInicio_Click(object sender, EventArgs e)
        {
            Formas.CloseOtherForms(this);
        }
        private void btmSalir_Click(object sender, EventArgs e)
        {
            // code
            this.Close();
        }
        public static void BloquearBotonesMenu()
        {
            btmUsuario.Enabled = false;
            btmVanessa.Enabled = false;
            btnNataly.Enabled = false;
            btnFrida.Enabled = false;
            btnRoles.Enabled = false;

            showNoSubMenu(subChristopherPanel);
            showNoSubMenu(subVanessaPanel);
            showNoSubMenu(subNatalyPanel);
            showNoSubMenu(subFridaPanel);
            showNoSubMenu(pnlSubRoles);

            btmSalir.Enabled = false;
        }

        public static void DesbloquearBotonesMenu()
        {
            btmUsuario.Enabled = true;
            btmVanessa.Enabled = true;
            btnNataly.Enabled = true;
            btnFrida.Enabled = true;
            btnRoles.Enabled = true;

            showSubMenu(subChristopherPanel);
            showSubMenu(subVanessaPanel);
            showSubMenu(subNatalyPanel);
            showSubMenu(subFridaPanel);
            showSubMenu(pnlSubRoles);

            btmSalir.Enabled = true;
        }

        private void btnRegistroAuditorias_Click(object sender, EventArgs e)
        {
            Form frmAuditoria = new frmAuditoria();
            abrirChildFormMDI(frmAuditoria);
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }


        //-------------------------------------------------------------------------------Permisos

        /// <summary>
        /// Verifica los permisos del usuario actual y habilita o deshabilita los botones del menú según corresponda.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        //private void VerificarPermisos()
        //{
        //    // Usuarios
        //    if (permisosUsuario.Contains(22)) // Ver usuarios
        //    {
        //        btmUsuario.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(23)) // Agregar usuario
        //    {
        //        btnAgregarUsuario.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(24)) // Editar usuario
        //    {
        //        btnEditarUsuario.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(25)) // Eliminar usuario
        //    {
        //        btnEliminarUsuarios.Enabled = true;
        //    }

        //    // Roles
        //    if (permisosUsuario.Contains(26)) // Ver roles
        //    {
        //        btnGestionRoles.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(27)) // Agregar rol
        //    {
        //        btnCreacionRoles.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(28)) // Editar rol
        //    {
        //        btnEditarRol.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(29)) // Eliminar rol
        //    {
        //        btnEliminarRol.Enabled = true;
        //    }

        //    // Permisos
        //    if (permisosUsuario.Contains(30)) // Ver permisos
        //    {
        //        btnVerPermisos.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(31)) // Ver asignaciones de permisos a roles
        //    {
        //        btnVerAsignacionesPermisos.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(32)) // Asignar permiso a rol
        //    {
        //        btnAsignarPermisoRol.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(33)) // Editar asignación de permiso a rol
        //    {
        //        btnEditarAsignacionPermiso.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(34)) // Eliminar asignación de permiso a rol
        //    {
        //        btnEliminarAsignacionPermiso.Enabled = true;
        //    }

        //    // Empleados
        //    if (permisosUsuario.Contains(35)) // Ver empleados
        //    {
        //        btnListaEmpleados.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(36)) // Agregar empleados
        //    {
        //        RegistroEmpleados.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(37)) // Editar empleados
        //    {
        //        btnEditarEmpleado.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(38)) // Eliminar empleados
        //    {
        //        btnEliminarEmpleado.Enabled = true;
        //    }

        //    // Contratos
        //    if (permisosUsuario.Contains(39)) // Ver contratos
        //    {
        //        btnListaContratos.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(40)) // Agregar contrato
        //    {
        //        btnAgregarContrato.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(41)) // Editar contrato
        //    {
        //        btnEditarContrato.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(42)) // Eliminar contrato
        //    {
        //        btnEliminarContrato.Enabled = true;
        //    }

        //    // Bitácora
        //    if (permisosUsuario.Contains(43)) // Ver bitácora
        //    {
        //        btnVerBitacora.Enabled = true;
        //    }
        //    if (permisosUsuario.Contains(44)) // Eliminar movimiento en bitácora
        //    {
        //        btnEliminarMovimiento.Enabled = true;
        //    }
        //}
    }
}
