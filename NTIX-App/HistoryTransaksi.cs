using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using System;
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

namespace NTIX_App
{
    public partial class HistoryTransaksi : Form
    {
        FunctionClass f = new FunctionClass();
        public HistoryTransaksi()
        {
            InitializeComponent();
        }

        private void HistoryTransaksi_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            string query = "SELECT l.id, l.id_produk, u.nama_produk, u.harga_produk, l.qty,l.nama_pelanggan, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l " + "JOIN produk u ON l.id_produk = u.id";
            f.showData(query, Dgv_DataTransaksi);
        }

        private void Dgv_DataTransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.Dgv_DataTransaksi.Rows[e.RowIndex];
        }

        private void Btn_CheckDataKegiatan_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BerandaOwner().Show();
        }

        private void Btn_CheckDataProduk_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CheckDataProduk().Show();
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

        private void Btn_FilterTransaksi_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (DataTable dt = new DataTable("log"))
                    {
                        // Query dasar
                        string baseQuery = "SELECT l.id, l.id_produk, u.nama_produk, u.harga_produk, l.qty, l.nama_pelanggan, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l " + "JOIN produk u ON l.id_produk = u.id";

                        // Persiapkan parameter dan kondisi WHERE
                        List<MySqlParameter> parameters = new List<MySqlParameter>();
                        string whereCondition = "";

                        // Tambahkan kondisi tanggal jika dipilih
                        if (Dtp_DataTransaksi1.Value != DateTime.Now || Dtp_DataTransaksi2.Value != DateTime.Now)
                        {
                            whereCondition += " l.created_at BETWEEN @fromdate AND @todate";
                            parameters.Add(new MySqlParameter("@fromdate", Dtp_DataTransaksi1.Value));
                            parameters.Add(new MySqlParameter("@todate", Dtp_DataTransaksi2.Value.AddDays(1))); // Tambah 1 hari agar mencakup hingga akhir hari yang dipilih
                        }

                        // Gabungkan semua kondisi menjadi satu query
                        string fullQuery = baseQuery;
                        if (!string.IsNullOrEmpty(whereCondition))
                            fullQuery += " WHERE" + whereCondition;

                        // Eksekusi query
                        using (MySqlCommand cmd = new MySqlCommand(fullQuery, conn))
                        {
                            foreach (MySqlParameter parameter in parameters)
                                cmd.Parameters.Add(parameter);

                            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                            mySqlDataAdapter.Fill(dt);
                            Dgv_DataTransaksi.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_RisetDataTransaksi_Click(object sender, EventArgs e)
        {
            Dtp_DataTransaksi1.Value = DateTime.Now;
            Dtp_DataTransaksi2.Value = DateTime.Now;
            LoadData();
        }

        private void txt_SearchDataTransaksi_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void PerformSearch()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (DataTable dt = new DataTable("log"))
                    {
                        // Query dasar
                        string baseQuery = "SELECT l.id, l.id_produk, u.nama_produk, u.harga_produk, l.qty, l.nama_pelanggan, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l " +"JOIN produk u ON l.id_produk = u.id";

                        // Persiapkan parameter dan kondisi WHERE
                        List<MySqlParameter> parameters = new List<MySqlParameter>();
                        string whereCondition = "";

                        // Tambahkan kondisi search
                        if (!string.IsNullOrEmpty(txt_SearchDataTransaksi.Text))
                        {
                            whereCondition += " u.nama_produk LIKE @search";
                            parameters.Add(new MySqlParameter("@search", $"%{txt_SearchDataTransaksi.Text}%"));
                        }

                        // Gabungkan semua kondisi menjadi satu query
                        string fullQuery = baseQuery;
                        if (!string.IsNullOrEmpty(whereCondition))
                            fullQuery += " WHERE" + whereCondition;

                        // Eksekusi query
                        using (MySqlCommand cmd = new MySqlCommand(fullQuery, conn))
                        {
                            foreach (MySqlParameter parameter in parameters)
                                cmd.Parameters.Add(parameter);

                            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                            mySqlDataAdapter.Fill(dt);
                            Dgv_DataTransaksi.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public string Harga()
        {
            int total = 0;

            for (int i = 0; i < Dgv_DataTransaksi.Rows.Count; ++i)
            {
                string nilaiSel = Dgv_DataTransaksi.Rows[i].Cells[7].Value?.ToString(); // Mengambil nilai sel sebagai string

                if (int.TryParse(nilaiSel, out int hargaBaris))
                {
                    total += hargaBaris;
                }
                else
                {
                    // Penanganan jika nilai tidak dapat diubah menjadi bilangan bulat
                    Console.WriteLine($"Nilai pada baris {i} tidak dapat diubah menjadi bilangan bulat: {nilaiSel}");
                }
            }

            return total.ToString();
        }

        private void Btn_UnduhPdfDataTransaksi_Click(object sender, EventArgs e)
        {
            // Membuat instance dari class Document iTextSharp
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);


            // Membuat objek paragraph dengan judul
            Paragraph title = new Paragraph("Laporan Transaksi", fontTitle);
            title.Alignment = Element.ALIGN_CENTER; // Mengatur perataan teks
            title.SpacingAfter = 40;
            title.SpacingBefore = 40;

            // Membuat objek Paragraph untuk menampung kalimat di bagian bawah tabel
            Paragraph p = new Paragraph("Total Pemasukan : Rp. " + Harga(), font);
            p.SpacingBefore = 30;

            // Menentukan lokasi penyimpanan file PDF
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF (.pdf)|.pdf";
            saveFileDialog1.FileName = "laporan transaksi.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Membuka file PDF
                PdfWriter.GetInstance(doc, new FileStream(saveFileDialog1.FileName, FileMode.Create));

                // Membuka dokumen
                doc.Open();

                // Membuat table dengan jumlah kolom sesuai dengan jumlah kolom di dalam DataGridView
                PdfPTable table = new PdfPTable(Dgv_DataTransaksi.Columns.Count);

                // Menambahkan header ke dalam table
                for (int i = 0; i < Dgv_DataTransaksi.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(Dgv_DataTransaksi.Columns[i].HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = 5;
                    cell.BorderWidth = 1;
                    table.AddCell(cell);
                }

                // Menambahkan data dari DataGridView ke dalam table
                for (int i = 0; i < Dgv_DataTransaksi.Rows.Count; i++)
                {
                    for (int j = 0; j < Dgv_DataTransaksi.Columns.Count; j++)
                    {
                        if (Dgv_DataTransaksi[j, i].Value != null)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(Dgv_DataTransaksi[j, i].Value.ToString()));
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

                // Menambahkan kalimat ke dokumen
                doc.Add(p);

                // Menutup dokumen dan writer
                doc.Close();
                MessageBox.Show("Data berhasil di-print ke dalam file PDF.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Membuka file PDF setelah disimpan
                Process.Start(saveFileDialog1.FileName);
            }
        }
    }
}
