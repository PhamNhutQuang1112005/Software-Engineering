// GUI/GUI_FormThemLoaiChiTieu.Designer.cs
namespace GUI
{
    partial class GUI_FormThemLoaiChiTieu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTenChiTieu;
        private System.Windows.Forms.Label lblDonVi;
        private System.Windows.Forms.Label lblPhongBan;
        private System.Windows.Forms.Label lblGiaTriChuan;
        private System.Windows.Forms.TextBox txtTenChiTieu;
        private System.Windows.Forms.ComboBox cboDonVi;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.TextBox txtGiaTriChuan;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// true if managed resources should be disposed; otherwise, false.
        /// </param>
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_FormThemLoaiChiTieu));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTenChiTieu = new System.Windows.Forms.Label();
            this.lblDonVi = new System.Windows.Forms.Label();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.lblGiaTriChuan = new System.Windows.Forms.Label();
            this.txtTenChiTieu = new System.Windows.Forms.TextBox();
            this.cboDonVi = new System.Windows.Forms.ComboBox();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.txtGiaTriChuan = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(21, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(215, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm Loại chỉ tiêu (LCT)";
            // 
            // lblTenChiTieu
            // 
            this.lblTenChiTieu.AutoSize = true;
            this.lblTenChiTieu.Location = new System.Drawing.Point(23, 55);
            this.lblTenChiTieu.Name = "lblTenChiTieu";
            this.lblTenChiTieu.Size = new System.Drawing.Size(75, 16);
            this.lblTenChiTieu.TabIndex = 1;
            this.lblTenChiTieu.Text = "Tên chỉ tiêu";
            // 
            // lblDonVi
            // 
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.Location = new System.Drawing.Point(23, 91);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new System.Drawing.Size(44, 16);
            this.lblDonVi.TabIndex = 2;
            this.lblDonVi.Text = "Đơn vị";
            // 
            // lblPhongBan
            // 
            this.lblPhongBan.AutoSize = true;
            this.lblPhongBan.Location = new System.Drawing.Point(23, 126);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(72, 16);
            this.lblPhongBan.TabIndex = 3;
            this.lblPhongBan.Text = "Phòng ban";
            // 
            // lblGiaTriChuan
            // 
            this.lblGiaTriChuan.AutoSize = true;
            this.lblGiaTriChuan.Location = new System.Drawing.Point(23, 161);
            this.lblGiaTriChuan.Name = "lblGiaTriChuan";
            this.lblGiaTriChuan.Size = new System.Drawing.Size(80, 16);
            this.lblGiaTriChuan.TabIndex = 4;
            this.lblGiaTriChuan.Text = "Giá trị chuẩn";
            // 
            // txtTenChiTieu
            // 
            this.txtTenChiTieu.Location = new System.Drawing.Point(137, 52);
            this.txtTenChiTieu.Name = "txtTenChiTieu";
            this.txtTenChiTieu.Size = new System.Drawing.Size(308, 22);
            this.txtTenChiTieu.TabIndex = 5;
            // 
            // cboDonVi
            // 
            this.cboDonVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.Location = new System.Drawing.Point(137, 87);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(308, 24);
            this.cboDonVi.TabIndex = 6;
            // 
            // cboPhongBan
            // 
            this.cboPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhongBan.FormattingEnabled = true;
            this.cboPhongBan.Location = new System.Drawing.Point(137, 123);
            this.cboPhongBan.Name = "cboPhongBan";
            this.cboPhongBan.Size = new System.Drawing.Size(308, 24);
            this.cboPhongBan.TabIndex = 7;
            // 
            // txtGiaTriChuan
            // 
            this.txtGiaTriChuan.Location = new System.Drawing.Point(137, 158);
            this.txtGiaTriChuan.Name = "txtGiaTriChuan";
            this.txtGiaTriChuan.Size = new System.Drawing.Size(308, 22);
            this.txtGiaTriChuan.TabIndex = 8;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(256, 203);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(91, 30);
            this.btnLuu.TabIndex = 9;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.Location = new System.Drawing.Point(354, 203);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(91, 30);
            this.btnHuy.TabIndex = 10;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // GUI_FormThemLoaiChiTieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(473, 251);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtGiaTriChuan);
            this.Controls.Add(this.cboPhongBan);
            this.Controls.Add(this.cboDonVi);
            this.Controls.Add(this.txtTenChiTieu);
            this.Controls.Add(this.lblGiaTriChuan);
            this.Controls.Add(this.lblPhongBan);
            this.Controls.Add(this.lblDonVi);
            this.Controls.Add(this.lblTenChiTieu);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GUI_FormThemLoaiChiTieu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Loại chỉ tiêu";
            this.Load += new System.EventHandler(this.GUI_FormThemLoaiChiTieu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
