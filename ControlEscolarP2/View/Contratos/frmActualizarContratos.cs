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

namespace RecursosHumanos.View.Contratos
{
    public partial class frmActualizarContratos : Form
    {
        private readonly ContratoController _contratosController = new ContratoController();

        public frmActualizarContratos()
        {
            InitializeComponent();
            IniciarTabla();
            InicializarCampos();
            Panel pnlActualizar = splitContainer1.Panel1;
            Panel pnlListaContratos = splitContainer1.Panel2;
            splitContainer1.Panel1Collapsed = true;
            PoblaComboTipoContrato();
            dtpFechaInicio1.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            txtSalario.KeyPress += txtSalario_KeyPress;
        }
        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridContratos); // Configurar el estilo del DataGridView
            ConfigurarColumnas(); // Agregar columnas personalizadas
        }
        public static void InicializarCampos()
        {
            Formas.ConfigurarTextBox(txtMatricula1, "Ingresa tu matricula");

        }
        private void PoblaComboTipoContrato()
        {
            Dictionary<int, string> list_tipoCon = new Dictionary<int, string>
            {
                { 1, "Fijo" },
                { 2, "Temporal" }
            };

            cbxTipoContrato1.DataSource = new BindingSource(list_tipoCon, null);
            cbxTipoContrato1.DisplayMember = "Value";
            cbxTipoContrato1.ValueMember = "Key";

            cbxTipoContrato1.SelectedValue = 1;
        }

        private void ConfigurarColumnas()
        {
            dataGridContratos.Columns.Clear();
            dataGridContratos.AutoGenerateColumns = false;

            // ID oculto para manejar edición
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "Id_Contrato";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id_Contrato";
            colId.Visible = false;
            dataGridContratos.Columns.Add(colId);

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Matricula",
                HeaderText = "Matrícula",
                DataPropertyName = "Matricula"
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "NombreEmpleado"
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoContrato",
                HeaderText = "Tipo Contrato",
                DataPropertyName = "NombreTipoContrato"
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Salario",
                HeaderText = "Salario",
                DataPropertyName = "Sueldo",
                DefaultCellStyle = { Format = "C2" } // Formato moneda
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaInicio",
                HeaderText = "Fecha Inicio",
                DataPropertyName = "FechaInicio",
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaFin",
                HeaderText = "Fecha Fin",
                DataPropertyName = "FechaFin",
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoraEntrada",
                HeaderText = "Hora Entrada",
                DataPropertyName = "HoraEntrada",
                DefaultCellStyle = { Format = @"hh\:mm" } // TimeSpan
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoraSalida",
                HeaderText = "Hora Salida",
                DataPropertyName = "HoraSalida",
                DefaultCellStyle = { Format = @"hh\:mm" }
            });

            dataGridContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion"
            });

            dataGridContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridContratos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridContratos.ReadOnly = true;
            dataGridContratos.AllowUserToAddRows = false;
            dataGridContratos.AllowUserToDeleteRows = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridContratos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un contrato para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener el ID del contrato seleccionado
            int idContrato = Convert.ToInt32(dataGridContratos.SelectedRows[0].Cells["Id_Contrato"].Value);

            // Obtener los detalles desde el controller
            var contrato = _contratosController.ObtenerDetalleContrato(idContrato);

            if (contrato == null)
            {
                MessageBox.Show("No se pudo obtener la información del contrato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que no esté finalizado o cancelado (inactivo)
            if (!contrato.Estatus || contrato.FechaFin.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Este contrato está finalizado o cancelado y no puede ser editado.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar u ocultar el panel de actualización
            if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
                btnActualizar.Text = "Ocultar Actualizar";

                // Cargar nuevamente los contratos (opcional)
                string matricula = txtMatricula1.Text.Trim();
                if (!string.IsNullOrWhiteSpace(matricula) && matricula != "Ingresa tu matricula")
                {
                    CargarContratos(matricula);
                }
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
                btnActualizar.Text = "Actualizar";
            }

        }

        private void CargarContratos(string matricula)
        {
            try
            {
                var contratos = _contratosController.ObtenerTodosLosContratosPorMatricula(matricula);
                dataGridContratos.DataSource = null;

                if (contratos == null || contratos.Count == 0)
                {
                    MessageBox.Show("No se encontraron contratos para esta matrícula.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Id_Contrato", typeof(int));
                dt.Columns.Add("Matricula", typeof(string));
                dt.Columns.Add("TipoContrato", typeof(string));
                dt.Columns.Add("FechaInicio", typeof(string));
                dt.Columns.Add("FechaFin", typeof(string));
                dt.Columns.Add("HoraEntrada", typeof(string));
                dt.Columns.Add("HoraSalida", typeof(string));
                dt.Columns.Add("Sueldo", typeof(string));
                dt.Columns.Add("Descripcion", typeof(string));
                dt.Columns.Add("EstadoLaboral", typeof(string));

                foreach (var contrato in contratos)
                {
                    dt.Rows.Add(
                        contrato.Id_Contrato,
                        contrato.Matricula,
                        contrato.Id_TipoContrato.ToString(), // podrías cambiarlo por el nombre si lo agregas después
                        contrato.FechaInicio.ToString("dd/MM/yyyy"),
                        contrato.FechaFin.ToString("dd/MM/yyyy"),
                        contrato.HoraEntrada.ToString(@"hh\:mm"),
                        contrato.HoraSalida.ToString(@"hh\:mm"),
                        contrato.Sueldo.ToString("C2"),
                        contrato.Descripcion,
                        contrato.Estatus ? "Activo" : "Inactivo"
                    );
                }

                dataGridContratos.DataSource = dt;
                ConfigurarColumnas(); // Asegúrate de que esta coincida con las columnas de arriba
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar contratos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula1.Text.Trim();

            if (string.IsNullOrWhiteSpace(matricula) )
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Contrato contratoActualizado = new Contrato
            {
                Matricula = txtMatricula1.Text.Trim(),
                Id_TipoContrato = ((KeyValuePair<int, string>)cbxTipoContrato1.SelectedItem).Key,
                Sueldo = double.TryParse(txtSalario.Text, out double sueldo) ? sueldo : 0,
                FechaInicio = dtpFechaInicio1.Value.Date,
                FechaFin = dtpFechaFin.Value.Date,
                HoraEntrada = dtpHoraEntrada.Value.TimeOfDay,
                HoraSalida = dtpHoraSalida.Value.TimeOfDay,
                Descripcion = txtDescripcion.Text.Trim(),
                Estatus = true
            };

            var resultado = _contratosController.ActualizarContrato(contratoActualizado);

            if (resultado.exito)
            {
                MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                splitContainer1.Panel1Collapsed = true;
                btnActualizar.Text = "Actualizar";
                CargarContratos(txtMatricula1.Text.Trim());
            }
            else
            {
                MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (BuscarMatricula())
            {
                MessageBox.Show("Cargando datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool BuscarMatricula()
        {
            if (string.IsNullOrWhiteSpace(txtMatricula1.Text) || txtMatricula1.Text == "Ingresa tu matricula")
            {
                MessageBox.Show("Por favor, ingrese su matrícula.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!EmpleadoNegocio.EsNoMatriculaValido(txtMatricula1.Text.Trim()))
            {
                MessageBox.Show("Número de matrícula inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        // Método para validar que solo se ingresen números en el campo Salario
        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea cualquier carácter que no sea número
            }
        }
    }
}
