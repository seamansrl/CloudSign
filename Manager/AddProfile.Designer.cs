namespace Horus
{
    partial class AddProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProfile));
            this.Cancelar = new System.Windows.Forms.Button();
            this.Aceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Tipo = new System.Windows.Forms.ComboBox();
            this.Perfil = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.CloudIcon = new System.Windows.Forms.PictureBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TitlePanel = new System.Windows.Forms.Label();
            this.FondoMovil = new System.Windows.Forms.PictureBox();
            this.CentralPanel = new System.Windows.Forms.Panel();
            this.SubPanel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.SeperiorPanel = new System.Windows.Forms.Panel();
            this.Ayuda = new System.Windows.Forms.PictureBox();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.Route = new System.Windows.Forms.Label();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloudIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FondoMovil)).BeginInit();
            this.CentralPanel.SuspendLayout();
            this.SubPanel1.SuspendLayout();
            this.SeperiorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayuda)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancelar
            // 
            this.Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancelar.ForeColor = System.Drawing.Color.Gray;
            this.Cancelar.Location = new System.Drawing.Point(800, 458);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(168, 50);
            this.Cancelar.TabIndex = 3;
            this.Cancelar.Text = "CANCELAR";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // Aceptar
            // 
            this.Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar.ForeColor = System.Drawing.Color.Gray;
            this.Aceptar.Location = new System.Drawing.Point(626, 458);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(168, 50);
            this.Aceptar.TabIndex = 2;
            this.Aceptar.Text = "ACEPTAR";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(55, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo de perfil:";
            // 
            // Tipo
            // 
            this.Tipo.BackColor = System.Drawing.Color.White;
            this.Tipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Tipo.ForeColor = System.Drawing.Color.Black;
            this.Tipo.FormattingEnabled = true;
            this.Tipo.Items.AddRange(new object[] {
            "FaceID",
            "Tapabocas"});
            this.Tipo.Location = new System.Drawing.Point(179, 153);
            this.Tipo.Name = "Tipo";
            this.Tipo.Size = new System.Drawing.Size(254, 24);
            this.Tipo.TabIndex = 1;
            this.Tipo.Text = "FaceID";
            // 
            // Perfil
            // 
            this.Perfil.BackColor = System.Drawing.Color.White;
            this.Perfil.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Perfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Perfil.ForeColor = System.Drawing.Color.Black;
            this.Perfil.Location = new System.Drawing.Point(179, 125);
            this.Perfil.MaxLength = 200;
            this.Perfil.Name = "Perfil";
            this.Perfil.Size = new System.Drawing.Size(455, 19);
            this.Perfil.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(55, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nuevo perfil:";
            // 
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(47)))));
            this.MenuPanel.Controls.Add(this.CloudIcon);
            this.MenuPanel.Controls.Add(this.Button2);
            this.MenuPanel.Controls.Add(this.Button1);
            this.MenuPanel.Controls.Add(this.TitlePanel);
            this.MenuPanel.Controls.Add(this.FondoMovil);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(250, 622);
            this.MenuPanel.TabIndex = 29;
            // 
            // CloudIcon
            // 
            this.CloudIcon.Image = ((System.Drawing.Image)(resources.GetObject("CloudIcon.Image")));
            this.CloudIcon.Location = new System.Drawing.Point(114, 13);
            this.CloudIcon.Name = "CloudIcon";
            this.CloudIcon.Size = new System.Drawing.Size(26, 26);
            this.CloudIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CloudIcon.TabIndex = 28;
            this.CloudIcon.TabStop = false;
            // 
            // Button2
            // 
            this.Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button2.Enabled = false;
            this.Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.Location = new System.Drawing.Point(0, 106);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(250, 48);
            this.Button2.TabIndex = 5;
            this.Button2.Text = "        Nuevo perfil";
            this.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button2.UseVisualStyleBackColor = false;
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Location = new System.Drawing.Point(0, 56);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(250, 48);
            this.Button1.TabIndex = 4;
            this.Button1.Text = "    << Volver";
            this.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TitlePanel
            // 
            this.TitlePanel.AutoSize = true;
            this.TitlePanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitlePanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TitlePanel.Location = new System.Drawing.Point(31, 19);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(79, 16);
            this.TitlePanel.TabIndex = 0;
            this.TitlePanel.Text = "Cloud Sign";
            // 
            // FondoMovil
            // 
            this.FondoMovil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FondoMovil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.FondoMovil.Image = ((System.Drawing.Image)(resources.GetObject("FondoMovil.Image")));
            this.FondoMovil.Location = new System.Drawing.Point(0, 23);
            this.FondoMovil.Name = "FondoMovil";
            this.FondoMovil.Size = new System.Drawing.Size(251, 600);
            this.FondoMovil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.FondoMovil.TabIndex = 11;
            this.FondoMovil.TabStop = false;
            // 
            // CentralPanel
            // 
            this.CentralPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.CentralPanel.Controls.Add(this.Cancelar);
            this.CentralPanel.Controls.Add(this.Aceptar);
            this.CentralPanel.Controls.Add(this.label2);
            this.CentralPanel.Controls.Add(this.Tipo);
            this.CentralPanel.Controls.Add(this.Perfil);
            this.CentralPanel.Controls.Add(this.label1);
            this.CentralPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CentralPanel.Location = new System.Drawing.Point(250, 102);
            this.CentralPanel.Name = "CentralPanel";
            this.CentralPanel.Padding = new System.Windows.Forms.Padding(30);
            this.CentralPanel.Size = new System.Drawing.Size(980, 520);
            this.CentralPanel.TabIndex = 38;
            // 
            // SubPanel1
            // 
            this.SubPanel1.BackColor = System.Drawing.Color.White;
            this.SubPanel1.Controls.Add(this.label16);
            this.SubPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.SubPanel1.Location = new System.Drawing.Point(250, 56);
            this.SubPanel1.Name = "SubPanel1";
            this.SubPanel1.Size = new System.Drawing.Size(980, 46);
            this.SubPanel1.TabIndex = 39;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label16.Location = new System.Drawing.Point(23, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(122, 22);
            this.label16.TabIndex = 1;
            this.label16.Text = "Nuevo Perfil";
            // 
            // SeperiorPanel
            // 
            this.SeperiorPanel.Controls.Add(this.Ayuda);
            this.SeperiorPanel.Controls.Add(this.LinePanel);
            this.SeperiorPanel.Controls.Add(this.Route);
            this.SeperiorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SeperiorPanel.Location = new System.Drawing.Point(250, 0);
            this.SeperiorPanel.Name = "SeperiorPanel";
            this.SeperiorPanel.Size = new System.Drawing.Size(980, 56);
            this.SeperiorPanel.TabIndex = 37;
            // 
            // Ayuda
            // 
            this.Ayuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ayuda.Image = global::Horus.Properties.Resources.ayuda;
            this.Ayuda.Location = new System.Drawing.Point(937, 13);
            this.Ayuda.Name = "Ayuda";
            this.Ayuda.Size = new System.Drawing.Size(31, 29);
            this.Ayuda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ayuda.TabIndex = 34;
            this.Ayuda.TabStop = false;
            this.Ayuda.Click += new System.EventHandler(this.Ayuda_Click);
            // 
            // LinePanel
            // 
            this.LinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LinePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LinePanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LinePanel.Location = new System.Drawing.Point(0, 55);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(980, 1);
            this.LinePanel.TabIndex = 6;
            // 
            // Route
            // 
            this.Route.AutoSize = true;
            this.Route.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Route.ForeColor = System.Drawing.Color.Gray;
            this.Route.Location = new System.Drawing.Point(24, 21);
            this.Route.Name = "Route";
            this.Route.Size = new System.Drawing.Size(241, 17);
            this.Route.TabIndex = 7;
            this.Route.Text = "... / Usuarios / Perfiles / Nuevo Perfil";
            // 
            // AddProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1230, 622);
            this.Controls.Add(this.CentralPanel);
            this.Controls.Add(this.SubPanel1);
            this.Controls.Add(this.SeperiorPanel);
            this.Controls.Add(this.MenuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Perfil";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddProfile_Load);
            this.MenuPanel.ResumeLayout(false);
            this.MenuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloudIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FondoMovil)).EndInit();
            this.CentralPanel.ResumeLayout(false);
            this.CentralPanel.PerformLayout();
            this.SubPanel1.ResumeLayout(false);
            this.SubPanel1.PerformLayout();
            this.SeperiorPanel.ResumeLayout(false);
            this.SeperiorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayuda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.TextBox Perfil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Tipo;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Label TitlePanel;
        private System.Windows.Forms.Panel CentralPanel;
        private System.Windows.Forms.Panel SubPanel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel SeperiorPanel;
        private System.Windows.Forms.Label Route;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.PictureBox FondoMovil;
        private System.Windows.Forms.PictureBox CloudIcon;
        private System.Windows.Forms.PictureBox Ayuda;
    }
}