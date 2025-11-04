// GUI/GUI_FormThongSoTheoLoai.cs
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL;
using static GUI.GUI_Form_DangNhap;

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
            _donHangID   = donHangID;
            _viTriID     = viTriID;
            _loaiViTriID = loaiViTriID;
            _tenDonHang  = tenDonHang;
            _diaChi      = diaChi;
            _tenLoaiViTri = tenLoaiViTri;
        }

        private void GUI_FormThongSoTheoLoai_Load(object sender, EventArgs e)
        {
            // Header
            lblDonHang.Text = string.IsNullOrWhiteSpace(_tenDonHang)
                ? "Đơn hàng"
                : _tenDonHang + " – " + _tenLoaiViTri;
            lblDiaChi.Text  = "Địa chỉ: " + (_diaChi ?? "…");

            // Gắn BindingSource cho lưới NGAY TỪ ĐẦU
            dgv.DataSource = _bs;

            LoadCombosOnce();
            ForceRebind(); // nạp dữ liệu lần đầu
        }

        // ================== NẠP DANH MỤC (1 lần) ==================
        private void LoadCombosOnce()
        {
            try
            {
                // Chỉ tiêu
                var dtLCT = _bll.GetAllLoaiChiTieu();
                cboLoaiChiTieu.DataSource    = dtLCT;
                cboLoaiChiTieu.DisplayMember = (dtLCT != null && dtLCT.Columns.Contains("TenChiTieu")) ? "TenChiTieu" : "TenLoaiChiTieu";
                cboLoaiChiTieu.ValueMember   = "LoaiChiTieuID";
                cboLoaiChiTieu.StartIndex    = -1; // Guna2

                // Loại/Phòng phân tích
                var dtLPT = _bll.GetAllLoaiPhanTich();
                cboLoaiPhanTich.DataSource    = dtLPT;
                cboLoaiPhanTich.DisplayMember = "TenLoai";
                cboLoaiPhanTich.ValueMember   = "LoaiPhanTichID";
                cboLoaiPhanTich.StartIndex    = -1;

                // Người phụ trách & Thầu phụ (nếu có BLL_TaiKhoan)
                try
                {
                    var userBll = new BLL_TaiKhoan();
                    var dtUsers = userBll.GetAllNguoiDung();

                    cboNguoiPhuTrach.DataSource    = dtUsers.Copy();
                    cboNguoiPhuTrach.DisplayMember = "HoVaTen";
                    cboNguoiPhuTrach.ValueMember   = "NguoiDungID";
                    cboNguoiPhuTrach.StartIndex    = -1;

                    cboThauPhu.DataSource    = dtUsers;
                    cboThauPhu.DisplayMember = "HoVaTen";
                    cboThauPhu.ValueMember   = "NguoiDungID";
                    cboThauPhu.StartIndex    = -1;
                }
                catch { /* nếu chưa có BLL_TaiKhoan thì bỏ trống 2 combo này */ }
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

        private void StyleGridColumns()
        {
            if (dgv.Columns.Count == 0) return;

            Hide("ViTriID"); Hide("LoaiViTriID");
            Hide("LoaiChiTieuID"); Hide("DonViID");
            Hide("LoaiPhanTichID"); Hide("NguoiPhanTichID"); Hide("ThauPhuID"); Hide("GiaTri"); Hide("GiaTriSo"); 

            SetHeader("TenThongSo",       "Mã thông số");
            SetHeader("TenLoaiChiTieu",   "Chỉ tiêu");
            SetHeader("TenDonVi",         "Đơn vị");
            SetHeader("TenLoaiPhanTich",  "Phòng phân tích");
            SetHeader("TenNguoiPhanTich", "Người phụ trách");
            SetHeader("TenThauPhu",       "Thầu phụ");
            SetHeader("GiaTriQuyChuan",   "Giá trị chuẩn");
            SetHeader("KetLuan",          "Kết luận");
            // 🔹 Bước 1: Lấy PhòngBanID hiện tại của user đăng nhập
            string phongBanIDHienTai = Session.CurrentUser.PhongBanID;

            // 🔹 Bước 2: Lấy ra tên phòng ban thật
            BLL_TaiKhoan bllTaiKhoan = new BLL_TaiKhoan();
            var phongBan = bllTaiKhoan.GetPhongBanByID(phongBanIDHienTai);
            string tenPhongBanNguoiDung = phongBan?.TenPhongBan ?? "";

            // 🔹 Bước 3: Lấy tên phòng ban trong dòng của DataGridView
            string tenPhongBanTrongBang = dgv.CurrentRow.Cells["TenLoaiPhanTich"].Value.ToString();

            // 🔹 Bước 4: Khóa/mở cột chỉnh sửa theo điều kiện
            foreach (DataGridViewColumn c in dgv.Columns) c.ReadOnly = true;

            if (tenPhongBanNguoiDung == tenPhongBanTrongBang)
            {
                // ✅ Cùng phòng ban → cho phép sửa
                AllowEdit("GiaTriQuyChuan");
            }
            else
            {
                // 🚫 Khác phòng ban → khóa sửa
                dgv.Columns["GiaTriQuyChuan"].ReadOnly = true;
            }

            // Luôn cho phép sửa cột Kết luận
            AllowEdit("KetLuan");
        }

        // ===================== SỰ KIỆN NÚT =====================
        private void btnThem_Click(object sender, EventArgs e)
        {
            var lct = cboLoaiChiTieu.SelectedValue?.ToString();
            var lpt = cboLoaiPhanTich.SelectedValue?.ToString();
            var nd  = cboNguoiPhuTrach.SelectedValue?.ToString();
            var tp  = cboThauPhu.SelectedValue?.ToString();

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
    }
}
