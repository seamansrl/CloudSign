namespace Horus
{
    partial class Cuentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cuentas));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.CentralPanel = new System.Windows.Forms.Panel();
            this.LogoProvider = new System.Windows.Forms.PictureBox();
            this.ListaDeUsuario = new System.Windows.Forms.DataGridView();
            this.NOMBRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RESETEAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRINCIPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BLOQUEADO = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PERFILES = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ELIMINAR = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CAMBIAR_CLAVE = new System.Windows.Forms.DataGridViewButtonColumn();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.Icon = new System.Windows.Forms.PictureBox();
            this.Leyenda = new System.Windows.Forms.TextBox();
            this.Titulo = new System.Windows.Forms.Label();
            this.SeperiorPanel = new System.Windows.Forms.Panel();
            this.Ayuda = new System.Windows.Forms.PictureBox();
            this.LineaPanelV = new System.Windows.Forms.Panel();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.Button2 = new System.Windows.Forms.Button();
            this.Route = new System.Windows.Forms.Label();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.CloudIcon = new System.Windows.Forms.PictureBox();
            this.LineaPanel = new System.Windows.Forms.Panel();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TitlePanel = new System.Windows.Forms.Label();
            this.FondoMovil = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.WaitAnimation = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            this.CentralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListaDeUsuario)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
            this.SeperiorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayuda)).BeginInit();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloudIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FondoMovil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaitAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.CentralPanel);
            this.MainPanel.Controls.Add(this.HeaderPanel);
            this.MainPanel.Controls.Add(this.SeperiorPanel);
            this.MainPanel.Controls.Add(this.MenuPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1262, 700);
            this.MainPanel.TabIndex = 2;
            this.MainPanel.Visible = false;
            // 
            // CentralPanel
            // 
            this.CentralPanel.BackColor = System.Drawing.Color.White;
            this.CentralPanel.Controls.Add(this.LogoProvider);
            this.CentralPanel.Controls.Add(this.ListaDeUsuario);
            this.CentralPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CentralPanel.Location = new System.Drawing.Point(250, 207);
            this.CentralPanel.Name = "CentralPanel";
            this.CentralPanel.Padding = new System.Windows.Forms.Padding(30);
            this.CentralPanel.Size = new System.Drawing.Size(1012, 493);
            this.CentralPanel.TabIndex = 8;
            // 
            // LogoProvider
            // 
            this.LogoProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LogoProvider.BackColor = System.Drawing.Color.White;
            this.LogoProvider.Image = global::Horus.Properties.Resources.logo_dynamic;
            this.LogoProvider.Location = new System.Drawing.Point(4, 433);
            this.LogoProvider.Name = "LogoProvider";
            this.LogoProvider.Size = new System.Drawing.Size(223, 56);
            this.LogoProvider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LogoProvider.TabIndex = 32;
            this.LogoProvider.TabStop = false;
            this.LogoProvider.Visible = false;
            // 
            // ListaDeUsuario
            // 
            this.ListaDeUsuario.AllowUserToAddRows = false;
            this.ListaDeUsuario.AllowUserToDeleteRows = false;
            this.ListaDeUsuario.BackgroundColor = System.Drawing.Color.White;
            this.ListaDeUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListaDeUsuario.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ListaDeUsuario.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaDeUsuario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ListaDeUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaDeUsuario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMBRE,
            this.UUID,
            this.RESETEAR,
            this.PRINCIPAL,
            this.BLOQUEADO,
            this.PERFILES,
            this.ELIMINAR,
            this.CAMBIAR_CLAVE});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListaDeUsuario.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListaDeUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListaDeUsuario.EnableHeadersVisualStyles = false;
            this.ListaDeUsuario.GridColor = System.Drawing.Color.LightBlue;
            this.ListaDeUsuario.Location = new System.Drawing.Point(30, 30);
            this.ListaDeUsuario.MultiSelect = false;
            this.ListaDeUsuario.Name = "ListaDeUsuario";
            this.ListaDeUsuario.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaDeUsuario.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ListaDeUsuario.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.ListaDeUsuario.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.ListaDeUsuario.RowTemplate.Height = 40;
            this.ListaDeUsuario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListaDeUsuario.ShowCellErrors = false;
            this.ListaDeUsuario.ShowCellToolTips = false;
            this.ListaDeUsuario.ShowEditingIcon = false;
            this.ListaDeUsuario.ShowRowErrors = false;
            this.ListaDeUsuario.Size = new System.Drawing.Size(952, 433);
            this.ListaDeUsuario.TabIndex = 0;
            this.ListaDeUsuario.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListaDeUsuario_CellContentClick);
            // 
            // NOMBRE
            // 
            this.NOMBRE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NOMBRE.Frozen = true;
            this.NOMBRE.HeaderText = "USUARIO";
            this.NOMBRE.MinimumWidth = 300;
            this.NOMBRE.Name = "NOMBRE";
            this.NOMBRE.ReadOnly = true;
            this.NOMBRE.Width = 300;
            // 
            // UUID
            // 
            this.UUID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UUID.Frozen = true;
            this.UUID.HeaderText = "UUID DEL USUARIO";
            this.UUID.Name = "UUID";
            this.UUID.ReadOnly = true;
            this.UUID.Visible = false;
            // 
            // RESETEAR
            // 
            this.RESETEAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RESETEAR.Frozen = true;
            this.RESETEAR.HeaderText = "RESETEAR";
            this.RESETEAR.Name = "RESETEAR";
            this.RESETEAR.ReadOnly = true;
            this.RESETEAR.Visible = false;
            // 
            // PRINCIPAL
            // 
            this.PRINCIPAL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PRINCIPAL.Frozen = true;
            this.PRINCIPAL.HeaderText = "PRINCIPAL";
            this.PRINCIPAL.Name = "PRINCIPAL";
            this.PRINCIPAL.ReadOnly = true;
            this.PRINCIPAL.Visible = false;
            // 
            // BLOQUEADO
            // 
            this.BLOQUEADO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BLOQUEADO.Frozen = true;
            this.BLOQUEADO.HeaderText = "BLOQUEADO";
            this.BLOQUEADO.Name = "BLOQUEADO";
            this.BLOQUEADO.ReadOnly = true;
            this.BLOQUEADO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BLOQUEADO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.BLOQUEADO.Width = 126;
            // 
            // PERFILES
            // 
            this.PERFILES.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PERFILES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PERFILES.Frozen = true;
            this.PERFILES.HeaderText = "";
            this.PERFILES.Name = "PERFILES";
            this.PERFILES.ReadOnly = true;
            this.PERFILES.Text = "CONFIGURAR PERFILES";
            this.PERFILES.UseColumnTextForButtonValue = true;
            this.PERFILES.Width = 5;
            // 
            // ELIMINAR
            // 
            this.ELIMINAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ELIMINAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ELIMINAR.Frozen = true;
            this.ELIMINAR.HeaderText = "";
            this.ELIMINAR.Name = "ELIMINAR";
            this.ELIMINAR.ReadOnly = true;
            this.ELIMINAR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ELIMINAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ELIMINAR.Text = "ELIMINAR";
            this.ELIMINAR.UseColumnTextForButtonValue = true;
            this.ELIMINAR.Width = 18;
            // 
            // CAMBIAR_CLAVE
            // 
            this.CAMBIAR_CLAVE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CAMBIAR_CLAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CAMBIAR_CLAVE.Frozen = true;
            this.CAMBIAR_CLAVE.HeaderText = "";
            this.CAMBIAR_CLAVE.Name = "CAMBIAR_CLAVE";
            this.CAMBIAR_CLAVE.ReadOnly = true;
            this.CAMBIAR_CLAVE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CAMBIAR_CLAVE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CAMBIAR_CLAVE.Text = "CAMBIAR CLAVE";
            this.CAMBIAR_CLAVE.UseColumnTextForButtonValue = true;
            this.CAMBIAR_CLAVE.Width = 18;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.White;
            this.HeaderPanel.Controls.Add(this.Icon);
            this.HeaderPanel.Controls.Add(this.Leyenda);
            this.HeaderPanel.Controls.Add(this.Titulo);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(250, 56);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(1012, 151);
            this.HeaderPanel.TabIndex = 9;
            // 
            // Icon
            // 
            this.Icon.Image = ((System.Drawing.Image)(resources.GetObject("Icon.Image")));
            this.Icon.Location = new System.Drawing.Point(6, 6);
            this.Icon.Name = "Icon";
            this.Icon.Size = new System.Drawing.Size(116, 145);
            this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Icon.TabIndex = 32;
            this.Icon.TabStop = false;
            // 
            // Leyenda
            // 
            this.Leyenda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Leyenda.BackColor = System.Drawing.Color.White;
            this.Leyenda.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Leyenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Leyenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Leyenda.Location = new System.Drawing.Point(163, 53);
            this.Leyenda.Multiline = true;
            this.Leyenda.Name = "Leyenda";
            this.Leyenda.ReadOnly = true;
            this.Leyenda.Size = new System.Drawing.Size(819, 85);
            this.Leyenda.TabIndex = 28;
            this.Leyenda.Text = resources.GetString("Leyenda.Text");
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Titulo.Location = new System.Drawing.Point(132, 25);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(91, 22);
            this.Titulo.TabIndex = 1;
            this.Titulo.Text = "Recintos";
            // 
            // SeperiorPanel
            // 
            this.SeperiorPanel.Controls.Add(this.Ayuda);
            this.SeperiorPanel.Controls.Add(this.LineaPanelV);
            this.SeperiorPanel.Controls.Add(this.LinePanel);
            this.SeperiorPanel.Controls.Add(this.Button2);
            this.SeperiorPanel.Controls.Add(this.Route);
            this.SeperiorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SeperiorPanel.Location = new System.Drawing.Point(250, 0);
            this.SeperiorPanel.Name = "SeperiorPanel";
            this.SeperiorPanel.Size = new System.Drawing.Size(1012, 56);
            this.SeperiorPanel.TabIndex = 7;
            // 
            // Ayuda
            // 
            this.Ayuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ayuda.Image = global::Horus.Properties.Resources.ayuda;
            this.Ayuda.Location = new System.Drawing.Point(969, 13);
            this.Ayuda.Name = "Ayuda";
            this.Ayuda.Size = new System.Drawing.Size(31, 29);
            this.Ayuda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ayuda.TabIndex = 33;
            this.Ayuda.TabStop = false;
            this.Ayuda.Click += new System.EventHandler(this.Ayuda_Click);
            // 
            // LineaPanelV
            // 
            this.LineaPanelV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineaPanelV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LineaPanelV.Location = new System.Drawing.Point(841, 0);
            this.LineaPanelV.Name = "LineaPanelV";
            this.LineaPanelV.Size = new System.Drawing.Size(1, 56);
            this.LineaPanelV.TabIndex = 8;
            // 
            // LinePanel
            // 
            this.LinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LinePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LinePanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LinePanel.Location = new System.Drawing.Point(0, 55);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(1012, 1);
            this.LinePanel.TabIndex = 6;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(112)))), ((int)(((byte)(178)))));
            this.Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(112)))), ((int)(((byte)(178)))));
            this.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(112)))), ((int)(((byte)(178)))));
            this.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(150)))), ((int)(((byte)(230)))));
            this.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.ForeColor = System.Drawing.Color.White;
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button2.Location = new System.Drawing.Point(659, 1);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(168, 55);
            this.Button2.TabIndex = 2;
            this.Button2.Text = "  Agregar edificio";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Route
            // 
            this.Route.AutoSize = true;
            this.Route.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Route.ForeColor = System.Drawing.Color.Gray;
            this.Route.Location = new System.Drawing.Point(24, 21);
            this.Route.Name = "Route";
            this.Route.Size = new System.Drawing.Size(143, 17);
            this.Route.TabIndex = 7;
            this.Route.Text = "Mi Cuenta / Recintos";
            // 
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.MenuPanel.Controls.Add(this.CloudIcon);
            this.MenuPanel.Controls.Add(this.LineaPanel);
            this.MenuPanel.Controls.Add(this.Button4);
            this.MenuPanel.Controls.Add(this.Button1);
            this.MenuPanel.Controls.Add(this.TitlePanel);
            this.MenuPanel.Controls.Add(this.FondoMovil);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(250, 700);
            this.MenuPanel.TabIndex = 6;
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
            // LineaPanel
            // 
            this.LineaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineaPanel.BackColor = System.Drawing.Color.White;
            this.LineaPanel.Location = new System.Drawing.Point(0, 648);
            this.LineaPanel.Name = "LineaPanel";
            this.LineaPanel.Size = new System.Drawing.Size(250, 1);
            this.LineaPanel.TabIndex = 6;
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(41)))), ((int)(((byte)(52)))));
            this.Button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(63)))), ((int)(((byte)(72)))));
            this.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button4.ForeColor = System.Drawing.Color.White;
            this.Button4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Button4.Location = new System.Drawing.Point(0, 650);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(250, 48);
            this.Button4.TabIndex = 3;
            this.Button4.Text = "         Cuenta";
            this.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button4.UseVisualStyleBackColor = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
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
            this.Button1.TabIndex = 1;
            this.Button1.Text = "    Logout";
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
            this.FondoMovil.Image = global::Horus.Properties.Resources.Edificios;
            this.FondoMovil.Location = new System.Drawing.Point(0, 48);
            this.FondoMovil.Name = "FondoMovil";
            this.FondoMovil.Size = new System.Drawing.Size(251, 600);
            this.FondoMovil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.FondoMovil.TabIndex = 8;
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
            this.WaitAnimation.Location = new System.Drawing.Point(685, 262);
            this.WaitAnimation.Name = "WaitAnimation";
            this.WaitAnimation.Size = new System.Drawing.Size(160, 160);
            this.WaitAnimation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WaitAnimation.TabIndex = 5;
            this.WaitAnimation.TabStop = false;
            // 
            // Cuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1262, 700);
            this.Controls.Add(this.WaitAnimation);
            this.Controls.Add(this.MainPanel);
            this.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Cuentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrator";
            this.Load += new System.EventHandler(this.Administrator_Load);
            this.Resize += new System.EventHandler(this.Administrator_Resize);
            this.MainPanel.ResumeLayout(false);
            this.CentralPanel.ResumeLayout(false);
            this.CentralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListaDeUsuario)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
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

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel CentralPanel;
        private System.Windows.Forms.DataGridView ListaDeUsuario;
        private System.Windows.Forms.Panel SeperiorPanel;
        private System.Windows.Forms.Panel LineaPanelV;
        private System.Windows.Forms.Label Route;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Panel LineaPanel;
        private System.Windows.Forms.Button Button4;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Label TitlePanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox WaitAnimation;
        private System.Windows.Forms.PictureBox FondoMovil;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.TextBox Leyenda;
        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.PictureBox Icon;
        private System.Windows.Forms.PictureBox CloudIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMBRE;
        private System.Windows.Forms.DataGridViewTextBoxColumn UUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RESETEAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRINCIPAL;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BLOQUEADO;
        private System.Windows.Forms.DataGridViewButtonColumn PERFILES;
        private System.Windows.Forms.DataGridViewButtonColumn ELIMINAR;
        private System.Windows.Forms.DataGridViewButtonColumn CAMBIAR_CLAVE;
        public System.Windows.Forms.PictureBox LogoProvider;
        private System.Windows.Forms.PictureBox Ayuda;
    }
}