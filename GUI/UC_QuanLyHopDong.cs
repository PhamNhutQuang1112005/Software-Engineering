using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class UC_QuanLyHopDong : UserControl
    {
        private string _selectedId = null;
        private Guna2Panel _selectedCard = null;
        private const int BottomPaddingForShadow = 30;
        private const int FlowTopPadding = 0;

        public UC_QuanLyHopDong()
        {
            InitializeComponent();
            this.Load += UC_QuanLyHopDong_Load;

            if (guna2TextBox1 != null)
                guna2TextBox1.TextChanged += (s, e) => ApplyFilters();
            // Designer đã gán Click cho guna2Button1 (Thêm), tránh gán trùng để không mở form 2 lần
            if (guna2Button2 != null)
                guna2Button2.Click += guna2Button2_Click; // Xóa
            if (guna2Button3 != null)
                guna2Button3.Click += guna2Button3_Click; // Sửa

            // Wire filter combos
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
                // Khách hàng: lấy distinct KhachHangID
                var khList = _cacheAll.AsEnumerable()
                    .Select(r => r.Field<string>("KhachHangID"))
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();
                loctheokhachhang.Items.Clear();
                loctheokhachhang.Items.Add("(Tất cả)");
                loctheokhachhang.Items.AddRange(khList.Cast<object>().ToArray());
                loctheokhachhang.SelectedIndex = 0;

                // Trạng thái: distinct theo cột TrangThai
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
            var kw = (guna2TextBox1?.Text ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(kw))
            {
                rows = rows.Where(r =>
                    (r.Field<string>("MaHopDong") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("KhachHangID") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("TrangThai") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (r.Field<string>("GhiChu") ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0
                );
            }

            // lọc theo khách hàng
            var selKH = loctheokhachhang?.SelectedItem as string;
            if (!string.IsNullOrWhiteSpace(selKH) && selKH != "(Tất cả)")
            {
                rows = rows.Where(r => string.Equals(r.Field<string>("KhachHangID"), selKH, StringComparison.OrdinalIgnoreCase));
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
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
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
            string hopDongID  = Convert.ToString(row["HopDongID"]   ?? "");
            string ma         = Convert.ToString(row["MaHopDong"]   ?? "");
            string khId       = Convert.ToString(row["KhachHangID"] ?? "");
            string maKH       = Convert.ToString(row["MaKhachHang"] ?? "");
            string tenCty     = Convert.ToString(row["TenCongTy"]   ?? "");
            string kyHanID    = Convert.ToString(row["KyHanID"]     ?? "");
            string tenKyHan   = Convert.ToString(row["TenKyHan"]    ?? "");
            string trangThai  = Convert.ToString(row["TrangThai"]   ?? "");
            string ghiChu     = Convert.ToString(row["GhiChu"]      ?? "");
            string ngayKy     = row["NgayKy"]      == DBNull.Value ? "" : Convert.ToDateTime(row["NgayKy"]).ToString("yyyy-MM-dd");
            string ngayBD     = row["NgayBatDau"]  == DBNull.Value ? "" : Convert.ToDateTime(row["NgayBatDau"]).ToString("yyyy-MM-dd");
            string ngayKT     = row["NgayKetThuc"] == DBNull.Value ? "" : Convert.ToDateTime(row["NgayKetThuc"]).ToString("yyyy-MM-dd");

            var card = new Guna2Panel
            {
                Width = 420,
                Height = 240,
                BorderRadius = 10,
                ShadowDecoration = { Enabled = true },
                FillColor = Color.White,
                Margin = new Padding(15),
                Tag = hopDongID,
                Cursor = Cursors.Hand
            };

            // Header: HopDongID + MaHopDong
            var lblHeader = new Label
            {
                Text = $"{hopDongID} | {ma}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = false,
                Height = 28,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var lblKH = new Label { Text = $"👤 KH: {khId} ({maKH}) - {tenCty}", Font = new Font("Segoe UI", 9), Height = 20, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            var lblKyHan = new Label { Text = $"⏱ Kỳ hạn: {kyHanID} - {tenKyHan}", Font = new Font("Segoe UI", 9), Height = 20, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            var lblNgayKy = new Label { Text = $"🗓 Ngày ký: {ngayKy}", Font = new Font("Segoe UI", 9), Height = 20, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            var lblHieuLuc = new Label { Text = $"⏳ Hiệu lực: {ngayBD} → {ngayKT}", Font = new Font("Segoe UI", 9), Height = 20, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            var lblTrangThai = new Label { Text = $"📌 Trạng thái: {trangThai}", Font = new Font("Segoe UI", 9), Height = 20, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            var lblMoTa = new Label { Text = $"📝 Mô tả nhiệm vụ: {(string.IsNullOrWhiteSpace(ghiChu) ? "(Không có)" : ghiChu)}", Font = new Font("Segoe UI", 9), Height = 40, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };

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
                card.FillColor = Color.FromArgb(245, 250, 255);
                card.BorderColor = Color.FromArgb(51, 153, 255);
                card.BorderThickness = 2;
            }
            else
            {
                card.FillColor = Color.White;
                card.BorderColor = Color.Transparent;
                card.BorderThickness = 0;
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
                    BLL_HopDong.XoaHopDong(_selectedId);
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
    }
}
