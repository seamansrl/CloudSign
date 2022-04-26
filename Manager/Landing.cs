using System;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using ZedGraph;

namespace Horus
{
    public partial class Landing : Form {
        String ServerAPIURL = "" + Program.Server;
        Panel SubPanel;

        public Landing() {
            InitializeComponent();
        }

        private async void Access() {

            this.MainPanel.Visible = false;
            this.WaitAnimation.Visible = true;

            try {
                if (this.Clave.Text.Trim() != "" && this.Usuario.Text.Trim() != "") {
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    HttpResponseMessage response;

                    form.Add(new StringContent(this.Usuario.Text.Trim()), "user");
                    form.Add(new StringContent(this.Clave.Text.Trim()), "password");

                    response = await httpClient.PostAsync(ServerAPIURL + "/api/v2/admin/login", form);

                    response.EnsureSuccessStatusCode();
                    httpClient.Dispose();
                    String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                    if (RecivedMatrix[0] == "200") {
                        Program.LogInToken = RecivedMatrix[1];


                        SubPanel = new Panel();
                        SubPanel.Dock = DockStyle.Fill;

                        this.Controls.Add(SubPanel);

                        SubPanel.Show();

                        this.MainPanel.Visible = false;

                        Cuentas AdminitradorWindow = new Cuentas();
                        AdminitradorWindow.TopLevel = false;
                        AdminitradorWindow.Dock = DockStyle.Fill;

                        SubPanel.Controls.Add(AdminitradorWindow);

                        AdminitradorWindow.Show();

                        AdminitradorWindow.FormClosed += new FormClosedEventHandler(SubPanel_unload);

                        this.Usuario.Text = "";
                        this.Clave.Text = "";
                    }
                    else {
                        this.MainPanel.Visible = true;
                        this.WaitAnimation.Visible = false;
                        Application.DoEvents();
                        MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                    }
                }
                else {
                    this.MainPanel.Visible = true;
                    this.WaitAnimation.Visible = false;
                    Application.DoEvents();
                    MessageBox.Show("Faltan datos", "Atención", MessageBoxButtons.OK);
                }
            }
            catch (Exception Ex) {
                this.MainPanel.Visible = true;
                this.WaitAnimation.Visible = false;
                Application.DoEvents();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }

            this.WaitAnimation.Visible = false;
        }


        private void Alta_Click(object sender, EventArgs e) {
            try {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                AddAccount AdministradorDeCaras = new AddAccount();
                AdministradorDeCaras.TopLevel = false;
                AdministradorDeCaras.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(AdministradorDeCaras);

                AdministradorDeCaras.Show();

                AdministradorDeCaras.FormClosed += new FormClosedEventHandler(SubPanel_unload);

                this.Usuario.Text = "";
                this.Clave.Text = "";
            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void SubPanel_unload(object sender, FormClosedEventArgs e) {
            this.MainPanel.Visible = true;
            SubPanel.Visible = false;
            SubPanel.Dispose();
            SubPanel = null;
        }

        private void Aceptar_Click(object sender, EventArgs e) {
            Access();
        }

        private void Landing_Resize(object sender, EventArgs e) {
            LoginPanel.Top = (this.Height / 2) - (LoginPanel.Height / 2);
            //LoginPanel.Left = (this.Width / 2) - (LoginPanel.Width / 2);

            WaitAnimation.Top = (this.Height / 2) - (WaitAnimation.Height / 2);
            WaitAnimation.Left = (this.Width / 2) - (WaitAnimation.Width / 2);

            Application.DoEvents();
        }

        private void Landing_Load(object sender, EventArgs e) {

            // CREO DIRECTORIOS DE BASE
            if (!Directory.Exists(@"c:\CloudSign\log")) {
                Directory.CreateDirectory(@"c:\CloudSign\log");
            }

            if (!Directory.Exists(@"c:\CloudSign\log\images")) {
                Directory.CreateDirectory(@"c:\CloudSign\log\images");
            }

            if (!Directory.Exists(@"c:\CloudSign\config")) {
                Directory.CreateDirectory(@"c:\CloudSign\config");
            }

            if (!Directory.Exists(@"c:\CloudSign\database")) {
                Directory.CreateDirectory(@"c:\CloudSign\database");
            }

            if (!Directory.Exists(@"c:\CloudSign\images")) {
                Directory.CreateDirectory(@"c:\CloudSign\images");
            }

            if (!Directory.Exists(@"c:\CloudSign\devices")) {
                Directory.CreateDirectory(@"c:\CloudSign\devices");
            }

            if (Program.ShowLogo)
                this.LogoProvider.Visible = true;
            else
                this.LogoProvider.Visible = false;

            this.LoginPanel.Left = 0;

            for (Int32 LeftMove = (this.LoginPanel.Width * -1); LeftMove < -9; LeftMove = LeftMove + 30)
            {
                this.LoginPanel.Left = LeftMove;
                Landing_Resize(sender, e);
            }

            this.Usuario.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("https://www.proyectohorus.com.ar");
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
