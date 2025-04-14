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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            separador1 = new Guna.UI2.WinForms.Guna2Separator();
            lblFechaNacimiento = new Label();
            dtpFechaIngreso = new Guna.UI2.WinForms.Guna2DateTimePicker();
            guna2ContainerControl1 = new Guna.UI2.WinForms.Guna2ContainerControl();
            pnlTabla = new Guna.UI2.WinForms.Guna2Panel();
            dataGridEmpleados = new Guna.UI2.WinForms.Guna2DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            Ubicación = new DataGridViewTextBoxColumn();
            Teléfono = new DataGridViewTextBoxColumn();
            Correo = new DataGridViewTextBoxColumn();
            pnlInfoUsuario = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblInfo = new Label();
            pnlTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).BeginInit();
            pnlInfoUsuario.SuspendLayout();
            SuspendLayout();
            // 
            // separador1
            // 
            separador1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            separador1.Location = new Point(12, 53);
            separador1.Name = "separador1";
            separador1.Size = new Size(985, 10);
            separador1.TabIndex = 3;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.Anchor = AnchorStyles.Right;
            lblFechaNacimiento.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblFechaNacimiento.ForeColor = Color.Black;
            lblFechaNacimiento.Location = new Point(1473, 685);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(151, 59);
            lblFechaNacimiento.TabIndex = 7;
            lblFechaNacimiento.Text = "Fecha de Nacimiento: ";
            // 
            // dtpFechaIngreso
            // 
            dtpFechaIngreso.Anchor = AnchorStyles.Right;
            dtpFechaIngreso.BackColor = Color.Transparent;
            dtpFechaIngreso.Checked = true;
            dtpFechaIngreso.CustomizableEdges = customizableEdges1;
            dtpFechaIngreso.FillColor = Color.White;
            dtpFechaIngreso.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaIngreso.Format = DateTimePickerFormat.Short;
            dtpFechaIngreso.ImeMode = ImeMode.NoControl;
            dtpFechaIngreso.Location = new Point(1647, 696);
            dtpFechaIngreso.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFechaIngreso.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFechaIngreso.Name = "dtpFechaIngreso";
            dtpFechaIngreso.RightToLeft = RightToLeft.No;
            dtpFechaIngreso.ShadowDecoration.CustomizableEdges = customizableEdges2;
            dtpFechaIngreso.Size = new Size(154, 31);
            dtpFechaIngreso.TabIndex = 5;
            dtpFechaIngreso.Value = new DateTime(2025, 3, 7, 0, 3, 12, 692);
            // 
            // guna2ContainerControl1
            // 
            guna2ContainerControl1.CustomizableEdges = customizableEdges3;
            guna2ContainerControl1.Location = new Point(6, 250);
            guna2ContainerControl1.Name = "guna2ContainerControl1";
            guna2ContainerControl1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2ContainerControl1.Size = new Size(200, 200);
            guna2ContainerControl1.TabIndex = 52;
            guna2ContainerControl1.Text = "guna2ContainerControl1";
            // 
            // pnlTabla
            // 
            pnlTabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTabla.Controls.Add(dataGridEmpleados);
            pnlTabla.CustomizableEdges = customizableEdges5;
            pnlTabla.Location = new Point(9, 69);
            pnlTabla.Name = "pnlTabla";
            pnlTabla.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlTabla.Size = new Size(996, 722);
            pnlTabla.TabIndex = 53;
            // 
            // dataGridEmpleados
            // 
            dataGridEmpleados.AllowUserToAddRows = false;
            dataGridEmpleados.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridEmpleados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(1);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridEmpleados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridEmpleados.ColumnHeadersHeight = 15;
            dataGridEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridEmpleados.Columns.AddRange(new DataGridViewColumn[] { Nombre, Ubicación, Teléfono, Correo });
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.White;
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle7.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle7.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            dataGridEmpleados.DefaultCellStyle = dataGridViewCellStyle7;
            dataGridEmpleados.Dock = DockStyle.Fill;
            dataGridEmpleados.GridColor = Color.FromArgb(231, 229, 255);
            dataGridEmpleados.Location = new Point(0, 0);
            dataGridEmpleados.Name = "dataGridEmpleados";
            dataGridEmpleados.RowHeadersVisible = false;
            dataGridEmpleados.RowHeadersWidth = 51;
            dataGridEmpleados.Size = new Size(996, 722);
            dataGridEmpleados.TabIndex = 0;
            dataGridEmpleados.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridEmpleados.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridEmpleados.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridEmpleados.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridEmpleados.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridEmpleados.ThemeStyle.BackColor = Color.White;
            dataGridEmpleados.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridEmpleados.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridEmpleados.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridEmpleados.ThemeStyle.HeaderStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridEmpleados.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridEmpleados.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridEmpleados.ThemeStyle.HeaderStyle.Height = 15;
            dataGridEmpleados.ThemeStyle.ReadOnly = false;
            dataGridEmpleados.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridEmpleados.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridEmpleados.ThemeStyle.RowsStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridEmpleados.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridEmpleados.ThemeStyle.RowsStyle.Height = 25;
            dataGridEmpleados.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridEmpleados.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // Nombre
            // 
            Nombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Nombre.DefaultCellStyle = dataGridViewCellStyle3;
            Nombre.FillWeight = 182.057571F;
            Nombre.HeaderText = "Nombre";
            Nombre.MaxInputLength = 60;
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.Width = 200;
            // 
            // Ubicación
            // 
            Ubicación.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Ubicación.DefaultCellStyle = dataGridViewCellStyle4;
            Ubicación.FillWeight = 108.153435F;
            Ubicación.HeaderText = "Ubicación";
            Ubicación.MaxInputLength = 50;
            Ubicación.MinimumWidth = 6;
            Ubicación.Name = "Ubicación";
            Ubicación.Resizable = DataGridViewTriState.False;
            Ubicación.Width = 200;
            // 
            // Teléfono
            // 
            Teléfono.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Teléfono.DefaultCellStyle = dataGridViewCellStyle5;
            Teléfono.FillWeight = 94.41109F;
            Teléfono.HeaderText = "Teléfono";
            Teléfono.MaxInputLength = 100;
            Teléfono.MinimumWidth = 6;
            Teléfono.Name = "Teléfono";
            Teléfono.SortMode = DataGridViewColumnSortMode.Programmatic;
            Teléfono.Width = 200;
            // 
            // Correo
            // 
            Correo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Correo.DefaultCellStyle = dataGridViewCellStyle6;
            Correo.FillWeight = 51.9261055F;
            Correo.HeaderText = "Correo";
            Correo.MaxInputLength = 100;
            Correo.MinimumWidth = 6;
            Correo.Name = "Correo";
            Correo.Width = 200;
            // 
            // pnlInfoUsuario
            // 
            pnlInfoUsuario.BackColor = Color.White;
            pnlInfoUsuario.Controls.Add(lblInfo);
            pnlInfoUsuario.Controls.Add(pnlTabla);
            pnlInfoUsuario.Controls.Add(guna2ContainerControl1);
            pnlInfoUsuario.Controls.Add(dtpFechaIngreso);
            pnlInfoUsuario.Controls.Add(lblFechaNacimiento);
            pnlInfoUsuario.Controls.Add(separador1);
            pnlInfoUsuario.CustomizableEdges = customizableEdges7;
            pnlInfoUsuario.Dock = DockStyle.Fill;
            pnlInfoUsuario.Font = new Font("Microsoft Sans Serif", 8.25F);
            pnlInfoUsuario.Location = new Point(0, 0);
            pnlInfoUsuario.Name = "pnlInfoUsuario";
            pnlInfoUsuario.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pnlInfoUsuario.Size = new Size(1011, 803);
            pnlInfoUsuario.TabIndex = 27;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.Location = new Point(18, 13);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(987, 37);
            lblInfo.TabIndex = 54;
            lblInfo.Text = "Listado de Departamentos";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmListadoDepartamentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 803);
            Controls.Add(pnlInfoUsuario);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmListadoDepartamentos";
            Text = "frmListadoUsuarios";
            pnlTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).EndInit();
            pnlInfoUsuario.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Separator separador1;
        private Label lblFechaNacimiento;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaIngreso;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl1;
        private Guna.UI2.WinForms.Guna2Panel pnlTabla;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridEmpleados;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlInfoUsuario;
        private Label lblInfo;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Ubicación;
        private DataGridViewTextBoxColumn Teléfono;
        private DataGridViewTextBoxColumn Correo;
    }
}