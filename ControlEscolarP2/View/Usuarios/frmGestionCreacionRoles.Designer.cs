namespace RecursosHumanos.View
{
    partial class frmGestionCreacionRoles
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblTitulo = new Label();
            separador1 = new Guna.UI2.WinForms.Guna2Separator();
            lblFechaNacimiento = new Label();
            dtpFechaIngreso = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblRol = new Label();
            btnBuscar = new Guna.UI2.WinForms.Guna2Button();
            txtRol = new Guna.UI2.WinForms.Guna2TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new Guna.UI2.WinForms.Guna2TextBox();
            lblInfo = new Label();
            lblInfPermisos = new Label();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            pnlInfoUsuario = new Guna.UI2.WinForms.Guna2GradientPanel();
            pnlTabla = new Guna.UI2.WinForms.Guna2Panel();
            dataGridPermisos = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlInfoUsuario.SuspendLayout();
            pnlTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridPermisos).BeginInit();
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
            lblTitulo.TabIndex = 26;
            lblTitulo.Text = "Gestion de roles";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // separador1
            // 
            separador1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            separador1.Location = new Point(10, 130);
            separador1.Name = "separador1";
            separador1.Size = new Size(989, 10);
            separador1.TabIndex = 3;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.Anchor = AnchorStyles.Right;
            lblFechaNacimiento.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblFechaNacimiento.ForeColor = Color.Black;
            lblFechaNacimiento.Location = new Point(2284, 972);
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
            dtpFechaIngreso.Location = new Point(2458, 983);
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
            lblRol.Location = new Point(13, 59);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(163, 27);
            lblRol.TabIndex = 48;
            lblRol.Text = "Codigo:";
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Animated = true;
            btnBuscar.CustomizableEdges = customizableEdges3;
            btnBuscar.DisabledState.BorderColor = Color.DarkGray;
            btnBuscar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBuscar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBuscar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBuscar.FillColor = Color.DimGray;
            btnBuscar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(1584, 45);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnBuscar.Size = new Size(210, 45);
            btnBuscar.TabIndex = 51;
            btnBuscar.Text = "Buscar";
            // 
            // txtRol
            // 
            txtRol.Animated = true;
            txtRol.BorderRadius = 5;
            txtRol.CustomizableEdges = customizableEdges5;
            txtRol.DefaultText = "Ingrese codigo del Rol";
            txtRol.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtRol.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtRol.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtRol.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtRol.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRol.Font = new Font("Segoe UI", 9.75F);
            txtRol.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtRol.Location = new Point(201, 50);
            txtRol.Margin = new Padding(3, 4, 3, 4);
            txtRol.MaxLength = 20;
            txtRol.Name = "txtRol";
            txtRol.PlaceholderText = "";
            txtRol.SelectedText = "";
            txtRol.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtRol.Size = new Size(272, 36);
            txtRol.TabIndex = 40;
            // 
            // lblDescripcion
            // 
            lblDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDescripcion.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblDescripcion.ForeColor = Color.Black;
            lblDescripcion.Location = new Point(12, 100);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(145, 27);
            lblDescripcion.TabIndex = 54;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Animated = true;
            txtDescripcion.BorderRadius = 5;
            txtDescripcion.CustomizableEdges = customizableEdges7;
            txtDescripcion.DefaultText = "Describa lo que realizara el rol";
            txtDescripcion.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtDescripcion.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtDescripcion.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtDescripcion.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtDescripcion.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtDescripcion.Font = new Font("Segoe UI", 9.75F);
            txtDescripcion.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtDescripcion.Location = new Point(201, 94);
            txtDescripcion.Margin = new Padding(3, 4, 3, 4);
            txtDescripcion.MaxLength = 100;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "";
            txtDescripcion.SelectedText = "";
            txtDescripcion.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtDescripcion.Size = new Size(798, 36);
            txtDescripcion.TabIndex = 53;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.Location = new Point(12, 9);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(365, 37);
            lblInfo.TabIndex = 56;
            lblInfo.Text = "Creacion de Rol de usuario";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblInfPermisos
            // 
            lblInfPermisos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfPermisos.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfPermisos.ForeColor = Color.DimGray;
            lblInfPermisos.Location = new Point(12, 154);
            lblInfPermisos.Name = "lblInfPermisos";
            lblInfPermisos.Size = new Size(427, 37);
            lblInfPermisos.TabIndex = 57;
            lblInfPermisos.Text = "Seleccione los permisos de este rol";
            lblInfPermisos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.Animated = true;
            btnGuardar.CustomizableEdges = customizableEdges9;
            btnGuardar.DisabledState.BorderColor = Color.DarkGray;
            btnGuardar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGuardar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGuardar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGuardar.FillColor = Color.DimGray;
            btnGuardar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(784, 9);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnGuardar.Size = new Size(210, 45);
            btnGuardar.TabIndex = 58;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Animated = true;
            btnCancelar.BackColor = Color.Black;
            btnCancelar.CustomBorderColor = Color.Black;
            btnCancelar.CustomBorderThickness = new Padding(1);
            btnCancelar.CustomizableEdges = customizableEdges11;
            btnCancelar.DisabledState.BorderColor = Color.DarkGray;
            btnCancelar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancelar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancelar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancelar.FillColor = Color.White;
            btnCancelar.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(602, 9);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCancelar.Size = new Size(164, 45);
            btnCancelar.TabIndex = 59;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // pnlInfoUsuario
            // 
            pnlInfoUsuario.BackColor = Color.White;
            pnlInfoUsuario.Controls.Add(pnlTabla);
            pnlInfoUsuario.Controls.Add(btnCancelar);
            pnlInfoUsuario.Controls.Add(btnGuardar);
            pnlInfoUsuario.Controls.Add(lblInfPermisos);
            pnlInfoUsuario.Controls.Add(lblInfo);
            pnlInfoUsuario.Controls.Add(txtDescripcion);
            pnlInfoUsuario.Controls.Add(lblDescripcion);
            pnlInfoUsuario.Controls.Add(txtRol);
            pnlInfoUsuario.Controls.Add(btnBuscar);
            pnlInfoUsuario.Controls.Add(lblRol);
            pnlInfoUsuario.Controls.Add(dtpFechaIngreso);
            pnlInfoUsuario.Controls.Add(lblFechaNacimiento);
            pnlInfoUsuario.Controls.Add(separador1);
            pnlInfoUsuario.CustomizableEdges = customizableEdges15;
            pnlInfoUsuario.Dock = DockStyle.Fill;
            pnlInfoUsuario.Font = new Font("Microsoft Sans Serif", 8.25F);
            pnlInfoUsuario.Location = new Point(0, 64);
            pnlInfoUsuario.Name = "pnlInfoUsuario";
            pnlInfoUsuario.ShadowDecoration.CustomizableEdges = customizableEdges16;
            pnlInfoUsuario.Size = new Size(1011, 739);
            pnlInfoUsuario.TabIndex = 28;
            // 
            // pnlTabla
            // 
            pnlTabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTabla.Controls.Add(dataGridPermisos);
            pnlTabla.CustomizableEdges = customizableEdges13;
            pnlTabla.Location = new Point(13, 194);
            pnlTabla.Name = "pnlTabla";
            pnlTabla.ShadowDecoration.CustomizableEdges = customizableEdges14;
            pnlTabla.Size = new Size(985, 517);
            pnlTabla.TabIndex = 60;
            // 
            // dataGridRoles
            // 
            dataGridPermisos.AllowUserToAddRows = false;
            dataGridPermisos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridPermisos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridPermisos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridPermisos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridPermisos.ColumnHeadersHeight = 15;
            dataGridPermisos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridPermisos.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridPermisos.GridColor = Color.FromArgb(231, 229, 255);
            dataGridPermisos.Location = new Point(0, 2);
            dataGridPermisos.Name = "dataGridRoles";
            dataGridPermisos.RowHeadersVisible = false;
            dataGridPermisos.Size = new Size(985, 517);
            dataGridPermisos.TabIndex = 0;
            dataGridPermisos.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridPermisos.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridPermisos.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridPermisos.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridPermisos.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridPermisos.ThemeStyle.BackColor = Color.White;
            dataGridPermisos.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridPermisos.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridPermisos.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridPermisos.ThemeStyle.HeaderStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridPermisos.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridPermisos.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridPermisos.ThemeStyle.HeaderStyle.Height = 15;
            dataGridPermisos.ThemeStyle.ReadOnly = false;
            dataGridPermisos.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridPermisos.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridPermisos.ThemeStyle.RowsStyle.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridPermisos.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridPermisos.ThemeStyle.RowsStyle.Height = 25;
            dataGridPermisos.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridPermisos.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // frmGestionCreacionRoles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 803);
            Controls.Add(pnlInfoUsuario);
            Controls.Add(lblTitulo);
            Name = "frmGestionCreacionRoles";
            Text = "frmGestionPermisosRoles";
            pnlInfoUsuario.ResumeLayout(false);
            pnlTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridPermisos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private Guna.UI2.WinForms.Guna2Separator separador1;
        private Label lblFechaNacimiento;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaIngreso;
        private Label lblRol;
        private Guna.UI2.WinForms.Guna2Button btnBuscar;
        private Guna.UI2.WinForms.Guna2TextBox txtRol;
        private Label lblDescripcion;
        private Guna.UI2.WinForms.Guna2TextBox txtDescripcion;
        private Label lblInfo;
        private Label lblInfPermisos;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlInfoUsuario;
        private Guna.UI2.WinForms.Guna2Panel pnlTabla;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridPermisos;
    }
}