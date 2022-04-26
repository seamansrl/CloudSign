namespace Horus
{
    partial class Configuracion
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuracion));
            this.ListaDeConfiguraciones = new System.Windows.Forms.DataGridView();
            this.UUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ELIMINAR = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Leyenda = new System.Windows.Forms.TextBox();
            this.Valor = new System.Windows.Forms.TextBox();
            this.Parametro = new System.Windows.Forms.ComboBox();
            this.Aceptar = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.CentralPanel = new System.Windows.Forms.Panel();
            this.LogoProvider = new System.Windows.Forms.PictureBox();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.SeperiorPanel = new System.Windows.Forms.Panel();
            this.Ayuda = new System.Windows.Forms.PictureBox();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.Route = new System.Windows.Forms.Label();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.Button9 = new System.Windows.Forms.Button();
            this.CloudIcon = new System.Windows.Forms.PictureBox();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TitlePanel = new System.Windows.Forms.Label();
            this.FondoMovil = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.WaitAnimation = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ListaDeConfiguraciones)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.CentralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoProvider)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            this.SeperiorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayuda)).BeginInit();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloudIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FondoMovil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaitAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // ListaDeConfiguraciones
            // 
            this.ListaDeConfiguraciones.AllowUserToAddRows = false;
            this.ListaDeConfiguraciones.AllowUserToDeleteRows = false;
            this.ListaDeConfiguraciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListaDeConfiguraciones.BackgroundColor = System.Drawing.Color.White;
            this.ListaDeConfiguraciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListaDeConfiguraciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaDeConfiguraciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListaDeConfiguraciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaDeConfiguraciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UUID,
            this.PARAM,
            this.VAR,
            this.ELIMINAR});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListaDeConfiguraciones.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListaDeConfiguraciones.GridColor = System.Drawing.Color.LightBlue;
            this.ListaDeConfiguraciones.Location = new System.Drawing.Point(33, 226);
            this.ListaDeConfiguraciones.Name = "ListaDeConfiguraciones";
            this.ListaDeConfiguraciones.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaDeConfiguraciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ListaDeConfiguraciones.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.ListaDeConfiguraciones.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.ListaDeConfiguraciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListaDeConfiguraciones.ShowCellErrors = false;
            this.ListaDeConfiguraciones.ShowCellToolTips = false;
            this.ListaDeConfiguraciones.ShowEditingIcon = false;
            this.ListaDeConfiguraciones.ShowRowErrors = false;
            this.ListaDeConfiguraciones.Size = new System.Drawing.Size(959, 385);
            this.ListaDeConfiguraciones.TabIndex = 0;
            this.ListaDeConfiguraciones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListaDeDetecciones_CellContentClick);
            // 
            // UUID
            // 
            this.UUID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UUID.HeaderText = "UUID";
            this.UUID.Name = "UUID";
            this.UUID.ReadOnly = true;
            this.UUID.Visible = false;
            // 
            // PARAM
            // 
            this.PARAM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PARAM.HeaderText = "PARAMETRO";
            this.PARAM.MinimumWidth = 250;
            this.PARAM.Name = "PARAM";
            this.PARAM.ReadOnly = true;
            this.PARAM.Width = 250;
            // 
            // VAR
            // 
            this.VAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VAR.HeaderText = "VALOR";
            this.VAR.Name = "VAR";
            this.VAR.ReadOnly = true;
            this.VAR.Width = 81;
            // 
            // ELIMINAR
            // 
            this.ELIMINAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ELIMINAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ELIMINAR.HeaderText = "";
            this.ELIMINAR.MinimumWidth = 250;
            this.ELIMINAR.Name = "ELIMINAR";
            this.ELIMINAR.ReadOnly = true;
            this.ELIMINAR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ELIMINAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ELIMINAR.Text = "ELIMINAR";
            this.ELIMINAR.UseColumnTextForButtonValue = true;
            this.ELIMINAR.Width = 250;
            // 
            // Leyenda
            // 
            this.Leyenda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Leyenda.BackColor = System.Drawing.Color.White;
            this.Leyenda.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Leyenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Leyenda.ForeColor = System.Drawing.Color.Black;
            this.Leyenda.Location = new System.Drawing.Point(39, 103);
            this.Leyenda.Multiline = true;
            this.Leyenda.Name = "Leyenda";
            this.Leyenda.ReadOnly = true;
            this.Leyenda.Size = new System.Drawing.Size(953, 38);
            this.Leyenda.TabIndex = 29;
            this.Leyenda.Text = "Desde esta ventana podrá configurar una serie de parámetros que le permitirá ajus" +
    "tar al perfil a sus necesidades específicas de uso.";
            // 
            // Valor
            // 
            this.Valor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Valor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.Valor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Valor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Valor.Location = new System.Drawing.Point(237, 147);
            this.Valor.Name = "Valor";
            this.Valor.Size = new System.Drawing.Size(755, 19);
            this.Valor.TabIndex = 1;
            // 
            // Parametro
            // 
            this.Parametro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.Parametro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Parametro.FormattingEnabled = true;
            this.Parametro.Location = new System.Drawing.Point(40, 147);
            this.Parametro.Name = "Parametro";
            this.Parametro.Size = new System.Drawing.Size(191, 21);
            this.Parametro.TabIndex = 0;
            // 
            // Aceptar
            // 
            this.Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Aceptar.Location = new System.Drawing.Point(824, 173);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(168, 34);
            this.Aceptar.TabIndex = 2;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.CentralPanel);
            this.MainPanel.Controls.Add(this.SeperiorPanel);
            this.MainPanel.Controls.Add(this.MenuPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1275, 700);
            this.MainPanel.TabIndex = 3;
            this.MainPanel.Visible = false;
            // 
            // CentralPanel
            // 
            this.CentralPanel.BackColor = System.Drawing.Color.White;
            this.CentralPanel.Controls.Add(this.LogoProvider);
            this.CentralPanel.Controls.Add(this.ListaDeConfiguraciones);
            this.CentralPanel.Controls.Add(this.Valor);
            this.CentralPanel.Controls.Add(this.Leyenda);
            this.CentralPanel.Controls.Add(this.Parametro);
            this.CentralPanel.Controls.Add(this.HeaderPanel);
            this.CentralPanel.Controls.Add(this.Aceptar);
            this.CentralPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CentralPanel.Location = new System.Drawing.Point(250, 56);
            this.CentralPanel.Name = "CentralPanel";
            this.CentralPanel.Padding = new System.Windows.Forms.Padding(30);
            this.CentralPanel.Size = new System.Drawing.Size(1025, 644);
            this.CentralPanel.TabIndex = 31;
            // 
            // LogoProvider
            // 
            this.LogoProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LogoProvider.BackColor = System.Drawing.Color.White;
            this.LogoProvider.Image = global::Horus.Properties.Resources.logo_dynamic;
            this.LogoProvider.Location = new System.Drawing.Point(5, 584);
            this.LogoProvider.Name = "LogoProvider";
            this.LogoProvider.Size = new System.Drawing.Size(223, 56);
            this.LogoProvider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LogoProvider.TabIndex = 32;
            this.LogoProvider.TabStop = false;
            this.LogoProvider.Visible = false;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.DimGray;
            this.HeaderPanel.Controls.Add(this.label2);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(30, 30);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(965, 46);
            this.HeaderPanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(5, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Configuración";
            // 
            // SeperiorPanel
            // 
            this.SeperiorPanel.Controls.Add(this.Ayuda);
            this.SeperiorPanel.Controls.Add(this.LinePanel);
            this.SeperiorPanel.Controls.Add(this.Route);
            this.SeperiorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SeperiorPanel.Location = new System.Drawing.Point(250, 0);
            this.SeperiorPanel.Name = "SeperiorPanel";
            this.SeperiorPanel.Size = new System.Drawing.Size(1025, 56);
            this.SeperiorPanel.TabIndex = 32;
            // 
            // Ayuda
            // 
            this.Ayuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ayuda.Image = global::Horus.Properties.Resources.ayuda;
            this.Ayuda.Location = new System.Drawing.Point(982, 12);
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
            this.LinePanel.Size = new System.Drawing.Size(1025, 1);
            this.LinePanel.TabIndex = 6;
            // 
            // Route
            // 
            this.Route.AutoSize = true;
            this.Route.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Route.ForeColor = System.Drawing.Color.Gray;
            this.Route.Location = new System.Drawing.Point(24, 21);
            this.Route.Name = "Route";
            this.Route.Size = new System.Drawing.Size(269, 17);
            this.Route.TabIndex = 7;
            this.Route.Text = "... / Usuarios / Perfiles / Configuraciones";
            // 
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(47)))));
            this.MenuPanel.Controls.Add(this.Button9);
            this.MenuPanel.Controls.Add(this.CloudIcon);
            this.MenuPanel.Controls.Add(this.Button3);
            this.MenuPanel.Controls.Add(this.Button1);
            this.MenuPanel.Controls.Add(this.TitlePanel);
            this.MenuPanel.Controls.Add(this.FondoMovil);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(250, 700);
            this.MenuPanel.TabIndex = 30;
            // 
            // Button9
            // 
            this.Button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button9.ForeColor = System.Drawing.Color.White;
            this.Button9.Location = new System.Drawing.Point(0, 156);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(250, 48);
            this.Button9.TabIndex = 5;
            this.Button9.Text = "            Camaras";
            this.Button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button9.UseVisualStyleBackColor = false;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
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
            // Button3
            // 
            this.Button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button3.Enabled = false;
            this.Button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button3.ForeColor = System.Drawing.Color.White;
            this.Button3.Location = new System.Drawing.Point(0, 106);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(250, 48);
            this.Button3.TabIndex = 4;
            this.Button3.Text = "        Configuración";
            this.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button3.UseVisualStyleBackColor = false;
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
            this.Button1.TabIndex = 3;
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
            this.FondoMovil.Location = new System.Drawing.Point(0, 102);
            this.FondoMovil.Name = "FondoMovil";
            this.FondoMovil.Size = new System.Drawing.Size(251, 600);
            this.FondoMovil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.FondoMovil.TabIndex = 10;
            this.FondoMovil.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WaitAnimation
            // 
            this.WaitAnimation.Image = global::Horus.Properties.Resources.Loading;
            this.WaitAnimation.Location = new System.Drawing.Point(652, 270);
            this.WaitAnimation.Name = "WaitAnimation";
            this.WaitAnimation.Size = new System.Drawing.Size(160, 160);
            this.WaitAnimation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WaitAnimation.TabIndex = 4;
            this.WaitAnimation.TabStop = false;
            // 
            // Configuracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1275, 700);
            this.Controls.Add(this.WaitAnimation);
            this.Controls.Add(this.MainPanel);
            this.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configuracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuraciones";
            this.Load += new System.EventHandler(this.Administrator_Load);
            this.Resize += new System.EventHandler(this.Administrator_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ListaDeConfiguraciones)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.CentralPanel.ResumeLayout(false);
            this.CentralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoProvider)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.SeperiorPanel.ResumeLayout(false);
            this.SeperiorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayuda)).EndInit();
            this.MenuPanel.ResumeLayout(false);
            this.MenuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloudIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FondoMovil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaitAnimation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView ListaDeConfiguraciones;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TextBox Valor;
        private System.Windows.Forms.ComboBox Parametro;
        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.TextBox Leyenda;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Label TitlePanel;
        private System.Windows.Forms.Panel CentralPanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel SeperiorPanel;
        private System.Windows.Forms.Label Route;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Button Button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox WaitAnimation;
        private System.Windows.Forms.DataGridViewTextBoxColumn UUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PARAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAR;
        private System.Windows.Forms.DataGridViewButtonColumn ELIMINAR;
        private System.Windows.Forms.PictureBox FondoMovil;
        private System.Windows.Forms.PictureBox CloudIcon;
        private System.Windows.Forms.Button Button9;
        public System.Windows.Forms.PictureBox LogoProvider;
        private System.Windows.Forms.PictureBox Ayuda;
    }
}