using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Horus
{
    public partial class Activate : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String globalAccount = "";

        public Activate(String Account = "")
        {
            InitializeComponent();
            globalAccount = Account;
        }

        private void Activate_Load(object sender, EventArgs e)
        {
            this.Cuenta.Text = globalAccount;

            this.Usuario.Focus();
        }

        private async void CreateUser()
        {
            try
            {
                if (this.Clave.Text.Trim() == this.ClaveRepeat.Text.Trim())
                {
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    HttpResponseMessage response;

                    form.Add(new StringContent(this.Usuario.Text.Trim()), "user");
                    form.Add(new StringContent(this.Clave.Text.Trim()), "password");
                    form.Add(new StringContent("true"), "activate");

                    response = await httpClient.PostAsync(ServerAPIURL + "/api/v2/admin/accounts=" + this.Cuenta.Text.Trim() + "/users", form);

                    response.EnsureSuccessStatusCode();
                    httpClient.Dispose();
                    String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                    if (RecivedMatrix[0] == "200")
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Las claves no coinciden", "Atención", MessageBoxButtons.OK);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            CreateUser();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
