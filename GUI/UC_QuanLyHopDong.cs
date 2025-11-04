using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI
{
    public partial class UC_QuanLyHopDong : UserControl
    {
        private readonly Btnbeautifull _theme = new Btnbeautifull()
        {
            Text = Color.White,
            Outline = Color.FromArgb(120, 195, 170),
            SearchFill = Color.Azure,
            SearchText = Color.Black,
            SearchPlaceholder = Color.Black
        };
        private string _selectedId = null;
        private Guna2Panel _selectedCard = null;
        private const int BottomPaddingForShadow = 30;
        private const int FlowTopPadding = 0;

        // Card theme (match UC_QuanLyDonHang / UC_QuanLyKhachHang)
        private static readonly Color ClrOutline = Color.FromArgb(120, 195, 170);
        private static readonly Color ClrText = Color.WhiteSmoke;
        private static readonly Color ClrCardBg = Color.FromArgb(40, 255, 255, 255);

        public UC_QuanLyHopDong()
        {
            InitializeComponent();
            this.Load += UC_QuanLyHopDong_Load;

            if (ThanhTimKiem != null)
                ThanhTimKiem.TextChanged += (s, e) => ApplyFilters();
            // Designer đã gán Click cho thêm; chỉ gắn các nút khác cần thiết
            if (xoahopdong != null)
                xoahopdong.Click += guna2Button2_Click; // Xóa
            if (suahopdong != null)
                suahopdong.Click += guna2Button3_Click; // Sửa

            if (loctheotrangthai != null) loctheotrangthai.SelectedIndexChanged += (s, e) => ApplyFilters();
            if (loctheokhachhang != null) loctheokhachhang.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private DataTable _cacheAll; // cache dữ liệu để lọc client-side

        private void UC_QuanLyHopDong_Load(object sender, EventArgs e)
        {
            EnsureFlow();
            LayoutFlowUnderToolbar();
            LoadDanhSachHopDong();
            InitFilterCombos();

            // Style controls to match other UCs (helper-based)
            try
            {
                if (themhopdong != null) PillStyler.Button(themhopdong, _theme);
                if (xoahopdong != null) PillStyler.Button(xoahopdong, _theme);
                if (suahopdong != null) PillStyler.Button(suahopdong, _theme);
                if (loctheokhachhang != null) PillStyler.Combo(loctheokhachhang, _theme);
                if (loctheotrangthai != null) PillStyler.Combo(loctheotrangthai, _theme);
                if (ThanhTimKiem != null) PillStyler.SearchBox(ThanhTimKiem, _theme, ThanhTimKiem.PlaceholderText);
            }
            catch { }
        }

        private void EnsureFlow()
        {
            // Đảm bảo flowLayoutPanel1 tồn tại (bạn đã thêm trong Designer). Nếu chưa, tạo tạm.
            if (flowLayoutPanel1 == null)
            {
                flowLayoutPanel1 = new FlowLayoutPanel
                {
                    Name = "flowLayoutPanel1",
                    BackColor = Color.Transparent,
                    Location = new Point(10, 380),
                    Size = new Size(this.Width - 20, this.Height - 400),
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
            // Panel trên cùng để căn dưới giống UC_QuanLyKhachHang (guna2Panel1)
            int left = this.Padding.Left;
            int top = (guna2Panel1 != null ? guna2Panel1.Bottom : 0);
            int right = this.Width - this.Padding.Right;
            int bottom = this.Height - this.Padding.Bottom;

            flowLayoutPanel1.Location = new Point(left, top);
            flowLayoutPanel1.Size = new Size(Math.Max(0, right - left), Math.Max(0, bottom - top));
        }

        private void LoadDanhSachHopDong()
        {
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            try
            {
                _cacheAll = BLL_HopDong.GetAllHopDong();
                BuildCards(_cacheAll);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flowLayoutPanel1.ResumeLayout();
                CenterCards();
            }
        }

        private void InitFilterCombos()
        {
            try
            {
                if (_cacheAll == null) return;

                // === Khách hàng: DataSource hiển thị tên (TenCongTy), Value là ID (KhachHangID) ===
                var khDistinct = _cacheAll.AsEnumerable()
                    .Where(r => !string.IsNullOrWhiteSpace(r.Field<string>("KhachHangID")))
                    .GroupBy(r => r.Field<string>("KhachHangID"))
                    .Select(g => new
                    {
                        KhachHangID = g.Key,
                        TenCongTy = g.Select(r => r.Field<string>("TenCongTy")).FirstOrDefault()
                    })
                    .OrderBy(x => x.TenCongTy ?? x.KhachHangID)
                    .ToList();

                var dtKH = new DataTable();
                dtKH.Columns.Add("KhachHangID", typeof(string));
                dtKH.Columns.Add("TenCongTy", typeof(string));
                dtKH.Rows.Add(DBNull.Value, "(Tất cả)"); // option all

                foreach (var x in khDistinct)
                    dtKH.Rows.Add(x.KhachHangID, x.TenCongTy ?? x.KhachHangID);

                loctheokhachhang.DataSource = dtKH;
                loctheokhachhang.DisplayMember = "TenCongTy";    // hiển thị tên
                loctheokhachhang.ValueMember = "KhachHangID";  // giá trị là ID
                loctheokhachhang.SelectedIndex = 0;

                // === Trạng thái ===
                var stList = _cacheAll.AsEnumerable()
                    .Select(r => r.Field<string>("TrangThai"))
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                loctheotrangthai.Items.Clear();
                loctheotrangthai.Items.Add("(Tất cả)");
                loctheotrangthai.Items.AddRange(stList.Cast<object>().ToArray());
                loctheotrangthai.SelectedIndex = 0;
            }
            catch { }
        }

        private void ApplyFilters()
        {
            if (_cacheAll == null) return;
            var rows = _cacheAll.AsEnumerable();

            // từ khóa
            var kw = (ThanhTimKiem?.Text ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(kw))
            {
                rows = rows.Where(r =>
                    (r.Field<string>("MaHopDong") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("KhachHangID") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("TenCongTy") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("TenKyHan") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("TrangThai") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("GhiChu") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0
                );
            }

            // lọc theo khách hàng
            var selKHId = loctheokhachhang?.SelectedValue as string;
            if (!string.IsNullOrWhiteSpace(selKHId))
            {
                rows = rows.Where(r => string.Equals(
                    r.Field<string>("KhachHangID"),
                    selKHId,
                    StringComparison.OrdinalIgnoreCase));
            }

            // lọc theo trạng thái
            var selSt = loctheotrangthai?.SelectedItem as string;
            if (!string.IsNullOrWhiteSpace(selSt) && selSt != "(Tất cả)")
            {
                rows = rows.Where(r => string.Equals(r.Field<string>("TrangThai"), selSt, StringComparison.OrdinalIgnoreCase));
            }

            var filtered = rows.Any() ? rows.CopyToDataTable() : _cacheAll.Clone();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();
            BuildCards(filtered);
            flowLayoutPanel1.ResumeLayout();
            CenterCards();
        }

        private void BuildCards(DataTable dt)
        {
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                flowLayoutPanel1.Controls.Add(TaoCardHopDong(row));
                count++;
            }
            if (count == 0)
            {
                var lbl = new Label
                {
                    Text = "Không tìm thấy hợp đồng nào.",
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                flowLayoutPanel1.Controls.Add(lbl);
            }

            bool stillExists = (dt != null) && dt.AsEnumerable().Any(r =>
                ((r["HopDongID"] ?? "").ToString()) == (_selectedId ?? ""));
            if (count == 0 || (_selectedId != null && !stillExists))
                ClearSelection();
        }

        private Guna2Panel TaoCardHopDong(DataRow row)
        {
            string hopDongID = Convert.ToString(row["HopDongID"] ?? "");
            string ma = Convert.ToString(row["MaHopDong"] ?? "");
            string khId = Convert.ToString(row["KhachHangID"] ?? "");
            string maKH = Convert.ToString(row["MaKhachHang"] ?? "");
            string tenCty = Convert.ToString(row["TenCongTy"] ?? "");
            string kyHanID = Convert.ToString(row["KyHanID"] ?? "");
            string tenKyHan = Convert.ToString(row["TenKyHan"] ?? "");
            string trangThai = Convert.ToString(row["TrangThai"] ?? "");
            string ghiChu = Convert.ToString(row["GhiChu"] ?? "");
            string ngayKy = row["NgayKy"] == DBNull.Value ? "" : Convert.ToDateTime(row["NgayKy"]).ToString("yyyy-MM-dd");
            string ngayBD = row["NgayBatDau"] == DBNull.Value ? "" : Convert.ToDateTime(row["NgayBatDau"]).ToString("yyyy-MM-dd");
            string ngayKT = row["NgayKetThuc"] == DBNull.Value ? "" : Convert.ToDateTime(row["NgayKetThuc"]).ToString("yyyy-MM-dd");

            const int fixedWidth = 420;
            const int fixedHeight = 175;

            var card = new Guna2Panel
            {
                Width = fixedWidth,
                Height = fixedHeight,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(fixedWidth, fixedHeight),
                MaximumSize = new Size(fixedWidth, fixedHeight),
                BorderRadius = 18,
                ShadowDecoration = { Enabled = false },
                FillColor = ClrCardBg,
                BorderColor = ClrOutline,
                BorderThickness = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(15),
                Padding = new Padding(16, 12, 16, 14),
                Tag = hopDongID,
                Cursor = Cursors.Hand
            };

            int contentWidth = fixedWidth - card.Padding.Left - card.Padding.Right;

            Func<string, Font, Label> L = (text, font) => new Label
            {
                AutoSize = true,
                MaximumSize = new Size(contentWidth, 0), // wrap theo chiều rộng nội dung
                Dock = DockStyle.Top,
                Text = text,
                Font = font ?? new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = ClrText,
                BackColor = Color.Transparent
            };

            var lblHeader = L($"{ma}", new Font("Segoe UI", 11, FontStyle.Bold));
            var lblKH = L($"👤 KH: {(string.IsNullOrEmpty(tenCty) ? "(Chưa có tên)" : tenCty)}", null);
            var lblKyHan = L($"⏱ Kỳ hạn: {(string.IsNullOrEmpty(tenKyHan) ? "" : $"{tenKyHan}")}", null);
            var lblNgayKy = L($"🗓 Ngày ký: {ngayKy}", null);
            var lblHieuLuc = L($"⏳ Hiệu lực: {ngayBD} → {ngayKT}", null);
            var lblTrangThai = L($"📌 Trạng thái: {trangThai}", null);
            var lblMoTa = L("📝 " + (string.IsNullOrWhiteSpace(ghiChu) ? "(Không có mô tả)" : ghiChu), null);

            // Clamp description to one line like DonHang's note
            if (!string.IsNullOrWhiteSpace(ghiChu))
            {
                lblMoTa.MaximumSize = new Size(contentWidth, 20);
                lblMoTa.AutoEllipsis = true;
                lblMoTa.TextAlign = ContentAlignment.MiddleLeft;
            }

            // Thêm theo thứ tự ngược để header ở trên cùng (Dock=Top)
            card.Controls.Add(lblMoTa);
            card.Controls.Add(lblTrangThai);
            card.Controls.Add(lblHieuLuc);
            card.Controls.Add(lblNgayKy);
            card.Controls.Add(lblKyHan);
            card.Controls.Add(lblKH);
            card.Controls.Add(lblHeader);

            AttachClickRecursive(card, () => SelectCard(card));
            card.DoubleClick += (s, e) => MoFormSua(hopDongID);

            if (_selectedId != null && _selectedId == hopDongID)
                ApplySelectedStyle(card, true);

            return card;
        }

        private void AttachClickRecursive(Control root, Action onClick)
        {
            root.Click += (s, e) => onClick();
            foreach (Control child in root.Controls)
                AttachClickRecursive(child, onClick);
        }

        private void SelectCard(Guna2Panel card)
        {
            if (_selectedCard == card) return;
            if (_selectedCard != null) ApplySelectedStyle(_selectedCard, false);
            _selectedCard = card;
            _selectedId = (card.Tag ?? "").ToString();
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

        private void CenterCards()
        {
            if (flowLayoutPanel1.Controls.Count == 0) return;
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (var f = new GUI_Form_Them_HopDong())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachHopDong();
                    InitFilterCombos();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Vui lòng chọn một hợp đồng để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa hợp đồng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    BLL_HopDong.DeleteHopDong(_selectedId);
                    ClearSelection();
                    LoadDanhSachHopDong();
                    InitFilterCombos();
                    MessageBox.Show("Đã xóa hợp đồng!", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hợp đồng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Vui lòng chọn một hợp đồng để sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MoFormSua(_selectedId);
        }

        private void MoFormSua(string id)
        {
            using (var f = new GUI_Form_Them_HopDong(id))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachHopDong();
                    InitFilterCombos();
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lockhachhang_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {

        }

        private void loctrangthai_Click(object sender, EventArgs e)
        {

        }
    }
}
