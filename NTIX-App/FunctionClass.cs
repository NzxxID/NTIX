using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTIX_App
{
    internal class FunctionClass
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=ntix-db");
        public static string role = null;
        public static string id_user = "";
        public static string username = "";
        private Form activateForm;

        // Public Void Untuk MengGet Data Role Di Database
        public void getDatarole(ComboBox cb)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select distinct role from users", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                cb.Items.Clear(); 

                while (reader.Read())
                {
                    string data = reader["role"].ToString();

                    
                    if (!cb.Items.Contains(data))
                    {
                        cb.Items.Add(data);
                    }
                }

                reader.Close();
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

        // Public Void Untuk MengGet Data Nama Produk Di Database
        public void getDataNamaKonser(ComboBox cb)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select distinct nama_produk from produk", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                cb.Items.Clear();

                while (reader.Read())
                {
                    string data = reader["nama_produk"].ToString();

                   
                    if (!cb.Items.Contains(data))
                    {
                        cb.Items.Add(data);
                    }
                }

                reader.Close();
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

        // Public Void Untuk Command Koneksi Database
        public void command(String query)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

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

        // Public Void Untuk Log Activity
        public void openChildForm(Form childForm, Panel panel, object btnSender)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }

            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel.Controls.Add(childForm);
            panel.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }
        public void showData(string query, DataGridView dg)
        {
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                dg.DataSource = dt;
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

        // Public Void Untuk Mendisable Huruf
        public void disable(KeyPressEventArgs e, object sender)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
