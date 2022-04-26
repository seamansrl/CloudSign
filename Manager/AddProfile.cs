using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace Horus
{
    public partial class AddProfile : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";

        public AddProfile(String UUID)
        {
            InitializeComponent();

            GlobalUUID = UUID;
        }

        private async void AddNewProfile()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpResponseMessage response;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                form.Add(new StringContent(this.Tipo.SelectedIndex.ToString()), "profiletype");
                form.Add(new StringContent(this.Perfil.Text.Trim()), "profilename");
                form.Add(new StringContent("FASTCNN-FACE-MASK"), "catalog");

                response = await httpClient.PostAsync(ServerAPIURL + "/api/v2/admin/accounts/users=" + GlobalUUID + "/profiles", form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();

                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] != "201")
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                else
                {
                    if (this.Tipo.SelectedIndex == 1)
                    {
                        String UUID = "";

                        // ACTUALIZO EL NOMBRE DEL OK DEL TAPABOCA
                        httpClient = new HttpClient();
                        form = new MultipartFormDataContent();

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                        form.Add(new StringContent("BIENVENIDO"), "name");

                        response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + RecivedMatrix[1] + "/detections=07bd1b46563911eabb289c5a44391055", form);

                        response.EnsureSuccessStatusCode();
                        httpClient.Dispose();

                        // ACTUALIZO EL VALOR DEL OK DEL TAPABOCA
                        httpClient = new HttpClient();
                        form = new MultipartFormDataContent();

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                        form.Add(new StringContent("{ \"user\":\"Bienvenido\",\"id\":\"1\", \"accessfrom\":0,\"accessto\":2359,\"day0\":true,\"day1\":true,\"day2\":true,\"day3\":true,\"day4\":true,\"day5\":true,\"day6\":true, \"active\":true}"), "value");


                        response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + RecivedMatrix[1] + "/detections=07bd1b46563911eabb289c5a44391055", form);

                        response.EnsureSuccessStatusCode();
                        httpClient.Dispose();

                         
                        UUID = "";

                        // ACTUALIZO EL NOMBRE DEL FAIL DEL TAPABOCA
                        httpClient = new HttpClient();
                        form = new MultipartFormDataContent();

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                        form.Add(new StringContent("USE TAPABOCA"), "name");

                        response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + RecivedMatrix[1] + "/detections=07bd1b2a563911ea92e7ac5a44391055", form);

                        response.EnsureSuccessStatusCode();
                        httpClient.Dispose();

                        // ACTUALIZO EL VALOR DEL FAIL DEL TAPABOCA
                        httpClient = new HttpClient();
                        form = new MultipartFormDataContent();

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                        form.Add(new StringContent("{ \"user\":\"Use tapaboca\",\"id\":\"1\", \"accessfrom\":0,\"accessto\":2359,\"day0\":true,\"day1\":true,\"day2\":true,\"day3\":true,\"day4\":true,\"day5\":true,\"day6\":true, \"active\":false,\"leyenda\":\"Use tapabocas\"}"), "value");

                        response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + RecivedMatrix[1] + "/detections=07bd1b2a563911ea92e7ac5a44391055", form);

                        response.EnsureSuccessStatusCode();
                        httpClient.Dispose();
                    }

                    this.Close();
                }
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
            AddNewProfile();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void AddProfile_Load(object sender, EventArgs e)
        {
            this.Perfil.Focus();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
