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

namespace RecursosHumanos.View
{
    public partial class frmInasistencias : Form
    {
        public frmInasistencias()
        {
            InitializeComponent();
            InicializarCampos();
        }

        // Inicializa los campos de texto con texto predeterminado
        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula1, "Ingresa tu matricula");
            Formas.ConfigurarTextBox(txtAusencia, "Ingresa una descripcion");
        }

        // Evento que se ejecuta al hacer clic en "Buscar"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Validar matrícula
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
                MessageBox.Show("Datos cargados", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        // Evento que se ejecuta al hacer clic en "Aceptar"
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar matrícula
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
                {
                    MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (txtAusencia.Text == "" || txtAusencia.Text== "Ingresa una descripcion") // Verifica si está vacío
            {
                MessageBox.Show("Por favor, ingrese el motivo de la ausencia.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (!EmpleadoNegocio.ValidarFechas(dtpFechaInicio1.Value, dtpFechaFin.Value))
            {
                MessageBox.Show("La fecha de entrada debe ser menor que la fecha de salida.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
             MessageBox.Show("Ausencia guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
