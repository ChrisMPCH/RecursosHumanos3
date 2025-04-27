namespace RecursosHumanos.View
{
    partial class frmListadoDepartamentos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            separador1 = new Guna.UI2.WinForms.Guna2Separator();
            lblFechaNacimiento = new Label();
            dtpFechaIngreso = new Guna.UI2.WinForms.Guna2DateTimePicker();
            guna2ContainerControl1 = new Guna.UI2.WinForms.Guna2ContainerControl();
            pnlInfoUsuario = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblInfo = new Label();
            dgvDepartamentos = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlInfoUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).BeginInit();
            SuspendLayout();
            // 
            // separador1
            // 
            separador1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            separador1.Location = new Point(14, 71);
            separador1.Margin = new Padding(3, 4, 3, 4);
            separador1.Name = "separador1";
            separador1.Size = new Size(1126, 13);
            separador1.TabIndex = 3;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.Anchor = AnchorStyles.Right;
            lblFechaNacimiento.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblFechaNacimiento.ForeColor = Color.Black;
            lblFechaNacimiento.Location = new Point(1683, 913);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(173, 79);
            lblFechaNacimiento.TabIndex = 7;
            lblFechaNacimiento.Text = "Fecha de Nacimiento: ";
            // 
            // dtpFechaIngreso
            // 
            dtpFechaIngreso.Anchor = AnchorStyles.Right;
            dtpFechaIngreso.BackColor = Color.Transparent;
            dtpFechaIngreso.Checked = true;
            dtpFechaIngreso.CustomizableEdges = customizableEdges7;
            dtpFechaIngreso.FillColor = Color.White;
            dtpFechaIngreso.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaIngreso.Format = DateTimePickerFormat.Short;
            dtpFechaIngreso.ImeMode = ImeMode.NoControl;
            dtpFechaIngreso.Location = new Point(1882, 928);
            dtpFechaIngreso.Margin = new Padding(3, 4, 3, 4);
            dtpFechaIngreso.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFechaIngreso.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFechaIngreso.Name = "dtpFechaIngreso";
            dtpFechaIngreso.RightToLeft = RightToLeft.No;
            dtpFechaIngreso.ShadowDecoration.CustomizableEdges = customizableEdges8;
            dtpFechaIngreso.Size = new Size(176, 41);
            dtpFechaIngreso.TabIndex = 5;
            dtpFechaIngreso.Value = new DateTime(2025, 3, 7, 0, 3, 12, 692);
            // 
            // guna2ContainerControl1
            // 
            guna2ContainerControl1.CustomizableEdges = customizableEdges9;
            guna2ContainerControl1.Location = new Point(7, 333);
            guna2ContainerControl1.Margin = new Padding(3, 4, 3, 4);
            guna2ContainerControl1.Name = "guna2ContainerControl1";
            guna2ContainerControl1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2ContainerControl1.Size = new Size(229, 267);
            guna2ContainerControl1.TabIndex = 52;
            guna2ContainerControl1.Text = "guna2ContainerControl1";
            // 
            // pnlInfoUsuario
            // 
            pnlInfoUsuario.BackColor = Color.White;
            pnlInfoUsuario.Controls.Add(dgvDepartamentos);
            pnlInfoUsuario.Controls.Add(lblInfo);
            pnlInfoUsuario.Controls.Add(guna2ContainerControl1);
            pnlInfoUsuario.Controls.Add(dtpFechaIngreso);
            pnlInfoUsuario.Controls.Add(lblFechaNacimiento);
            pnlInfoUsuario.Controls.Add(separador1);
            pnlInfoUsuario.CustomizableEdges = customizableEdges11;
            pnlInfoUsuario.Dock = DockStyle.Fill;
            pnlInfoUsuario.Font = new Font("Microsoft Sans Serif", 8.25F);
            pnlInfoUsuario.Location = new Point(0, 0);
            pnlInfoUsuario.Margin = new Padding(3, 4, 3, 4);
            pnlInfoUsuario.Name = "pnlInfoUsuario";
            pnlInfoUsuario.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pnlInfoUsuario.Size = new Size(1155, 1071);
            pnlInfoUsuario.TabIndex = 27;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.Location = new Point(21, 17);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(1128, 49);
            lblInfo.TabIndex = 54;
            lblInfo.Text = "Listado de Departamentos";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvDepartamentos
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            dgvDepartamentos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvDepartamentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvDepartamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvDepartamentos.DefaultCellStyle = dataGridViewCellStyle6;
            dgvDepartamentos.GridColor = Color.FromArgb(231, 229, 255);
            dgvDepartamentos.Location = new Point(14, 91);
            dgvDepartamentos.Name = "dgvDepartamentos";
            dgvDepartamentos.RowHeadersVisible = false;
            dgvDepartamentos.RowHeadersWidth = 51;
            dgvDepartamentos.Size = new Size(1126, 567);
            dgvDepartamentos.TabIndex = 55;
            dgvDepartamentos.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvDepartamentos.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvDepartamentos.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvDepartamentos.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvDepartamentos.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvDepartamentos.ThemeStyle.BackColor = Color.White;
            dgvDepartamentos.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvDepartamentos.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvDepartamentos.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDepartamentos.ThemeStyle.HeaderStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dgvDepartamentos.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvDepartamentos.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDepartamentos.ThemeStyle.HeaderStyle.Height = 4;
            dgvDepartamentos.ThemeStyle.ReadOnly = false;
            dgvDepartamentos.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvDepartamentos.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDepartamentos.ThemeStyle.RowsStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dgvDepartamentos.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvDepartamentos.ThemeStyle.RowsStyle.Height = 29;
            dgvDepartamentos.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvDepartamentos.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // frmListadoDepartamentos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 1071);
            Controls.Add(pnlInfoUsuario);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmListadoDepartamentos";
            Text = "frmListadoUsuarios";
            pnlInfoUsuario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Separator separador1;
        private Label lblFechaNacimiento;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaIngreso;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl1;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlInfoUsuario;
        private Label lblInfo;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDepartamentos;
    }
}