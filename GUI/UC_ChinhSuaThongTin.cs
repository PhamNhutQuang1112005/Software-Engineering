using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL;
using DTO;
namespace GUI
{
    public partial class UC_ChinhSuaThongTin : UserControl
    {
        // Chỉ nhận username; các ID khác sẽ tự lấy từ DB
        private readonly string tenDangNhap;

        // Lưu lại để cập nhật
        private string nguoiDungID;
        private string vaiTroID;
        private string phongBanID;

        private readonly BLL_TaiKhoan bllNguoiDung = new BLL_TaiKhoan();

        // ======= NHẬN MỖI TÊN ĐĂNG NHẬP =======
        public UC_ChinhSuaThongTin(string tenDN)
        {
            InitializeComponent();
            tenDangNhap = tenDN ?? string.Empty;

            // Cập nhật label tên hiển thị theo họ tên khi gõ
            txtHoTen.TextChanged += (s, e) => display_name.Text = txtHoTen.Text.Trim();
        }

        private void UC_ChinhSuaThongTin_Load(object sender, EventArgs e)
        {
            TryLoadUserByUsername();
        }

        // ======= Load dữ liệu người dùng theo Tên đăng nhập =======
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

                // Lấy đúng dòng theo TenDangNhap, không có thì lấy dòng đầu
                var row = dt.AsEnumerable()
                            .FirstOrDefault(r =>
                                r.Table.Columns.Contains("TenDangNhap") &&
                                string.Equals(Convert.ToString(r["TenDangNhap"]), tenDangNhap, StringComparison.OrdinalIgnoreCase))
                          ?? dt.Rows[0];

                // Cache các ID để cập nhật về sau (không cho sửa vai trò/phòng)
                nguoiDungID = row.Table.Columns.Contains("NguoiDungID") ? Convert.ToString(row["NguoiDungID"]) : null;
                vaiTroID    = row.Table.Columns.Contains("VaiTroID")    ? Convert.ToString(row["VaiTroID"])    : null;
                phongBanID  = row.Table.Columns.Contains("PhongBanID")  ? Convert.ToString(row["PhongBanID"])  : null;

                // Bind thông tin
                txtHoTen.Text    = row.Table.Columns.Contains("HoVaTen")   ? Convert.ToString(row["HoVaTen"])   : "";
                txtSDT.Text      = row.Table.Columns.Contains("DienThoai")  ? Convert.ToString(row["DienThoai"]) : "";
                txtEmail.Text    = row.Table.Columns.Contains("Email")      ? Convert.ToString(row["Email"])     : "";
                

                // Hiển thị tên lớn + phòng (nếu có label)
                display_name.Text = string.IsNullOrWhiteSpace(txtHoTen.Text) ? tenDangNhap : txtHoTen.Text;
                if (this.Controls.ContainsKey("phong_display"))
                {
                    var tenPhong = row.Table.Columns.Contains("TenPhongBan")
                                   ? Convert.ToString(row["TenPhongBan"])
                                   : (row.Table.Columns.Contains("PhongBanID") ? Convert.ToString(row["PhongBanID"]) : "");
                    phong_display.Text = tenPhong;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                display_name.Text = tenDangNhap;
            }
        }

        // ======= Validate tối thiểu =======
        private bool ValidateInput(out string msg)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            { msg = "Vui lòng nhập họ và tên."; txtHoTen.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            { msg = "Vui lòng nhập email."; txtEmail.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(tenDangNhap))
            { msg = "Thiếu Tên đăng nhập để cập nhật."; return false; }

            if (string.IsNullOrWhiteSpace(nguoiDungID))
            { msg = "Thiếu ID người dùng (không thể cập nhật)."; return false; }

            msg = null; return true;
        }

        // ======= Lưu thay đổi =======
        private void comfirm_change_Click(object sender, EventArgs e)
{
    if (!ValidateInput(out string msg))
    {
        MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    try
    {
        string hoTen     = txtHoTen.Text.Trim();
        string sdt       = string.IsNullOrWhiteSpace(txtSDT.Text)   ? null : txtSDT.Text.Trim();
        string email     = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
        string matKhauMoi = string.IsNullOrWhiteSpace(txtMatKhauMoi.Text) ? null : txtMatKhauMoi.Text.Trim();

        // Gói DTO theo chuẩn mới (không đổi vai trò/phòng ban ở màn này)
        var dto = new DTO_NguoiDung
        {
            NguoiDungID = nguoiDungID,   // đã cache khi load
            TenDangNhap = tenDangNhap,   // truyền lại username hiện tại
            HoVaTen     = hoTen,
            DienThoai   = sdt,
            Email       = email,
            VaiTroID    = vaiTroID,      // giữ nguyên
            PhongBanID  = phongBanID     // giữ nguyên
        };

        // matKhauMoi == null => không đổi mật khẩu
        bllNguoiDung.SuaNguoiDung(dto, matKhauMoi);

        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


        // ======= Hủy (giữ nguyên hành vi cũ: xóa nội dung) =======
        private void decline_change_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            
            txtMatKhauCu.Clear();
            txtMatKhauMoi.Clear();
            display_name.Text = string.Empty;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void phong_display_Click(object sender, EventArgs e)
        {

        }
    }
}
