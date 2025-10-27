using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class UC_ChinhSuaThongTin : UserControl
    {
        // ==================== Biến lớp ====================
        private readonly string tenDangNhap;
        private string nguoiDungID;
        private string vaiTroID;
        private string phongBanID;

        // Lưu giá trị gốc để reset
        private string originalHoTen;
        private string originalSDT;
        private string originalEmail;

        private readonly BLL_TaiKhoan bllNguoiDung = new BLL_TaiKhoan();

        // ==================== Khởi tạo ====================
        public UC_ChinhSuaThongTin(string tenDN)
        {
            InitializeComponent();
            tenDangNhap = tenDN ?? string.Empty;
            txtHoTen.TextChanged += (s, e) => display_name.Text = txtHoTen.Text.Trim();
        }

        private void UC_ChinhSuaThongTin_Load(object sender, EventArgs e)
        {
            TryLoadUserByUsername();
        }

        // ==================== Load dữ liệu người dùng ====================
        private void TryLoadUserByUsername()
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                MessageBox.Show("Thiếu Tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dt = bllNguoiDung.TimKiemNguoiDung(tenDangNhap);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    display_name.Text = tenDangNhap;
                    return;
                }

                var row = dt.AsEnumerable()
                    .FirstOrDefault(r =>
                        r.Table.Columns.Contains("TenDangNhap") &&
                        string.Equals(Convert.ToString(r["TenDangNhap"]), tenDangNhap, StringComparison.OrdinalIgnoreCase))
                    ?? dt.Rows[0];

                nguoiDungID = row["NguoiDungID"]?.ToString();
                vaiTroID    = row["VaiTroID"]?.ToString();
                phongBanID  = row["PhongBanID"]?.ToString();

                txtHoTen.Text  = row["HoVaTen"]?.ToString() ?? "";
                txtSDT.Text    = row["DienThoai"]?.ToString() ?? "";
                txtEmail.Text  = row["Email"]?.ToString() ?? "";
                guna2TextBox1.Text = row["TenPhongBan"]?.ToString() ?? row["PhongBanID"]?.ToString() ?? "";

                display_name.Text = string.IsNullOrWhiteSpace(txtHoTen.Text) ? tenDangNhap : txtHoTen.Text;

                // === Lưu giá trị gốc để reset khi cần ===
                originalHoTen = txtHoTen.Text;
                originalSDT   = txtSDT.Text;
                originalEmail = txtEmail.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                display_name.Text = tenDangNhap;
            }
        }

        // ==================== Reload lại dữ liệu (cho UserMenu gọi) ====================
        public void ReloadData()
        {
            TryLoadUserByUsername();
        }

        // ==================== Validate ====================
        private bool ValidateInput(out string msg)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            { msg = "Vui lòng nhập họ và tên."; txtHoTen.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            { msg = "Vui lòng nhập email."; txtEmail.Focus(); return false; }

            if (!string.IsNullOrEmpty(txtSDT.Text))
            {
                if (!txtSDT.Text.All(char.IsDigit))
                { msg = "Số điện thoại chỉ được chứa chữ số."; txtSDT.Focus(); return false; }

                if (txtSDT.Text.Length != 10)
                { msg = "Số điện thoại phải gồm đúng 10 chữ số."; txtSDT.Focus(); return false; }
            }

            if (string.IsNullOrWhiteSpace(nguoiDungID))
            { msg = "Thiếu ID người dùng (không thể cập nhật)."; return false; }

            msg = null; return true;
        }

        // ==================== Lưu thay đổi ====================
        private void comfirm_change_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out string msg))
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dto = new DTO_NguoiDung
                {
                    NguoiDungID = nguoiDungID,
                    TenDangNhap = tenDangNhap,
                    HoVaTen     = txtHoTen.Text.Trim(),
                    DienThoai   = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim(),
                    Email       = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    VaiTroID    = vaiTroID,
                    PhongBanID  = phongBanID
                };

                string matKhauMoi = string.IsNullOrWhiteSpace(txtMatKhauMoi.Text) ? null : txtMatKhauMoi.Text.Trim();
                bllNguoiDung.SuaNguoiDung(dto, matKhauMoi);

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật lại giá trị gốc để lần sau reset đúng dữ liệu mới
                originalHoTen = txtHoTen.Text;
                originalSDT   = txtSDT.Text;
                originalEmail = txtEmail.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== Hủy thay đổi (reset về dữ liệu gốc) ====================
        private void decline_change_Click(object sender, EventArgs e)
        {
            // Reset về dữ liệu ban đầu (không xóa trắng)
            txtHoTen.Text = originalHoTen;
            txtSDT.Text   = originalSDT;
            txtEmail.Text = originalEmail;

            // Xóa mật khẩu đang nhập (cho an toàn)
            txtMatKhauCu.Clear();
            txtMatKhauMoi.Clear();
        }
    }
}
