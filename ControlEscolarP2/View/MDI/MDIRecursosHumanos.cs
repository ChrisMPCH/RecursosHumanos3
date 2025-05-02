using RecursosHumanos.Controller;
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
            VerificarPermisos();
        }

        private void MDIRecursosHumanos_Load()
        {
            ActualizarActividadReciente();
            ActualizarEstadisticas(); // Llama a la función para actualizar las estadísticas al cargar el formulario
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

        //-------------------------------------------------------------------------------Permisos

        /// <summary>
        /// Verifica los permisos del usuario actual y habilita o deshabilita los botones del menú según corresponda.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public static void VerificarPermisos()
        {
            if (permisosUsuario == null || permisosUsuario.Count == 0)
            {
                MessageBox.Show("No se han asignado permisos para este usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Usuarios
            if (permisosUsuario.Contains(22)) // Ver usuarios
            {
            btnLisUsuarios.Enabled = true;
            }
            if (permisosUsuario.Contains(23) || permisosUsuario.Contains(36)) // Agregar usuario y empleado
            {
                subRegistroPersonas.Enabled = true;
            }
            if (permisosUsuario.Contains(24)) // Editar usuario
            {
            btnActualizarUsuarios.Enabled = true;
            }
            if (permisosUsuario.Contains(25)) // Eliminar usuario
            {
            btnEliminarUsuarios.Enabled = true;
            }

            // Roles
            if (permisosUsuario.Contains(26)) // Ver roles
            {
            btnGestionRoles.Enabled = true;
            }
            if (permisosUsuario.Contains(27) || permisosUsuario.Contains(30)) // ver permisos de rol
            {
                btnCreacionRoles.Enabled = true;

            }

            // Permisos
            if (permisosUsuario.Contains(30)) // Ver permisos
            {
            btnCreacionRoles.Enabled = true;
            }

            // Empleados
            if (permisosUsuario.Contains(35)) // Ver empleados
            {
            btnListaEmpleados.Enabled = true;
            }
            if (permisosUsuario.Contains(37)) // Editar empleados
            {
            btnActualizarEmpleados.Enabled = true;
            }
            if (permisosUsuario.Contains(38)) // Eliminar empleados
            {
            btnEliminarEmpleados.Enabled = true;
            }

            // Contratos
            if (permisosUsuario.Contains(39)) // Ver contratos
            {
            btnReportes.Enabled = true;
            }
            if (permisosUsuario.Contains(40)) // Agregar contrato
            {
            btnContratos.Enabled = true;
            }
            if (permisosUsuario.Contains(41)) // Editar contrato
            {
            btnListaContratos.Enabled = true;
            }

            // Bitácora 
            if (permisosUsuario.Contains(43)) // Ver bitácora
            {
            btnRegistroAuditorias.Enabled = true;
            }

            // Puestos
            if (permisosUsuario.Contains(45)) // Ver puestos
            {
                btnPuestos.Enabled = true;

            }

            // Departamentos
            if (permisosUsuario.Contains(49)) // Ver departamentos
            {
                btnDepartamentos.Enabled = true;

            }
            
            // Asistencias
            if (permisosUsuario.Contains(53)) // Gestionar asistencias
            {
                btnEntradas.Enabled = true;
            }

            // Ausencias
            if (permisosUsuario.Contains(54)) // Gestionar ausencias
            {
                btnSalidas.Enabled = true;
            }

            // Contar días trabajados
            if (permisosUsuario.Contains(55)) // Gestionar días trabajados
            {
                btnContarDias.Enabled = true;
            }

        }

        private void ActualizarEstadisticas()
        {
            try
            {
                // Porcentaje de empleados activos
                EmpleadosController empleadoController = new EmpleadosController();
                double porcentajeEmpleadosActivos = empleadoController.ObtenerPorcentajeEmpleadosActivos();
                lblEmpleadosActNumero.Text = $"{porcentajeEmpleadosActivos:F0}%"; // Formato sin decimales

                // Porcentaje de asistencias hoy (ejemplo, necesitarás un método similar)
                // Porcentaje de asistencias hoy
               // AsistenciaController asistenciaController = new AsistenciaController();
                //double porcentajeAsistenciasHoy = asistenciaController.ObtenerPorcentajeAsistenciasHoy();
                //lblAsistenciaNumero.Text = $"{porcentajeAsistenciasHoy:F0}%";
                // Porcentaje de cumpleaños (ejemplo, necesitarás un método similar)

                // Porcentaje de contratos activos (ejemplo, necesitarás un método similar)
                ContratoController contratoController = new ContratoController();
                double porcentajeContratosActivos = contratoController.ObtenerPorcentajeContratosActivos();
                lblContratosActNumero.Text = $"{porcentajeContratosActivos:F0}%";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar las estadísticas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActualizarActividadReciente()
        {
            try
            {
                AuditoriasController auditoriasController = new AuditoriasController();
                // Obtener las últimas 3 auditorías activas
                var auditorias = auditoriasController.ObtenerAuditorias(estatus: 1)
                    .OrderByDescending(a => a.Fecha_Movimiento)
                    .Take(3)
                    .ToList();

                // Limpiar controles
                lblUsuario1.Text = "";
                lblAccion1.Text = "";
                lblTempo1.Text = "";
                lblUsuario2.Text = "";
                lblAccion2.Text = "";
                lblTempo2.Text = "";
                lblUsuario3.Text = "";
                lblAccion3.Text = "";
                lblTempo3.Text = "";

                // Mostrar las auditorías
                if (auditorias.Count > 0)
                {
                    lblUsuario1.Text = auditorias[0].UsuarioResponsable?.UsuarioNombre ?? $"Usuario {auditorias[0].Id_Usuario}";
                    lblAccion1.Text = auditorias[0].Detalle;
                    lblTempo1.Text = CalcularTiempo(auditorias[0].Fecha_Movimiento);
                }
                if (auditorias.Count > 1)
                {
                    lblUsuario2.Text = auditorias[1].UsuarioResponsable?.UsuarioNombre ?? $"Usuario {auditorias[1].Id_Usuario}";
                    lblAccion2.Text = auditorias[1].Detalle;
                    lblTempo2.Text = CalcularTiempo(auditorias[1].Fecha_Movimiento);
                }
                if (auditorias.Count > 2)
                {
                    lblUsuario3.Text = auditorias[2].UsuarioResponsable?.UsuarioNombre ?? $"Usuario {auditorias[2].Id_Usuario}";
                    lblAccion3.Text = auditorias[2].Detalle;
                    lblTempo3.Text = CalcularTiempo(auditorias[2].Fecha_Movimiento);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la actividad reciente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CalcularTiempo(DateTime fechaHora)
        {
            TimeSpan diferencia = DateTime.Now - fechaHora;
            if (diferencia.TotalMinutes < 1)
                return "Hace menos de 1 min";
            else if (diferencia.TotalMinutes < 60)
                return $"Hace {(int)diferencia.TotalMinutes} min";
            else if (diferencia.TotalHours < 24)
                return $"Hace {(int)diferencia.TotalHours} horas";
            else
                return $"Hace {(int)diferencia.TotalDays} días";
        }
    }
}
