// GUI/GUI_FormThongSoTheoLoai.cs
using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static GUI.GUI_Form_DangNhap;

namespace GUI
{
    public partial class GUI_FormThongSoTheoLoai : Form
    {
        // ==== Input context ====
        private readonly string _donHangID;
        private readonly string _viTriID;
        private readonly string _loaiViTriID;
        private readonly string _tenDonHang;
        private readonly string _diaChi;
        private readonly string _tenLoaiViTri;
        private bool IsAdmin()
        {
            var vt = Session.CurrentUser?.VaiTroID;
            var pb = Session.CurrentUser?.PhongBanID;
            return string.Equals(vt, "VT001", StringComparison.OrdinalIgnoreCase)
                && string.Equals(pb, "PB006", StringComparison.OrdinalIgnoreCase);
        }


        // ==== Services ====
        private readonly BLL_ThongSoQuanTrac _bll = new BLL_ThongSoQuanTrac();

        // ==== Caches / state ====
        private List<DTO_LoaiChiTieu> _lctAll;      // nguồn LCT gốc (DTO)
        private List<DTO_NguoiDung> _usersAll;    // nguồn User gốc (DTO) từ BLL_TaiKhoan
        private List<DTO_NguoiDung> _usersPB;     // user đã lọc theo phòng ban session
        private bool _suspend = false;              // chặn vòng lặp SelectedIndexChanged

        // BindingSource cho grid
        private readonly BindingSource _bs = new BindingSource();

        public GUI_FormThongSoTheoLoai() { InitializeComponent(); }

        public GUI_FormThongSoTheoLoai(
            string donHangID, string viTriID, string loaiViTriID,
            string tenDonHang, string diaChi, string tenLoaiViTri) : this()
        {
            _donHangID = donHangID;
            _viTriID = viTriID;
            _loaiViTriID = loaiViTriID;
            _tenDonHang = tenDonHang;
            _diaChi = diaChi;
            _tenLoaiViTri = tenLoaiViTri;
        }

        private void GUI_FormThongSoTheoLoai_Load(object sender, EventArgs e)
        {


            lblDonHang.Text = string.IsNullOrWhiteSpace(_tenDonHang)
                ? "Đơn hàng"
                : $"{_tenDonHang} – {_tenLoaiViTri}";
            var dt = _bll.GetViTriByDonHang(_donHangID); // DataTable các vị trí của đơn hàng
            string diaChi = null;
            if (dt != null && dt.Columns.Contains("DiaChi"))
            {
                var row = dt.AsEnumerable()
                            .FirstOrDefault(r => string.Equals(Convert.ToString(r["ViTriID"]), _viTriID,
                                                               StringComparison.OrdinalIgnoreCase));
                if (row != null) diaChi = Convert.ToString(row["DiaChi"]);
            }
            lblDiaChi.Text = "Địa chỉ: " + (string.IsNullOrWhiteSpace(diaChi) ? "…" : diaChi);


            dgv.DataSource = _bs;

            LoadCombos();
            ForceRebind();

            this.Activated += (s, ev) =>
            {
                string keep = null;
                if (dgv.CurrentRow != null && dgv.Columns.Contains("TenThongSo"))
                    keep = Convert.ToString(dgv.CurrentRow.Cells["TenThongSo"].Value);
                ForceRebind(keep);
            };
        }

        // ================== Load danh mục ==================
        private void LoadCombos()
        {
            try
            {
                // --- 1) Loại Chỉ Tiêu (DTO: có PhongBan, GiaTriChuan)
                _lctAll = _bll.GetAllLoaiChiTieuDTO() ?? new List<DTO_LoaiChiTieu>();
                cboLoaiChiTieu.DataSource = _lctAll.ToList();
                cboLoaiChiTieu.DisplayMember = nameof(DTO_LoaiChiTieu.TenChiTieu);
                cboLoaiChiTieu.ValueMember = nameof(DTO_LoaiChiTieu.LoaiChiTieuID);
                cboLoaiChiTieu.SelectedIndex = -1;



                // --- 3) Users theo PHÒNG BAN SESSION (yêu cầu)
                // Sau khi đã có _usersAll:
                // --- 3) Users theo PHÒNG BAN SESSION ---
                var bllTk = new BLL_TaiKhoan();
                var dtUsers = bllTk.GetAllNguoiDung(); // DataTable gốc -> map DTO

                _usersAll = (dtUsers == null) ? new List<DTO_NguoiDung>() :
                    dtUsers.AsEnumerable().Select(r => new DTO_NguoiDung
                    {
                        NguoiDungID = Convert.ToString(r["NguoiDungID"]),
                        HoVaTen = Convert.ToString(r["HoVaTen"]),
                        PhongBanID = Convert.ToString(r["PhongBanID"])
                    }).ToList();

                // lọc theo session / admin
                string myPB = Session.CurrentUser?.PhongBanID?.ToString();
                _usersPB = IsAdmin()
                    ? _usersAll.ToList()
                    : (string.IsNullOrWhiteSpace(myPB)
                        ? _usersAll.ToList()
                        : _usersAll.Where(u => string.Equals(u.PhongBanID, myPB, StringComparison.OrdinalIgnoreCase)).ToList());

                // build view (có guard tránh null)
                var src = _usersPB ?? new List<DTO_NguoiDung>();
                var usersView = src.Select(u => new
                {
                    u.NguoiDungID,
                    u.HoVaTen,
                    u.PhongBanID,
                    DisplayUser = $"{u.HoVaTen} - " +
                                  (string.Equals(u.PhongBanID, "PB003", StringComparison.OrdinalIgnoreCase) ? "HT" :
                                   string.Equals(u.PhongBanID, "PB004", StringComparison.OrdinalIgnoreCase) ? "TN" : u.PhongBanID)
                }).ToList();

                cboNguoiPhuTrach.DataSource = usersView;   // không để null
                cboNguoiPhuTrach.DisplayMember = "DisplayUser";
                cboNguoiPhuTrach.ValueMember = "NguoiDungID";
                try { cboNguoiPhuTrach.StartIndex = -1; } catch { cboNguoiPhuTrach.SelectedIndex = -1; }

                // thầu phụ
                Action refreshThauPhu = () =>
                {
                    var npt = cboNguoiPhuTrach.SelectedValue?.ToString();
                    var thauView = usersView.Where(x => !string.Equals(x.NguoiDungID, npt, StringComparison.OrdinalIgnoreCase)).ToList();
                    cboThauPhu.DataSource = thauView;
                    cboThauPhu.DisplayMember = "DisplayUser";
                    cboThauPhu.ValueMember = "NguoiDungID";
                    try { cboThauPhu.StartIndex = -1; } catch { cboThauPhu.SelectedIndex = -1; }
                };
                refreshThauPhu();


                cboNguoiPhuTrach.SelectedIndexChanged += (s, e) =>
                {
                    if (_suspend) return;
                    refreshThauPhu();
                    // Người -> lọc Chỉ tiêu theo PB người đang chọn
                    var pb = GetSelectedUserPB();
                    ApplyFilterLCT_ByPB(pb);
                };

                // Chỉ tiêu -> lọc Người theo PB của LCT
                var vt = Session.CurrentUser?.VaiTroID;
                var pb1 = Session.CurrentUser?.PhongBanID;

                // Kiểm tra quyền: Admin hoặc phòng ban HT / TN
                bool allowSelect =
                    IsAdmin() ||
                    string.Equals(pb1, "PB003", StringComparison.OrdinalIgnoreCase) || // HT
                    string.Equals(pb1, "PB004", StringComparison.OrdinalIgnoreCase);   // TN

                // Chỉ người được phép mới enable combobox
                cboNguoiPhuTrach.Enabled = allowSelect;
                cboThauPhu.Enabled = allowSelect;
                cboLoaiChiTieu.Enabled = allowSelect;

                // Nếu bị khóa thì set màu nền xám nhẹ để người dùng thấy rõ
                if (!allowSelect)
                {
                    cboNguoiPhuTrach.FillColor = System.Drawing.Color.Gainsboro;
                    cboThauPhu.FillColor = System.Drawing.Color.Gainsboro;
                    cboLoaiChiTieu.FillColor = System.Drawing.Color.Gainsboro;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message);
            }
        }

        // ================== Thao tác dữ liệu ==================
        private void ForceRebind(string selectTenThongSo = null)
        {
            try
            {
                _bs.DataSource = null;
                _bs.ResetBindings(false);

                var fresh = _bll.GetThongSoByViTriLoai(_viTriID, _loaiViTriID);
                _bs.DataSource = fresh;
                _bs.ResetBindings(false);

                StyleGridColumns(); // GIỮ LẠI
                dgv.Refresh();
                Application.DoEvents();

                if (!string.IsNullOrEmpty(selectTenThongSo) && dgv.Columns.Contains("TenThongSo"))
                {
                    foreach (DataGridViewRow r in dgv.Rows)
                    {
                        var v = Convert.ToString(r.Cells["TenThongSo"].Value);
                        if (string.Equals(v, selectTenThongSo, StringComparison.OrdinalIgnoreCase))
                        {
                            r.Selected = true;
                            if (r.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible) is DataGridViewCell first)
                                dgv.CurrentCell = first;
                            dgv.FirstDisplayedScrollingRowIndex = r.Index;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // ================== StyleGridColumns (GIỮ LẠI) ==================
        private string GetPhongBanIDByNguoiDungID(string nguoiDungID, BLL_TaiKhoan bll)
        {
            var dt = bll.GetAllNguoiDung();
            return dt?.AsEnumerable()
                     .FirstOrDefault(r => string.Equals(
                         Convert.ToString(r["NguoiDungID"])?.Trim(),
                         nguoiDungID?.Trim(),
                         StringComparison.OrdinalIgnoreCase))
                    ?["PhongBanID"]?.ToString()?.Trim();
        }

        private void StyleGridColumns()
        {
            if (dgv == null || dgv.Columns.Count == 0) return;

            // Ẩn & header
            Hide("ViTriID"); Hide("LoaiViTriID");
            Hide("LoaiChiTieuID"); Hide("DonViID");
            Hide("LoaiPhanTichID"); Hide("NguoiPhanTichID"); Hide("ThauPhuID");
            Hide("TenThongSo"); Hide("TenLoaiPhanTich");
            Hide("GiaTriSo");
            SetHeader("TenLoaiChiTieu", "Chỉ tiêu");
            SetHeader("TenDonVi", "Đơn vị");
            SetHeader("TenNguoiPhanTich", "Người phụ trách");
            SetHeader("TenThauPhu", "Thầu phụ");
            SetHeader("GiaTri", "Giá trị");
            SetHeader("GiaTriQuyChuan", "Giá trị chuẩn");
            SetHeader("KetLuan", "Kết luận");

            // Khóa mặc định
            foreach (DataGridViewColumn c in dgv.Columns) c.ReadOnly = true;

            // Mở khoá mức cột cho các cột có thể chỉnh
            if (dgv.Columns.Contains("GiaTri")) dgv.Columns["GiaTri"].ReadOnly = false;
            if (dgv.Columns.Contains("GiaTriQuyChuan")) dgv.Columns["GiaTriQuyChuan"].ReadOnly = true;
            if (dgv.Columns.Contains("KetLuan")) dgv.Columns["KetLuan"].ReadOnly = false;

            var bll = new BLL_TaiKhoan();

            // User hiện tại
            string pbUserID = Session.CurrentUser?.PhongBanID?.Trim();
            var pbUser = !string.IsNullOrEmpty(pbUserID) ? bll.GetPhongBanByID(pbUserID) : null;
            string pbUserIdNorm = pbUser?.PhongBanID?.Trim();

            string vaiTroId = Session.CurrentUser?.VaiTroID?.Trim();

            if (string.IsNullOrEmpty(pbUserIdNorm))
            {
                MessageBox.Show("Không lấy được PhongBanID của user hiện tại.");
                return;
            }

            // Điều kiện "siêu sửa": PB006 + VT001
            // THÊM QUYỀN ADMIN Ở ĐÂY
            bool isSuperEditor =
                IsAdmin() || // Admin: VT001 & PB006
                (string.Equals(pbUserIdNorm, "PB006", StringComparison.OrdinalIgnoreCase) &&
                 string.Equals(vaiTroId, "VT001", StringComparison.OrdinalIgnoreCase));


            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (r.IsNewRow) continue;

                // Luôn cho sửa Kết luận
                if (dgv.Columns.Contains("KetLuan")) r.Cells["KetLuan"].ReadOnly = false;

                // Lấy NguoiPhanTichID của dòng
                string nptID = Convert.ToString(r.Cells["NguoiPhanTichID"]?.Value)?.Trim();

                bool sameDept = false;

                if (!string.IsNullOrEmpty(nptID))
                {
                    // Từ NguoiPhanTichID -> PhongBanID
                    string pbNptID = GetPhongBanIDByNguoiDungID(nptID, bll);
                    if (!string.IsNullOrEmpty(pbNptID))
                    {
                        var pbNpt = bll.GetPhongBanByID(pbNptID);
                        string pbNptId = pbNpt?.PhongBanID?.Trim();
                        sameDept = !string.IsNullOrEmpty(pbNptId) &&
                                   string.Equals(pbUserIdNorm, pbNptId, StringComparison.OrdinalIgnoreCase);
                    }
                }

                bool canEdit = isSuperEditor || sameDept;

                if (dgv.Columns.Contains("GiaTri")) r.Cells["GiaTri"].ReadOnly = !canEdit;

            }
        }

        // ============== NÚT ==============
        private void btnThem_Click(object sender, EventArgs e)
        {
            var lct = cboLoaiChiTieu.SelectedValue?.ToString();
            var nd = cboNguoiPhuTrach.SelectedValue?.ToString();
            var tp = cboThauPhu.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(lct) || string.IsNullOrWhiteSpace(nd))
            {
                MessageBox.Show("Chọn đủ Người phụ trách và Chỉ tiêu.");
                return;
            }

            try
            {
                // Đơn vị mặc định theo LCT
                var dv = _bll.GetDefaultDonViID_ByLoaiChiTieu(lct);
                if (string.IsNullOrWhiteSpace(dv))
                {
                    MessageBox.Show("Không xác định được Đơn vị từ Chỉ tiêu.");
                    return;
                }
                const int PRECISION = 3; // đổi tuỳ bạn
                                         // Lấy Giá trị chuẩn từ DTO LCT đang chọn (FLOAT)
                float? giaTriChuan = null;
                if (cboLoaiChiTieu.SelectedItem is DTO_LoaiChiTieu sel &&
                    !string.IsNullOrWhiteSpace(sel.GiaTriChuan))
                {
                    var s = sel.GiaTriChuan.Replace(',', '.'); // chuẩn hoá dấu thập phân
                    if (float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands,
                           CultureInfo.InvariantCulture, out var fv))
                    {
                        giaTriChuan = (float)Math.Round(fv, PRECISION, MidpointRounding.AwayFromZero);
                    }
                }

                // Use BLL method that inserts for source ViTri and clones metadata across DonHang atomically
                var key = _bll.InsertThongSoAndCloneAcrossDonHang(
                    _donHangID, _viTriID, _loaiViTriID, lct, dv, "LPA002", nd, tp, giaTriChuan);

                // Rebind và giữ dòng mới
                ForceRebind(key);
                // synchronization succeeded (silent)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm thông số: " + ex.Message);
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            var dt = _bs.DataSource as DataTable;
            if (dt == null) return;

            var changes = dt.GetChanges();
            if (changes == null || changes.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi.");
                return;
            }

            try
            {
                // =========================================
                // 1) AUTO UPDATE KẾT LUẬN TRƯỚC KHI LƯU
                // =========================================
                foreach (DataRow row in changes.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    // Lấy 2 cột liên quan
                    float giaTri = 0;
                    float quyChuan = 0;

                    float.TryParse(Convert.ToString(row["GiaTri"]), out giaTri);
                    float.TryParse(Convert.ToString(row["GiaTriQuyChuan"]), out quyChuan);

                    string ketLuan;

                    // --- Logic kết luận ---
                    if (giaTri < 0)
                    {
                        ketLuan = "Không đạt";
                    }
                    else if (quyChuan > 0 && giaTri >= 2 * quyChuan)
                    {
                        ketLuan = "Không đạt";
                    }
                    else
                    {
                        ketLuan = "Đạt";
                    }

                    row["KetLuan"] = ketLuan;
                }

                // =========================================
                // 2) LƯU VÀO DB
                // =========================================
                _bll.UpdateThongSo(changes);

                // =========================================
                // 3) REBIND & GIỮ DÒNG CŨ
                // =========================================
                string keep = null;
                if (dgv.CurrentRow != null && dgv.Columns.Contains("TenThongSo"))
                    keep = Convert.ToString(dgv.CurrentRow.Cells["TenThongSo"].Value);

                ForceRebind(keep);

                MessageBox.Show("Đã lưu thay đổi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu: " + ex.Message);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn dòng để xóa.");
                return;
            }

            string key = null;
            if (dgv.Columns.Contains("ThongSoID"))
                key = Convert.ToString(dgv.CurrentRow.Cells["ThongSoID"].Value);
            if (string.IsNullOrWhiteSpace(key) && dgv.Columns.Contains("TenThongSo"))
                key = Convert.ToString(dgv.CurrentRow.Cells["TenThongSo"].Value);

            if (string.IsNullOrWhiteSpace(key))
            {
                MessageBox.Show("Không xác định được khóa của thông số để xóa.");
                return;
            }

            if (MessageBox.Show($"Xóa thông số '{key}' ?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            bool ok = false;
            try
            {
                ok = _bll.DeleteThongSo(key);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
            finally
            {
                ForceRebind();
            }

            MessageBox.Show(ok ? "Đã xóa thông số." : "Xóa không thành công.");
        }

        // ================== Helpers  ==================
        private void Hide(string col)
        {
            if (dgv.Columns.Contains(col)) dgv.Columns[col].Visible = false;
        }
        private void SetHeader(string col, string header)
        {
            if (dgv.Columns.Contains(col)) dgv.Columns[col].HeaderText = header;
        }


        private string GetSelectedUserPB()
        {
            var id = cboNguoiPhuTrach.SelectedValue?.ToString();
            var u = _usersPB?.FirstOrDefault(x => string.Equals(x.NguoiDungID, id, StringComparison.OrdinalIgnoreCase));
            return u?.PhongBanID?.Trim();
        }
        private void ApplyFilterLCT_ByPB(string pbId)
        {
            _suspend = true;
            try
            {

                var filtered = (IsAdmin() || string.IsNullOrWhiteSpace(pbId))
             ? _lctAll
             : _lctAll.Where(x => string.Equals(x.PhongBan, pbId, StringComparison.OrdinalIgnoreCase)).ToList();

                var old = cboLoaiChiTieu.SelectedValue?.ToString();
                cboLoaiChiTieu.DataSource = filtered;
                cboLoaiChiTieu.DisplayMember = nameof(DTO_LoaiChiTieu.TenChiTieu);
                cboLoaiChiTieu.ValueMember = nameof(DTO_LoaiChiTieu.LoaiChiTieuID);

                if (string.IsNullOrEmpty(old) || !filtered.Any(x => string.Equals(x.LoaiChiTieuID, old, StringComparison.OrdinalIgnoreCase)))
                    cboLoaiChiTieu.SelectedIndex = -1;
            }
            finally { _suspend = false; }
        }

        private void lblNguoi_Click(object sender, EventArgs e)
        {

        }
    }
}
