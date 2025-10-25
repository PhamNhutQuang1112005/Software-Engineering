using System;
using System.Data;
using System.Linq; // <== thêm
using System.Windows.Forms;
using BLL;

namespace GUI
{
    public partial class GUI_FormThemNguoiDung : Form
    {
        private readonly string nguoiDungID;
        private readonly DataRow rowToEdit;
        private readonly string originalUsername; // <== thêm: lưu username gốc khi sửa
        private readonly BLL_TaiKhoan bllNguoiDung = new BLL_TaiKhoan();

        public GUI_FormThemNguoiDung()
        {
            InitializeComponent();
            this.nguoiDungID = null;
            this.rowToEdit = null;
            this.originalUsername = null;
            this.Text = "Thêm người dùng";
        }

        public GUI_FormThemNguoiDung(DataRow row)
        {
            InitializeComponent();
            this.rowToEdit = row;
            this.nguoiDungID = row?["NguoiDungID"]?.ToString();
            this.originalUsername = row?["TenDangNhap"]?.ToString();
            this.Text = "Sửa người dùng";
        }
        
        private void GUI_FormThemNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                // Nạp danh sách vai trò & phòng ban
                cboVaiTro.DataSource = bllNguoiDung.GetAllVaiTro();
                cboVaiTro.DisplayMember = "TenVaiTro";
                cboVaiTro.ValueMember = "VaiTroID";

                cboPhongBan.DataSource = bllNguoiDung.GetAllPhongBan();
                cboPhongBan.DisplayMember = "TenPhongBan";
                cboPhongBan.ValueMember = "PhongBanID";

                if (rowToEdit != null)
                {
                    txtTenDangNhap.Text = rowToEdit["TenDangNhap"]?.ToString();

                    if (rowToEdit.Table.Columns.Contains("MatKhauHash"))
                        txtMatKhau.Text = rowToEdit["MatKhauHash"]?.ToString();
                    else
                        txtMatKhau.Text = "";

                    txtHoTen.Text = rowToEdit["HoVaTen"]?.ToString();
                    txtSDT.Text   = rowToEdit["DienThoai"]?.ToString();
                    txtEmail.Text = rowToEdit["Email"]?.ToString();

                    cboVaiTro.Text   = rowToEdit["TenVaiTro"]?.ToString();
                    cboPhongBan.Text = rowToEdit["TenPhongBan"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private bool ValidateInput(out string msg)
{
    if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
    { msg = "Vui lòng nhập tên đăng nhập."; txtTenDangNhap.Focus(); return false; }

    // Chỉ yêu cầu mật khẩu khi THÊM MỚI (nguoiDungID == null)
    if (string.IsNullOrEmpty(nguoiDungID) && string.IsNullOrWhiteSpace(txtMatKhau.Text))
    { msg = "Vui lòng nhập mật khẩu."; txtMatKhau.Focus(); return false; }

    if (string.IsNullOrWhiteSpace(txtHoTen.Text))
    { msg = "Vui lòng nhập họ và tên."; txtHoTen.Focus(); return false; }

    if (cboVaiTro.SelectedValue == null)
    { msg = "Vui lòng chọn vai trò."; cboVaiTro.Focus(); return false; }

    // Email bắt buộc & đúng định dạng
    string email = txtEmail.Text.Trim();
    if (string.IsNullOrWhiteSpace(email))
    { msg = "Vui lòng nhập email."; txtEmail.Focus(); return false; }
    if (!IsValidEmail(email))
    { msg = "Email không hợp lệ. Vui lòng kiểm tra lại."; txtEmail.Focus(); return false; }

    // SDT nếu nhập thì phải 10 chữ số
    string sdt = txtSDT.Text.Trim();
    if (!string.IsNullOrWhiteSpace(sdt) && (sdt.Length != 10 || !sdt.All(char.IsDigit)))
    { msg = "Số điện thoại phải gồm đúng 10 chữ số."; txtSDT.Focus(); return false; }

    msg = null;
    return true;
}

        private bool IsValidEmail(string email)
{
    try
    {
        var addr = new System.Net.Mail.MailAddress(email);
        // so khớp đúng chuỗi gốc (loại bỏ các trường hợp format lạ)
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}

        // == Kiểm tra username đã tồn tại (loại trừ 1 ID nếu đang sửa) ==
        private bool UsernameExists(string tenDN, string excludeID = null)
        {
            // Dùng danh sách tất cả người dùng để so sánh chính xác (tránh LIKE của TimKiem)
            DataTable all = bllNguoiDung.LayTatCaNguoiDung();
            if (all == null) return false;

            return all.AsEnumerable().Any(r =>
            {
                string u = r.Table.Columns.Contains("TenDangNhap") ? r["TenDangNhap"]?.ToString() : null;
                if (string.IsNullOrEmpty(u)) return false;

                // Nếu excludeID != null thì bỏ qua chính record đang sửa
                if (!string.IsNullOrEmpty(excludeID) && r.Table.Columns.Contains("NguoiDungID"))
                {
                    string id = r["NguoiDungID"]?.ToString();
                    if (string.Equals(id, excludeID, StringComparison.OrdinalIgnoreCase))
                        return false;
                }

                return string.Equals(u, tenDN, StringComparison.OrdinalIgnoreCase);
            });
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out string msg))
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string tenDN   = txtTenDangNhap.Text.Trim();

                // Sửa + để trống mật khẩu => gửi null để KHÔNG đổi mật khẩu
                string matKhau;
                if (!string.IsNullOrEmpty(nguoiDungID) && string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    matKhau = null;                // KHÔNG đổi
                else
                    matKhau = txtMatKhau.Text.Trim();  // Thêm mới hoặc sửa có nhập mật khẩu

                string hoTen    = txtHoTen.Text.Trim();
                string sdt      = string.IsNullOrWhiteSpace(txtSDT.Text)   ? null : txtSDT.Text.Trim();
                string email    = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string vaiTro   = cboVaiTro.SelectedValue?.ToString();
                string phongBan = cboPhongBan.SelectedValue?.ToString();

                // ====== CHẶN TRÙNG USERNAME ======
                if (string.IsNullOrEmpty(nguoiDungID))
                {
                    // Thêm mới: bất kỳ trùng nào cũng không cho
                    if (UsernameExists(tenDN))
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Trùng tên đăng nhập",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDangNhap.Focus();
                        txtTenDangNhap.SelectAll();
                        return;
                    }
                }
                else
                {
                    // Sửa: chỉ kiểm tra khi người dùng đổi username
                    if (!string.Equals(tenDN, originalUsername, StringComparison.OrdinalIgnoreCase) &&
                        UsernameExists(tenDN, excludeID: nguoiDungID))
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Trùng tên đăng nhập",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDangNhap.Focus();
                        txtTenDangNhap.SelectAll();
                        return;
                    }
                }
                // ==================================

                if (string.IsNullOrEmpty(nguoiDungID))
                {
                    // Thêm mới
                    bllNguoiDung.ThemNguoiDung(tenDN, matKhau, hoTen, sdt, email, vaiTro, phongBan);
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Sửa
                    bllNguoiDung.SuaNguoiDung(nguoiDungID, tenDN, matKhau, hoTen, sdt, email, vaiTro, phongBan);
                    MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtSDT_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        e.Handled = true;
        }
    }
}
