using BLL;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace GUI
{
    public partial class GUI_FormThemNguoiDung : Form
    {
        private readonly string nguoiDungID;
        private readonly DataRow rowToEdit;
        private readonly string originalUsername;
        private readonly BLL_TaiKhoan bllNguoiDung = new BLL_TaiKhoan();
        private byte[] _avatarBytes;
        public Action OnUsersChanged { get; set; }
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
                // Vai trò: loại khỏi combo mục 'Kỹ thuật viên'
                var roles = bllNguoiDung.GetAllVaiTro();
                var filtered = roles
                    .Where(r => !string.Equals(r.TenVaiTro, "Kỹ thuật viên", StringComparison.OrdinalIgnoreCase)
                             && !string.Equals(r.TenVaiTro, "Ky thuat vien", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                cboVaiTro.DataSource = filtered;
                cboVaiTro.DisplayMember = "TenVaiTro";
                cboVaiTro.ValueMember = "VaiTroID";

                // Phòng ban giữ nguyên
                cboPhongBan.DataSource = bllNguoiDung.GetAllPhongBan();
                cboPhongBan.DisplayMember = "TenPhongBan";
                cboPhongBan.ValueMember = "PhongBanID";

                picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

                // ===== Lấy dữ liệu người dùng CHỈ bằng hàm có sẵn: GetAllNguoiDung() =====
                DataRow src = rowToEdit; // ưu tiên row truyền vào
                // Nếu không có row hoặc row thiếu các cột mới (DiaChi/HinhDaiDien) thì lấy lại từ DB
                bool needFetchFromDb =
                    (src == null) ||
                    !src.Table.Columns.Contains("DiaChi") ||
                    !src.Table.Columns.Contains("HinhDaiDien");

                if (needFetchFromDb)
                {
                    DataTable all = bllNguoiDung.GetAllNguoiDung(); // << CHỈ dùng hàm có sẵn
                    if (all != null && all.Rows.Count > 0)
                    {
                        // 1) Tìm theo NguoiDungID (nếu có)
                        if (!string.IsNullOrEmpty(nguoiDungID) && all.Columns.Contains("NguoiDungID"))
                        {
                            src = all.AsEnumerable()
                                     .FirstOrDefault(r => Convert.ToString(r["NguoiDungID"]) == nguoiDungID);
                        }

                        // 2) Fallback: tìm theo TenDangNhap gốc (nếu đang sửa)
                        if (src == null && !string.IsNullOrEmpty(originalUsername) && all.Columns.Contains("TenDangNhap"))
                        {
                            src = all.AsEnumerable()
                                     .FirstOrDefault(r => string.Equals(
                                         Convert.ToString(r["TenDangNhap"]), originalUsername,
                                         StringComparison.OrdinalIgnoreCase));
                        }

                        // 3) Fallback cuối: nếu textbox đã có username thì thử theo textbox
                        if (src == null && all.Columns.Contains("TenDangNhap") && !string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
                        {
                            var ten = txtTenDangNhap.Text.Trim();
                            src = all.AsEnumerable()
                                     .FirstOrDefault(r => string.Equals(
                                         Convert.ToString(r["TenDangNhap"]), ten,
                                         StringComparison.OrdinalIgnoreCase));
                        }
                    }
                }

                // Bind lên UI nếu tìm được; nếu không thì giữ logic thêm mới (để trống)
                if (src != null) BindFromRow(src);
                else
                {
                    // thêm mới: đảm bảo không hiển thị ảnh cũ
                    picAvatar.Image = null;
                    _avatarBytes = null;
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
            DataTable all = bllNguoiDung.GetAllNguoiDung();
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
                string tenDN = txtTenDangNhap.Text.Trim();
                string matKhau = (!string.IsNullOrEmpty(nguoiDungID) && string.IsNullOrWhiteSpace(txtMatKhau.Text))
                                 ? null : txtMatKhau.Text.Trim();
                string hoTen = txtHoTen.Text.Trim();
                string sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
                string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string vaiTro = cboVaiTro.SelectedValue?.ToString();
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
                    HoVaTen = hoTen,
                    DienThoai = sdt,
                    Email = email,
                    VaiTroID = vaiTro,
                    PhongBanID = phongBan,
                    DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                    HinhDaiDien = _avatarBytes
                };

                if (string.IsNullOrEmpty(nguoiDungID))
                {
                    bllNguoiDung.AddNguoiDung(dto, matKhau);            // THÊM
                    MessageBox.Show("Thêm người dùng thành công!");
                }
                else
                {
                    bllNguoiDung.UpdateNguoiDung(dto, matKhau);         // SỬA
                    MessageBox.Show("Cập nhật người dùng thành công!");
                }

                OnUsersChanged?.Invoke();             // 🔔 báo màn cha reload ngay
                this.DialogResult = DialogResult.OK;  // để màn cha biết là thành công
                this.Close();                         // đóng form
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

        private void txtHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép phím điều khiển (Backspace, Delete, mũi tên, v.v.)
            if (char.IsControl(e.KeyChar))
                return;

            // Nếu không phải là chữ hoặc khoảng trắng => chặn
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Ngăn nhập ký tự đó
                System.Media.SystemSounds.Beep.Play(); // Phát tiếng "bíp" nhẹ (tùy chọn)
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ảnh|*.png;*.jpg;*.jpeg;*.bmp";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    var img = Image.FromFile(ofd.FileName);
                    picAvatar.Image = img;

                    using (var ms = new System.IO.MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // normalize PNG
                        _avatarBytes = ms.ToArray();
                    }
                }
            }
        }
        private void BindFromRow(DataRow r)
{
    if (r == null) return;

    txtTenDangNhap.Text = r.Table.Columns.Contains("TenDangNhap") ? r["TenDangNhap"]?.ToString() : "";
    txtMatKhau.Text     = string.Empty; // không show pass khi sửa
    txtHoTen.Text       = r.Table.Columns.Contains("HoVaTen")     ? r["HoVaTen"]?.ToString()     : "";
    txtSDT.Text         = r.Table.Columns.Contains("DienThoai")   ? r["DienThoai"]?.ToString()   : "";
    txtEmail.Text       = r.Table.Columns.Contains("Email")       ? r["Email"]?.ToString()       : "";
    txtDiaChi.Text      = r.Table.Columns.Contains("DiaChi")      ? r["DiaChi"]?.ToString()      : "";

    // Avatar
    if (r.Table.Columns.Contains("HinhDaiDien") && r["HinhDaiDien"] != DBNull.Value)
    {
        var bytes = (byte[])r["HinhDaiDien"];
        using (var ms = new MemoryStream(bytes))
            picAvatar.Image = Image.FromStream(ms);
        // để _avatarBytes = null => nếu không chọn ảnh mới thì giữ ảnh cũ
        _avatarBytes = null;
    }
    else
    {
        picAvatar.Image = null;
        _avatarBytes = null;
    }

    // Vai trò / Phòng ban theo ID (nếu có)
    if (r.Table.Columns.Contains("VaiTroID"))
        cboVaiTro.SelectedValue = r["VaiTroID"]?.ToString();
    if (r.Table.Columns.Contains("PhongBanID"))
        cboPhongBan.SelectedValue = r["PhongBanID"]?.ToString();

    // Fallback theo tên hiển thị (khi SelectedValue chưa match)
    if (cboVaiTro.SelectedIndex < 0 && r.Table.Columns.Contains("TenVaiTro"))
        cboVaiTro.SelectedIndex = cboVaiTro.FindStringExact(r["TenVaiTro"]?.ToString() ?? "");
    if (cboPhongBan.SelectedIndex < 0 && r.Table.Columns.Contains("TenPhongBan"))
        cboPhongBan.SelectedIndex = cboPhongBan.FindStringExact(r["TenPhongBan"]?.ToString() ?? "");
}

        private void cboPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
