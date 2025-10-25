using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL;

namespace GUI
{
    public partial class GUI_Form_Them_HopDong : Form
    {
        private readonly string hopDongID;

        public GUI_Form_Them_HopDong()
        {
            InitializeComponent();
            this.hopDongID = null;
            this.Text = "Thêm hợp đồng";
            WireEvents();
            InitCombos();
            InitFormForAdd();
        }

        public GUI_Form_Them_HopDong(string id)
        {
            InitializeComponent();
            this.hopDongID = id;
            this.Text = "Sửa hợp đồng";
            WireEvents();
            InitCombos();
            LoadByIdAndFill(id);
            LockMaHopDong();
        }

        private void WireEvents()
        {
            themhopdong.Click += btnLuu_Click;
            huy.Click += btnHuy_Click;
        }

        // ====== INIT UI ======
        private void InitCombos()
        {
            try
            {
                // Khách hàng: KhachHangID
                var kh = BLL_KhachHang.GetAllKhachHang();
                if (kh != null)
                {
                    khachhang.DisplayMember = "KhachHangID";
                    khachhang.ValueMember = "KhachHangID";
                    khachhang.DataSource = kh;
                }

                // Loại hợp đồng (Kỳ hạn): KyHanID + TenKyHan
                var ky = BLL_KyHanHopDong.GetAllKyHanHopDong();
                if (ky != null)
                {
                    loaihopdong.DisplayMember = "TenKyHan";
                    loaihopdong.ValueMember = "KyHanID";
                    loaihopdong.DataSource = ky;
                }

                // Phòng phụ trách: dữ liệu tạm (không DB)
                var dsPhong = new[]
                {
                    "Môi trường", "Kinh doanh", "Kỹ thuật", "Vận hành"
                };
                phongphutrach.Items.Clear();
                phongphutrach.Items.AddRange(dsPhong);
                if (phongphutrach.Items.Count > 0) phongphutrach.SelectedIndex = 0;
            }
            catch { }
        }

        private void InitFormForAdd()
        {
            // Khóa ô mã hợp đồng, sinh gợi ý mã hợp đồng và HopDongID theo quy tắc: HD-năm-số thứ tự
            LockMaHopDong();
            var nextId = GenerateNextHopDongId(); // HDyyyy-####
            var suggestedMa = GenerateSuggestedMaHopDong(); // HD-yyyyMMdd-HHmmss
            mahopdong.Text = suggestedMa;
            // Lưu ý: HopDongID sẽ dùng nextId khi lưu
            mahopdong.Tag = nextId; // giữ tạm HopDongID trong Tag
        }

        private void LockMaHopDong()
        {
            mahopdong.ReadOnly = true;
        }

        // Tạo HopDongID tăng dần: HD-năm-số thứ tự (dựa trên dữ liệu hiện có)
        private string GenerateNextHopDongId()
        {
            try
            {
                var dt = BLL_HopDong.GetAllHopDong();
                int year = DateTime.Now.Year;
                // Lọc các dòng có HopDongID dạng HD-<year>-<stt>
                var ids = dt?.AsEnumerable()
                    .Select(r => Convert.ToString(r["HopDongID"]))
                    .Where(s => !string.IsNullOrWhiteSpace(s) && s.StartsWith("HD-" + year + "-"))
                    .Select(s =>
                    {
                        var parts = s.Split('-');
                        int n; return (parts.Length == 3 && int.TryParse(parts[2], out n)) ? n : 0;
                    })
                    .ToList() ?? new System.Collections.Generic.List<int>();

                int next = ids.Any() ? ids.Max() + 1 : 1;
                return $"HD-{year}-{next}";
            }
            catch
            {
                return $"HD-{DateTime.Now.Year}-1";
            }
        }

        // Sinh gợi ý MaHopDong theo prefix: HD-yyyyMMdd-HHmmss
        private string GenerateSuggestedMaHopDong()
        {
            return "HD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        private void LoadByIdAndFill(string id)
        {
            try
            {
                var dt = BLL_HopDong.GetAllHopDong();
                if (dt == null) return;
                string safeId = (id ?? string.Empty).Replace("'", "''");
                DataRow[] rows = dt.Select("HopDongID = '" + safeId + "'");
                if (rows.Length == 0) return;
                var r = rows[0];

                mahopdong.Text = r["MaHopDong"]?.ToString();
                ngaykyhopdong.Value = r["NgayKy"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(r["NgayKy"]);
                if (r["NgayBatDau"] != DBNull.Value) ngaybatdauhopdong.Value = Convert.ToDateTime(r["NgayBatDau"]);
                if (r["NgayKetThuc"] != DBNull.Value) ngayhethanhopdong.Value = Convert.ToDateTime(r["NgayKetThuc"]);
                tomtatnhiemvu.Text = r["GhiChu"]?.ToString();

                // Set selected combos nếu có dữ liệu
                var khId = r["KhachHangID"]?.ToString();
                if (!string.IsNullOrWhiteSpace(khId)) khachhang.SelectedValue = khId;
                var kyId = r["KyHanID"]?.ToString();
                if (!string.IsNullOrWhiteSpace(kyId)) loaihopdong.SelectedValue = kyId;
            }
            catch { }
        }

        private bool ValidateInputs(out string message)
        {
            // Ô mã hợp đồng là ReadOnly và đã sinh, vẫn kiểm tra đề phòng
            if (string.IsNullOrWhiteSpace(mahopdong.Text))
            {
                message = "Mã hợp đồng chưa sẵn sàng.";
                mahopdong.Focus(); return false;
            }
            if (khachhang.SelectedValue == null)
            {
                message = "Vui lòng chọn Khách hàng.";
                khachhang.Focus(); return false;
            }
            if (loaihopdong.SelectedValue == null)
            {
                message = "Vui lòng chọn Loại hợp đồng.";
                loaihopdong.Focus(); return false;
            }
            // Ngày kết thúc >= ngày ký và >= ngày bắt đầu (nếu có)
            if (ngayhethanhopdong.Value.Date < ngaykyhopdong.Value.Date)
            {
                message = "Ngày kết thúc không được trước Ngày ký.";
                ngayhethanhopdong.Focus(); return false;
            }
            if (ngaybatdauhopdong.Value.Date > ngayhethanhopdong.Value.Date)
            {
                message = "Ngày bắt đầu không được sau Ngày kết thúc.";
                ngaybatdau.Focus(); return false;
            }
            message = null;
            return true;
        }

        private string CalcTrangThai(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            var today = DateTime.Today;
            if (ngayBatDau.HasValue && today < ngayBatDau.Value.Date) return "chuẩn bị hiệu lực";
            if (ngayKetThuc.HasValue && today > ngayKetThuc.Value.Date) return "đã kết thúc";
            if (ngayBatDau.HasValue && ngayKetThuc.HasValue && today >= ngayBatDau.Value.Date && today <= ngayKetThuc.Value.Date) return "đang hiệu lực";
            // nếu thiếu thông tin ngày: coi như đang hiệu lực sau khi ký
            return "đang hiệu lực";
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

                string hopDongId = hopDongID ?? (mahopdong.Tag as string) ?? GenerateNextHopDongId();
                string maHopDong = mahopdong.Text.Trim();
                string khachHangID = (khachhang.SelectedValue ?? khachhang.Text)?.ToString();
                DateTime ngayKy = ngaykyhopdong.Value.Date;
                string kyHanID = (loaihopdong.SelectedValue ?? loaihopdong.Text)?.ToString();
                DateTime? ngayBatDau = ngaybatdauhopdong.Value.Date;
                DateTime? ngayKetThuc = ngayhethanhopdong.Value.Date;
                string trangThai = CalcTrangThai(ngayBatDau, ngayKetThuc);
                string ghiChu = string.IsNullOrWhiteSpace(tomtatnhiemvu.Text) ? null : tomtatnhiemvu.Text.Trim();

                if (string.IsNullOrEmpty(hopDongID))
                {
                    BLL_HopDong.ThemHopDong(hopDongId, maHopDong, khachHangID, ngayKy, kyHanID, ngayBatDau, ngayKetThuc, trangThai, ghiChu);
                    MessageBox.Show("Thêm hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    BLL_HopDong.SuaHopDong(hopDongId, maHopDong, khachHangID, ngayKy, kyHanID, ngayBatDau, ngayKetThuc, trangThai, ghiChu);
                    MessageBox.Show("Cập nhật hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ngayhethanhopdong.Value.Date < ngaykyhopdong.Value.Date)
                {
                    ngayhethanhopdong.Value = ngaykyhopdong.Value.Date;
                }
            }
            catch { }
        }

        private void label9_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
    }
}
