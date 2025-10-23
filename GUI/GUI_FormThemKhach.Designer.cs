using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class GUI_FormThemKhach
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2TextBox txtMaKH;
        private Guna.UI2.WinForms.Guna2TextBox txtTenCongTy;
        private Guna.UI2.WinForms.Guna2TextBox txtMaSoThue;
        private Guna.UI2.WinForms.Guna2TextBox txtNguoiDaiDien;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtGhiChu;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtMaKH = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenCongTy = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaSoThue = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNguoiDaiDien = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtGhiChu = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // txtMaKH
            // 
            this.txtMaKH.BorderRadius = 10;
            this.txtMaKH.BorderThickness = 2;
            this.txtMaKH.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaKH.DefaultText = "";
            this.txtMaKH.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaKH.Location = new System.Drawing.Point(22, 22);
            this.txtMaKH.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.PlaceholderText = "Mã khách hàng";
            this.txtMaKH.SelectedText = "";
            this.txtMaKH.Size = new System.Drawing.Size(382, 42);
            this.txtMaKH.TabIndex = 0;
            // 
            // txtTenCongTy
            // 
            this.txtTenCongTy.BorderRadius = 10;
            this.txtTenCongTy.BorderThickness = 2;
            this.txtTenCongTy.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenCongTy.DefaultText = "";
            this.txtTenCongTy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenCongTy.Location = new System.Drawing.Point(468, 22);
            this.txtTenCongTy.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtTenCongTy.Name = "txtTenCongTy";
            this.txtTenCongTy.PlaceholderText = "Tên công ty";
            this.txtTenCongTy.SelectedText = "";
            this.txtTenCongTy.Size = new System.Drawing.Size(382, 42);
            this.txtTenCongTy.TabIndex = 1;
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.BorderRadius = 10;
            this.txtMaSoThue.BorderThickness = 2;
            this.txtMaSoThue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaSoThue.DefaultText = "";
            this.txtMaSoThue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaSoThue.Location = new System.Drawing.Point(22, 196);
            this.txtMaSoThue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.PlaceholderText = "Mã số thuế";
            this.txtMaSoThue.SelectedText = "";
            this.txtMaSoThue.Size = new System.Drawing.Size(382, 42);
            this.txtMaSoThue.TabIndex = 6;
            // 
            // txtNguoiDaiDien
            // 
            this.txtNguoiDaiDien.BorderRadius = 10;
            this.txtNguoiDaiDien.BorderThickness = 2;
            this.txtNguoiDaiDien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNguoiDaiDien.DefaultText = "";
            this.txtNguoiDaiDien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNguoiDaiDien.Location = new System.Drawing.Point(22, 80);
            this.txtNguoiDaiDien.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtNguoiDaiDien.Name = "txtNguoiDaiDien";
            this.txtNguoiDaiDien.PlaceholderText = "Người đại diện";
            this.txtNguoiDaiDien.SelectedText = "";
            this.txtNguoiDaiDien.Size = new System.Drawing.Size(382, 42);
            this.txtNguoiDaiDien.TabIndex = 2;
            // 
            // txtSDT
            // 
            this.txtSDT.BorderRadius = 10;
            this.txtSDT.BorderThickness = 2;
            this.txtSDT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSDT.DefaultText = "";
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSDT.Location = new System.Drawing.Point(468, 80);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.PlaceholderText = "Số điện thoại";
            this.txtSDT.SelectedText = "";
            this.txtSDT.Size = new System.Drawing.Size(382, 42);
            this.txtSDT.TabIndex = 3;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderRadius = 10;
            this.txtEmail.BorderThickness = 2;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(22, 138);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Email";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(382, 42);
            this.txtEmail.TabIndex = 4;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderRadius = 10;
            this.txtDiaChi.BorderThickness = 2;
            this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChi.DefaultText = "";
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new System.Drawing.Point(468, 138);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "Địa chỉ";
            this.txtDiaChi.SelectedText = "";
            this.txtDiaChi.Size = new System.Drawing.Size(382, 42);
            this.txtDiaChi.TabIndex = 5;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BorderRadius = 10;
            this.txtGhiChu.BorderThickness = 2;
            this.txtGhiChu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGhiChu.DefaultText = "";
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(22, 254);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.PlaceholderText = "Ghi chú";
            this.txtGhiChu.SelectedText = "";
            this.txtGhiChu.Size = new System.Drawing.Size(828, 150);
            this.txtGhiChu.TabIndex = 7;
            // 
            // btnLuu
            // 
            this.btnLuu.AutoRoundedCorners = true;
            this.btnLuu.BorderColor = System.Drawing.Color.White;
            this.btnLuu.BorderRadius = 19;
            this.btnLuu.BorderThickness = 2;
            this.btnLuu.FillColor = System.Drawing.Color.Green;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(299, 414);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 40);
            this.btnLuu.TabIndex = 8;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.AutoRoundedCorners = true;
            this.btnHuy.BorderColor = System.Drawing.Color.White;
            this.btnHuy.BorderRadius = 19;
            this.btnHuy.BorderThickness = 2;
            this.btnHuy.FillColor = System.Drawing.Color.Green;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(439, 414);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 40);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // GUI_FormThemKhach
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(858, 466);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.txtTenCongTy);
            this.Controls.Add(this.txtNguoiDaiDien);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.txtMaSoThue);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GUI_FormThemKhach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khách hàng";
            this.Load += new System.EventHandler(this.GUI_FormThemKhach_Load);
            this.ResumeLayout(false);

        }
    }
}
