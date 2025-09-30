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
            this.guna2CirclePictureBox2 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.guna2Shapes1 = new Guna.UI2.WinForms.Guna2Shapes();
            this.display_name = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.phong_label = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.phong_display = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.hoten = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2ContextMenuStrip1 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.nhap_hoten_email = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.sdt = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.ngaysinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2TextBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.mkmoi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2TextBox4 = new Guna.UI2.WinForms.Guna2TextBox();
            this.mkcu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2TextBox5 = new Guna.UI2.WinForms.Guna2TextBox();
            this.email = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.comfirm_change = new Guna.UI2.WinForms.Guna2Button();
            this.decline_change = new Guna.UI2.WinForms.Guna2Button();
            this.mainPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.sidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).BeginInit();
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
            this.contentPanel.Controls.Add(this.decline_change);
            this.contentPanel.Controls.Add(this.comfirm_change);
            this.contentPanel.Controls.Add(this.guna2TextBox3);
            this.contentPanel.Controls.Add(this.mkmoi);
            this.contentPanel.Controls.Add(this.guna2TextBox4);
            this.contentPanel.Controls.Add(this.mkcu);
            this.contentPanel.Controls.Add(this.guna2TextBox5);
            this.contentPanel.Controls.Add(this.email);
            this.contentPanel.Controls.Add(this.guna2TextBox2);
            this.contentPanel.Controls.Add(this.ngaysinh);
            this.contentPanel.Controls.Add(this.guna2TextBox1);
            this.contentPanel.Controls.Add(this.sdt);
            this.contentPanel.Controls.Add(this.nhap_hoten_email);
            this.contentPanel.Controls.Add(this.hoten);
            this.contentPanel.Controls.Add(this.phong_display);
            this.contentPanel.Controls.Add(this.phong_label);
            this.contentPanel.Controls.Add(this.display_name);
            this.contentPanel.Controls.Add(this.guna2Shapes1);
            this.contentPanel.Controls.Add(this.guna2CirclePictureBox2);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(70, 50);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(100);
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
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.BackColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.BorderColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.BorderRadius = 25;
            this.btnTrangChu.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnTrangChu.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnTrangChu.FillColor = System.Drawing.Color.DarkSeaGreen;
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTrangChu.ForeColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.HoverState.FillColor = System.Drawing.Color.White;
            this.btnTrangChu.Image = global::UI.Properties.Resources.trang_chu;
            this.btnTrangChu.ImageSize = new System.Drawing.Size(24, 24);
            this.btnTrangChu.Location = new System.Drawing.Point(10, 20);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.PressedColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.Size = new System.Drawing.Size(50, 50);
            this.btnTrangChu.TabIndex = 0;
            // 
            // guna2CirclePictureBox2
            // 
            this.guna2CirclePictureBox2.ImageRotate = 0F;
            this.guna2CirclePictureBox2.Location = new System.Drawing.Point(542, 40);
            this.guna2CirclePictureBox2.Name = "guna2CirclePictureBox2";
            this.guna2CirclePictureBox2.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox2.Size = new System.Drawing.Size(110, 105);
            this.guna2CirclePictureBox2.TabIndex = 0;
            this.guna2CirclePictureBox2.TabStop = false;
            // 
            // guna2Shapes1
            // 
            this.guna2Shapes1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.guna2Shapes1.CausesValidation = false;
            this.guna2Shapes1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Shapes1.Location = new System.Drawing.Point(429, 164);
            this.guna2Shapes1.Name = "guna2Shapes1";
            this.guna2Shapes1.PolygonSides = 2;
            this.guna2Shapes1.PolygonSkip = 1;
            this.guna2Shapes1.Rotate = 0F;
            this.guna2Shapes1.Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle;
            this.guna2Shapes1.Size = new System.Drawing.Size(347, 100);
            this.guna2Shapes1.TabIndex = 2;
            this.guna2Shapes1.Text = "guna2Shapes1";
            this.guna2Shapes1.UseTransparentBackground = true;
            this.guna2Shapes1.Zoom = 80;
            // 
            // display_name
            // 
            this.display_name.BackColor = System.Drawing.Color.Transparent;
            this.display_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.display_name.ForeColor = System.Drawing.Color.White;
            this.display_name.Location = new System.Drawing.Point(523, 186);
            this.display_name.Name = "display_name";
            this.display_name.Size = new System.Drawing.Size(160, 22);
            this.display_name.TabIndex = 3;
            this.display_name.Text = "Nguyễn Văn Thành";
            // 
            // phong_label
            // 
            this.phong_label.BackColor = System.Drawing.Color.Transparent;
            this.phong_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phong_label.ForeColor = System.Drawing.Color.White;
            this.phong_label.Location = new System.Drawing.Point(485, 214);
            this.phong_label.Name = "phong_label";
            this.phong_label.Size = new System.Drawing.Size(148, 22);
            this.phong_label.TabIndex = 4;
            this.phong_label.Text = "Nhân viên phòng:";
            // 
            // phong_display
            // 
            this.phong_display.BackColor = System.Drawing.Color.Transparent;
            this.phong_display.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phong_display.ForeColor = System.Drawing.Color.White;
            this.phong_display.Location = new System.Drawing.Point(639, 214);
            this.phong_display.Name = "phong_display";
            this.phong_display.Size = new System.Drawing.Size(83, 22);
            this.phong_display.TabIndex = 5;
            this.phong_display.Text = "kinh doanh";
            // 
            // hoten
            // 
            this.hoten.BackColor = System.Drawing.Color.Transparent;
            this.hoten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hoten.ForeColor = System.Drawing.Color.White;
            this.hoten.Location = new System.Drawing.Point(148, 289);
            this.hoten.Name = "hoten";
            this.hoten.Size = new System.Drawing.Size(73, 27);
            this.hoten.TabIndex = 6;
            this.hoten.Text = "Họ tên:";
            // 
            // guna2ContextMenuStrip1
            // 
            this.guna2ContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            this.guna2ContextMenuStrip1.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip1.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // nhap_hoten_email
            // 
            this.nhap_hoten_email.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.nhap_hoten_email.BorderRadius = 10;
            this.nhap_hoten_email.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nhap_hoten_email.DefaultText = "";
            this.nhap_hoten_email.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.nhap_hoten_email.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.nhap_hoten_email.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nhap_hoten_email.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nhap_hoten_email.FillColor = System.Drawing.Color.Transparent;
            this.nhap_hoten_email.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nhap_hoten_email.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nhap_hoten_email.ForeColor = System.Drawing.Color.White;
            this.nhap_hoten_email.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nhap_hoten_email.Location = new System.Drawing.Point(300, 283);
            this.nhap_hoten_email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nhap_hoten_email.Name = "nhap_hoten_email";
            this.nhap_hoten_email.PlaceholderText = "";
            this.nhap_hoten_email.SelectedText = "";
            this.nhap_hoten_email.Size = new System.Drawing.Size(254, 37);
            this.nhap_hoten_email.TabIndex = 13;
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.guna2TextBox1.BorderRadius = 10;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(300, 330);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(254, 37);
            this.guna2TextBox1.TabIndex = 15;
            // 
            // sdt
            // 
            this.sdt.BackColor = System.Drawing.Color.Transparent;
            this.sdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sdt.ForeColor = System.Drawing.Color.White;
            this.sdt.Location = new System.Drawing.Point(148, 336);
            this.sdt.Name = "sdt";
            this.sdt.Size = new System.Drawing.Size(137, 27);
            this.sdt.TabIndex = 14;
            this.sdt.Text = "Số điện thoại:";
            // 
            // guna2TextBox2
            // 
            this.guna2TextBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.guna2TextBox2.BorderRadius = 10;
            this.guna2TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox2.DefaultText = "";
            this.guna2TextBox2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2TextBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox2.Location = new System.Drawing.Point(300, 377);
            this.guna2TextBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2TextBox2.Name = "guna2TextBox2";
            this.guna2TextBox2.PlaceholderText = "";
            this.guna2TextBox2.SelectedText = "";
            this.guna2TextBox2.Size = new System.Drawing.Size(254, 37);
            this.guna2TextBox2.TabIndex = 17;
            // 
            // ngaysinh
            // 
            this.ngaysinh.BackColor = System.Drawing.Color.Transparent;
            this.ngaysinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ngaysinh.ForeColor = System.Drawing.Color.White;
            this.ngaysinh.Location = new System.Drawing.Point(148, 383);
            this.ngaysinh.Name = "ngaysinh";
            this.ngaysinh.Size = new System.Drawing.Size(106, 27);
            this.ngaysinh.TabIndex = 16;
            this.ngaysinh.Text = "Ngày sinh:";
            // 
            // guna2TextBox3
            // 
            this.guna2TextBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.guna2TextBox3.BorderRadius = 10;
            this.guna2TextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox3.DefaultText = "";
            this.guna2TextBox3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2TextBox3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox3.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Location = new System.Drawing.Point(818, 377);
            this.guna2TextBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2TextBox3.Name = "guna2TextBox3";
            this.guna2TextBox3.PlaceholderText = "";
            this.guna2TextBox3.SelectedText = "";
            this.guna2TextBox3.Size = new System.Drawing.Size(254, 37);
            this.guna2TextBox3.TabIndex = 23;
            // 
            // mkmoi
            // 
            this.mkmoi.BackColor = System.Drawing.Color.Transparent;
            this.mkmoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mkmoi.ForeColor = System.Drawing.Color.White;
            this.mkmoi.Location = new System.Drawing.Point(666, 383);
            this.mkmoi.Name = "mkmoi";
            this.mkmoi.Size = new System.Drawing.Size(139, 27);
            this.mkmoi.TabIndex = 22;
            this.mkmoi.Text = "Mật khẩu mới:";
            // 
            // guna2TextBox4
            // 
            this.guna2TextBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.guna2TextBox4.BorderRadius = 10;
            this.guna2TextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox4.DefaultText = "";
            this.guna2TextBox4.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox4.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox4.FillColor = System.Drawing.Color.Transparent;
            this.guna2TextBox4.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox4.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox4.Location = new System.Drawing.Point(818, 330);
            this.guna2TextBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2TextBox4.Name = "guna2TextBox4";
            this.guna2TextBox4.PlaceholderText = "";
            this.guna2TextBox4.SelectedText = "";
            this.guna2TextBox4.Size = new System.Drawing.Size(254, 37);
            this.guna2TextBox4.TabIndex = 21;
            // 
            // mkcu
            // 
            this.mkcu.BackColor = System.Drawing.Color.Transparent;
            this.mkcu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mkcu.ForeColor = System.Drawing.Color.White;
            this.mkcu.Location = new System.Drawing.Point(666, 336);
            this.mkcu.Name = "mkcu";
            this.mkcu.Size = new System.Drawing.Size(128, 27);
            this.mkcu.TabIndex = 20;
            this.mkcu.Text = "Mật khẩu cũ:";
            // 
            // guna2TextBox5
            // 
            this.guna2TextBox5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.guna2TextBox5.BorderRadius = 10;
            this.guna2TextBox5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox5.DefaultText = "";
            this.guna2TextBox5.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox5.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox5.FillColor = System.Drawing.Color.Transparent;
            this.guna2TextBox5.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox5.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox5.Location = new System.Drawing.Point(818, 283);
            this.guna2TextBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2TextBox5.Name = "guna2TextBox5";
            this.guna2TextBox5.PlaceholderText = "";
            this.guna2TextBox5.SelectedText = "";
            this.guna2TextBox5.Size = new System.Drawing.Size(254, 37);
            this.guna2TextBox5.TabIndex = 19;
            // 
            // email
            // 
            this.email.BackColor = System.Drawing.Color.Transparent;
            this.email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.ForeColor = System.Drawing.Color.White;
            this.email.Location = new System.Drawing.Point(666, 289);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(63, 27);
            this.email.TabIndex = 18;
            this.email.Text = "Email:";
            // 
            // comfirm_change
            // 
            this.comfirm_change.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.comfirm_change.BorderRadius = 20;
            this.comfirm_change.BorderThickness = 2;
            this.comfirm_change.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.comfirm_change.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.comfirm_change.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.comfirm_change.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.comfirm_change.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(82)))), ((int)(((byte)(73)))));
            this.comfirm_change.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.comfirm_change.ForeColor = System.Drawing.Color.White;
            this.comfirm_change.Location = new System.Drawing.Point(447, 440);
            this.comfirm_change.Name = "comfirm_change";
            this.comfirm_change.Size = new System.Drawing.Size(137, 45);
            this.comfirm_change.TabIndex = 24;
            this.comfirm_change.Text = "Thay đổi";
            // 
            // decline_change
            // 
            this.decline_change.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(245)))), ((int)(((byte)(210)))));
            this.decline_change.BorderRadius = 20;
            this.decline_change.BorderThickness = 2;
            this.decline_change.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.decline_change.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.decline_change.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.decline_change.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.decline_change.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(82)))), ((int)(((byte)(73)))));
            this.decline_change.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.decline_change.ForeColor = System.Drawing.Color.White;
            this.decline_change.Location = new System.Drawing.Point(619, 440);
            this.decline_change.Name = "decline_change";
            this.decline_change.Size = new System.Drawing.Size(137, 45);
            this.decline_change.TabIndex = 25;
            this.decline_change.Text = "Hủy";
            // 
            // UI_main
            // 
            this.ClientSize = new System.Drawing.Size(1280, 552);
            this.Controls.Add(this.mainPanel);
            this.Name = "UI_main";
            this.Text = "GreenSol";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.mainPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.sidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnThongbao;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox2;
        private Guna.UI2.WinForms.Guna2Shapes guna2Shapes1;
        private Guna.UI2.WinForms.Guna2HtmlLabel display_name;
        private Guna.UI2.WinForms.Guna2TextBox nhap_hoten_email;
        private Guna.UI2.WinForms.Guna2HtmlLabel hoten;
        private Guna.UI2.WinForms.Guna2HtmlLabel phong_display;
        private Guna.UI2.WinForms.Guna2HtmlLabel phong_label;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private Guna.UI2.WinForms.Guna2Button comfirm_change;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox3;
        private Guna.UI2.WinForms.Guna2HtmlLabel mkmoi;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox4;
        private Guna.UI2.WinForms.Guna2HtmlLabel mkcu;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox5;
        private Guna.UI2.WinForms.Guna2HtmlLabel email;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox2;
        private Guna.UI2.WinForms.Guna2HtmlLabel ngaysinh;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel sdt;
        private Guna.UI2.WinForms.Guna2Button decline_change;
    }

}
