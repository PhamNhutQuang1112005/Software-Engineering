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
            this.logo = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.slogan1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.slogan2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.slogan3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.slogan4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.gradient_box_message = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.mainPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.sidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.gradient_box_message.SuspendLayout();
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
            this.contentPanel.Controls.Add(this.gradient_box_message);
            this.contentPanel.Controls.Add(this.slogan4);
            this.contentPanel.Controls.Add(this.slogan3);
            this.contentPanel.Controls.Add(this.slogan2);
            this.contentPanel.Controls.Add(this.slogan1);
            this.contentPanel.Controls.Add(this.logo);
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
            // logo
            // 
            this.logo.Image = global::UI.Properties.Resources.Green_Sol;
            this.logo.ImageRotate = 0F;
            this.logo.Location = new System.Drawing.Point(140, 40);
            this.logo.Name = "logo";
            this.logo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.logo.Size = new System.Drawing.Size(165, 160);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // slogan1
            // 
            this.slogan1.BackColor = System.Drawing.Color.Transparent;
            this.slogan1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slogan1.ForeColor = System.Drawing.Color.White;
            this.slogan1.Location = new System.Drawing.Point(371, 62);
            this.slogan1.Name = "slogan1";
            this.slogan1.Size = new System.Drawing.Size(108, 40);
            this.slogan1.TabIndex = 1;
            this.slogan1.Text = "Gốc rễ";
            this.slogan1.Click += new System.EventHandler(this.slogan1_Click);
            // 
            // slogan2
            // 
            this.slogan2.BackColor = System.Drawing.Color.Transparent;
            this.slogan2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slogan2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(109)))), ((int)(((byte)(77)))));
            this.slogan2.Location = new System.Drawing.Point(485, 62);
            this.slogan2.Name = "slogan2";
            this.slogan2.Size = new System.Drawing.Size(193, 40);
            this.slogan2.TabIndex = 2;
            this.slogan2.Text = "vững vàng...";
            // 
            // slogan3
            // 
            this.slogan3.BackColor = System.Drawing.Color.Transparent;
            this.slogan3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slogan3.ForeColor = System.Drawing.Color.White;
            this.slogan3.Location = new System.Drawing.Point(671, 126);
            this.slogan3.Name = "slogan3";
            this.slogan3.Size = new System.Drawing.Size(181, 40);
            this.slogan3.TabIndex = 3;
            this.slogan3.Text = "...Giải pháp";
            this.slogan3.Click += new System.EventHandler(this.guna2HtmlLabel2_Click);
            // 
            // slogan4
            // 
            this.slogan4.BackColor = System.Drawing.Color.Transparent;
            this.slogan4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slogan4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(220)))), ((int)(((byte)(89)))));
            this.slogan4.Location = new System.Drawing.Point(858, 126);
            this.slogan4.Name = "slogan4";
            this.slogan4.Size = new System.Drawing.Size(183, 40);
            this.slogan4.TabIndex = 4;
            this.slogan4.Text = "thông minh!";
            // 
            // gradient_box_message
            // 
            this.gradient_box_message.BackColor = System.Drawing.Color.Transparent;
            this.gradient_box_message.BorderRadius = 20;
            this.gradient_box_message.BorderThickness = 1;
            this.gradient_box_message.Controls.Add(this.guna2HtmlLabel1);
            this.gradient_box_message.FillColor = System.Drawing.Color.Black;
            this.gradient_box_message.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gradient_box_message.FillColor3 = System.Drawing.Color.Transparent;
            this.gradient_box_message.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gradient_box_message.Location = new System.Drawing.Point(116, 239);
            this.gradient_box_message.Name = "gradient_box_message";
            this.gradient_box_message.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gradient_box_message.Size = new System.Drawing.Size(960, 423);
            this.gradient_box_message.TabIndex = 5;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(66, 31);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(853, 18);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "text placeholder: GreenSol ra đời với một mục tiêu rõ ràng: mang đến những giải p" +
    "háp thông minh và bền vững cho công tác quan trắc môi trường.";
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
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.gradient_box_message.ResumeLayout(false);
            this.gradient_box_message.PerformLayout();
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnThongbao;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2CirclePictureBox logo;
        private Guna.UI2.WinForms.Guna2HtmlLabel slogan1;
        private Guna.UI2.WinForms.Guna2HtmlLabel slogan3;
        private Guna.UI2.WinForms.Guna2HtmlLabel slogan2;
        private Guna.UI2.WinForms.Guna2HtmlLabel slogan4;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel gradient_box_message;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }

}
