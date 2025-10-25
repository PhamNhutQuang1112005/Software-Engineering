// ========================================================
// UC_QuanLyKhachHang.cs  (.NET Framework 4.7.2)
// CRUD + xem danh sách, chọn/xóa/sửa, tìm kiếm theo tên/MST/MãKH/Email/SĐT.
// ========================================================
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class UC_QuanLyKhachHang : UserControl
    {
        private string _selectedId = null;
        private Guna2Panel _selectedCard = null;

        private const int BottomPaddingForShadow = 30;
        private const int FlowTopPadding = 0;

        public UC_QuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void UC_QuanLyKhachHang_Load(object sender, EventArgs e)
        {
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

            LayoutFlowUnderToolbar();
            LoadDanhSachKhachHang();
        }

        private void LayoutFlowUnderToolbar()
        {
            int left = this.Padding.Left;
            int top = guna2Panel1.Bottom;
            int right = this.Width - this.Padding.Right;
            int bottom = this.Height - this.Padding.Bottom;

            flowLayoutPanel1.Location = new Point(left, top);
            flowLayoutPanel1.Size = new Size(Math.Max(0, right - left), Math.Max(0, bottom - top));
        }

        // ====== LOAD DANH SÁCH ======
        private void LoadDanhSachKhachHang(string keyword = "")
        {
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            try
            {
                DataTable dt = BLL_KhachHang.GetAllKhachHang(); // FIX tên lớp BLL
                var rows = dt != null ? dt.AsEnumerable() : Enumerable.Empty<DataRow>();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string kw = keyword.Trim();
                    rows = rows.Where(r =>
                        (Convert.ToString(r["TenCongTy"]  ?? "").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (Convert.ToString(r["Email"]      ?? "").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (Convert.ToString(r["DienThoai"]  ?? "").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (Convert.ToString(r["MaSoThue"]   ?? "").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) || // + MST
                        (Convert.ToString(r["MaKhachHang"]?? "").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0)    // + MãKH
                    );
                }

                int count = 0;
                foreach (DataRow row in rows)
                {
                    flowLayoutPanel1.Controls.Add(TaoCardKhachHang(row));
                    count++;
                }

                if (count == 0)
                {
                    Label lbl = new Label
                    {
                        Text = "Không tìm thấy khách hàng nào.",
                        Font = new Font("Segoe UI", 10, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Dock = DockStyle.Top,
                        Height = 40,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    flowLayoutPanel1.Controls.Add(lbl);
                }

                bool stillExists = (dt != null) && dt.AsEnumerable().Any(r =>
                    ((r["KhachHangID"] ?? "").ToString()) == (_selectedId ?? ""));
                if (count == 0 || (_selectedId != null && !stillExists))
                    ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flowLayoutPanel1.ResumeLayout();
                CenterCards();
            }
        }

        // ====== TẠO CARD ======
        private Guna2Panel TaoCardKhachHang(DataRow row)
        {
            string id     = Convert.ToString(row["KhachHangID"] ?? "");
            string ten    = Convert.ToString(row["TenCongTy"]   ?? "(Chưa có tên)");
            string email  = Convert.ToString(row["Email"]       ?? "");
            string sdt    = Convert.ToString(row["DienThoai"]   ?? "");
            string mst    = Convert.ToString(row["MaSoThue"]    ?? "");
            string diachi = Convert.ToString(row["DiaChi"]      ?? "");

            var card = new Guna2Panel
            {
                Width = 320,
                Height = 185, // tăng nhẹ để đủ 4-5 dòng
                BorderRadius = 10,
                ShadowDecoration = { Enabled = true },
                FillColor = Color.White,
                Margin = new Padding(15),
                Tag = id,
                Cursor = Cursors.Hand
            };

            var lblTen = new Label
            {
                Text = string.IsNullOrWhiteSpace(ten) ? "(Chưa có tên)" : ten,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = false,
                Height = 30,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var lblMst = new Label
            {
                Text = "🧾 " + (string.IsNullOrWhiteSpace(mst) ? "(Chưa có MST)" : mst),
                Font = new Font("Segoe UI", 9),
                Height = 22,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var lblEmail = new Label
            {
                Text = "📧 " + (string.IsNullOrWhiteSpace(email) ? "(Chưa có email)" : email),
                Font = new Font("Segoe UI", 9),
                Height = 22,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var lblSdt = new Label
            {
                Text = "📞 " + (string.IsNullOrWhiteSpace(sdt) ? "(Chưa có SĐT)" : sdt),
                Font = new Font("Segoe UI", 9),
                Height = 22,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var lblDiaChi = new Label
            {
                Text = "🏠 " + (string.IsNullOrWhiteSpace(diachi) ? "(Không có địa chỉ)" : diachi),
                Font = new Font("Segoe UI", 9),
                Height = 22,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(lblDiaChi);
            card.Controls.Add(lblSdt);
            card.Controls.Add(lblEmail);
            card.Controls.Add(lblMst);  // + thêm MST
            card.Controls.Add(lblTen);

            AttachClickRecursive(card, () => SelectCard(card));
            card.DoubleClick += (s, e) => MoFormSua(id);

            if (_selectedId != null && _selectedId == id)
                ApplySelectedStyle(card, true);

            return card;
        }

        private void AttachClickRecursive(Control root, Action onClick)
        {
            root.Click += (s, e) => onClick();
            foreach (Control child in root.Controls)
                AttachClickRecursive(child, onClick);
        }

        // ====== CHỌN / BỎ CHỌN ======
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

        // ====== CĂN GIỮA ======
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
            int usedW  = perRow * itemFullW - sample.Margin.Right;
            int leftPad = Math.Max(0, (clientW - usedW) / 2);

            var p = flowLayoutPanel1.Padding;
            flowLayoutPanel1.Padding = new Padding(leftPad, p.Top, 0, p.Bottom);

            flowLayoutPanel1.PerformLayout();
        }

        // ====== TÌM KIẾM & NÚT ======
        private void ThanhTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachKhachHang(ThanhTimKiem.Text);
        }

        private void themkhachhang_Click(object sender, EventArgs e)
        {
            using (var f = new GUI_FormThemKhach())
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadDanhSachKhachHang(ThanhTimKiem.Text);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ten = LayTenKhachHang(_selectedId) ?? "(không rõ)";
            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng '" + ten + "'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    BLL_KhachHang.XoaKhachHang(_selectedId); // FIX tên lớp
                    ClearSelection();
                    LoadDanhSachKhachHang(ThanhTimKiem.Text);
                    MessageBox.Show("Đã xóa khách hàng!", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MoFormSua(_selectedId);
        }

        private void MoFormSua(string id)
        {
            using (var f = new GUI_FormThemKhach(id)) // cần overload nhận string id
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadDanhSachKhachHang(ThanhTimKiem.Text);
            }
        }

        private string LayTenKhachHang(string id)
        {
            try
            {
                var dt = BLL_KhachHang.GetAllKhachHang(); // FIX tên lớp
                if (dt == null) return null;
                var row = dt.AsEnumerable().FirstOrDefault(r =>
                              ((r["KhachHangID"] ?? "").ToString()) == (id ?? ""));
                return row != null ? row.Field<string>("TenCongTy") : null;
            }
            catch { return null; }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
