using BLL;
using DTO;
using System;
using System.Data;
using System.Data.SqlClient; // for SqlException retry handling
using System.Linq;
using System.Windows.Forms;
namespace GUI
{
    public partial class GUI_Form_Them_HopDong : Form
    {
        private readonly string hopDongID;
        private const int MoTaMaxLen = 1024; // giới hạn mô tả
        // Tooltip cảnh báo nhẹ, không chiếm focus
        private readonly ToolTip _warnTip = new ToolTip
        {
            IsBalloon = true,
            ToolTipIcon = ToolTipIcon.Warning,
            ToolTipTitle = "Thông báo",
            UseAnimation = true,
            UseFading = true,
            InitialDelay = 0,
            ReshowDelay = 0,
            AutoPopDelay = 3000
        };

        public GUI_Form_Them_HopDong()
        {
            InitializeComponent();
            this.hopDongID = null;
            this.Text = "Thêm hợp đồng";
            WireEvents();
            InitCombos();
            InitFormForAdd();
            InitMoTaLimiter();
            InitMoTaTopLeft(); // căn chữ top-left cho mô tả
        }

        public GUI_Form_Them_HopDong(string id)
        {
            InitializeComponent();
            this.hopDongID = id;
            this.Text = "Sửa hợp đồng";
            WireEvents();
            InitCombos();
            LoadByIdAndFill(id);
            InitMoTaLimiter();
            InitMoTaTopLeft(); // căn chữ top-left cho mô tả
        }

        private void WireEvents()
        {
            themhopdong.Click += btnLuu_Click;
            huy.Click += btnHuy_Click;
            // end date is derived on save; no UI DateTimePicker for end date anymore
            loaihopdong.SelectedIndexChanged += (s, e) => { /* derive on save */ };
            ngaykyhopdong.ValueChanged += NgayKy_ValueChanged;
            ngaybatdauhopdong.ValueChanged += NgayBatDau_ValueChanged;
        }

        // warn if start date is before sign date (and auto-fix)
        private void NgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            EnsureStartNotBeforeSign(true);
        }

        private void NgayKy_ValueChanged(object sender, EventArgs e)
        {
            EnsureStartNotBeforeSign(false);
        }

        private void EnsureStartNotBeforeSign(bool showWarning)
        {
            try
            {
                var sign = ngaykyhopdong.Value.Date;
                var start = ngaybatdauhopdong.Value.Date;
                if (start < sign)
                {
                    if (showWarning)
                    {
                        // Hiển thị tooltip ngay trên control, không mở cửa sổ mới
                        _warnTip.Hide(ngaybatdauhopdong);
                        _warnTip.Show(
                            "Ngày bắt đầu không được trước Ngày ký.",
                            ngaybatdauhopdong,
                            ngaybatdauhopdong.Width / 2,
                            -40,
                            2500);
                    }
                    ngaybatdauhopdong.Value = sign;
                }
            }
            catch { }
        }

        // Căn top-left cho ô mô tả
        private void InitMoTaTopLeft()
        {
            if (tomtatnhiemvu == null) return;
            try
            {
                tomtatnhiemvu.Multiline = true;
                tomtatnhiemvu.TextAlign = HorizontalAlignment.Left; // ngang trái
                tomtatnhiemvu.AutoSize = false;                     // tránh auto-center dọc
                tomtatnhiemvu.ScrollBars = ScrollBars.Vertical;     // tiện xem khi dài
                if (tomtatnhiemvu.Padding.Top < 4)
                    tomtatnhiemvu.Padding = new Padding(6, 6, 6, 6);
                if (tomtatnhiemvu.Height < 100)
                    tomtatnhiemvu.Height = 120;
            }
            catch { }
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
                // Khách hàng
                var kh = BLL_KhachHang.GetAllKhachHang();
                if (kh != null)
                {
                    khachhang.DisplayMember = "TenCongTy";
                    khachhang.ValueMember = "KhachHangID";
                    khachhang.DataSource = kh;
                }

                // Kỳ hạn: chỉ loại bỏ mục 'Quý' khỏi combo (không đổi DB, không đổi logic khác)
                var ky = BLL_KyHanHopDong.GetAllKyHanHopDong();
                if (ky != null)
                {
                    if (ky.Columns.Contains("TenKyHan"))
                    {
                        foreach (DataRow r in ky.Rows.Cast<DataRow>().ToList())
                        {
                            var ten = Convert.ToString(r["TenKyHan"] ?? "").Trim();
                            if (ten.Equals("Quý", StringComparison.OrdinalIgnoreCase) || ten.Equals("Quy", StringComparison.OrdinalIgnoreCase))
                                r.Delete();
                        }
                        ky.AcceptChanges();
                    }
                    loaihopdong.DisplayMember = "TenKyHan";
                    loaihopdong.ValueMember = "KyHanID";
                    loaihopdong.DataSource = ky;
                }
            }
            catch { }
        }

        private void InitFormForAdd()
        {
            // Không còn dùng ô IDhopdong trên UI; chỉ gợi ý tên hợp đồng (MaHopDong)
            var suggestedName = GenerateSuggestedMaHopDong();   // ví dụ: HD-yyyyMMdd-HHmmss

            // Tên hợp đồng (có thể sửa)
            if (tenhopdong != null)
                tenhopdong.Text = suggestedName;
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

                // Ngày
                ngaykyhopdong.Value = r["NgayKy"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(r["NgayKy"]);
                if (r["NgayBatDau"] != DBNull.Value) ngaybatdauhopdong.Value = Convert.ToDateTime(r["NgayBatDau"]);
                // Không còn UI cho Ngày kết thúc -> tính khi lưu

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
            if (khachhang.SelectedValue == null)
            { message = "Vui lòng chọn Khách hàng."; khachhang.Focus(); return false; }
            if (loaihopdong.SelectedValue == null)
            { message = "Vui lòng chọn Loại hợp đồng."; loaihopdong.Focus(); return false; }
            message = null; return true;
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

        // Lấy số tháng từ lựa chọn Kỳ hạn (chỉ hỗ trợ: "X tháng", "X năm (...)" hoặc "Quý (...)" )
        private int GetMonthsFromSelectedLoaiHopDong()
        {
            try
            {
                string ten = null;
                if (loaihopdong?.SelectedItem is DataRowView drv && drv.Row.Table.Columns.Contains("TenKyHan"))
                {
                    ten = Convert.ToString(drv.Row["TenKyHan"]);
                }
                if (string.IsNullOrWhiteSpace(ten))
                    ten = Convert.ToString(loaihopdong?.Text);

                return ParseMonthsFromTenKyHan(ten);
            }
            catch { return 0; }
        }

        private static int ExtractFirstNumber(string s)
        {
            int num = 0; bool found = false;
            foreach (var ch in s)
            {
                if (char.IsDigit(ch)) { num = num * 10 + (ch - '0'); found = true; }
                else if (found) break;
            }
            return found ? num : 0;
        }

        private int ParseMonthsFromTenKyHan(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten)) return 0;
            var s = ten.Trim().ToLowerInvariant();

            // Ưu tiên "quý" (3 tháng) nếu xuất hiện (không có số đếm quý theo confirm)
            if (s.Contains("quý") || s.Contains("quy")) return 3;

            // Sau đó tới "năm": ví dụ "1 năm (12 tháng)"
            if (s.Contains("năm") || s.Contains("nam"))
            {
                int n = ExtractFirstNumber(s);
                if (n <= 0) n = 1;
                return n * 12;
            }

            // Cuối cùng "tháng": ví dụ "6 tháng"
            if (s.Contains("tháng") || s.Contains("thang"))
            {
                int m = ExtractFirstNumber(s);
                if (m <= 0) m = 1;
                return m;
            }

            return 0;
        }

        private DateTime CalcNgayKetThucFromBase(DateTime baseDate)
        {
            int months = GetMonthsFromSelectedLoaiHopDong();
            if (months <= 0) return baseDate; // không xác định -> bằng ngày mốc
            return baseDate.AddMonths(months);
        }

        // Helpers for HopDongID parsing/building (retry unique ID)
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

                string hopDongId = hopDongID ?? GenerateNextHopDongId();

                // Tên hợp đồng sẽ lưu xuống cột MaHopDong
                string maHopDong = (tenhopdong != null && !string.IsNullOrWhiteSpace(tenhopdong.Text))
                    ? tenhopdong.Text.Trim()
                    : GenerateSuggestedMaHopDong();
                if (maHopDong.Length > 50) maHopDong = maHopDong.Substring(0, 50); // phòng NVARCHAR(50)

                string khachHangID = (khachhang.SelectedValue ?? khachhang.Text)?.ToString();
                DateTime ngayKy = ngaykyhopdong.Value.Date;
                string kyHanID = (loaihopdong.SelectedValue ?? loaihopdong.Text)?.ToString();
                DateTime? ngayBatDau = ngaybatdauhopdong.Value.Date;

                // Tính tự động Ngày kết thúc từ NGÀY BẮT ĐẦU (fallback Ngày ký nếu thiếu)
                var baseDate = ngayBatDau.HasValue ? ngayBatDau.Value : ngayKy;
                DateTime? ngayKetThuc = CalcNgayKetThucFromBase(baseDate);

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
                            var dtoAdd = new DTO_HopDong
                            {
                                HopDongID = currentId,
                                MaHopDong = maHopDong,
                                KhachHangID = khachHangID,
                                NgayKy = ngayKy,
                                KyHanID = kyHanID,
                                NgayBatDau = ngayBatDau,
                                NgayKetThuc = ngayKetThuc,
                                TrangThai = trangThai,
                                GhiChu = ghiChu
                            };
                            BLL_HopDong.AddHopDong(dtoAdd);

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
                    var dtoUpd = new DTO_HopDong
                    {
                        HopDongID = hopDongID,
                        MaHopDong = maHopDong,
                        KhachHangID = khachHangID,
                        NgayKy = ngayKy,
                        KyHanID = kyHanID,
                        NgayBatDau = ngayBatDau,
                        NgayKetThuc = ngayKetThuc,
                        TrangThai = trangThai,
                        GhiChu = ghiChu
                    };
                    BLL_HopDong.UpdateHopDong(dtoUpd);

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

        // ĐÃ BỎ UI 'ngayhethanhopdong'
        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void loaihopdong_SelectedIndexChanged(object sender, EventArgs e) { }
        private void khachhang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void IDhopdong_TextChanged(object sender, EventArgs e) { }
        private void tenhopdong_TextChanged(object sender, EventArgs e) { }
        private void huy_Click(object sender, EventArgs e) { }
        private void tomtatnhiemvu_TextChanged(object sender, EventArgs e) { }
        private void GUI_Form_Them_HopDong_Load(object sender, EventArgs e) { }
    }
}
