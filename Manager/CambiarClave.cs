using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Horus
{
    public partial class CambiarClave : Form
    {
        String GlobalUUID = "";
        String ServerAPIURL = "" + Program.Server;

        public CambiarClave(String UUID = "")
        {
            InitializeComponent();

            GlobalUUID = UUID;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void AplicarCambios()
        {
            try
            {
                if (this.Clave.Text.Trim() == this.ClaveRepeat.Text.Trim())
                {
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    HttpResponseMessage response;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                    form.Add(new StringContent(this.Clave.Text.Trim()), "password");

                    response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users=" + GlobalUUID, form);

                    response.EnsureSuccessStatusCode();
                    httpClient.Dispose();
                    String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');


                    if (RecivedMatrix[0] != "200")
                        MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                    else
                        this.Close();
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
            AplicarCambios();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void CambiarClave_Load(object sender, EventArgs e)
        {
            this.Clave.Focus();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
