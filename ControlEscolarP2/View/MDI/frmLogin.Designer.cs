
using Guna.UI2.WinForms;

namespace RecursosHumanos.View
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            lbUsuario = new Label();
            lbContraseña = new Label();
            txtContrasenia = new Guna2TextBox();
            txtUsuario = new Guna2TextBox();
            btnLogin = new Guna2Button();
            btnCancelar = new Guna2Button();
            lblInicio = new Label();
            lblInfo = new Label();
            pcVerContraseña = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pcVerContraseña).BeginInit();
            SuspendLayout();
            // 
            // lbUsuario
            // 
            lbUsuario.AutoSize = true;
            lbUsuario.Font = new Font("Century Gothic", 20.25F, FontStyle.Bold);
            lbUsuario.Location = new Point(158, 122);
            lbUsuario.Name = "lbUsuario";
            lbUsuario.Size = new Size(126, 32);
            lbUsuario.TabIndex = 0;
            lbUsuario.Text = "Usuario: ";
            // 
            // lbContraseña
            // 
            lbContraseña.AccessibleRole = AccessibleRole.None;
            lbContraseña.AutoSize = true;
            lbContraseña.Font = new Font("Century Gothic", 20.25F, FontStyle.Bold);
            lbContraseña.Location = new Point(158, 221);
            lbContraseña.Name = "lbContraseña";
            lbContraseña.Size = new Size(182, 32);
            lbContraseña.TabIndex = 1;
            lbContraseña.Text = "Contraseña: ";
            // 
            // txtContrasenia
            // 
            txtContrasenia.Animated = true;
            txtContrasenia.BorderRadius = 5;
            txtContrasenia.CustomizableEdges = customizableEdges1;
            txtContrasenia.DefaultText = "Ingrese su contraseña";
            txtContrasenia.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtContrasenia.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtContrasenia.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtContrasenia.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtContrasenia.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtContrasenia.Font = new Font("Segoe UI", 9.75F);
            txtContrasenia.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtContrasenia.Location = new Point(158, 257);
            txtContrasenia.Margin = new Padding(3, 4, 3, 4);
            txtContrasenia.MaxLength = 20;
            txtContrasenia.Name = "txtContrasenia";
            txtContrasenia.PlaceholderText = "";
            txtContrasenia.SelectedText = "";
            txtContrasenia.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtContrasenia.Size = new Size(272, 36);
            txtContrasenia.TabIndex = 2;
            // 
            // txtUsuario
            // 
            txtUsuario.Animated = true;
            txtUsuario.BorderRadius = 5;
            txtUsuario.CustomizableEdges = customizableEdges3;
            txtUsuario.DefaultText = "Ingrese su usuario";
            txtUsuario.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUsuario.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUsuario.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUsuario.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUsuario.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUsuario.Font = new Font("Segoe UI", 9.75F);
            txtUsuario.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUsuario.Location = new Point(158, 158);
            txtUsuario.Margin = new Padding(3, 4, 3, 4);
            txtUsuario.MaxLength = 20;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "";
            txtUsuario.SelectedText = "";
            txtUsuario.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtUsuario.Size = new Size(272, 36);
            txtUsuario.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogin.Animated = true;
            btnLogin.CustomizableEdges = customizableEdges5;
            btnLogin.DisabledState.BorderColor = Color.DarkGray;
            btnLogin.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogin.FillColor = Color.DimGray;
            btnLogin.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(370, 372);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnLogin.Size = new Size(164, 45);
            btnLogin.TabIndex = 44;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Animated = true;
            btnCancelar.BackColor = Color.Black;
            btnCancelar.CustomBorderColor = Color.Black;
            btnCancelar.CustomBorderThickness = new Padding(1);
            btnCancelar.CustomizableEdges = customizableEdges7;
            btnCancelar.DisabledState.BorderColor = Color.DarkGray;
            btnCancelar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancelar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancelar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancelar.FillColor = Color.White;
            btnCancelar.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(48, 372);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCancelar.Size = new Size(164, 45);
            btnCancelar.TabIndex = 43;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblInicio
            // 
            lblInicio.Dock = DockStyle.Top;
            lblInicio.Font = new Font("Century Gothic", 20.25F, FontStyle.Bold);
            lblInicio.Location = new Point(0, 0);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(580, 44);
            lblInicio.TabIndex = 45;
            lblInicio.Text = "Sistema de Recursos Humanos";
            lblInicio.TextAlign = ContentAlignment.BottomCenter;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.Location = new Point(12, 44);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(556, 40);
            lblInfo.TabIndex = 57;
            lblInfo.Text = "Ingrese sus credenciales para acceder";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pcVerContraseña
            // 
            pcVerContraseña.BackgroundImageLayout = ImageLayout.Zoom;
            pcVerContraseña.Cursor = Cursors.Hand;
            pcVerContraseña.Image = (Image)resources.GetObject("pcVerContraseña.Image");
            pcVerContraseña.Location = new Point(436, 257);
            pcVerContraseña.Name = "pcVerContraseña";
            pcVerContraseña.Size = new Size(39, 36);
            pcVerContraseña.SizeMode = PictureBoxSizeMode.Zoom;
            pcVerContraseña.TabIndex = 58;
            pcVerContraseña.TabStop = false;
            pcVerContraseña.Click += pcVerContraseña_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(580, 460);
            Controls.Add(pcVerContraseña);
            Controls.Add(lblInfo);
            Controls.Add(lblInicio);
            Controls.Add(btnLogin);
            Controls.Add(btnCancelar);
            Controls.Add(txtUsuario);
            Controls.Add(txtContrasenia);
            Controls.Add(lbContraseña);
            Controls.Add(lbUsuario);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(580, 460);
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)pcVerContraseña).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Label lbUsuario;
        private Label lbContraseña;
        private Guna2TextBox txtContrasenia;
        private Guna2TextBox txtUsuario;
        private Guna2Button btnLogin;
        private Guna2Button btnCancelar;
        private Label lblInicio;
        private Label lblInfo;
        private PictureBox pcVerContraseña;
    }
}