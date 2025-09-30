using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class UI_main : Form
    {
        private bool isDark = true; // trạng thái theme hiện tại

        public UI_main()
        {
            InitializeComponent();
            guna2Panel5.Visible = false;
            guna2Panel6.Visible = false;












        }
        // chuyển các button thành public để gọi từ form khác
        public Guna.UI2.WinForms.Guna2Button BtnTrangChu => btnTrangChu;
        public Guna.UI2.WinForms.Guna2Button BtnKhachHang => btnKhachHang;
        public Guna.UI2.WinForms.Guna2Button BtnHopDong => btnHopDong;
        public Guna.UI2.WinForms.Guna2Button BtnDonHang => btnDonHang;
        public Guna.UI2.WinForms.Guna2Button BtnThongKeTienDo => btnThongKeTienDo;
        public Guna.UI2.WinForms.Guna2Button BtnThongKeDonHang => btnThongKeDonHang;
        public Guna.UI2.WinForms.Guna2Button BtnQuanLyUsers => btnQuanLyUsers;
        public Guna.UI2.WinForms.Guna2Button BtnQuanLyUser => btnQuanLyUser;
        public Guna.UI2.WinForms.Guna2Button BtnThongBao => btnThongbao;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Khi load form, mặc định chạy Dark mode
            ApplyDarkTheme();

            
        }

        /// <summary>
        /// Áp dụng giao diện Dark mode
        /// </summary>
        private void ApplyDarkTheme()
        {
            // Main background
            mainPanel.BackColor = Color.FromArgb(30, 40, 30);
            contentPanel.BackgroundImage = Properties.Resources.background__dark_;

            // Header
            header.FillColor = Color.FromArgb(151, 176, 103);
            header.FillColor2 = Color.FromArgb(47, 82, 73);

            lblUser.ForeColor = Color.White;
            lblVersion.ForeColor = Color.White;

            // Sidebar
            sidebar.FillColor = Color.FromArgb(151, 176, 103);
            sidebar.FillColor2 = Color.FromArgb(47, 82, 73);
            foreach (var btn in sidebar.Controls.OfType<Guna2Button>())
            {
                btn.FillColor = Color.DarkSeaGreen;
                btn.ForeColor = Color.Black;
            }

            // Đổi icon nút sang Light mode (mặt trời)
            guna2CircleButton1.Image = Properties.Resources.light_mode;
        }

        /// <summary>
        /// Áp dụng giao diện Light mode
        /// </summary>
        private void ApplyLightTheme()
        {
            // Main background
            mainPanel.BackColor = Color.WhiteSmoke;
            contentPanel.BackgroundImage = Properties.Resources.background__light_; // cần thêm ảnh Light vào Resources

            // Header dùng gradient #c6d870 → #eff5d2
            header.FillColor = ColorTranslator.FromHtml("#c6d870");
            header.FillColor2 = ColorTranslator.FromHtml("#eff5d2");

            lblUser.ForeColor = Color.Black;
            lblVersion.ForeColor = Color.DimGray;

            // Sidebar cũng dùng gradient tương tự
            sidebar.FillColor = ColorTranslator.FromHtml("#c6d870");
            sidebar.FillColor2 = ColorTranslator.FromHtml("#eff5d2");

            // Buttons (nền trắng, chữ đen)
            foreach (var btn in sidebar.Controls.OfType<Guna2Button>())
            {
                btn.FillColor = Color.LemonChiffon;
                btn.ForeColor = Color.Black;
            }

            // Đổi icon nút sang Dark mode (mặt trăng)
            guna2CircleButton1.Image = Properties.Resources.dark_mode;
        }
       

        /// <summary>
        /// Xử lý khi bấm nút chuyển theme
        /// </summary>
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (isDark)
            {
                ApplyLightTheme();
                isDark = false;

                // đổi toàn bộ ảnh trong contentPanel sang ảnh sáng
                ChangePictureBoxImages(contentPanel, Properties.Resources.account);
            }
            else
            {
                ApplyDarkTheme();
                isDark = true;

                // đổi toàn bộ ảnh trong contentPanel sang ảnh tối
                ChangePictureBoxImages(contentPanel, Properties.Resources.user);
            }
        }
        private void ChangePictureBoxImages(Control parent, Image img)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2PictureBox pic)
                   
                {
                    pic.Image = img; // đổi ảnh
                    pic.SizeMode = PictureBoxSizeMode.Zoom; // cho chắc ảnh hiển thị
                }
                else if (ctrl.HasChildren) // nếu control này chứa control con
                {
                    ChangePictureBoxImages(ctrl, img); // duyệt tiếp
                }
            }
        }


        // ========================
        // Các event xử lý button menu
        // ========================

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Trang Chủ
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Khách Hàng
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Hợp Đồng
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Đơn Hàng
        }

        private void btnThongKeTienDo_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Thống Kê Tiến Độ
        }

        private void btnThongKeDonHang_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Thống Kê Đơn Hàng
        }

        private void btnQuanLyUsers_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Quản Lý Users
        }

        private void btnQuanLyUser_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi bấm Quản Lý User
        }

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e) {
            guna2Panel5.Visible = !guna2Panel5.Visible;
        }
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e) { }
        private void lblUser_Click(object sender, EventArgs e) { }
        private void btnTrangChu_Click_1(object sender, EventArgs e) { }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            guna2Panel6.Visible = !guna2Panel6.Visible;


        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            
        }

        private void btnQuanLyUsers_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            ClearPanelInputs(guna2Panel6);
            guna2Panel6.Visible=false;
        }
        private void ClearPanelInputs(Panel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                switch (ctrl)
                {

                    case Guna.UI2.WinForms.Guna2TextBox gtb: // Guna2TextBox
                        gtb.Text = "";
                        break;

                    case Guna.UI2.WinForms.Guna2ComboBox gcb: // Guna2ComboBox
                        gcb.SelectedIndex = -1;
                        break;
                    case CheckBox chk:    // nếu có checkbox
                        chk.Checked = false;
                        break;
                    case RadioButton rb:  // nếu có radio
                        rb.Checked = false;
                        break;
                }
            }
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
