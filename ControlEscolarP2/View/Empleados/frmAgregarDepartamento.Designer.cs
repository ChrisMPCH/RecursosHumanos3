namespace RecursosHumanos.View
{
    partial class frmAgregarDepartamento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public static System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgregarDepartamento));
            pnlInfoEsmpleado = new Guna.UI2.WinForms.Guna2GradientPanel();
            txtCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            txtTelefono = new Guna.UI2.WinForms.Guna2TextBox();
            txtUbicacion = new Guna.UI2.WinForms.Guna2TextBox();
            txtNombre = new Guna.UI2.WinForms.Guna2TextBox();
            btnCargaMasiva = new Guna.UI2.WinForms.Guna2Button();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            lblNombre = new Label();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            lblCorreo = new Label();
            lblTelefono = new Label();
            lblUbicacion = new Label();
            guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            lblBienvenida2 = new Label();
            ofdArchivo = new OpenFileDialog();
            InfoMatricula = new ToolTip(components);
            pnlInfoEsmpleado.SuspendLayout();
            SuspendLayout();
            // 
            // pnlInfoEsmpleado
            // 
            pnlInfoEsmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlInfoEsmpleado.BackColor = Color.White;
            pnlInfoEsmpleado.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pnlInfoEsmpleado.Controls.Add(txtCorreo);
            pnlInfoEsmpleado.Controls.Add(txtTelefono);
            pnlInfoEsmpleado.Controls.Add(txtUbicacion);
            pnlInfoEsmpleado.Controls.Add(txtNombre);
            pnlInfoEsmpleado.Controls.Add(btnCargaMasiva);
            pnlInfoEsmpleado.Controls.Add(btnGuardar);
            pnlInfoEsmpleado.Controls.Add(lblNombre);
            pnlInfoEsmpleado.Controls.Add(btnCancelar);
            pnlInfoEsmpleado.Controls.Add(lblCorreo);
            pnlInfoEsmpleado.Controls.Add(lblTelefono);
            pnlInfoEsmpleado.Controls.Add(lblUbicacion);
            pnlInfoEsmpleado.Controls.Add(guna2Separator1);
            pnlInfoEsmpleado.Controls.Add(lblBienvenida2);
            pnlInfoEsmpleado.CustomizableEdges = customizableEdges15;
            pnlInfoEsmpleado.Location = new Point(0, 0);
            pnlInfoEsmpleado.Name = "pnlInfoEsmpleado";
            pnlInfoEsmpleado.ShadowDecoration.CustomizableEdges = customizableEdges16;
            pnlInfoEsmpleado.Size = new Size(1001, 291);
            pnlInfoEsmpleado.TabIndex = 4;
            pnlInfoEsmpleado.Paint += pnlInfoEsmpleado_Paint;
            // 
            // txtCorreo
            // 
            txtCorreo.Animated = true;
            txtCorreo.BorderRadius = 5;
            txtCorreo.CustomizableEdges = customizableEdges1;
            txtCorreo.DefaultText = "Ingrese correo electrónico";
            txtCorreo.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtCorreo.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtCorreo.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtCorreo.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtCorreo.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCorreo.Font = new Font("Segoe UI", 9.75F);
            txtCorreo.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCorreo.Location = new Point(157, 202);
            txtCorreo.Margin = new Padding(3, 4, 3, 4);
            txtCorreo.MaxLength = 100;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.PlaceholderText = "";
            txtCorreo.SelectedText = "";
            txtCorreo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtCorreo.Size = new Size(434, 34);
            txtCorreo.TabIndex = 40;
            // 
            // txtTelefono
            // 
            txtTelefono.Animated = true;
            txtTelefono.BorderRadius = 5;
            txtTelefono.CustomizableEdges = customizableEdges3;
            txtTelefono.DefaultText = "Ingrese teléfono";
            txtTelefono.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtTelefono.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtTelefono.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtTelefono.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtTelefono.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtTelefono.Font = new Font("Segoe UI", 9.75F);
            txtTelefono.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtTelefono.Location = new Point(157, 160);
            txtTelefono.Margin = new Padding(3, 4, 3, 4);
            txtTelefono.MaxLength = 20;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "";
            txtTelefono.SelectedText = "";
            txtTelefono.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtTelefono.Size = new Size(434, 34);
            txtTelefono.TabIndex = 39;
            //txtTelefono.TextChanged += this.txtTelefono_TextChanged;
            // 
            // txtUbicacion
            // 
            txtUbicacion.Animated = true;
            txtUbicacion.BorderRadius = 5;
            txtUbicacion.CustomizableEdges = customizableEdges5;
            txtUbicacion.DefaultText = "Ingrese ubicacion";
            txtUbicacion.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUbicacion.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUbicacion.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUbicacion.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUbicacion.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUbicacion.Font = new Font("Segoe UI", 9.75F);
            txtUbicacion.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUbicacion.Location = new Point(157, 120);
            txtUbicacion.Margin = new Padding(3, 4, 3, 4);
            txtUbicacion.MaxLength = 100;
            txtUbicacion.Name = "txtUbicacion";
            txtUbicacion.PlaceholderText = "";
            txtUbicacion.SelectedText = "";
            txtUbicacion.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtUbicacion.Size = new Size(434, 34);
            txtUbicacion.TabIndex = 38;
            //txtUbicacion.TextChanged += this.txtUbicacion_TextChanged;
            // 
            // txtNombre
            // 
            txtNombre.Animated = true;
            txtNombre.BorderRadius = 5;
            txtNombre.CustomizableEdges = customizableEdges7;
            txtNombre.DefaultText = "Ingrese nombre";
            txtNombre.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNombre.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNombre.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNombre.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNombre.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNombre.Font = new Font("Segoe UI", 9.75F);
            txtNombre.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNombre.Location = new Point(157, 76);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "";
            txtNombre.SelectedText = "";
            txtNombre.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtNombre.Size = new Size(434, 34);
            txtNombre.TabIndex = 37;
            //txtNombre.TextChanged += this.txtNombre_TextChanged;
            // 
            // btnCargaMasiva
            // 
            btnCargaMasiva.CustomizableEdges = customizableEdges9;
            btnCargaMasiva.DisabledState.BorderColor = Color.DarkGray;
            btnCargaMasiva.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCargaMasiva.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCargaMasiva.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCargaMasiva.FillColor = Color.DimGray;
            btnCargaMasiva.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnCargaMasiva.ForeColor = Color.White;
            btnCargaMasiva.Location = new Point(791, 160);
            btnCargaMasiva.Name = "btnCargaMasiva";
            btnCargaMasiva.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnCargaMasiva.Size = new Size(188, 43);
            btnCargaMasiva.TabIndex = 29;
            btnCargaMasiva.Text = "Carga Masiva";
            btnCargaMasiva.Click += btnCargaMasiva_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.CustomizableEdges = customizableEdges11;
            btnGuardar.DisabledState.BorderColor = Color.DarkGray;
            btnGuardar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGuardar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGuardar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGuardar.FillColor = Color.DimGray;
            btnGuardar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(791, 101);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnGuardar.Size = new Size(188, 43);
            btnGuardar.TabIndex = 25;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lblNombre
            // 
            lblNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblNombre.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblNombre.ForeColor = Color.Black;
            lblNombre.Location = new Point(46, 79);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(105, 25);
            lblNombre.TabIndex = 23;
            lblNombre.Text = "Nombre: ";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Black;
            btnCancelar.CustomBorderColor = Color.Black;
            btnCancelar.CustomBorderThickness = new Padding(1);
            btnCancelar.CustomizableEdges = customizableEdges13;
            btnCancelar.DisabledState.BorderColor = Color.DarkGray;
            btnCancelar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancelar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancelar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancelar.FillColor = Color.White;
            btnCancelar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(791, 221);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnCancelar.Size = new Size(188, 43);
            btnCancelar.TabIndex = 22;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblCorreo
            // 
            lblCorreo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblCorreo.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblCorreo.ForeColor = Color.Black;
            lblCorreo.Location = new Point(60, 202);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(91, 25);
            lblCorreo.TabIndex = 9;
            lblCorreo.Text = "Correo:";
            // 
            // lblTelefono
            // 
            lblTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTelefono.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblTelefono.ForeColor = Color.Black;
            lblTelefono.Location = new Point(45, 160);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(106, 25);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "Teléfono:";
            // 
            // lblUbicacion
            // 
            lblUbicacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblUbicacion.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            lblUbicacion.ForeColor = Color.Black;
            lblUbicacion.Location = new Point(23, 119);
            lblUbicacion.Name = "lblUbicacion";
            lblUbicacion.Size = new Size(128, 25);
            lblUbicacion.TabIndex = 5;
            lblUbicacion.Text = "Ubicación:";
            // 
            // guna2Separator1
            // 
            guna2Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2Separator1.Location = new Point(7, 47);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(986, 10);
            guna2Separator1.TabIndex = 3;
            // 
            // lblBienvenida2
            // 
            lblBienvenida2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblBienvenida2.Font = new Font("Century Gothic", 20.25F, FontStyle.Bold);
            lblBienvenida2.ForeColor = Color.DimGray;
            lblBienvenida2.Location = new Point(18, 13);
            lblBienvenida2.Name = "lblBienvenida2";
            lblBienvenida2.Size = new Size(995, 37);
            lblBienvenida2.TabIndex = 2;
            lblBienvenida2.Text = "Informacion del departamento";
            // 
            // ofdArchivo
            // 
            ofdArchivo.FileName = "Carga masiva de Departamentos";
            // 
            // frmAgregarDepartamento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1001, 291);
            Controls.Add(pnlInfoEsmpleado);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmAgregarDepartamento";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPrueba";
            pnlInfoEsmpleado.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        public static Guna.UI2.WinForms.Guna2GradientPanel pnlInfoEsmpleado;
        public static Label lblBienvenida2;
        public static Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        public static Label lblCorreo;
        public static Label lblTelefono;
        public static Label lblUbicacion;
        public static Guna.UI2.WinForms.Guna2Button btnCancelar;
        public static Label lblNombre;
        public static Guna.UI2.WinForms.Guna2Button btnGuardar;
        public static Guna.UI2.WinForms.Guna2Button btnCargaMasiva;
        public static OpenFileDialog ofdArchivo;
        public static ToolTip InfoMatricula;
        public static Guna.UI2.WinForms.Guna2TextBox txtNombre;
        public static Guna.UI2.WinForms.Guna2TextBox txtCorreo;
        public static Guna.UI2.WinForms.Guna2TextBox txtTelefono;
        public static Guna.UI2.WinForms.Guna2TextBox txtUbicacion;
    }
}