
﻿using BLL; // Thêm để gọi tầng nghiệp vụ
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace GUI
{
    public partial class GUI_Form_DangNhap : Form
    {
        private readonly BLL_TaiKhoan bllTaiKhoan = new BLL_TaiKhoan();
        private readonly BLL_SendEmail _bllEmail = new BLL_SendEmail();
        // đặt ở đầu class form
        private BLL_SendEmail bllEmail = new BLL_SendEmail();

        private string _currentOtp = null;
        string emailNguoiNhan = "";
        public static class Session
        {
            public static DTO_NguoiDung CurrentUser { get; set; }
        }
        public GUI_Form_DangNhap()
        {
            InitializeComponent();
            SmoothUI.Apply(this);
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === Cấu hình tab ===
            TAB.Appearance = TabAppearance.FlatButtons;
            TAB.ItemSize = new Size(0, 1);
            TAB.SizeMode = TabSizeMode.Fixed;
            TAB.TabStop = false;
            TAB.Multiline = false;
            TAB.Region = null;
            TAB.TabPages[0].BackColor = Color.Transparent;
            TAB.TabMenuVisible = false;

            // (Tuỳ chọn) Đặt tiêu đề tuỳ biến bằng Tag, nếu text TabPage không đúng ý
            ĐăngNhap.Tag = "CHÀO MỪNG BẠN";
            TaoTK.Tag = "TÀI KHOẢN MỚI";
            SMS.Tag = "XÁC THỰC SMS";
            NhapSMS.Tag = "XÁC THỰC NGƯỜI DÙNG";
            XacNhanMK.Tag = "THAY ĐỔI MẬT KHẨU";

            // === Đồng bộ label8 khi đổi tab ===
            TAB.SelectedIndexChanged += (s, e) => SyncHeaderWithTab();

            // Chọn tab mặc định và cập nhật tiêu đề lần đầu
            TAB.SelectedTab = ĐăngNhap;
            SyncHeaderWithTab();

            // === Sự kiện chuyển tab ===
            this.label10.Click += (s, e) => TAB.SelectedTab = TaoTK;
            this.label11.Click += (s, e) => TAB.SelectedTab = SMS;

        }

        // Hàm đồng bộ tiêu đề
        private void SyncHeaderWithTab()
        {
            if (TAB.SelectedTab == null) { label8.Text = string.Empty; return; }
            label8.Text = (string)(TAB.SelectedTab.Tag ?? TAB.SelectedTab.Text ?? "");

        }


        private void GUI_Form_DangNhap_Load(object sender, EventArgs e)
        {
            CheckAndToggleCreateAccountVisibility();
        }

        // =================== Xử lý phím chuyển tab ===================


        // =================== Các nút điều hướng ===================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TAB.SelectedTab = TaoTK;
        }
        private void CheckAndToggleCreateAccountVisibility()
        {
            try
            {
                bool hasAdmin = bllTaiKhoan.HasAdminAccount();

                // Nếu có admin => hiển thị tab tạo tài khoản
                label10.Visible = !hasAdmin;
                label7.Visible = !hasAdmin;
                label10.Enabled = !hasAdmin;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra tài khoản admin:\n{ex.Message}",
                                "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label10_Click(object sender, EventArgs e) => TAB.SelectedTab = TaoTK;
        private void label11_Click(object sender, EventArgs e) => TAB.SelectedTab = SMS;
        private void guna2Button7_Click(object sender, EventArgs e) => TAB.SelectedTab = ĐăngNhap;
        private void guna2Button6_Click(object sender, EventArgs e) => TAB.SelectedTab = ĐăngNhap;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string email = guna2TextBox3.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email trước khi gửi OTP!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            if (bllEmail.SendOtp(email, out string otp))
            {
                MessageBox.Show($"✅ Mã OTP đã được gửi tới: {email}\n",
                                "Gửi OTP thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang tab nhập OTP
                TAB.SelectedTab = NhapSMS;
            }
            else
            {
                MessageBox.Show("❌ Gửi email thất bại.\nVui lòng kiểm tra lại địa chỉ email hoặc kết nối mạng.",
                                "Lỗi gửi OTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            string userInput = guna2TextBox2.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Vui lòng nhập mã OTP!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentOtp == null)
            {
                MessageBox.Show("Chưa có mã OTP nào được gửi. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // So sánh mã người dùng nhập với mã hệ thống đã gửi
            if (userInput == _currentOtp)
            {
                MessageBox.Show("✅ Xác nhận OTP thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang tab chính hoặc trang SMS như bạn định
                TAB.SelectedTab = SMS;

                // Xoá OTP để tránh dùng lại
                _currentOtp = null;
            }
            else
            {
                MessageBox.Show("❌ Mã OTP không chính xác. Vui lòng thử lại!", "Sai mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string email = guna2TextBox3.Text.Trim();
            string userOtp = guna2TextBox2.Text.Trim(); // textbox nơi người dùng nhập OTP

            if (string.IsNullOrEmpty(userOtp))
            {
                MessageBox.Show("Vui lòng nhập mã OTP!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            bool verified = bllEmail.VerifyOtp(email, userOtp);

            if (verified)
            {
                MessageBox.Show("✅ Xác thực OTP thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                TAB.SelectedTab = XacNhanMK;
                // --> Chuyển sang bước kế tiếp (ví dụ đăng nhập / đặt lại mật khẩu)
            }
            else
            {
                MessageBox.Show("❌ Mã OTP không hợp lệ hoặc đã hết hạn!", "Lỗi xác thực",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            string email = guna2TextBox3.Text.Trim();
            string mkMoi = guna2TextBox12.Text.Trim();
            string xacNhan = guna2TextBox1.Text.Trim();

            try
            {
                BLL_TaiKhoan bll = new BLL_TaiKhoan();
                bll.ChangePassword_Email(email, mkMoi, xacNhan);

                MessageBox.Show("Đổi mật khẩu thành công!");
 TAB.SelectedTab = ĐăngNhap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }     

        // =================== Nút Đăng Nhập thật ===================
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string username = textDangNhap.Text.Trim();
            string password = textMK.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = bllTaiKhoan.Login(username, password); // trả DTO_NguoiDung

                if (user != null)
                {
                    // (nếu đã có AppSession, có thể lưu lại)
                    Session.CurrentUser = user;

                    MessageBox.Show($"Chào mừng {user.HoVaTen} ({user.VaiTroID})!",
                        "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    this.Hide();

                    var main = new GUI_main(username);

                    // Khi form chính đóng → thoát hẳn ứng dụng
                    main.FormClosed += (s, args) => Application.Exit();

                    main.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void hoten_email_Click(object sender, EventArgs e) { }
        private void matkhau_Click(object sender, EventArgs e) { }
        private void ĐăngNhập_Click(object sender, EventArgs e) { }
        private void ĐăngKí_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void QuênMậtKhẩu_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void guna2TextBox10_TextChanged(object sender, EventArgs e) { }

        private void guna2TextBox11_TextChanged(object sender, EventArgs e)
        {

        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void NhapSMS_Click(object sender, EventArgs e)
        {

        }

        private void ĐăngNhap_Click(object sender, EventArgs e)
        {

        }

        private void label8_TextChanged(object sender, EventArgs e)
        {

        }

        private void matkhauopen_MouseDown(object sender, MouseEventArgs e)
        {
            textMK.UseSystemPasswordChar = false;
        }

        private void matkhauopen_MouseUp(object sender, MouseEventArgs e)
        {
            textMK.UseSystemPasswordChar = true;
        }

        private void matkhauopen_MouseLeave(object sender, EventArgs e)
        {
            textMK.UseSystemPasswordChar = true;
        }
        //Nút tạo tài khoản Admin moi
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            emailNguoiNhan = guna2TextBox3.Text.Trim();
                     string tenDN   = txtdangnhap.Text.Trim();
              string matKhau = txtmatkhau.Text.Trim();
                    string sdt = txtSDT.Text.Trim();

             if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matKhau))
             {
                 MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!",
                               "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              return;
             }

              try
              {
           //  --- Kiểm tra trùng tên đăng nhập ---
                if (UsernameExists(tenDN))
               {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.",
                                 "Trùng tên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 txtdangnhap.Focus();
                txtdangnhap.SelectAll();
                 return;
               }
                if (!string.IsNullOrEmpty(sdt))
                {
                if (sdt.Length != 10)
              {
                 MessageBox.Show("Số điện thoại phải gồm đúng 10 chữ số!",
                          "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              txtSDT.Focus();
              return;
            }
        }

        // --- Tạo DTO người dùng mới ---
           var dto = new DTO_NguoiDung
            {
              TenDangNhap = tenDN,
              HoVaTen     = "",      // để trống, chỉnh sau
               DienThoai   = sdt,
                 Email       = "",
            VaiTroID    = "VT001",   // Admin
               PhongBanID  = "PB001"    // Phòng ban mặc định
             };

       //  --- Gọi tầng nghiệp vụ để thêm ---
            bllTaiKhoan.AddNguoiDung(dto, matKhau);

           MessageBox.Show($"Tạo tài khoản {tenDN} (Admin) thành công!",
             "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Quay về tab đăng nhập
         TAB.SelectedTab = ĐăngNhap;
             textDangNhap.Clear();
            txtmatkhau.Clear();
          }
          catch (Exception ex)
          {
              MessageBox.Show("Lỗi khi tạo tài khoản: " + ex.Message,
            "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
          }
        public bool UsernameExists(string tenDN)
        {
            DataTable all = bllTaiKhoan.GetAllNguoiDung();
            if (all == null) return false;

            return all.AsEnumerable().Any(r =>
            {
                string u = r.Table.Columns.Contains("TenDangNhap") ? r["TenDangNhap"]?.ToString() : null;
                if (string.IsNullOrEmpty(u)) return false;


                return string.Equals(u, tenDN, StringComparison.OrdinalIgnoreCase);
            });
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // bỏ ký tự không hợp lệ
            }
        }

        private void SMS_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
