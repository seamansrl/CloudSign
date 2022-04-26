using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Horus
{
    public partial class Camaras : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";
        Panel SubPanel;
        Boolean IsLoaded = false;
        Bitmap bitmap;
        String ImageUUID = "";
        public Camaras(String UUID = "")
        {
            InitializeComponent();

            GlobalUUID = UUID;
        }

        private void LoadCamaras()
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

        private void LoadList()
        {
            try 
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken);

                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalUUID + "/instances");

                String[] RecivedMatrix = response.Split('|');

                ListaDeCamaras.Rows.Clear();

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
                            String timestamp = RecivedMatrix[2];
                            String status = RecivedMatrix[3];

                            if (code == "200")
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(ListaDeCamaras);

                                row.Height = 40;

                                row.Cells[0].Value = uuid;
                                row.Cells[1].Value = timestamp;
                                row.Cells[2].Value = status;
                                ListaDeCamaras.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }

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
            LoadCamaras();
        }
        private void ListaDeCamaras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String UUID = "";

            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                try
                {
                    if (e.RowIndex > -1)
                        UUID = this.ListaDeCamaras.Rows[e.RowIndex].Cells[0].Value.ToString();

                    SubPanel = new Panel();
                    SubPanel.Dock = DockStyle.Fill;

                    this.Controls.Add(SubPanel);

                    SubPanel.Show();

                    this.MainPanel.Visible = false;

                    Datalog Datalog_Window = new Datalog(UUID);
                    Datalog_Window.TopLevel = false;
                    Datalog_Window.Dock = DockStyle.Fill;

                    SubPanel.Controls.Add(Datalog_Window);

                    Datalog_Window.Show();

                    Datalog_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                }
                catch { }
            }

            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                if (e.RowIndex > -1)
                    ImageUUID = this.ListaDeCamaras.Rows[e.RowIndex].Cells[0].Value.ToString();

                this.PreviewPanel.Visible = true;
                this.CentralPanel.Enabled = false;
            }

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
            
            

            if (this.PreviewPanel.Visible == true)
            {
                timer1.Interval = 1000;
                if (ImageUUID.Trim() != "")
                {
                    try
                    {
                        WebClient webClient = new WebClient();
                        webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());

                        byte[] data = webClient.DownloadData(ServerAPIURL + "/api/v2/media/photo/" + ImageUUID);

                        using (System.IO.MemoryStream mem = new System.IO.MemoryStream(data))
                        {
                            using (var imagen = Image.FromStream(mem))
                            {
                                bitmap = new Bitmap(imagen);
                            }
                        }
                    }
                    catch
                    {
                        bitmap = new Bitmap(160, 160);
                    }

                    this.Preview.Image = bitmap;
                }
            }
            else
            {
                timer1.Interval = 5000;
                if (IsLoaded)
                    LoadList();
                else
                    LoadCamaras();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClosePreviw_Click(object sender, EventArgs e)
        {
            this.PreviewPanel.Visible = false;
            this.CentralPanel.Enabled = true;
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
