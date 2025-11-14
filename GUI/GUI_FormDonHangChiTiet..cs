// GUI/GUI_FormDonHangChiTiet.cs
using BLL;
using Guna.UI2.WinForms;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static GUI.GUI_Form_DangNhap;
namespace GUI
{
    public partial class GUI_FormDonHangChiTiet : Form
    {
        private readonly string _donHangID;
        private readonly string _tenDonHang;
        private readonly string _diaChi;

        private readonly BLL_ThongSoQuanTrac _bll = new BLL_ThongSoQuanTrac();
        private string _selectedViTriID;

        public GUI_FormDonHangChiTiet()
        {
            InitializeComponent();
        }

        public GUI_FormDonHangChiTiet(string donHangID, string tenDonHang = null, string diaChi = null) : this()
        {
            _donHangID = donHangID;
            _tenDonHang = !string.IsNullOrEmpty(tenDonHang) ? tenDonHang : ("Đơn hàng: " + donHangID);
            _diaChi = !string.IsNullOrEmpty(diaChi) ? diaChi : "(Chưa rõ địa chỉ)";
        }

        // Expose DonHangID để form khác kiểm tra
        public string DonHangID { get { return _donHangID; } }

        // Cho phép form khác đẩy địa chỉ mới và reload thẻ vị trí
        public void OnKhachHangDiaChiUpdated(string newDiaChi)
        {
            if (!string.IsNullOrWhiteSpace(newDiaChi))
            {
                lblDiaChi.Text = "Địa chỉ: " + newDiaChi;
            }
            // reload danh sách vị trí để phản ánh địa chỉ mới trong DB
            ReloadViTriAndGrid(_selectedViTriID);
        }

        private void GUI_FormDonHangChiTiet_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_tenDonHang)) lblDonHang.Text = "Tên đơn hàng: " + _tenDonHang;
            if (!string.IsNullOrEmpty(_diaChi)) lblDiaChi.Text = "Địa chỉ: " + _diaChi;

            // Thử lấy địa chỉ khách hàng/ vị trí từ DB để ghi đè nếu có
            LoadDiaChiKhachHang();

            // ---- Chỉ styling UI (SeaGreen) ----
            ApplyGridTheme();
            StyleToolbarButtons();
            btnThemChiTieu.Enabled = IsKeHoach();
            btnThemChiTieu.Visible = IsKeHoach();
            try
            {
                // tạo đủ 3 vị trí nếu chưa có (giữ nguyên tên hàm)
                _bll.EnsureViTriAndThongSo(_donHangID);

                // nạp danh sách vị trí
                DataTable dtViTri = _bll.GetViTriByDonHang(_donHangID);
                pnlViTri.Controls.Clear();
                _selectedViTriID = null;

                foreach (DataRow r in dtViTri.Rows)
                {
                    string viTriID = Convert.ToString(r["ViTriID"]);
                    string ten = dtViTri.Columns.Contains("TenViTri") ? Convert.ToString(r["TenViTri"]) : viTriID;
                    string diaChi = dtViTri.Columns.Contains("DiaChi") ? Convert.ToString(r["DiaChi"]) : string.Empty;
                    Control card = MakeViTriCard(ten, viTriID, diaChi);
                    pnlViTri.Controls.Add(card);

                    if (_selectedViTriID == null)
                        _selectedViTriID = viTriID;
                }

                // không seed loại vị trí mặc định
                RefreshLoaiViTriGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Tìm địa chỉ ưu tiên theo Khách hàng của đơn hàng; nếu không có, fallback theo địa chỉ của vị trí đầu tiên có địa chỉ
        private void LoadDiaChiKhachHang()
        {
            try
            {
                string khId = null;

                // 1) Lấy DonHang -> IDKhachHang hoặc HopDongID
                var dtDH = BLL_DonHang.GetDonHangByID(_donHangID);
                if (dtDH != null && dtDH.Rows.Count > 0)
                {
                    var row = dtDH.Rows[0];
                    if (dtDH.Columns.Contains("IDKhachHang"))
                        khId = Convert.ToString(row["IDKhachHang"]);

                    if (string.IsNullOrWhiteSpace(khId) && dtDH.Columns.Contains("HopDongID"))
                    {
                        string hopDongId = Convert.ToString(row["HopDongID"]);
                        var dtHD = BLL_DonHang.GetAllHopDong();
                        if (dtHD != null && dtHD.Columns.Contains("HopDongID") && dtHD.Columns.Contains("KhachHangID"))
                        {
                            string safe = (hopDongId ?? "").Replace("'", "''");
                            DataRow[] r = dtHD.Select("HopDongID = '" + safe + "'");
                            if (r.Length > 0) khId = Convert.ToString(r[0]["KhachHangID"]);
                        }
                    }
                }

                // 2) Với KhachHangID -> lấy DiaChi khách hàng
                string dc = null;
                if (!string.IsNullOrWhiteSpace(khId))
                {
                    var dtKH = BLL_KhachHang.GetAllKhachHang();
                    if (dtKH != null && dtKH.Columns.Contains("KhachHangID") && dtKH.Columns.Contains("DiaChi"))
                    {
                        string safeKh = khId.Replace("'", "''");
                        DataRow[] rkh = dtKH.Select("KhachHangID = '" + safeKh + "'");
                        if (rkh.Length > 0) dc = Convert.ToString(rkh[0]["DiaChi"]);
                    }
                }

                // 3) Fallback: lấy từ địa chỉ của vị trí bất kỳ thuộc đơn hàng
                if (string.IsNullOrWhiteSpace(dc))
                {
                    var dtVT = _bll.GetViTriByDonHang(_donHangID);
                    if (dtVT != null && dtVT.Columns.Contains("DiaChi"))
                    {
                        foreach (DataRow vr in dtVT.Rows)
                        {
                            var s = Convert.ToString(vr["DiaChi"]);
                            if (!string.IsNullOrWhiteSpace(s)) { dc = s; break; }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(dc))
                    lblDiaChi.Text = "Địa chỉ: " + dc;
            }
            catch
            {
                // ignore lỗi lấy địa chỉ để không chặn UI
            }
        }

        // ---- UI only: grid theme SeaGreen ----
        private void ApplyGridTheme()
        {
            dgvLoaiViTri.EnableHeadersVisualStyles = false;
            dgvLoaiViTri.ColumnHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            dgvLoaiViTri.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLoaiViTri.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5f);
            dgvLoaiViTri.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 252, 249);
            dgvLoaiViTri.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 220);
            dgvLoaiViTri.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvLoaiViTri.GridColor = Color.FromArgb(220, 240, 235);
        }

        // ---- UI only: pill buttons ----
        private void StyleToolbarButtons()
        {
            void Pill(Guna2Button b, Color fill)
            {
                b.AutoRoundedCorners = true;
                b.BorderRadius = 15;
                b.FillColor = fill;
                b.ForeColor = Color.White;
                b.Font = new Font("Segoe UI", 9f);
                b.HoverState.FillColor = ControlPaint.Light(fill, 0.12f);
                b.PressedColor = ControlPaint.Dark(fill, 0.05f);
            }

            Pill(btnThemLoai, Color.SeaGreen);
            Pill(btnXoaLoai, Color.FromArgb(200, 60, 60));
            Pill(btnOpenThongSo, Color.ForestGreen);
        }

        private Control MakeViTriCard(string ten, string viTriID, string diaChi)
        {
            // Card chính
            var card = new Guna2Panel
            {
                Width = 200,
                Height = 140,
                BorderColor = Color.FromArgb(208, 235, 228),
                BorderThickness = 1,
                BorderRadius = 14,
                Padding = new Padding(12, 12, 12, 8),
                Margin = new Padding(10),
                FillColor = Color.White
            };
            // Bóng đổ nhẹ cho card
            card.ShadowDecoration.Enabled = true;
            card.ShadowDecoration.BorderRadius = 14;
            card.ShadowDecoration.Depth = 4;
            card.ShadowDecoration.Color = Color.FromArgb(190, 230, 220);

            // Tiêu đề (Tên vị trí)
            var lblTitle = new Label
            {
                Text = ten,
                AutoSize = false,
                Width = card.Width - 24,
                Height = 24,
                Left = 12,
                Top = 10,
                Font = new Font("Segoe UI Semibold", 11f),
                ForeColor = Color.FromArgb(30, 50, 50)
            };
            lblTitle.AutoEllipsis = true;
            lblTitle.BackColor = Color.Transparent;

            // Địa chỉ (của vị trí)
            var lblAddr = new Label
            {
                Text = string.IsNullOrWhiteSpace(diaChi) ? "Chưa xác định" : diaChi,
                AutoSize = false,
                Width = card.Width - 24,
                Height = 20,
                Left = 12,
                Top = lblTitle.Bottom + 2,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.DimGray,
                BackColor = Color.Transparent
            };
            lblAddr.AutoEllipsis = true;

            // Footer chứa 2 nút (Dock bottom để không bị lệch)
            var footer = new Guna2Panel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                Padding = new Padding(12, 6, 12, 10),
                FillColor = Color.Transparent
            };

            var btnChon = new Guna2Button
            {
                Text = "Chọn",
                AutoRoundedCorners = true,
                BorderRadius = 16,
                Width = 60,
                Height = 34,
                FillColor = Color.SeaGreen,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.Transparent
            };
            btnChon.Dock = DockStyle.Left;

            var btnSua = new Guna2Button
            {
                Text = "Sửa",
                AutoRoundedCorners = true,
                BorderRadius = 16,
                Width = 60,
                Height = 34,
                FillColor = Color.FromArgb(90, 130, 255), // xanh nhạt dễ nhìn
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.Transparent
            };
            btnSua.Dock = DockStyle.Right;

            // Hành vi nút
            btnChon.Click += delegate
            {
                _selectedViTriID = viTriID;
                RefreshLoaiViTriGrid();
            };

            btnSua.Click += delegate
            {
                
                string newDiaChi = Interaction.InputBox("Nhập địa chỉ mới:", "Đổi địa chỉ", diaChi);

                if (!string.IsNullOrWhiteSpace(ten) || !string.IsNullOrWhiteSpace(newDiaChi))
                {
                    bool ok = _bll.UpdateTenVaDiaChiViTri(viTriID, ten, newDiaChi);
                    if (ok)
                    {
                        ReloadViTriAndGrid(viTriID);
                        MessageBox.Show("Cập nhật xong.");
                    }
                    else
                    {
                        MessageBox.Show("Không cập nhật được.");
                    }
                }
            };

            // Lắp ráp
            footer.Controls.Add(btnChon);
            footer.Controls.Add(btnSua);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblAddr);
            card.Controls.Add(footer);
            return card;
        }

        private void RefreshLoaiViTriGrid()
        {
            if (string.IsNullOrEmpty(_selectedViTriID)) return;
            DataTable dt = _bll.GetLoaiViTriByViTri(_selectedViTriID);
            dgvLoaiViTri.DataSource = dt;

            if (dgvLoaiViTri.Columns.Contains("LoaiViTriID"))
                dgvLoaiViTri.Columns["LoaiViTriID"].Visible = false;

            if (dgvLoaiViTri.Columns.Contains("TenLoai"))
            {
                string tenViTri = "Vị trí";
                try
                {
                    var vt = _bll.GetViTriByDonHang(_donHangID);
                    int i = 1;
                    foreach (DataRow r in vt.Rows)
                    {
                        if (Convert.ToString(r["ViTriID"]) == _selectedViTriID)
                        { tenViTri = $"Vị trí {i}"; break; }
                        i++;
                    }
                }
                catch { }
                dgvLoaiViTri.Columns["TenLoai"].HeaderText = $"{tenViTri} – Loại đo";
            }
        }

        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedViTriID)) return;

            try
            {
                DataTable dtAll = _bll.GetAllLoaiViTri();
                using (SelectLoaiViTriDialog dlg = new SelectLoaiViTriDialog(dtAll))
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK) return;

                    string tenLoai;
                    if (dlg.Mode == SelectLoaiViTriDialog.SelectMode.UseExisting)
                    {
                        tenLoai = dlg.SelectedTenLoai;
                        if (string.IsNullOrWhiteSpace(tenLoai))
                        {
                            MessageBox.Show("Chưa chọn loại có sẵn.");
                            return;
                        }
                    }
                    else
                    {
                        tenLoai = (dlg.NewTenLoai ?? "").Trim();
                        if (string.IsNullOrWhiteSpace(tenLoai))
                        {
                            MessageBox.Show("Nhập tên loại mới.");
                            return;
                        }
                    }

                    string loaiViTriID = _bll.AddLoaiViTriToViTri(_selectedViTriID, tenLoai);
                    if (!string.IsNullOrEmpty(loaiViTriID))
                        RefreshLoaiViTriGrid();

                    try
                    {
                        DataTable dtViTriAll = _bll.GetViTriByDonHang(_donHangID);
                        foreach (DataRow rv in dtViTriAll.Rows)
                        {
                            string vtId = Convert.ToString(rv["ViTriID"]);
                            if (string.Equals(vtId, _selectedViTriID, StringComparison.OrdinalIgnoreCase))
                                continue;

                            bool existed = false;
                            DataTable dtLoaiOfVt = _bll.GetLoaiViTriByViTri(vtId);
                            if (dtLoaiOfVt != null && dtLoaiOfVt.Columns.Contains("TenLoai"))
                            {
                                foreach (DataRow lr in dtLoaiOfVt.Rows)
                                {
                                    string ten = Convert.ToString(lr["TenLoai"]);
                                    if (!string.IsNullOrEmpty(ten) &&
                                        ten.Equals(tenLoai, StringComparison.OrdinalIgnoreCase))
                                    {
                                        existed = true;
                                        break;
                                    }
                                }
                            }

                            if (!existed)
                            {
                                _bll.AddLoaiViTriToViTri(vtId, tenLoai);
                            }
                        }
                        // synchronization completed silently (removed debug popup)
                    }
                    catch (Exception exSync)
                    {
                        MessageBox.Show("Đồng bộ sang các vị trí khác gặp lỗi: " + exSync.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm loại vị trí: " + ex.Message);
            }
        }

        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedViTriID) || dgvLoaiViTri.CurrentRow == null) return;

            DataGridViewRow row = dgvLoaiViTri.CurrentRow;
            string loaiViTriID = row.Cells["LoaiViTriID"] != null ? Convert.ToString(row.Cells["LoaiViTriID"].Value) : null;
            string tenLoai = row.Cells["TenLoai"] != null ? Convert.ToString(row.Cells["TenLoai"].Value) : null;
            if (string.IsNullOrEmpty(loaiViTriID)) return;

            if (MessageBox.Show("Xóa loại vị trí '" + tenLoai + "' khỏi vị trí này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (_bll.DeleteLoaiViTriFromViTri(_selectedViTriID, loaiViTriID))
                        RefreshLoaiViTriGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa loại vị trí: " + ex.Message);
                }
            }
        }

        private void btnOpenThongSo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedViTriID) || dgvLoaiViTri.CurrentRow == null) return;

            DataGridViewRow row = dgvLoaiViTri.CurrentRow;
            string loaiViTriID = row.Cells["LoaiViTriID"] != null ? Convert.ToString(row.Cells["LoaiViTriID"].Value) : null;
            string tenLoai = row.Cells["TenLoai"] != null ? Convert.ToString(row.Cells["TenLoai"].Value) : null;
            if (string.IsNullOrEmpty(loaiViTriID)) return;

            using (GUI_FormThongSoTheoLoai f = new GUI_FormThongSoTheoLoai(_donHangID, _selectedViTriID, loaiViTriID,
                                                                           _tenDonHang, _diaChi, tenLoai))
            {
                f.ShowDialog(this);
            }
        }

        private void dgvLoaiViTri_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            btnOpenThongSo_Click(sender, EventArgs.Empty);
        }

        private sealed class SelectLoaiViTriDialog : Form
        {
            public enum SelectMode { UseExisting, CreateNew }
            public SelectMode Mode { get { return tab.SelectedIndex == 0 ? SelectMode.UseExisting : SelectMode.CreateNew; } }

            public string SelectedTenLoai
            {
                get
                {
                    DataRowView drv = cboExisting.SelectedItem as DataRowView;
                    return drv != null ? Convert.ToString(drv["TenLoai"]) : null;
                }
            }
            public string NewTenLoai { get { return txtNew.Text != null ? txtNew.Text.Trim() : null; } }

            private readonly DataTable _src;
            private readonly TabControl tab = new TabControl();
            private readonly ComboBox cboExisting = new ComboBox();
            private readonly TextBox txtNew = new TextBox();

            public SelectLoaiViTriDialog(DataTable allLoai)
            {
                _src = allLoai ?? new DataTable();
                this.Text = "Thêm loại vị trí";
                this.Width = 420;
                this.Height = 210;
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false; this.MinimizeBox = false;

                tab.Dock = DockStyle.Top; tab.Height = 120;

                TabPage page1 = new TabPage("Chọn có sẵn");
                TabPage page2 = new TabPage("Tạo mới");

                Label l1 = new Label(); l1.Left = 12; l1.Top = 0; l1.AutoSize = true; l1.Text = "Chọn loại vị trí trong CSDL:";
                cboExisting.Left = 12; cboExisting.Top = 20; cboExisting.Width = 360; cboExisting.DropDownStyle = ComboBoxStyle.DropDownList;
                page1.Controls.Add(l1); page1.Controls.Add(cboExisting);

                Label l2 = new Label(); l2.Left = 12; l2.Top = 0; l2.AutoSize = true; l2.Text = "Nhập tên loại mới:";
                txtNew.Left = 12; txtNew.Top = 20; txtNew.Width = 360;
                page2.Controls.Add(l2); page2.Controls.Add(txtNew);

                tab.TabPages.Add(page1);
                tab.TabPages.Add(page2);

                Button ok = new Button(); ok.Text = "OK"; ok.Left = 220; ok.Top = 130; ok.Width = 80; ok.DialogResult = DialogResult.OK;
                Button can = new Button(); can.Text = "Hủy"; can.Left = 312; can.Top = 130; can.Width = 80; can.DialogResult = DialogResult.Cancel;

                this.Controls.Add(tab);
                this.Controls.Add(ok);
                this.Controls.Add(can);
                this.AcceptButton = ok; this.CancelButton = can;

                if (_src.Rows.Count > 0)
                {
                    cboExisting.DataSource = _src;
                    cboExisting.DisplayMember = "TenLoai";
                    cboExisting.ValueMember = "LoaiViTriID";
                }
            }
        }
        private void ReloadViTriAndGrid(string preferViTriID = null)
        {
            DataTable dtViTri = _bll.GetViTriByDonHang(_donHangID);
            pnlViTri.Controls.Clear();
            string firstId = null;

            foreach (DataRow r in dtViTri.Rows)
            {
                string id = Convert.ToString(r["ViTriID"]);
                string ten = dtViTri.Columns.Contains("TenViTri") ? Convert.ToString(r["TenViTri"]) : id;
                string dc = dtViTri.Columns.Contains("DiaChi") ? Convert.ToString(r["DiaChi"]) : string.Empty;

                pnlViTri.Controls.Add(MakeViTriCard(ten, id, dc));
                if (firstId == null) firstId = id;
            }

            _selectedViTriID = !string.IsNullOrEmpty(preferViTriID) ? preferViTriID :
                               string.IsNullOrEmpty(_selectedViTriID) ? firstId : _selectedViTriID;

            RefreshLoaiViTriGrid();
        }
        private bool IsKeHoach()
        {
            var u = Session.CurrentUser;
            return u != null &&
                   string.Equals(u.PhongBanID, "PB002", StringComparison.OrdinalIgnoreCase)||string.Equals(u.PhongBanID, "PB006", StringComparison.OrdinalIgnoreCase);
        }
        private void btnThemChiTieu_Click(object sender, EventArgs e)
        {
            using (var f = new GUI_FormThemLoaiChiTieu())
    {
        if (f.ShowDialog(this) == DialogResult.OK && f.Saved)
        {
            // Ở form này không có combobox Loại chỉ tiêu,
            // nên chỉ cần báo đã thêm xong là đủ.
            if (f.NewLoaiChiTieu != null)
            {
                MessageBox.Show("Đã thêm chỉ tiêu mới: " + f.NewLoaiChiTieu.TenChiTieu,
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã thêm chỉ tiêu mới.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
        }
    }
}
