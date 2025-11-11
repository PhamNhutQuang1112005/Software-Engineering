using System;
using System.Data;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_ThongSoQuanTrac
    {
        private readonly DAL_ThongSoQuanTrac _dal = new DAL_ThongSoQuanTrac();

        // ===== Lookup (DataTable) =====
        public bool UpdateTenVaDiaChiViTri(string viTriID, string tenViTri, string diaChi)
{
    return _dal.UpdateTenVaDiaChiViTri(viTriID, tenViTri, diaChi);
}

        public DataTable GetAllLoaiChiTieu()  { return _dal.GetAllLoaiChiTieu(); }
        public DataTable GetAllDonVi()        { return _dal.GetAllDonVi(); }
        public DataTable GetAllLoaiPhanTich() { return _dal.GetAllLoaiPhanTich(); }
        public DataTable GetAllLoaiViTri()    { return _dal.GetAllLoaiViTri(); }

        // ===== Lookup (DTO) =====
        public List<DTO_LoaiChiTieu>  GetAllLoaiChiTieuDTO()  { return _dal.GetAllLoaiChiTieuDTO_(); }
        public List<DTO_DonVi>        GetAllDonViDTO()        { return _dal.GetAllDonViDTO_(); }
        public List<DTO_LoaiPhanTich> GetAllLoaiPhanTichDTO() { return _dal.GetAllLoaiPhanTichDTO_(); }
        public List<DTO_LoaiViTri>    GetAllLoaiViTriDTO()    { return _dal.GetAllLoaiViTriDTO_(); }

        // ===== Vị trí =====
        public void      EnsureViTriAndThongSo(string donHangID) { _dal.EnsureViTriAndThongSo(donHangID); }
        public DataTable GetViTriByDonHang(string donHangID)     { return _dal.GetViTriByDonHang(donHangID); }

        // ===== Loại vị trí theo Vị trí =====
        public DataTable GetLoaiViTriByViTri(string viTriID)                 { return _dal.GetLoaiViTriByViTri(viTriID); }
        public string    AddLoaiViTriToViTri(string viTriID, string tenLoai) { return _dal.AddLoaiViTriToViTri(viTriID, tenLoai); }
        public bool      DeleteLoaiViTriFromViTri(string viTriID, string loaiViTriID)
        {
            return _dal.DeleteLoaiViTriFromViTri(viTriID, loaiViTriID);
        }

        // ===== Thông số =====
        public DataTable GetThongSoByViTri(string viTriID)                          { return _dal.GetThongSoByViTri(viTriID); }
        public DataTable GetThongSoByViTriLoai(string viTriID, string loaiViTriID)  { return _dal.GetThongSoByViTriLoai(viTriID, loaiViTriID); }

        

        // BLL_ThongSoQuanTrac.cs
public string InsertThongSoMoi_ReturnKey_WithLoai(
    string viTriID, string loaiViTriID, string loaiChiTieuID, string donViID,
    string loaiPhanTichID, string nguoiPhanTichID, string thauPhuID, float? giaTriQuyChuan)
{
    return _dal.InsertThongSoMoi_ReturnKey_WithLoai(
        viTriID, loaiViTriID, loaiChiTieuID, donViID, loaiPhanTichID, nguoiPhanTichID, thauPhuID, giaTriQuyChuan);
}


        public bool  DeleteThongSo(string tenThongSo) { return _dal.DeleteThongSo(tenThongSo); }
        public void  UpdateThongSo(DataTable dtChanges) { _dal.UpdateThongSo_Batch(dtChanges); }

        public string GetDefaultDonViID_ByLoaiChiTieu(string loaiChiTieuID)
        {
            return _dal.GetDefaultDonViID_ByLoaiChiTieu(loaiChiTieuID);
        }
    }
}
