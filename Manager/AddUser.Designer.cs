namespace Horus
{
    partial class AddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUser));
            this.Cancelar = new System.Windows.Forms.Button();
            this.Aceptar = new System.Windows.Forms.Button();
            this.ClaveRepeat = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Clave = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Usuario = new System.Windows.Forms.TextBox();
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
            this.Cancelar.ForeColor = System.Drawing.Color.DimGray;
            this.Cancelar.Location = new System.Drawing.Point(816, 497);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(168, 50);
            this.Cancelar.TabIndex = 4;
            this.Cancelar.Text = "CANCELAR";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // Aceptar
            // 
            this.Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar.ForeColor = System.Drawing.Color.DimGray;
            this.Aceptar.Location = new System.Drawing.Point(642, 497);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(168, 50);
            this.Aceptar.TabIndex = 3;
            this.Aceptar.Text = "ACEPTAR";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // ClaveRepeat
            // 
            this.ClaveRepeat.BackColor = System.Drawing.Color.White;
            this.ClaveRepeat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClaveRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ClaveRepeat.ForeColor = System.Drawing.Color.Black;
            this.ClaveRepeat.Location = new System.Drawing.Point(179, 181);
            this.ClaveRepeat.MaxLength = 40;
            this.ClaveRepeat.Name = "ClaveRepeat";
            this.ClaveRepeat.PasswordChar = '*';
            this.ClaveRepeat.Size = new System.Drawing.Size(455, 19);
            this.ClaveRepeat.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(58, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 16);
            this.label11.TabIndex = 20;
            this.label11.Text = "Repita clave:";
            // 
            // Clave
            // 
            this.Clave.BackColor = System.Drawing.Color.White;
            this.Clave.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Clave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Clave.ForeColor = System.Drawing.Color.Black;
            this.Clave.Location = new System.Drawing.Point(179, 156);
            this.Clave.MaxLength = 40;
            this.Clave.Name = "Clave";
            this.Clave.PasswordChar = '*';
            this.Clave.Size = new System.Drawing.Size(455, 19);
            this.Clave.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(58, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nueva Clave:";
            // 
            // Usuario
            // 
            this.Usuario.BackColor = System.Drawing.Color.White;
            this.Usuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Usuario.ForeColor = System.Drawing.Color.Black;
            this.Usuario.Location = new System.Drawing.Point(179, 131);
            this.Usuario.MaxLength = 200;
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(455, 19);
            this.Usuario.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(58, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
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
            this.MenuPanel.Size = new System.Drawing.Size(250, 661);
            this.MenuPanel.TabIndex = 20;
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
            this.Button2.TabIndex = 6;
            this.Button2.Text = "        Nuevo usuario";
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
            this.Button1.TabIndex = 5;
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
            this.FondoMovil.Location = new System.Drawing.Point(0, 62);
            this.FondoMovil.Name = "FondoMovil";
            this.FondoMovil.Size = new System.Drawing.Size(251, 600);
            this.FondoMovil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.FondoMovil.TabIndex = 11;
            this.FondoMovil.TabStop = false;
            // 
            // CentralPanel
            // 
            this.CentralPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.CentralPanel.Controls.Add(this.ClaveRepeat);
            this.CentralPanel.Controls.Add(this.Cancelar);
            this.CentralPanel.Controls.Add(this.label11);
            this.CentralPanel.Controls.Add(this.Aceptar);
            this.CentralPanel.Controls.Add(this.Clave);
            this.CentralPanel.Controls.Add(this.label2);
            this.CentralPanel.Controls.Add(this.Usuario);
            this.CentralPanel.Controls.Add(this.label1);
            this.CentralPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CentralPanel.Location = new System.Drawing.Point(250, 102);
            this.CentralPanel.Name = "CentralPanel";
            this.CentralPanel.Padding = new System.Windows.Forms.Padding(30);
            this.CentralPanel.Size = new System.Drawing.Size(996, 559);
            this.CentralPanel.TabIndex = 35;
            // 
            // SubPanel1
            // 
            this.SubPanel1.BackColor = System.Drawing.Color.White;
            this.SubPanel1.Controls.Add(this.label16);
            this.SubPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.SubPanel1.Location = new System.Drawing.Point(250, 56);
            this.SubPanel1.Name = "SubPanel1";
            this.SubPanel1.Size = new System.Drawing.Size(996, 46);
            this.SubPanel1.TabIndex = 36;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label16.Location = new System.Drawing.Point(23, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(139, 22);
            this.label16.TabIndex = 1;
            this.label16.Text = "Nuevo recinto";
            // 
            // SeperiorPanel
            // 
            this.SeperiorPanel.Controls.Add(this.Ayuda);
            this.SeperiorPanel.Controls.Add(this.LinePanel);
            this.SeperiorPanel.Controls.Add(this.Route);
            this.SeperiorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SeperiorPanel.Location = new System.Drawing.Point(250, 0);
            this.SeperiorPanel.Name = "SeperiorPanel";
            this.SeperiorPanel.Size = new System.Drawing.Size(996, 56);
            this.SeperiorPanel.TabIndex = 34;
            // 
            // Ayuda
            // 
            this.Ayuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ayuda.Image = global::Horus.Properties.Resources.ayuda;
            this.Ayuda.Location = new System.Drawing.Point(953, 13);
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
            this.LinePanel.Size = new System.Drawing.Size(996, 1);
            this.LinePanel.TabIndex = 6;
            // 
            // Route
            // 
            this.Route.AutoSize = true;
            this.Route.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Route.ForeColor = System.Drawing.Color.Gray;
            this.Route.Location = new System.Drawing.Point(24, 21);
            this.Route.Name = "Route";
            this.Route.Size = new System.Drawing.Size(191, 17);
            this.Route.TabIndex = 7;
            this.Route.Text = "... / Usuarios / Nuevo recinto";
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1246, 661);
            this.Controls.Add(this.CentralPanel);
            this.Controls.Add(this.SubPanel1);
            this.Controls.Add(this.SeperiorPanel);
            this.Controls.Add(this.MenuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Usuario";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddUser_Load);
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
        private System.Windows.Forms.TextBox ClaveRepeat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Clave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Usuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Label TitlePanel;
        private System.Windows.Forms.Button Button2;
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