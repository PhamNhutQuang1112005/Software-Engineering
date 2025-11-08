// GUI/GUI_FormThongSoTheoLoai.cs
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static GUI.GUI_Form_DangNhap;
using Guna.UI2.WinForms; // thêm dòng này
namespace GUI
{
    public partial class GUI_FormThongSoTheoLoai : Form
    {
        private readonly string _donHangID;
        private readonly string _viTriID;
        private readonly string _loaiViTriID;
        private readonly string _tenDonHang;
        private readonly string _diaChi;
        private readonly string _tenLoaiViTri;

        private readonly BLL_ThongSoQuanTrac _bll = new BLL_ThongSoQuanTrac();

        // *** QUAN TRỌNG: BindingSource để làm mới lưới ngay lập tức
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
            // Header
            lblDonHang.Text = string.IsNullOrWhiteSpace(_tenDonHang)
                ? "Đơn hàng"
                : _tenDonHang + " – " + _tenLoaiViTri;
            lblDiaChi.Text = "Địa chỉ: " + (_diaChi ?? "…");

            // Gắn BindingSource cho lưới NGAY TỪ ĐẦU
            dgv.DataSource = _bs;

            LoadCombosOnce();
            ForceRebind(); // nạp dữ liệu lần đầu
            this.Activated += (s, ev) =>
{
    string keep = null;
    if (dgv.CurrentRow != null && dgv.Columns.Contains("TenThongSo"))
        keep = Convert.ToString(dgv.CurrentRow.Cells["TenThongSo"].Value);

    ForceRebind(keep); // luôn lấy lại từ DB khi quay lại form
};

        }

        // ================== NẠP DANH MỤC (1 lần) ==================
        private void LoadCombosOnce()
        {
            try
            {
                // ===== Chỉ tiêu
                var dtLCT = _bll.GetAllLoaiChiTieu();
                cboLoaiChiTieu.DataSource = dtLCT;
                cboLoaiChiTieu.DisplayMember = (dtLCT != null && dtLCT.Columns.Contains("TenChiTieu")) ? "TenChiTieu" : "TenLoaiChiTieu";
                cboLoaiChiTieu.ValueMember = "LoaiChiTieuID";
                cboLoaiChiTieu.MaxDropDownItems = 6;
                try { cboLoaiChiTieu.StartIndex = -1; } catch { cboLoaiChiTieu.SelectedIndex = -1; }
                
                // ===== Loại/Phòng phân tích
                var dtLPT = _bll.GetAllLoaiPhanTich();
                cboLoaiPhanTich.DataSource = dtLPT;
                cboLoaiPhanTich.DisplayMember = "TenLoai";
                cboLoaiPhanTich.ValueMember = "LoaiPhanTichID";
                try { cboLoaiPhanTich.StartIndex = -1; } catch { cboLoaiPhanTich.SelectedIndex = -1; }

                // ===== Người phụ trách / Thầu phụ (PB003 & PB004, hiển thị HT/TN)
                var userBll = new BLL_TaiKhoan();
                var dtUsers = userBll.GetAllNguoiDung();
                var allowed = new HashSet<string>(new[] { "PB003", "PB004" }, StringComparer.OrdinalIgnoreCase);

                DataTable dtAllowed = (dtUsers != null) ? dtUsers.Clone() : new DataTable();
                if (dtUsers != null && dtUsers.Columns.Contains("PhongBanID"))
                {
                    foreach (DataRow r in dtUsers.Rows)
                    {
                        var pb = Convert.ToString(r["PhongBanID"]).Trim();
                        if (allowed.Contains(pb)) dtAllowed.Rows.Add(r.ItemArray);
                    }
                }

                if (!dtAllowed.Columns.Contains("DisplayUser"))
                    dtAllowed.Columns.Add("DisplayUser", typeof(string));

                foreach (DataRow r in dtAllowed.Rows)
                {
                    var name = Convert.ToString(r["HoVaTen"]).Trim();
                    var pb = Convert.ToString(r["PhongBanID"]).Trim();
                    string tag = string.Equals(pb, "PB003", StringComparison.OrdinalIgnoreCase) ? "HT"
                               : string.Equals(pb, "PB004", StringComparison.OrdinalIgnoreCase) ? "TN"
                               : pb; // fallback
                    r["DisplayUser"] = name + " - " + tag;
                }

                // Bind Người phụ trách
                cboNguoiPhuTrach.DataSource = dtAllowed.Copy();
                cboNguoiPhuTrach.DisplayMember = "DisplayUser";
                cboNguoiPhuTrach.ValueMember = "NguoiDungID";
                try { cboNguoiPhuTrach.StartIndex = -1; } catch { cboNguoiPhuTrach.SelectedIndex = -1; }

                // Bind Thầu phụ (không trùng Người phụ trách) + auto cập nhật khi đổi
                Action updateThauPhu = () =>
                {
                    string nptID = Convert.ToString(cboNguoiPhuTrach.SelectedValue ?? "").Trim();
                    DataTable dtThau = dtAllowed.Clone();
                    foreach (DataRow r in dtAllowed.Rows)
                    {
                        var id = Convert.ToString(r["NguoiDungID"]).Trim();
                        if (!string.Equals(id, nptID, StringComparison.OrdinalIgnoreCase))
                            dtThau.Rows.Add(r.ItemArray);
                    }
                    cboThauPhu.DataSource = dtThau;
                    cboThauPhu.DisplayMember = "DisplayUser";
                    cboThauPhu.ValueMember = "NguoiDungID";
                    try { cboThauPhu.StartIndex = -1; } catch { cboThauPhu.SelectedIndex = -1; }
                };
                updateThauPhu();
                cboNguoiPhuTrach.SelectedIndexChanged += (s, e) => updateThauPhu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message);
            }
        }


        // ================== REQUERY + REBIND CỨNG ==================
        private void ForceRebind(string selectTenThongSo = null)
        {
            try
            {
                // Ngắt DataSource trước để đảm bảo lưới refresh tức thời
                _bs.DataSource = null;
                _bs.ResetBindings(false);

                var fresh = _bll.GetThongSoByViTriLoai(_viTriID, _loaiViTriID); // luôn query mới
                _bs.DataSource = fresh;
                _bs.ResetBindings(false); // báo cho lưới cập nhật ngay lập tức

                StyleGridColumns();       // đặt header/readonly/ẩn cột
                dgv.Refresh();            // ép refresh
                Application.DoEvents();   // “đẩy” UI (an toàn)

                if (!string.IsNullOrEmpty(selectTenThongSo))
                    TrySelectRowByCell("TenThongSo", selectTenThongSo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

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
            Hide("LoaiPhanTichID"); Hide("NguoiPhanTichID"); Hide("ThauPhuID"); Hide("GiaTriSo");
            Hide("TenThongSo");
            SetHeader("TenLoaiChiTieu", "Chỉ tiêu");
            SetHeader("TenDonVi", "Đơn vị");
            SetHeader("TenLoaiPhanTich", "Loại Phân Tích");
            SetHeader("TenNguoiPhanTich", "Người phụ trách");
            SetHeader("TenThauPhu", "Thầu phụ");
            SetHeader("GiaTri", "Giá trị ");
            SetHeader("GiaTriQuyChuan", "Giá trị chuẩn");
            SetHeader("KetLuan", "Kết luận");

            // Mặc định: khóa tất cả cột
            foreach (DataGridViewColumn c in dgv.Columns) c.ReadOnly = true;

            // Mở khóa mức CỘT cho 3 cột có thể chỉnh (để còn khóa/mở theo từng ô)
            if (dgv.Columns.Contains("GiaTri")) dgv.Columns["GiaTri"].ReadOnly = false;
            if (dgv.Columns.Contains("GiaTriQuyChuan")) dgv.Columns["GiaTriQuyChuan"].ReadOnly = false;
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

            // Điều kiện "siêu sửa" gắn trực tiếp vào phần điều kiện
            bool isSuperEditor =
                string.Equals(pbUserIdNorm, "PB006", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(vaiTroId, "VT001", StringComparison.OrdinalIgnoreCase);

            int allow = 0, deny = 0;

            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (r.IsNewRow) continue;

                // luôn cho sửa Kết luận
                if (dgv.Columns.Contains("KetLuan")) r.Cells["KetLuan"].ReadOnly = false;

                // Lấy NguoiPhanTichID của dòng
                string nptID = Convert.ToString(r.Cells["NguoiPhanTichID"]?.Value)?.Trim();

                // sameDept mặc định là false, sẽ tính nếu có NPT
                bool sameDept = false;

                if (!string.IsNullOrEmpty(nptID))
                {
                    // Từ NguoiPhanTichID -> PhongBanID (không dùng GetNguoiDungByID)
                    string pbNptID = GetPhongBanIDByNguoiDungID(nptID, bll);
                    if (!string.IsNullOrEmpty(pbNptID))
                    {
                        // Dùng GetPhongBanByID (đúng yêu cầu)
                        var pbNpt = bll.GetPhongBanByID(pbNptID);
                        string pbNptIdNorm = pbNpt?.PhongBanID?.Trim();

                        sameDept = !string.IsNullOrEmpty(pbNptIdNorm) &&
                                   string.Equals(pbUserIdNorm, pbNptIdNorm, StringComparison.OrdinalIgnoreCase);
                    }
                }

                // === Điều kiện cuối cùng nằm "trong phần đk": super || cùng phòng ===
                bool canEdit = isSuperEditor || sameDept;

                if (dgv.Columns.Contains("GiaTri")) r.Cells["GiaTri"].ReadOnly = !canEdit;
                if (dgv.Columns.Contains("GiaTriQuyChuan")) r.Cells["GiaTriQuyChuan"].ReadOnly = !canEdit;

                if (canEdit) allow++; else deny++;
            }

            // (tuỳ bạn) có thể log allow/deny nếu cần
            //Debug.WriteLine($"Editable rows: {allow}, locked rows: {deny}");
        }



        // ===================== SỰ KIỆN NÚT =====================
        private void btnThem_Click(object sender, EventArgs e)
        {
            var lct = cboLoaiChiTieu.SelectedValue?.ToString();
            var lpt = cboLoaiPhanTich.SelectedValue?.ToString();
            var nd = cboNguoiPhuTrach.SelectedValue?.ToString();
            var tp = cboThauPhu.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(lct) || string.IsNullOrWhiteSpace(lpt) || string.IsNullOrWhiteSpace(nd))
            {
                MessageBox.Show("Chọn đủ Chỉ tiêu / Phòng phân tích / Người phụ trách.");
                return;
            }

            try
            {
                var dv = _bll.GetDefaultDonViID_ByLoaiChiTieu(lct);
                if (string.IsNullOrWhiteSpace(dv))
                {
                    MessageBox.Show("Không xác định được Đơn vị từ Chỉ tiêu.");
                    return;
                }

                var key = _bll.InsertThongSoMoi_ReturnKey_WithLoai(_viTriID, _loaiViTriID, lct, dv, lpt, nd, tp);

                // Phase 1: Đồng bộ thông số (theo LoaiChiTieu) sang các vị trí khác của đơn hàng
                SyncLoaiChiTieuAcrossOtherPositions(lct, dv, lpt, nd, tp);

                // Rebind NGAY LẬP TỨC sau khi thêm
                ForceRebind(key);
                MessageBox.Show("Đã thêm thông số mới.");
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
                _bll.UpdateThongSo(changes);

                // giữ dòng đang chọn nếu có
                string keep = null;
                if (dgv.CurrentRow != null && dgv.Columns.Contains("TenThongSo"))
                    keep = Convert.ToString(dgv.CurrentRow.Cells["TenThongSo"].Value);

                // Rebind NGAY
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

            // Ưu tiên ID nếu lưới có, fallback về TenThongSo
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
                // LUÔN rebind để UI cập nhật ngay, kể cả khi ok=false
                ForceRebind();
            }

            if (ok)
                MessageBox.Show("Đã xóa thông số.");
            else
                MessageBox.Show("Đã xóa thông số!");
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: popup chi tiết
        }

        // =================  TIỆN ÍCH LƯỚI  =================
        private void Hide(string col)
        {
            if (dgv.Columns.Contains(col)) dgv.Columns[col].Visible = false;
        }
        private void SetHeader(string col, string header)
        {
            if (dgv.Columns.Contains(col)) dgv.Columns[col].HeaderText = header;
        }
        private void AllowEdit(string col)
        {
            if (dgv.Columns.Contains(col)) dgv.Columns[col].ReadOnly = false;
        }
        private void TrySelectRowByCell(string colName, string value)
        {
            if (!dgv.Columns.Contains(colName) || string.IsNullOrEmpty(value)) return;

            foreach (DataGridViewRow r in dgv.Rows)
            {
                var v = r.Cells[colName].Value;
                if (v != null && string.Equals(Convert.ToString(v), value, StringComparison.OrdinalIgnoreCase))
                {
                    r.Selected = true;
                    var firstVisible = r.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                    if (firstVisible != null) dgv.CurrentCell = firstVisible;
                    dgv.FirstDisplayedScrollingRowIndex = r.Index;
                    break;
                }
            }
        }

        private void cboLoaiChiTieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Để trống (Designer có gắn). DonVi sẽ lấy lúc nhấn Thêm để an toàn.
        }

        // ============== PHASE 1: ĐỒNG BỘ THÔNG SỐ ==============
        private void SyncLoaiChiTieuAcrossOtherPositions(string loaiChiTieuID, string donViID,
                                                         string loaiPhanTichID, string nguoiPhuTrachID, string thauPhuID)
        {
            try
            {
                var dtViTriAll = _bll.GetViTriByDonHang(_donHangID);
                if (dtViTriAll == null) return;

                foreach (DataRow r in dtViTriAll.Rows)
                {
                    string vtId = Convert.ToString(r["ViTriID"]);
                    if (string.IsNullOrEmpty(vtId) || string.Equals(vtId, _viTriID, StringComparison.OrdinalIgnoreCase))
                        continue;

                    // Kiểm tra tồn tại LoaiChiTieu ở vị trí khác
                    var dtThongSo = _bll.GetThongSoByViTriLoai(vtId, _loaiViTriID);
                    bool exists = false;
                    if (dtThongSo != null && dtThongSo.Columns.Contains("LoaiChiTieuID"))
                    {
                        foreach (DataRow tr in dtThongSo.Rows)
                        {
                            if (string.Equals(Convert.ToString(tr["LoaiChiTieuID"]), loaiChiTieuID, StringComparison.OrdinalIgnoreCase))
                            { exists = true; break; }
                        }
                    }
                    if (exists) continue;

                    // Thêm placeholder
                    _bll.InsertThongSoMoi_ReturnKey_WithLoai(vtId, _loaiViTriID, loaiChiTieuID, donViID,
                                                             loaiPhanTichID, nguoiPhuTrachID, thauPhuID);
                }
            }
            catch { /* bỏ qua lỗi đồng bộ để không chặn thao tác thêm */ }
        }
    }
}
