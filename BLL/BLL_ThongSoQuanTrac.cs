
using System;
using System.Data;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    /// <summary>
    /// BLL chỉ đóng vai trò trung gian: nhận yêu cầu từ GUI, chuyển tiếp sang DAL.
    /// Tuyệt đối KHÔNG truy vấn trực tiếp DB hay viết SQL ở đây.
    /// </summary>
    public class BLL_ThongSoQuanTrac
    {
        private readonly DAL_ThongSoQuanTrac _dal = new DAL_ThongSoQuanTrac();

        // ===== Lookup (DataTable cho GUI cũ) =====
        public DataTable GetAllLoaiChiTieu()  => _dal.GetAllLoaiChiTieu();
        public DataTable GetAllDonVi()        => _dal.GetAllDonVi();
        public DataTable GetAllLoaiPhanTich() => _dal.GetAllLoaiPhanTich();
        public DataTable GetAllNguoiDung()    => _dal.GetAllNguoiDung();

        // ===== Lookup (DTO cho GUI mới nếu cần) =====
        public List<DTO_LoaiChiTieu>  GetAllLoaiChiTieuDTO()  => _dal.GetAllLoaiChiTieuDTO_();
        public List<DTO_DonVi>        GetAllDonViDTO()        => _dal.GetAllDonViDTO_();
        public List<DTO_LoaiPhanTich> GetAllLoaiPhanTichDTO() => _dal.GetAllLoaiPhanTichDTO_();
        public List<DTO_NguoiDung>    GetAllNguoiDungDTO()    => _dal.GetAllNguoiDungDTO_();

        // ===== Vị trí & Thông số =====
        public void EnsureViTriAndThongSo(string donHangID)
        {
            if (string.IsNullOrWhiteSpace(donHangID))
                throw new ArgumentException("DonHangID rỗng.");
            _dal.EnsureViTriAndThongSo(donHangID);
        }

        public DataTable GetViTriByDonHang(string donHangID) => _dal.GetViTriByDonHang(donHangID);
        public DataTable GetThongSoByViTri(string viTriID)   => _dal.GetThongSoByViTri(viTriID);

        public List<DTO_ViTri> GetViTriByDonHangDTO(string donHangID) => _dal.GetViTriByDonHangDTO_(donHangID);
        public List<DTO_ThongSoMoiTruong> GetThongSoByViTriDTO(string viTriID) => _dal.GetThongSoByViTriDTO_(viTriID);

        public string InsertThongSoMoi_ReturnKey(string viTriID, string loaiChiTieuID, string donViID,
                                                 string loaiPhanTichID, string nguoiPhanTichID, string thauPhuID = null)
            => _dal.InsertThongSoMoi_ReturnKey(viTriID, loaiChiTieuID, donViID, loaiPhanTichID, nguoiPhanTichID, thauPhuID);

        public bool DeleteThongSo(string tenThongSo)
        {
            if (string.IsNullOrWhiteSpace(tenThongSo))
                throw new ArgumentException("Thiếu khóa TenThongSo");
            return _dal.DeleteThongSo(tenThongSo);
        }

        /// <summary>
        /// Lưu các thay đổi từ DataTable của lưới.
        /// BLL chỉ chuyển tiếp CRUD xuống DAL (DAL gọi Stored Procedure).
        /// </summary>
        public void UpdateThongSo(DataTable dtChanges)
        {
            if (dtChanges == null || dtChanges.Rows.Count == 0) return;
            _dal.UpdateThongSo_Batch(dtChanges);
        }
    }
}
