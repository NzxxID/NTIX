using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace NTIX_App
{
    public partial class BerandaOwner : Form
    {
        FunctionClass f = new FunctionClass();
        private string initialFilter;

        public BerandaOwner()
        {
            InitializeComponent();
            defaultQuery = "SELECT l.id, l.id_user, u.nama, u.role, l.activity, l.created_at FROM log l JOIN users u ON l.id_user = u.id";
        }
        private string defaultQuery;
        private void LoadData()
        {
           
        }

        private void LoadData(string query = null)
        {
            query = query ?? defaultQuery;
            f.showData(query, Dgv_DataKegiatan);
        }
        private void Cb_Role_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void BerandaOwner_Load(object sender, EventArgs e)
        {
            initialFilter = defaultQuery;
            string query = "SELECT l.id, l.id_user, u.nama, u.role, l.activity, l.created_at FROM log l JOIN users u ON l.id_user = u.id";
            f.showData(query, Dgv_DataKegiatan);
        }

        private void Btn_Keluar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Keluar?", "Keluar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Logout', NOW())");
                this.Hide();
                new Login().Show();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void Dgv_DataKegiatan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.Dgv_DataKegiatan.Rows[e.RowIndex];
        }

        private void Dgv_DataTransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void txt_SearchDataKegiatan_TextChanged(object sender, EventArgs e)
        {
            string searchText = txt_SearchDataKegiatan.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadData();
            }
            else
            {
                string query = $"{defaultQuery} WHERE u.nama LIKE '%{searchText}%'";
                LoadData(query);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cb_Role.SelectedIndex = -1;
            txt_SearchDataKegiatan.Text = "";
            Dtp_DataKegiatan1.Value = DateTime.Now;
            Dtp_DataKegiatan2.Value = DateTime.Now;

            // Menggunakan status filter awal
            LoadData(initialFilter);

        }

        private void Btn_CheckDataTransaksi_Click(object sender, EventArgs e)
        {
            this.Hide();
            new HistoryTransaksi().Show();
        }

        private void Btn_CheckDataProduk_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CheckDataProduk().Show();
        }

        private void Btn_FilterKegiatan_Click(object sender, EventArgs e)
        {
            try
            {
                // Ambil nilai dari ComboBox dan DateTimePicker
                string selectedRole = Cb_Role.SelectedItem?.ToString();
                DateTime fromDate = Dtp_DataKegiatan1.Value;
                DateTime toDate = Dtp_DataKegiatan2.Value;

                // Query dasar
                string baseQuery = "SELECT l.id, l.id_user, u.nama, u.role, l.activity, l.created_at " + "FROM log l " +"JOIN users u ON l.id_user = u.id";

                // Persiapkan parameter dan kondisi WHERE
                List<MySqlParameter> parameters = new List<MySqlParameter>();
                string whereCondition = "";

                // Tambahkan kondisi role jika dipilih
                if (!string.IsNullOrEmpty(selectedRole))
                {
                    whereCondition += " u.role = @role";
                    parameters.Add(new MySqlParameter("@role", selectedRole));
                }

                // Tambahkan kondisi tanggal jika dipilih
                if (fromDate != DateTime.Now && toDate != DateTime.Now)
                {
                    if (!string.IsNullOrEmpty(whereCondition))
                    whereCondition += " AND";
                    whereCondition += " DATE(l.created_at) BETWEEN @fromdate AND @todate";
                    parameters.Add(new MySqlParameter("@fromdate", fromDate.Date));
                    parameters.Add(new MySqlParameter("@todate", toDate.Date.AddDays(0))); // Tambah 1 hari agar mencakup hingga akhir hari yang dipilih
                }

                // Gabungkan semua kondisi menjadi satu query
                string fullQuery = baseQuery;
                if (!string.IsNullOrEmpty(whereCondition))
                    fullQuery += " WHERE" + whereCondition;

                // Eksekusi query
                using (MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db"))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (DataTable dt = new DataTable("log"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(fullQuery, conn))
                        {
                            foreach (MySqlParameter parameter in parameters)
                                cmd.Parameters.Add(parameter);

                            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                            mySqlDataAdapter.Fill(dt);
                            Dgv_DataKegiatan.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_UnduhPdfDataKegiatan_Click(object sender, EventArgs e)
        {
            // Membuat instance dari class Document iTextSharp
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);


            // Membuat objek paragraph dengan judul
            Paragraph title = new Paragraph("Laporan Kegiatan", fontTitle);
            Paragraph currentDate = new Paragraph("Tanggal: " + DateTime.Now.ToShortDateString(), font);
            currentDate.Alignment = Element.ALIGN_RIGHT; // Mengatur perataan teks ke kanan
            currentDate.SpacingAfter = 10; // Spasi setelah tanggal
            currentDate.SpacingBefore = 10; // Spasi sebelum tanggal]   
            title.Alignment = Element.ALIGN_CENTER; // Mengatur perataan teks
            title.SpacingAfter = 40;
            title.SpacingBefore = 40;

            // Menentukan lokasi penyimpanan file PDF
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF (.pdf)|.pdf";
            saveFileDialog1.FileName = "laporan Kegiatan.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Membuka file PDF
                PdfWriter.GetInstance(doc, new FileStream(saveFileDialog1.FileName, FileMode.Create));

                // Membuka dokumen
                doc.Open();

                //Menampilkan Tanggal Cetak Pdf
                doc.Add(currentDate);

                // Membuat table dengan jumlah kolom sesuai dengan jumlah kolom di dalam DataGridView
                PdfPTable table = new PdfPTable(Dgv_DataKegiatan.Columns.Count);

                // Menambahkan header ke dalam table
                for (int i = 0; i < Dgv_DataKegiatan.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(Dgv_DataKegiatan.Columns[i].HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = 5;
                    cell.BorderWidth = 1;
                    table.AddCell(cell);
                }

                // Menambahkan data dari DataGridView ke dalam table
                for (int i = 0; i < Dgv_DataKegiatan.Rows.Count; i++)
                {
                    for (int j = 0; j < Dgv_DataKegiatan.Columns.Count; j++)
                    {
                        if (Dgv_DataKegiatan[j, i].Value != null)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(Dgv_DataKegiatan[j, i].Value.ToString()));
                            cell.Padding = 5;
                            cell.BorderWidth = 1;
                            table.AddCell(cell);
                        }
                    }
                }

                // Mengatur garis di sekitar tabel
                table.DefaultCell.BorderWidth = 0;
                table.DefaultCell.BorderColor = new iTextSharp.text.BaseColor(200, 200, 200);
                table.DefaultCell.Padding = 7;
                table.WidthPercentage = 100;

                // Menambahkan paragraph ke dokumen
                doc.Add(title);

                // Menambahkan table ke dalam dokumen
                doc.Add(table);

                // Menutup dokumen dan writer
                doc.Close();
                MessageBox.Show("Data berhasil di-print ke dalam file PDF.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Membuka file PDF setelah disimpan
                Process.Start(saveFileDialog1.FileName);
            }
        }
    }
}
