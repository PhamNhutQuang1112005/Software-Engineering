create database SQL
-- =============================================
-- BẢNG TRA CỨU (Lookup Tables)
-- =============================================

-- 1. Bảng Vai trò
CREATE TABLE VaiTro (
    VaiTroID NVARCHAR(50) PRIMARY KEY,
    TenVaiTro NVARCHAR(100) NOT NULL UNIQUE
);

-- 2. Bảng Phòng ban
CREATE TABLE PhongBan (
    PhongBanID NVARCHAR(50) PRIMARY KEY,
    TenPhongBan NVARCHAR(100) NOT NULL UNIQUE
);

-- 3. Bảng Kỳ hạn hợp đồng
CREATE TABLE KyHanHopDong (
    KyHanID NVARCHAR(50) PRIMARY KEY,
    TenKyHan NVARCHAR(100) NOT NULL UNIQUE
);

-- 4. Bảng Trạng thái đơn hàng
CREATE TABLE TrangThaiDonHang (
    TrangThaiID NVARCHAR(50) PRIMARY KEY,
    TenTrangThai NVARCHAR(100) NOT NULL UNIQUE
);

-- 5. Bảng Loại vị trí
CREATE TABLE LoaiViTri (
    LoaiViTriID NVARCHAR(50) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL UNIQUE
);

-- 6. Bảng Loại chỉ tiêu
CREATE TABLE LoaiChiTieu (
    LoaiChiTieuID NVARCHAR(50) PRIMARY KEY,
    TenChiTieu NVARCHAR(100) NOT NULL UNIQUE
);

-- 7. Bảng Đơn vị
CREATE TABLE DonVi (
    DonViID NVARCHAR(50) PRIMARY KEY,
    TenDonVi NVARCHAR(100) NOT NULL UNIQUE
);

-- 8. Bảng Loại phân tích
CREATE TABLE LoaiPhanTich (
    LoaiPhanTichID NVARCHAR(50) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL UNIQUE
);

-- 9. Bảng Loại thông báo
CREATE TABLE LoaiThongBao (
    LoaiThongBaoID NVARCHAR(50) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL UNIQUE
);

-- =============================================
-- BẢNG NGƯỜI DÙNG VÀ KHÁCH HÀNG
-- =============================================

-- 10. Bảng Người dùng
CREATE TABLE NguoiDung (
    NguoiDungID NVARCHAR(50) PRIMARY KEY,
    TenDangNhap NVARCHAR(100) NOT NULL UNIQUE,
    MatKhauHash NVARCHAR(255) NOT NULL,
    HoVaTen NVARCHAR(200) NOT NULL,
    DienThoai NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    VaiTroID NVARCHAR(50) NOT NULL,
    PhongBanID NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    SoLanDangNhapSai INT NOT NULL DEFAULT 0,
    BiKhoaDen DATETIME NULL,
    LanDangNhapCuoi DATETIME NULL,
    CONSTRAINT FK_NguoiDung_VaiTro FOREIGN KEY (VaiTroID) REFERENCES VaiTro(VaiTroID),
    CONSTRAINT FK_NguoiDung_PhongBan FOREIGN KEY (PhongBanID) REFERENCES PhongBan(PhongBanID)
);

-- 11. Bảng Khách hàng
CREATE TABLE KhachHang (
    KhachHangID NVARCHAR(50) PRIMARY KEY,
    MaKhachHang NVARCHAR(50) NOT NULL UNIQUE,
    TenCongTy NVARCHAR(200) NOT NULL,
    MaSoThue NVARCHAR(50) NULL,
    NguoiDaiDien NVARCHAR(200) NULL,
    DienThoai NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    DiaChi NVARCHAR(500) NULL,
    GhiChu NVARCHAR(MAX) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME NULL
);

-- =============================================
-- BẢNG HỢP ĐỒNG VÀ ĐƠN HÀNG
-- =============================================

-- 12. Bảng Hợp đồng
CREATE TABLE HopDong (
    HopDongID NVARCHAR(50) PRIMARY KEY,
    MaHopDong NVARCHAR(50) NOT NULL UNIQUE,
    KhachHangID NVARCHAR(50) NOT NULL,
    NgayKy DATE NOT NULL,
    KyHanID NVARCHAR(50) NOT NULL,
    NgayBatDau DATE NULL,
    NgayKetThuc DATE NULL,
    TrangThai NVARCHAR(50) NULL,
    GhiChu NVARCHAR(MAX) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_HopDong_KhachHang FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID),
    CONSTRAINT FK_HopDong_KyHan FOREIGN KEY (KyHanID) REFERENCES KyHanHopDong(KyHanID)
);

-- 13. Bảng Đơn hàng
CREATE TABLE DonHang (
    DonHangID NVARCHAR(50) PRIMARY KEY,
    MaDonHang NVARCHAR(50) NOT NULL UNIQUE,
    HopDongID NVARCHAR(50) NOT NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayLayMau DATE NULL,
    NgayDuKienTraKetQua DATE NULL,
    NgayTraThucTe DATE NULL,
    Ky NVARCHAR(50) NULL,
    TrangThaiID NVARCHAR(50) NOT NULL,
    GhiChu NVARCHAR(MAX) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME NULL,
    CONSTRAINT FK_DonHang_HopDong FOREIGN KEY (HopDongID) REFERENCES HopDong(HopDongID),
    CONSTRAINT FK_DonHang_TrangThai FOREIGN KEY (TrangThaiID) REFERENCES TrangThaiDonHang(TrangThaiID)
);

-- =============================================
-- BẢNG VỊ TRÍ VÀ THÔNG SỐ MÔI TRƯỜNG
-- =============================================

-- 14. Bảng Vị trí lấy mẫu
CREATE TABLE ViTriLayMau (
    ViTriID NVARCHAR(50) PRIMARY KEY,
    DonHangID NVARCHAR(50) NOT NULL,
    MaViTri NVARCHAR(50) NULL,
    TenViTri NVARCHAR(200) NOT NULL,
    LoaiViTriID NVARCHAR(50) NOT NULL,
    DiaChi NVARCHAR(500) NULL,
    CONSTRAINT FK_ViTriLayMau_DonHang FOREIGN KEY (DonHangID) REFERENCES DonHang(DonHangID),
    CONSTRAINT FK_ViTriLayMau_LoaiViTri FOREIGN KEY (LoaiViTriID) REFERENCES LoaiViTri(LoaiViTriID)
);

-- 15. Bảng Thông số môi trường
CREATE TABLE ThongSoMoiTruong (
    ThongSoID NVARCHAR(50) PRIMARY KEY,
    ViTriID NVARCHAR(50) NOT NULL,
    MaThongSo NVARCHAR(50) NULL,
    TenThongSo NVARCHAR(200) NOT NULL,
    LoaiChiTieuID NVARCHAR(50) NULL,
    DonViID NVARCHAR(50) NULL,
    LoaiPhanTichID NVARCHAR(50) NULL,
    GiaTri NVARCHAR(200) NULL,
    GiaTriSo FLOAT NULL,
    GiaTriQuyChuan NVARCHAR(100) NULL,
    KetLuan NVARCHAR(MAX) NULL,
    NguoiPhanTichID NVARCHAR(50) NULL,
    ThauPhuID NVARCHAR(50) NULL,
    CONSTRAINT FK_ThongSo_ViTri FOREIGN KEY (ViTriID) REFERENCES ViTriLayMau(ViTriID),
    CONSTRAINT FK_ThongSo_LoaiChiTieu FOREIGN KEY (LoaiChiTieuID) REFERENCES LoaiChiTieu(LoaiChiTieuID),
    CONSTRAINT FK_ThongSo_DonVi FOREIGN KEY (DonViID) REFERENCES DonVi(DonViID),
    CONSTRAINT FK_ThongSo_LoaiPhanTich FOREIGN KEY (LoaiPhanTichID) REFERENCES LoaiPhanTich(LoaiPhanTichID),
    CONSTRAINT FK_ThongSo_NguoiPhanTich FOREIGN KEY (NguoiPhanTichID) REFERENCES NguoiDung(NguoiDungID),
    CONSTRAINT FK_ThongSo_ThauPhu FOREIGN KEY (ThauPhuID) REFERENCES NguoiDung(NguoiDungID)
);

-- =============================================
-- BẢNG KẾT QUẢ VÀ THÔNG BÁO
-- =============================================

-- 16. Bảng Tập kết quả
CREATE TABLE TapKetQua (
    KetQuaID NVARCHAR(50) PRIMARY KEY,
    DonHangID NVARCHAR(50) NOT NULL,
    TenFile NVARCHAR(255) NOT NULL,
    DuongDan NVARCHAR(500) NULL,
    LoaiFile NVARCHAR(50) NULL,
    GeneratedBy NVARCHAR(50) NULL,
    GeneratedAt DATETIME NOT NULL DEFAULT GETDATE(),
    TrangThaiXuat NVARCHAR(50) NULL,
    CONSTRAINT FK_TapKetQua_DonHang FOREIGN KEY (DonHangID) REFERENCES DonHang(DonHangID),
    CONSTRAINT FK_TapKetQua_NguoiDung FOREIGN KEY (GeneratedBy) REFERENCES NguoiDung(NguoiDungID)
);

-- 17. Bảng Thông báo
CREATE TABLE ThongBao (
    ThongBaoID NVARCHAR(50) PRIMARY KEY,
    DonHangID NVARCHAR(50) NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    LoaiThongBaoID NVARCHAR(50) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(50) NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    ReadAt DATETIME NULL,
    CONSTRAINT FK_ThongBao_DonHang FOREIGN KEY (DonHangID) REFERENCES DonHang(DonHangID),
    CONSTRAINT FK_ThongBao_LoaiThongBao FOREIGN KEY (LoaiThongBaoID) REFERENCES LoaiThongBao(LoaiThongBaoID),
    CONSTRAINT FK_ThongBao_NguoiDung FOREIGN KEY (CreatedBy) REFERENCES NguoiDung(NguoiDungID)
);

-- =============================================
-- BẢNG AUDIT VÀ LỊCH SỬ
-- =============================================

-- 18. Bảng Nhật ký hoạt động
CREATE TABLE NhatKy (
    NhatKyID NVARCHAR(50) PRIMARY KEY,
    NguoiDungID NVARCHAR(50) NULL,
    HanhDong NVARCHAR(200) NOT NULL,
    TenThucThe NVARCHAR(100) NULL,
    ThucTheID NVARCHAR(50) NULL,
    ChiTiet NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_NhatKy_NguoiDung FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID)
);

-- 19. Bảng Lịch sử tìm kiếm
CREATE TABLE LichSuTimKiem (
    TimKiemID NVARCHAR(50) PRIMARY KEY,
    NguoiDungID NVARCHAR(50) NULL,
    NoiDungTimKiem NVARCHAR(500) NOT NULL,
    KieuTimKiem NVARCHAR(20) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TimKiem_NguoiDung FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID)
);

-- =============================================
-- BẢNG MÔ HÌNH AI
-- =============================================

-- 20. Bảng Mô hình AI
CREATE TABLE MoHinhAI (
    MoHinhID NVARCHAR(50) PRIMARY KEY,
    TenMoHinh NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    ChuSoHuuID NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    TaoLuc DATETIME NOT NULL DEFAULT GETDATE(),
    CapNhatLuc DATETIME NULL
);

-- 21. Bảng Phiên bản mô hình
CREATE TABLE PhienBanMoHinh (
    PhienBanID NVARCHAR(50) PRIMARY KEY,
    MoHinhID NVARCHAR(50) NOT NULL,
    TagPhienBan NVARCHAR(50) NOT NULL,
    Framework NVARCHAR(100) NULL,
    DuongDanArtefact NVARCHAR(500) NULL,
    MetricsSummary NVARCHAR(MAX) NULL,
    DuocTrien BIT NOT NULL DEFAULT 0,
    TaoLuc DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_PhienBan_MoHinh FOREIGN KEY (MoHinhID) REFERENCES MoHinhAI(MoHinhID)
);

-- 22. Bảng Dự đoán
CREATE TABLE DuDoan (
    DuDoanID NVARCHAR(50) PRIMARY KEY,
    PhienBanID NVARCHAR(50) NOT NULL,
    LoaiThucThe NVARCHAR(50) NOT NULL,
    ThucTheID NVARCHAR(50) NOT NULL,
    SuKienMucTieu NVARCHAR(50) NOT NULL,
    NhanDuDoan NVARCHAR(200) NOT NULL,
    DiemDuDoan FLOAT NULL,
    GiaiThich NVARCHAR(MAX) NULL,
    ThoiGianDuDoan DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_DuDoan_PhienBan FOREIGN KEY (PhienBanID) REFERENCES PhienBanMoHinh(PhienBanID)
);

-- =============================================
-- TẠO INDEX ĐỂ TỐI ƯU HIỆU SUẤT
-- =============================================

-- Index cho các khóa ngoại thường xuyên truy vấn
CREATE INDEX IX_HopDong_KhachHangID ON HopDong(KhachHangID);
CREATE INDEX IX_DonHang_HopDongID ON DonHang(HopDongID);
CREATE INDEX IX_DonHang_TrangThaiID ON DonHang(TrangThaiID);
CREATE INDEX IX_ViTriLayMau_DonHangID ON ViTriLayMau(DonHangID);
CREATE INDEX IX_ThongSoMoiTruong_ViTriID ON ThongSoMoiTruong(ViTriID);
CREATE INDEX IX_TapKetQua_DonHangID ON TapKetQua(DonHangID);
CREATE INDEX IX_ThongBao_DonHangID ON ThongBao(DonHangID);
CREATE INDEX IX_ThongBao_IsRead ON ThongBao(IsRead);
CREATE INDEX IX_NhatKy_NguoiDungID ON NhatKy(NguoiDungID);
CREATE INDEX IX_NhatKy_CreatedAt ON NhatKy(CreatedAt);

-- Index cho các trường tìm kiếm
CREATE INDEX IX_DonHang_NgayLayMau ON DonHang(NgayLayMau);
CREATE INDEX IX_DonHang_NgayDuKienTraKetQua ON DonHang(NgayDuKienTraKetQua);
CREATE INDEX IX_KhachHang_IsDeleted ON KhachHang(IsDeleted);
CREATE INDEX IX_HopDong_IsDeleted ON HopDong(IsDeleted);
CREATE INDEX IX_DonHang_IsDeleted ON DonHang(IsDeleted);

-- =============================================
-- THÊM DỮ LIỆU MẪU CHO CÁC BẢNG TRA CỨU
-- =============================================

-- Vai trò
INSERT INTO VaiTro (VaiTroID, TenVaiTro) VALUES
('VT001', N'Admin'),
('VT002', N'Nhân viên'),
('VT003', N'Quản lý'),
('VT004', N'Kỹ thuật viên');

-- Phòng ban
INSERT INTO PhongBan (PhongBanID, TenPhongBan) VALUES
('PB001', N'Kinh doanh'),
('PB002', N'Kế hoạch'),
('PB003', N'Hiện trường'),
('PB004', N'Thí nghiệm'),
('PB005', N'Kết quả');

-- Kỳ hạn hợp đồng
INSERT INTO KyHanHopDong (KyHanID, TenKyHan) VALUES
('KH001', N'Quý'),
('KH002', N'6 tháng'),
('KH003', N'1 năm');

-- Trạng thái đơn hàng
INSERT INTO TrangThaiDonHang (TrangThaiID, TenTrangThai) VALUES
('TT001', N'Đang xử lý'),
('TT002', N'Hoàn thành'),
('TT003', N'Quá hạn'),
('TT004', N'Hủy');

-- Loại vị trí
INSERT INTO LoaiViTri (LoaiViTriID, TenLoai) VALUES
('LVT001', N'Đất'),
('LVT002', N'Nước'),
('LVT003', N'Không khí'),
('LVT004', N'Khí thải');

-- Loại chỉ tiêu
INSERT INTO LoaiChiTieu (LoaiChiTieuID, TenChiTieu) VALUES
('LCT001', N'SO2'),
('LCT002', N'NO2'),
('LCT003', N'CO'),
('LCT004', N'Bụi'),
('LCT005', N'BOD'),
('LCT006', N'COD'),
('LCT007', N'pH');

-- Đơn vị
INSERT INTO DonVi (DonViID, TenDonVi) VALUES
('DV001', N'µg/m³'),
('DV002', N'mg/L'),
('DV003', N'°C'),
('DV004', N'%'),
('DV005', N'ppm');

-- Loại phân tích
INSERT INTO LoaiPhanTich (LoaiPhanTichID, TenLoai) VALUES
('LPA001', N'Hiện trường'),
('LPA002', N'Thí nghiệm');

-- Loại thông báo
INSERT INTO LoaiThongBao (LoaiThongBaoID, TenLoai) VALUES
('LTB001', N'Nhắc hạn hợp đồng'),
('LTB002', N'Đơn hàng sắp đến hạn'),
('LTB003', N'Quá hạn'),
('LTB004', N'Cảnh báo vượt ngưỡng'),
('LTB005', N'Thông báo hệ thống');

GO

PRINT N'Đã tạo thành công cơ sở dữ liệu Quản lý Đơn hàng Quan trắc Môi trường!';
PRINT N'Tổng số bảng: 22';
PRINT N'- 9 bảng tra cứu (Lookup)';
PRINT N'- 2 bảng người dùng và khách hàng';
PRINT N'- 2 bảng hợp đồng và đơn hàng';
PRINT N'- 2 bảng vị trí và thông số môi trường';
PRINT N'- 2 bảng kết quả và thông báo';
PRINT N'- 2 bảng audit và lịch sử';
PRINT N'- 3 bảng mô hình AI';
GO
CREATE PROCEDURE sp_ThemNguoiDung
    @NguoiDungID NVARCHAR(50),
    @TenDangNhap NVARCHAR(100),
    @MatKhauHash NVARCHAR(255),
    @HoVaTen NVARCHAR(200),
    @DienThoai NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @VaiTroID NVARCHAR(50),
    @PhongBanID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra VaiTro tồn tại
        IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE VaiTroID = @VaiTroID)
        BEGIN
            RAISERROR(N'Vai trò không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra TenDangNhap đã tồn tại
        IF EXISTS (SELECT 1 FROM NguoiDung WHERE TenDangNhap = @TenDangNhap)
        BEGIN
            RAISERROR(N'Tên đăng nhập đã tồn tại!', 16, 1);
            RETURN;
        END
        
        INSERT INTO NguoiDung (
            NguoiDungID, TenDangNhap, MatKhauHash, HoVaTen, 
            DienThoai, Email, VaiTroID, PhongBanID
        )
        VALUES (
            @NguoiDungID, @TenDangNhap, @MatKhauHash, @HoVaTen,
            @DienThoai, @Email, @VaiTroID, @PhongBanID
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm người dùng thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 2. THÊM KHÁCH HÀNG =====
CREATE PROCEDURE sp_ThemKhachHang
    @KhachHangID NVARCHAR(50),
    @MaKhachHang NVARCHAR(50),
    @TenCongTy NVARCHAR(200),
    @MaSoThue NVARCHAR(50) = NULL,
    @NguoiDaiDien NVARCHAR(200) = NULL,
    @DienThoai NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @DiaChi NVARCHAR(500) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra MaKhachHang đã tồn tại
        IF EXISTS (SELECT 1 FROM KhachHang WHERE MaKhachHang = @MaKhachHang AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Mã khách hàng đã tồn tại!', 16, 1);
            RETURN;
        END
        
        INSERT INTO KhachHang (
            KhachHangID, MaKhachHang, TenCongTy, MaSoThue,
            NguoiDaiDien, DienThoai, Email, DiaChi, GhiChu
        )
        VALUES (
            @KhachHangID, @MaKhachHang, @TenCongTy, @MaSoThue,
            @NguoiDaiDien, @DienThoai, @Email, @DiaChi, @GhiChu
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm khách hàng thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 3. THÊM HỢP ĐỒNG =====
CREATE PROCEDURE sp_ThemHopDong
    @HopDongID NVARCHAR(50),
    @MaHopDong NVARCHAR(50),
    @KhachHangID NVARCHAR(50),
    @NgayKy DATE,
    @KyHanID NVARCHAR(50),
    @NgayBatDau DATE = NULL,
    @NgayKetThuc DATE = NULL,
    @TrangThai NVARCHAR(50) = NULL,
    @GhiChu NVARCHAR(MAX) = NULL,
    @NguoiDungID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra KhachHang tồn tại
        IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE KhachHangID = @KhachHangID AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Khách hàng không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra MaHopDong đã tồn tại
        IF EXISTS (SELECT 1 FROM HopDong WHERE MaHopDong = @MaHopDong AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Mã hợp đồng đã tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra KyHan tồn tại
        IF NOT EXISTS (SELECT 1 FROM KyHanHopDong WHERE KyHanID = @KyHanID)
        BEGIN
            RAISERROR(N'Kỳ hạn không hợp lệ!', 16, 1);
            RETURN;
        END
        
        INSERT INTO HopDong (
            HopDongID, MaHopDong, KhachHangID, NgayKy,
            KyHanID, NgayBatDau, NgayKetThuc, TrangThai, GhiChu
        )
        VALUES (
            @HopDongID, @MaHopDong, @KhachHangID, @NgayKy,
            @KyHanID, @NgayBatDau, @NgayKetThuc, @TrangThai, @GhiChu
        );
        
        -- Ghi nhật ký
        IF @NguoiDungID IS NOT NULL
        BEGIN
            INSERT INTO NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet)
            VALUES (NEWID(), @NguoiDungID, N'Thêm hợp đồng', N'HopDong', @HopDongID, 
                    N'Thêm hợp đồng: ' + @MaHopDong);
        END
        
        COMMIT TRANSACTION;
        PRINT N'Thêm hợp đồng thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 4. THÊM ĐƠN HÀNG =====
CREATE PROCEDURE sp_ThemDonHang
    @DonHangID NVARCHAR(50),
    @MaDonHang NVARCHAR(50),
    @HopDongID NVARCHAR(50),
    @NgayLayMau DATE = NULL,
    @NgayDuKienTraKetQua DATE = NULL,
    @Ky NVARCHAR(50) = NULL,
    @TrangThaiID NVARCHAR(50),
    @GhiChu NVARCHAR(MAX) = NULL,
    @NguoiDungID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra HopDong tồn tại
        IF NOT EXISTS (SELECT 1 FROM HopDong WHERE HopDongID = @HopDongID AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Hợp đồng không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra MaDonHang đã tồn tại
        IF EXISTS (SELECT 1 FROM DonHang WHERE MaDonHang = @MaDonHang AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Mã đơn hàng đã tồn tại!', 16, 1);
            RETURN;
        END
        
        INSERT INTO DonHang (
            DonHangID, MaDonHang, HopDongID, NgayLayMau,
            NgayDuKienTraKetQua, Ky, TrangThaiID, GhiChu
        )
        VALUES (
            @DonHangID, @MaDonHang, @HopDongID, @NgayLayMau,
            @NgayDuKienTraKetQua, @Ky, @TrangThaiID, @GhiChu
        );
        
        -- Ghi nhật ký
        IF @NguoiDungID IS NOT NULL
        BEGIN
            INSERT INTO NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet)
            VALUES (NEWID(), @NguoiDungID, N'Thêm đơn hàng', N'DonHang', @DonHangID,
                    N'Thêm đơn hàng: ' + @MaDonHang);
        END
        
        COMMIT TRANSACTION;
        PRINT N'Thêm đơn hàng thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 5. THÊM VỊ TRÍ LẤY MẪU =====
CREATE PROCEDURE sp_ThemViTriLayMau
    @ViTriID NVARCHAR(50),
    @DonHangID NVARCHAR(50),
    @MaViTri NVARCHAR(50) = NULL,
    @TenViTri NVARCHAR(200),
    @LoaiViTriID NVARCHAR(50),
    @DiaChi NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra DonHang tồn tại
        IF NOT EXISTS (SELECT 1 FROM DonHang WHERE DonHangID = @DonHangID AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Đơn hàng không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra LoaiViTri tồn tại
        IF NOT EXISTS (SELECT 1 FROM LoaiViTri WHERE LoaiViTriID = @LoaiViTriID)
        BEGIN
            RAISERROR(N'Loại vị trí không hợp lệ!', 16, 1);
            RETURN;
        END
        
        INSERT INTO ViTriLayMau (
            ViTriID, DonHangID, MaViTri, TenViTri, LoaiViTriID, DiaChi
        )
        VALUES (
            @ViTriID, @DonHangID, @MaViTri, @TenViTri, @LoaiViTriID, @DiaChi
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm vị trí lấy mẫu thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 6. THÊM THÔNG SỐ MÔI TRƯỜNG =====
CREATE PROCEDURE sp_ThemThongSoMoiTruong
    @ThongSoID NVARCHAR(50),
    @ViTriID NVARCHAR(50),
    @MaThongSo NVARCHAR(50) = NULL,
    @TenThongSo NVARCHAR(200),
    @LoaiChiTieuID NVARCHAR(50) = NULL,
    @DonViID NVARCHAR(50) = NULL,
    @LoaiPhanTichID NVARCHAR(50) = NULL,
    @GiaTri NVARCHAR(200) = NULL,
    @GiaTriSo FLOAT = NULL,
    @GiaTriQuyChuan NVARCHAR(100) = NULL,
    @KetLuan NVARCHAR(MAX) = NULL,
    @NguoiPhanTichID NVARCHAR(50) = NULL,
    @ThauPhuID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra ViTri tồn tại
        IF NOT EXISTS (SELECT 1 FROM ViTriLayMau WHERE ViTriID = @ViTriID)
        BEGIN
            RAISERROR(N'Vị trí lấy mẫu không tồn tại!', 16, 1);
            RETURN;
        END
        
        INSERT INTO ThongSoMoiTruong (
            ThongSoID, ViTriID, MaThongSo, TenThongSo,
            LoaiChiTieuID, DonViID, LoaiPhanTichID,
            GiaTri, GiaTriSo, GiaTriQuyChuan, KetLuan,
            NguoiPhanTichID, ThauPhuID
        )
        VALUES (
            @ThongSoID, @ViTriID, @MaThongSo, @TenThongSo,
            @LoaiChiTieuID, @DonViID, @LoaiPhanTichID,
            @GiaTri, @GiaTriSo, @GiaTriQuyChuan, @KetLuan,
            @NguoiPhanTichID, @ThauPhuID
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm thông số môi trường thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 7. THÊM TẬP KẾT QUẢ =====
CREATE PROCEDURE sp_ThemTapKetQua
    @KetQuaID NVARCHAR(50),
    @DonHangID NVARCHAR(50),
    @TenFile NVARCHAR(255),
    @DuongDan NVARCHAR(500) = NULL,
    @LoaiFile NVARCHAR(50) = NULL,
    @GeneratedBy NVARCHAR(50) = NULL,
    @TrangThaiXuat NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra DonHang tồn tại
        IF NOT EXISTS (SELECT 1 FROM DonHang WHERE DonHangID = @DonHangID AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Đơn hàng không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra tất cả ThongSoMoiTruong đã có giá trị
        DECLARE @SoThongSoChuaNhap INT;
        SELECT @SoThongSoChuaNhap = COUNT(*)
        FROM ViTriLayMau v
        INNER JOIN ThongSoMoiTruong ts ON v.ViTriID = ts.ViTriID
        WHERE v.DonHangID = @DonHangID 
          AND ts.GiaTri IS NULL 
          AND ts.GiaTriSo IS NULL;
        
        IF @SoThongSoChuaNhap > 0
        BEGIN
            RAISERROR(N'Còn thông số môi trường chưa nhập giá trị! Không thể xuất kết quả.', 16, 1);
            RETURN;
        END
        
        INSERT INTO TapKetQua (
            KetQuaID, DonHangID, TenFile, DuongDan,
            LoaiFile, GeneratedBy, TrangThaiXuat
        )
        VALUES (
            @KetQuaID, @DonHangID, @TenFile, @DuongDan,
            @LoaiFile, @GeneratedBy, @TrangThaiXuat
        );
        
        -- Ghi nhật ký
        IF @GeneratedBy IS NOT NULL
        BEGIN
            INSERT INTO NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet)
            VALUES (NEWID(), @GeneratedBy, N'Xuất kết quả', N'TapKetQua', @KetQuaID,
                    N'Xuất file: ' + @TenFile + N' cho đơn hàng: ' + @DonHangID);
        END
        
        COMMIT TRANSACTION;
        PRINT N'Thêm tập kết quả thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 8. THÊM THÔNG BÁO =====
CREATE PROCEDURE sp_ThemThongBao
    @ThongBaoID NVARCHAR(50),
    @DonHangID NVARCHAR(50) = NULL,
    @NoiDung NVARCHAR(MAX),
    @LoaiThongBaoID NVARCHAR(50) = NULL,
    @CreatedBy NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        INSERT INTO ThongBao (
            ThongBaoID, DonHangID, NoiDung, LoaiThongBaoID, CreatedBy
        )
        VALUES (
            @ThongBaoID, @DonHangID, @NoiDung, @LoaiThongBaoID, @CreatedBy
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm thông báo thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 9. GHI NHẬT KÝ =====
CREATE PROCEDURE sp_GhiNhatKy
    @NhatKyID NVARCHAR(50),
    @NguoiDungID NVARCHAR(50) = NULL,
    @HanhDong NVARCHAR(200),
    @TenThucThe NVARCHAR(100) = NULL,
    @ThucTheID NVARCHAR(50) = NULL,
    @ChiTiet NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO NhatKy (
            NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet
        )
        VALUES (
            @NhatKyID, @NguoiDungID, @HanhDong, @TenThucThe, @ThucTheID, @ChiTiet
        );
        
        PRINT N'Ghi nhật ký thành công!';
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 10. THÊM MÔ HÌNH AI =====
CREATE PROCEDURE sp_ThemMoHinhAI
    @MoHinhID NVARCHAR(50),
    @TenMoHinh NVARCHAR(200),
    @MoTa NVARCHAR(MAX) = NULL,
    @ChuSoHuuID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        INSERT INTO MoHinhAI (
            MoHinhID, TenMoHinh, MoTa, ChuSoHuuID
        )
        VALUES (
            @MoHinhID, @TenMoHinh, @MoTa, @ChuSoHuuID
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm mô hình AI thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 11. THÊM PHIÊN BẢN MÔ HÌNH =====
CREATE PROCEDURE sp_ThemPhienBanMoHinh
    @PhienBanID NVARCHAR(50),
    @MoHinhID NVARCHAR(50),
    @TagPhienBan NVARCHAR(50),
    @Framework NVARCHAR(100) = NULL,
    @DuongDanArtefact NVARCHAR(500) = NULL,
    @MetricsSummary NVARCHAR(MAX) = NULL,
    @DuocTrien BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra MoHinh tồn tại
        IF NOT EXISTS (SELECT 1 FROM MoHinhAI WHERE MoHinhID = @MoHinhID)
        BEGIN
            RAISERROR(N'Mô hình AI không tồn tại!', 16, 1);
            RETURN;
        END
        
        INSERT INTO PhienBanMoHinh (
            PhienBanID, MoHinhID, TagPhienBan, Framework,
            DuongDanArtefact, MetricsSummary, DuocTrien
        )
        VALUES (
            @PhienBanID, @MoHinhID, @TagPhienBan, @Framework,
            @DuongDanArtefact, @MetricsSummary, @DuocTrien
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm phiên bản mô hình thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ===== 12. THÊM DỰ ĐOÁN =====
CREATE PROCEDURE sp_ThemDuDoan
    @DuDoanID NVARCHAR(50),
    @PhienBanID NVARCHAR(50),
    @LoaiThucThe NVARCHAR(50),
    @ThucTheID NVARCHAR(50),
    @SuKienMucTieu NVARCHAR(50),
    @NhanDuDoan NVARCHAR(200),
    @DiemDuDoan FLOAT = NULL,
    @GiaiThich NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra PhienBan tồn tại
        IF NOT EXISTS (SELECT 1 FROM PhienBanMoHinh WHERE PhienBanID = @PhienBanID)
        BEGIN
            RAISERROR(N'Phiên bản mô hình không tồn tại!', 16, 1);
            RETURN;
        END
        
        INSERT INTO DuDoan (
            DuDoanID, PhienBanID, LoaiThucThe, ThucTheID,
            SuKienMucTieu, NhanDuDoan, DiemDuDoan, GiaiThich
        )
        VALUES (
            @DuDoanID, @PhienBanID, @LoaiThucThe, @ThucTheID,
            @SuKienMucTieu, @NhanDuDoan, @DiemDuDoan, @GiaiThich
        );
        
        COMMIT TRANSACTION;
        PRINT N'Thêm dự đoán thành công!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO
GO
CREATE OR ALTER PROCEDURE sp_GetKhachHang
    @Keyword NVARCHAR(200) = NULL,          -- tên/ MST/ điện thoại/ email
    @Page    INT = 1,                        -- 1-based
    @PageSize INT = 50
AS
BEGIN
    SET NOCOUNT ON;

    WITH Q AS (
        SELECT
            KhachHangID,
            MaKhachHang,
            TenCongTy,
            MaSoThue,
            NguoiDaiDien,
            DienThoai,
            Email,
            DiaChi,
            GhiChu,
            ROW_NUMBER() OVER (ORDER BY TenCongTy) AS rn
        FROM KhachHang
        WHERE IsDeleted = 0
          AND (
                @Keyword IS NULL OR
                @Keyword = N'' OR
                TenCongTy   LIKE N'%' + @Keyword + N'%' OR
                MaKhachHang LIKE N'%' + @Keyword + N'%' OR
                MaSoThue    LIKE N'%' + @Keyword + N'%' OR
                DienThoai   LIKE N'%' + @Keyword + N'%' OR
                Email       LIKE N'%' + @Keyword + N'%'
          )
    )
    SELECT *
    FROM Q
    WHERE rn BETWEEN ((@Page-1)*@PageSize + 1) AND (@Page*@PageSize);

    -- Tổng số bản ghi (để làm phân trang client)
    SELECT COUNT(*) AS Total
    FROM KhachHang
    WHERE IsDeleted = 0
      AND (
            @Keyword IS NULL OR
            @Keyword = N'' OR
            TenCongTy   LIKE N'%' + @Keyword + N'%' OR
            MaKhachHang LIKE N'%' + @Keyword + N'%' OR
            MaSoThue    LIKE N'%' + @Keyword + N'%' OR
            DienThoai   LIKE N'%' + @Keyword + N'%' OR
            Email       LIKE N'%' + @Keyword + N'%'
      );
END
GO
GO
CREATE OR ALTER PROCEDURE sp_DangNhapNguoiDung
    @TenDangNhap NVARCHAR(100),
    @MatKhau NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserID NVARCHAR(50),
            @MatKhauHash NVARCHAR(255),
            @IsActive BIT,
            @SoLanSai INT,
            @BiKhoaDen DATETIME;

    -- Kiểm tra tài khoản tồn tại
    SELECT 
        @UserID = NguoiDungID,
        @MatKhauHash = MatKhauHash,
        @IsActive = IsActive,
        @SoLanSai = SoLanDangNhapSai,
        @BiKhoaDen = BiKhoaDen
    FROM NguoiDung
    WHERE TenDangNhap = @TenDangNhap;

    IF @UserID IS NULL
    BEGIN
        RAISERROR(N'Tên đăng nhập không tồn tại!', 16, 1);
        RETURN;
    END

    -- Kiểm tra tài khoản bị khóa
    IF @IsActive = 0
    BEGIN
        RAISERROR(N'Tài khoản đã bị vô hiệu hóa!', 16, 1);
        RETURN;
    END

    IF @BiKhoaDen IS NOT NULL AND @BiKhoaDen > GETDATE()
    BEGIN
        DECLARE @timeleft INT = DATEDIFF(MINUTE, GETDATE(), @BiKhoaDen);
        RAISERROR(N'Tài khoản bị khóa tạm thời! Vui lòng thử lại sau %d phút.', 16, 1, @timeleft);
        RETURN;
    END

    -- So sánh mật khẩu (ở đây chưa hash, có thể thay bằng HASHBYTES)
    IF @MatKhauHash <> @MatKhau
    BEGIN
        UPDATE NguoiDung
        SET SoLanDangNhapSai = SoLanDangNhapSai + 1
        WHERE NguoiDungID = @UserID;

        IF (SELECT SoLanDangNhapSai FROM NguoiDung WHERE NguoiDungID = @UserID) >= 5
        BEGIN
            UPDATE NguoiDung
            SET BiKhoaDen = DATEADD(MINUTE, 10, GETDATE()), SoLanDangNhapSai = 0
            WHERE NguoiDungID = @UserID;

            RAISERROR(N'Bạn đã nhập sai quá 5 lần! Tài khoản bị khóa 10 phút.', 16, 1);
            RETURN;
        END

        RAISERROR(N'Mật khẩu không đúng!', 16, 1);
        RETURN;
    END

    -- Reset đếm sai và cập nhật lần đăng nhập cuối
    UPDATE NguoiDung
    SET SoLanDangNhapSai = 0,
        LanDangNhapCuoi = GETDATE()
    WHERE NguoiDungID = @UserID;

    -- Trả thông tin người dùng
    SELECT 
        NguoiDungID,
        HoVaTen,
        VaiTroID,
        PhongBanID,
        Email,
        DienThoai
    FROM NguoiDung
    WHERE NguoiDungID = @UserID;
END
GO
INSERT INTO NguoiDung (
    NguoiDungID,
    TenDangNhap,
    MatKhauHash,
    HoVaTen,
    DienThoai,
    Email,
    VaiTroID,
    PhongBanID,
    IsActive
)
VALUES (
    NEWID(),               -- tự sinh ID ngẫu nhiên
    N'admin',              -- tên đăng nhập
    N'123456',             -- mật khẩu test (bản rõ, không mã hóa)
    N'Quản trị viên hệ thống',
    N'0909123456',
    N'admin@greensol.vn',
    N'VT001',              -- vai trò Admin (theo bảng VaiTro)
    N'PB001',              -- phòng ban Kinh doanh (theo bảng PhongBan)
    1                      -- đang hoạt động
);
INSERT INTO NguoiDung (
    NguoiDungID,
    TenDangNhap,
    MatKhauHash,
    HoVaTen,
    DienThoai,
    Email,
    VaiTroID,
    PhongBanID,
    IsActive
)
VALUES (
    NEWID(),               -- tự sinh ID ngẫu nhiên
    N'52300220',              -- tên đăng nhập
    N'123456',             -- mật khẩu test (bản rõ, không mã hóa)
    N'Thằng làm',
    N'0345696717',        --số điện thoại
    N'admin@greensol.vn',
    N'VT001',              -- vai trò Admin (theo bảng VaiTro)
    N'PB001',              -- phòng ban Kinh doanh (theo bảng PhongBan)
    1                      -- đang hoạt động
);
