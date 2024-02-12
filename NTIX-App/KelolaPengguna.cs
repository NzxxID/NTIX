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
    public partial class KelolaPengguna : Form
    {
        string id;
        FunctionClass f = new FunctionClass();
        public KelolaPengguna()
        {
            InitializeComponent();
        }
        void clear()
        {
            txt_NamaPengguna.Text = string.Empty;
            txt_KataSandi.Text = string.Empty;
            txt_Nama.Text = string.Empty;
            Cb_Role.Text = string.Empty;
            f.showData("select * from users", Dgv_KelolaPengguna);
        }

        private void KelolaPengguna_Load(object sender, EventArgs e)
        {
            f.showData("SELECT * FROM users", Dgv_KelolaPengguna);
            f.getDatarole(Cb_Role);
            Btn_Edit.Enabled = false;
            Btn_Hapus.Enabled = false;

            // Tambahkan ini untuk mengaitkan event dengan TextBox pencarian
            txt_Search.TextChanged += new EventHandler(txt_Search_TextChanged);
        }

        private void Lbl_KelolaPengguna_Click(object sender, EventArgs e)
        {

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
            if (txt_NamaPengguna.Text == string.Empty || txt_KataSandi.Text == string.Empty || txt_Nama.Text == string.Empty || Cb_Role.Text == string.Empty)
            {
                MessageBox.Show("Semua Kolom Harus Di Isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Menyimpan Pengguna?", "Hapus?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Admin Menambahkan Pengguna', NOW())");
                    string query = "INSERT INTO users ( username, password, nama, role, created_at) VALUES ( '" + txt_NamaPengguna.Text + "', '" + txt_KataSandi.Text + "', '" + txt_Nama.Text + "', '" + Cb_Role.Text + "', NOW())";
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

            if (txt_NamaPengguna.Text == string.Empty || txt_KataSandi.Text == string.Empty || txt_Nama.Text == string.Empty || Cb_Role.Text == string.Empty)
            {
                MessageBox.Show("semua kolom harus di isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Mengedit Pengguna?", "Hapus?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Admin Mengedit Pengguna', NOW())");
                    f.command("update users set username = '" + txt_NamaPengguna.Text + "', password = '" + txt_KataSandi.Text + "', nama = '" + txt_Nama.Text + "', role = '" + Cb_Role.Text + "', updated_at = NOW() where id = '" + id + "' ");
                    clear();
                }
                else if (result == DialogResult.No)
                {

                }
                
            }
        }

        private void Btn_Hapus_Click(object sender, EventArgs e)
        {
            if (txt_NamaPengguna.Text == string.Empty || txt_KataSandi.Text == string.Empty || txt_Nama.Text == string.Empty || Cb_Role.Text == string.Empty)
            {
                MessageBox.Show("semua kolom harus di isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda Yakin Akan Menghapus Pengguna?", "Hapus?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Admin Menghapus Pengguna', NOW())");
                    f.command("delete from users where username = '" + txt_NamaPengguna.Text + "'");
                    clear();
                }
                else if (result == DialogResult.No)
                {

                }
            }
        }

        private void Dgv_KelolaPengguna_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Btn_Simpan.Enabled = false;
            Btn_Edit.Enabled = true;
            Btn_Hapus.Enabled = true;

            DataGridViewRow dr = this.Dgv_KelolaPengguna.Rows[e.RowIndex];

            txt_NamaPengguna.Text = dr.Cells[1].Value.ToString();
            txt_KataSandi.Text = dr.Cells[2].Value.ToString();
            txt_Nama.Text = dr.Cells[3].Value.ToString();
            Cb_Role.Text = dr.Cells[4].Value.ToString();
            id = dr.Cells[0].Value.ToString();
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
                f.showData("SELECT * FROM users", Dgv_KelolaPengguna);
            }
            else
            {
                string query = $"SELECT * FROM users WHERE nama LIKE '%{searchText}%' OR username LIKE '%{searchText}%' OR role LIKE '%{searchText}%'";
                f.showData(query, Dgv_KelolaPengguna);
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
