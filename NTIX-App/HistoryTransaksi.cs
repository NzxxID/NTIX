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
            FillCbNamaKonser();
        }
        private void LoadData()
        {
            string query = "SELECT l.id, l.id_produk, u.nama_produk, u.harga_produk, l.qty,l.nama_pelanggan, l.nomor_unik, l.kategori, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l " + "JOIN produk u ON l.id_produk = u.id";
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
                        string baseQuery = "SELECT l.id, l.id_produk, u.nama_produk, u.harga_produk, l.qty, l.nama_pelanggan, l.nomor_unik, l.kategori, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l " + "JOIN produk u ON l.id_produk = u.id";

                        // Persiapkan parameter dan kondisi WHERE
                        List<MySqlParameter> parameters = new List<MySqlParameter>();
                        string whereCondition = "";

                        // Tambahkan kondisi tanggal jika dipilih
                        if (Dtp_DataTransaksi1.Value != DateTime.Now || Dtp_DataTransaksi2.Value != DateTime.Now)
                        {
                            whereCondition += " DATE (l.created_at) BETWEEN @fromdate AND @todate";
                            parameters.Add(new MySqlParameter("@fromdate", Dtp_DataTransaksi1.Value.Date));
                            parameters.Add(new MySqlParameter("@todate", Dtp_DataTransaksi2.Value.Date.AddDays(0))); // Tambah 1 hari agar mencakup hingga akhir hari yang dipilih
                        }

                        // Tambahkan kondisi nama produk jika dipilih
                        if (Cb_NamaKonser.SelectedIndex != -1)
                        {
                            string selectedProductName = Cb_NamaKonser.SelectedItem.ToString();
                            whereCondition += (whereCondition == "" ? "" : " AND ") + "u.nama_produk = @nama_produk";
                            parameters.Add(new MySqlParameter("@nama_produk", selectedProductName));
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
            Cb_NamaKonser.SelectedIndex = -1;
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
                        string baseQuery = "SELECT l.id, l.id_produk, u.nama_produk, u.harga_produk, l.qty, l.nama_pelanggan, l.nomor_unik, l.kategori, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l " + "JOIN produk u ON l.id_produk = u.id";

                        // Persiapkan parameter dan kondisi WHERE
                        List<MySqlParameter> parameters = new List<MySqlParameter>();
                        string whereCondition = "";

                        // Tambahkan kondisi search
                        if (!string.IsNullOrEmpty(txt_SearchDataTransaksi.Text))
                        {
                            whereCondition += " (u.nama_produk LIKE @search OR " + "u.harga_produk LIKE @search OR " + "l.nama_pelanggan LIKE @search OR " + "l.nomor_unik LIKE @search OR " + "l.no_hp LIKE @search)";
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
                // Mengambil nilai total harga dari setiap baris transaksi
                string nilaiSel = Dgv_DataTransaksi.Rows[i].Cells["total_harga"].Value?.ToString();

                if (int.TryParse(nilaiSel, out int totalHarga))
                {
                    total += totalHarga;
                }
                else
                {
                    // Penanganan jika nilai tidak dapat diubah menjadi bilangan bulat
                    Console.WriteLine($"Nilai pada baris {i} tidak dapat diubah menjadi bilangan bulat: {nilaiSel}");
                }
            }

            // Menggunakan ToString("N0") untuk menambahkan tanda titik sebagai pemisah ribuan
            return total.ToString("N0");
        }

        private void Btn_UnduhPdfDataTransaksi_Click(object sender, EventArgs e)
        {
            // Membuat instance dari class Document iTextSharp
            Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 20f, 0f);

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12);

            // Membuat objek paragraph dengan judul
            Paragraph title = new Paragraph("Laporan Transaksi", fontTitle);
            title.Alignment = Element.ALIGN_CENTER; // Mengatur perataan teks
            title.SpacingAfter = 20;

            // Tambahkan tanggal saat diprint
            title.Add(new Paragraph($"Tanggal Cetak: {DateTime.Now.ToShortDateString()}", font));
            title.SpacingAfter = 20;

            // Menentukan lokasi penyimpanan file PDF
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF (.pdf)|.pdf";
            saveFileDialog1.FileName = "laporan transaksi.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Membuka file PDF
                    PdfWriter.GetInstance(doc, new FileStream(saveFileDialog1.FileName, FileMode.Create));

                    // Membuka dokumen
                    doc.Open();

                    // Query untuk mendapatkan data transaksi yang diurutkan berdasarkan ID terkecil ke terbesar
                    string baseQuery = "SELECT l.id, l.id_produk, u.nama_produk, u.harga_produk, l.qty, l.nama_pelanggan, l.nomor_unik, l.kategori, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l JOIN produk u ON l.id_produk = u.id";

                    // Persiapkan parameter dan kondisi WHERE
                    List<MySqlParameter> parameters = new List<MySqlParameter>();
                    string whereCondition = "";

                    // Tambahkan kondisi tanggal jika dipilih
                    if (Dtp_DataTransaksi1.Value != DateTime.Now || Dtp_DataTransaksi2.Value != DateTime.Now)
                    {
                        whereCondition += " DATE (l.created_at) BETWEEN @fromdate AND @todate";
                        parameters.Add(new MySqlParameter("@fromdate", Dtp_DataTransaksi1.Value.Date));
                        parameters.Add(new MySqlParameter("@todate", Dtp_DataTransaksi2.Value.Date.AddDays(0))); // Tambah 1 hari agar mencakup hingga akhir hari yang dipilih
                    }

                    // Tambahkan kondisi nama produk jika dipilih
                    if (Cb_NamaKonser.SelectedIndex != -1)
                    {
                        string selectedProductName = Cb_NamaKonser.SelectedItem.ToString();
                        whereCondition += (whereCondition == "" ? "" : " AND ") + "u.nama_produk = @nama_produk";
                        parameters.Add(new MySqlParameter("@nama_produk", selectedProductName));
                    }

                    // Gabungkan semua kondisi menjadi satu query
                    string fullQuery = baseQuery;
                    if (!string.IsNullOrEmpty(whereCondition))
                        fullQuery += " WHERE" + whereCondition;

                    // Eksekusi query
                    using (MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db"))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(fullQuery, conn))
                        {
                            foreach (MySqlParameter parameter in parameters)
                                cmd.Parameters.Add(parameter);

                            using (MySqlDataReader rdr = cmd.ExecuteReader())
                            {
                                PdfPTable table = new PdfPTable(rdr.FieldCount);

                                // Menambahkan header ke dalam table
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(rdr.GetName(i), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.BOLD)));
                                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell.Padding = 5;
                                    cell.BorderWidth = 1;
                                    table.AddCell(cell);
                                }

                                // Menambahkan data dari hasil query ke dalam table
                                while (rdr.Read())
                                {
                                    for (int i = 0; i < rdr.FieldCount; i++)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(rdr[i].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7)));
                                        cell.Padding = 5;
                                        cell.BorderWidth = 1;
                                        table.AddCell(cell);
                                    }
                                }

                                // Mengatur garis di sekitar tabel
                                table.DefaultCell.BorderWidth = 0;
                                table.DefaultCell.BorderColor = new iTextSharp.text.BaseColor(200, 200, 200);
                                table.DefaultCell.Padding = 10;
                                table.WidthPercentage = 100;

                                // Menambahkan paragraph ke dokumen
                                doc.Add(title);

                                // Menambahkan table ke dalam dokumen
                                doc.Add(table);

                                // Membuat objek Paragraph untuk menampung kalimat di bagian bawah tabel
                                Paragraph p = new Paragraph("Total Pemasukan : Rp. " + Harga(), font);
                                p.Alignment = Element.ALIGN_RIGHT;
                                p.SpacingBefore = 10;
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
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Cb_NamaKonser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void FillCbNamaKonser()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db"))
                {
                    conn.Open();
                    string query = "SELECT nama_produk FROM produk";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Cb_NamaKonser.Items.Add(rdr["nama_produk"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
