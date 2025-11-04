// GUI/GUI_FormDonHangChiTiet.cs
using BLL;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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

        private void GUI_FormDonHangChiTiet_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_tenDonHang)) lblDonHang.Text ="Tên đơn hàng: "+ _tenDonHang;
            if (!string.IsNullOrEmpty(_diaChi)) lblDiaChi.Text = "Địa chỉ: " + _diaChi;

            // ---- Chỉ styling UI (SeaGreen) ----
            ApplyGridTheme();
            StyleToolbarButtons();

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

                    Control card = MakeViTriCard(ten, viTriID);
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

        private Control MakeViTriCard(string ten, string viTriID)
        {
            // card gọn, viền SeaGreen nhạt
            Guna2Panel panel = new Guna2Panel
            {
                Width = 165,
                Height = 100,
                BorderColor = Color.FromArgb(190, 230, 220),
                BorderThickness = 1,
                BorderRadius = 12,
                Padding = new Padding(12),
                Margin = new Padding(8),
                FillColor = Color.White
            };

            Label lbl = new Label
            {
                Text = ten,
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 12f),
                Left = 12,
                Top = 8
            };
            lbl.BackColor = Color.Transparent;
            Guna2Button btn = new Guna2Button
            {
                Text = "Chọn",
                Width = 84,
                Height = 32,
                Left = 12,
                Top = 40
            };
            btn.AutoRoundedCorners = true;
            btn.BorderRadius = 15;
            btn.FillColor = Color.SeaGreen;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9f);
            //Bấm rồi nó load lại grid loại vị trí
            btn.Click += delegate
            {
                _selectedViTriID = viTriID;
                RefreshLoaiViTriGrid();
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(btn);
            return panel;
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


        // ===== Giữ nguyên các handler dưới đây =====

        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedViTriID)) return;

            try
            {
                DataTable dtAll = _bll.GetAllLoaiViTri(); // giữ nguyên
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

                    // 1) Thêm vào vị trí đang chọn (giữ nguyên)
                    string loaiViTriID = _bll.AddLoaiViTriToViTri(_selectedViTriID, tenLoai);
                    if (!string.IsNullOrEmpty(loaiViTriID))
                        RefreshLoaiViTriGrid();

                    // 2) ĐỒNG BỘ sang các vị trí còn lại (vị trí 2, 3) thuộc cùng Đơn hàng
                    try
                    {
                        DataTable dtViTriAll = _bll.GetViTriByDonHang(_donHangID);
                        foreach (DataRow rv in dtViTriAll.Rows)
                        {
                            string vtId = Convert.ToString(rv["ViTriID"]);
                            if (string.Equals(vtId, _selectedViTriID, StringComparison.OrdinalIgnoreCase))
                                continue; // bỏ qua vị trí đang chọn

                            // Tránh thêm trùng: kiểm tra xem TenLoai đã có ở vị trí này chưa
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
                                // Gọi đúng hàm sẵn có, truyền cùng tên loại để link/tạo giống như vị trí 1
                                _bll.AddLoaiViTriToViTri(vtId, tenLoai);
                            }
                        }
                        // (Tuỳ chọn) Thông báo nhẹ
                        MessageBox.Show("Đã đồng bộ loại vị trí đến mọi vị trí của đơn hàng.");
                    }
                    catch (Exception exSync)
                    {
                        // Không chặn thao tác chính nếu sync lỗi; chỉ báo nhẹ
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

        // ========= Dialog: Chọn / Tạo Loại vị trí (giữ nguyên tên lớp & thuộc tính) =========
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
                    // không cần set SelectedIndex bằng tay
                }
            }
        }
    }
}
