
/* =========================================================
   B?NG TRA C?U (Lookup Tables)
   ========================================================= */

-- 1. Vai trò
IF OBJECT_ID(N'dbo.VaiTro', N'U') IS NULL
CREATE TABLE dbo.VaiTro (
    VaiTroID   NVARCHAR(50)  PRIMARY KEY,
    TenVaiTro  NVARCHAR(100) NOT NULL UNIQUE
);

-- 2. Phòng ban
IF OBJECT_ID(N'dbo.PhongBan', N'U') IS NULL
CREATE TABLE dbo.PhongBan (
    PhongBanID   NVARCHAR(50)  PRIMARY KEY,
    TenPhongBan  NVARCHAR(100) NOT NULL UNIQUE
);

-- 3. K? h?n h?p ??ng
IF OBJECT_ID(N'dbo.KyHanHopDong', N'U') IS NULL
CREATE TABLE dbo.KyHanHopDong (
    KyHanID   NVARCHAR(50)  PRIMARY KEY,
    TenKyHan  NVARCHAR(100) NOT NULL UNIQUE
);

-- 4. Tr?ng thái ??n hàng
IF OBJECT_ID(N'dbo.TrangThaiDonHang', N'U') IS NULL
CREATE TABLE dbo.TrangThaiDonHang (
    TrangThaiID   NVARCHAR(50)  PRIMARY KEY,
    TenTrangThai  NVARCHAR(100) NOT NULL UNIQUE
);

-- 5. Lo?i v? trí
IF OBJECT_ID(N'dbo.LoaiViTri', N'U') IS NULL
CREATE TABLE dbo.LoaiViTri (
    LoaiViTriID NVARCHAR(50)  PRIMARY KEY,
    TenLoai     NVARCHAR(100) NOT NULL UNIQUE
);

-- 6. Lo?i ch? tiêu
IF OBJECT_ID(N'dbo.LoaiChiTieu', N'U') IS NULL
CREATE TABLE dbo.LoaiChiTieu (
    LoaiChiTieuID NVARCHAR(50)  PRIMARY KEY,
    TenChiTieu    NVARCHAR(100) NOT NULL UNIQUE
);

-- 7. ??n v?
IF OBJECT_ID(N'dbo.DonVi', N'U') IS NULL
CREATE TABLE dbo.DonVi (
    DonViID   NVARCHAR(50)  PRIMARY KEY,
    TenDonVi  NVARCHAR(100) NOT NULL UNIQUE
);

-- 8. Lo?i phân tích
IF OBJECT_ID(N'dbo.LoaiPhanTich', N'U') IS NULL
CREATE TABLE dbo.LoaiPhanTich (
    LoaiPhanTichID NVARCHAR(50)  PRIMARY KEY,
    TenLoai        NVARCHAR(100) NOT NULL UNIQUE
);

-- 9. Lo?i thông báo
IF OBJECT_ID(N'dbo.LoaiThongBao', N'U') IS NULL
CREATE TABLE dbo.LoaiThongBao (
    LoaiThongBaoID NVARCHAR(50)  PRIMARY KEY,
    TenLoai        NVARCHAR(100) NOT NULL UNIQUE
);

GO
/* =========================================================
   B?NG NG??I DÙNG & KHÁCH HÀNG
   ========================================================= */

-- 10. Ng??i dùng
IF OBJECT_ID(N'dbo.NguoiDung', N'U') IS NULL
CREATE TABLE dbo.NguoiDung (
    NguoiDungID        NVARCHAR(50)  PRIMARY KEY,
    TenDangNhap        NVARCHAR(100) NOT NULL UNIQUE,
    MatKhauHash        NVARCHAR(255) NOT NULL,
    HoVaTen            NVARCHAR(200) NOT NULL,
    DienThoai          NVARCHAR(20)  NULL,
    Email              NVARCHAR(100) NULL,
    VaiTroID           NVARCHAR(50)  NOT NULL,
    PhongBanID         NVARCHAR(50)  NULL,
    IsActive           BIT           NOT NULL DEFAULT(1),
    SoLanDangNhapSai   INT           NOT NULL DEFAULT(0),
    BiKhoaDen          DATETIME      NULL,
    LanDangNhapCuoi    DATETIME      NULL,
    CONSTRAINT FK_NguoiDung_VaiTro    FOREIGN KEY (VaiTroID)   REFERENCES dbo.VaiTro(VaiTroID),
    CONSTRAINT FK_NguoiDung_PhongBan  FOREIGN KEY (PhongBanID) REFERENCES dbo.PhongBan(PhongBanID)
);

-- 11. Khách hàng
IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
CREATE TABLE dbo.KhachHang (
    KhachHangID   NVARCHAR(50)  PRIMARY KEY,
    MaKhachHang   NVARCHAR(50)  NOT NULL UNIQUE,
    TenCongTy     NVARCHAR(200) NOT NULL,
    MaSoThue      NVARCHAR(50)  NULL,
    NguoiDaiDien  NVARCHAR(200) NULL,
    DienThoai     NVARCHAR(20)  NULL,
    Email         NVARCHAR(100) NULL,
    DiaChi        NVARCHAR(500) NULL,
    GhiChu        NVARCHAR(MAX) NULL,
    IsDeleted     BIT           NOT NULL DEFAULT(0),
    DeletedAt     DATETIME      NULL
);

GO
/* =========================================================
   B?NG H?P ??NG & ??N HÀNG
   ========================================================= */

-- 12. H?p ??ng
IF OBJECT_ID(N'dbo.HopDong', N'U') IS NULL
CREATE TABLE dbo.HopDong (
    HopDongID     NVARCHAR(50)  PRIMARY KEY,
    MaHopDong     NVARCHAR(50)  NOT NULL UNIQUE,
    KhachHangID   NVARCHAR(50)  NOT NULL,
    NgayKy        DATE          NOT NULL,
    KyHanID       NVARCHAR(50)  NOT NULL,
    NgayBatDau    DATE          NULL,
    NgayKetThuc   DATE          NULL,
    TrangThai     NVARCHAR(50)  NULL,
    GhiChu        NVARCHAR(MAX) NULL,
    IsDeleted     BIT           NOT NULL DEFAULT(0),
    DeletedAt     DATETIME      NULL,
    CONSTRAINT FK_HopDong_KhachHang FOREIGN KEY (KhachHangID) REFERENCES dbo.KhachHang(KhachHangID),
    CONSTRAINT FK_HopDong_KyHan     FOREIGN KEY (KyHanID)     REFERENCES dbo.KyHanHopDong(KyHanID)
);

-- 13. ??n hàng
IF OBJECT_ID(N'dbo.DonHang', N'U') IS NULL
CREATE TABLE dbo.DonHang (
    DonHangID              NVARCHAR(50)  PRIMARY KEY,
    MaDonHang              NVARCHAR(50)  NOT NULL UNIQUE,
    HopDongID              NVARCHAR(50)  NOT NULL,
    NgayTao                DATETIME      NOT NULL DEFAULT(GETDATE()),
    NgayLayMau             DATE          NULL,
    NgayDuKienTraKetQua    DATE          NULL,
    NgayTraThucTe          DATE          NULL,
    Ky                     NVARCHAR(50)  NULL,
    TrangThaiID            NVARCHAR(50)  NOT NULL,
    GhiChu                 NVARCHAR(MAX) NULL,
    IsDeleted              BIT           NOT NULL DEFAULT(0),
    DeletedAt              DATETIME      NULL,
    CONSTRAINT FK_DonHang_HopDong   FOREIGN KEY (HopDongID)   REFERENCES dbo.HopDong(HopDongID),
    CONSTRAINT FK_DonHang_TrangThai FOREIGN KEY (TrangThaiID) REFERENCES dbo.TrangThaiDonHang(TrangThaiID)
);

GO
/* =========================================================
   B?NG V? TRÍ & THÔNG S? MÔI TR??NG
   ========================================================= */

-- 14. V? trí l?y m?u
IF OBJECT_ID(N'dbo.ViTriLayMau', N'U') IS NULL
CREATE TABLE dbo.ViTriLayMau (
    ViTriID      NVARCHAR(50)  PRIMARY KEY,
    DonHangID    NVARCHAR(50)  NOT NULL,
    MaViTri      NVARCHAR(50)  NULL,
    TenViTri     NVARCHAR(200) NOT NULL,
    LoaiViTriID  NVARCHAR(50)  NOT NULL,
    DiaChi       NVARCHAR(500) NULL,
    CONSTRAINT FK_ViTriLayMau_DonHang   FOREIGN KEY (DonHangID)   REFERENCES dbo.DonHang(DonHangID),
    CONSTRAINT FK_ViTriLayMau_LoaiViTri FOREIGN KEY (LoaiViTriID) REFERENCES dbo.LoaiViTri(LoaiViTriID)
);

-- 15. Thông s? môi tr??ng
IF OBJECT_ID(N'dbo.ThongSoMoiTruong', N'U') IS NULL
CREATE TABLE dbo.ThongSoMoiTruong (
    ThongSoID        NVARCHAR(50)  PRIMARY KEY,
    ViTriID          NVARCHAR(50)  NOT NULL,
    MaThongSo        NVARCHAR(50)  NULL,
    TenThongSo       NVARCHAR(200) NOT NULL,
    LoaiChiTieuID    NVARCHAR(50)  NULL,
    DonViID          NVARCHAR(50)  NULL,
    LoaiPhanTichID   NVARCHAR(50)  NULL,
    GiaTri           NVARCHAR(200) NULL,
    GiaTriSo         FLOAT         NULL,
    GiaTriQuyChuan   NVARCHAR(100) NULL,
    KetLuan          NVARCHAR(MAX) NULL,
    NguoiPhanTichID  NVARCHAR(50)  NULL,
    ThauPhuID        NVARCHAR(50)  NULL,
    CONSTRAINT FK_ThongSo_ViTri         FOREIGN KEY (ViTriID)        REFERENCES dbo.ViTriLayMau(ViTriID),
    CONSTRAINT FK_ThongSo_LoaiChiTieu   FOREIGN KEY (LoaiChiTieuID)  REFERENCES dbo.LoaiChiTieu(LoaiChiTieuID),
    CONSTRAINT FK_ThongSo_DonVi         FOREIGN KEY (DonViID)        REFERENCES dbo.DonVi(DonViID),
    CONSTRAINT FK_ThongSo_LoaiPhanTich  FOREIGN KEY (LoaiPhanTichID) REFERENCES dbo.LoaiPhanTich(LoaiPhanTichID),
    CONSTRAINT FK_ThongSo_NguoiPhanTich FOREIGN KEY (NguoiPhanTichID)REFERENCES dbo.NguoiDung(NguoiDungID),
    CONSTRAINT FK_ThongSo_ThauPhu       FOREIGN KEY (ThauPhuID)      REFERENCES dbo.NguoiDung(NguoiDungID)
);

GO
/* =========================================================
   B?NG K?T QU? & THÔNG BÁO
   ========================================================= */

-- 16. T?p k?t qu?
IF OBJECT_ID(N'dbo.TapKetQua', N'U') IS NULL
CREATE TABLE dbo.TapKetQua (
    KetQuaID      NVARCHAR(50)  PRIMARY KEY,
    DonHangID     NVARCHAR(50)  NOT NULL,
    TenFile       NVARCHAR(255) NOT NULL,
    DuongDan      NVARCHAR(500) NULL,
    LoaiFile      NVARCHAR(50)  NULL,
    GeneratedBy   NVARCHAR(50)  NULL,
    GeneratedAt   DATETIME      NOT NULL DEFAULT(GETDATE()),
    TrangThaiXuat NVARCHAR(50)  NULL,
    CONSTRAINT FK_TapKetQua_DonHang  FOREIGN KEY (DonHangID)  REFERENCES dbo.DonHang(DonHangID),
    CONSTRAINT FK_TapKetQua_NguoiDung FOREIGN KEY (GeneratedBy) REFERENCES dbo.NguoiDung(NguoiDungID)
);

-- 17. Thông báo
IF OBJECT_ID(N'dbo.ThongBao', N'U') IS NULL
CREATE TABLE dbo.ThongBao (
    ThongBaoID      NVARCHAR(50)  PRIMARY KEY,
    DonHangID       NVARCHAR(50)  NULL,
    NoiDung         NVARCHAR(MAX) NOT NULL,
    LoaiThongBaoID  NVARCHAR(50)  NULL,
    CreatedAt       DATETIME      NOT NULL DEFAULT(GETDATE()),
    CreatedBy       NVARCHAR(50)  NULL,
    IsRead          BIT           NOT NULL DEFAULT(0),
    ReadAt          DATETIME      NULL,
    CONSTRAINT FK_ThongBao_DonHang      FOREIGN KEY (DonHangID)      REFERENCES dbo.DonHang(DonHangID),
    CONSTRAINT FK_ThongBao_LoaiThongBao FOREIGN KEY (LoaiThongBaoID) REFERENCES dbo.LoaiThongBao(LoaiThongBaoID),
    CONSTRAINT FK_ThongBao_NguoiDung    FOREIGN KEY (CreatedBy)      REFERENCES dbo.NguoiDung(NguoiDungID)
);

GO
/* =========================================================
   B?NG AUDIT & L?CH S?
   ========================================================= */

-- 18. Nh?t ký
IF OBJECT_ID(N'dbo.NhatKy', N'U') IS NULL
CREATE TABLE dbo.NhatKy (
    NhatKyID    NVARCHAR(50)  PRIMARY KEY,
    NguoiDungID NVARCHAR(50)  NULL,
    HanhDong    NVARCHAR(200) NOT NULL,
    TenThucThe  NVARCHAR(100) NULL,
    ThucTheID   NVARCHAR(50)  NULL,
    ChiTiet     NVARCHAR(MAX) NULL,
    CreatedAt   DATETIME      NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT FK_NhatKy_NguoiDung FOREIGN KEY (NguoiDungID) REFERENCES dbo.NguoiDung(NguoiDungID)
);

-- 19. L?ch s? tìm ki?m
IF OBJECT_ID(N'dbo.LichSuTimKiem', N'U') IS NULL
CREATE TABLE dbo.LichSuTimKiem (
    TimKiemID       NVARCHAR(50)  PRIMARY KEY,
    NguoiDungID     NVARCHAR(50)  NULL,
    NoiDungTimKiem  NVARCHAR(500) NOT NULL,
    KieuTimKiem     NVARCHAR(20)  NULL,
    CreatedAt       DATETIME      NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT FK_TimKiem_NguoiDung FOREIGN KEY (NguoiDungID) REFERENCES dbo.NguoiDung(NguoiDungID)
);

GO
/* =========================================================
   B?NG MÔ HÌNH AI
   ========================================================= */

-- 20. Mô hình AI
IF OBJECT_ID(N'dbo.MoHinhAI', N'U') IS NULL
CREATE TABLE dbo.MoHinhAI (
    MoHinhID    NVARCHAR(50)  PRIMARY KEY,
    TenMoHinh   NVARCHAR(200) NOT NULL,
    MoTa        NVARCHAR(MAX) NULL,
    ChuSoHuuID  NVARCHAR(50)  NULL,
    IsActive    BIT           NOT NULL DEFAULT(1),
    TaoLuc      DATETIME      NOT NULL DEFAULT(GETDATE()),
    CapNhatLuc  DATETIME      NULL
);

-- 21. Phiên b?n mô hình
IF OBJECT_ID(N'dbo.PhienBanMoHinh', N'U') IS NULL
CREATE TABLE dbo.PhienBanMoHinh (
    PhienBanID       NVARCHAR(50)  PRIMARY KEY,
    MoHinhID         NVARCHAR(50)  NOT NULL,
    TagPhienBan      NVARCHAR(50)  NOT NULL,
    Framework        NVARCHAR(100) NULL,
    DuongDanArtefact NVARCHAR(500) NULL,
    MetricsSummary   NVARCHAR(MAX) NULL,
    DuocTrien        BIT           NOT NULL DEFAULT(0),
    TaoLuc           DATETIME      NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT FK_PhienBan_MoHinh FOREIGN KEY (MoHinhID) REFERENCES dbo.MoHinhAI(MoHinhID)
);

-- 22. D? ?oán
IF OBJECT_ID(N'dbo.DuDoan', N'U') IS NULL
CREATE TABLE dbo.DuDoan (
    DuDoanID        NVARCHAR(50)  PRIMARY KEY,
    PhienBanID      NVARCHAR(50)  NOT NULL,
    LoaiThucThe     NVARCHAR(50)  NOT NULL,
    ThucTheID       NVARCHAR(50)  NOT NULL,
    SuKienMucTieu   NVARCHAR(50)  NOT NULL,
    NhanDuDoan      NVARCHAR(200) NOT NULL,
    DiemDuDoan      FLOAT         NULL,
    GiaiThich       NVARCHAR(MAX) NULL,
    ThoiGianDuDoan  DATETIME      NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT FK_DuDoan_PhienBan FOREIGN KEY (PhienBanID) REFERENCES dbo.PhienBanMoHinh(PhienBanID)
);
GO

/* =========================================================
   INDEX T?I ?U
   ========================================================= */
CREATE INDEX IX_HopDong_KhachHangID           ON dbo.HopDong(KhachHangID);
CREATE INDEX IX_DonHang_HopDongID             ON dbo.DonHang(HopDongID);
CREATE INDEX IX_DonHang_TrangThaiID           ON dbo.DonHang(TrangThaiID);
CREATE INDEX IX_ViTriLayMau_DonHangID         ON dbo.ViTriLayMau(DonHangID);
CREATE INDEX IX_ThongSoMoiTruong_ViTriID      ON dbo.ThongSoMoiTruong(ViTriID);
CREATE INDEX IX_TapKetQua_DonHangID           ON dbo.TapKetQua(DonHangID);
CREATE INDEX IX_ThongBao_DonHangID            ON dbo.ThongBao(DonHangID);
CREATE INDEX IX_ThongBao_IsRead               ON dbo.ThongBao(IsRead);
CREATE INDEX IX_NhatKy_NguoiDungID            ON dbo.NhatKy(NguoiDungID);
CREATE INDEX IX_NhatKy_CreatedAt              ON dbo.NhatKy(CreatedAt);
CREATE INDEX IX_DonHang_NgayLayMau            ON dbo.DonHang(NgayLayMau);
CREATE INDEX IX_DonHang_NgayDuKienTraKetQua   ON dbo.DonHang(NgayDuKienTraKetQua);
CREATE INDEX IX_KhachHang_IsDeleted           ON dbo.KhachHang(IsDeleted);
CREATE INDEX IX_HopDong_IsDeleted             ON dbo.HopDong(IsDeleted);
CREATE INDEX IX_DonHang_IsDeleted             ON dbo.DonHang(IsDeleted);
GO

/* =========================================================
   SEED D? LI?U LOOKUP
   ========================================================= */
IF NOT EXISTS (SELECT 1 FROM dbo.VaiTro)
BEGIN
    INSERT INTO dbo.VaiTro (VaiTroID, TenVaiTro) VALUES
    ('VT001', N'Admin'),
    ('VT002', N'Nhân viên'),
    ('VT003', N'Qu?n lý'),
    ('VT004', N'K? thu?t viên');
END

IF NOT EXISTS (SELECT 1 FROM dbo.PhongBan)
BEGIN
    INSERT INTO dbo.PhongBan (PhongBanID, TenPhongBan) VALUES
    ('PB001', N'Kinh doanh'),
    ('PB002', N'K? ho?ch'),
    ('PB003', N'Hi?n tr??ng'),
    ('PB004', N'Thí nghi?m'),
    ('PB005', N'K?t qu?');
END

IF NOT EXISTS (SELECT 1 FROM dbo.KyHanHopDong)
BEGIN
    INSERT INTO dbo.KyHanHopDong (KyHanID, TenKyHan) VALUES
    ('KH001', N'Quý'),
    ('KH002', N'6 tháng'),
    ('KH003', N'1 n?m');
END

IF NOT EXISTS (SELECT 1 FROM dbo.TrangThaiDonHang)
BEGIN
    INSERT INTO dbo.TrangThaiDonHang (TrangThaiID, TenTrangThai) VALUES
    ('TT001', N'?ang x? lý'),
    ('TT002', N'Hoàn thành'),
    ('TT003', N'Quá h?n'),
    ('TT004', N'H?y');
END

IF NOT EXISTS (SELECT 1 FROM dbo.LoaiViTri)
BEGIN
    INSERT INTO dbo.LoaiViTri (LoaiViTriID, TenLoai) VALUES
    ('LVT001', N'??t'),
    ('LVT002', N'N??c'),
    ('LVT003', N'Không khí'),
    ('LVT004', N'Khí th?i');
END

IF NOT EXISTS (SELECT 1 FROM dbo.LoaiChiTieu)
BEGIN
    INSERT INTO dbo.LoaiChiTieu (LoaiChiTieuID, TenChiTieu) VALUES
    ('LCT001', N'SO2'),
    ('LCT002', N'NO2'),
    ('LCT003', N'CO'),
    ('LCT004', N'B?i'),
    ('LCT005', N'BOD'),
    ('LCT006', N'COD'),
    ('LCT007', N'pH');
END

IF NOT EXISTS (SELECT 1 FROM dbo.DonVi)
BEGIN
    INSERT INTO dbo.DonVi (DonViID, TenDonVi) VALUES
    ('DV001', N'µg/m³'),
    ('DV002', N'mg/L'),
    ('DV003', N'°C'),
    ('DV004', N'%'),
    ('DV005', N'ppm');
END

IF NOT EXISTS (SELECT 1 FROM dbo.LoaiPhanTich)
BEGIN
    INSERT INTO dbo.LoaiPhanTich (LoaiPhanTichID, TenLoai) VALUES
    ('LPA001', N'Hi?n tr??ng'),
    ('LPA002', N'Thí nghi?m');
END

IF NOT EXISTS (SELECT 1 FROM dbo.LoaiThongBao)
BEGIN
    INSERT INTO dbo.LoaiThongBao (LoaiThongBaoID, TenLoai) VALUES
    ('LTB001', N'Nh?c h?n h?p ??ng'),
    ('LTB002', N'??n hàng s?p ??n h?n'),
    ('LTB003', N'Quá h?n'),
    ('LTB004', N'C?nh báo v??t ng??ng'),
    ('LTB005', N'Thông báo h? th?ng');
END
GO

PRINT N'?ã t?o CSDL & schema.';
PRINT N'T?ng s? b?ng: 22';
GO

/* =========================================================
   STORED PROCEDURE — NGHI?P V?
   (?ã “??i/gi?” theo ph?n b?n ?ang dev)
   ========================================================= */

-- 1) ??ng nh?p ng??i dùng (THÊM L?I theo b?n b?n ?ang dev)
CREATE  PROCEDURE dbo.sp_DangNhapNguoiDung
    @TenDangNhap NVARCHAR(100),
    @MatKhau     NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserID NVARCHAR(50),
            @MatKhauHash NVARCHAR(255),
            @IsActive BIT,
            @SoLanSai INT,
            @BiKhoaDen DATETIME;

    SELECT 
        @UserID      = NguoiDungID,
        @MatKhauHash = MatKhauHash,
        @IsActive    = IsActive,
        @SoLanSai    = SoLanDangNhapSai,
        @BiKhoaDen   = BiKhoaDen
    FROM dbo.NguoiDung
    WHERE TenDangNhap = @TenDangNhap;

    IF @UserID IS NULL
    BEGIN
        RAISERROR(N'Tên ??ng nh?p không t?n t?i!', 16, 1); RETURN;
    END

    IF @IsActive = 0
    BEGIN
        RAISERROR(N'Tài kho?n ?ã b? vô hi?u hóa!', 16, 1); RETURN;
    END

    IF @BiKhoaDen IS NOT NULL AND @BiKhoaDen > GETDATE()
    BEGIN
        DECLARE @timeleft INT = DATEDIFF(MINUTE, GETDATE(), @BiKhoaDen);
        RAISERROR(N'Tài kho?n b? khóa t?m th?i! Vui lòng th? l?i sau %d phút.', 16, 1, @timeleft);
        RETURN;
    END

    IF @MatKhauHash <> @MatKhau
    BEGIN
        UPDATE dbo.NguoiDung
        SET SoLanDangNhapSai = SoLanDangNhapSai + 1
        WHERE NguoiDungID = @UserID;

        IF (SELECT SoLanDangNhapSai FROM dbo.NguoiDung WHERE NguoiDungID = @UserID) >= 5
        BEGIN
            UPDATE dbo.NguoiDung
            SET BiKhoaDen = DATEADD(MINUTE, 10, GETDATE()), SoLanDangNhapSai = 0
            WHERE NguoiDungID = @UserID;

            RAISERROR(N'B?n ?ã nh?p sai quá 5 l?n! Tài kho?n b? khóa 10 phút.', 16, 1);
            RETURN;
        END

        RAISERROR(N'M?t kh?u không ?úng!', 16, 1); RETURN;
    END

    UPDATE dbo.NguoiDung
    SET SoLanDangNhapSai = 0,
        LanDangNhapCuoi = GETDATE()
    WHERE NguoiDungID = @UserID;

    SELECT NguoiDungID, HoVaTen, VaiTroID, PhongBanID, Email, DienThoai
    FROM dbo.NguoiDung
    WHERE NguoiDungID = @UserID;
END
GO

-- 2) Thêm ng??i dùng
CREATE  PROCEDURE dbo.sp_ThemNguoiDung
    @TenDangNhap NVARCHAR(100),
    @MatKhauHash NVARCHAR(255),
    @HoVaTen     NVARCHAR(200),
    @DienThoai   NVARCHAR(20)  = NULL,
    @Email       NVARCHAR(100) = NULL,
    @VaiTroID    NVARCHAR(50),
    @PhongBanID  NVARCHAR(50)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

        -- Ki?m tra Vai trò t?n t?i
        IF NOT EXISTS (SELECT 1 FROM dbo.VaiTro WHERE VaiTroID = @VaiTroID)
        BEGIN
            RAISERROR(N'Vai trò không t?n t?i!', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- Ki?m tra trùng tên ??ng nh?p
        IF EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE TenDangNhap = @TenDangNhap)
        BEGIN
            RAISERROR(N'Tên ??ng nh?p ?ã t?n t?i!', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- === Sinh mã NguoiDungID d?ng NV001, NV002... ===
        DECLARE @NextID NVARCHAR(10);
        SELECT @NextID = 'NV' + RIGHT('000' + CAST(ISNULL(MAX(TRY_CAST(SUBSTRING(NguoiDungID, 3, LEN(NguoiDungID)-2) AS INT)), 0) + 1 AS NVARCHAR(3)), 3)
        FROM dbo.NguoiDung;

        -- N?u b?ng tr?ng
        IF @NextID IS NULL SET @NextID = 'NV001';

        -- Thêm ng??i dùng
        INSERT INTO dbo.NguoiDung (NguoiDungID, TenDangNhap, MatKhauHash, HoVaTen,
                                   DienThoai, Email, VaiTroID, PhongBanID)
        VALUES (@NextID, @TenDangNhap, @MatKhauHash, @HoVaTen,
                @DienThoai, @Email, @VaiTroID, @PhongBanID);

        COMMIT;
        PRINT N'Thêm ng??i dùng thành công! ID = ' + @NextID;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO


-- 3) Thêm khách hàng (??I THEO B?N M?I: t? sinh KhachHangID KH0001)
CREATE  PROCEDURE dbo.sp_ThemKhachHang
    @MaKhachHang NVARCHAR(50),
    @TenCongTy   NVARCHAR(200),
    @MaSoThue    NVARCHAR(50)  = NULL,
    @NguoiDaiDien NVARCHAR(200)= NULL,
    @DienThoai   NVARCHAR(20)  = NULL,
    @Email       NVARCHAR(100) = NULL,
    @DiaChi      NVARCHAR(500) = NULL,
    @GhiChu      NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

        IF EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaKhachHang = @MaKhachHang AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Mã khách hàng ?ã t?n t?i!', 16, 1);
            ROLLBACK; RETURN;
        END

        DECLARE @NewID NVARCHAR(50), @MaxID INT;
        SELECT @MaxID = MAX(CAST(SUBSTRING(KhachHangID, 3, LEN(KhachHangID) - 2) AS INT))
        FROM dbo.KhachHang WHERE KhachHangID LIKE 'KH%';
        IF @MaxID IS NULL SET @MaxID = 0;
        SET @NewID = 'KH' + RIGHT('0000' + CAST(@MaxID + 1 AS NVARCHAR(4)), 4);

        INSERT INTO dbo.KhachHang
            (KhachHangID, MaKhachHang, TenCongTy, MaSoThue, NguoiDaiDien, DienThoai, Email, DiaChi, GhiChu)
        VALUES
            (@NewID, @MaKhachHang, @TenCongTy, @MaSoThue, @NguoiDaiDien, @DienThoai, @Email, @DiaChi, @GhiChu);

        COMMIT;
        PRINT N'Thêm khách hàng thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 4) C?p nh?t khách hàng (B? SUNG)
CREATE  PROCEDURE dbo.sp_KhachHang_Update
    @KhachHangID NVARCHAR(50),
    @MaKhachHang NVARCHAR(50),
    @TenCongTy   NVARCHAR(200),
    @MaSoThue    NVARCHAR(50)=NULL,
    @NguoiDaiDien NVARCHAR(200)=NULL,
    @DienThoai   NVARCHAR(20)=NULL,
    @Email       NVARCHAR(100)=NULL,
    @DiaChi      NVARCHAR(500)=NULL,
    @GhiChu      NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        UPDATE dbo.KhachHang
        SET MaKhachHang=@MaKhachHang,
            TenCongTy=@TenCongTy,
            MaSoThue=@MaSoThue,
            NguoiDaiDien=@NguoiDaiDien,
            DienThoai=@DienThoai,
            Email=@Email,
            DiaChi=@DiaChi,
            GhiChu=@GhiChu
        WHERE KhachHangID=@KhachHangID AND IsDeleted=0;

        IF @@ROWCOUNT=0 THROW 50010, N'C?p nh?t KH th?t b?i (không tìm th?y).', 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- 5) L?y danh sách khách hàng (B? SUNG)
CREATE  PROCEDURE dbo.sp_LayDanhSachKhachHang
AS
BEGIN
  SET NOCOUNT ON;
  SELECT KhachHangID, MaKhachHang, TenCongTy, MaSoThue, NguoiDaiDien,
         DienThoai, Email, DiaChi, GhiChu
  FROM dbo.KhachHang
  WHERE IsDeleted = 0
  ORDER BY TenCongTy;
END
GO

-- 6) Xóa khách hàng (m?m/c?ng, có cascade) (B? SUNG)
CREATE  PROCEDURE dbo.sp_XoaKhachHang
    @KhachHangID NVARCHAR(50),
    @XoaCung BIT = 0,
    @Cascade BIT = 1,
    @NguoiDungID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;

        IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE KhachHangID = @KhachHangID)
            RAISERROR(N'Khách hàng không t?n t?i!', 16, 1);

        IF @XoaCung = 1
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.HopDong WHERE KhachHangID = @KhachHangID)
                RAISERROR(N'Không th? xóa c?ng vì còn H?p ??ng liên quan. Hãy xóa H?p ??ng tr??c.', 16, 1);

            DELETE FROM dbo.KhachHang WHERE KhachHangID = @KhachHangID;

            IF @NguoiDungID IS NOT NULL
                INSERT INTO dbo.NhatKy (NhatKyID,NguoiDungID,HanhDong,TenThucThe,ThucTheID,ChiTiet,CreatedAt)
                VALUES (NEWID(), @NguoiDungID, N'Xóa KH (c?ng)', N'KhachHang', @KhachHangID,
                        N'Xóa c?ng khách hàng ' + @KhachHangID, GETDATE());
        END
        ELSE
        BEGIN
            UPDATE dbo.KhachHang
            SET IsDeleted = 1, DeletedAt = GETDATE()
            WHERE KhachHangID = @KhachHangID;

            IF @Cascade = 1
            BEGIN
                UPDATE dbo.HopDong
                SET IsDeleted = 1, DeletedAt = GETDATE()
                WHERE KhachHangID = @KhachHangID;

                UPDATE dbo.DonHang
                SET IsDeleted = 1, DeletedAt = GETDATE()
                WHERE HopDongID IN (SELECT HD.HopDongID FROM dbo.HopDong AS HD WHERE HD.KhachHangID = @KhachHangID);
            END

            IF @NguoiDungID IS NOT NULL
                INSERT INTO dbo.NhatKy (NhatKyID,NguoiDungID,HanhDong,TenThucThe,ThucTheID,ChiTiet,CreatedAt)
                VALUES (NEWID(), @NguoiDungID, N'Xóa KH (m?m)', N'KhachHang', @KhachHangID,
                        N'Xóa m?m khách hàng ' + @KhachHangID + N' (Cascade=' + CAST(@Cascade AS NVARCHAR(1)) + N')',
                        GETDATE());
        END

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 7) Thêm h?p ??ng
CREATE  PROCEDURE dbo.sp_ThemHopDong
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
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE KhachHangID = @KhachHangID AND IsDeleted = 0)
            RAISERROR(N'Khách hàng không t?n t?i!', 16, 1);
        ELSE IF EXISTS (SELECT 1 FROM dbo.HopDong WHERE MaHopDong = @MaHopDong AND IsDeleted = 0)
            RAISERROR(N'Mã h?p ??ng ?ã t?n t?i!', 16, 1);
        ELSE IF NOT EXISTS (SELECT 1 FROM dbo.KyHanHopDong WHERE KyHanID = @KyHanID)
            RAISERROR(N'K? h?n không h?p l?!', 16, 1);
        ELSE
        BEGIN
            INSERT INTO dbo.HopDong
                (HopDongID, MaHopDong, KhachHangID, NgayKy, KyHanID, NgayBatDau, NgayKetThuc, TrangThai, GhiChu)
            VALUES
                (@HopDongID, @MaHopDong, @KhachHangID, @NgayKy, @KyHanID, @NgayBatDau, @NgayKetThuc, @TrangThai, @GhiChu);

            IF @NguoiDungID IS NOT NULL
                INSERT INTO dbo.NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet)
                VALUES (NEWID(), @NguoiDungID, N'Thêm h?p ??ng', N'HopDong', @HopDongID, N'Thêm h?p ??ng: ' + @MaHopDong);
        END
        COMMIT;
        PRINT N'Thêm h?p ??ng thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 8) Thêm ??n hàng
CREATE  PROCEDURE dbo.sp_ThemDonHang
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
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.HopDong WHERE HopDongID = @HopDongID AND IsDeleted = 0)
            RAISERROR(N'H?p ??ng không t?n t?i!', 16, 1);
        ELSE IF EXISTS (SELECT 1 FROM dbo.DonHang WHERE MaDonHang = @MaDonHang AND IsDeleted = 0)
            RAISERROR(N'Mã ??n hàng ?ã t?n t?i!', 16, 1);
        ELSE
        BEGIN
            INSERT INTO dbo.DonHang
                (DonHangID, MaDonHang, HopDongID, NgayLayMau, NgayDuKienTraKetQua, Ky, TrangThaiID, GhiChu)
            VALUES
                (@DonHangID, @MaDonHang, @HopDongID, @NgayLayMau, @NgayDuKienTraKetQua, @Ky, @TrangThaiID, @GhiChu);

            IF @NguoiDungID IS NOT NULL
                INSERT INTO dbo.NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet)
                VALUES (NEWID(), @NguoiDungID, N'Thêm ??n hàng', N'DonHang', @DonHangID, N'Thêm ??n hàng: ' + @MaDonHang);
        END
        COMMIT;
        PRINT N'Thêm ??n hàng thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO


-- 9) Thêm v? trí l?y m?u
CREATE  PROCEDURE dbo.sp_ThemViTriLayMau
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
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.DonHang WHERE DonHangID = @DonHangID AND IsDeleted = 0)
            RAISERROR(N'??n hàng không t?n t?i!', 16, 1);
        ELSE IF NOT EXISTS (SELECT 1 FROM dbo.LoaiViTri WHERE LoaiViTriID = @LoaiViTriID)
            RAISERROR(N'Lo?i v? trí không h?p l?!', 16, 1);
        ELSE
        BEGIN
            INSERT INTO dbo.ViTriLayMau (ViTriID, DonHangID, MaViTri, TenViTri, LoaiViTriID, DiaChi)
            VALUES (@ViTriID, @DonHangID, @MaViTri, @TenViTri, @LoaiViTriID, @DiaChi);
        END
        COMMIT;
        PRINT N'Thêm v? trí l?y m?u thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 10) Thêm thông s? môi tr??ng
CREATE PROCEDURE dbo.sp_ThemThongSoMoiTruong
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
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.ViTriLayMau WHERE ViTriID = @ViTriID)
            RAISERROR(N'V? trí l?y m?u không t?n t?i!', 16, 1);
        ELSE
        BEGIN
            INSERT INTO dbo.ThongSoMoiTruong
                (ThongSoID, ViTriID, MaThongSo, TenThongSo, LoaiChiTieuID, DonViID, LoaiPhanTichID,
                 GiaTri, GiaTriSo, GiaTriQuyChuan, KetLuan, NguoiPhanTichID, ThauPhuID)
            VALUES
                (@ThongSoID, @ViTriID, @MaThongSo, @TenThongSo, @LoaiChiTieuID, @DonViID, @LoaiPhanTichID,
                 @GiaTri, @GiaTriSo, @GiaTriQuyChuan, @KetLuan, @NguoiPhanTichID, @ThauPhuID);
        END
        COMMIT;
        PRINT N'Thêm thông s? môi tr??ng thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 11) Thêm t?p k?t qu?
CREATE PROCEDURE dbo.sp_ThemTapKetQua
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
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.DonHang WHERE DonHangID = @DonHangID AND IsDeleted = 0)
            RAISERROR(N'??n hàng không t?n t?i!', 16, 1);
        ELSE
        BEGIN
            DECLARE @SoThongSoChuaNhap INT;
            SELECT @SoThongSoChuaNhap = COUNT(*)
            FROM dbo.ViTriLayMau v
            INNER JOIN dbo.ThongSoMoiTruong ts ON v.ViTriID = ts.ViTriID
            WHERE v.DonHangID = @DonHangID 
              AND ts.GiaTri IS NULL 
              AND ts.GiaTriSo IS NULL;

            IF @SoThongSoChuaNhap > 0
                RAISERROR(N'Còn thông s? môi tr??ng ch?a nh?p giá tr?! Không th? xu?t k?t qu?.', 16, 1);
            ELSE
            BEGIN
                INSERT INTO dbo.TapKetQua
                    (KetQuaID, DonHangID, TenFile, DuongDan, LoaiFile, GeneratedBy, TrangThaiXuat)
                VALUES
                    (@KetQuaID, @DonHangID, @TenFile, @DuongDan, @LoaiFile, @GeneratedBy, @TrangThaiXuat);

                IF @GeneratedBy IS NOT NULL
                    INSERT INTO dbo.NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet)
                    VALUES (NEWID(), @GeneratedBy, N'Xu?t k?t qu?', N'TapKetQua', @KetQuaID,
                            N'Xu?t file: ' + @TenFile + N' cho ??n hàng: ' + @DonHangID);
            END
        END
        COMMIT;
        PRINT N'Thêm t?p k?t qu? thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 12) Thêm thông báo
CREATE  PROCEDURE dbo.sp_ThemThongBao
    @ThongBaoID NVARCHAR(50),
    @DonHangID NVARCHAR(50) = NULL,
    @NoiDung NVARCHAR(MAX),
    @LoaiThongBaoID NVARCHAR(50) = NULL,
    @CreatedBy NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;
        INSERT INTO dbo.ThongBao (ThongBaoID, DonHangID, NoiDung, LoaiThongBaoID, CreatedBy)
        VALUES (@ThongBaoID, @DonHangID, @NoiDung, @LoaiThongBaoID, @CreatedBy);
        COMMIT;
        PRINT N'Thêm thông báo thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO


-- 13) Ghi nh?t ký
CREATE  PROCEDURE dbo.sp_GhiNhatKy
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
        INSERT INTO dbo.NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet)
        VALUES (@NhatKyID, @NguoiDungID, @HanhDong, @TenThucThe, @ThucTheID, @ChiTiet);
        PRINT N'Ghi nh?t ký thành công!';
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO


-- 14) Thêm mô hình AI
CREATE  PROCEDURE dbo.sp_ThemMoHinhAI
    @MoHinhID NVARCHAR(50),
    @TenMoHinh NVARCHAR(200),
    @MoTa NVARCHAR(MAX) = NULL,
    @ChuSoHuuID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;
        INSERT INTO dbo.MoHinhAI (MoHinhID, TenMoHinh, MoTa, ChuSoHuuID)
        VALUES (@MoHinhID, @TenMoHinh, @MoTa, @ChuSoHuuID);
        COMMIT;
        PRINT N'Thêm mô hình AI thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 15) Thêm phiên b?n mô hình
CREATE  PROCEDURE dbo.sp_ThemPhienBanMoHinh
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
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.MoHinhAI WHERE MoHinhID = @MoHinhID)
            RAISERROR(N'Mô hình AI không t?n t?i!', 16, 1);
        ELSE
        BEGIN
            INSERT INTO dbo.PhienBanMoHinh
                (PhienBanID, MoHinhID, TagPhienBan, Framework, DuongDanArtefact, MetricsSummary, DuocTrien)
            VALUES
                (@PhienBanID, @MoHinhID, @TagPhienBan, @Framework, @DuongDanArtefact, @MetricsSummary, @DuocTrien);
        END
        COMMIT;
        PRINT N'Thêm phiên b?n mô hình thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO


-- 16) Thêm d? ?oán
CREATE  PROCEDURE dbo.sp_ThemDuDoan
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
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.PhienBanMoHinh WHERE PhienBanID = @PhienBanID)
            RAISERROR(N'Phiên b?n mô hình không t?n t?i!', 16, 1);
        ELSE
        BEGIN
            INSERT INTO dbo.DuDoan
                (DuDoanID, PhienBanID, LoaiThucThe, ThucTheID, SuKienMucTieu, NhanDuDoan, DiemDuDoan, GiaiThich)
            VALUES
                (@DuDoanID, @PhienBanID, @LoaiThucThe, @ThucTheID, @SuKienMucTieu, @NhanDuDoan, @DiemDuDoan, @GiaiThich);
        END
        COMMIT;
        PRINT N'Thêm d? ?oán thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO

-- 17) Tìm ki?m khách hàng có phân trang
CREATE  PROCEDURE dbo.sp_GetKhachHang
    @Keyword  NVARCHAR(200) = NULL,
    @Page     INT = 1,
    @PageSize INT = 50
AS
BEGIN
    SET NOCOUNT ON;

    WITH Q AS (
        SELECT
            KhachHangID, MaKhachHang, TenCongTy, MaSoThue, NguoiDaiDien,
            DienThoai, Email, DiaChi, GhiChu,
            ROW_NUMBER() OVER (ORDER BY TenCongTy) AS rn
        FROM dbo.KhachHang
        WHERE IsDeleted = 0
          AND (
               @Keyword IS NULL OR @Keyword = N'' OR
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

    SELECT COUNT(*) AS Total
    FROM dbo.KhachHang
    WHERE IsDeleted = 0
      AND (
           @Keyword IS NULL OR @Keyword = N'' OR
           TenCongTy   LIKE N'%' + @Keyword + N'%' OR
           MaKhachHang LIKE N'%' + @Keyword + N'%' OR
           MaSoThue    LIKE N'%' + @Keyword + N'%' OR
           DienThoai   LIKE N'%' + @Keyword + N'%' OR
           Email       LIKE N'%' + @Keyword + N'%'
      );
END
GO

PRINT N'? ?Ã T?O STORED PROCEDURES (?ã ??ng b? v?i ph?n b?n ?ang dev).';
GO

/* =========================================================
   (TÙY CH?N) SEED USER M?U
   ========================================================= */
IF NOT EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE TenDangNhap = N'admin')
INSERT INTO dbo.NguoiDung (NguoiDungID, TenDangNhap, MatKhauHash, HoVaTen, DienThoai, Email, VaiTroID, PhongBanID, IsActive)
VALUES (NEWID(), N'admin', N'123456', N'Qu?n tr? viên h? th?ng', N'0909123456', N'admin@greensol.vn', N'VT001', N'PB001', 1);

IF NOT EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE TenDangNhap = N'52300220')
INSERT INTO dbo.NguoiDung (NguoiDungID, TenDangNhap, MatKhauHash, HoVaTen, DienThoai, Email, VaiTroID, PhongBanID, IsActive)
VALUES (NEWID(), N'52300220', N'123456', N'Th?ng làm', N'0345696717', N'admin@greensol.vn', N'VT001', N'PB001', 1);
GO
INSERT [dbo].[KhachHang] ([KhachHangID], [MaKhachHang], [TenCongTy], [MaSoThue], [NguoiDaiDien], [DienThoai], [Email], [DiaChi], [GhiChu], [IsDeleted], [DeletedAt]) VALUES (N'KH0002', N'C002', N'Công ty C? ph?n ABC', N'0301123456', N'Tr?n Th? B', N'0912345678', N'info@abc.vn', N'45 Lý Th??ng Ki?t, Qu?n 10, TP.HCM', N'ch?c ko', 0, NULL)
GO
INSERT [dbo].[KhachHang] ([KhachHangID], [MaKhachHang], [TenCongTy], [MaSoThue], [NguoiDaiDien], [DienThoai], [Email], [DiaChi], [GhiChu], [IsDeleted], [DeletedAt]) VALUES (N'KH0003', N'C003', N'Công ty TNHH TechSmart', N'0109876543', N'Lê Minh C', N'0987654321', N'lm.c@techsmart.vn', N'25 Nguy?n V?n Linh, Qu?n 7, TP.HCM', N'Khách hàng doanh nghi?p', 0, NULL)
GO
INSERT [dbo].[KhachHang] ([KhachHangID], [MaKhachHang], [TenCongTy], [MaSoThue], [NguoiDaiDien], [DienThoai], [Email], [DiaChi], [GhiChu], [IsDeleted], [DeletedAt]) VALUES (N'KH0004', N'C004', N'Công ty TNHH Hòa Bình', N'0405566778', N'Ph?m Ng?c D', N'0977123456', N'sales@hoabinh.com', N'78 Hoàng Di?u, Qu?n H?i Châu, ?à N?ng', N'Khách hàng mi?n Trung', 0, NULL)
GO

PRINT N'?? Hoàn t?t h?p nh?t schema + SP. Format s?ch, ch?y ???c ngay.';
GO

CREATE  PROCEDURE dbo.sp_GetAllNguoiDung
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ND.NguoiDungID,
        ND.TenDangNhap,
        ND.HoVaTen,
        ND.DienThoai,
        ND.Email,
        ND.IsActive,
        ND.LanDangNhapCuoi,
        VT.VaiTroID,
        VT.TenVaiTro,
        PB.PhongBanID,
        PB.TenPhongBan
    FROM dbo.NguoiDung AS ND
    LEFT JOIN dbo.VaiTro AS VT ON ND.VaiTroID = VT.VaiTroID
    LEFT JOIN dbo.PhongBan AS PB ON ND.PhongBanID = PB.PhongBanID
    ORDER BY ND.NguoiDungID;
END
GO
CREATE  PROCEDURE dbo.sp_GetAllVaiTro
AS
BEGIN
    SET NOCOUNT ON;
    SELECT VaiTroID, TenVaiTro
    FROM dbo.VaiTro
    ORDER BY TenVaiTro;
END
GO

/* ================================
   L?Y DANH SÁCH PHÒNG BAN
   ================================ */
CREATE  PROCEDURE dbo.sp_GetAllPhongBan
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PhongBanID, TenPhongBan
    FROM dbo.PhongBan
    ORDER BY TenPhongBan;
END
GO
CREATE  PROCEDURE dbo.sp_SuaNguoiDung
    @NguoiDungID NVARCHAR(50),
    @TenDangNhap NVARCHAR(100),
    @MatKhauHash NVARCHAR(255),
    @HoVaTen     NVARCHAR(200),
    @DienThoai   NVARCHAR(20)  = NULL,
    @Email       NVARCHAR(100) = NULL,
    @VaiTroID    NVARCHAR(50),
    @PhongBanID  NVARCHAR(50)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

        -- Ki?m tra t?n t?i
        IF NOT EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE NguoiDungID = @NguoiDungID)
        BEGIN
            RAISERROR(N'Ng??i dùng không t?n t?i!', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- Ki?m tra Vai trò t?n t?i
        IF NOT EXISTS (SELECT 1 FROM dbo.VaiTro WHERE VaiTroID = @VaiTroID)
        BEGIN
            RAISERROR(N'Vai trò không t?n t?i!', 16, 1);
            ROLLBACK;
            RETURN;
        END

        UPDATE dbo.NguoiDung
        SET 
            TenDangNhap = @TenDangNhap,
            MatKhauHash = @MatKhauHash,
            HoVaTen = @HoVaTen,
            DienThoai = @DienThoai,
            Email = @Email,
            VaiTroID = @VaiTroID,
            PhongBanID = @PhongBanID
        WHERE NguoiDungID = @NguoiDungID;

        COMMIT;
        PRINT N'C?p nh?t ng??i dùng thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO
CREATE  PROCEDURE dbo.sp_XoaNguoiDung
    @NguoiDungID NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

        IF NOT EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE NguoiDungID = @NguoiDungID)
        BEGIN
            RAISERROR(N'Ng??i dùng không t?n t?i!', 16, 1);
            ROLLBACK;
            RETURN;
        END

        DELETE FROM dbo.NguoiDung WHERE NguoiDungID = @NguoiDungID;

        COMMIT;
        PRINT N'?ã xóa ng??i dùng thành công!';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO
CREATE  PROCEDURE dbo.sp_TimKiemNguoiDung
    @Keyword NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ND.NguoiDungID,
        ND.TenDangNhap,
        ND.HoVaTen,
        ND.DienThoai,
        ND.Email,
        ND.IsActive,
        ND.LanDangNhapCuoi,
        VT.VaiTroID,
        VT.TenVaiTro,
        PB.PhongBanID,
        PB.TenPhongBan
    FROM dbo.NguoiDung AS ND
    LEFT JOIN dbo.VaiTro AS VT ON ND.VaiTroID = VT.VaiTroID
    LEFT JOIN dbo.PhongBan AS PB ON ND.PhongBanID = PB.PhongBanID
    WHERE 
        @Keyword IS NULL OR @Keyword = N'' OR
        ND.TenDangNhap LIKE N'%' + @Keyword + N'%' OR
        ND.HoVaTen LIKE N'%' + @Keyword + N'%' OR
        ND.DienThoai LIKE N'%' + @Keyword + N'%' OR
        ND.Email LIKE N'%' + @Keyword + N'%' OR
        VT.TenVaiTro LIKE N'%' + @Keyword + N'%' OR
        PB.TenPhongBan LIKE N'%' + @Keyword + N'%'
    ORDER BY ND.HoVaTen;
END
GO

/* ================================
   L?Y DANH SÁCH H?P ??NG
   ================================ */

CREATE PROCEDURE dbo.sp_GetAllHopDong
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        HD.HopDongID,
        HD.MaHopDong,
        HD.KhachHangID,
        KH.MaKhachHang,
        KH.TenCongTy,
        HD.NgayKy,
        HD.KyHanID,
        KHAN.TenKyHan,
        HD.NgayBatDau,
        HD.NgayKetThuc,
        HD.TrangThai,
        HD.GhiChu,
        HD.IsDeleted,
        HD.DeletedAt
    FROM dbo.HopDong AS HD
    INNER JOIN dbo.KhachHang AS KH
        ON KH.KhachHangID = HD.KhachHangID
       AND KH.IsDeleted = 0           -- ki?m tra khóa ph? khách hàng còn hi?u l?c
    LEFT JOIN dbo.KyHanHopDong AS KHAN
        ON KHAN.KyHanID = HD.KyHanID
    WHERE HD.IsDeleted = 0
    ORDER BY HD.NgayKy DESC, HD.MaHopDong;
END
GO

-- S?A H?P ??NG

CREATE  PROCEDURE dbo.sp_HopDong_Update
    @HopDongID     NVARCHAR(50),
    @MaHopDong     NVARCHAR(50),
    @KhachHangID   NVARCHAR(50),
    @NgayKy        DATE,
    @KyHanID       NVARCHAR(50),
    @NgayBatDau    DATE          = NULL,
    @NgayKetThuc   DATE          = NULL,
    @TrangThai     NVARCHAR(50)  = NULL,
    @GhiChu        NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON; 
    SET XACT_ABORT ON;
    BEGIN TRY
        -- Ki?m tra FK: Khách hàng còn t?n t?i và ch?a b? xóa m?m
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.KhachHang 
            WHERE KhachHangID = @KhachHangID AND IsDeleted = 0
        )
            RAISERROR(N'Khách hàng không t?n t?i ho?c ?ã b? xóa!', 16, 1);

        -- Ki?m tra FK: K? h?n h?p l?
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.KyHanHopDong 
            WHERE KyHanID = @KyHanID
        )
            RAISERROR(N'K? h?n không h?p l?!', 16, 1);

        -- Ki?m tra trùng mã h?p ??ng (tr? chính nó)
        IF EXISTS (
            SELECT 1 
            FROM dbo.HopDong 
            WHERE MaHopDong = @MaHopDong 
              AND HopDongID <> @HopDongID
              AND IsDeleted = 0
        )
            RAISERROR(N'Mã h?p ??ng ?ã t?n t?i!', 16, 1);

        UPDATE dbo.HopDong
        SET MaHopDong   = @MaHopDong,
            KhachHangID = @KhachHangID,
            NgayKy      = @NgayKy,
            KyHanID     = @KyHanID,
            NgayBatDau  = @NgayBatDau,
            NgayKetThuc = @NgayKetThuc,
            TrangThai   = @TrangThai,
            GhiChu      = @GhiChu
        WHERE HopDongID = @HopDongID
          AND IsDeleted = 0;

        IF @@ROWCOUNT = 0 
            THROW 50020, N'C?p nh?t H?p ??ng th?t b?i (không tìm th?y ho?c ?ã b? xóa).', 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO


-- XÓA H?P ??NG (m?m/c?ng, có cascade ??n hàng)

CREATE PROCEDURE dbo.sp_XoaHopDong
    @HopDongID   NVARCHAR(50),
    @XoaCung     BIT = 0,
    @Cascade     BIT = 1,
    @NguoiDungID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON; 
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;

        -- T?n t?i H?p ??ng?
        IF NOT EXISTS (SELECT 1 FROM dbo.HopDong WHERE HopDongID = @HopDongID)
            RAISERROR(N'H?p ??ng không t?n t?i!', 16, 1);

        IF @XoaCung = 1
        BEGIN
            -- Không xóa c?ng khi còn ??n hàng liên quan
            IF EXISTS (SELECT 1 FROM dbo.DonHang WHERE HopDongID = @HopDongID)
                RAISERROR(N'Không th? xóa c?ng vì còn ??n hàng liên quan. Hãy xóa ??n hàng tr??c.', 16, 1);

            DELETE FROM dbo.HopDong WHERE HopDongID = @HopDongID;

            IF @NguoiDungID IS NOT NULL
                INSERT INTO dbo.NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet, CreatedAt)
                VALUES (NEWID(), @NguoiDungID, N'Xóa H? (c?ng)', N'HopDong', @HopDongID,
                        N'Xóa c?ng H?p ??ng ' + @HopDongID, GETDATE());
        END
        ELSE
        BEGIN
            -- Xóa m?m H?p ??ng
            UPDATE dbo.HopDong
            SET IsDeleted = 1, DeletedAt = GETDATE()
            WHERE HopDongID = @HopDongID;

            -- Cascade xóa m?m ??n hàng thu?c H?p ??ng
            IF @Cascade = 1
            BEGIN
                UPDATE dbo.DonHang
                SET IsDeleted = 1, DeletedAt = GETDATE()
                WHERE DonHangID IN (
                    SELECT DH.DonHangID
                    FROM dbo.DonHang AS DH
                    WHERE DH.HopDongID = @HopDongID
                );
            END

            IF @NguoiDungID IS NOT NULL
                INSERT INTO dbo.NhatKy (NhatKyID, NguoiDungID, HanhDong, TenThucThe, ThucTheID, ChiTiet, CreatedAt)
                VALUES (NEWID(), @NguoiDungID, N'Xóa H? (m?m)', N'HopDong', @HopDongID,
                        N'Xóa m?m H?p ??ng ' + @HopDongID + N' (Cascade=' + CAST(@Cascade AS NVARCHAR(1)) + N')',
                        GETDATE());
        END

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH
END
GO


/* ================================
   L?Y DANH SÁCH K? H?N
   ================================ */

CREATE  PROCEDURE dbo.sp_GetAllKyHanHopDong
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        KyHanID,
        TenKyHan
    FROM dbo.KyHanHopDong
    ORDER BY TenKyHan;
END
GO

-- insert into b?ng h?p ??ng

INSERT INTO dbo.HopDong
    (HopDongID, MaHopDong, KhachHangID, NgayKy, KyHanID, NgayBatDau, NgayKetThuc, TrangThai, GhiChu)
VALUES
    (N'HD0001', N'HD-2025-001', N'KH0002', '2025-01-15', N'KH001', '2025-01-15', '2025-04-15', N'Hi?u l?c',     N'H?p ??ng chu k? Quý'),
    (N'HD0002', N'HD-2025-002', N'KH0003', '2025-02-10', N'KH002', '2025-02-10', '2025-08-10', N'Hi?u l?c',     N'H?p ??ng 6 tháng'),
    (N'HD0003', N'HD-2025-003', N'KH0004', '2025-03-05', N'KH003', '2025-03-05', '2026-03-05', N'Chu?n b? hi?u l?c', N'H?p ??ng 1 n?m');