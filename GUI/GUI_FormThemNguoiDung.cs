using System;
using System.Data;
using System.Windows.Forms;
using BLL;

namespace GUI
{
    public partial class GUI_FormThemNguoiDung : Form
    {
        private readonly string nguoiDungID;
        private readonly DataRow rowToEdit;
        private readonly BLL_TaiKhoan bllNguoiDung = new BLL_TaiKhoan();

        public GUI_FormThemNguoiDung()
        {
            InitializeComponent();
            this.nguoiDungID = null;
            this.rowToEdit = null;
            this.Text = "Thêm người dùng";
        }

        public GUI_FormThemNguoiDung(DataRow row)
        {
            InitializeComponent();
            this.rowToEdit = row;
            this.nguoiDungID = row?["NguoiDungID"]?.ToString();
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
                    txtHoTen.Text       = rowToEdit["HoVaTen"]?.ToString();
                    txtSDT.Text         = rowToEdit["DienThoai"]?.ToString();
                    txtEmail.Text       = rowToEdit["Email"]?.ToString();

                    cboVaiTro.Text      = rowToEdit["TenVaiTro"]?.ToString();
                    cboPhongBan.Text    = rowToEdit["TenPhongBan"]?.ToString();
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
            {
                msg = "Vui lòng nhập tên đăng nhập."; txtTenDangNhap.Focus(); return false;
            }
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                msg = "Vui lòng nhập mật khẩu."; txtMatKhau.Focus(); return false;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                msg = "Vui lòng nhập họ và tên."; txtHoTen.Focus(); return false;
            }
            if (cboVaiTro.SelectedValue == null)
            {
                msg = "Vui lòng chọn vai trò."; cboVaiTro.Focus(); return false;
            }

            msg = null; return true;
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
                string matKhau = txtMatKhau.Text.Trim();
                string hoTen   = txtHoTen.Text.Trim();
                string sdt     = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
                string email   = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string vaiTro  = cboVaiTro.SelectedValue?.ToString();
                string phongBan= cboPhongBan.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(nguoiDungID))
                {
                    // Thêm mới
                    bllNguoiDung.ThemNguoiDung(tenDN, matKhau, hoTen, sdt, email, vaiTro, phongBan);
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Sửa (bạn có thể thêm proc update riêng)
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
    }
}
