using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace Horus
{
    public partial class ModifyProfile : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";

        public ModifyProfile(String UUID)
        {
            InitializeComponent();

            GlobalUUID = UUID;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Aceptar_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpResponseMessage response;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
            form.Add(new StringContent(this.Perfil.Text.Trim()), "profilename");

            response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalUUID + "/name", form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

            if (RecivedMatrix[0] != "200")
                MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
            else
            {
                this.Close();
            }
        }

        private void LoadProfileName()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());

            String response1 = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalUUID + "/name");

            String[] RecivedMatrix = response1.Split('|');

            if (RecivedMatrix[0] == "200")
            {
                this.Perfil.Text = RecivedMatrix[1];
            }
        }

        private void AddProfile_Load(object sender, EventArgs e)
        {
            LoadProfileName();

            this.Perfil.Focus();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
