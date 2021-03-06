using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Horus
{
    public partial class AddUser : Form
    {
        String ServerAPIURL = "" + Program.Server;

        public AddUser()
        {
            InitializeComponent();
        }

        private async void AddNewUser()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpResponseMessage response;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                form.Add(new StringContent(this.Usuario.Text.Trim()), "user");
                form.Add(new StringContent(this.Clave.Text.Trim()), "password");

                response = await httpClient.PostAsync(ServerAPIURL + "/api/v2/admin/accounts/users", form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] != "201")
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                else
                    this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            this.Usuario.Focus();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
