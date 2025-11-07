using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq; // để duyệt Application.OpenForms
using BLL;

namespace GUI
{
    public partial class GUI_FormThemKhach : Form
    {
        // Nếu null => THÊM, ngược lại => SỬA
        private readonly string khachHangID;
        private readonly DataRow rowToEdit;
        private readonly BLL_ThongSoQuanTrac _bllViTri = new BLL_ThongSoQuanTrac(); // dùng để update địa chỉ vị trí

        public GUI_FormThemKhach()
        {
            InitializeComponent();
            this.khachHangID = null;
            this.rowToEdit = null;
            this.Text = "Thêm khách hàng";
            txtMaKH.Text = GenerateSuggestedMaKhachHang();
        }

        // Overload: truyền sẵn DataRow để sửa
        public GUI_FormThemKhach(DataRow row)
        {
            InitializeComponent();
            this.rowToEdit = row;
            this.khachHangID = row?["KhachHangID"]?.ToString();
            this.Text = "Sửa khách hàng";
        }

        // Overload: truyền ID để sửa
        public GUI_FormThemKhach(string id)
        {
            InitializeComponent();
            this.khachHangID = id;
            this.rowToEdit = null;
            this.Text = "Sửa khách hàng";
            LoadByIdAndFill(id);
        }

        // Nạp dữ liệu theo ID và fill control (không cần Linq)
        private void LoadByIdAndFill(string id)
        {
            try
            {
                var dt = BLL_KhachHang.GetAllKhachHang();
                if (dt == null) return;

                string safeId = (id ?? "").Replace("'", "''");
                DataRow[] rows = dt.Select("KhachHangID = '" + safeId + "'");
                if (rows.Length == 0) return;

                var r = rows[0];
                txtMaKH.Text = Convert.ToString(r["MaKhachHang"]);
                txtTenCongTy.Text = Convert.ToString(r["TenCongTy"]);
                txtMaSoThue.Text = Convert.ToString(r["MaSoThue"]);
                txtNguoiDaiDien.Text = Convert.ToString(r["NguoiDaiDien"]);
                txtSDT.Text = Convert.ToString(r["DienThoai"]);
                txtEmail.Text = Convert.ToString(r["Email"]);
                txtDiaChi.Text = Convert.ToString(r["DiaChi"]);
                txtGhiChu.Text = Convert.ToString(r["GhiChu"]);
            }
            catch
            {
                // Có thể log/MessageBox nếu cần
            }
        }

        private void GUI_FormThemKhach_Load(object sender, EventArgs e)
        {
            if (rowToEdit != null)
            {
                // Map dữ liệu lên control khi sửa (chế độ DataRow)
                txtMaKH.Text = rowToEdit["MaKhachHang"]?.ToString();
                txtTenCongTy.Text = rowToEdit["TenCongTy"]?.ToString();
                txtMaSoThue.Text = rowToEdit["MaSoThue"]?.ToString();
                txtNguoiDaiDien.Text = rowToEdit["NguoiDaiDien"]?.ToString();
                txtSDT.Text = rowToEdit["DienThoai"]?.ToString();
                txtEmail.Text = rowToEdit["Email"]?.ToString();
                txtDiaChi.Text = rowToEdit["DiaChi"]?.ToString();
                txtGhiChu.Text = rowToEdit["GhiChu"]?.ToString();
            }
        }

        private bool ValidateInputs(out string message)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                message = "Vui lòng nhập Mã khách hàng.";
                txtMaKH.Focus(); return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenCongTy.Text))
            {
                message = "Vui lòng nhập Tên công ty.";
                txtTenCongTy.Focus(); return false;
            }

            // MST: rỗng thì bỏ qua; nếu có phải 10 hoặc 13 số
            if (!string.IsNullOrWhiteSpace(txtMaSoThue.Text))
            {
                var ms = Regex.Replace(txtMaSoThue.Text, @"\D", "");
                if (!(ms.Length == 10 || ms.Length == 13))
                {
                    message = "Mã số thuế phải gồm 10 hoặc 13 chữ số.";
                    txtMaSoThue.Focus(); return false;
                }
            }

            // Email: đơn giản
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!Regex.IsMatch(txtEmail.Text.Trim(),
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                {
                    message = "Email không hợp lệ.";
                    txtEmail.Focus(); return false;
                }
            }

            // SĐT: tối đa 20 ký tự theo DB
            if (!string.IsNullOrWhiteSpace(txtSDT.Text) && txtSDT.Text.Trim().Length > 20)
            {
                message = "Số điện thoại không được vượt quá 20 ký tự.";
                txtSDT.Focus(); return false;
            }

            message = null;
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs(out var msg))
                {
                    MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuẩn hóa input
                string ma = txtMaKH.Text.Trim();
                string ten = txtTenCongTy.Text.Trim();
                string maSoThue = string.IsNullOrWhiteSpace(txtMaSoThue.Text) ? null : txtMaSoThue.Text.Trim();
                string nguoiDaiDien = string.IsNullOrWhiteSpace(txtNguoiDaiDien.Text) ? null : txtNguoiDaiDien.Text.Trim();
                string sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
                string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string diachi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim();
                string ghichu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

                if (string.IsNullOrEmpty(khachHangID))
                {
                    // THÊM MỚI: ID tự sinh trong proc
                    BLL_KhachHang.AddKhachHang(ma, ten, maSoThue, nguoiDaiDien, sdt, email, diachi, ghichu);
                    MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // CẬP NHẬT
                    BLL_KhachHang.UpdateKhachHang(khachHangID, ma, ten, maSoThue, nguoiDaiDien, sdt, email, diachi, ghichu);
                    MessageBox.Show("Cập nhật khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đồng bộ địa chỉ mới cho tất cả vị trí thuộc các đơn hàng của khách này (nếu có địa chỉ mới)
                    if (!string.IsNullOrWhiteSpace(diachi))
                        SyncDiaChiKhachHangToViTri(khachHangID, diachi);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đồng bộ địa chỉ khách hàng xuống tất cả ViTri thuộc các đơn hàng của khách hàng đó
        private void SyncDiaChiKhachHangToViTri(string khId, string newDiaChi)
        {
            try
            {
                var dtDonHang = BLL_DonHang.GetAllDonHang();
                if (dtDonHang == null || !dtDonHang.Columns.Contains("IDKhachHang")) return;

                // Chỉ lấy các đơn hàng của khách này
                string safeId = (khId ?? "").Replace("'", "''");
                DataRow[] dhRows = dtDonHang.Select("IDKhachHang = '" + safeId + "'");
                if (dhRows.Length == 0) return;

                foreach (var dhRow in dhRows)
                {
                    string donHangID = Convert.ToString(dhRow["DonHangID"]);
                    if (string.IsNullOrWhiteSpace(donHangID)) continue;

                    DataTable dtViTri = _bllViTri.GetViTriByDonHang(donHangID);
                    if (dtViTri == null || !dtViTri.Columns.Contains("ViTriID")) continue;

                    foreach (DataRow vt in dtViTri.Rows)
                    {
                        string viTriID = Convert.ToString(vt["ViTriID"]);
                        if (string.IsNullOrWhiteSpace(viTriID)) continue;
                        string tenViTri = dtViTri.Columns.Contains("TenViTri") ? Convert.ToString(vt["TenViTri"]) : null;
                        // Cập nhật tên giữ nguyên, chỉ thay DiaChi
                        _bllViTri.UpdateTenVaDiaChiViTri(viTriID, tenViTri, newDiaChi);
                    }
                }

                // Cập nhật UI nếu form chi tiết đơn hàng đang mở
                foreach (Form f in Application.OpenForms)
                {
                    var chiTiet = f as GUI_FormDonHangChiTiet;
                    if (chiTiet == null) continue;
                    // Kiểm tra DonHangID form có thuộc khách này không
                    // Cách: tra lại DonHangID -> IDKhachHang trong dtDonHang
                    string dhId = chiTiet.DonHangID;
                    if (string.IsNullOrWhiteSpace(dhId)) continue;
                    DataRow[] match = dtDonHang.Select("DonHangID = '" + dhId.Replace("'", "''") + "' AND IDKhachHang = '" + safeId + "'");
                    if (match.Length > 0)
                    {
                        chiTiet.OnKhachHangDiaChiUpdated(newDiaChi);
                    }
                }
            }
            catch
            {
                // bỏ qua lỗi đồng bộ để không cản việc lưu KH
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e) { }

        private string GenerateSuggestedMaKhachHang()
        {
            return "KH-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        private void txtTenCongTy_TextChanged(object sender, EventArgs e) { }

        private void txtNguoiDaiDien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTenCongTy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
    }
}
