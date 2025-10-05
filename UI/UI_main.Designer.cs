using System;
using System.Linq;

namespace UI
{
    partial class UI_main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Guna.UI2.WinForms.Guna2GradientPanel sidebar;
        private Guna.UI2.WinForms.Guna2GradientPanel header;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel contentPanel;
        private Guna.UI2.WinForms.Guna2Panel mainPanel;

        private Guna.UI2.WinForms.Guna2Button btnTrangChu;
        private Guna.UI2.WinForms.Guna2Button btnKhachHang;
        private Guna.UI2.WinForms.Guna2Button btnHopDong;
        private Guna.UI2.WinForms.Guna2Button btnDonHang;
        private Guna.UI2.WinForms.Guna2Button btnThongKeTienDo;
        private Guna.UI2.WinForms.Guna2Button btnThongKeDonHang;
        private Guna.UI2.WinForms.Guna2Button btnQuanLyUsers;
        private Guna.UI2.WinForms.Guna2Button btnQuanLyUser;


        private void InitializeComponent()
        {
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.header = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnThongbao = new Guna.UI2.WinForms.Guna2Button();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.sidebar = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.btnQuanLyUser = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuanLyUsers = new Guna.UI2.WinForms.Guna2Button();
            this.btnThongKeDonHang = new Guna.UI2.WinForms.Guna2Button();
            this.btnThongKeTienDo = new Guna.UI2.WinForms.Guna2Button();
            this.btnDonHang = new Guna.UI2.WinForms.Guna2Button();
            this.btnHopDong = new Guna.UI2.WinForms.Guna2Button();
            this.btnKhachHang = new Guna.UI2.WinForms.Guna2Button();
            this.btnTrangChu = new Guna.UI2.WinForms.Guna2Button();
            this.mainPanel.SuspendLayout();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.sidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(30)))));
            this.mainPanel.Controls.Add(this.contentPanel);
            this.mainPanel.Controls.Add(this.header);
            this.mainPanel.Controls.Add(this.sidebar);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1280, 552);
            this.mainPanel.TabIndex = 0;
            // 
            // contentPanel
            // 
            this.contentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contentPanel.BackColor = System.Drawing.Color.Transparent;
            this.contentPanel.BackgroundImage = global::UI.Properties.Resources.background__dark_;
            this.contentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(70, 50);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1210, 502);
            this.contentPanel.TabIndex = 0;
            this.contentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.contentPanel_Paint);
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.Transparent;
            this.header.Controls.Add(this.guna2CircleButton1);
            this.header.Controls.Add(this.btnThongbao);
            this.header.Controls.Add(this.guna2CirclePictureBox1);
            this.header.Controls.Add(this.lblVersion);
            this.header.Controls.Add(this.lblUser);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(176)))), ((int)(((byte)(103)))));
            this.header.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(82)))), ((int)(((byte)(73)))));
            this.header.Location = new System.Drawing.Point(70, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(1210, 50);
            this.header.TabIndex = 1;
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.Image = global::UI.Properties.Resources.light_mode;
            this.guna2CircleButton1.Location = new System.Drawing.Point(103, 0);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(50, 50);
            this.guna2CircleButton1.TabIndex = 3;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // btnThongbao
            // 
            this.btnThongbao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThongbao.FillColor = System.Drawing.Color.Transparent;
            this.btnThongbao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThongbao.ForeColor = System.Drawing.Color.White;
            this.btnThongbao.Image = global::UI.Properties.Resources.thong_bao;
            this.btnThongbao.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThongbao.Location = new System.Drawing.Point(1157, 0);
            this.btnThongbao.Name = "btnThongbao";
            this.btnThongbao.Size = new System.Drawing.Size(50, 50);
            this.btnThongbao.TabIndex = 2;
            this.btnThongbao.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox1.Image = global::UI.Properties.Resources.Green_Sol;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(323, 8);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(30, 30);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2CirclePictureBox1.TabIndex = 0;
            this.guna2CirclePictureBox1.TabStop = false;
            this.guna2CirclePictureBox1.Click += new System.EventHandler(this.guna2CirclePictureBox1_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(6, 14);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(86, 23);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "ver 1.1.0.2";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(178, 14);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(139, 23);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "xin chào, <user>";
            this.lblUser.Click += new System.EventHandler(this.lblUser_Click);
            // 
            // sidebar
            // 
            this.sidebar.Controls.Add(this.btnQuanLyUser);
            this.sidebar.Controls.Add(this.btnQuanLyUsers);
            this.sidebar.Controls.Add(this.btnThongKeDonHang);
            this.sidebar.Controls.Add(this.btnThongKeTienDo);
            this.sidebar.Controls.Add(this.btnDonHang);
            this.sidebar.Controls.Add(this.btnHopDong);
            this.sidebar.Controls.Add(this.btnKhachHang);
            this.sidebar.Controls.Add(this.btnTrangChu);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(176)))), ((int)(((byte)(103)))));
            this.sidebar.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(82)))), ((int)(((byte)(73)))));
            this.sidebar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(70, 552);
            this.sidebar.TabIndex = 2;
            this.sidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.sidebar_Paint);
            // 
            // btnQuanLyUser
            // 
            this.btnQuanLyUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuanLyUser.BackColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUser.BorderColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUser.BorderRadius = 25;
            this.btnQuanLyUser.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnQuanLyUser.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnQuanLyUser.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnQuanLyUser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuanLyUser.ForeColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUser.HoverState.FillColor = System.Drawing.Color.White;
            this.btnQuanLyUser.Image = global::UI.Properties.Resources.quan_ly_user;
            this.btnQuanLyUser.ImageSize = new System.Drawing.Size(24, 24);
            this.btnQuanLyUser.Location = new System.Drawing.Point(10, 490);
            this.btnQuanLyUser.Name = "btnQuanLyUser";
            this.btnQuanLyUser.PressedColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUser.Size = new System.Drawing.Size(50, 50);
            this.btnQuanLyUser.TabIndex = 7;
            // 
            // btnQuanLyUsers
            // 
            this.btnQuanLyUsers.BackColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUsers.BorderColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUsers.BorderRadius = 25;
            this.btnQuanLyUsers.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnQuanLyUsers.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnQuanLyUsers.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnQuanLyUsers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuanLyUsers.ForeColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUsers.HoverState.FillColor = System.Drawing.Color.White;
            this.btnQuanLyUsers.Image = global::UI.Properties.Resources.quan_ly_users;
            this.btnQuanLyUsers.ImageSize = new System.Drawing.Size(24, 24);
            this.btnQuanLyUsers.Location = new System.Drawing.Point(10, 380);
            this.btnQuanLyUsers.Name = "btnQuanLyUsers";
            this.btnQuanLyUsers.PressedColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUsers.Size = new System.Drawing.Size(50, 50);
            this.btnQuanLyUsers.TabIndex = 6;
        
            this.btnQuanLyUsers.Click += new EventHandler(this.btnQuanLyUsers_Click);
            // 
            // btnThongKeDonHang
            // 
            this.btnThongKeDonHang.BackColor = System.Drawing.Color.Transparent;
            this.btnThongKeDonHang.BorderColor = System.Drawing.Color.Transparent;
            this.btnThongKeDonHang.BorderRadius = 25;
            this.btnThongKeDonHang.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnThongKeDonHang.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnThongKeDonHang.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnThongKeDonHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThongKeDonHang.ForeColor = System.Drawing.Color.Transparent;
            this.btnThongKeDonHang.HoverState.FillColor = System.Drawing.Color.White;
            this.btnThongKeDonHang.Image = global::UI.Properties.Resources.thong_ke_don_hang;
            this.btnThongKeDonHang.ImageSize = new System.Drawing.Size(24, 24);
            this.btnThongKeDonHang.Location = new System.Drawing.Point(10, 320);
            this.btnThongKeDonHang.Name = "btnThongKeDonHang";
            this.btnThongKeDonHang.PressedColor = System.Drawing.Color.Transparent;
            this.btnThongKeDonHang.Size = new System.Drawing.Size(50, 50);
            this.btnThongKeDonHang.TabIndex = 5;
            this.btnThongKeDonHang.Click += new System.EventHandler(this.btnThongKeDonHang_Click);
            // 
            // btnThongKeTienDo
            // 
            this.btnThongKeTienDo.BackColor = System.Drawing.Color.Transparent;
            this.btnThongKeTienDo.BorderColor = System.Drawing.Color.Transparent;
            this.btnThongKeTienDo.BorderRadius = 25;
            this.btnThongKeTienDo.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnThongKeTienDo.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnThongKeTienDo.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnThongKeTienDo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThongKeTienDo.ForeColor = System.Drawing.Color.Transparent;
            this.btnThongKeTienDo.HoverState.FillColor = System.Drawing.Color.White;
            this.btnThongKeTienDo.Image = global::UI.Properties.Resources.thong_ke_tien_do;
            this.btnThongKeTienDo.ImageSize = new System.Drawing.Size(24, 24);
            this.btnThongKeTienDo.Location = new System.Drawing.Point(10, 260);
            this.btnThongKeTienDo.Name = "btnThongKeTienDo";
            this.btnThongKeTienDo.PressedColor = System.Drawing.Color.Transparent;
            this.btnThongKeTienDo.Size = new System.Drawing.Size(50, 50);
            this.btnThongKeTienDo.TabIndex = 4;
            this.btnThongKeTienDo.Click += new System.EventHandler(this.btnThongKeTienDo_Click);
            // 
            // btnDonHang
            // 
            this.btnDonHang.BackColor = System.Drawing.Color.Transparent;
            this.btnDonHang.BorderColor = System.Drawing.Color.Transparent;
            this.btnDonHang.BorderRadius = 25;
            this.btnDonHang.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnDonHang.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnDonHang.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnDonHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDonHang.ForeColor = System.Drawing.Color.Transparent;
            this.btnDonHang.HoverState.FillColor = System.Drawing.Color.White;
            this.btnDonHang.Image = global::UI.Properties.Resources.don_hang;
            this.btnDonHang.ImageSize = new System.Drawing.Size(24, 24);
            this.btnDonHang.Location = new System.Drawing.Point(10, 200);
            this.btnDonHang.Name = "btnDonHang";
            this.btnDonHang.PressedColor = System.Drawing.Color.Transparent;
            this.btnDonHang.Size = new System.Drawing.Size(50, 50);
            this.btnDonHang.TabIndex = 3;
           this.btnDonHang.Click += new System.EventHandler(this.btnDonHang_Click);
            // 
            // btnHopDong
            // 
            this.btnHopDong.BackColor = System.Drawing.Color.Transparent;
            this.btnHopDong.BorderColor = System.Drawing.Color.Transparent;
            this.btnHopDong.BorderRadius = 25;
            this.btnHopDong.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnHopDong.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnHopDong.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnHopDong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHopDong.ForeColor = System.Drawing.Color.Transparent;
            this.btnHopDong.HoverState.FillColor = System.Drawing.Color.White;
            this.btnHopDong.Image = global::UI.Properties.Resources.hop_dong;
            this.btnHopDong.ImageSize = new System.Drawing.Size(24, 24);
            this.btnHopDong.Location = new System.Drawing.Point(10, 140);
            this.btnHopDong.Name = "btnHopDong";
            this.btnHopDong.PressedColor = System.Drawing.Color.Transparent;
            this.btnHopDong.Size = new System.Drawing.Size(50, 50);
            this.btnHopDong.TabIndex = 2;
            this.btnHopDong.Click += new System.EventHandler(this.btnHopDong_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.BackColor = System.Drawing.Color.Transparent;
            this.btnKhachHang.BorderColor = System.Drawing.Color.Transparent;
            this.btnKhachHang.BorderRadius = 25;
            this.btnKhachHang.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnKhachHang.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnKhachHang.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKhachHang.ForeColor = System.Drawing.Color.Transparent;
            this.btnKhachHang.HoverState.FillColor = System.Drawing.Color.White;
            this.btnKhachHang.Image = global::UI.Properties.Resources.khach_hang;
            this.btnKhachHang.ImageSize = new System.Drawing.Size(24, 24);
            this.btnKhachHang.Location = new System.Drawing.Point(10, 80);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.PressedColor = System.Drawing.Color.Transparent;
            this.btnKhachHang.Size = new System.Drawing.Size(50, 50);
            this.btnKhachHang.TabIndex = 1;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnTrangChu
            // 
            // btnTrangChu
            this.btnTrangChu.BackColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.BorderColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.BorderRadius = 25;

            // 👉 Bật chế độ RadioButton để chỉ có 1 nút được chọn cùng lúc
            this.btnTrangChu.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;

            // ⚙️ Màu mặc định (khi chưa chọn)
            this.btnTrangChu.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnTrangChu.ForeColor = System.Drawing.Color.Black;
            this.btnTrangChu.ImageSize = new System.Drawing.Size(24, 24);

            // 🌈 Khi rê chuột
            this.btnTrangChu.HoverState.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnTrangChu.HoverState.ForeColor = System.Drawing.Color.ForestGreen;

            // 🌟 Khi được chọn (CheckedState = active)
            this.btnTrangChu.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnTrangChu.CheckedState.ForeColor = System.Drawing.Color.ForestGreen;
            

            // ✨ Tuỳ chọn – thêm hiệu ứng bóng sáng cho nút được chọn
            this.btnTrangChu.ShadowDecoration.Enabled = true;
            this.btnTrangChu.ShadowDecoration.Color = System.Drawing.Color.LimeGreen;
            this.btnTrangChu.ShadowDecoration.Depth = 5;
            this.btnTrangChu.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;

            // 🖼️ Ảnh và vị trí
            this.btnTrangChu.Image = global::UI.Properties.Resources.trang_chu;
            this.btnTrangChu.ImageSize = new System.Drawing.Size(24, 24);
            this.btnTrangChu.Location = new System.Drawing.Point(10, 20);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.PressedColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.Size = new System.Drawing.Size(50, 50);
            this.btnTrangChu.TabIndex = 0;

            // 🎯 Gán sự kiện click
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);


            // 
            // UI_main
            // 
            this.ClientSize = new System.Drawing.Size(1280, 552);
            this.Controls.Add(this.mainPanel);
            this.Name = "UI_main";
            this.Text = "GreenSol";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.mainPanel.ResumeLayout(false);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.sidebar.ResumeLayout(false);
            this.ResumeLayout(false);
            foreach (var btn in this.sidebar.Controls.OfType<Guna.UI2.WinForms.Guna2Button>())
            {
                btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;

                // 🌈 Màu mặc định
                btn.FillColor = System.Drawing.Color.DarkSeaGreen;
                btn.ForeColor = System.Drawing.Color.Black;

                // 🌟 Khi hover
                btn.HoverState.FillColor = System.Drawing.Color.WhiteSmoke;
                btn.HoverState.ForeColor = System.Drawing.Color.ForestGreen;

                // ✅ Khi được chọn
                btn.CheckedState.FillColor = System.Drawing.Color.White;
                btn.CheckedState.ForeColor = System.Drawing.Color.ForestGreen;

                // ✨ Hiệu ứng bóng sáng khi chọn
                btn.ShadowDecoration.Enabled = true;
                btn.ShadowDecoration.Color = System.Drawing.Color.ForestGreen;
                btn.ShadowDecoration.Depth = 5;
                btn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            }

        }


        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnThongbao;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;

    }

}
