using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;

namespace Horus {
    public partial class LocalView : Form {
        String IP = "";
        Boolean Flanco = true;
        static public Boolean Ready = false;
        static public Bitmap bitmap;
        static public String Valor;

        public LocalView(String IPGlobal) {
            InitializeComponent();

            IP = IPGlobal;
        }

        private readonly Random _random = new Random();

        static void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            try {
                byte[] data = e.Result;

                using (System.IO.MemoryStream mem = new System.IO.MemoryStream(data)) {
                    using (var imagen = Image.FromStream(mem)) {
                        bitmap = new Bitmap(imagen);
                    }
                }
            }
            catch { }

            Ready = false;
        }

        static void DownloadStringComplete(Object sender, DownloadStringCompletedEventArgs e) 
        {
            try {
                Valor = e.Result;
            }
            catch { }
        }

        private void GetImageFromDevice() 
        {
            if (Ready == false) {
                if (this.PreviewPanel.Visible == true) {
                    if (IP.Trim() != "") {
                        try {
                            this.Status.Text = "...";

                            String url = "http://" + IP + "/image";
                            WebClient client = new WebClient();
                            client.DownloadDataCompleted += DownloadDataCompleted;
                            client.DownloadDataAsync(new Uri(url));

                            url = "http://" + IP + "/data";
                            WebClient webClient = new WebClient();
                            webClient.DownloadStringCompleted += DownloadStringComplete;
                            webClient.DownloadStringAsync(new Uri(url));

                            Ready = true;

                            if (bitmap != null)
                                this.PreviewPanel.Image = bitmap;

                            if (Valor != "") {
                                Boolean Acceso = false;
                                Boolean Detected = false;

                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                var deserializedResult = serializer.Deserialize<dynamic>(Valor);

                                try { this.Detectado.Text = (String)deserializedResult["user"]; } catch { }
                                try { this.Temperatura.Text = System.Convert.ToString((float)deserializedResult["temp"]); } catch { }
                                try { Acceso = System.Convert.ToBoolean(deserializedResult["acceso"]); } catch { }
                                try { Detected = System.Convert.ToBoolean(deserializedResult["detected"]); } catch { }

                                if (Detected)
                                    this.Status.Text = "Identificando...";
                                else
                                    this.Status.Text = "...";

                                if (this.Detectado.Text.Trim() == "")
                                    Flanco = true;
                                else {
                                    if (Flanco && (this.Detectado.Text.Trim() != "" && this.Temperatura.Text != "0" && this.Temperatura.Text != "")) {

                                        if (Historial.Rows.Count >= 1000)
                                            borrarListaToolStripMenuItem_Click(null, null);

                                        Flanco = false;

                                        if (this.Detectado.Text.Trim() == "0")
                                            this.Detectado.Text = "---";

                                        Bitmap bitmap = new Bitmap(this.PreviewPanel.Image);

                                        DataGridViewRow row = new DataGridViewRow();
                                        row.CreateCells(Historial);

                                        row.MinimumHeight = 100;

                                        DateTime IndexTime = DateTime.Now;

                                        row.Cells[0].Value = IndexTime.ToString("yyyy-MM-dd hh:mm:ss");
                                        row.Cells[1].Value = this.Detectado.Text;
                                        row.Cells[2].Value = this.Temperatura.Text;
                                        row.Cells[3].Value = bitmap;

                                        if (!Acceso) {
                                            row.DefaultCellStyle.BackColor = Color.Red;
                                            row.DefaultCellStyle.ForeColor = Color.White;
                                            row.DefaultCellStyle.SelectionBackColor = Color.Brown;
                                            row.DefaultCellStyle.SelectionForeColor = Color.White;

                                            this.Status.Text = "Acceso denegado";
                                        }

                                        Historial.Rows.Add(row);

                                        Historial.Refresh();

                                        Historial.Rows[Historial.Rows.Count - 1].Selected = true;
                                        Historial.FirstDisplayedScrollingRowIndex = Historial.RowCount - 1;

                                        // AGREGO AL LOG LOCAL
                                        String ImageName = IndexTime.ToString("yyyyMMddhhmmss") + ".jpg";
                                        String FullPath = @"c:\CloudSign\log\images\" + ImageName;

                                        AddToLog(@"c:\CloudSign\log\loging.csv", row.Cells[0].Value + "|" + row.Cells[1].Value + "|" + row.Cells[2].Value + "|" + ImageName + "|" + Acceso);
                                        bitmap.Save(FullPath);
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            else {
                this.PreviewPanel.Image = new Bitmap(320,160);
            }
        }

        private void AddToLog(String path, String Data) {
            if (!File.Exists(path)) {
                using (StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine(Data);
                }
            }
            else {
                using (StreamWriter sw = File.AppendText(path)) {
                    sw.WriteLine(Data);
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e) {
            GetImageFromDevice();
        }

        private void LocalView_Load(object sender, EventArgs e) {
            this.ShowIP.Text = IP;

            String filename = @"c:\CloudSign\log\loging.csv";

            if (File.Exists(filename)) {
                String[] lines = File.ReadAllLines(filename);

                foreach (String line in lines) {

                    String[] Elements = line.Split('|');
                    String FullPath = @"c:\CloudSign\log\images\" + Elements[3];

                    Bitmap bitmap = new Bitmap(FullPath);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(Historial);

                    row.MinimumHeight = 100;

                    DateTime IndexTime = DateTime.Now;

                    row.Cells[0].Value = Elements[0];
                    row.Cells[1].Value = Elements[1];
                    row.Cells[2].Value = Elements[2];
                    row.Cells[3].Value = bitmap;

                    if (!System.Convert.ToBoolean(Elements[4])) {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.SelectionBackColor = Color.Brown;
                        row.DefaultCellStyle.SelectionForeColor = Color.White;

                        this.Status.Text = "Acceso denegado";
                    }

                    Historial.Rows.Add(row);

                    Historial.Refresh();

                    Historial.Rows[Historial.Rows.Count - 1].Selected = true;
                    Historial.FirstDisplayedScrollingRowIndex = Historial.RowCount - 1;

                }
            }
        }

        private void borrarListaToolStripMenuItem_Click(object sender, EventArgs e) {

            try {
                String OlfFilename = @"c:\CloudSign\log\loging.csv";
                String NewFilename = @"c:\CloudSign\log\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";

                if (File.Exists(OlfFilename) && !File.Exists(NewFilename)) {
                    File.Move(OlfFilename, NewFilename);

                    Historial.Rows.Clear();
                }
            }
            catch { }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void salvarHistorialToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveToCSV(this.Historial);
        }

        private void SaveToCSV(DataGridView DGV) {
            String filename = "";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Output.csv";

            if (sfd.ShowDialog() == DialogResult.OK) {
                MessageBox.Show("Exportando, le avisaremos cuando termine el proceso.");

                if (File.Exists(filename)) {
                    try {
                        File.Delete(filename);
                    }
                    catch (IOException ex) {
                        MessageBox.Show("No fue posible escibir en el disco." + ex.Message);
                    }
                }

                Int32 columnCount = DGV.ColumnCount;
                String columnNames = "";
                String[] output = new string[DGV.RowCount + 1];

                for (int i = 0; i < columnCount; i++) {
                    columnNames += DGV.Columns[i].Name.ToString() + ",";
                }

                output[0] += columnNames;

                for (int i = 1; (i - 1) < DGV.RowCount; i++) {
                    for (int j = 0; j < columnCount; j++) {
                        output[i] += DGV.Rows[i - 1].Cells[j].Value.ToString() + ",";
                    }
                }

                File.WriteAllLines(sfd.FileName, output, System.Text.Encoding.UTF8);

                MessageBox.Show("El archivo fue exportado con exito.");
            }
        }

        private void Historial_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }
    }
}
