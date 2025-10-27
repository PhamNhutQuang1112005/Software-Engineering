using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class GUI_FormThemNguoiDung : Form
    {
        private readonly string nguoiDungID;
        private readonly DataRow rowToEdit;
        private readonly string originalUsername;
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
                cboVaiTro.DataSource = bllNguoiDung.GetAllVaiTro();
                cboVaiTro.DisplayMember = "TenVaiTro";
                cboVaiTro.ValueMember = "VaiTroID";

                cboPhongBan.DataSource = bllNguoiDung.GetAllPhongBan();
                cboPhongBan.DisplayMember = "TenPhongBan";
                cboPhongBan.ValueMember = "PhongBanID";

                if (rowToEdit != null)
                {
                    txtTenDangNhap.Text = rowToEdit["TenDangNhap"]?.ToString();
                    txtMatKhau.Text = string.Empty; // không show mật khẩu khi sửa
                    txtHoTen.Text = rowToEdit["HoVaTen"]?.ToString();
                    txtSDT.Text   = rowToEdit["DienThoai"]?.ToString();
                    txtEmail.Text = rowToEdit["Email"]?.ToString();

                    if (rowToEdit.Table.Columns.Contains("VaiTroID"))
                        cboVaiTro.SelectedValue = rowToEdit["VaiTroID"]?.ToString();
                    if (rowToEdit.Table.Columns.Contains("PhongBanID"))
                        cboPhongBan.SelectedValue = rowToEdit["PhongBanID"]?.ToString();

                    if (cboVaiTro.SelectedIndex < 0 && rowToEdit.Table.Columns.Contains("TenVaiTro"))
                        cboVaiTro.SelectedIndex = cboVaiTro.FindStringExact(rowToEdit["TenVaiTro"]?.ToString() ?? "");
                    if (cboPhongBan.SelectedIndex < 0 && rowToEdit.Table.Columns.Contains("TenPhongBan"))
                        cboPhongBan.SelectedIndex = cboPhongBan.FindStringExact(rowToEdit["TenPhongBan"]?.ToString() ?? "");
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

            if (string.IsNullOrEmpty(nguoiDungID) && string.IsNullOrWhiteSpace(txtMatKhau.Text))
            { msg = "Vui lòng nhập mật khẩu."; txtMatKhau.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            { msg = "Vui lòng nhập họ và tên."; txtHoTen.Focus(); return false; }

            if (cboVaiTro.SelectedValue == null)
            { msg = "Vui lòng chọn vai trò."; cboVaiTro.Focus(); return false; }

            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            { msg = "Vui lòng nhập email."; txtEmail.Focus(); return false; }
            if (!IsValidEmail(email))
            { msg = "Email không hợp lệ. Vui lòng kiểm tra lại."; txtEmail.Focus(); return false; }

            string sdt = txtSDT.Text.Trim();
            if (!string.IsNullOrWhiteSpace(sdt) && (sdt.Length != 10 || !sdt.All(char.IsDigit)))
            { msg = "Số điện thoại phải gồm đúng 10 chữ số."; txtSDT.Focus(); return false; }

            msg = null; return true;
        }

        private bool IsValidEmail(string email)
        {
            try { var a = new System.Net.Mail.MailAddress(email); return a.Address == email; }
            catch { return false; }
        }

        public bool UsernameExists(string tenDN, string excludeID = null)
        {
            DataTable all = bllNguoiDung.LayTatCaNguoiDung();
            if (all == null) return false;

            return all.AsEnumerable().Any(r =>
            {
                string u = r.Table.Columns.Contains("TenDangNhap") ? r["TenDangNhap"]?.ToString() : null;
                if (string.IsNullOrEmpty(u)) return false;

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
                string matKhau = (!string.IsNullOrEmpty(nguoiDungID) && string.IsNullOrWhiteSpace(txtMatKhau.Text))
                                 ? null : txtMatKhau.Text.Trim();
                string hoTen    = txtHoTen.Text.Trim();
                string sdt      = string.IsNullOrWhiteSpace(txtSDT.Text)   ? null : txtSDT.Text.Trim();
                string email    = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string vaiTro   = cboVaiTro.SelectedValue?.ToString();
                string phongBan = cboPhongBan.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(nguoiDungID))
                {
                    if (UsernameExists(tenDN))
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Trùng tên đăng nhập",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDangNhap.Focus(); txtTenDangNhap.SelectAll(); return;
                    }
                }
                else
                {
                    if (!string.Equals(tenDN, originalUsername, StringComparison.OrdinalIgnoreCase) &&
                        UsernameExists(tenDN, excludeID: nguoiDungID))
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Trùng tên đăng nhập",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDangNhap.Focus(); txtTenDangNhap.SelectAll(); return;
                    }
                }

                var dto = new DTO_NguoiDung
                {
                    NguoiDungID = nguoiDungID,
                    TenDangNhap = tenDN,
                    HoVaTen     = hoTen,
                    DienThoai   = sdt,
                    Email       = email,
                    VaiTroID    = vaiTro,
                    PhongBanID  = phongBan
                };

                if (string.IsNullOrEmpty(nguoiDungID))
                {
                    bllNguoiDung.ThemNguoiDung(dto, matKhau);
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bllNguoiDung.SuaNguoiDung(dto, matKhau);
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}
