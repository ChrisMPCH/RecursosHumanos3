namespace RecursosHumanos.View
{
    partial class frmListaUsuarios
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitulo = new Label();
            separador1 = new Guna.UI2.WinForms.Guna2Separator();
            lblFechaNacimiento = new Label();
            dtpFechaIngreso = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblRol = new Label();
            cbRoles = new Guna.UI2.WinForms.Guna2ComboBox();
            btnBuscar = new Guna.UI2.WinForms.Guna2Button();
            guna2ContainerControl1 = new Guna.UI2.WinForms.Guna2ContainerControl();
            pnlTabla = new Guna.UI2.WinForms.Guna2Panel();
            dataGridUsuarios = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlInfoUsuario = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblInfo = new Label();
            pnlTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridUsuarios).BeginInit();
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
            lblTitulo.Text = "Listado de usuarios";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // separador1
            // 
            separador1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            separador1.Location = new Point(12, 97);
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
            // lblRol
            // 
            lblRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblRol.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblRol.ForeColor = Color.Black;
            lblRol.Location = new Point(14, 61);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(49, 27);
            lblRol.TabIndex = 48;
            lblRol.Text = "Rol:";
            // 
            // cbRoles
            // 
            cbRoles.BackColor = Color.Transparent;
            cbRoles.BorderRadius = 5;
            cbRoles.CustomizableEdges = customizableEdges3;
            cbRoles.DrawMode = DrawMode.OwnerDrawFixed;
            cbRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoles.FocusedColor = Color.FromArgb(94, 148, 255);
            cbRoles.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbRoles.Font = new Font("Segoe UI", 10F);
            cbRoles.ForeColor = Color.FromArgb(68, 88, 112);
            cbRoles.ItemHeight = 30;
            cbRoles.Location = new Point(69, 52);
            cbRoles.Name = "cbRoles";
            cbRoles.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbRoles.Size = new Size(164, 36);
            cbRoles.TabIndex = 50;
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
            btnBuscar.Location = new Point(773, 45);
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
            guna2ContainerControl1.Location = new Point(6, 250);
            guna2ContainerControl1.Name = "guna2ContainerControl1";
            guna2ContainerControl1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2ContainerControl1.Size = new Size(200, 200);
            guna2ContainerControl1.TabIndex = 52;
            guna2ContainerControl1.Text = "guna2ContainerControl1";
            // 
            // pnlTabla
            // 
            pnlTabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTabla.Controls.Add(dataGridUsuarios);
            pnlTabla.CustomizableEdges = customizableEdges9;
            pnlTabla.Location = new Point(12, 126);
            pnlTabla.Name = "pnlTabla";
            pnlTabla.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlTabla.Size = new Size(985, 575);
            pnlTabla.TabIndex = 53;
            // 
            // dataGridUsuarios
            // 
            dataGridUsuarios.AllowUserToAddRows = false;
            dataGridUsuarios.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridUsuarios.ColumnHeadersHeight = 15;
            dataGridUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridUsuarios.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridUsuarios.Dock = DockStyle.Fill;
            dataGridUsuarios.GridColor = Color.FromArgb(231, 229, 255);
            dataGridUsuarios.Location = new Point(0, 0);
            dataGridUsuarios.Name = "dataGridUsuarios";
            dataGridUsuarios.ReadOnly = true;
            dataGridUsuarios.RowHeadersVisible = false;
            dataGridUsuarios.Size = new Size(985, 575);
            dataGridUsuarios.TabIndex = 0;
            dataGridUsuarios.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridUsuarios.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridUsuarios.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridUsuarios.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridUsuarios.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridUsuarios.ThemeStyle.BackColor = Color.White;
            dataGridUsuarios.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridUsuarios.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridUsuarios.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridUsuarios.ThemeStyle.HeaderStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridUsuarios.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridUsuarios.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridUsuarios.ThemeStyle.HeaderStyle.Height = 15;
            dataGridUsuarios.ThemeStyle.ReadOnly = true;
            dataGridUsuarios.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridUsuarios.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridUsuarios.ThemeStyle.RowsStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridUsuarios.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridUsuarios.ThemeStyle.RowsStyle.Height = 25;
            dataGridUsuarios.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridUsuarios.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // pnlInfoUsuario
            // 
            pnlInfoUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlInfoUsuario.BackColor = Color.White;
            pnlInfoUsuario.Controls.Add(lblInfo);
            pnlInfoUsuario.Controls.Add(pnlTabla);
            pnlInfoUsuario.Controls.Add(guna2ContainerControl1);
            pnlInfoUsuario.Controls.Add(btnBuscar);
            pnlInfoUsuario.Controls.Add(cbRoles);
            pnlInfoUsuario.Controls.Add(lblRol);
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
            lblInfo.Location = new Point(6, 5);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(987, 37);
            lblInfo.TabIndex = 54;
            lblInfo.Text = "Puedes filtrar por Rol de usuario";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmListaUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 803);
            Controls.Add(pnlInfoUsuario);
            Controls.Add(lblTitulo);
            Name = "frmListaUsuarios";
            Text = "frmListadoUsuarios";
            pnlTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridUsuarios).EndInit();
            pnlInfoUsuario.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private Guna.UI2.WinForms.Guna2Separator separador1;
        private Label lblFechaNacimiento;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaIngreso;
        private Label lblRol;
        private Guna.UI2.WinForms.Guna2ComboBox cbRoles;
        private Guna.UI2.WinForms.Guna2Button btnBuscar;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl1;
        private Guna.UI2.WinForms.Guna2Panel pnlTabla;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridUsuarios;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlInfoUsuario;
        private Label lblInfo;
    }
}