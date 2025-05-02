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
            ConfigurarHoraEntradaSalida(); // Configura DateTimePicker para hora

        }
        private void IniciarTabla()
        {
            Formas.ConfigurarEstiloDataGridView(dataGridContratos); // Configurar el estilo del DataGridView
            ConfigurarColumnas(); // Agregar columnas personalizadas
        }
        private void ConfigurarHoraEntradaSalida()
        {
            dtpHoraEntrada.Format = DateTimePickerFormat.Custom;
            dtpHoraEntrada.CustomFormat = "HH:mm"; // 24 horas (Ejemplo: 14:30)
            dtpHoraEntrada.ShowUpDown = true; // Solo permite cambiar la hora

            dtpHoraSalida.Format = DateTimePickerFormat.Custom;
            dtpHoraSalida.CustomFormat = "HH:mm";
            dtpHoraSalida.ShowUpDown = true;
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
            // Si ya está visible, oculta el panel y regresa
            if (!splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = true;
                btnActualizar.Text = "Actualizar";
                return;
            }

            // Validar que se haya seleccionado un contrato
            if (dataGridContratos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un contrato para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener el ID del contrato seleccionado
            int idContrato = Convert.ToInt32(dataGridContratos.SelectedRows[0].Cells["Id_Contrato"].Value);
            var contrato = _contratosController.ObtenerDetalleContrato(idContrato);

            if (contrato == null)
            {
                MessageBox.Show("No se pudo obtener la información del contrato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar contrato activo
            if (!contrato.Estatus || contrato.FechaFin < DateTime.Now.Date)
            {
                MessageBox.Show("Este contrato ya finalizó o está inactivo.", "No editable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar el panel y llenar los campos
            splitContainer1.Panel1Collapsed = false;
            btnActualizar.Text = "Ocultar Actualizar";

            txtMatriculaA.Text = contrato.Matricula;
            txtMatricula1.ReadOnly = true;
            txtDescripcion.Text = contrato.Descripcion;
            txtSalario.Text = contrato.Sueldo.ToString("F2");
            dtpFechaInicio1.Value = contrato.FechaInicio;
            dtpFechaFin.Value = contrato.FechaFin;
            dtpHoraEntrada.Value = DateTime.Today.Add(contrato.HoraEntrada);
            dtpHoraSalida.Value = DateTime.Today.Add(contrato.HoraSalida);
            cbxTipoContrato1.SelectedValue = contrato.Id_TipoContrato;
            btnGuardar.Tag = contrato.Id_Contrato;
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
                dt.Columns.Add("NombreTipoContrato", typeof(string)); 
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
                        contrato.NombreTipoContrato, 
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
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar contratos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula1.Text.Trim();

            if (string.IsNullOrWhiteSpace(matricula) || !EmpleadoNegocio.EsNoMatriculaValido(matricula))
            {
                MessageBox.Show("Matrícula inválida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(btnGuardar.Tag is int idContrato))
            {
                MessageBox.Show("No se ha seleccionado un contrato válido para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar sueldo y fechas
            if (!double.TryParse(txtSalario.Text, out double sueldo) || sueldo <= 0)
            {
                MessageBox.Show("Salario inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpFechaInicio1.Value.Date >= dtpFechaFin.Value.Date)
            {
                MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear objeto contrato con ID
            Contrato contratoActualizado = new Contrato
            {
                Id_Contrato = idContrato, // <--- aquí está la clave
                Matricula = matricula,
                Id_TipoContrato = ((KeyValuePair<int, string>)cbxTipoContrato1.SelectedItem).Key,
                Sueldo = sueldo,
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
                CargarContratos(matricula); // refresca la tabla
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
                string matricula = txtMatricula1.Text.Trim();
                CargarContratos(matricula); 
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
