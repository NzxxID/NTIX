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
    public partial class BerandaAdmin : Form
    {
        FunctionClass f = new FunctionClass();
        private string initialFilter;
        public BerandaAdmin()
        {
            InitializeComponent();
            defaultQuery = "SELECT * From Users";
        }
        private string defaultQuery;
        private void LoadData(string query = null)
        {
            query = query ?? defaultQuery;
            f.showData(query, Dgv_DaftarPengguna);
        }

        private void BerandaAdmin_Load(object sender, EventArgs e)
        {
            initialFilter = defaultQuery;
            string query = "SELECT * From users";
            f.showData(query, Dgv_DaftarPengguna);
            f.showData("SELECT * FROM produk", Dgv_DaftarProduk);
        }

        private void Btn_TambahPengguna_Click(object sender, EventArgs e)
        {
            this.Hide();
            new KelolaPengguna().Show();
        }

        private void Btn_TambahProduk_Click(object sender, EventArgs e)
        {
            this.Hide();
            new KelolaProduk().Show();
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

        private void Dgv_DaftarPengguna_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.Dgv_DaftarPengguna.Rows[e.RowIndex];
        }

        private void Dgv_DaftarProduk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.Dgv_DaftarProduk.Rows[e.RowIndex];
        }

        private void Dgv_DaftarPengguna_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_SearchDaftarPengguna_TextChanged(object sender, EventArgs e)
        {
            string searchText = txt_SearchDaftarPengguna.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadData();
            }
            else
            {
                string query = $"{defaultQuery} WHERE nama LIKE '%{searchText}%'";
                LoadData(query);
            }
        }

        private void txt_SearchDaftarProduk_TextChanged(object sender, EventArgs e)
        {
            string searchText = txt_SearchDaftarProduk.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadDataProduk();
            }
            else
            {
                string query = $"SELECT * FROM produk WHERE nama_produk LIKE '%{searchText}%'";
                f.showData(query, Dgv_DaftarProduk);
            }

        }
        private void LoadDataProduk(string query = null)
        {
            query = query ?? "SELECT * FROM produk";
            f.showData(query, Dgv_DaftarProduk);
        }
    }
}
