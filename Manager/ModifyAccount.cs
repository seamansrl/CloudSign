using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace Horus
{
    public partial class ModifyAccount : Form
    {
        String ServerAPIURL = "" + Program.Server;
        Panel SubPanel;
        public ModifyAccount()
        {
            InitializeComponent();
        }

        private async void Actualizar()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpResponseMessage response;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                form.Add(new StringContent(this.Nombre.Text), "name");
                form.Add(new StringContent(this.Apellido.Text), "last_name");
                form.Add(new StringContent(this.Tipo_de_documento.Text), "id_type");
                form.Add(new StringContent(this.Numero_de_documento.Text), "id_number");
                form.Add(new StringContent(this.Pais.Text), "country");
                form.Add(new StringContent(this.Provincia.Text), "state");
                form.Add(new StringContent(this.Ciudad.Text), "city");
                form.Add(new StringContent(this.Direccion.Text), "adress");
                form.Add(new StringContent(this.Codigo_postal.Text), "postal_code");
                form.Add(new StringContent(this.Telefono.Text), "phone");
                form.Add(new StringContent(this.EMail.Text), "email");
                form.Add(new StringContent(this.Empresa.Text), "company");

                response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts", form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] != "200")
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                else
                    this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void GetPaises()
        {
            try
            {
                WebClient webClient = new WebClient();
                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/dictionary/countries");

                String[] RecivedMatrix = response.Split('|');

                this.PaisList.Items.Clear();

                if (RecivedMatrix[0] == "200")
                {
                    String[] ProfilesList = response.Split('\n');

                    foreach (String Profile in ProfilesList)
                    {
                        if (Profile != "")
                        {
                            RecivedMatrix = Profile.Split('|');

                            String code = RecivedMatrix[0];
                            String id = RecivedMatrix[1];
                            String name = RecivedMatrix[2];

                            this.PaisList.Items.Add(name + " | " + id);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void GetProvincias(String PaisID)
        {
            try
            {
                WebClient webClient = new WebClient();
                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/dictionary/countries=" + PaisID + "/states");

                String[] RecivedMatrix = response.Split('|');

                this.ProvinciaList.Items.Clear();

                if (RecivedMatrix[0] == "200")
                {
                    String[] ProfilesList = response.Split('\n');

                    foreach (String Profile in ProfilesList)
                    {
                        if (Profile != "")
                        {
                            RecivedMatrix = Profile.Split('|');

                            String code = RecivedMatrix[0];
                            String id = RecivedMatrix[1];
                            String name = RecivedMatrix[2];

                            this.ProvinciaList.Items.Add(name + " | " + id);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void GetCiudades(String StateID)
        {
            try
            {
                WebClient webClient = new WebClient();
                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/dictionary/countries/states=" + StateID + "/cities");

                String[] RecivedMatrix = response.Split('|');

                this.CiudadList.Items.Clear();

                if (RecivedMatrix[0] == "200")
                {
                    String[] ProfilesList = response.Split('\n');

                    foreach (String Profile in ProfilesList)
                    {
                        if (Profile != "")
                        {
                            RecivedMatrix = Profile.Split('|');

                            String code = RecivedMatrix[0];
                            String id = RecivedMatrix[1];
                            String name = RecivedMatrix[2];

                            this.CiudadList.Items.Add(name + " | " + id);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void GetDocTypes()
        {
            try
            {
                WebClient webClient = new WebClient();
                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/dictionary/documents");

                String[] RecivedMatrix = response.Split('|');

                this.DocTypesList.Items.Clear();

                if (RecivedMatrix[0] == "200")
                {
                    String[] ProfilesList = response.Split('\n');

                    foreach (String Profile in ProfilesList)
                    {
                        if (Profile != "")
                        {
                            RecivedMatrix = Profile.Split('|');

                            String code = RecivedMatrix[0];
                            String id = RecivedMatrix[1];
                            String type = RecivedMatrix[2];

                            this.DocTypesList.Items.Add(type + " | " + id);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void GetAccount()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts");

                String[] RecivedMatrix = response.Split('|');

                if (RecivedMatrix[0] == "200")
                {
                    this.Nombre.Text = RecivedMatrix[2];
                    this.Apellido.Text = RecivedMatrix[3];
                    this.Tipo_de_documento.Text = RecivedMatrix[4];
                    this.Numero_de_documento.Text = RecivedMatrix[5];
                    this.Pais.Text = RecivedMatrix[6];
                    this.Provincia.Text = RecivedMatrix[7];
                    this.Ciudad.Text = RecivedMatrix[8];
                    this.Direccion.Text = RecivedMatrix[9];
                    this.Codigo_postal.Text = RecivedMatrix[10];
                    this.Telefono.Text = RecivedMatrix[11];
                    this.EMail.Text = RecivedMatrix[12];
                    this.Empresa.Text = RecivedMatrix[13];
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void PaisList_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.PaisList.Text.Trim() != "")
                {
                    this.Pais.Text = this.PaisList.Text.Split('|')[1].Trim();
                    GetProvincias(this.Pais.Text.Trim());
                }
            }
            catch { }
        }

        private void ProvinciaList_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.ProvinciaList.Text.Trim() != "")
                {
                    this.Provincia.Text = this.ProvinciaList.Text.Split('|')[1].Trim();
                    GetCiudades(this.Provincia.Text.Trim());
                }
            }
            catch { }
        }

        private void CiudadList_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.CiudadList.Text.Trim() != "")
                {
                    this.Ciudad.Text = this.CiudadList.Text.Split('|')[1].Trim();
                }
            }
            catch { }
        }

        private void AddAccount_Load(object sender, EventArgs e)
        {
            GetAccount();
            GetPaises();
            GetDocTypes();
        }

        private void PaisList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProvinciaList.Items.Clear();
            this.ProvinciaList.Text = "";
            this.Provincia.Text = "";

            this.CiudadList.Items.Clear();
            this.CiudadList.Text = "";
            this.Ciudad.Text = "";
        }

        private void ProvinciaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CiudadList.Items.Clear();
            this.CiudadList.Text = "";
            this.Ciudad.Text = "";
        }

        private void DocTypesList_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.DocTypesList.Text.Trim() != "")
                {
                    this.Tipo_de_documento.Text = this.DocTypesList.Text.Trim().Split('|')[1];
                }
            }
            catch { }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        String GetAdminUser()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken);

                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users");

                String[] RecivedMatrix = response.Split('|');

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
                                if (Convert.ToBoolean(principal) == true)
                                    return uuid;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }

            return "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String UUID = GetAdminUser();

                if (UUID == "")
                    return;

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

        private void SubPanel_unload(object sender, FormClosedEventArgs e)
        {
            this.MainPanel.Visible = true;
            SubPanel.Visible = false;
            SubPanel.Dispose();
            SubPanel = null;
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            this.Nombre.Focus();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
