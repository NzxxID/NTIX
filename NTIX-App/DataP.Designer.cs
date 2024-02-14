namespace NTIX_App
{
    partial class DataP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataP));
            this.Dgv_DataP = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Tutup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DataP)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_DataP
            // 
            this.Dgv_DataP.BackgroundColor = System.Drawing.Color.White;
            this.Dgv_DataP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_DataP.Location = new System.Drawing.Point(12, 65);
            this.Dgv_DataP.Name = "Dgv_DataP";
            this.Dgv_DataP.Size = new System.Drawing.Size(776, 291);
            this.Dgv_DataP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Daftar Produk";
            // 
            // Btn_Tutup
            // 
            this.Btn_Tutup.Font = new System.Drawing.Font("Mongolian Baiti", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Tutup.ForeColor = System.Drawing.Color.Black;
            this.Btn_Tutup.Location = new System.Drawing.Point(12, 362);
            this.Btn_Tutup.Name = "Btn_Tutup";
            this.Btn_Tutup.Size = new System.Drawing.Size(150, 37);
            this.Btn_Tutup.TabIndex = 2;
            this.Btn_Tutup.Text = "Tutup";
            this.Btn_Tutup.UseVisualStyleBackColor = true;
            this.Btn_Tutup.Click += new System.EventHandler(this.Btn_Tutup_Click);
            // 
            // DataP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 451);
            this.Controls.Add(this.Btn_Tutup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Dgv_DataP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(815, 490);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(815, 490);
            this.Name = "DataP";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataP";
            this.Load += new System.EventHandler(this.DataP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DataP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_DataP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Tutup;
    }
}