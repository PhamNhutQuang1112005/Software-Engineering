using System;
using System.Data;
using DAL;

namespace BLL
{
    public class BLL_ThongSoQuanTrac
    {
        private readonly DAL_ThongSoQuanTrac dal = new DAL_ThongSoQuanTrac();

        // Lookup
        public DataTable GetAllLoaiChiTieu()   => dal.GetAllLoaiChiTieu();
        public DataTable GetAllDonVi()         => dal.GetAllDonVi();
        public DataTable GetAllLoaiPhanTich()  => dal.GetAllLoaiPhanTich();
        public DataTable GetAllNguoiDung()     => dal.GetAllNguoiDung();

        // ViTri + ThongSo
        public void EnsureViTriAndThongSo(string donHangID)
        {
            if (string.IsNullOrWhiteSpace(donHangID))
                throw new ArgumentException("DonHangID rỗng.");
            dal.EnsureViTriAndThongSo(donHangID);
        }

        public DataTable GetViTriByDonHang(string donHangID) => dal.GetViTriByDonHang(donHangID);
        public DataTable GetThongSoByViTri(string viTriID)   => dal.GetThongSoByViTri(viTriID);

        public bool InsertThongSoMoi(string viTriID, string loaiChiTieuID, string donViID, string loaiPhanTichID, string nguoiPhanTichID)
        {
            if (string.IsNullOrWhiteSpace(viTriID)) throw new ArgumentException("Vị trí không hợp lệ");
            if (string.IsNullOrWhiteSpace(loaiChiTieuID) ||
                string.IsNullOrWhiteSpace(donViID) ||
                string.IsNullOrWhiteSpace(loaiPhanTichID) ||
                string.IsNullOrWhiteSpace(nguoiPhanTichID))
                throw new ArgumentException("Thiếu tham số khi thêm thông số.");

            return dal.InsertThongSoMoi(viTriID, loaiChiTieuID, donViID, loaiPhanTichID, nguoiPhanTichID);
        }

        /// <summary>
        /// Lưu các thay đổi từ DataTable của lưới.
        /// Hỗ trợ cập nhật đầy đủ: 4 FK + GiaTri/GiaTriSo/GiaTriQuyChuan + KetLuan.
        /// </summary>
        public void UpdateThongSo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            foreach (DataRow r in dt.Rows)
            {
                if (r.RowState == DataRowState.Unchanged) continue;
                if (r.RowState == DataRowState.Deleted)   continue; // không xử lý xóa ở đây

                string ten = Convert.ToString(r["TenThongSo"]);
                if (string.IsNullOrWhiteSpace(ten)) continue;

                // cờ có cột
                bool hasLct = dt.Columns.Contains("LoaiChiTieuID");
                bool hasDv  = dt.Columns.Contains("DonViID");
                bool hasLpt = dt.Columns.Contains("LoaiPhanTichID");
                bool hasNd  = dt.Columns.Contains("NguoiPhanTichID");
                bool hasGT  = dt.Columns.Contains("GiaTri");
                bool hasGTS = dt.Columns.Contains("GiaTriSo");
                bool hasGTQ = dt.Columns.Contains("GiaTriQuyChuan");
                bool hasKL  = dt.Columns.Contains("KetLuan");

                object lctID = hasLct ? r["LoaiChiTieuID"]   : null;
                object dvID  = hasDv  ? r["DonViID"]         : null;
                object lptID = hasLpt ? r["LoaiPhanTichID"]  : null;
                object ndID  = hasNd  ? r["NguoiPhanTichID"] : null;
                object gt    = hasGT  ? r["GiaTri"]          : null;
                object gts   = hasGTS ? r["GiaTriSo"]        : null;
                object gtq   = hasGTQ ? r["GiaTriQuyChuan"]  : null;
                object kl    = hasKL  ? r["KetLuan"]         : null;

                // Nếu không có cột nào trong số trên thì bỏ qua
                if (!(hasLct || hasDv || hasLpt || hasNd || hasGT || hasGTS || hasGTQ || hasKL)) continue;

                dal.UpdateThongSo_Full(
                    ten,
                    lctID, hasLct,
                    dvID,  hasDv,
                    lptID, hasLpt,
                    ndID,  hasNd,
                    gt,    hasGT,
                    gts,   hasGTS,
                    gtq,   hasGTQ,
                    kl,    hasKL
                );
            }

            dt.AcceptChanges();
        }

        public bool DeleteThongSo(string tenThongSo)
        {
            if (string.IsNullOrWhiteSpace(tenThongSo))
                throw new ArgumentException("Thiếu khóa TenThongSo");
            return dal.DeleteThongSo(tenThongSo);
        }

        public string InsertThongSoMoi_ReturnKey(string viTriID, string loaiChiTieuID, string donViID,
                                                 string loaiPhanTichID, string nguoiPhanTichID, string thauPhuID = null)
        {
            string key = "TS_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            bool ok = dal.InsertThongSoMoi_WithGivenKey(viTriID, key, loaiChiTieuID, donViID, loaiPhanTichID, nguoiPhanTichID, thauPhuID);
            return ok ? key : null;
        }
    }
}
