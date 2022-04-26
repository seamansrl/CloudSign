using System;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Horus
{
    public partial class Datalog : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";
        Panel SubPanel;
        Boolean IsLoaded = false;
        public Datalog(String UUID = "")
        {
            InitializeComponent();

            GlobalUUID = UUID;
        }

        private void LoadDataLog()
        {
            this.MainPanel.Visible = false;
            this.WaitAnimation.Visible = true;
            Application.DoEvents();

            try
            {
                System.Threading.Thread.Sleep(100);


                LoadList();
            }
            catch { }

            this.WaitAnimation.Visible = false;
            this.MainPanel.Visible = true;
            IsLoaded = true;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private void LoadList()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken);

                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users/profiles/instances=" + GlobalUUID + "/log");

                String[] RecivedMatrix = response.Split('|');

                ListaDeDatalog.Rows.Clear();

                if (RecivedMatrix[0] == "200")
                {
                    String[] UserList = response.Split('\n');

                    foreach (String User in UserList)
                    {
                        Application.DoEvents();
                        if (User != "")
                        {
                            RecivedMatrix = User.Split('|');

                            String code = RecivedMatrix[0];
                            String uuid = RecivedMatrix[1];
                            String timestamp = UnixTimeStampToDateTime(Convert.ToDouble(RecivedMatrix[2])).ToString("yyyy-MM-dd hh:mm:ss");
                            String fullvalue = "";
                            String value = "";

                            for (int i = 4; i < RecivedMatrix.Length; i++)
                            {
                                if (RecivedMatrix[i] != null)
                                    fullvalue = fullvalue + RecivedMatrix[i] + "|";
                            }

                            if (fullvalue != "")
                            {
                                try
                                {
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    var deserializedResult = serializer.Deserialize<dynamic>(fullvalue.Replace("|", ""));

                                    value = deserializedResult["user"];
                                }
                                catch
                                {
                                    value = fullvalue.Replace("|", "");
                                }
                            }

                            if (code == "200")
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(ListaDeDatalog);

                                row.Height = 40;

                                row.Cells[0].Value = uuid;
                                row.Cells[1].Value = timestamp;
                                row.Cells[2].Value = value;
                                ListaDeDatalog.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private void Administrator_Load(object sender, EventArgs e)
        {
            if (Program.ShowLogo)
                this.LogoProvider.Visible = true;
            else
                this.LogoProvider.Visible = false;

            Application.DoEvents();
        }

        private void Administrator_Resize(object sender, EventArgs e) {
            WaitAnimation.Top = (this.Height / 2) - (WaitAnimation.Height / 2);
            WaitAnimation.Left = (this.Width / 2) - (WaitAnimation.Width / 2);

            if (this.Width < 1000)
                this.Route.Visible = false;
            else
                this.Route.Visible = true;
        }
        private void SubPanel_unload(object sender, FormClosedEventArgs e) {
            this.MainPanel.Visible = true;
            SubPanel.Visible = false;
            SubPanel.Dispose();
            SubPanel = null;
            LoadDataLog();
        }
        private void ListaDeDatalog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
 
        }
        private void Button4_Click_1(object sender, EventArgs e) {
            try {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                ModifyAccount ModifyAccount_window = new ModifyAccount();
                ModifyAccount_window.TopLevel = false;
                ModifyAccount_window.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(ModifyAccount_window);

                ModifyAccount_window.Show();

                ModifyAccount_window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Interval = 5000;
            if (IsLoaded)
                LoadList();
            else
                LoadDataLog();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
