using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Horus
{
    public partial class ConfigDevice: Form
    {
        Panel SubPanel;
        String GlobalUUID = "";
        String GlobalTYPE = "";
        String GlobalUSER = "";
        public ConfigDevice(String UUID = "", String TYPE = "", String USER = "")
        {
            GlobalUUID = UUID.Trim();
            GlobalTYPE = TYPE;
            GlobalUSER = USER;

            InitializeComponent();
        }
        private void SubPanel_unload(object sender, FormClosedEventArgs e) {
            this.MainPanel.Visible = true;
            SubPanel.Visible = false;
            SubPanel.Dispose();
            SubPanel = null;

            this.Close();
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            String JsonFile = "";

            try
            {
                JsonFile += "{" + Environment.NewLine;

                if (this.ssid.Text.Trim() != "" || this.password.Text.Trim() != "")
                {
                    JsonFile += "\"ssid\":\"" + this.ssid.Text.Trim() + "\"," + Environment.NewLine;
                    JsonFile += "\"password\":\"" + this.password.Text.Trim() + "\"," + Environment.NewLine;


                    if (this.ftp_user.Text.Trim() != "" || this.ftp_password.Text.Trim() != "")
                    {
                        JsonFile += "\"ftp_user\":\"" + this.ftp_user.Text.Trim() + "\"," + Environment.NewLine;
                        JsonFile += "\"ftp_password\":\"" + this.ftp_password.Text.Trim() + "\"," + Environment.NewLine;
                    }

                    if (this.api_user.Text.Trim() != "" && this.api_password.Text.Trim() != "" && this.api_profile_uuid.Text.Trim() != "")
                    {
                        JsonFile += "\"api_user\":\"" + this.api_user.Text.Trim() + "\"," + Environment.NewLine;
                        JsonFile += "\"api_password\":\"" + this.api_password.Text.Trim() + "\"," + Environment.NewLine;
                        JsonFile += "\"api_profile_uuid\":\"" + this.api_profile_uuid.Text.Trim() + "\"," + Environment.NewLine;
                    }
                    else
                    {
                        this.id_not_required.Checked = true;
                        MessageBox.Show(this, "Faltan datos de acceso al servicio en nube por lo cual se desactivaran estas funciones avanzadas.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    if (this.ok_url.Text.Trim() != "" && this.ok_verb.Text.Trim() != "")
                    {
                        if (this.ok_url.Text.Length >= 10)
                        {
                            if (this.ok_url.Text.Trim().Substring(0, 7).ToLower() != "http://" && this.ok_url.Text.Trim().Substring(0, 8).ToLower() != "https://")
                                this.ok_url.Text = "http://" + this.ok_url.Text;

                            JsonFile += "\"ok_url\":\"" + this.ok_url.Text.Trim() + "\"," + Environment.NewLine;
                            JsonFile += "\"ok_verb\":\"" + this.ok_verb.Text.Trim() + "\"," + Environment.NewLine;
                        }
                    }

                    if (this.fail_tmp_url.Text.Trim() != "" && this.fail_temp_verb.Text.Trim() != "")
                    {
                        if (this.fail_tmp_url.Text.Length >= 10)
                        {
                            if (this.fail_tmp_url.Text.Trim().Substring(0, 7).ToLower() != "http://" && this.fail_tmp_url.Text.Trim().Substring(0, 8).ToLower() != "https://")
                                this.fail_tmp_url.Text = "http://" + this.fail_tmp_url.Text;

                            JsonFile += "\"fail_tmp_url\":\"" + this.fail_tmp_url.Text.Trim() + "\"," + Environment.NewLine;
                            JsonFile += "\"fail_temp_verb\":\"" + this.fail_temp_verb.Text.Trim() + "\"," + Environment.NewLine;
                        }
                    }

                    if (this.fail_Unknow_url.Text.Trim() != "" && this.fail_unknow_verb.Text.Trim() != "")
                    {
                        if (this.fail_Unknow_url.Text.Length >= 10)
                        {
                            if (this.fail_Unknow_url.Text.Trim().Substring(0, 7).ToLower() != "http://" && this.fail_Unknow_url.Text.Trim().Substring(0, 8).ToLower() != "https://")
                                this.fail_Unknow_url.Text = "http://" + this.fail_Unknow_url.Text;

                            JsonFile += "\"fail_Unknow_url\":\"" + this.fail_Unknow_url.Text.Trim() + "\"," + Environment.NewLine;
                            JsonFile += "\"fail_unknow_verb\":\"" + this.fail_unknow_verb.Text.Trim() + "\"," + Environment.NewLine;
                        }
                    }

                    if (this.horus_mode.SelectedIndex.ToString().Trim() != "")
                        JsonFile += "\"horus_mode\":" + this.horus_mode.SelectedIndex.ToString() + "," + Environment.NewLine;
                    else
                        JsonFile += "\"horus_mode\":0," + Environment.NewLine;

                    JsonFile += "\"horus_auth\":1," + Environment.NewLine;
                }
                else
                {
                    MessageBox.Show(this, "No se especifico la red WIFI a usar por lo cual se desactivaran las funciones de red. Esto generara que no pueda volver a acceder al dispositivo de forma remota", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (this.min_temp.Text.Trim() != "")
                {
                    if (Convert.ToInt32(this.min_temp.Text.Trim()) < 25 || Convert.ToInt32(this.min_temp.Text.Trim()) > 45)
                        this.min_temp.Text = "25";

                    JsonFile += "\"min_temp\":" + this.min_temp.Text.Trim() + "," + Environment.NewLine;
                }

                if (this.max_temp.Text.Trim() != "")
                {
                    if (Convert.ToInt32(this.max_temp.Text.Trim()) < 25 || Convert.ToInt32(this.max_temp.Text.Trim()) > 45)
                        this.max_temp.Text = "45";

                    JsonFile += "\"max_temp\":" + this.max_temp.Text.Trim() + "," + Environment.NewLine;
                }

                if (this.alert_temp.Text.Trim() != "")
                {
                    if (Convert.ToInt32(this.alert_temp.Text.Trim()) < 25 || Convert.ToInt32(this.alert_temp.Text.Trim()) > 45)
                        this.alert_temp.Text = "39";

                    JsonFile += "\"alert_temp\":" + this.alert_temp.Text.Trim() + "," + Environment.NewLine;
                }

                if (this.alert_temp.Text.Trim() != "")
                    JsonFile += "\"device_name\":\"" + this.device_name.Text.Trim() + "\"," + Environment.NewLine;
                else
                    JsonFile += "\"device_name\":\"" + "Cam_" + DateTime.Now.ToString("yyMMddhhmmss");



                if (this.action_wait.Text.Trim() != "")
                    JsonFile += "\"action_wait\":" + this.action_wait.Text + "," + Environment.NewLine;
                else
                    JsonFile += "\"action_wait\":4000," + Environment.NewLine;

                if (this.ttl_time_out.Text.Trim() != "")
                    JsonFile += "\"ttl_time_out\":" + this.ttl_time_out.Text + "," + Environment.NewLine;
                else
                    JsonFile += "\"ttl_time_out\":4000," + Environment.NewLine;


                JsonFile += "\"face_not_required\":" + this.face_not_required.Checked.ToString().ToLower() + "," + Environment.NewLine;
                JsonFile += "\"id_not_required\":" + this.id_not_required.Checked.ToString().ToLower() + "," + Environment.NewLine;
                JsonFile += "\"tem_not_required\":" + this.tem_not_required.Checked.ToString().ToLower() + "," + Environment.NewLine;

                String SignClockOffSet = this.clock_offset.Text.Trim().Substring(0, 1).Replace("0", "");
                Int32 ValueClockOffSet = Convert.ToInt32(this.clock_offset.Text.Trim().Replace("+", "").Replace("-", ""));
                ValueClockOffSet = 3600 * ValueClockOffSet;

                JsonFile += "\"clock_offset\":" + SignClockOffSet + ValueClockOffSet.ToString().ToLower() + "," + Environment.NewLine;

                JsonFile += "\"custom_vars\":\"\"" + Environment.NewLine;

                JsonFile += "}";

            }
            catch 
            {
                MessageBox.Show(this, "Hay errores en la configuración", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Preview.Text = JsonFile;

            this.PreviewPanel.Visible = true;
            this.PreviewPanelShadow.Visible = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfigDevice_Load(object sender, EventArgs e)
        {
            this.api_profile_uuid.Text = GlobalUUID;
            this.api_user.Text = GlobalUSER;
            this.ftp_user.Text = "admin";
            this.ftp_password.Text = "CloudSign";
            this.min_temp.Text = "33";
            this.max_temp.Text = "45";
            this.alert_temp.Text = "39";
            this.horus_mode.SelectedIndex = Convert.ToInt32(GlobalTYPE);
            this.device_name.Text = "Cam_" + DateTime.Now.ToString("yyMMddhhmmss");

            this.ssid.Focus();
            
        }

        private void horus_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.horus_mode.SelectedIndex == 1)
                this.face_not_required.Checked = true;
            else
                this.face_not_required.Checked = false;
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            this.PreviewPanelShadow.Visible = false;
            this.PreviewPanel.Visible = false;
        }

        private void Exportar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "c:\\";
                    saveFileDialog.Filter = "json file (*.json)|*.json";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        String path = saveFileDialog.FileName;

                        if (!File.Exists(path))
                            File.WriteAllText(path, this.Preview.Text, Encoding.GetEncoding(28591));

                        this.PreviewPanelShadow.Visible = false;
                        this.PreviewPanel.Visible = false;
                    }
                }
            }
            catch { }
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
