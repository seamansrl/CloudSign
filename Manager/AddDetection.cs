using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Horus
{
    public partial class AddDetection : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";
        String Valor = "";

        private async void AgregarDeteccion()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpResponseMessage response;

                Valor = "{";
                Valor = Valor + "\"user\":\"" + this.Nombre.Text.Trim() + "\",";
                Valor = Valor + "\"id\":\"" + this.DNI.Text.Trim() + "\",";
                Valor = Valor + "\"accessfrom\":" + this.HoraDesde.Value.ToString("HHmm") + ",";
                Valor = Valor + "\"accessto\":" + this.HoraHasta.Value.ToString("HHmm") + ",";

                if (this.Day0.Checked)
                    Valor = Valor + "\"day0\":true,";
                else
                    Valor = Valor + "\"day0\":false,";

                if (this.Day1.Checked)
                    Valor = Valor + "\"day1\":true,";
                else
                    Valor = Valor + "\"day1\":false,";

                if (this.Day2.Checked)
                    Valor = Valor + "\"day2\":true,";
                else
                    Valor = Valor + "\"day2\":false,";

                if (this.Day3.Checked)
                    Valor = Valor + "\"day3\":true,";
                else
                    Valor = Valor + "\"day3\":false,";

                if (this.Day4.Checked)
                    Valor = Valor + "\"day4\":true,";
                else
                    Valor = Valor + "\"day4\":false,";

                if (this.Day5.Checked)
                    Valor = Valor + "\"day5\":true,";
                else
                    Valor = Valor + "\"day5\":false,";

                if (this.Day6.Checked)
                    Valor = Valor + "\"day6\":true,";
                else
                    Valor = Valor + "\"day6\":false,";

                if (this.Activo.Checked)
                    Valor = Valor + "\"active\":true,";
                else
                {
                    Valor = Valor + "\"active\":false,";

                    if (this.Motivo.Text.Trim() != "")
                        Valor = Valor + "\"leyenda\":\"" + this.Motivo.Text.Trim() + "\",";
                }

                if (this.TargetList.Rows.Count > 0)
                {
                    Valor = Valor + "\"target\":[";

                    Int32 Count = 0;

                    foreach (DataGridViewRow row in this.TargetList.Rows)
                    {
                        Valor = Valor + "\"" + row.Cells[0].Value.ToString().ToLower().Trim() + "\"";

                        Count++;
                        if (Count != this.TargetList.Rows.Count)
                            Valor = Valor + ",";
                    }

                    Valor = Valor + "],";
                }

                if (this.GeneroFemenino.Checked)
                    Valor = Valor + "\"gender\":\"f\"";
                else if (this.GeneroMasculino.Checked)
                    Valor = Valor + "\"gender\":\"m\"";
                else
                    Valor = Valor + "\"gender\":\"o\"";


                Valor = Valor + "}";



                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                form.Add(new StringContent(this.Nombre.Text.Trim()), "name");
                form.Add(new StringContent(Valor.Trim()), "value");

                response = await httpClient.PostAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalUUID + "/detections", form);

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


        public AddDetection(String UUID)
        {
            InitializeComponent();

            GlobalUUID = UUID;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            AgregarDeteccion();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void AddTarget_Click(object sender, EventArgs e)
        {
            if (this.Target.Text.Trim() == "")
                return;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(this.TargetList);


            row.Cells[0].Value = this.Target.Text;
            this.TargetList.Rows.Add(row);

            if (this.TargetList.Rows.Count > 0)
                this.PanelDeAviso.Visible = false;
            else
                this.PanelDeAviso.Visible = true;

            this.Target.Text = "";
        }

        private void Activo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Activo.Checked == true)
                this.Motivo.Enabled = false;
            else
                this.Motivo.Enabled = true;
        }

        private void TargetList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.TargetList.Rows.Remove(this.TargetList.Rows[e.RowIndex]);

            if (this.TargetList.Rows.Count > 0)
                this.PanelDeAviso.Visible = false;
            else
                this.PanelDeAviso.Visible = true;
        }

        private void AddDetection_Load(object sender, EventArgs e)
        {
            this.Nombre.Focus();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
