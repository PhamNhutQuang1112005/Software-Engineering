using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class UC_QuanLyDonHang : UserControl
    {  private string _selectedId = null;
        private Guna2Panel _selectedCard = null;
        public UC_QuanLyDonHang()
        {
            InitializeComponent();
          

        }

        private void UC_QuanLyDonHang_Load(object sender, System.EventArgs e)
        {
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.AutoScroll = true;

            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LoadDanhSachHopDong();

        }


        // ============ LOAD DANH SÁCH ============
       private void LoadDanhSachHopDong()
{
    flowLayoutPanel1.Controls.Clear();

    DataTable dtHopDong = BLL_HopDong.GetAllHopDong();
    DataTable dtKhachHang = BLL_KhachHang.GetAllKhachHang();
    DataTable dtDonHang = BLL_DonHang.GetAllDonHang();
    DataTable dtTrangThai = BLL_DonHang.GetAllTrangThaiDonHang();

    foreach (DataRow row in dtHopDong.Rows)
    {
        if (Convert.ToInt32(row["IsDeleted"]) == 1) continue;

        string maHopDong = row["MaHopDong"]?.ToString() ?? "(Không rõ)";
        string khachHangID = row["KhachHangID"]?.ToString();
        string trangThaiHopDong = row["TrangThai"]?.ToString() ?? "(Không rõ)";
        string ghiChu = string.IsNullOrWhiteSpace(row["GhiChu"]?.ToString()) ? "(Không có mô tả)" : row["GhiChu"].ToString();

        // === Lấy tên khách hàng ===
        string tenKhachHang = "(Không rõ)";
        if (!string.IsNullOrEmpty(khachHangID))
        {
            var rowKH = dtKhachHang.AsEnumerable()
                .FirstOrDefault(k => k["KhachHangID"].ToString() == khachHangID);
            if (rowKH != null)
                tenKhachHang = rowKH["TenCongTy"]?.ToString() ?? "(Không có tên)";
        }

        // === Lấy thông tin đơn hàng theo HopDongID ===
        var rowDH = dtDonHang.AsEnumerable()
            .FirstOrDefault(d => d["HopDongID"].ToString() == row["HopDongID"].ToString());

        string maDonHang = "(Không có)";
        string tenTienDo = "(Không rõ)";
        if (rowDH != null)
        {
            maDonHang = rowDH["MaDonHang"]?.ToString() ?? "(Không có mã)";

            string trangThaiID = rowDH["TrangThaiID"]?.ToString();
            if (!string.IsNullOrEmpty(trangThaiID))
            {
                var rowTrangThai = dtTrangThai.AsEnumerable()
                    .FirstOrDefault(t => t["TrangThaiID"].ToString() == trangThaiID);
                if (rowTrangThai != null)
                    tenTienDo = rowTrangThai["TenTrangThai"]?.ToString() ?? "(Không rõ)";
            }
        }

        // === Tạo card hiển thị ===
        Guna2Panel card = new Guna2Panel
        {
            Width = 420,
            Height = 210,
            BorderRadius = 12,
            FillColor = Color.White,
            ShadowDecoration = { Enabled = true },
            Margin = new Padding(25),
            Cursor = Cursors.Hand
        };


        Label lblMa = new Label
        {
            Text = $"📄 Hợp đồng: {maHopDong}",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            Dock = DockStyle.Top,
            Height = 30,
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label lblKH = new Label
        {
            Text = $"👤 Khách hàng: {tenKhachHang}",
            Font = new Font("Segoe UI", 10),
            Dock = DockStyle.Top,
            Height = 25,
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label lblDH = new Label
        {
            Text = $"📦 Đơn hàng: {maDonHang}",
            Font = new Font("Segoe UI", 10),
            Dock = DockStyle.Top,
            Height = 25,
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label lblTienDo = new Label
        {
            Text = $"⏳ Tiến độ: {tenTienDo}",
            Font = new Font("Segoe UI", 10),
            Dock = DockStyle.Top,
            Height = 25,
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label lblTT = new Label
        {
            Text = $"⚙️ Trạng thái hợp đồng: {trangThaiHopDong}",
            Font = new Font("Segoe UI", 9),
            Dock = DockStyle.Top,
            Height = 25,
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label lblGC = new Label
        {
            Text = $"📑 {ghiChu}",
            Font = new Font("Segoe UI", 9, FontStyle.Italic),
            ForeColor = Color.Gray,
            Dock = DockStyle.Top,
            Height = 25,
            TextAlign = ContentAlignment.MiddleCenter
        };

        card.Controls.Add(lblGC);
        card.Controls.Add(lblTT);
        card.Controls.Add(lblTienDo);
        card.Controls.Add(lblDH);
        card.Controls.Add(lblKH);
        card.Controls.Add(lblMa);

        flowLayoutPanel1.Controls.Add(card);
    }

    CenterCards();
}


        private Guna2Panel TaoCardDonHang(DataRow row, DataTable hopDong, DataTable trangThai)
        {
            string id = Convert.ToString(row["DonHangID"]);
            string maDonHang = Convert.ToString(row["MaDonHang"]);
            string hopDongID = Convert.ToString(row["HopDongID"]);
            string trangThaiID = Convert.ToString(row["TrangThaiID"]);
            string moTa = Convert.ToString(row["GhiChu"]);
           

            string tenHopDong = hopDong.AsEnumerable()
                .FirstOrDefault(h => h["HopDongID"].ToString() == hopDongID)?["HopDongID"]?.ToString() ?? "(Không rõ)";
            string tenTrangThai = trangThai.AsEnumerable()
                .FirstOrDefault(t => t["TrangThaiID"].ToString() == trangThaiID)?["TenTrangThai"]?.ToString() ?? "(Không rõ)";

            var card = new Guna2Panel
            {
                Width = 320,
                Height = 185,
                BorderRadius = 10,
                ShadowDecoration = { Enabled = true },
                FillColor = Color.White,
                Margin = new Padding(15),
                Tag = id,
                Cursor = Cursors.Hand
            };

            var lblTen = new Label
            {
                Text = "Mã: " + maDonHang,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblHopDong = new Label
            {
                Text = "📄 Hợp đồng: " + tenHopDong,
                Font = new Font("Segoe UI", 9),
                Height = 24,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblTrangThai = new Label
            {
                Text = "⚙️ Trạng thái: " + tenTrangThai,
                Font = new Font("Segoe UI", 9),
                Height = 24,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

           

            var lblMoTa = new Label
            {
                Text = "📝 " + (string.IsNullOrWhiteSpace(moTa) ? "(Không có mô tả)" : moTa),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                Height = 24,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(lblMoTa);
          
            card.Controls.Add(lblTrangThai);
            card.Controls.Add(lblHopDong);
            card.Controls.Add(lblTen);

            AttachClickRecursive(card, () => SelectCard(card));

            return card;
        }
        private void SelectCard(Guna2Panel card)
        {
            if (_selectedCard == card) return;
            if (_selectedCard != null) ApplySelectedStyle(_selectedCard, false);

            _selectedCard = card;
            _selectedId = card.Tag?.ToString();
            ApplySelectedStyle(card, true);
        }

        private void ApplySelectedStyle(Guna2Panel card, bool selected)
        {
            if (selected)
            {
                card.FillColor = Color.FromArgb(240, 250, 255);
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
        private void AttachClickRecursive(Control root, Action onClick)
        {
            root.Click += (s, e) => onClick();
            foreach (Control child in root.Controls)
                AttachClickRecursive(child, onClick);
        }

        private void guna2Button1_Click(object sender, System.EventArgs e)
        {
            GUI_FormThemDonHang gUI_FormThemDonHang = new GUI_FormThemDonHang();
            gUI_FormThemDonHang.ShowDialog();
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

        private void guna2HtmlLabel9_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel12_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel13_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel14_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel15_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel16_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel17_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel18_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel19_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel20_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel21_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel22_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel23_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel24_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, System.EventArgs e)
        {

        }

        private void label2_Click(object sender, System.EventArgs e)
        {

        }

        private void label4_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, System.EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, System.EventArgs e)
        {

        }

        private void themdonhang_Click(object sender, System.EventArgs e)
        {
            GUI_FormThemDonHang gUI_Form_them_donhang = new GUI_FormThemDonHang();
            gUI_Form_them_donhang.ShowDialog();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy tên đơn hàng (hoặc mã đơn hàng) để hiển thị
            var tenDon = LayTenDonHang(_selectedId) ?? "(không rõ)";

            if (MessageBox.Show("Bạn có chắc muốn xóa đơn hàng '" + tenDon + "'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // Gọi hàm trong lớp BLL để xóa đơn hàng
                    BLL_DonHang.XoaDonHang(_selectedId);

                    // Xóa lựa chọn hiện tại + load lại danh sách
                    

                    MessageBox.Show("Đã xóa đơn hàng!", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa đơn hàng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private string LayTenDonHang(string donHangID)
        {
            try
            {
                // Lấy toàn bộ bảng đơn hàng từ BLL
                DataTable dt = BLL_DonHang.GetAllDonHang();

                // Tìm dòng có DonHangID trùng với id đang chọn
                DataRow row = dt.AsEnumerable()
                    .FirstOrDefault(r => r["DonHangID"].ToString() == donHangID);

                if (row != null)
                {
                    // Trả về mã hoặc tên đơn hàng
                    return row["MaDonHang"]?.ToString() ?? "(Không có mã)";
                }

                return "(Không tìm thấy)";
            }
            catch
            {
                return "(Lỗi khi lấy tên)";
            }
        }

    }
}
