using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Management;
using System.IO;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace Horus
{
    public partial class Scaner : Form
    {
        Thread myThread = null;

        public Scaner()
        {
            InitializeComponent();

            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = "En espera";
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public string GetURL(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch 
            {
                return "";
            }
        }

        public String GetIPAddress()
        {
            String IPAddress = ""; 
            String Str = "";

            try
            {
                Str = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
                IPAddress[] addr = ipEntry.AddressList;

                foreach (IPAddress ips in addr)
                    if (ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        String[] NetAddress = ips.ToString().Split('.');

                        this.txtIP.Items.Add(NetAddress[0] + "." + NetAddress[1] + "." + NetAddress[2] + ".[1 a 254]");
                    }
            }
            catch { }

            return IPAddress;
        } 

        public void scan(string subnet) 
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                Ping myPing;
                PingReply reply;
                IPAddress addr;

                String HostName = "";
                Int32 Version = 0;
                String Info = "";
                Boolean ConfigMode = false;

                progressBar1.Maximum = 254;
                progressBar1.Value = 0;
                listVAddr.Items.Clear();

                for (int i = 1; i < 254; i++)
                {
                    String[] NetAddress = subnet.Split('.');
                    subnet = NetAddress[0] + "." + NetAddress[1] + "." + NetAddress[2];

                    String subnetn = "." + i.ToString();
                    myPing = new Ping();
                    reply = myPing.Send(subnet + subnetn, 900);

                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Text = "Escaneando: " + subnet + subnetn;

                    if (reply.Status == IPStatus.Success)
                    {
                        HostName = "";

                        try
                        {
                            addr = IPAddress.Parse(subnet + subnetn);
                            Info = GetURL("http://" + addr.ToString() + "/info");

                            if (Info != "")
                            {
                                var deserializedResult = serializer.Deserialize<dynamic>(Info);

                                try { HostName = (String)deserializedResult["device_name"]; } catch { }
                                try { Version = (Int32)deserializedResult["version"]; } catch { }
                                try { ConfigMode = (Boolean)deserializedResult["config_mode"]; } catch { }

                                if (HostName != "")
                                    listVAddr.Items.Add(new ListViewItem(new String[] { subnet + subnetn, HostName, Version.ToString(), ConfigMode.ToString() }));
                            }
                        }
                        catch { }
                    }
                    progressBar1.Value += 1;
                }

                SaveToCSV(this.listVAddr);

                cmdScan.Enabled = true;
                cmdStop.Enabled = false;
                txtIP.Enabled = true;

                this.progressBar1.Value = 0;

                lblStatus.Text = "Listo!";
            }
            catch 
            {
                myThread.Abort();

                cmdScan.Enabled = true;
                cmdStop.Enabled = false;
                txtIP.Enabled = true;

                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "En espera";

                this.progressBar1.Value = 0;
            }
        }

        private void SaveToCSV(ListView DGV) {

            Int32 columnCount = DGV.Columns.Count;

            String[] output = new string[DGV.Items.Count];

            for (int i = 0; i < DGV.Items.Count; i++) {
                for (int j = 0; j < columnCount; j++) {
                    output[i] += DGV.Items[i].SubItems[j].Text + "|";
                }
            }

            File.WriteAllLines(@"c:\CloudSign\config\ips.config", output, System.Text.Encoding.UTF8);
        }

        private void cmdScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIP.Text == string.Empty)
                {
                    MessageBox.Show("Red no seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    myThread = new Thread(() => scan(txtIP.Text));
                    myThread.Start();

                    if (myThread.IsAlive == true)
                    {
                        cmdStop.Enabled = true;
                        cmdScan.Enabled = false;
                        txtIP.Enabled = false;
                    }
                }
            }
            catch { }
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            try
            {
                myThread.Abort();

                cmdScan.Enabled = true;
                cmdStop.Enabled = false;
                txtIP.Enabled = true;

                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "En espera";

                this.progressBar1.Value = 0;
            }
            catch { }
        }

        private void listVAddr_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (listVAddr.FocusedItem.Bounds.Contains(e.Location) == true)
                    {
                        if (this.listVAddr.SelectedItems[0].SubItems[3].Text.ToLower() == "true")
                            this.shutdownToolStripMenuItem.Checked = true;
                        else
                            this.shutdownToolStripMenuItem.Checked = false;

                        conMenuStripIP.Show(Cursor.Position);
                    }
                }
            }
            catch { }
        }


        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // SUBIR CONFIGURACIÓN

            this.PreviewPanel.Visible = true;
            this.PreviewPanelShadow.Visible = true;
            this.UsuarioFTP.Text = "";
            this.ClaveFTP.Text = "";

            this.CentralPanel.Enabled = false;
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // FUERA DE SERVICIO
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                String Info = "";
                String Host = listVAddr.SelectedItems[0].Text.ToString();

                if (Host.Trim() != "")
                {
                    Info = GetURL("http://" + Host + "/config");

                    if (Info != "")
                    {
                        var deserializedResult = serializer.Deserialize<dynamic>(Info);

                        try 
                        { 
                            this.shutdownToolStripMenuItem.Checked = (Boolean)deserializedResult["config_mode"];
                            this.listVAddr.SelectedItems[0].SubItems[3].Text = this.shutdownToolStripMenuItem.Checked.ToString();
                        } catch { }
                    }
                }
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String filename = @"c:\CloudSign\config\ips.config";

            if (File.Exists(filename)) {
                String[] lines = File.ReadAllLines(filename);

                foreach (String line in lines) {

                    String[] Elements = line.Split('|');

                    if (Elements[1] != "")
                        listVAddr.Items.Add(new ListViewItem(new String[] { Elements[0], Elements[1], Elements[2], Elements[3] }));

                }
            }

            this.txtIP.Text = GetIPAddress();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.PreviewPanel.Visible = false;
            this.PreviewPanelShadow.Visible = false;
            this.UsuarioFTP.Text = "";
            this.ClaveFTP.Text = "";
            this.CentralPanel.Enabled = true;

        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            this.PreviewPanel.Visible = false;
            this.PreviewPanelShadow.Visible = false;
            this.UsuarioFTP.Text = "";
            this.ClaveFTP.Text = "";
            this.CentralPanel.Enabled = true;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.UsuarioFTP.Text.Trim() == "" || this.ClaveFTP.Text.Trim() == "")
                    return;

                this.PreviewPanel.Visible = false;
                this.PreviewPanelShadow.Visible = false;
                this.CentralPanel.Enabled = true;

                this.openFileDialog1.Title = "Seleccione el archivo de configuración";
                this.openFileDialog1.FileName = "";
                this.openFileDialog1.Filter = "Archivos de configuración (*.json)|*.json";

                // codigo para abrir el cuadro de dialogo
                if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DialogResult DialogResult = MessageBox.Show(this, "Se dispone a cargar una nueva configuración al dispositivo Cloud Sign. Verifique que todos los parámetros sean correctos o podría perder comunicación con el equipo. " + Environment.NewLine + Environment.NewLine + "¿Continuamos?", "Atención", MessageBoxButtons.OKCancel);
                    if (DialogResult == DialogResult.OK)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();

                        Boolean ConfigMode = false;
                        String Host = listVAddr.SelectedItems[0].Text.ToString();

                        String Info = GetURL("http://" + Host + "/info");

                        if (Info != "")
                        {
                            var deserializedResult = serializer.Deserialize<dynamic>(Info);

                            try { ConfigMode = (Boolean)deserializedResult["config_mode"]; } catch { }

                            if (ConfigMode == false)
                            {
                                GetURL("http://" + Host + "/config");

                                WebClient client = new WebClient();
                                client.Credentials = new NetworkCredential(this.UsuarioFTP.Text, this.ClaveFTP.Text);
                                client.UploadFile("ftp://" + Host + "/config.json", openFileDialog1.FileName);

                                GetURL("http://" + Host + "/config");
                            }
                            else
                            {
                                WebClient client = new WebClient();
                                client.Credentials = new NetworkCredential(this.UsuarioFTP.Text, this.ClaveFTP.Text);
                                client.UploadFile("ftp://" + Host + "/config.json", openFileDialog1.FileName);
                            }

                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                MessageBox.Show(this, "Configuración cargada", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show(this, "Error al subir el archivo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void vistaLocalToolStripMenuItem_Click(object sender, EventArgs e) {

            String IP = listVAddr.SelectedItems[0].Text.ToString();

            if (IP.Trim() != "") {

                LocalView LocalView_window = new LocalView(IP);

                LocalView_window.Show();
            }
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
