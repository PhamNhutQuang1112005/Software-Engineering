using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class GUI_FormThongSoDonHang_V2 : Form
    {
        private readonly string _donHangID;
        private readonly BLL_ThongSoQuanTrac _bll = new BLL_ThongSoQuanTrac();
        private readonly BLL_TaiKhoan _bllUser = new BLL_TaiKhoan();

        // Chống vòng lặp & binding
        private bool _isBinding = false;
        private bool _suppressComboEvents = false;

        // Cột được phép sửa trên dòng đang chọn
        private static readonly string[] EditableCols = { "GiaTri", "GiaTriSo", "GiaTriQuyChuan", "KetLuan" };

        // ===== Theme SeaGreen =====
        private static class EnvTheme
        {
            public static readonly Color Primary      = Color.SeaGreen;             // #2E8B57
            public static readonly Color PrimaryDark  = Color.ForestGreen;
            public static readonly Color Accent       = Color.MediumSeaGreen;
            public static readonly Color Soft         = Color.FromArgb(244, 251, 247);
            public static readonly Color HeaderText   = Color.White;
            public static readonly Color RowAlt       = Color.FromArgb(240, 248, 244);
            public static readonly Color RowHover     = Color.FromArgb(226, 243, 236);
            public static readonly Color GridLine     = Color.FromArgb(197, 224, 212);
        }

        public GUI_FormThongSoDonHang_V2(string donHangID)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _donHangID = donHangID;
        }

        private void GUI_FormThongSoDonHang_V2_Load(object sender, EventArgs e)
        {
            lblTieuDe.Text = $"Chi tiết Đơn Hàng: {_donHangID}";
            LoadComboboxData();
            WireComboEvents();
            LoadTabs();
            ApplySeagreenTheme();
        }

        // ============== THEME ==============
        private void ApplySeagreenTheme()
        {
            this.Text = "Thông số môi trường – " + _donHangID;

            // Header panel
            headerPanel.FillColor  = EnvTheme.Primary;
            headerPanel.FillColor2 = EnvTheme.PrimaryDark;
            headerPanel.FillColor3 = EnvTheme.Primary;
            headerPanel.FillColor4 = EnvTheme.PrimaryDark;

            lblTieuDe.ForeColor = EnvTheme.HeaderText;

            // Card
            cardPanel.FillColor = Color.White;
            cardPanel.BorderColor = EnvTheme.Primary;
            sectionTitle.ForeColor = EnvTheme.Primary;

            // Combos
            var combos = new[] { cboLoaiChiTieu, cboDonVi, cboLoaiPhanTich, cboNguoiPhuTrach };
            foreach (var cb in combos)
            {
                cb.BorderRadius = 12;
                cb.BorderColor = EnvTheme.Primary;
                cb.FocusedColor = EnvTheme.Primary;
                cb.FocusedState.BorderColor = EnvTheme.Primary;
            }

            // Buttons
            StylePillButton(btnThemChiTieu, EnvTheme.Accent, Color.White);
            StylePillButton(btnLuuThayDoi, EnvTheme.Primary, Color.White);
            StylePillButton(btnXoa, Color.IndianRed, Color.White);
        }

        private static void StylePillButton(Guna2Button btn, Color bg, Color fg)
        {
            btn.BorderRadius = 18;
            btn.FillColor = bg;
            btn.ForeColor = fg;
            btn.HoverState.FillColor = ControlPaint.Light(bg, 0.15f);
            btn.PressedColor = ControlPaint.Dark(bg, 0.05f);
        }

        // ============== Combobox ==============
        private void LoadComboboxData()
        {
            var dtLCT = _bll.GetAllLoaiChiTieu();
            cboLoaiChiTieu.DataSource = dtLCT;
            cboLoaiChiTieu.DisplayMember = "TenChiTieu";
            cboLoaiChiTieu.ValueMember   = "LoaiChiTieuID";
            cboLoaiChiTieu.DropDownStyle = ComboBoxStyle.DropDownList;

            var dtDV = _bll.GetAllDonVi();
            cboDonVi.DataSource = dtDV;
            cboDonVi.DisplayMember = "TenDonVi";
            cboDonVi.ValueMember   = "DonViID";
            cboDonVi.DropDownStyle = ComboBoxStyle.DropDownList;

            var dtLPT = _bll.GetAllLoaiPhanTich();
            cboLoaiPhanTich.DataSource = dtLPT;
            cboLoaiPhanTich.DisplayMember = "TenLoai";
            cboLoaiPhanTich.ValueMember   = "LoaiPhanTichID";
            cboLoaiPhanTich.DropDownStyle = ComboBoxStyle.DropDownList;

            var dtUsers = _bllUser.LayTatCaNguoiDung();
            cboNguoiPhuTrach.DataSource = dtUsers;
            cboNguoiPhuTrach.DisplayMember = "HoVaTen";
            cboNguoiPhuTrach.ValueMember   = "NguoiDungID";
            cboNguoiPhuTrach.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void WireComboEvents()
        {
            cboLoaiChiTieu.SelectedValueChanged   += (s, e) => ApplyCombosToCurrentRow(GetActiveGrid());
            cboDonVi.SelectedValueChanged         += (s, e) => ApplyCombosToCurrentRow(GetActiveGrid());
            cboLoaiPhanTich.SelectedValueChanged  += (s, e) => ApplyCombosToCurrentRow(GetActiveGrid());
            cboNguoiPhuTrach.SelectedValueChanged += (s, e) => ApplyCombosToCurrentRow(GetActiveGrid());
        }

        // ============== Tabs & Grid ==============
        private void LoadTabs()
        {
            _bll.EnsureViTriAndThongSo(_donHangID);

            tabViTri.TabPages.Clear();
            var dtViTri = _bll.GetViTriByDonHang(_donHangID);

            foreach (DataRow row in dtViTri.Rows)
            {
                string viTriID = Convert.ToString(row["ViTriID"]);
                string tenViTri = Convert.ToString(row["TenViTri"]);

                var tab = new TabPage(tenViTri) { Tag = viTriID };

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = false,
                    AllowUserToAddRows = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    Name = "dgvThongSo",
                    EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2,
                    BackgroundColor = Color.White
                };

                dgv.DataBindingComplete += (s, e) => ApplyGridPresentation((DataGridView)s);
                dgv.DataError += (s, e) => { e.Cancel = true; };

                dgv.SelectionChanged += (s, e) =>
                {
                    if (_isBinding) return;
                    var g = (DataGridView)s;
                    SyncCombosFromGrid(g);
                    LockRowsExceptCurrent(g);
                };
                dgv.CellClick += (s, e) =>
                {
                    if (_isBinding) return;
                    var g = (DataGridView)s;
                    SyncCombosFromGrid(g);
                    LockRowsExceptCurrent(g);
                };

                dgv.CellBeginEdit += (s, e) =>
                {
                    var g = (DataGridView)s;
                    if (g.CurrentRow == null || e.RowIndex != g.CurrentRow.Index) { e.Cancel = true; return; }
                    string colName = g.Columns[e.ColumnIndex].Name;
                    if (!EditableCols.Contains(colName)) e.Cancel = true;
                };

                tab.Controls.Add(dgv);
                tabViTri.TabPages.Add(tab);

                RebindGridFor(viTriID);
            }

            tabViTri.SelectedIndexChanged += (s, e) => RefreshActiveTab();
        }

        private static DataGridViewCell GetFirstEditableVisibleCell(DataGridViewRow row)
        {
            var g = row.DataGridView;
            foreach (DataGridViewColumn col in g.Columns)
            {
                if (!col.Visible) continue;
                var cell = row.Cells[col.Index];
                if (!cell.ReadOnly && col.Visible) return cell;
            }
            return null;
        }
        private static DataGridViewCell GetFirstVisibleCell(DataGridViewRow row)
        {
            var g = row.DataGridView;
            foreach (DataGridViewColumn col in g.Columns)
            {
                if (!col.Visible) continue;
                return row.Cells[col.Index];
            }
            return null;
        }
        private static void SafeSetCurrentCell(DataGridView g, DataGridViewRow row)
        {
            if (g == null || row == null) return;
            var cell = GetFirstEditableVisibleCell(row) ?? GetFirstVisibleCell(row);
            if (cell != null && cell.Visible) g.CurrentCell = cell;
        }

        private void ApplyGridPresentation(DataGridView dgv)
        {
            // Ẩn khóa/ID + ẩn TenThongSo
            string[] hidden = { "TenThongSo","ViTriID","LoaiChiTieuID","DonViID","LoaiPhanTichID","NguoiPhanTichID","ThauPhuID" };
            foreach (var col in hidden)
                if (dgv.Columns.Contains(col)) dgv.Columns[col].Visible = false;

            // Header Việt hóa
            var headerMap = new (string name, string text)[]
            {
                ("TenLoaiChiTieu","Chỉ tiêu"),
                ("GiaTri","Giá trị"),
                ("TenDonVi","Đơn vị"),
                ("GiaTriQuyChuan","Giới hạn"),
                ("KetLuan","Trạng thái"),
                ("GiaTriSo","Giá trị số"),
                ("TenLoaiPhanTich","Phòng phân tích"),
                ("TenNguoiPhanTich","Người phụ trách"),
                ("TenThauPhu","Thầu phụ")
            };
            foreach (var h in headerMap)
                if (dgv.Columns.Contains(h.name)) dgv.Columns[h.name].HeaderText = h.text;

            // Thứ tự cột chính
            string[] order = { "TenLoaiChiTieu","GiaTri","TenDonVi","GiaTriQuyChuan","KetLuan","GiaTriSo","TenLoaiPhanTich","TenNguoiPhanTich","TenThauPhu" };
            int idx = 0;
            foreach (var c in order)
                if (dgv.Columns.Contains(c)) dgv.Columns[c].DisplayIndex = idx++;

            // SeaGreen styling
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = EnvTheme.Primary;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = EnvTheme.HeaderText;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 36;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgv.DefaultCellStyle.SelectionBackColor = EnvTheme.RowHover;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = EnvTheme.RowAlt;
            dgv.RowTemplate.Height = 32;
            dgv.RowHeadersVisible = false;
            dgv.GridColor = EnvTheme.GridLine;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;

            // Khóa mặc định các cột; 4 cột giá trị sẽ mở ở dòng đang chọn
            foreach (DataGridViewColumn col in dgv.Columns) col.ReadOnly = true;
            foreach (var c in EditableCols)
                if (dgv.Columns.Contains(c)) dgv.Columns[c].ReadOnly = false;
        }

        private void RebindGridFor(string viTriID, string preferTenThongSo = null)
        {
            var tab = tabViTri.TabPages.Cast<TabPage>().FirstOrDefault(t => Convert.ToString(t.Tag) == viTriID);
            if (tab == null) return;
            var dgv = tab.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv == null) return;

            string currentKey = preferTenThongSo;
            if (currentKey == null && dgv.CurrentRow != null && dgv.Columns.Contains("TenThongSo"))
                currentKey = Convert.ToString(dgv.CurrentRow.Cells["TenThongSo"].Value);

            _isBinding = true;
            try
            {
                var dt = _bll.GetThongSoByViTri(viTriID);

                tab.SuspendLayout();
                dgv.SuspendLayout();

                dgv.DataSource = dt;
                ApplyGridPresentation(dgv);

                dgv.ClearSelection();
                DataGridViewRow selectedRow = null;

                if (!string.IsNullOrEmpty(currentKey) && dt.Columns.Contains("TenThongSo"))
                {
                    foreach (DataGridViewRow r in dgv.Rows)
                    {
                        var val = Convert.ToString(r.Cells["TenThongSo"].Value);
                        if (string.Equals(val, currentKey, StringComparison.OrdinalIgnoreCase))
                        {
                            r.Selected = true;
                            selectedRow = r;
                            break;
                        }
                    }
                }

                if (selectedRow == null && dgv.Rows.Count > 0)
                {
                    selectedRow = dgv.Rows[0];
                    selectedRow.Selected = true;
                }

                SafeSetCurrentCell(dgv, selectedRow);
            }
            finally
            {
                dgv.ResumeLayout();
                tab.ResumeLayout();
                _isBinding = false;
            }

            SyncCombosFromGrid(dgv);
            LockRowsExceptCurrent(dgv);
        }

        private void RefreshActiveTab(string preferTenThongSo = null)
        {
            if (tabViTri.SelectedTab == null) return;
            string viTriID = Convert.ToString(tabViTri.SelectedTab.Tag);
            if (!string.IsNullOrEmpty(viTriID))
                RebindGridFor(viTriID, preferTenThongSo);
        }

        private DataGridView GetActiveGrid()
        {
            return tabViTri?.SelectedTab?.Controls.OfType<DataGridView>().FirstOrDefault();
        }

        // ============== Grid → Combos ==============
        private void SyncCombosFromGrid(DataGridView dgv)
        {
            if (dgv == null || dgv.CurrentRow == null) return;
            var r = dgv.CurrentRow;

            object Get(string col) => (dgv.Columns.Contains(col) && r.Cells[col] != null) ? r.Cells[col].Value : null;

            void SafeSet(ComboBox cb, object val)
            {
                if (cb?.DataSource == null || val == null) return;
                _suppressComboEvents = true;
                try
                {
                    var s = Convert.ToString(val);
                    var dt = (cb.DataSource as DataTable);
                    var valCol = cb.ValueMember;
                    if (dt != null && dt.Columns.Contains(valCol))
                    {
                        bool exists = dt.AsEnumerable().Any(x => Convert.ToString(x[valCol]) == s);
                        if (!exists) { cb.SelectedIndex = -1; return; }
                    }
                    cb.SelectedValue = s;
                }
                catch { }
                finally { _suppressComboEvents = false; }
            }

            SafeSet(cboLoaiChiTieu,  Get("LoaiChiTieuID"));
            SafeSet(cboDonVi,        Get("DonViID"));
            SafeSet(cboLoaiPhanTich, Get("LoaiPhanTichID"));
            SafeSet(cboNguoiPhuTrach,Get("NguoiPhanTichID"));
        }

        // ============== Combos → DÒNG ĐANG CHỌN ==============
        private void ApplyCombosToCurrentRow(DataGridView dgv)
        {
            if (_suppressComboEvents || _isBinding) return;
            if (dgv == null || dgv.CurrentRow == null) return;
            var r = dgv.CurrentRow;

            string lctID = SafeSelectedValue(cboLoaiChiTieu);
            string dvID  = SafeSelectedValue(cboDonVi);
            string lptID = SafeSelectedValue(cboLoaiPhanTich);
            string ndID  = SafeSelectedValue(cboNguoiPhuTrach);

            SetCellIfChanged(dgv, r, "LoaiChiTieuID",  lctID);
            SetCellIfChanged(dgv, r, "DonViID",        dvID);
            SetCellIfChanged(dgv, r, "LoaiPhanTichID", lptID);
            SetCellIfChanged(dgv, r, "NguoiPhanTichID",ndID);

            // cập nhật tên hiển thị cho nhìn thấy ngay
            SetCellIfChanged(dgv, r, "TenLoaiChiTieu" , cboLoaiChiTieu.Text?.Trim());
            SetCellIfChanged(dgv, r, "TenDonVi"       , cboDonVi.Text?.Trim());
            SetCellIfChanged(dgv, r, "TenLoaiPhanTich", cboLoaiPhanTich.Text?.Trim());
            SetCellIfChanged(dgv, r, "TenNguoiPhanTich", cboNguoiPhuTrach.Text?.Trim());

            dgv.NotifyCurrentCellDirty(true);
            dgv.EndEdit();
            this.Validate();
        }

        private static string SafeSelectedValue(ComboBox cb)
        {
            if (cb == null || cb.SelectedValue == null) return null;
            return cb.SelectedValue is DataRowView ? null : Convert.ToString(cb.SelectedValue);
        }

        private static void SetCellIfChanged(DataGridView dgv, DataGridViewRow r, string colName, object newVal)
        {
            if (!dgv.Columns.Contains(colName)) return;
            var cur = r.Cells[colName]?.Value;
            var curStr = cur?.ToString();
            var newStr = newVal?.ToString();
            if (curStr != newStr) r.Cells[colName].Value = newVal ?? DBNull.Value;
        }

        // ============== Chỉ cho phép edit dòng đang chọn ==============
        private void LockRowsExceptCurrent(DataGridView dgv)
        {
            if (dgv == null) return;

            foreach (DataGridViewRow row in dgv.Rows) row.ReadOnly = true;

            var cur = dgv.CurrentRow;
            if (cur == null)
            {
                if (dgv.Rows.Count > 0)
                {
                    var r0 = dgv.Rows[0];
                    r0.Selected = true;
                    SafeSetCurrentCell(dgv, r0);
                }
                return;
            }

            cur.ReadOnly = false;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                var col = dgv.Columns[i];
                bool canEdit = EditableCols.Contains(col.Name);
                cur.Cells[i].ReadOnly = !canEdit;
            }

            if (dgv.CurrentCell == null || !dgv.CurrentCell.Visible || dgv.CurrentCell.ReadOnly)
                SafeSetCurrentCell(dgv, cur);
        }

        // ============== Buttons ==============
        private void btnThemChiTieu_Click(object sender, EventArgs e)
        {
            if (tabViTri.SelectedTab == null)
            {
                MessageBox.Show("Vui lòng chọn Vị trí.");
                return;
            }

            string viTriID        = Convert.ToString(tabViTri.SelectedTab.Tag);
            string loaiChiTieuID  = Convert.ToString(cboLoaiChiTieu.SelectedValue);
            string donViID        = Convert.ToString(cboDonVi.SelectedValue);
            string loaiPhanTichID = Convert.ToString(cboLoaiPhanTich.SelectedValue);
            string nguoiDungID    = Convert.ToString(cboNguoiPhuTrach.SelectedValue);

            if (string.IsNullOrWhiteSpace(viTriID) ||
                string.IsNullOrWhiteSpace(loaiChiTieuID) ||
                string.IsNullOrWhiteSpace(donViID) ||
                string.IsNullOrWhiteSpace(loaiPhanTichID) ||
                string.IsNullOrWhiteSpace(nguoiDungID))
            {
                MessageBox.Show("Chọn đầy đủ: Chỉ tiêu, Đơn vị, Phòng phân tích, Người phụ trách.");
                return;
            }

            try
            {
                string newKey = _bll.InsertThongSoMoi_ReturnKey(viTriID, loaiChiTieuID, donViID, loaiPhanTichID, nguoiDungID);
                if (!string.IsNullOrEmpty(newKey))
                {
                    RefreshActiveTab(newKey); // focus dòng mới
                    MessageBox.Show("Đã thêm thông số mới.");
                }
                else
                {
                    RefreshActiveTab();
                    MessageBox.Show("Không thể thêm thông số mới.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        private void btnLuuThayDoi_Click(object sender, EventArgs e)
        {
            var dgv = GetActiveGrid();
            if (dgv == null || dgv.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để lưu.");
                return;
            }

            try
            {
                ApplyCombosToCurrentRow(dgv);   // ép ghi 4 combo vào dòng hiện tại

                dgv.EndEdit();
                this.Validate();

                var dt = dgv.DataSource as DataTable;
                if (dt == null) { MessageBox.Show("Không có dữ liệu để lưu."); return; }

                var changes = dt.GetChanges();
                if (changes == null || changes.Rows.Count == 0)
                {
                    MessageBox.Show("Không có thay đổi nào.");
                    return;
                }

                _bll.UpdateThongSo(changes);
                dt.AcceptChanges();

                RefreshActiveTab();
                MessageBox.Show("Đã lưu thay đổi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var dgv = GetActiveGrid();
            if (dgv == null || dgv.CurrentRow == null)
            {
                MessageBox.Show("Chọn một dòng để xóa.");
                return;
            }

            string tenThongSo = dgv.CurrentRow.Cells["TenThongSo"] != null
                                ? Convert.ToString(dgv.CurrentRow.Cells["TenThongSo"].Value)
                                : null;

            if (string.IsNullOrWhiteSpace(tenThongSo))
            {
                MessageBox.Show("Không tìm thấy khoá (TenThongSo).");
                return;
            }

            if (MessageBox.Show($"Xóa thông số: {tenThongSo} ?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                _bll.DeleteThongSo(tenThongSo);
                RefreshActiveTab();
                MessageBox.Show("Đã xóa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }
    }
}
