namespace NTIX_App
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Pnl_Login = new System.Windows.Forms.Panel();
            this.Pb_Ilustrasi = new System.Windows.Forms.PictureBox();
            this.Lbl_Motivasi = new System.Windows.Forms.Label();
            this.Lbl_SelamatDatang = new System.Windows.Forms.Label();
            this.Lbl_NamaPengguna = new System.Windows.Forms.Label();
            this.txt_NamaPengguna = new System.Windows.Forms.TextBox();
            this.txt_KataSandi = new System.Windows.Forms.TextBox();
            this.Lbl_KataSandi = new System.Windows.Forms.Label();
            this.Btn_Masuk = new System.Windows.Forms.Button();
            this.Pnl_Login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_Ilustrasi)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Login
            // 
            this.Pnl_Login.BackColor = System.Drawing.Color.MidnightBlue;
            this.Pnl_Login.Controls.Add(this.Pb_Ilustrasi);
            this.Pnl_Login.Controls.Add(this.Lbl_Motivasi);
            this.Pnl_Login.Controls.Add(this.Lbl_SelamatDatang);
            this.Pnl_Login.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Login.Name = "Pnl_Login";
            this.Pnl_Login.Size = new System.Drawing.Size(519, 102);
            this.Pnl_Login.TabIndex = 0;
            // 
            // Pb_Ilustrasi
            // 
            this.Pb_Ilustrasi.Image = ((System.Drawing.Image)(resources.GetObject("Pb_Ilustrasi.Image")));
            this.Pb_Ilustrasi.Location = new System.Drawing.Point(410, 6);
            this.Pb_Ilustrasi.Name = "Pb_Ilustrasi";
            this.Pb_Ilustrasi.Size = new System.Drawing.Size(90, 90);
            this.Pb_Ilustrasi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pb_Ilustrasi.TabIndex = 2;
            this.Pb_Ilustrasi.TabStop = false;
            // 
            // Lbl_Motivasi
            // 
            this.Lbl_Motivasi.AutoSize = true;
            this.Lbl_Motivasi.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Motivasi.ForeColor = System.Drawing.Color.White;
            this.Lbl_Motivasi.Location = new System.Drawing.Point(15, 44);
            this.Lbl_Motivasi.Name = "Lbl_Motivasi";
            this.Lbl_Motivasi.Size = new System.Drawing.Size(356, 16);
            this.Lbl_Motivasi.TabIndex = 1;
            this.Lbl_Motivasi.Text = "Masuk Untuk Mendapat Pengalaman Yang Lenih Baik";
            // 
            // Lbl_SelamatDatang
            // 
            this.Lbl_SelamatDatang.AutoSize = true;
            this.Lbl_SelamatDatang.Font = new System.Drawing.Font("Mongolian Baiti", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_SelamatDatang.ForeColor = System.Drawing.Color.White;
            this.Lbl_SelamatDatang.Location = new System.Drawing.Point(13, 13);
            this.Lbl_SelamatDatang.Name = "Lbl_SelamatDatang";
            this.Lbl_SelamatDatang.Size = new System.Drawing.Size(187, 29);
            this.Lbl_SelamatDatang.TabIndex = 0;
            this.Lbl_SelamatDatang.Text = "Selamat Datang";
            // 
            // Lbl_NamaPengguna
            // 
            this.Lbl_NamaPengguna.AutoSize = true;
            this.Lbl_NamaPengguna.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_NamaPengguna.Location = new System.Drawing.Point(151, 130);
            this.Lbl_NamaPengguna.Name = "Lbl_NamaPengguna";
            this.Lbl_NamaPengguna.Size = new System.Drawing.Size(112, 16);
            this.Lbl_NamaPengguna.TabIndex = 1;
            this.Lbl_NamaPengguna.Text = "Nama Pengguna";
            // 
            // txt_NamaPengguna
            // 
            this.txt_NamaPengguna.Location = new System.Drawing.Point(151, 150);
            this.txt_NamaPengguna.Name = "txt_NamaPengguna";
            this.txt_NamaPengguna.Size = new System.Drawing.Size(213, 20);
            this.txt_NamaPengguna.TabIndex = 2;
            // 
            // txt_KataSandi
            // 
            this.txt_KataSandi.Location = new System.Drawing.Point(151, 196);
            this.txt_KataSandi.Name = "txt_KataSandi";
            this.txt_KataSandi.Size = new System.Drawing.Size(213, 20);
            this.txt_KataSandi.TabIndex = 4;
            this.txt_KataSandi.UseSystemPasswordChar = true;
            // 
            // Lbl_KataSandi
            // 
            this.Lbl_KataSandi.AutoSize = true;
            this.Lbl_KataSandi.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_KataSandi.Location = new System.Drawing.Point(151, 176);
            this.Lbl_KataSandi.Name = "Lbl_KataSandi";
            this.Lbl_KataSandi.Size = new System.Drawing.Size(77, 16);
            this.Lbl_KataSandi.TabIndex = 3;
            this.Lbl_KataSandi.Text = "Kata Sandi";
            // 
            // Btn_Masuk
            // 
            this.Btn_Masuk.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Masuk.Location = new System.Drawing.Point(218, 227);
            this.Btn_Masuk.Name = "Btn_Masuk";
            this.Btn_Masuk.Size = new System.Drawing.Size(75, 23);
            this.Btn_Masuk.TabIndex = 5;
            this.Btn_Masuk.Text = "Masuk";
            this.Btn_Masuk.UseVisualStyleBackColor = true;
            this.Btn_Masuk.Click += new System.EventHandler(this.Btn_Masuk_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 296);
            this.Controls.Add(this.Btn_Masuk);
            this.Controls.Add(this.txt_KataSandi);
            this.Controls.Add(this.Lbl_KataSandi);
            this.Controls.Add(this.txt_NamaPengguna);
            this.Controls.Add(this.Lbl_NamaPengguna);
            this.Controls.Add(this.Pnl_Login);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Pnl_Login.ResumeLayout(false);
            this.Pnl_Login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_Ilustrasi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Login;
        private System.Windows.Forms.Label Lbl_SelamatDatang;
        private System.Windows.Forms.PictureBox Pb_Ilustrasi;
        private System.Windows.Forms.Label Lbl_Motivasi;
        private System.Windows.Forms.Label Lbl_NamaPengguna;
        private System.Windows.Forms.TextBox txt_NamaPengguna;
        private System.Windows.Forms.TextBox txt_KataSandi;
        private System.Windows.Forms.Label Lbl_KataSandi;
        private System.Windows.Forms.Button Btn_Masuk;
    }
}

