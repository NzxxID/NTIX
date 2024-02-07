﻿using iTextSharp.text.pdf;
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
    public partial class CheckDataProduk : Form
    {
        FunctionClass f = new FunctionClass();
        public CheckDataProduk()
        {
            InitializeComponent();
        }

        private void Dgv_DataProduk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.Dgv_DataProduk.Rows[e.RowIndex];
        }

        private void CheckDataProduk_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            f.showData("Select * from produk", Dgv_DataProduk);
        }

        private void Btn_CheckDataTransaksi_Click(object sender, EventArgs e)
        {
            this.Hide();
            new HistoryTransaksi().Show();
        }

        private void Btn_CheckDataKegiatan_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BerandaOwner().Show();
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

        private void Btn_FilterProduk_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (DataTable dt = new DataTable("produk"))
                    {
                        // Query dasar
                        string baseQuery = "SELECT * FROM produk";

                        // Persiapkan parameter dan kondisi WHERE
                        List<MySqlParameter> parameters = new List<MySqlParameter>();
                        string whereCondition = "";

                        // Tambahkan kondisi tanggal jika dipilih
                        if (Dtp_DataProduk1.Value != DateTime.Now || Dtp_DataProduk2.Value != DateTime.Now)
                        {
                            whereCondition += " created_at BETWEEN @fromdate AND @todate";
                            parameters.Add(new MySqlParameter("@fromdate", Dtp_DataProduk1.Value));
                            parameters.Add(new MySqlParameter("@todate", Dtp_DataProduk2.Value.AddDays(1))); // Tambah 1 hari agar mencakup hingga akhir hari yang dipilih
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
                            Dgv_DataProduk.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_RisetDataProduk_Click(object sender, EventArgs e)
        {
            Dtp_DataProduk1.Value = DateTime.Now;
            Dtp_DataProduk2.Value = DateTime.Now;
            LoadData();
        }

        private void txt_SearchDataProduk_TextChanged(object sender, EventArgs e)
        {
            SearchDataProduk(txt_SearchDataProduk.Text);
        }
        private void SearchDataProduk(string searchText)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (DataTable dt = new DataTable("produk"))
                    {
                        string searchQuery = "SELECT * FROM produk WHERE nama_produk LIKE @searchText";

                        using (MySqlCommand cmd = new MySqlCommand(searchQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                            mySqlDataAdapter.Fill(dt);
                            Dgv_DataProduk.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_UnduhPdfDataProduk_Click(object sender, EventArgs e)
        {
            // Membuat instance dari class Document iTextSharp
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);


            // Membuat objek paragraph dengan judul
            Paragraph title = new Paragraph("Laporan Kegiatan", fontTitle);
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

                // Membuat table dengan jumlah kolom sesuai dengan jumlah kolom di dalam DataGridView
                PdfPTable table = new PdfPTable(Dgv_DataProduk.Columns.Count);

                // Menambahkan header ke dalam table
                for (int i = 0; i < Dgv_DataProduk.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(Dgv_DataProduk.Columns[i].HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = 5;
                    cell.BorderWidth = 1;
                    table.AddCell(cell);
                }

                // Menambahkan data dari DataGridView ke dalam table
                for (int i = 0; i < Dgv_DataProduk.Rows.Count; i++)
                {
                    for (int j = 0; j < Dgv_DataProduk.Columns.Count; j++)
                    {
                        if (Dgv_DataProduk[j, i].Value != null)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(Dgv_DataProduk[j, i].Value.ToString()));
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