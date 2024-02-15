using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTIX_App
{
    public partial class Transaksi : Form
    {
        string id;
        MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;database=ntix-db");
        FunctionClass f = new FunctionClass();
        public Transaksi()
        {
            InitializeComponent();
        }
        void clear()
        {

            Cb_NamaKonser.Text = string.Empty;
            txt_TotalPembayaran.Text = string.Empty;
            txt_Harga.Text = string.Empty;
            txt_UangBayar.Text = string.Empty;
            txt_Kuantitas.Text = string.Empty;
            txt_UangKembali.Text = string.Empty;
            txt_NamaPemesan.Text = string.Empty;
            txt_NoHp.Text = string.Empty;
            txt_SearchJenisMusik.Text = string.Empty;
            Cb_Kategori.Text = string.Empty;
            txt_NoUnik.Text = string.Empty;
        }

        private void Transaksi_Load(object sender, EventArgs e)
        {
           
           
            f.getDataNamaKonser(Cb_NamaKonser);
            txt_Kuantitas.TextChanged += UpdateTotalBelanjaOtomatis;
            txt_Harga.TextChanged += UpdateTotalBelanjaOtomatis;

            txt_TotalPembayaran.TextChanged += UpdateKembalianOtomatis;
            txt_UangBayar.TextChanged += UpdateKembalianOtomatis;

            Btn_Pesan.Click -= Btn_Pesan_Click;
            Btn_Pesan.Click += Btn_Pesan_Click;

            RefreshDataGridView();
        }
        private void RefreshDataGridView()
        {
            string query = "SELECT l.id, u.nama_produk, u.harga_produk, l.qty, l.nama_pelanggan, l.nomor_unik, l.kategori, l.no_hp, l.total_harga, l.uang_bayar, l.uang_kembalian, l.created_at " + "FROM transaksi l " + "JOIN produk u ON l.id_produk = u.id " + "ORDER BY l.id DESC";

            f.showData(query, Dgv_Transaksi);
        }
        private void ProcessTransaction()
        {
            // Mendapatkan nilai dari kontrol input
            string namaPelanggan = txt_NamaPemesan.Text;
            string namaProduk = Cb_NamaKonser.Text;
            string kategori = Cb_Kategori.Text; // Ambil nilai kategori dari ComboBox
            int qty = int.Parse(txt_Kuantitas.Text);
            decimal noHp = decimal.Parse(txt_NoHp.Text);
            decimal totalHarga = decimal.Parse(txt_TotalPembayaran.Text);
            decimal uangBayar = decimal.Parse(txt_UangBayar.Text);
            decimal uangKembali = decimal.Parse(txt_UangKembali.Text);
            int nomorUnik;

            // Mendapatkan nomor unik secara acak
            Random random = new Random();
            nomorUnik = random.Next(10000, 99999);

            try
            {
                // Membuka koneksi ke database
                conn.Open();

                // Pastikan semua input telah diisi
                if (string.IsNullOrEmpty(namaPelanggan) || string.IsNullOrEmpty(namaProduk) ||
                    qty <= 0 || noHp <= 0 || totalHarga <= 0 || uangBayar <= 0 || uangKembali < 0)
                {
                    MessageBox.Show("Harap lengkapi semua input.");
                    return;
                }

                // Mendapatkan ID produk berdasarkan nama produk
                int idProduk = GetProductId(namaProduk);
                if (idProduk == -1)
                {
                    MessageBox.Show("PRODUK TIDAK DITEMUKAN");
                    return;
                }

                // Cek stok produk
                if (!IsStockAvailable(idProduk, qty))
                {
                    MessageBox.Show("STOK PRODUK HABIS");
                    return;
                }

                // Menyimpan data transaksi ke tabel transaksi
                MySqlCommand insertTransaksiCommand = new MySqlCommand("INSERT INTO transaksi (id_produk, nama_pelanggan, qty, no_hp, total_harga, uang_bayar, uang_kembalian, nomor_unik, kategori, created_at) VALUES (@idProduk, @namaPelanggan, @qty, @nohp, @totalHarga, @uangBayar, @uangKembalian, @nomorUnik, @kategori, NOW())", conn);
                insertTransaksiCommand.Parameters.AddWithValue("@idProduk", idProduk);
                insertTransaksiCommand.Parameters.AddWithValue("@namaPelanggan", namaPelanggan);
                insertTransaksiCommand.Parameters.AddWithValue("@qty", qty);
                insertTransaksiCommand.Parameters.AddWithValue("@nohp", noHp);
                insertTransaksiCommand.Parameters.AddWithValue("@totalHarga", totalHarga);
                insertTransaksiCommand.Parameters.AddWithValue("@uangBayar", uangBayar);
                insertTransaksiCommand.Parameters.AddWithValue("@uangKembalian", uangKembali);
                insertTransaksiCommand.Parameters.AddWithValue("@nomorUnik", nomorUnik);
                insertTransaksiCommand.Parameters.AddWithValue("@kategori", kategori); // Tambahkan parameter kategori

                // Eksekusi perintah SQL
                insertTransaksiCommand.ExecuteNonQuery();

                // Update the quantity of the purchased product
                UpdateProductQuantity(idProduk, qty);

                MessageBox.Show("Transaksi berhasil");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Menutup koneksi setelah selesai
                conn.Close();
                clear(); // Clear the form after successful transaction

                // Memanggil kembali metode Transaksi_Load untuk merefresh form
                RefreshDataGridView();
            }
        }
        private bool IsStockAvailable(int productId, int quantity)
        {
            // Mendapatkan stok produk dari database
            MySqlCommand getStockCommand = new MySqlCommand("SELECT stok FROM produk WHERE id = @idProduk", conn);
            getStockCommand.Parameters.AddWithValue("@idProduk", productId);

            using (MySqlDataReader reader = getStockCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    int currentStock = reader.GetInt32(0);
                    return currentStock >= quantity; // Return true jika stok cukup, false jika tidak cukup
                }
                else
                {
                    return false; // Return false jika produk tidak ditemukan
                }
            }
        }
        private int GetProductId(string productName)
        {
            // Mendapatkan ID produk berdasarkan nama produk
            MySqlCommand getIdProdukCommand = new MySqlCommand("SELECT id FROM produk WHERE nama_produk = @namaProduk", conn);
            getIdProdukCommand.Parameters.AddWithValue("@namaProduk", productName);

            using (MySqlDataReader reader = getIdProdukCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
                else
                {
                    // Handle case when no matching record is found
                    return -1;
                }
            }
        }
        private void UpdateProductQuantity(int productId, int quantity)
        {
            // Update the quantity of the purchased product in the database
            MySqlCommand updateQuantityCommand = new MySqlCommand("UPDATE produk SET stok = stok - @quantity WHERE id = @idProduk", conn);
            updateQuantityCommand.Parameters.AddWithValue("@quantity", quantity);
            updateQuantityCommand.Parameters.AddWithValue("@idProduk", productId);
            updateQuantityCommand.ExecuteNonQuery();
        }

        private void UpdateKembalianOtomatis(object sender, EventArgs e)
        {
            // Memeriksa apakah nilai di TextBox Total dan Uang Bayar valid
            if (decimal.TryParse(txt_TotalPembayaran.Text, out decimal totalBelanja) && decimal.TryParse(txt_UangBayar.Text, out decimal uangBayar))
            {
                // Menghitung kembalian
                decimal kembalian = HitungKembalian(totalBelanja, uangBayar);

                // Menampilkan hasil ke TextBox Kembalian
                txt_UangKembali.Text = kembalian.ToString("N2"); // Menampilkan sebagai mata uang
            }
            else
            {
                // Jika nilai tidak valid, tampilkan pesan kesalahan di TextBox Kembalian
                txt_UangKembali.Text = "";
            }
        }
        private decimal HitungKembalian(decimal TotalBelanja, decimal UangBayar)
        {
            return UangBayar - TotalBelanja;
        }
        private void UpdateTotalBelanjaOtomatis(object sender, EventArgs e)
        {
            if (int.TryParse(txt_Kuantitas.Text, out int kuantitas) && decimal.TryParse(txt_Harga.Text, out decimal hargaSatuan))
            {
                decimal TotalBelanja = HitungTotalBelanja(kuantitas, hargaSatuan);

                txt_TotalPembayaran.Text = TotalBelanja.ToString("N2");
            }
        }
        private decimal HitungTotalBelanja(int kuantitas, decimal hargaSatuan)
        {
            decimal total = kuantitas * hargaSatuan;
            return total;
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

        private void Btn_Pesan_Click(object sender, EventArgs e)
        {
            f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Kasir Melakukan Transaksi', NOW())");
            ProcessTransaction();
            clear();
        }

        private void Btn_Batal_Click(object sender, EventArgs e)
        {
            Btn_Pesan.Enabled = true;
            Cb_NamaKonser.Enabled = true;
            Cb_Kategori.Enabled = true;
            txt_Kuantitas.Enabled = true;
            txt_NamaPemesan.Enabled = true;
            txt_NoHp.Enabled = true;
            txt_UangBayar.Enabled = true;

            DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Membatalkan?", "Batal?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                clear();
            }
            else if (result == DialogResult.No)
            {

            }
           
        }

        private void txt_SearchJenisMusik_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;database=ntix-db");

            conn.Open();
            if (txt_SearchJenisMusik.Text != "")
            {
                // Gunakan operator LIKE dalam query
                MySqlCommand cmd = new MySqlCommand("SELECT nama_produk FROM produk WHERE jenis_musik LIKE @jenis_musik", conn);

                // Gunakan '%' untuk memungkinkan pencocokan parsial
                cmd.Parameters.AddWithValue("@jenis_musik", "%" + txt_SearchJenisMusik.Text + "%");

                MySqlDataReader reader = cmd.ExecuteReader();

                // Hapus item yang ada di ComboBox
                Cb_NamaKonser.Items.Clear();

                while (reader.Read())
                {
                    // Tambahkan setiap hasil ke ComboBox
                    Cb_NamaKonser.Items.Add(reader.GetString(0));
                }

                // Set teks ComboBox jika hanya ada satu hasil
                if (Cb_NamaKonser.Items.Count == 1)
                {
                    Cb_NamaKonser.SelectedIndex = 0;
                }

                conn.Close();
            }
            else
            {
                // Bersihkan ComboBox dan TextBox dan segarkan data
                RefreshNamaKonser();
                Cb_NamaKonser.Text = ""; // Bersihkan item yang dipilih di ComboBox
                txt_NoUnik.Text = ""; // Bersihkan NoUnik
            }
        }
        private void RefreshNamaKonser()
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;database=ntix-db");
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT nama_produk FROM produk", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Clear existing items in the ComboBox
            Cb_NamaKonser.Items.Clear();

            while (reader.Read())
            {
                // Add each product to the ComboBox
                Cb_NamaKonser.Items.Add(reader.GetString(0));
            }

            conn.Close();
        }

        private void Cb_NamaKonser_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;database=ntix-db");

            conn.Open();
            if (Cb_NamaKonser.Text != "")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT harga_produk, harga_produkVIP FROM produk WHERE nama_produk = @nama_produk", conn);
                cmd.Parameters.AddWithValue("@nama_produk", Cb_NamaKonser.Text);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Clear existing items in the ComboBox kategori
                    Cb_Kategori.Items.Clear();
                    // Add the options "Reguler" and "VIP" to the ComboBox kategori
                    Cb_Kategori.Items.Add("Reguler");
                    Cb_Kategori.Items.Add("VIP");

                    // Set the ComboBox kategori text if there is only one result
                    if (Cb_Kategori.Items.Count == 1)
                    {
                        Cb_Kategori.SelectedIndex = 0;
                    }

                    int uniqueNumber = GenerateUniqueNumber();
                    txt_NoUnik.Text = uniqueNumber.ToString();
                }
                else
                {
                    // Handle the case where no matching record is found
                    // txt_Harga.Text = "Not Found";
                    txt_NoUnik.Text = ""; // Uncomment this line to clear NoUnik if no matching record found
                    Cb_Kategori.Items.Clear(); // Clear ComboBox kategori if no matching record found
                }

                conn.Close();
            }

        }

        private int GenerateUniqueNumber()
        {
            Random random = new Random();
            return random.Next(10000, 99999); // Contoh nomor unik antara 10000 dan 99999
        }

        private void Dgv_Transaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Btn_Pesan.Enabled = false;
            Cb_NamaKonser.Enabled = false;
            txt_NoUnik.Enabled = false;
            Cb_Kategori.Enabled = false;
            txt_Harga.Enabled = false;
            txt_Kuantitas.Enabled = false;
            txt_NamaPemesan.Enabled = false;
            txt_NoHp.Enabled = false;
            txt_TotalPembayaran.Enabled = false;
            txt_UangBayar.Enabled = false;
            txt_UangKembali.Enabled = false;

            DataGridViewRow dr = this.Dgv_Transaksi.Rows[e.RowIndex];
            Cb_NamaKonser.Text = dr.Cells[1].Value.ToString();
            txt_Harga.Text = decimal.Parse(dr.Cells[2].Value.ToString()).ToString("N2");
            txt_Kuantitas.Text = dr.Cells[3].Value.ToString();
            txt_NamaPemesan.Text = dr.Cells[4].Value.ToString();
            txt_NoUnik.Text = dr.Cells[5].Value.ToString();
            Cb_Kategori.Text = dr.Cells[6].Value.ToString();
            txt_NoHp.Text = dr.Cells[7].Value.ToString();
            txt_TotalPembayaran.Text = decimal.Parse(dr.Cells[8].Value.ToString()).ToString("N2");
            txt_UangBayar.Text = decimal.Parse(dr.Cells[9].Value.ToString()).ToString("N2");
            txt_UangKembali.Text = decimal.Parse(dr.Cells[10].Value.ToString()).ToString("N2");
            id = dr.Cells[0].Value.ToString();
        }

        private void Dgv_Produk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txt_UangBayar_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.disable(e, sender);
        }

        private void txt_Kuantitas_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.disable(e, sender);
        }

        private void txt_NoHp_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.disable(e, sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new DataP().Show();
        }

        private void txt_Harga_TextChanged(object sender, EventArgs e)
        {
            txt_Harga.Enabled = false;
        }

        private void txt_TotalPembayaran_TextChanged(object sender, EventArgs e)
        {
            txt_TotalPembayaran.Enabled = false;
        }

        private void txt_UangKembali_TextChanged(object sender, EventArgs e)
        {
            txt_UangKembali.Enabled = false;
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            Laporan frm = new Laporan();
            CrystalReport1 cr1 = new CrystalReport1();
            TextObject NomorUnik = (TextObject)cr1.ReportDefinition.Sections["Section2"].ReportObjects["NoUnik"];
            NomorUnik.Text = txt_NoUnik.Text;
            TextObject NamaPemesan = (TextObject)cr1.ReportDefinition.Sections["Section2"].ReportObjects["NamaPelanggan"];
            NamaPemesan.Text = txt_NamaPemesan.Text;
            TextObject NoHandphone = (TextObject)cr1.ReportDefinition.Sections["Section2"].ReportObjects["NoHandphone"];
            NoHandphone.Text = txt_NoHp.Text;
            TextObject NamaProduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["NamaProduk"];
            NamaProduk.Text = Cb_NamaKonser.Text;
            TextObject Kategori = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["Kategori"];
            Kategori.Text = Cb_Kategori.Text;
            TextObject HargaProduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["Harga"];
            HargaProduk.Text = txt_Harga.Text;
            TextObject Kuantitas = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["Kuantitas"];
            Kuantitas.Text = txt_Kuantitas.Text;
            TextObject Total = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["Total"];
            Total.Text = txt_TotalPembayaran.Text;
            TextObject TotalHarga = (TextObject)cr1.ReportDefinition.Sections["Section4"].ReportObjects["TotalHarga"];
            TotalHarga.Text = txt_TotalPembayaran.Text;
            TextObject UangBayar = (TextObject)cr1.ReportDefinition.Sections["Section4"].ReportObjects["UangBayar"];
            UangBayar.Text = txt_UangBayar.Text;
            TextObject UangKembali = (TextObject)cr1.ReportDefinition.Sections["Section4"].ReportObjects["UangKembali"];
            UangKembali.Text = txt_UangKembali.Text;
            TextObject tgl = (TextObject)cr1.ReportDefinition.Sections["Section2"].ReportObjects["Tanggal"];
            tgl.Text = DateTime.Now.ToString("yyyy-MM-dd");


            frm.crystalReportViewer1.ReportSource = cr1;
            frm.Show();
        }
        private string FormatCurrency(decimal value)
        {
            return string.Format("{0:N}", value);
        }

        private void txt_UangBayar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_TotalPembayaran.Text))
            {
                if (decimal.TryParse(txt_TotalPembayaran.Text, out decimal payment) && decimal.TryParse(txt_UangBayar.Text, out decimal cash))
                {
                    decimal returnAmount = cash - payment;

                    // Format nilai returnAmount sebagai mata uang Rupiah dan tampilkan pada returntxt
                    txt_UangKembali.Text = returnAmount.ToString("C0", new System.Globalization.CultureInfo("id-ID")).Replace("Rp", "").Trim();
                }
                else
                {
                    txt_UangKembali.Text = "";
                }
            }
            else
            {
                txt_UangKembali.Text = "";
            }

            // Format angka seperti mata uang Rupiah pada cashtxt
            if (decimal.TryParse(txt_UangBayar.Text, out decimal cashValue))
            {
                txt_UangBayar.Text = cashValue.ToString("N0"); // Format sebagai angka dengan titik ribuan
                txt_UangBayar.SelectionStart = txt_UangBayar.Text.Length;
            }
            else
            {
                txt_UangBayar.Text = "";
            }
            if (decimal.TryParse(txt_TotalPembayaran.Text, out decimal paymentValue))
            {
                txt_TotalPembayaran.Text = paymentValue.ToString("N0"); // Format sebagai angka dengan titik ribuan
                txt_TotalPembayaran.SelectionStart = txt_TotalPembayaran.Text.Length;
            }
            else
            {
                txt_TotalPembayaran.Text = "";
            }

        }

        private void Cb_Kategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;database=ntix-db");

            conn.Open();
            if (Cb_NamaKonser.Text != "")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT harga_produk, harga_produkVIP FROM produk WHERE nama_produk = @nama_produk", conn);
                cmd.Parameters.AddWithValue("@nama_produk", Cb_NamaKonser.Text);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    decimal harga = 0;
                    if (Cb_Kategori.Text == "Reguler")
                    {
                        harga = reader.GetDecimal(0);
                    }
                    else if (Cb_Kategori.Text == "VIP")
                    {
                        harga = reader.GetDecimal(1);
                    }
                    txt_Harga.Text = harga.ToString();
                }

                conn.Close();
            }

        }

        private void txt_NoUnik_TextChanged(object sender, EventArgs e)
        {
            txt_NoUnik.Enabled = false;
        }
    }
}
