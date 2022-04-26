using System;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace Horus
{
    public partial class ModifyDetection : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";
        String GlobalProfileUUID = "";
        String Valor = "";

        public ModifyDetection(String UUID, String ProfileUUID)
        {
            InitializeComponent();

            GlobalUUID = UUID;
            GlobalProfileUUID = ProfileUUID;
        }
        private void LoadDetectionName()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());

            String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalProfileUUID + "/detections=" + GlobalUUID + "/name");

            String[] RecivedMatrix = response.Split('|');

            if (RecivedMatrix[0] == "200")
            {
                this.Nombre.Text = RecivedMatrix[1];
            }
        }

        private void LoadDetectionValue()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());

            String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalProfileUUID + "/detections=" + GlobalUUID + "/value");

            String[] RecivedMatrix = response.Split('|');

            if (RecivedMatrix[0] == "200")
            {
                Valor = RecivedMatrix[1];


                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var deserializedResult = serializer.Deserialize<dynamic>(Valor);

                try { this.Nombre.Text = deserializedResult["user"];} catch { }
                try { this.DNI.Text = deserializedResult["id"];} catch { }
                try { this.HoraDesde.Value = Convert.ToDateTime(deserializedResult["accessfrom"].ToString("00:00"));} catch { }
                try { this.HoraHasta.Value = Convert.ToDateTime(deserializedResult["accessto"].ToString("00:00"));} catch { }
                try { this.Day0.Checked = (Boolean)deserializedResult["day0"];} catch { }
                try { this.Day1.Checked = (Boolean)deserializedResult["day1"];} catch { }
                try { this.Day2.Checked = (Boolean)deserializedResult["day2"];} catch { }
                try { this.Day3.Checked = (Boolean)deserializedResult["day3"];} catch { }
                try { this.Day4.Checked = (Boolean)deserializedResult["day4"];} catch { }
                try { this.Day5.Checked = (Boolean)deserializedResult["day5"];} catch { }
                try { this.Day6.Checked = (Boolean)deserializedResult["day6"];} catch { }
                try { this.Activo.Checked = (Boolean)deserializedResult["active"];} catch { }
                try { this.Motivo.Text = deserializedResult["leyenda"];} catch { }

                try
                {
                    var TargetArray = deserializedResult["target"];

                    foreach (var TargetVar in TargetArray)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(this.TargetList);

                        row.Cells[0].Value = (String)TargetVar;
                        this.TargetList.Rows.Add(row);
                    }

                }
                catch { }

                if (this.TargetList.Rows.Count > 0)
                    this.PanelDeAviso.Visible = false;
                else
                    this.PanelDeAviso.Visible = true;

                try
                {
                    if ((String)deserializedResult["gender"] == "m")
                        this.GeneroMasculino.Checked = true;
                    else if ((String)deserializedResult["gender"] == "f")
                        this.GeneroFemenino.Checked = true;
                    else
                        this.GeneroOtro.Checked = true;
                }
                catch { }

            }
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

            response = await httpClient.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalProfileUUID + "/detections=" + GlobalUUID, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

            if (RecivedMatrix[0] != "200")
                MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
            else
            {
                HttpClient httpClient1 = new HttpClient();
                MultipartFormDataContent form1 = new MultipartFormDataContent();
                HttpResponseMessage response1;

                httpClient1.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                form1.Add(new StringContent(Valor.Trim()), "value");

                response1 = await httpClient1.PutAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalProfileUUID + "/detections=" + GlobalUUID, form1);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix1 = response1.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix1[0] != "200")
                    MessageBox.Show(RecivedMatrix1[1], "Atención", MessageBoxButtons.OK);
                else
                {
                    this.Close();
                }
            }
        }

        private void ModifyDetection_Load(object sender, EventArgs e)
        {
            LoadDetectionName();
            LoadDetectionValue();

            this.Nombre.Focus();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Activo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Activo.Checked == true)
                this.Motivo.Enabled = false;
            else
                this.Motivo.Enabled = true;
        }

        private void AddTarget_Click(object sender, EventArgs e)
        {
            if (this.Target.Text.Trim() == "")
                return;

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(this.TargetList);

            row.Cells[0].Value = this.Target.Text.Trim().ToLower();
            this.TargetList.Rows.Add(row);

            if (this.TargetList.Rows.Count > 0)
                this.PanelDeAviso.Visible = false;
            else
                this.PanelDeAviso.Visible = true;

            this.Target.Text = "";
        }

        private void TargetList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.TargetList.Rows.Remove(this.TargetList.Rows[e.RowIndex]);

            if (this.TargetList.Rows.Count > 0)
                this.PanelDeAviso.Visible = false;
            else
                this.PanelDeAviso.Visible = true;
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
