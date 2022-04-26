using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Horus
{
    public partial class Cuentas : Form
    {
        String ServerAPIURL = "" + Program.Server;
        Panel SubPanel;

        public Cuentas()
        {
            InitializeComponent();
        }

        private void LoadUsers()
        {
            this.MainPanel.Visible = false;
            this.WaitAnimation.Visible = true;
            Application.DoEvents();

            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken);

                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users");

                String[] RecivedMatrix = response.Split('|');

                ListaDeUsuario.Rows.Clear();

                if (RecivedMatrix[0] == "200")
                {
                    String[] UserList = response.Split('\n');

                    foreach (String User in UserList)
                    {
                        if (User != "")
                        {
                            RecivedMatrix = User.Split('|');

                            String code = RecivedMatrix[0];
                            String uuid = RecivedMatrix[1];
                            String user = RecivedMatrix[2];
                            String resetear = RecivedMatrix[3];
                            String principal = RecivedMatrix[4];
                            String bloqueado = RecivedMatrix[5];

                            if (code == "200")
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(ListaDeUsuario);

                                row.Height = 40;

                                if (principal == "False")
                                {
                                    row.Cells[0].Value = user;
                                    row.Cells[1].Value = uuid;
                                    row.Cells[2].Value = resetear;
                                    row.Cells[3].Value = principal;
                                    row.Cells[4].Value = bloqueado;
                                    ListaDeUsuario.Rows.Add(row);
                                }
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

                response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users=" + UUID, form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] != "200")
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                else
                {
                    LoadUsers();
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

                response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users=" + UUID, form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] != "200")
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                else
                {
                    LoadUsers();
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
                if (MessageBox.Show("Se dispone a borrar un usuario, al hacerlo eliminara tambien todos sus perfiles y detecciones, ¿quiere continuar?", "Atención!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    WebClient webClient = new WebClient();


                    webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                    String response = Encoding.ASCII.GetString(webClient.UploadValues(ServerAPIURL + "/api/v2/admin/accounts/users=" + UUID, "DELETE", new System.Collections.Specialized.NameValueCollection()));

                    String[] RecivedMatrix = response.Split('|');


                    if (RecivedMatrix[0] != "200")
                        MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                    else
                    {
                        LoadUsers();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            if (Program.ShowLogo)
                this.LogoProvider.Visible = true;
            else
                this.LogoProvider.Visible = false;

            Application.DoEvents();
        }

        private void Administrator_Resize(object sender, EventArgs e) {
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

            LoadUsers();
        }

        private void ListaDeUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String USER = "";
            String UUID = "";
            String VALUE = "";


            if (e.ColumnIndex == 5 && e.RowIndex > -1)
            {
                try
                {
                    UUID = this.ListaDeUsuario.Rows[e.RowIndex].Cells[1].Value.ToString();
                    USER = this.ListaDeUsuario.Rows[e.RowIndex].Cells[0].Value.ToString();

                    SubPanel = new Panel();
                    SubPanel.Dock = DockStyle.Fill;

                    this.Controls.Add(SubPanel);

                    SubPanel.Show();

                    this.MainPanel.Visible = false;

                    Perfiles AdminitradorWindow = new Perfiles(UUID, USER);
                    AdminitradorWindow.TopLevel = false;
                    AdminitradorWindow.Dock = DockStyle.Fill;

                    SubPanel.Controls.Add(AdminitradorWindow);

                    AdminitradorWindow.Show();

                    AdminitradorWindow.FormClosed += new FormClosedEventHandler(SubPanel_unload);

                }
                catch { }
            }


            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {
                try
                {
                    if (e.RowIndex > -1)
                    {
                        UUID = this.ListaDeUsuario.Rows[e.RowIndex].Cells[1].Value.ToString();
                        VALUE = this.ListaDeUsuario.Rows[e.RowIndex].Cells[3].Value.ToString();
                    }

                    if (VALUE == "False")
                    {
                        Borrar(UUID);
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar una cuenta principal", "Atención", MessageBoxButtons.OK);
                    }
                }
                catch { }
            }

            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                try
                {
                    if (e.RowIndex > -1)
                    {
                        UUID = this.ListaDeUsuario.Rows[e.RowIndex].Cells[1].Value.ToString();
                        VALUE = this.ListaDeUsuario.Rows[e.RowIndex].Cells[4].Value.ToString();
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

            if (e.ColumnIndex == 7 && e.RowIndex > -1)
            {
                try
                {
                    if (e.RowIndex > -1)
                        UUID = this.ListaDeUsuario.Rows[e.RowIndex].Cells[1].Value.ToString();


                    SubPanel = new Panel();
                    SubPanel.Dock = DockStyle.Fill;

                    this.Controls.Add(SubPanel);

                    SubPanel.Show();

                    this.MainPanel.Visible = false;

                    CambiarClave CambiarClave_Window = new CambiarClave(UUID);
                    CambiarClave_Window.TopLevel = false;
                    CambiarClave_Window.Dock = DockStyle.Fill;

                    SubPanel.Controls.Add(CambiarClave_Window);

                    CambiarClave_Window.Show();

                    CambiarClave_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e) 
        {
            try 
            {
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
            catch (Exception Ex) 
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button2_Click(object sender, EventArgs e) 
        {
            try {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                AddUser AddUser_Window = new AddUser();
                AddUser_Window.TopLevel = false;
                AddUser_Window.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(AddUser_Window);

                AddUser_Window.Show();

                AddUser_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
            }
            catch (Exception Ex) 
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Enabled = false;
            LoadUsers();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
