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
    public partial class DataP : Form
    {
        FunctionClass f = new FunctionClass();
        public DataP()
        {
            InitializeComponent();
        }

        private void DataP_Load(object sender, EventArgs e)
        {
            f.showData("select * from produk", Dgv_DataP);
        }

        private void Btn_Tutup_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
