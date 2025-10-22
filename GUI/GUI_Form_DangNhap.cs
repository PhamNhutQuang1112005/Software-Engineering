using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using BLL; // Thêm để gọi tầng nghiệp vụ

namespace GUI
{
    public partial class GUI_Form_DangNhap : Form
    {
        private readonly BLL_TaiKhoan bllTaiKhoan = new BLL_TaiKhoan();

        public GUI_Form_DangNhap()
        {
            InitializeComponent();
            SmoothUI.Apply(this);
            this.DoubleBuffered = true;

            // === Cấu hình form cố định kích thước ===
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new Size(950, 530);
            this.MaximumSize = new Size(950, 530);
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
            TAB.SelectedTab = ĐăngNhap;

            // === Sự kiện chuyển tab ===
            this.label10.Click += (s, e) => TAB.SelectedTab = TaoTK;
            this.label11.Click += (s, e) => TAB.SelectedTab = SMS;

            // === Gán phím chuyển tab ===
            this.KeyPreview = true;
            this.KeyDown += GUI_Form_DangNhap_KeyDown;
        }

        private void GUI_Form_DangNhap_Load(object sender, EventArgs e) { }

        // =================== Xử lý phím chuyển tab ===================
        private void GUI_Form_DangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            int current = TAB.SelectedIndex;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                if (current < TAB.TabCount - 1)
                    TAB.SelectedIndex++;
                else
                    TAB.SelectedIndex = 0;
            }

            if (e.KeyCode == Keys.Left)
            {
                if (current > 0)
                    TAB.SelectedIndex--;
                else
                    TAB.SelectedIndex = TAB.TabCount - 1;
            }
        }

        // =================== Các nút điều hướng ===================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TAB.SelectedTab = TaoTK;
        }

        private void label10_Click(object sender, EventArgs e) => TAB.SelectedTab = TaoTK;
        private void label11_Click(object sender, EventArgs e) => TAB.SelectedTab = SMS;
        private void guna2Button7_Click(object sender, EventArgs e) => TAB.SelectedTab = ĐăngNhap;
        private void guna2Button6_Click(object sender, EventArgs e) => TAB.SelectedTab = ĐăngNhap;
        private void guna2Button1_Click(object sender, EventArgs e) => TAB.SelectedTab = NhapSMS;
        private void guna2Button8_Click(object sender, EventArgs e) => TAB.SelectedTab = SMS;
        private void guna2Button5_Click(object sender, EventArgs e) => TAB.SelectedTab = XacNhanMK;
        private void guna2Button9_Click(object sender, EventArgs e) => TAB.SelectedTab = ĐăngNhap;

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
                DataTable result = bllTaiKhoan.DangNhap(username, password);

                if (result.Rows.Count > 0)
                {
                    string hoten = result.Rows[0]["HoVaTen"].ToString();
                    string vaitro = result.Rows[0]["VaiTroID"].ToString();

                    MessageBox.Show($"Chào mừng {hoten} ({vaitro})!", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    GUI_main main = new GUI_main();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =================== Các event trống ===================
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
    }
}
