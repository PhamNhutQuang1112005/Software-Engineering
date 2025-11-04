using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class UC_QuanLyThongSoDonHang : UserControl
    {
        private string _selectedId = null;
        private Guna2Panel _selectedCard = null;

        // Cache
        private DataTable _rawDonHang;   // từ BLL_DonHang.GetAllDonHang()
        private DataTable _viewDonHang;  // đã enrich để hiển thị & lọc
        private DataTable _dmHopDong;
        private DataTable _dmKhachHang;
        private DataTable _dmTrangThai;

        // Theme (đã dùng sẵn trong project)
        private readonly Btnbeautifull _theme = new Btnbeautifull()
        {
            Text = Color.White,
            Outline = Color.FromArgb(120, 195, 170),
            SearchFill = Color.Azure,
            SearchText = Color.Black,
            SearchPlaceholder = Color.Black
        };

        // Màu dùng chung
        private static readonly Color ClrOutline = Color.FromArgb(120, 195, 170);
        private static readonly Color ClrText    = Color.WhiteSmoke;
        private static readonly Color ClrHint    = Color.FromArgb(220, 220, 220);
        private static readonly Color ClrCardBg  = Color.FromArgb(40, 255, 255, 255);

        public UC_QuanLyThongSoDonHang()
        {
            InitializeComponent();
            EnableTransparentBgStyles();
            flowLayoutPanel1.Resize += flowLayoutPanel1_Resize;

            // GẮN LOAD ĐÚNG (tránh nhầm tên trước đó)
            this.Load += UC_QuanLyThongSoDonHang_Load;
        }

        // Nếu Designer lỡ trỏ vào tên cũ, giữ wrapper gọi về hàm mới
        private void UC_QuanLyDonHang_Load(object sender, EventArgs e)
            => UC_QuanLyThongSoDonHang_Load(sender, e);

        private void UC_QuanLyThongSoDonHang_Load(object sender, System.EventArgs e)
        {
            // Theme + control style
            InitTransparentTheme();

            // Style combobox/textbox
            PillStyler.Combo(guna2ComboBox2, _theme);
            PillStyler.Combo(guna2ComboBox1, _theme);
            PillStyler.Combo(guna2ComboBox3, _theme);
            PillStyler.SearchBox(guna2TextBox1, _theme, "Tìm kiếm theo mã/tên KH/HĐ...");

            EnsureFlow();
            LayoutFlowUnderToolbar();
            ReloadAll();
            NapComboLoc();

            // Gắn sự kiện lọc chung một hàm
            guna2ComboBox1.SelectedIndexChanged += (s, ev) => LocDonHang();
            guna2ComboBox2.SelectedIndexChanged += (s, ev) => LocDonHang();
            guna2ComboBox3.SelectedIndexChanged += (s, ev) => LocDonHang();
            guna2TextBox1.TextChanged           += (s, ev) => LocDonHang();
        }

        private void NapComboLoc()
        {
            // ===== COMBO NGÀY =====
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("(Tất cả)");
            for (int i = 1; i <= 31; i++)
                guna2ComboBox1.Items.Add(i.ToString());
            guna2ComboBox1.SelectedIndex = 0;

            // ===== COMBO THÁNG =====
            guna2ComboBox2.Items.Clear();
            guna2ComboBox2.Items.Add("(Tất cả)");
            for (int i = 1; i <= 12; i++)
                guna2ComboBox2.Items.Add(i.ToString());
            guna2ComboBox2.SelectedIndex = 0;

            // ===== COMBO QUÝ =====
            guna2ComboBox3.Items.Clear();
            guna2ComboBox3.Items.Add("(Tất cả)");
            guna2ComboBox3.Items.Add("Quý 1");
            guna2ComboBox3.Items.Add("Quý 2");
            guna2ComboBox3.Items.Add("Quý 3");
            guna2ComboBox3.Items.Add("Quý 4");
            guna2ComboBox3.SelectedIndex = 0;
        }

        private void LocDonHang()
        {
            try
            {
                if (_viewDonHang == null || _viewDonHang.Rows.Count == 0)
                {
                    RebuildCards(_viewDonHang);
                    return;
                }

                // ===== LẤY GIÁ TRỊ LỌC =====
                int? ngay = null;
                int? thang = null;
                string quy = null;

                if (guna2ComboBox1.Text != "(Tất cả)" && int.TryParse(guna2ComboBox1.Text, out int n))
                    ngay = n;

                if (guna2ComboBox2.Text != "(Tất cả)" && int.TryParse(guna2ComboBox2.Text, out int t))
                    thang = t;

                if (guna2ComboBox3.Text != "(Tất cả)")
                    quy = guna2ComboBox3.Text;

                string kw = (guna2TextBox1?.Text ?? "").Trim();

                var rows = _viewDonHang.AsEnumerable().Where(r =>
                {
                    if (r["NgayTao"] == DBNull.Value) return false;
                    DateTime ngayTao = Convert.ToDateTime(r["NgayTao"]);

                    bool matchNgay  = !ngay.HasValue  || ngayTao.Day   == ngay.Value;
                    bool matchThang = !thang.HasValue || ngayTao.Month == thang.Value;

                    int quyThang = (ngayTao.Month - 1) / 3 + 1;
                    bool matchQuy =
                        string.IsNullOrEmpty(quy) ||
                        (quy == "Quý 1" && quyThang == 1) ||
                        (quy == "Quý 2" && quyThang == 2) ||
                        (quy == "Quý 3" && quyThang == 3) ||
                        (quy == "Quý 4" && quyThang == 4);

                    if (!(matchNgay && matchThang && matchQuy)) return false;

                    if (string.IsNullOrEmpty(kw)) return true;

                    string s(string col) => _viewDonHang.Columns.Contains(col) ? Convert.ToString(r[col] ?? "") : "";
                    return s("DonHangID").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0
                        || s("MaDonHang").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0
                        || s("MaHopDong").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0
                        || s("TenCongTy").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0
                        || s("GhiChu").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0;
                });

                DataTable filtered = rows.Any() ? rows.CopyToDataTable() : _viewDonHang.Clone();
                RebuildCards(filtered);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc đơn hàng: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============== Layout helpers ===============
        private const int BottomPaddingForShadow = 30;
        private const int FlowTopPadding = 0;

        private void EnsureFlow()
        {
            if (flowLayoutPanel1 == null)
            {
                flowLayoutPanel1 = new FlowLayoutPanel
                {
                    Name = "flowLayoutPanel1",
                    BackColor = Color.Transparent,
                    Location = new Point(10, 180),
                    Size = new Size(this.Width - 20, this.Height - 200),
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                    AutoScroll = true,
                    WrapContents = true,
                    FlowDirection = FlowDirection.LeftToRight
                };
                this.Controls.Add(flowLayoutPanel1);
            }

            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            var p = flowLayoutPanel1.Padding;
            flowLayoutPanel1.Padding = new Padding(p.Left, FlowTopPadding, p.Right, BottomPaddingForShadow);
            flowLayoutPanel1.AutoScrollMargin = new Size(16, 24);

            flowLayoutPanel1.Layout += (s, ev) => CenterCards();
            flowLayoutPanel1.SizeChanged += (s, ev) => CenterCards();
            this.SizeChanged += (s, ev) => LayoutFlowUnderToolbar();
        }

        private void LayoutFlowUnderToolbar()
        {
            int left = this.Padding.Left;
            int top = (guna2Panel2 != null ? guna2Panel2.Bottom : 0);
            int right = this.Width - this.Padding.Right;
            int bottom = this.Height - this.Padding.Bottom;

            flowLayoutPanel1.Location = new Point(left + 8, top + 8);
            flowLayoutPanel1.Size = new Size(Math.Max(0, right - left - 16), Math.Max(0, bottom - top - 16));
        }

        private void CenterCards()
        {
            if (flowLayoutPanel1 == null || flowLayoutPanel1.Controls.Count == 0) return;
            var sample = flowLayoutPanel1.Controls.Cast<Control>().FirstOrDefault(c => c.Visible);
            if (sample == null) return;

            int clientW = flowLayoutPanel1.ClientSize.Width;
            if (flowLayoutPanel1.VerticalScroll.Visible)
                clientW -= SystemInformation.VerticalScrollBarWidth;

            int itemW = sample.Width;
            int itemMargin = sample.Margin.Horizontal;
            int itemFullW = itemW + itemMargin;

            int perRow = Math.Max(1, (clientW + sample.Margin.Left) / itemFullW);
            int usedW = perRow * itemFullW - sample.Margin.Right;
            int leftPad = Math.Max(0, (clientW - usedW) / 2);

            var p = flowLayoutPanel1.Padding;
            flowLayoutPanel1.Padding = new Padding(leftPad, p.Top, 0, p.Bottom);
            flowLayoutPanel1.PerformLayout();
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Guna2Panel card)
                    card.Width = flowLayoutPanel1.ClientSize.Width - 40;
            }
        }

        private void EnableTransparentBgStyles()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
        }

        // =============== Load & Enrich ===============
        private void ReloadAll()
        {
            try
            {
                _dmHopDong   = BLL_HopDong.GetAllHopDong();           // HopDongID, MaHopDong, KhachHangID, ...
                _dmKhachHang = BLL_KhachHang.GetAllKhachHang();       // KhachHangID, TenCongTy, MaKhachHang, ...
                _dmTrangThai = BLL_DonHang.GetAllTrangThaiDonHang();  // TrangThaiID, TenTrangThai

                _rawDonHang  = BLL_DonHang.GetAllDonHang();
                _viewDonHang = EnrichDonHang(_rawDonHang, _dmHopDong, _dmKhachHang, _dmTrangThai);

                RebuildCards(_viewDonHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static DataTable EnrichDonHang(DataTable donHang, DataTable dmHD, DataTable dmKH, DataTable dmTT)
        {
            var result = donHang.Copy();
            if (!result.Columns.Contains("MaHopDong"))    result.Columns.Add("MaHopDong", typeof(string));
            if (!result.Columns.Contains("TenCongTy"))    result.Columns.Add("TenCongTy", typeof(string));
            if (!result.Columns.Contains("MaKhachHang"))  result.Columns.Add("MaKhachHang", typeof(string));
            if (!result.Columns.Contains("TenTrangThai")) result.Columns.Add("TenTrangThai", typeof(string));
            if (!result.Columns.Contains("NgayTao"))      result.Columns.Add("NgayTao", typeof(DateTime));
            if (!result.Columns.Contains("DiaChi")) result.Columns.Add("DiaChi", typeof(string));
            // dict HopDong: (MaHopDong, KhachHangID)
            var hdById = new Dictionary<string, Tuple<string, string>>();
            if (dmHD != null)
            {
                foreach (DataRow r in dmHD.Rows)
                {
                    var key = Convert.ToString(r["HopDongID"] ?? "");
                    var ma  = Convert.ToString(dmHD.Columns.Contains("MaHopDong") ? r["MaHopDong"] : "");
                    var kh  = Convert.ToString(dmHD.Columns.Contains("KhachHangID") ? r["KhachHangID"] : "");
                    if (!hdById.ContainsKey(key))
                        hdById.Add(key, Tuple.Create(ma, kh));
                }
            }

            // dict KhachHang: (TenCongTy, MaKhachHang)
            var khById = new Dictionary<string, Tuple<string, string>>();
            if (dmKH != null)
            {
                foreach (DataRow r in dmKH.Rows)
                {
                    var key = Convert.ToString(r["KhachHangID"] ?? "");
                    var ten = Convert.ToString(dmKH.Columns.Contains("TenCongTy") ? r["TenCongTy"] : "");
                    var ma  = Convert.ToString(dmKH.Columns.Contains("MaKhachHang") ? r["MaKhachHang"] : "");
                    if (!khById.ContainsKey(key))
                        khById.Add(key, Tuple.Create(ten, ma));
                }
            }

            // dict TrangThai: TenTrangThai
            var ttById = new Dictionary<string, string>();
            if (dmTT != null)
            {
                foreach (DataRow r in dmTT.Rows)
                {
                    var key = Convert.ToString(r["TrangThaiID"] ?? "");
                    var ten = Convert.ToString(dmTT.Columns.Contains("TenTrangThai") ? r["TenTrangThai"] : "");
                    if (!ttById.ContainsKey(key))
                        ttById.Add(key, ten);
                }
            }

            foreach (DataRow r in result.Rows)
            {
                string hopDongID   = Convert.ToString(result.Columns.Contains("HopDongID") ? r["HopDongID"] : "");
                string trangThaiID = Convert.ToString(result.Columns.Contains("TrangThaiID") ? r["TrangThaiID"] : "");
                string khID        = "";

                if (result.Columns.Contains("IDKhachHang") && r["IDKhachHang"] != DBNull.Value)
                    khID = Convert.ToString(r["IDKhachHang"]);
                else if (hdById.ContainsKey(hopDongID))
                    khID = hdById[hopDongID].Item2;

                r["MaHopDong"] = hdById.ContainsKey(hopDongID) ? (hdById[hopDongID].Item1 ?? "") : "";

                if (khById.ContainsKey(khID))
                {
                    r["TenCongTy"]   = khById[khID].Item1 ?? "";
                    r["MaKhachHang"] = khById[khID].Item2 ?? "";
                }
                else
                {
                    r["TenCongTy"]   = "";
                    r["MaKhachHang"] = "";
                }

                r["TenTrangThai"] = ttById.ContainsKey(trangThaiID) ? ttById[trangThaiID] : "";
            }

            return result;
        }

        // =============== Build UI ===============
        private void RebuildCards(DataTable dt)
        {
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            int count = 0;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var card = TaoCardDonHang(row);
                    flowLayoutPanel1.Controls.Add(card);
                    count++;
                }
            }

            if (count == 0)
            {
                var lbl = new Label
                {
                    Text = "Không tìm thấy đơn hàng nào.",
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    ForeColor = Color.WhiteSmoke,
                    BackColor = Color.Transparent,
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                flowLayoutPanel1.Controls.Add(lbl);
            }

            bool stillExists = (dt != null) && dt.AsEnumerable().Any(r =>
                Convert.ToString(r["DonHangID"] ?? "") == (_selectedId ?? ""));
            if (count == 0 || (_selectedId != null && !stillExists))
                ClearSelection();

            flowLayoutPanel1.ResumeLayout();
            CenterCards();
        }

        private Guna2Panel TaoCardDonHang(DataRow row)
        {
            string donHangID   = Convert.ToString(row["DonHangID"]   ?? "");
            string maDonHang   = Convert.ToString(row["MaDonHang"]   ?? "");
            string hopDongID   = Convert.ToString(row["HopDongID"]   ?? "");
            string maHopDong   = Convert.ToString(row["MaHopDong"]   ?? "");
            string khachHangID = Convert.ToString(row.Table.Columns.Contains("IDKhachHang") ? row["IDKhachHang"] : "");
            if (string.IsNullOrEmpty(khachHangID))
                khachHangID = Convert.ToString(row.Table.Columns.Contains("KhachHangID") ? row["KhachHangID"] : "");
            string tenCongTy   = Convert.ToString(row["TenCongTy"]   ?? "");
            string maKhachHang = Convert.ToString(row["MaKhachHang"] ?? "");
            string tenTrangThai= Convert.ToString(row["TenTrangThai"]?? "");
            string trangThaiID = Convert.ToString(row.Table.Columns.Contains("TrangThaiID") ? row["TrangThaiID"] : "");

            string ngayLayMau  = (row.Table.Columns.Contains("NgayLayMau") && row["NgayLayMau"] != DBNull.Value)
                                ? Convert.ToDateTime(row["NgayLayMau"]).ToString("yyyy-MM-dd") : "";
            string ngayDuKien  = (row.Table.Columns.Contains("NgayDuKienTraKetQua") && row["NgayDuKienTraKetQua"] != DBNull.Value)
                                ? Convert.ToDateTime(row["NgayDuKienTraKetQua"]).ToString("yyyy-MM-dd") : "";
            string ngayTraTT   = (row.Table.Columns.Contains("NgayTraThucTe") && row["NgayTraThucTe"] != DBNull.Value)
                                ? Convert.ToDateTime(row["NgayTraThucTe"]).ToString("yyyy-MM-dd") : "";
            string ngaytao     = (row.Table.Columns.Contains("NgayTao") && row["NgayTao"] != DBNull.Value)
                                ? Convert.ToDateTime(row["NgayTao"]).ToString("yyyy-MM-dd") : "";
            string ky          = Convert.ToString(row.Table.Columns.Contains("Ky") ? row["Ky"] : "");
            string ghiChu      = Convert.ToString(row.Table.Columns.Contains("GhiChu") ? row["GhiChu"] : "");
            string diaChi      = Convert.ToString(row.Table.Columns.Contains("DiaChi") ? row["DiaChi"] : "");
            int fixedWidth = flowLayoutPanel1.ClientSize.Width - 40;
            var card = new Guna2Panel
            {
                Width = fixedWidth,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(fixedWidth, 0),
                MaximumSize = new Size(fixedWidth, int.MaxValue),

                BorderRadius = 18,
                BorderColor  = ClrOutline,
                BorderThickness = 1,
                ShadowDecoration = { Enabled = false },
                FillColor = ClrCardBg,
                BackColor = Color.Transparent,

                Margin = new Padding(15),
                Padding = new Padding(16, 12, 16, 14),
                Tag = donHangID,
                Cursor = Cursors.Hand
            };

            int contentWidth = fixedWidth - card.Padding.Left - card.Padding.Right;

            Func<string, Font, Label> L = (text, font) => new Label
            {
                AutoSize = true,
                MaximumSize = new Size(contentWidth, 0),
                Dock = DockStyle.Top,
                Text = text,
                Font = font ?? new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = ClrText,
                BackColor = Color.Transparent
            };

            var lblHeader    = L(string.Format("{0} | {1}", donHangID, maDonHang), new Font("Segoe UI", 11, FontStyle.Bold));
            var lblKH        = L(string.Format("👤 KH: {0}{1}{2}{3}",
                                    string.IsNullOrEmpty(khachHangID) ? "" : khachHangID,
                                    string.IsNullOrEmpty(maKhachHang) ? "" : (" (" + maKhachHang + ")"),
                                    (string.IsNullOrEmpty(khachHangID) && string.IsNullOrEmpty(maKhachHang)) ? "" : " - ",
                                    string.IsNullOrEmpty(tenCongTy) ? "(Chưa có tên)" : tenCongTy), null);
            var lblHD        = L(string.Format("📄 HĐ: {0}{1}{2}",
                                    string.IsNullOrEmpty(hopDongID) ? "" : hopDongID,
                                    string.IsNullOrEmpty(maHopDong) ? "" : " - ",
                                    maHopDong), null);
            var lblTrangThai = L(string.Format("📌 Trạng thái: {0}{1}{2}",
                                    string.IsNullOrEmpty(tenTrangThai) ? "" : tenTrangThai,
                                    (string.IsNullOrEmpty(tenTrangThai) || string.IsNullOrEmpty(trangThaiID)) ? "" : " (",
                                    string.IsNullOrEmpty(trangThaiID) ? "" : (trangThaiID + (string.IsNullOrEmpty(tenTrangThai) ? "" : ")"))), null);
            var lblDates     = L($"🗓 Ngày tạo: {ngaytao}", null);
            var lblKy        = L(string.IsNullOrWhiteSpace(ky) ? "" : ("⏱ Kỳ: " + ky), null);
            var lblGhiChu    = L("📝 " + (string.IsNullOrWhiteSpace(ghiChu) ? "(Không có ghi chú)" : ghiChu), null);

            lblGhiChu.MaximumSize = new Size(contentWidth, 20);  // Giới hạn ~1 dòng
            lblGhiChu.AutoEllipsis = true;
            lblGhiChu.TextAlign = ContentAlignment.MiddleLeft;

            card.Controls.Add(lblGhiChu);
            if (!string.IsNullOrEmpty(ky)) card.Controls.Add(lblKy);
            card.Controls.Add(lblDates);
            card.Controls.Add(lblTrangThai);
            card.Controls.Add(lblHD);
            card.Controls.Add(lblKH);
            card.Controls.Add(lblHeader);

            // Click toàn card và mọi child → mở chi tiết
            card.Click += (s, e) => Card_Click(s, e, row);
            foreach (Control ctrl in card.Controls)
                ctrl.Click += (s, e) => Card_Click(card, e, row);

            if (_selectedId != null && _selectedId == donHangID)
                ApplySelectedStyle(card, true);

            return card;
        }

        private void Card_Click(object sender, EventArgs e, DataRow row)
        {
            try
            {
                // Chọn card (highlight)
                if (sender is Guna2Panel p) SelectCard(p);

                string donHangID = Convert.ToString(row["DonHangID"]);
                string maDonHang = Convert.ToString(row["MaDonHang"] ?? "");
                string diaChi    = Convert.ToString(row.Table.Columns.Contains("DiaChi") ? row["DiaChi"] : "");

                if (string.IsNullOrEmpty(donHangID))
                {
                    MessageBox.Show("Không tìm thấy mã đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Ensure vị trí & thông số (nếu chưa có)
                var bll = new BLL_ThongSoQuanTrac();
                bll.EnsureViTriAndThongSo(donHangID);

                // 🔹 Mở form chi tiết
                using (var f = new GUI_FormDonHangChiTiet(donHangID,maDonHang,diaChi))
                {
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết đơn hàng: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectCard(Guna2Panel card)
        {
            if (_selectedCard == card) return;
            if (_selectedCard != null) ApplySelectedStyle(_selectedCard, false);
            _selectedCard = card;
            _selectedId = Convert.ToString(card.Tag ?? "");
            ApplySelectedStyle(card, true);
        }

        private void ClearSelection()
        {
            if (_selectedCard != null) ApplySelectedStyle(_selectedCard, false);
            _selectedCard = null;
            _selectedId = null;
        }

        private void ApplySelectedStyle(Guna2Panel card, bool selected)
        {
            if (selected)
            {
                card.FillColor = Color.FromArgb(60, 255, 255, 255);
                card.BorderColor = Color.FromArgb(180, ClrOutline);
                card.BorderThickness = 2;
            }
            else
            {
                card.FillColor = ClrCardBg;
                card.BorderColor = ClrOutline;
                card.BorderThickness = 1;
            }
        }

        // ================= THEME (Transparent + Pill) =================
        private void InitTransparentTheme()
        {
            try
            {
                if (flowLayoutPanel1 != null)
                    flowLayoutPanel1.BackColor = Color.Transparent;

                if (guna2Panel2 != null)
                    guna2Panel2.BackColor = Color.Transparent;

                StylePillCombo(guna2ComboBox1);
                StylePillCombo(guna2ComboBox2);
                StylePillCombo(guna2ComboBox3);

                StyleSearchBox(guna2TextBox1, "Thanh tìm kiếm");
            }
            catch { }
        }

        private void StylePillButton(Guna2Button btn)
        {
            if (btn == null) return;
            btn.BackColor   = Color.Transparent;
            btn.FillColor   = Color.Transparent;
            btn.ForeColor   = ClrText;
            btn.BorderColor = ClrOutline;
            btn.BorderThickness = 1;
            btn.AutoRoundedCorners = true;
            btn.BorderRadius = Math.Max(18, btn.Height / 2);
            btn.HoverState.FillColor   = Color.FromArgb(24, ClrOutline);
            btn.HoverState.BorderColor = ClrOutline;
            btn.PressedColor           = Color.FromArgb(40, ClrOutline);
            btn.ShadowDecoration.Enabled = false;
        }

        private void StylePillCombo(Guna2ComboBox cb)
        {
            if (cb == null) return;
            cb.BackColor   = Color.Transparent;
            cb.FillColor   = Color.FromArgb(0, 0, 0, 0);
            cb.BorderColor = ClrOutline;
            cb.ForeColor   = ClrText;
            cb.AutoRoundedCorners = true;
            cb.BorderRadius = Math.Max(18, cb.Height / 2);
            cb.DrawMode = DrawMode.OwnerDrawFixed;
            cb.ItemHeight = 30;
            cb.FocusedColor = ClrOutline;
            cb.FocusedState.BorderColor = ClrOutline;
            cb.ItemsAppearance.BackColor = Color.FromArgb(25, 25, 25);
            cb.ItemsAppearance.ForeColor = Color.WhiteSmoke;
            cb.ItemsAppearance.SelectedBackColor = Color.FromArgb(45, 45, 45);
            cb.ShadowDecoration.Enabled = false;
        }

        private void StyleSearchBox(Guna2TextBox txt, string placeholder)
        {
            if (txt == null) return;
            txt.BackColor   = Color.Transparent;
            txt.FillColor   = Color.Azure;
            txt.BorderColor = ClrOutline;
            txt.ForeColor   = Color.Black;
            txt.PlaceholderText = placeholder;
            txt.PlaceholderForeColor = Color.Black;
            txt.AutoRoundedCorners = true;
            txt.BorderRadius = Math.Max(20, txt.Height / 2);
            txt.FocusedState.BorderColor = ClrOutline;
            txt.ShadowDecoration.Enabled = false;
        }

        // =================== (Giữ stub handler Designer gọi tới) ===================
        private void guna2Button1_Click(object sender, System.EventArgs e)
        {
            using (var f = new GUI_FormThemDonHang())
            {
                f.ShowDialog();
                // Thêm xong → reload để thấy đơn hàng mới
                ReloadAll();
            }
        }

        private void guna2HtmlLabel9_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel10_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel11_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel12_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel13_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel14_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel15_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel16_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel17_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel18_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel8_Click(object sender, System.EventArgs e) { }
        private void guna2Panel4_Paint(object sender, PaintEventArgs e) { }
        private void guna2HtmlLabel19_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel20_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel21_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel22_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel23_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel24_Click(object sender, System.EventArgs e) { }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, System.EventArgs e) { }
        private void label4_Click(object sender, System.EventArgs e) { }
        private void guna2ComboBox3_SelectedIndexChanged(object sender, System.EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void guna2HtmlLabel6_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel5_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel4_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel3_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel2_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel1_Click(object sender, System.EventArgs e) { }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e) { }
        private void label1_Click(object sender, System.EventArgs e) { }
        private void guna2TextBox1_TextChanged(object sender, System.EventArgs e) { }
        private void guna2Button4_Click(object sender, System.EventArgs e) { }
        private void guna2Button3_Click(object sender, System.EventArgs e) { }
        private void guna2Button2_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel7_Click(object sender, System.EventArgs e) { }
        private void guna2HtmlLabel6_Click_1(object sender, System.EventArgs e) { }
        private void guna2TextBox1_TextChanged_1(object sender, System.EventArgs e) { }
        private void guna2Panel1_MouseDoubleClick(object sender, MouseEventArgs e) { }
        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e) { }
        private void guna2ComboBox3_SelectedIndexChanged_1(object sender, System.EventArgs e) { }
        private void guna2ComboBox2_SelectedIndexChanged_1(object sender, System.EventArgs e) { }
        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, System.EventArgs e) { }

        // Các handler lọc đã gắn LocDonHang() ở Load; giữ thêm 3 cái này nếu Designer trỏ tới:
        private void guna2ComboBox1_SelectedIndexChanged_2(object sender, EventArgs e) => LocDonHang();
        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e) => LocDonHang();
        private void guna2ComboBox3_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            LocDonHang();
            string quy = guna2ComboBox3.Text;
            guna2ComboBox2.Items.Clear();

            if (quy == "Quý 1")
                guna2ComboBox2.Items.AddRange(new object[] { "(Tất cả)", "1", "2", "3" });
            else if (quy == "Quý 2")
                guna2ComboBox2.Items.AddRange(new object[] { "(Tất cả)", "4", "5", "6" });
            else if (quy == "Quý 3")
                guna2ComboBox2.Items.AddRange(new object[] { "(Tất cả)", "7", "8", "9" });
            else if (quy == "Quý 4")
                guna2ComboBox2.Items.AddRange(new object[] { "(Tất cả)", "10", "11", "12" });
            else
            {
                guna2ComboBox2.Items.Add("(Tất cả)");
                for (int i = 1; i <= 12; i++) guna2ComboBox2.Items.Add(i.ToString());
            }

            guna2ComboBox2.SelectedIndex = 0;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        // Cho phép Form cha yêu cầu reload data từ DB mỗi lần hiển thị
public void ReloadFromParent(bool preserveFilters = true)
{
    try
    {
        // Lưu filter hiện tại (nếu muốn giữ lại)
        string oldNgay  = guna2ComboBox1?.Text;
        string oldThang = guna2ComboBox2?.Text;
        string oldQuy   = guna2ComboBox3?.Text;
        string oldKw    = guna2TextBox1?.Text;

        // Tải lại toàn bộ danh mục + đơn hàng từ DB
        ReloadAll();              // đã có sẵn trong control
        // RebuildCards(_viewDonHang) đã được gọi trong ReloadAll()

        if (preserveFilters)
        {
            // Khôi phục filter và áp lại lọc
            if (!string.IsNullOrEmpty(oldNgay)  && guna2ComboBox1.Items.Contains(oldNgay))
                guna2ComboBox1.SelectedItem = oldNgay;

            if (!string.IsNullOrEmpty(oldThang) && guna2ComboBox2.Items.Contains(oldThang))
                guna2ComboBox2.SelectedItem = oldThang;

            if (!string.IsNullOrEmpty(oldQuy)   && guna2ComboBox3.Items.Contains(oldQuy))
                guna2ComboBox3.SelectedItem = oldQuy;

            if (guna2TextBox1 != null)
                guna2TextBox1.Text = oldKw ?? string.Empty;

            LocDonHang(); // áp lại bộ lọc sau khi đã có _viewDonHang mới
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Không thể reload dữ liệu: " + ex.Message,
            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

    }
}
