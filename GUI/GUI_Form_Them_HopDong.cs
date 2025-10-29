using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL;
using System.Data.SqlClient; // for SqlException retry handling
using DTO;
namespace GUI
{
    public partial class GUI_Form_Them_HopDong : Form
    {
        private readonly string hopDongID;
        private const int MoTaMaxLen = 1024; // giới hạn mô tả

        public GUI_Form_Them_HopDong()
        {
            InitializeComponent();
            this.hopDongID = null;
            this.Text = "Thêm hợp đồng";
            WireEvents();
            InitCombos();
            InitFormForAdd();
            InitMoTaLimiter();
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
            InitMoTaLimiter();
        }

        private void WireEvents()
        {
            themhopdong.Click += btnLuu_Click;
            huy.Click += btnHuy_Click;
        }

        // Chỉ set MaxLength đơn giản, không cắt và không beep
        private void InitMoTaLimiter()
        {
            if (tomtatnhiemvu == null) return;
            try { tomtatnhiemvu.MaxLength = MoTaMaxLen; } catch { }
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
                    khachhang.DisplayMember = "TenCongTy";     // hiển thị tên
                    khachhang.ValueMember   = "KhachHangID";   // chọn theo ID
                    khachhang.DataSource    = kh;

                }

                // Loại hợp đồng (Kỳ hạn): KyHanID + TenKyHan
                var ky = BLL_KyHanHopDong.GetAllKyHanHopDong();
                if (ky != null)
                {
                    loaihopdong.DisplayMember = "TenKyHan";
                    loaihopdong.ValueMember = "KyHanID";
                    loaihopdong.DataSource = ky;
                }

            }
            catch { }
        }

        private void InitFormForAdd()
        {
            // Khóa ô mã hợp đồng, hiển thị HopDongID và gợi ý tên hợp đồng
            LockMaHopDong();
            var nextId = GenerateNextHopDongId();               // ví dụ: HD-2025-1
            var suggestedName = GenerateSuggestedMaHopDong();   // ví dụ: HD-yyyyMMdd-HHmmss

            // Mã hợp đồng (ID) - ReadOnly
            IDhopdong.Text = nextId;
            IDhopdong.Tag = nextId; // giữ HopDongID để lưu

            // Tên hợp đồng (có thể sửa)
            if (tenhopdong != null)
                tenhopdong.Text = suggestedName;
        }

        private void LockMaHopDong()
        {
            IDhopdong.ReadOnly = true;
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

        // Sinh gợi ý MaHopDong theo prefix: HD-yyyyMMdd-HHmmss (dùng làm Tên hợp đồng mặc định)
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

                // Hiển thị ID hợp đồng (khóa, không sửa)
                IDhopdong.Text = r["HopDongID"]?.ToString();

                // Ngày
                ngaykyhopdong.Value = r["NgayKy"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(r["NgayKy"]);
                if (r["NgayBatDau"] != DBNull.Value) ngaybatdauhopdong.Value = Convert.ToDateTime(r["NgayBatDau"]);
                if (r["NgayKetThuc"] != DBNull.Value) ngayhethanhopdong.Value = Convert.ToDateTime(r["NgayKetThuc"]);

                // Tên hợp đồng (lưu ở cột MaHopDong)
                if (tenhopdong != null)
                    tenhopdong.Text = r["MaHopDong"]?.ToString();

                // Ghi chú
                tomtatnhiemvu.Text = r["GhiChu"]?.ToString();

                // Combo
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
            if (string.IsNullOrWhiteSpace(IDhopdong.Text))
            {
                message = "Mã hợp đồng chưa sẵn sàng.";
                IDhopdong.Focus(); return false;
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
                ngaybatdauhopdong.Focus(); return false;
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

        // ====== Helpers for retry ID build ======
        private bool TryParseHopDongId(string id, out int year, out int seq)
        {
            year = DateTime.Now.Year;
            seq = 1;
            if (string.IsNullOrWhiteSpace(id)) return false;
            var parts = id.Split('-');
            if (parts.Length == 3
                && int.TryParse(parts[1], out var y)
                && int.TryParse(parts[2], out var s))
            {
                year = y;
                seq = s;
                return true;
            }
            return false;
        }

        private string BuildHopDongId(int year, int seq) => $"HD-{year}-{seq}";

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs(out var msg))
                {
                    MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra mô tả không vượt quá 1024 ký tự (thông báo đơn giản)
                var desc = (tomtatnhiemvu?.Text ?? string.Empty).Trim();
                if (desc.Length > MoTaMaxLen)
                {
                    MessageBox.Show("giới hạn kí tự 1024", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tomtatnhiemvu?.Focus();
                    return;
                }

                string hopDongId = hopDongID ?? (IDhopdong.Tag as string) ?? GenerateNextHopDongId();

                // Tên hợp đồng sẽ lưu xuống cột MaHopDong
                string maHopDong = (tenhopdong != null && !string.IsNullOrWhiteSpace(tenhopdong.Text))
                    ? tenhopdong.Text.Trim()
                    : GenerateSuggestedMaHopDong();
                if (maHopDong.Length > 50) maHopDong = maHopDong.Substring(0, 50); // phòng NVARCHAR(50)

                string khachHangID = (khachhang.SelectedValue ?? khachhang.Text)?.ToString();
                DateTime ngayKy = ngaykyhopdong.Value.Date;
                string kyHanID = (loaihopdong.SelectedValue ?? loaihopdong.Text)?.ToString();
                DateTime? ngayBatDau = ngaybatdauhopdong.Value.Date;
                DateTime? ngayKetThuc = ngayhethanhopdong.Value.Date;
                string trangThai = CalcTrangThai(ngayBatDau, ngayKetThuc);
                string ghiChu = string.IsNullOrWhiteSpace(desc) ? null : desc;

                if (string.IsNullOrEmpty(hopDongID))
                {
                    // Thêm mới: retry khi trùng PK/UNIQUE trên HopDongID
                    string baseId = hopDongId;
                    if (!TryParseHopDongId(baseId, out int year, out int seq))
                    {
                        year = DateTime.Now.Year;
                        seq = 1;
                    }

                    const int maxRetries = 50;
                    int attempt = 0;
                    while (true)
                    {
                        string currentId = BuildHopDongId(year, seq);
                        try
                        {
                            var dtoAdd = new DTO_HopDong {
    HopDongID   = currentId,
    MaHopDong   = maHopDong,
    KhachHangID = khachHangID,
    NgayKy      = ngayKy,
    KyHanID     = kyHanID,
    NgayBatDau  = ngayBatDau,
    NgayKetThuc = ngayKetThuc,
    TrangThai   = trangThai,
    GhiChu      = ghiChu
};
BLL_HopDong.ThemHopDong(dtoAdd);

                            MessageBox.Show("Thêm hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                        {
                            // Trùng khóa/unique -> tăng seq và thử lại
                            attempt++;
                            if (attempt >= maxRetries)
                                throw new Exception($"Không thể tạo HopDongID duy nhất sau {maxRetries} lần thử. Vui lòng thử lại.", ex);
                            seq++;
                            continue;
                        }
                    }
                }
                else
                {
                    // Cập nhật
                    var dtoUpd = new DTO_HopDong {
    HopDongID   = hopDongID,
    MaHopDong   = maHopDong,
    KhachHangID = khachHangID,
    NgayKy      = ngayKy,
    KyHanID     = kyHanID,
    NgayBatDau  = ngayBatDau,
    NgayKetThuc = ngayKetThuc,
    TrangThai   = trangThai,
    GhiChu      = ghiChu
};
BLL_HopDong.SuaHopDong(dtoUpd);

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

        private void loaihopdong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void khachhang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IDhopdong_TextChanged(object sender, EventArgs e)
        {

        }

        private void tenhopdong_TextChanged(object sender, EventArgs e)
        {

        }

        private void huy_Click(object sender, EventArgs e)
        {

        }
    }
}
