using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NTIX_App
{
    public partial class Login : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=ntix-db");
        FunctionClass f = new FunctionClass();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        void login()
        {
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * From users WHERE username='" + txt_NamaPengguna.Text + "'AND password='" + txt_KataSandi.Text + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FunctionClass.role = dr["role"].ToString();
                        FunctionClass.id_user = dr["id"].ToString();

                       
                        MessageBox.Show("login sukses !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (FunctionClass.role == "admin")
                        {
                            f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Login', NOW())");
                            this.Hide();
                            new BerandaAdmin().Show();
                        }
                        else if (FunctionClass.role == "kasir")
                        {
                            f.command("insert into log (id_user, activity, created_at) VALUES ('" + FunctionClass.id_user + "', 'Login', NOW())");
                            this.Hide();
                            new Transaksi().Show();
                        }
                        else if (FunctionClass.role == "owner")
                        {
                            this.Hide();
                            new BerandaOwner().Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Akun Yang Anda Masukan Salah", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Btn_Masuk_Click(object sender, EventArgs e)
        {
            login();
        }
    }
}
