using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Horus
{
    public partial class FaceUpload : Form
    {
        String GlobalUUID = "";
        String ServerAPIURL = "" + Program.Server;
        Image<Bgr, Byte> currentFrame;
        Capture grabber;


        void Loop(object sender, EventArgs e)
        { 
            try
            {
                currentFrame = grabber.QueryFrame().ToImage<Bgr, Byte>(); ;
                this.Preview.Image = currentFrame.ToBitmap();

            }
            catch
            {

            }
        }

        void Shutdown()
        {
            try
            {
                Application.Idle -= new EventHandler(Loop);

                if (grabber != null)
                {
                    grabber.Dispose();
                    grabber = null;
                }
            }
            catch
            {

            }
        }
        void Setup()
        {
            Shutdown();

            try
            {
                grabber = new Capture(0);


                grabber.QueryFrame();
                Application.Idle += new EventHandler(Loop);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        public FaceUpload(String UUID = "")
        {
            InitializeComponent();

            GlobalUUID = UUID;
        }

        private async void CargarImagen()
        {
            try
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "jpg file (*.jpg)|*.jpg";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;

                        HttpClient httpClient = new HttpClient();
                        MultipartFormDataContent form = new MultipartFormDataContent();
                        HttpResponseMessage response;

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken);
                        byte[] imagebytearraystring = ImageFileToByteArray(filePath);
                        form.Add(new ByteArrayContent(imagebytearraystring, 0, imagebytearraystring.Length), "photo", GlobalUUID);

                        response = await httpClient.PostAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles/detections=" +  GlobalUUID + "/vectors", form);

                        response.EnsureSuccessStatusCode();
                        httpClient.Dispose();
                        String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                        if (RecivedMatrix[0] == "200")
                        {
                            ListVectorImages();
                        }
                        else
                        {
                            MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void ListVectorImages()
        {
            this.MainPanel.Visible = false;
            this.WaitAnimation.Visible = true;
            Application.DoEvents();

            try
            {
                this.ListaDeImagenes.Rows.Clear();

                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());

                String response = webClient.DownloadString(ServerAPIURL + "/api/v2/admin/accounts/users/profiles/detections=" + GlobalUUID + "/vectors");

                String[] RecivedMatrix = response.Split('|');

                if (RecivedMatrix[0] == "200")
                {
                    String[] values = response.Split('\n');

                    foreach (String value in values)
                    {
                        Application.DoEvents();

                        if (value != "")
                        {
                            RecivedMatrix = value.Split('|');

                            String uuid = RecivedMatrix[1];

                            Bitmap bitmap;

                            try
                            {
                                WebClient webClient1 = new WebClient();
                                webClient1.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());

                                byte[] data = webClient.DownloadData(ServerAPIURL + "/api/v2/admin/accounts/users/profiles/detections/vectors=" + uuid + "/photo");

                                using (MemoryStream mem = new MemoryStream(data))
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

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(this.ListaDeImagenes);

                            row.Height = 40;

                            row.Cells[0].Value = uuid;
                            row.Cells[1].Value = bitmap;
                            row.MinimumHeight = 150;

                            this.ListaDeImagenes.Rows.Add(row);
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

        private void Borrar(String UUID)
        {
            try
            {
                if (MessageBox.Show("Se dispone a borrar un vector de detección, al hacerlo esa imagen dejara ee estar disponible para identificacion, ¿quiere continuar?", "Atención!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    WebClient webClient = new WebClient();
                    webClient.Headers.Add("Authorization", "Bearer " + Program.LogInToken.Trim());
                    String response = Encoding.ASCII.GetString(webClient.UploadValues(ServerAPIURL + "/api/v2/admin/accounts/users/profiles/detections/vectors=" + UUID, "DELETE", new System.Collections.Specialized.NameValueCollection()));

                    String[] RecivedMatrix = response.Split('|');

                    if (RecivedMatrix[0] != "200")
                        MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                    else
                    {
                        ListVectorImages();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private byte[] ImageFileToByteArray(string fullFilePath)
        {
            FileStream fs = File.OpenRead(fullFilePath);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return bytes;
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListaDeConfiguraciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String UUID = "";

            if (e.ColumnIndex == 2 && e.RowIndex > -1)
            {
                try
                {
                    if (e.RowIndex > -1)
                    {
                        UUID = this.ListaDeImagenes.Rows[e.RowIndex].Cells[0].Value.ToString();
                    }

                    Borrar(UUID);
                }
                catch { }
            }
        }

        private void FaceUpload_Load(object sender, EventArgs e)
        {
            if (Program.ShowLogo)
                this.LogoProvider.Visible = true;
            else
                this.LogoProvider.Visible = false;

            Application.DoEvents();
        }

        private void FaceUpload_Resize(object sender, EventArgs e) {
            WaitAnimation.Top = (this.Height / 2) - (WaitAnimation.Height / 2);
            WaitAnimation.Left = (this.Width / 2) - (WaitAnimation.Width / 2);

            if (this.Width < 1000)
                this.Route.Visible = false;
            else
                this.Route.Visible = true;
        }
        private void Button3_Click(object sender, EventArgs e) {
            CargarImagen();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Enabled = false;
            ListVectorImages();
        }

        private async void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Preview.Image.Save("temporal.jpg", ImageFormat.Jpeg);

                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpResponseMessage response;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.LogInToken);
                byte[] imagebytearraystring = ImageFileToByteArray("temporal.jpg");
                form.Add(new ByteArrayContent(imagebytearraystring, 0, imagebytearraystring.Length), "photo", GlobalUUID);

                response = await httpClient.PostAsync(ServerAPIURL + "/api/v2/admin/accounts/users/profiles/detections=" + GlobalUUID + "/vectors", form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                String[] RecivedMatrix = response.Content.ReadAsStringAsync().Result.Split('|');

                if (RecivedMatrix[0] == "200")
                {
                    ListVectorImages();
                }
                else
                {
                    MessageBox.Show(RecivedMatrix[1], "Atención", MessageBoxButtons.OK);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK);
            }


            this.PreviewPanel.Visible = false;

            Shutdown();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.PreviewPanel.Visible = false;

            Shutdown();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Setup();
            this.PreviewPanel.Visible = true;
        }

        private void Ayuda_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://proyectohorus.com.ar/cloudsign/docs/Manual.pdf");
        }
    }
}
