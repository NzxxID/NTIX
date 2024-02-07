namespace NTIX_App
{
    partial class BerandaOwner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BerandaOwner));
            this.Pb_IlustrasiOwner = new System.Windows.Forms.PictureBox();
            this.Lbl_Owner = new System.Windows.Forms.Label();
            this.Pnl_BerandaOwner = new System.Windows.Forms.Panel();
            this.Btn_Keluar = new System.Windows.Forms.Button();
            this.Dgv_DataKegiatan = new System.Windows.Forms.DataGridView();
            this.Btn_UnduhPdfDataKegiatan = new System.Windows.Forms.Button();
            this.Pb_SearchDataKegiatan = new System.Windows.Forms.PictureBox();
            this.txt_SearchDataKegiatan = new System.Windows.Forms.TextBox();
            this.Lbl_DataKegiatan = new System.Windows.Forms.Label();
            this.Dtp_DataKegiatan1 = new System.Windows.Forms.DateTimePicker();
            this.Dtp_DataKegiatan2 = new System.Windows.Forms.DateTimePicker();
            this.Cb_Role = new System.Windows.Forms.ComboBox();
            this.Btn_RisetDataKegiatan = new System.Windows.Forms.Button();
            this.Btn_CheckDataTransaksi = new System.Windows.Forms.Button();
            this.Btn_CheckDataProduk = new System.Windows.Forms.Button();
            this.Lbl_To = new System.Windows.Forms.Label();
            this.Btn_FilterKegiatan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_IlustrasiOwner)).BeginInit();
            this.Pnl_BerandaOwner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DataKegiatan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_SearchDataKegiatan)).BeginInit();
            this.SuspendLayout();
            // 
            // Pb_IlustrasiOwner
            // 
            this.Pb_IlustrasiOwner.Image = ((System.Drawing.Image)(resources.GetObject("Pb_IlustrasiOwner.Image")));
            this.Pb_IlustrasiOwner.Location = new System.Drawing.Point(28, 44);
            this.Pb_IlustrasiOwner.Name = "Pb_IlustrasiOwner";
            this.Pb_IlustrasiOwner.Size = new System.Drawing.Size(60, 60);
            this.Pb_IlustrasiOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pb_IlustrasiOwner.TabIndex = 0;
            this.Pb_IlustrasiOwner.TabStop = false;
            // 
            // Lbl_Owner
            // 
            this.Lbl_Owner.AutoSize = true;
            this.Lbl_Owner.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Owner.ForeColor = System.Drawing.Color.White;
            this.Lbl_Owner.Location = new System.Drawing.Point(34, 20);
            this.Lbl_Owner.Name = "Lbl_Owner";
            this.Lbl_Owner.Size = new System.Drawing.Size(51, 16);
            this.Lbl_Owner.TabIndex = 0;
            this.Lbl_Owner.Text = "Owner";
            // 
            // Pnl_BerandaOwner
            // 
            this.Pnl_BerandaOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Pnl_BerandaOwner.BackColor = System.Drawing.Color.MidnightBlue;
            this.Pnl_BerandaOwner.Controls.Add(this.Btn_Keluar);
            this.Pnl_BerandaOwner.Controls.Add(this.Pb_IlustrasiOwner);
            this.Pnl_BerandaOwner.Controls.Add(this.Lbl_Owner);
            this.Pnl_BerandaOwner.Location = new System.Drawing.Point(0, 0);
            this.Pnl_BerandaOwner.Name = "Pnl_BerandaOwner";
            this.Pnl_BerandaOwner.Size = new System.Drawing.Size(121, 451);
            this.Pnl_BerandaOwner.TabIndex = 1;
            // 
            // Btn_Keluar
            // 
            this.Btn_Keluar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Keluar.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Keluar.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Btn_Keluar.Location = new System.Drawing.Point(23, 390);
            this.Btn_Keluar.Name = "Btn_Keluar";
            this.Btn_Keluar.Size = new System.Drawing.Size(75, 23);
            this.Btn_Keluar.TabIndex = 1;
            this.Btn_Keluar.Text = "Keluar";
            this.Btn_Keluar.UseVisualStyleBackColor = true;
            this.Btn_Keluar.Click += new System.EventHandler(this.Btn_Keluar_Click);
            // 
            // Dgv_DataKegiatan
            // 
            this.Dgv_DataKegiatan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_DataKegiatan.BackgroundColor = System.Drawing.Color.White;
            this.Dgv_DataKegiatan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_DataKegiatan.Location = new System.Drawing.Point(131, 61);
            this.Dgv_DataKegiatan.Name = "Dgv_DataKegiatan";
            this.Dgv_DataKegiatan.Size = new System.Drawing.Size(651, 294);
            this.Dgv_DataKegiatan.TabIndex = 17;
            this.Dgv_DataKegiatan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_DataKegiatan_CellClick);
            // 
            // Btn_UnduhPdfDataKegiatan
            // 
            this.Btn_UnduhPdfDataKegiatan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_UnduhPdfDataKegiatan.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_UnduhPdfDataKegiatan.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Btn_UnduhPdfDataKegiatan.Location = new System.Drawing.Point(588, 361);
            this.Btn_UnduhPdfDataKegiatan.Name = "Btn_UnduhPdfDataKegiatan";
            this.Btn_UnduhPdfDataKegiatan.Size = new System.Drawing.Size(194, 23);
            this.Btn_UnduhPdfDataKegiatan.TabIndex = 21;
            this.Btn_UnduhPdfDataKegiatan.Text = "Unduh Pdf";
            this.Btn_UnduhPdfDataKegiatan.UseVisualStyleBackColor = true;
            this.Btn_UnduhPdfDataKegiatan.Click += new System.EventHandler(this.Btn_UnduhPdfDataKegiatan_Click);
            // 
            // Pb_SearchDataKegiatan
            // 
            this.Pb_SearchDataKegiatan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pb_SearchDataKegiatan.Image = ((System.Drawing.Image)(resources.GetObject("Pb_SearchDataKegiatan.Image")));
            this.Pb_SearchDataKegiatan.Location = new System.Drawing.Point(762, 39);
            this.Pb_SearchDataKegiatan.Name = "Pb_SearchDataKegiatan";
            this.Pb_SearchDataKegiatan.Size = new System.Drawing.Size(12, 12);
            this.Pb_SearchDataKegiatan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pb_SearchDataKegiatan.TabIndex = 20;
            this.Pb_SearchDataKegiatan.TabStop = false;
            // 
            // txt_SearchDataKegiatan
            // 
            this.txt_SearchDataKegiatan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_SearchDataKegiatan.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txt_SearchDataKegiatan.Location = new System.Drawing.Point(624, 35);
            this.txt_SearchDataKegiatan.Name = "txt_SearchDataKegiatan";
            this.txt_SearchDataKegiatan.Size = new System.Drawing.Size(158, 20);
            this.txt_SearchDataKegiatan.TabIndex = 18;
            this.txt_SearchDataKegiatan.TextChanged += new System.EventHandler(this.txt_SearchDataKegiatan_TextChanged);
            // 
            // Lbl_DataKegiatan
            // 
            this.Lbl_DataKegiatan.AutoSize = true;
            this.Lbl_DataKegiatan.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_DataKegiatan.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Lbl_DataKegiatan.Location = new System.Drawing.Point(127, 9);
            this.Lbl_DataKegiatan.Name = "Lbl_DataKegiatan";
            this.Lbl_DataKegiatan.Size = new System.Drawing.Size(130, 23);
            this.Lbl_DataKegiatan.TabIndex = 19;
            this.Lbl_DataKegiatan.Text = "Data Kegiatan";
            // 
            // Dtp_DataKegiatan1
            // 
            this.Dtp_DataKegiatan1.Location = new System.Drawing.Point(131, 35);
            this.Dtp_DataKegiatan1.Name = "Dtp_DataKegiatan1";
            this.Dtp_DataKegiatan1.Size = new System.Drawing.Size(148, 20);
            this.Dtp_DataKegiatan1.TabIndex = 25;
            // 
            // Dtp_DataKegiatan2
            // 
            this.Dtp_DataKegiatan2.Location = new System.Drawing.Point(307, 35);
            this.Dtp_DataKegiatan2.Name = "Dtp_DataKegiatan2";
            this.Dtp_DataKegiatan2.Size = new System.Drawing.Size(148, 20);
            this.Dtp_DataKegiatan2.TabIndex = 27;
            // 
            // Cb_Role
            // 
            this.Cb_Role.FormattingEnabled = true;
            this.Cb_Role.Items.AddRange(new object[] {
            "admin",
            "kasir"});
            this.Cb_Role.Location = new System.Drawing.Point(461, 35);
            this.Cb_Role.Name = "Cb_Role";
            this.Cb_Role.Size = new System.Drawing.Size(76, 21);
            this.Cb_Role.TabIndex = 28;
            // 
            // Btn_RisetDataKegiatan
            // 
            this.Btn_RisetDataKegiatan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_RisetDataKegiatan.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_RisetDataKegiatan.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Btn_RisetDataKegiatan.Location = new System.Drawing.Point(588, 390);
            this.Btn_RisetDataKegiatan.Name = "Btn_RisetDataKegiatan";
            this.Btn_RisetDataKegiatan.Size = new System.Drawing.Size(194, 23);
            this.Btn_RisetDataKegiatan.TabIndex = 29;
            this.Btn_RisetDataKegiatan.Text = "Reset";
            this.Btn_RisetDataKegiatan.UseVisualStyleBackColor = true;
            this.Btn_RisetDataKegiatan.Click += new System.EventHandler(this.button1_Click);
            // 
            // Btn_CheckDataTransaksi
            // 
            this.Btn_CheckDataTransaksi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_CheckDataTransaksi.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CheckDataTransaksi.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Btn_CheckDataTransaksi.Location = new System.Drawing.Point(131, 361);
            this.Btn_CheckDataTransaksi.Name = "Btn_CheckDataTransaksi";
            this.Btn_CheckDataTransaksi.Size = new System.Drawing.Size(194, 23);
            this.Btn_CheckDataTransaksi.TabIndex = 2;
            this.Btn_CheckDataTransaksi.Text = " Check Data Transaksi";
            this.Btn_CheckDataTransaksi.UseVisualStyleBackColor = true;
            this.Btn_CheckDataTransaksi.Click += new System.EventHandler(this.Btn_CheckDataTransaksi_Click);
            // 
            // Btn_CheckDataProduk
            // 
            this.Btn_CheckDataProduk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_CheckDataProduk.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CheckDataProduk.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Btn_CheckDataProduk.Location = new System.Drawing.Point(131, 390);
            this.Btn_CheckDataProduk.Name = "Btn_CheckDataProduk";
            this.Btn_CheckDataProduk.Size = new System.Drawing.Size(194, 23);
            this.Btn_CheckDataProduk.TabIndex = 3;
            this.Btn_CheckDataProduk.Text = "Check Data Produk";
            this.Btn_CheckDataProduk.UseVisualStyleBackColor = true;
            this.Btn_CheckDataProduk.Click += new System.EventHandler(this.Btn_CheckDataProduk_Click);
            // 
            // Lbl_To
            // 
            this.Lbl_To.AutoSize = true;
            this.Lbl_To.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_To.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Lbl_To.Location = new System.Drawing.Point(285, 39);
            this.Lbl_To.Name = "Lbl_To";
            this.Lbl_To.Size = new System.Drawing.Size(18, 11);
            this.Lbl_To.TabIndex = 30;
            this.Lbl_To.Text = "To";
            // 
            // Btn_FilterKegiatan
            // 
            this.Btn_FilterKegiatan.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_FilterKegiatan.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Btn_FilterKegiatan.Location = new System.Drawing.Point(543, 35);
            this.Btn_FilterKegiatan.Name = "Btn_FilterKegiatan";
            this.Btn_FilterKegiatan.Size = new System.Drawing.Size(75, 20);
            this.Btn_FilterKegiatan.TabIndex = 39;
            this.Btn_FilterKegiatan.Text = "Filter";
            this.Btn_FilterKegiatan.UseVisualStyleBackColor = true;
            this.Btn_FilterKegiatan.Click += new System.EventHandler(this.Btn_FilterKegiatan_Click);
            // 
            // BerandaOwner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 451);
            this.Controls.Add(this.Btn_FilterKegiatan);
            this.Controls.Add(this.Lbl_To);
            this.Controls.Add(this.Btn_CheckDataProduk);
            this.Controls.Add(this.Btn_RisetDataKegiatan);
            this.Controls.Add(this.Btn_CheckDataTransaksi);
            this.Controls.Add(this.Cb_Role);
            this.Controls.Add(this.Dtp_DataKegiatan2);
            this.Controls.Add(this.Dtp_DataKegiatan1);
            this.Controls.Add(this.Dgv_DataKegiatan);
            this.Controls.Add(this.Btn_UnduhPdfDataKegiatan);
            this.Controls.Add(this.Pb_SearchDataKegiatan);
            this.Controls.Add(this.txt_SearchDataKegiatan);
            this.Controls.Add(this.Lbl_DataKegiatan);
            this.Controls.Add(this.Pnl_BerandaOwner);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BerandaOwner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BerandaOwner";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BerandaOwner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pb_IlustrasiOwner)).EndInit();
            this.Pnl_BerandaOwner.ResumeLayout(false);
            this.Pnl_BerandaOwner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DataKegiatan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_SearchDataKegiatan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Pb_IlustrasiOwner;
        private System.Windows.Forms.Label Lbl_Owner;
        private System.Windows.Forms.Panel Pnl_BerandaOwner;
        private System.Windows.Forms.Button Btn_Keluar;
        private System.Windows.Forms.DataGridView Dgv_DataKegiatan;
        private System.Windows.Forms.Button Btn_UnduhPdfDataKegiatan;
        private System.Windows.Forms.PictureBox Pb_SearchDataKegiatan;
        private System.Windows.Forms.TextBox txt_SearchDataKegiatan;
        private System.Windows.Forms.Label Lbl_DataKegiatan;
        private System.Windows.Forms.DateTimePicker Dtp_DataKegiatan1;
        private System.Windows.Forms.DateTimePicker Dtp_DataKegiatan2;
        private System.Windows.Forms.ComboBox Cb_Role;
        private System.Windows.Forms.Button Btn_RisetDataKegiatan;
        private System.Windows.Forms.Button Btn_CheckDataProduk;
        private System.Windows.Forms.Button Btn_CheckDataTransaksi;
        private System.Windows.Forms.Label Lbl_To;
        private System.Windows.Forms.Button Btn_FilterKegiatan;
    }
}