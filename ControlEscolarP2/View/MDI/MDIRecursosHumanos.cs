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

        // Constructor del formulario principal, se inicializan los componentes y el menú MDI
        public MDIRecursosHumanos()
        {
            InitializeComponent(); // Inicializa los controles en el formulario
            MDIRecursosHumanos_Load();
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
        private void showSubMenu(Panel subMenu)
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
        private void picMenu_Click(object sender, EventArgs e)
        {
            Formas.CloseOtherForms(this);
        }


        //-----------------------------------------------------------------------Christopher
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

        private void btnPermisos_Click(object sender, EventArgs e)
        {

        }

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
            Form frmActualizarContrato = new frmActualizarContrato();
            abrirChildFormMDI(frmActualizarContrato);
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

        private void btnRegistroAuditorias_Click(object sender, EventArgs e)
        {
            Form frmAuditoria = new frmAuditoria();
            abrirChildFormMDI(frmAuditoria);
        }
    }
}
