namespace RecursosHumanos.View
{
    partial class frmListaEmpleados
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitulo = new Label();
            separador1 = new Guna.UI2.WinForms.Guna2Separator();
            lblFechaNacimiento = new Label();
            dtpFechaIngreso = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblDepartamento = new Label();
            cbxDepartamento = new Guna.UI2.WinForms.Guna2ComboBox();
            btnBuscar = new Guna.UI2.WinForms.Guna2Button();
            guna2ContainerControl1 = new Guna.UI2.WinForms.Guna2ContainerControl();
            pnlTabla = new Guna.UI2.WinForms.Guna2Panel();
            dataGridEmpleados = new Guna.UI2.WinForms.Guna2DataGridView();
            Matricula = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Departamento = new DataGridViewTextBoxColumn();
            Puesto = new DataGridViewTextBoxColumn();
            Estatus = new DataGridViewTextBoxColumn();
            pnlInfoUsuario = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblInfo = new Label();
            Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            pnlTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).BeginInit();
            pnlInfoUsuario.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = SystemColors.ControlText;
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Century Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1011, 64);
            lblTitulo.TabIndex = 25;
            lblTitulo.Text = "Listado de empleados";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // separador1
            // 
            separador1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            separador1.Location = new Point(12, 115);
            separador1.Name = "separador1";
            separador1.Size = new Size(971, 10);
            separador1.TabIndex = 3;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.Anchor = AnchorStyles.Right;
            lblFechaNacimiento.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblFechaNacimiento.ForeColor = Color.Black;
            lblFechaNacimiento.Location = new Point(1473, 653);
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
            dtpFechaIngreso.Location = new Point(1647, 664);
            dtpFechaIngreso.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFechaIngreso.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFechaIngreso.Name = "dtpFechaIngreso";
            dtpFechaIngreso.RightToLeft = RightToLeft.No;
            dtpFechaIngreso.ShadowDecoration.CustomizableEdges = customizableEdges2;
            dtpFechaIngreso.Size = new Size(154, 31);
            dtpFechaIngreso.TabIndex = 5;
            dtpFechaIngreso.Value = new DateTime(2025, 3, 7, 0, 3, 12, 692);
            // 
            // lblDepartamento
            // 
            lblDepartamento.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDepartamento.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblDepartamento.ForeColor = Color.Black;
            lblDepartamento.Location = new Point(14, 79);
            lblDepartamento.Name = "lblDepartamento";
            lblDepartamento.Size = new Size(168, 27);
            lblDepartamento.TabIndex = 48;
            lblDepartamento.Text = "Departamento:";
            // 
            // cbxDepartamento
            // 
            cbxDepartamento.BackColor = Color.Transparent;
            cbxDepartamento.BorderRadius = 5;
            cbxDepartamento.CustomizableEdges = customizableEdges3;
            cbxDepartamento.DrawMode = DrawMode.OwnerDrawFixed;
            cbxDepartamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxDepartamento.FocusedColor = Color.FromArgb(94, 148, 255);
            cbxDepartamento.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbxDepartamento.Font = new Font("Segoe UI", 10F);
            cbxDepartamento.ForeColor = Color.FromArgb(68, 88, 112);
            cbxDepartamento.ItemHeight = 30;
            cbxDepartamento.Location = new Point(188, 73);
            cbxDepartamento.Name = "cbxDepartamento";
            cbxDepartamento.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbxDepartamento.Size = new Size(179, 36);
            cbxDepartamento.TabIndex = 50;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Animated = true;
            btnBuscar.CustomizableEdges = customizableEdges5;
            btnBuscar.DisabledState.BorderColor = Color.DarkGray;
            btnBuscar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBuscar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBuscar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBuscar.FillColor = Color.DimGray;
            btnBuscar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(773, 63);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnBuscar.Size = new Size(210, 45);
            btnBuscar.TabIndex = 51;
            btnBuscar.Text = "Buscar";
            btnBuscar.Click += btnBuscar_Click;
            // 
            // guna2ContainerControl1
            // 
            guna2ContainerControl1.CustomizableEdges = customizableEdges7;
            guna2ContainerControl1.Location = new Point(6, 268);
            guna2ContainerControl1.Name = "guna2ContainerControl1";
            guna2ContainerControl1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2ContainerControl1.Size = new Size(200, 200);
            guna2ContainerControl1.TabIndex = 52;
            guna2ContainerControl1.Text = "guna2ContainerControl1";
            // 
            // pnlTabla
            // 
            pnlTabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTabla.Controls.Add(dataGridEmpleados);
            pnlTabla.CustomizableEdges = customizableEdges9;
            pnlTabla.Location = new Point(12, 144);
            pnlTabla.Name = "pnlTabla";
            pnlTabla.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlTabla.Size = new Size(985, 575);
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
            dataGridEmpleados.Columns.AddRange(new DataGridViewColumn[] { Matricula, Nombre, Departamento, Puesto, Estatus });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle8.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dataGridEmpleados.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridEmpleados.Dock = DockStyle.Fill;
            dataGridEmpleados.GridColor = Color.FromArgb(231, 229, 255);
            dataGridEmpleados.Location = new Point(0, 0);
            dataGridEmpleados.Name = "dataGridEmpleados";
            dataGridEmpleados.RowHeadersVisible = false;
            dataGridEmpleados.Size = new Size(985, 575);
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
            // Matricula
            // 
            Matricula.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Matricula.DefaultCellStyle = dataGridViewCellStyle3;
            Matricula.FillWeight = 63.45178F;
            Matricula.HeaderText = "Matricula";
            Matricula.MaxInputLength = 20;
            Matricula.Name = "Matricula";
            Matricula.ReadOnly = true;
            Matricula.Resizable = DataGridViewTriState.True;
            Matricula.Width = 200;
            // 
            // Nombre
            // 
            Nombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Nombre.DefaultCellStyle = dataGridViewCellStyle4;
            Nombre.FillWeight = 182.057571F;
            Nombre.HeaderText = "Nombre";
            Nombre.MaxInputLength = 60;
            Nombre.Name = "Nombre";
            Nombre.Width = 200;
            // 
            // Departamento
            // 
            Departamento.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Departamento.DefaultCellStyle = dataGridViewCellStyle5;
            Departamento.FillWeight = 108.153435F;
            Departamento.HeaderText = "Departamento";
            Departamento.MaxInputLength = 50;
            Departamento.Name = "Departamento";
            Departamento.Resizable = DataGridViewTriState.False;
            Departamento.Width = 200;
            // 
            // Puesto
            // 
            Puesto.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Puesto.DefaultCellStyle = dataGridViewCellStyle6;
            Puesto.FillWeight = 94.41109F;
            Puesto.HeaderText = "Puesto";
            Puesto.MaxInputLength = 100;
            Puesto.Name = "Puesto";
            Puesto.SortMode = DataGridViewColumnSortMode.Programmatic;
            Puesto.Width = 200;
            // 
            // Estatus
            // 
            Estatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Estatus.DefaultCellStyle = dataGridViewCellStyle7;
            Estatus.FillWeight = 51.9261055F;
            Estatus.HeaderText = "Estatus";
            Estatus.MaxInputLength = 100;
            Estatus.Name = "Estatus";
            Estatus.Width = 200;
            // 
            // pnlInfoUsuario
            // 
            pnlInfoUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlInfoUsuario.BackColor = Color.White;
            pnlInfoUsuario.Controls.Add(Separator2);
            pnlInfoUsuario.Controls.Add(lblInfo);
            pnlInfoUsuario.Controls.Add(pnlTabla);
            pnlInfoUsuario.Controls.Add(guna2ContainerControl1);
            pnlInfoUsuario.Controls.Add(btnBuscar);
            pnlInfoUsuario.Controls.Add(cbxDepartamento);
            pnlInfoUsuario.Controls.Add(lblDepartamento);
            pnlInfoUsuario.Controls.Add(dtpFechaIngreso);
            pnlInfoUsuario.Controls.Add(lblFechaNacimiento);
            pnlInfoUsuario.Controls.Add(separador1);
            pnlInfoUsuario.CustomizableEdges = customizableEdges11;
            pnlInfoUsuario.Font = new Font("Microsoft Sans Serif", 8.25F);
            pnlInfoUsuario.Location = new Point(0, 64);
            pnlInfoUsuario.Name = "pnlInfoUsuario";
            pnlInfoUsuario.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pnlInfoUsuario.Size = new Size(1011, 739);
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
            lblInfo.Text = "Filtrar por Departamentos";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Separator2
            // 
            Separator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Separator2.Location = new Point(12, 47);
            Separator2.Name = "Separator2";
            Separator2.Size = new Size(985, 10);
            Separator2.TabIndex = 55;
            // 
            // frmListaEmpleados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 803);
            Controls.Add(pnlInfoUsuario);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmListaEmpleados";
            Text = "frmListadoUsuarios";
            pnlTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).EndInit();
            pnlInfoUsuario.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private Guna.UI2.WinForms.Guna2Separator separador1;
        private Label lblFechaNacimiento;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaIngreso;
        private Label lblDepartamento;
        private Guna.UI2.WinForms.Guna2ComboBox cbxDepartamento;
        private Guna.UI2.WinForms.Guna2Button btnBuscar;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl1;
        private Guna.UI2.WinForms.Guna2Panel pnlTabla;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridEmpleados;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlInfoUsuario;
        private Label lblInfo;
        private DataGridViewTextBoxColumn Matricula;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Departamento;
        private DataGridViewTextBoxColumn Puesto;
        private DataGridViewTextBoxColumn Estatus;
        private Guna.UI2.WinForms.Guna2Separator Separator2;
    }
}