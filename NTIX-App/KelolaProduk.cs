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
    public partial class KelolaProduk : Form
    {
        string id;
        FunctionClass f = new FunctionClass();
        DateTime tgl;
        string tgls;
        public KelolaProduk()
        {
            InitializeComponent();
        }
        void clear()
        {
            txt_NamaKonser.Text = string.Empty;
            txt_JenisMusik.Text = string.Empty;
            txt_Harga.Text = string.Empty;
            txt_HargaVIP.Text = string.Empty;
            txt_Stok.Text = string.Empty;
            txt_Lokasi.Text = string.Empty;
            Dtp_TanggalKonser.Text = string.Empty;
            f.showData("Select * from produk", Dgv_KelolaProduk);

        }

        private void KelolaProduk_Load(object sender, EventArgs e)
        {

            f.showData("SELECT * FROM produk", Dgv_KelolaProduk);
            Btn_Edit.Enabled = false;
            Btn_Hapus.Enabled = false;

            // Tambahkan ini untuk mengaitkan event dengan TextBox pencarian
            txt_Search.TextChanged += new EventHandler(txt_Search_TextChanged);
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

        private void Btn_Simpan_Click(object sender, EventArgs e)
        {
            if (txt_NamaKonser.Text == string.Empty || txt_JenisMusik.Text == string.Empty || txt_Harga.Text == string.Empty || txt_HargaVIP.Text == string.Empty || txt_Stok.Text == string.Empty || txt_Lokasi.Text == string.Empty)
            {
                MessageBox.Show("Semua kolom harus diisi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Menyimpan Produk?", "Hapus?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Admin Menambahkan Produk', NOW())");
                    string query = "INSERT INTO produk ( nama_produk, jenis_musik, harga_produk, harga_produkVIP, stok, lokasi, tanggal_event, created_at) VALUES ( '" + txt_NamaKonser.Text + "', '" + txt_JenisMusik.Text + "', '" + txt_Harga.Text + "', '" + txt_HargaVIP.Text + "', '" + txt_Stok.Text + "','" + txt_Lokasi.Text + "', '" + tgls + "', NOW())";
                    f.command(query);
                    clear();
                }
                else if (result == DialogResult.No)
                {

                }
               
            }
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            Btn_Simpan.Enabled = true;
            Btn_Edit.Enabled = false;
            Btn_Hapus.Enabled = false;
            
            if (txt_NamaKonser.Text == string.Empty || txt_JenisMusik.Text == string.Empty || txt_Harga.Text == string.Empty || txt_Stok.Text == string.Empty || txt_Lokasi.Text == string.Empty)
            {
                MessageBox.Show("semua kolom harus di isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Mengedit Produk?", "Hapus?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Admin Mengedit Produk', NOW())");
                    f.command("update produk set nama_produk = '" + txt_NamaKonser.Text + "', jenis_musik = '" + txt_JenisMusik.Text + "', harga_produkVIP = '" + txt_HargaVIP.Text + "', harga_produk = '" + txt_Harga.Text + "', stok = '" + txt_Stok.Text + "', lokasi = '" + txt_Lokasi.Text + "', updated_at = NOW() where id = '" + id + "' ");
                    clear();
                }
                else if (result == DialogResult.No)
                {

                }
               
            }
        }

        private void Btn_Hapus_Click(object sender, EventArgs e)
        {
            if (txt_NamaKonser.Text == string.Empty || txt_JenisMusik.Text == string.Empty || txt_Harga.Text == string.Empty || txt_Stok.Text == string.Empty || txt_Lokasi.Text == string.Empty)
            {
                MessageBox.Show("semua kolom harus di isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Menghapus Produk?", "Hapus?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                 f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Admin Menghapus Produk', NOW())");
                 f.command("delete from produk where nama_produk = '" + txt_NamaKonser.Text + "'");
                 clear();
                }
                else if (result == DialogResult.No)
                {

                }
               
            }
        }

        private void Dgv_KelolaProduk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           Btn_Simpan.Enabled = false;
           Btn_Edit.Enabled = true;
           Btn_Hapus.Enabled = true;

            DataGridViewRow dr = this.Dgv_KelolaProduk.Rows[e.RowIndex];

            txt_NamaKonser.Text = dr.Cells[1].Value.ToString();
            txt_JenisMusik.Text = dr.Cells[2].Value.ToString();
            txt_Harga.Text = dr.Cells[3].Value.ToString();
            txt_HargaVIP.Text = dr.Cells[4].Value.ToString();
            txt_Stok.Text = dr.Cells[5].Value.ToString();
            txt_Lokasi.Text = dr.Cells[6].Value.ToString();
            id = dr.Cells[0].Value.ToString();
        }

        private void Dtp_TanggalKonser_ValueChanged(object sender, EventArgs e)
        {
            tgl = Dtp_TanggalKonser.Value;
            tgls = tgl.ToString("yyyy-MM-dd");
        }

        private void txt_Harga_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.disable(e, sender);
        }

        private void txt_Stok_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.disable(e, sender);
        }

        private void Pb_Kembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BerandaAdmin().Show();
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string searchText = txt_Search.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                f.showData("SELECT * FROM produk", Dgv_KelolaProduk);
            }
            else
            {
                string query = $"SELECT * FROM produk WHERE nama_produk LIKE '%{searchText}%' OR jenis_musik LIKE '%{searchText}%' OR harga_produk LIKE '%{searchText}%' OR stok LIKE '%{searchText}%' OR lokasi LIKE '%{searchText}%' OR tanggal_event LIKE '%{searchText}%'";
                f.showData(query, Dgv_KelolaProduk);
            }
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            Btn_Edit.Enabled = false;
            Btn_Hapus.Enabled = false;
            Btn_Simpan.Enabled = true;
            clear();
        }
    }
}
