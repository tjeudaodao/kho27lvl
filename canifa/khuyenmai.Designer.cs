namespace canifa
{
    partial class khuyenmai
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(khuyenmai));
            this.txtbarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbmatong = new System.Windows.Forms.Label();
            this.lbgiacuoicung = new System.Windows.Forms.Label();
            this.lbphantramgiam = new System.Windows.Forms.Label();
            this.datag1 = new System.Windows.Forms.DataGridView();
            this.txtmatong = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbmotasanpham = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbdelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.datag1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbdelete)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbarcode
            // 
            this.txtbarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbarcode.ForeColor = System.Drawing.Color.DimGray;
            this.txtbarcode.Location = new System.Drawing.Point(30, 39);
            this.txtbarcode.Name = "txtbarcode";
            this.txtbarcode.Size = new System.Drawing.Size(457, 38);
            this.txtbarcode.TabIndex = 0;
            this.txtbarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtbarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbarcode_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scan Barcode";
            // 
            // lbmatong
            // 
            this.lbmatong.BackColor = System.Drawing.Color.Gainsboro;
            this.lbmatong.Font = new System.Drawing.Font("Comic Sans MS", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbmatong.ForeColor = System.Drawing.Color.Indigo;
            this.lbmatong.Location = new System.Drawing.Point(30, 90);
            this.lbmatong.Name = "lbmatong";
            this.lbmatong.Size = new System.Drawing.Size(457, 81);
            this.lbmatong.TabIndex = 1;
            this.lbmatong.Text = "Mã tổng";
            this.lbmatong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbgiacuoicung
            // 
            this.lbgiacuoicung.BackColor = System.Drawing.Color.Gainsboro;
            this.lbgiacuoicung.Font = new System.Drawing.Font("Comic Sans MS", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbgiacuoicung.ForeColor = System.Drawing.Color.Indigo;
            this.lbgiacuoicung.Location = new System.Drawing.Point(505, 39);
            this.lbgiacuoicung.Name = "lbgiacuoicung";
            this.lbgiacuoicung.Size = new System.Drawing.Size(835, 268);
            this.lbgiacuoicung.TabIndex = 1;
            this.lbgiacuoicung.Text = "Giá chốt";
            this.lbgiacuoicung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbphantramgiam
            // 
            this.lbphantramgiam.BackColor = System.Drawing.Color.Gainsboro;
            this.lbphantramgiam.Font = new System.Drawing.Font("Comic Sans MS", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbphantramgiam.ForeColor = System.Drawing.Color.Indigo;
            this.lbphantramgiam.Location = new System.Drawing.Point(505, 318);
            this.lbphantramgiam.Name = "lbphantramgiam";
            this.lbphantramgiam.Size = new System.Drawing.Size(835, 206);
            this.lbphantramgiam.TabIndex = 1;
            this.lbphantramgiam.Text = "% Giảm";
            this.lbphantramgiam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // datag1
            // 
            this.datag1.AllowUserToAddRows = false;
            this.datag1.AllowUserToDeleteRows = false;
            this.datag1.AllowUserToResizeColumns = false;
            this.datag1.AllowUserToResizeRows = false;
            this.datag1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datag1.BackgroundColor = System.Drawing.Color.LightGray;
            this.datag1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datag1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.datag1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datag1.DefaultCellStyle = dataGridViewCellStyle4;
            this.datag1.Location = new System.Drawing.Point(30, 283);
            this.datag1.Name = "datag1";
            this.datag1.RowHeadersVisible = false;
            this.datag1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.datag1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datag1.Size = new System.Drawing.Size(457, 352);
            this.datag1.TabIndex = 2;
            this.datag1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datag1_CellClick);
            // 
            // txtmatong
            // 
            this.txtmatong.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmatong.ForeColor = System.Drawing.Color.DimGray;
            this.txtmatong.Location = new System.Drawing.Point(30, 213);
            this.txtmatong.Name = "txtmatong";
            this.txtmatong.Size = new System.Drawing.Size(410, 38);
            this.txtmatong.TabIndex = 1;
            this.txtmatong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtmatong.TextChanged += new System.EventHandler(this.txtmatong_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Tìm kiếm theo mã tổng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(159, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Bảng tổng hợp giá mã hàng ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gray;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(854, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 24);
            this.label7.TabIndex = 1;
            this.label7.Text = "Giá cuối cùng";
            // 
            // lbmotasanpham
            // 
            this.lbmotasanpham.BackColor = System.Drawing.Color.Gainsboro;
            this.lbmotasanpham.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbmotasanpham.ForeColor = System.Drawing.Color.Gray;
            this.lbmotasanpham.Location = new System.Drawing.Point(506, 534);
            this.lbmotasanpham.Name = "lbmotasanpham";
            this.lbmotasanpham.Size = new System.Drawing.Size(834, 101);
            this.lbmotasanpham.TabIndex = 1;
            this.lbmotasanpham.Text = "Mô tả sản phẩm";
            this.lbmotasanpham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 215);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pbdelete
            // 
            this.pbdelete.Image = ((System.Drawing.Image)(resources.GetObject("pbdelete.Image")));
            this.pbdelete.Location = new System.Drawing.Point(442, 216);
            this.pbdelete.Name = "pbdelete";
            this.pbdelete.Size = new System.Drawing.Size(45, 34);
            this.pbdelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbdelete.TabIndex = 3;
            this.pbdelete.TabStop = false;
            this.pbdelete.Click += new System.EventHandler(this.pbdelete_Click);
            // 
            // khuyenmai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbdelete);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.datag1);
            this.Controls.Add(this.lbmotasanpham);
            this.Controls.Add(this.lbphantramgiam);
            this.Controls.Add(this.lbgiacuoicung);
            this.Controls.Add(this.lbmatong);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmatong);
            this.Controls.Add(this.txtbarcode);
            this.Name = "khuyenmai";
            this.Size = new System.Drawing.Size(1366, 645);
            this.Load += new System.EventHandler(this.khuyenmai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datag1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbdelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbmatong;
        private System.Windows.Forms.Label lbgiacuoicung;
        private System.Windows.Forms.Label lbphantramgiam;
        private System.Windows.Forms.DataGridView datag1;
        private System.Windows.Forms.TextBox txtmatong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbmotasanpham;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbdelete;
    }
}
