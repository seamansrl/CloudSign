using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Horus
{
    public partial class Perfiles : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";
        String GlobalUSER = "";
        Panel SubPanel;

        public Perfiles(String UUID = "", String USER = "")
        {
            InitializeComponent();

            GlobalUUID = UUID;
            GlobalUSER = USER;
        }

        private void LoadProfiles()
        {
            this.MainPanel.Visible = false;
            this.WaitAnimation.Visible = true;
            Application.DoEvents();

            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());

                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users=" + GlobalUUID + "/profiles");

                String[] RecivedMatrix = response.Split('|');

                ListaDePerfiles.Rows.Clear();

                if (RecivedMatrix[0] == "200")
                {
                    String[] ProfilesList = response.Split('\n');

                    foreach (String Profile in ProfilesList)
                    {
                        if (Profile != "")
                        {
                            RecivedMatrix = Profile.Split('|');

                            String code = RecivedMatrix[0];
                            String uuid = RecivedMatrix[1];
                            String perfil = RecivedMatrix[2];
                            String bloqueado = RecivedMatrix[3];
                            String tipo = RecivedMatrix[4];

                            if (code == "200" && (tipo == "0" || tipo == "1"))
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(ListaDePerfiles);

                                row.Height = 40;

                                row.Cells[0].Value = perfil;
                                row.Cells[1].Value = uuid;
                                row.Cells[2].Value = bloqueado;
                                row.Cells[3].Value = tipo;


                                if (tipo == "1")
                                    row.Cells[4].Value = "CONFIGURAR";
                                else
                                    row.Cells[4].Value = "GESTIONAR";

                                ListaDePerfiles.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }

            this.WaitAnimation.Visible = false;
            this.MainPanel.Visible = true;
        }

        private async void Bloquear(String UUID)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpResponseMessage response;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                form.Add(new StringContent("true"), "lock");

                response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + UUID, form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] != "200")
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                else
                {
                    LoadProfiles();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private async void Desbloquear(String UUID)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpResponseMessage response;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                form.Add(new StringContent("false"), "lock");

                response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + UUID, form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] != "200")
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                else
                {
                    LoadProfiles();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Borrar(String UUID)
        {
            try
            {
                if (MessageBox.Show("Se dispone a borrar un perfil, al hacerlo eliminara tambien todos las detecciones, ¿quiere continuar?", "Atención!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    WebClient webClient = new WebClient();


                    webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                    String response = Encoding.ASCII.GetString(webClient.UploadValues(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + UUID, "DELETE", new System.Collections.Specialized.NameValueCollection()));

                    String[] RecivedMatrix = response.Split('|');

                    if (RecivedMatrix[0] != "200")
                        MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                    else
                    {
                        LoadProfiles();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Perfiles_Load(object sender, EventArgs e)
        {
            if (Program.ShowLogo)
                this.LogoProvider.Visible = true;
            else
                this.LogoProvider.Visible = false;

            Application.DoEvents();
        }

        private void Perfiles_Resize(object sender, EventArgs e) {
            WaitAnimation.Top = (this.Height / 2) - (WaitAnimation.Height / 2);
            WaitAnimation.Left = (this.Width / 2) - (WaitAnimation.Width / 2);

            if (this.Width < 1000)
                this.Route.Visible = false;
            else
                this.Route.Visible = true;

        }
        private void SubPanel_unload(object sender, FormClosedEventArgs e) {
            this.MainPanel.Visible = true;
            SubPanel.Visible = false;
            SubPanel.Dispose();
            SubPanel = null;
            LoadProfiles();
        }
        private void ListaDePerfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String UUID = "";
                String VALUE = "";
                String TYPE = "";

                if (e.ColumnIndex == 4 && e.RowIndex > -1)
                {
                    try
                    {
                        UUID = this.ListaDePerfiles.Rows[e.RowIndex].Cells[1].Value.ToString();
                        TYPE = this.ListaDePerfiles.Rows[e.RowIndex].Cells[3].Value.ToString();

                        if (TYPE == "1")
                        {
                            SubPanel = new Panel();
                            SubPanel.Dock = DockStyle.Fill;

                            this.Controls.Add(SubPanel);

                            SubPanel.Show();

                            this.MainPanel.Visible = false;

                            Configuracion configuracion_windows = new Configuracion(UUID);
                            configuracion_windows.TopLevel = false;
                            configuracion_windows.Dock = DockStyle.Fill;

                            SubPanel.Controls.Add(configuracion_windows);

                            configuracion_windows.Show();

                            configuracion_windows.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                        }
                        else
                        {
                            SubPanel = new Panel();
                            SubPanel.Dock = DockStyle.Fill;

                            this.Controls.Add(SubPanel);

                            SubPanel.Show();

                            this.MainPanel.Visible = false;

                            Detecciones Detecciones_Window = new Detecciones(UUID, this.ListaDePerfiles.Rows[e.RowIndex].Cells[3].Value.ToString());
                            Detecciones_Window.TopLevel = false;
                            Detecciones_Window.Dock = DockStyle.Fill;

                            SubPanel.Controls.Add(Detecciones_Window);

                            Detecciones_Window.Show();

                            Detecciones_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                        }
                    }
                    catch { }
                }


                if (e.ColumnIndex == 5 && e.RowIndex > -1)
                {
                    try
                    {
                        if (e.RowIndex > -1)
                            UUID = this.ListaDePerfiles.Rows[e.RowIndex].Cells[1].Value.ToString();

                        Borrar(UUID);
                    }
                    catch { }
                }

                if (e.ColumnIndex == 2 && e.RowIndex > -1)
                {
                    try
                    {
                        if (e.RowIndex > -1)
                        {
                            UUID = this.ListaDePerfiles.Rows[e.RowIndex].Cells[1].Value.ToString();
                            VALUE = this.ListaDePerfiles.Rows[e.RowIndex].Cells[2].Value.ToString();
                        }

                        if (VALUE == "False")
                        {
                            Bloquear(UUID);
                        }
                        else
                        {
                            Desbloquear(UUID);
                        }
                    }
                    catch { }
                }

                if (e.ColumnIndex == 6 && e.RowIndex > -1)
                {
                    try
                    {
                        if (e.RowIndex > -1)
                            UUID = this.ListaDePerfiles.Rows[e.RowIndex].Cells[1].Value.ToString();

                        SubPanel = new Panel();
                        SubPanel.Dock = DockStyle.Fill;

                        this.Controls.Add(SubPanel);

                        SubPanel.Show();

                        this.MainPanel.Visible = false;

                        ModifyProfile ModifyProfile_window = new ModifyProfile(UUID);
                        ModifyProfile_window.TopLevel = false;
                        ModifyProfile_window.Dock = DockStyle.Fill;

                        SubPanel.Controls.Add(ModifyProfile_window);

                        ModifyProfile_window.Show();

                        ModifyProfile_window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                    }
                    catch { }
                }

                if (e.ColumnIndex == 7 && e.RowIndex > -1)
                {
                    try
                    {
                        if (e.RowIndex > -1)
                        {
                            UUID = this.ListaDePerfiles.Rows[e.RowIndex].Cells[1].Value.ToString();
                            TYPE = this.ListaDePerfiles.Rows[e.RowIndex].Cells[3].Value.ToString();

                            SubPanel = new Panel();
                            SubPanel.Dock = DockStyle.Fill;

                            this.Controls.Add(SubPanel);

                            SubPanel.Show();

                            this.MainPanel.Visible = false;

                            ConfigDevice ConfigDevice_Window = new ConfigDevice(UUID, TYPE, GlobalUSER);
                            ConfigDevice_Window.TopLevel = false;
                            ConfigDevice_Window.Dock = DockStyle.Fill;

                            SubPanel.Controls.Add(ConfigDevice_Window);

                            ConfigDevice_Window.Show();

                            ConfigDevice_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                        }
                    }
                    catch { }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button2_Click(object sender, EventArgs e) {
            try {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                AddProfile AddProfile_Window = new AddProfile(GlobalUUID);
                AddProfile_Window.TopLevel = false;
                AddProfile_Window.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(AddProfile_Window);

                AddProfile_Window.Show();

                AddProfile_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button4_Click(object sender, EventArgs e) {
            try {
                ModifyAccount ModifyAccount_window = new ModifyAccount();
                ModifyAccount_window.ShowDialog();
                ModifyAccount_window.Close();
            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Button1_Click_1(object sender, EventArgs e) {
            this.Close();
        }

        private void Button4_Click_1(object sender, EventArgs e) {
            try {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                ModifyAccount ModifyAccount_window = new ModifyAccount();
                ModifyAccount_window.TopLevel = false;
                ModifyAccount_window.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(ModifyAccount_window);

                ModifyAccount_window.Show();

                ModifyAccount_window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Enabled = false;
            LoadProfiles();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Scaner Scaner_Window = new Scaner();
            Scaner_Window.Show();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
