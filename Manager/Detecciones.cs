using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Horus
{
    public partial class Detecciones : Form
    {
        String ServerAPIURL = "" + Program.Server;
        String GlobalUUID = "";
        String GlobaType = "";
        Panel SubPanel;

        public Detecciones(String UUID = "", String Type = "")
        {
            InitializeComponent();

            GlobalUUID = UUID;
            GlobaType = Type;
        }

        private void LoadDetections()
        {
            this.MainPanel.Visible = false;
            this.WaitAnimation.Visible = true;
            Application.DoEvents();

            try
            {
                if (GlobaType == "1")
                {
                    this.PERFILES.Visible = false;
                    this.ELIMINAR.Visible = false;
                    this.Alta.Visible = false;
                }

                System.Threading.Thread.Sleep(100);


                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken);

                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users/profiles=" + GlobalUUID + "/detections");

                String[] RecivedMatrix = response.Split('|');

                ListaDeDetecciones.Rows.Clear();

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
                            String name = RecivedMatrix[2];

                            if (code == "200")
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(ListaDeDetecciones);

                                row.Height = 40;

                                row.Cells[0].Value = name;
                                row.Cells[1].Value = uuid;
                                ListaDeDetecciones.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }

            this.WaitAnimation.Visible = false;
            this.MainPanel.Visible = true;
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
        private void Borrar(String UUID)
        {
            try
            {
                if (MessageBox.Show("Se dispone a borrar un usuario, al hacerlo eliminara tambien todos sus perfiles y detecciones, ¿quiere continuar?", "Atención!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    WebClient webClient = new WebClient();


                    webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                    String response = Encoding.ASCII.GetString(webClient.UploadValues(ServerAPIURL + "/api/v2/admin/accounts/users/profiles/detections=" + UUID, "DELETE", new System.Collections.Specialized.NameValueCollection()));

                    String[] RecivedMatrix = response.Split('|');

                    if (RecivedMatrix[0] != "200")
                        MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                    else
                    {
                        LoadDetections();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void SubPanel_unload(object sender, FormClosedEventArgs e) {
            this.MainPanel.Visible = true;
            SubPanel.Visible = false;
            SubPanel.Dispose();
            SubPanel = null;
            LoadDetections();
        }
        private void ListaDeDetecciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String UUID = "";
                String NAME = "";

                if (e.ColumnIndex == 3 && e.RowIndex > -1)
                {
                    try
                    {
                        if (e.RowIndex > -1)
                        {
                            NAME = this.ListaDeDetecciones.Rows[e.RowIndex].Cells[0].Value.ToString();
                            UUID = this.ListaDeDetecciones.Rows[e.RowIndex].Cells[1].Value.ToString();
                        }

                        Borrar(UUID);
                    }
                    catch { }
                }

                if (e.ColumnIndex == 2 && e.RowIndex > -1)
                {
                    try
                    {
                        if (e.RowIndex > -1)
                            UUID = this.ListaDeDetecciones.Rows[e.RowIndex].Cells[1].Value.ToString();

                        SubPanel = new Panel();
                        SubPanel.Dock = DockStyle.Fill;

                        this.Controls.Add(SubPanel);

                        SubPanel.Show();

                        this.MainPanel.Visible = false;

                        FaceUpload FaceUpload_Window = new FaceUpload(UUID);
                        FaceUpload_Window.TopLevel = false;
                        FaceUpload_Window.Dock = DockStyle.Fill;

                        SubPanel.Controls.Add(FaceUpload_Window);

                        FaceUpload_Window.Show();

                        FaceUpload_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                    }
                    catch { }
                }

                if (e.ColumnIndex == 4 && e.RowIndex > -1)
                {
                    try
                    {
                        if (e.RowIndex > -1)
                            UUID = this.ListaDeDetecciones.Rows[e.RowIndex].Cells[1].Value.ToString();

                        SubPanel = new Panel();
                        SubPanel.Dock = DockStyle.Fill;

                        this.Controls.Add(SubPanel);

                        SubPanel.Show();

                        this.MainPanel.Visible = false;

                        ModifyDetection ModifyDetection_Window = new ModifyDetection(UUID, GlobalUUID);
                        ModifyDetection_Window.TopLevel = false;
                        ModifyDetection_Window.Dock = DockStyle.Fill;

                        SubPanel.Controls.Add(ModifyDetection_Window);

                        ModifyDetection_Window.Show();

                        ModifyDetection_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
                    }
                    catch { }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button2_Click(object sender, EventArgs e) 
        {
            try {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                AddDetection AddDetection_Window = new AddDetection(GlobalUUID);
                AddDetection_Window.TopLevel = false;
                AddDetection_Window.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(AddDetection_Window);

                AddDetection_Window.Show();

                AddDetection_Window.FormClosed += new FormClosedEventHandler(SubPanel_unload);

            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button8_Click(object sender, EventArgs e) 
        {
            try {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                Configuracion configuracion_windows = new Configuracion(GlobalUUID);
                configuracion_windows.TopLevel = false;
                configuracion_windows.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(configuracion_windows);

                configuracion_windows.Show();

                configuracion_windows.FormClosed += new FormClosedEventHandler(SubPanel_unload);
            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button4_Click(object sender, EventArgs e) 
        {
            try {
                ModifyAccount ModifyAccount_window = new ModifyAccount();
                ModifyAccount_window.ShowDialog();
                ModifyAccount_window.Close();
            }
            catch (Exception Ex) {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Button3_Click(object sender, EventArgs e) {
            this.Close();
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
            timer1.Enabled = false;
            LoadDetections();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                SubPanel = new Panel();
                SubPanel.Dock = DockStyle.Fill;

                this.Controls.Add(SubPanel);

                SubPanel.Show();

                this.MainPanel.Visible = false;

                Camaras Camaras_window = new Camaras(GlobalUUID);
                Camaras_window.TopLevel = false;
                Camaras_window.Dock = DockStyle.Fill;

                SubPanel.Controls.Add(Camaras_window);

                Camaras_window.Show();

                Camaras_window.FormClosed += new FormClosedEventHandler(SubPanel_unload);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
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
