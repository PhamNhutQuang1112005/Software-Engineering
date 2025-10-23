USE [master]
GO
/****** Object:  Database [SQL1]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE DATABASE [SQL1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SQL1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SQL1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SQL1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SQL1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SQL1] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SQL1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SQL1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SQL1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SQL1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SQL1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SQL1] SET ARITHABORT OFF 
GO
ALTER DATABASE [SQL1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SQL1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SQL1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SQL1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SQL1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SQL1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SQL1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SQL1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SQL1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SQL1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SQL1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SQL1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SQL1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SQL1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SQL1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SQL1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SQL1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SQL1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SQL1] SET  MULTI_USER 
GO
ALTER DATABASE [SQL1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SQL1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SQL1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SQL1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SQL1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SQL1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SQL1] SET QUERY_STORE = ON
GO
ALTER DATABASE [SQL1] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SQL1]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[DonHangID] [nvarchar](50) NOT NULL,
	[MaDonHang] [nvarchar](50) NOT NULL,
	[HopDongID] [nvarchar](50) NOT NULL,
	[NgayTao] [datetime] NOT NULL,
	[NgayLayMau] [date] NULL,
	[NgayDuKienTraKetQua] [date] NULL,
	[NgayTraThucTe] [date] NULL,
	[Ky] [nvarchar](50) NULL,
	[TrangThaiID] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DonHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonVi]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonVi](
	[DonViID] [nvarchar](50) NOT NULL,
	[TenDonVi] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DonViID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenDonVi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DuDoan]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DuDoan](
	[DuDoanID] [nvarchar](50) NOT NULL,
	[PhienBanID] [nvarchar](50) NOT NULL,
	[LoaiThucThe] [nvarchar](50) NOT NULL,
	[ThucTheID] [nvarchar](50) NOT NULL,
	[SuKienMucTieu] [nvarchar](50) NOT NULL,
	[NhanDuDoan] [nvarchar](200) NOT NULL,
	[DiemDuDoan] [float] NULL,
	[GiaiThich] [nvarchar](max) NULL,
	[ThoiGianDuDoan] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DuDoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HopDong]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopDong](
	[HopDongID] [nvarchar](50) NOT NULL,
	[MaHopDong] [nvarchar](50) NOT NULL,
	[KhachHangID] [nvarchar](50) NOT NULL,
	[NgayKy] [date] NOT NULL,
	[KyHanID] [nvarchar](50) NOT NULL,
	[NgayBatDau] [date] NULL,
	[NgayKetThuc] [date] NULL,
	[TrangThai] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[HopDongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[MaHopDong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[KhachHangID] [nvarchar](50) NOT NULL,
	[MaKhachHang] [nvarchar](50) NOT NULL,
	[TenCongTy] [nvarchar](200) NOT NULL,
	[MaSoThue] [nvarchar](50) NULL,
	[NguoiDaiDien] [nvarchar](200) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[DiaChi] [nvarchar](500) NULL,
	[GhiChu] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[KhachHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KyHanHopDong]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KyHanHopDong](
	[KyHanID] [nvarchar](50) NOT NULL,
	[TenKyHan] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[KyHanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenKyHan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuTimKiem]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuTimKiem](
	[TimKiemID] [nvarchar](50) NOT NULL,
	[NguoiDungID] [nvarchar](50) NULL,
	[NoiDungTimKiem] [nvarchar](500) NOT NULL,
	[KieuTimKiem] [nvarchar](20) NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TimKiemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiChiTieu]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiChiTieu](
	[LoaiChiTieuID] [nvarchar](50) NOT NULL,
	[TenChiTieu] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LoaiChiTieuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenChiTieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiPhanTich]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiPhanTich](
	[LoaiPhanTichID] [nvarchar](50) NOT NULL,
	[TenLoai] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LoaiPhanTichID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiThongBao]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiThongBao](
	[LoaiThongBaoID] [nvarchar](50) NOT NULL,
	[TenLoai] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LoaiThongBaoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiViTri]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiViTri](
	[LoaiViTriID] [nvarchar](50) NOT NULL,
	[TenLoai] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LoaiViTriID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MoHinhAI]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoHinhAI](
	[MoHinhID] [nvarchar](50) NOT NULL,
	[TenMoHinh] [nvarchar](200) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[ChuSoHuuID] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[TaoLuc] [datetime] NOT NULL,
	[CapNhatLuc] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MoHinhID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[NguoiDungID] [nvarchar](50) NOT NULL,
	[TenDangNhap] [nvarchar](100) NOT NULL,
	[MatKhauHash] [nvarchar](255) NOT NULL,
	[HoVaTen] [nvarchar](200) NOT NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[VaiTroID] [nvarchar](50) NOT NULL,
	[PhongBanID] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[SoLanDangNhapSai] [int] NOT NULL,
	[BiKhoaDen] [datetime] NULL,
	[LanDangNhapCuoi] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[NguoiDungID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhatKy]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhatKy](
	[NhatKyID] [nvarchar](50) NOT NULL,
	[NguoiDungID] [nvarchar](50) NULL,
	[HanhDong] [nvarchar](200) NOT NULL,
	[TenThucThe] [nvarchar](100) NULL,
	[ThucTheID] [nvarchar](50) NULL,
	[ChiTiet] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NhatKyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhienBanMoHinh]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhienBanMoHinh](
	[PhienBanID] [nvarchar](50) NOT NULL,
	[MoHinhID] [nvarchar](50) NOT NULL,
	[TagPhienBan] [nvarchar](50) NOT NULL,
	[Framework] [nvarchar](100) NULL,
	[DuongDanArtefact] [nvarchar](500) NULL,
	[MetricsSummary] [nvarchar](max) NULL,
	[DuocTrien] [bit] NOT NULL,
	[TaoLuc] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PhienBanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongBan]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongBan](
	[PhongBanID] [nvarchar](50) NOT NULL,
	[TenPhongBan] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PhongBanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenPhongBan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TapKetQua]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TapKetQua](
	[KetQuaID] [nvarchar](50) NOT NULL,
	[DonHangID] [nvarchar](50) NOT NULL,
	[TenFile] [nvarchar](255) NOT NULL,
	[DuongDan] [nvarchar](500) NULL,
	[LoaiFile] [nvarchar](50) NULL,
	[GeneratedBy] [nvarchar](50) NULL,
	[GeneratedAt] [datetime] NOT NULL,
	[TrangThaiXuat] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[KetQuaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongBao]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBao](
	[ThongBaoID] [nvarchar](50) NOT NULL,
	[DonHangID] [nvarchar](50) NULL,
	[NoiDung] [nvarchar](max) NOT NULL,
	[LoaiThongBaoID] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[IsRead] [bit] NOT NULL,
	[ReadAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ThongBaoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongSoMoiTruong]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongSoMoiTruong](
	[ThongSoID] [nvarchar](50) NOT NULL,
	[ViTriID] [nvarchar](50) NOT NULL,
	[MaThongSo] [nvarchar](50) NULL,
	[TenThongSo] [nvarchar](200) NOT NULL,
	[LoaiChiTieuID] [nvarchar](50) NULL,
	[DonViID] [nvarchar](50) NULL,
	[LoaiPhanTichID] [nvarchar](50) NULL,
	[GiaTri] [nvarchar](200) NULL,
	[GiaTriSo] [float] NULL,
	[GiaTriQuyChuan] [nvarchar](100) NULL,
	[KetLuan] [nvarchar](max) NULL,
	[NguoiPhanTichID] [nvarchar](50) NULL,
	[ThauPhuID] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ThongSoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThaiDonHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThaiDonHang](
	[TrangThaiID] [nvarchar](50) NOT NULL,
	[TenTrangThai] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TrangThaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenTrangThai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaiTro]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaiTro](
	[VaiTroID] [nvarchar](50) NOT NULL,
	[TenVaiTro] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VaiTroID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenVaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViTriLayMau]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViTriLayMau](
	[ViTriID] [nvarchar](50) NOT NULL,
	[DonHangID] [nvarchar](50) NOT NULL,
	[MaViTri] [nvarchar](50) NULL,
	[TenViTri] [nvarchar](200) NOT NULL,
	[LoaiViTriID] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ViTriID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DonHang_HopDongID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_DonHang_HopDongID] ON [dbo].[DonHang]
(
	[HopDongID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DonHang_IsDeleted]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_DonHang_IsDeleted] ON [dbo].[DonHang]
(
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DonHang_NgayDuKienTraKetQua]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_DonHang_NgayDuKienTraKetQua] ON [dbo].[DonHang]
(
	[NgayDuKienTraKetQua] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DonHang_NgayLayMau]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_DonHang_NgayLayMau] ON [dbo].[DonHang]
(
	[NgayLayMau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DonHang_TrangThaiID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_DonHang_TrangThaiID] ON [dbo].[DonHang]
(
	[TrangThaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HopDong_IsDeleted]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_HopDong_IsDeleted] ON [dbo].[HopDong]
(
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HopDong_KhachHangID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_HopDong_KhachHangID] ON [dbo].[HopDong]
(
	[KhachHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_KhachHang_IsDeleted]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_KhachHang_IsDeleted] ON [dbo].[KhachHang]
(
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NhatKy_CreatedAt]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_NhatKy_CreatedAt] ON [dbo].[NhatKy]
(
	[CreatedAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_NhatKy_NguoiDungID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_NhatKy_NguoiDungID] ON [dbo].[NhatKy]
(
	[NguoiDungID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TapKetQua_DonHangID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_TapKetQua_DonHangID] ON [dbo].[TapKetQua]
(
	[DonHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ThongBao_DonHangID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_ThongBao_DonHangID] ON [dbo].[ThongBao]
(
	[DonHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ThongBao_IsRead]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_ThongBao_IsRead] ON [dbo].[ThongBao]
(
	[IsRead] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ThongSoMoiTruong_ViTriID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_ThongSoMoiTruong_ViTriID] ON [dbo].[ThongSoMoiTruong]
(
	[ViTriID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ViTriLayMau_DonHangID]    Script Date: 10/23/2025 4:39:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_ViTriLayMau_DonHangID] ON [dbo].[ViTriLayMau]
(
	[DonHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DonHang] ADD  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[DonHang] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DuDoan] ADD  DEFAULT (getdate()) FOR [ThoiGianDuDoan]
GO
ALTER TABLE [dbo].[HopDong] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[LichSuTimKiem] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[MoHinhAI] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MoHinhAI] ADD  DEFAULT (getdate()) FOR [TaoLuc]
GO
ALTER TABLE [dbo].[NguoiDung] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NguoiDung] ADD  DEFAULT ((0)) FOR [SoLanDangNhapSai]
GO
ALTER TABLE [dbo].[NhatKy] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[PhienBanMoHinh] ADD  DEFAULT ((0)) FOR [DuocTrien]
GO
ALTER TABLE [dbo].[PhienBanMoHinh] ADD  DEFAULT (getdate()) FOR [TaoLuc]
GO
ALTER TABLE [dbo].[TapKetQua] ADD  DEFAULT (getdate()) FOR [GeneratedAt]
GO
ALTER TABLE [dbo].[ThongBao] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ThongBao] ADD  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_HopDong] FOREIGN KEY([HopDongID])
REFERENCES [dbo].[HopDong] ([HopDongID])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_HopDong]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_TrangThai] FOREIGN KEY([TrangThaiID])
REFERENCES [dbo].[TrangThaiDonHang] ([TrangThaiID])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_TrangThai]
GO
ALTER TABLE [dbo].[DuDoan]  WITH CHECK ADD  CONSTRAINT [FK_DuDoan_PhienBan] FOREIGN KEY([PhienBanID])
REFERENCES [dbo].[PhienBanMoHinh] ([PhienBanID])
GO
ALTER TABLE [dbo].[DuDoan] CHECK CONSTRAINT [FK_DuDoan_PhienBan]
GO
ALTER TABLE [dbo].[HopDong]  WITH CHECK ADD  CONSTRAINT [FK_HopDong_KhachHang] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([KhachHangID])
GO
ALTER TABLE [dbo].[HopDong] CHECK CONSTRAINT [FK_HopDong_KhachHang]
GO
ALTER TABLE [dbo].[HopDong]  WITH CHECK ADD  CONSTRAINT [FK_HopDong_KyHan] FOREIGN KEY([KyHanID])
REFERENCES [dbo].[KyHanHopDong] ([KyHanID])
GO
ALTER TABLE [dbo].[HopDong] CHECK CONSTRAINT [FK_HopDong_KyHan]
GO
ALTER TABLE [dbo].[LichSuTimKiem]  WITH CHECK ADD  CONSTRAINT [FK_TimKiem_NguoiDung] FOREIGN KEY([NguoiDungID])
REFERENCES [dbo].[NguoiDung] ([NguoiDungID])
GO
ALTER TABLE [dbo].[LichSuTimKiem] CHECK CONSTRAINT [FK_TimKiem_NguoiDung]
GO
ALTER TABLE [dbo].[NguoiDung]  WITH CHECK ADD  CONSTRAINT [FK_NguoiDung_PhongBan] FOREIGN KEY([PhongBanID])
REFERENCES [dbo].[PhongBan] ([PhongBanID])
GO
ALTER TABLE [dbo].[NguoiDung] CHECK CONSTRAINT [FK_NguoiDung_PhongBan]
GO
ALTER TABLE [dbo].[NguoiDung]  WITH CHECK ADD  CONSTRAINT [FK_NguoiDung_VaiTro] FOREIGN KEY([VaiTroID])
REFERENCES [dbo].[VaiTro] ([VaiTroID])
GO
ALTER TABLE [dbo].[NguoiDung] CHECK CONSTRAINT [FK_NguoiDung_VaiTro]
GO
ALTER TABLE [dbo].[NhatKy]  WITH CHECK ADD  CONSTRAINT [FK_NhatKy_NguoiDung] FOREIGN KEY([NguoiDungID])
REFERENCES [dbo].[NguoiDung] ([NguoiDungID])
GO
ALTER TABLE [dbo].[NhatKy] CHECK CONSTRAINT [FK_NhatKy_NguoiDung]
GO
ALTER TABLE [dbo].[PhienBanMoHinh]  WITH CHECK ADD  CONSTRAINT [FK_PhienBan_MoHinh] FOREIGN KEY([MoHinhID])
REFERENCES [dbo].[MoHinhAI] ([MoHinhID])
GO
ALTER TABLE [dbo].[PhienBanMoHinh] CHECK CONSTRAINT [FK_PhienBan_MoHinh]
GO
ALTER TABLE [dbo].[TapKetQua]  WITH CHECK ADD  CONSTRAINT [FK_TapKetQua_DonHang] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([DonHangID])
GO
ALTER TABLE [dbo].[TapKetQua] CHECK CONSTRAINT [FK_TapKetQua_DonHang]
GO
ALTER TABLE [dbo].[TapKetQua]  WITH CHECK ADD  CONSTRAINT [FK_TapKetQua_NguoiDung] FOREIGN KEY([GeneratedBy])
REFERENCES [dbo].[NguoiDung] ([NguoiDungID])
GO
ALTER TABLE [dbo].[TapKetQua] CHECK CONSTRAINT [FK_TapKetQua_NguoiDung]
GO
ALTER TABLE [dbo].[ThongBao]  WITH CHECK ADD  CONSTRAINT [FK_ThongBao_DonHang] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([DonHangID])
GO
ALTER TABLE [dbo].[ThongBao] CHECK CONSTRAINT [FK_ThongBao_DonHang]
GO
ALTER TABLE [dbo].[ThongBao]  WITH CHECK ADD  CONSTRAINT [FK_ThongBao_LoaiThongBao] FOREIGN KEY([LoaiThongBaoID])
REFERENCES [dbo].[LoaiThongBao] ([LoaiThongBaoID])
GO
ALTER TABLE [dbo].[ThongBao] CHECK CONSTRAINT [FK_ThongBao_LoaiThongBao]
GO
ALTER TABLE [dbo].[ThongBao]  WITH CHECK ADD  CONSTRAINT [FK_ThongBao_NguoiDung] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[NguoiDung] ([NguoiDungID])
GO
ALTER TABLE [dbo].[ThongBao] CHECK CONSTRAINT [FK_ThongBao_NguoiDung]
GO
ALTER TABLE [dbo].[ThongSoMoiTruong]  WITH CHECK ADD  CONSTRAINT [FK_ThongSo_DonVi] FOREIGN KEY([DonViID])
REFERENCES [dbo].[DonVi] ([DonViID])
GO
ALTER TABLE [dbo].[ThongSoMoiTruong] CHECK CONSTRAINT [FK_ThongSo_DonVi]
GO
ALTER TABLE [dbo].[ThongSoMoiTruong]  WITH CHECK ADD  CONSTRAINT [FK_ThongSo_LoaiChiTieu] FOREIGN KEY([LoaiChiTieuID])
REFERENCES [dbo].[LoaiChiTieu] ([LoaiChiTieuID])
GO
ALTER TABLE [dbo].[ThongSoMoiTruong] CHECK CONSTRAINT [FK_ThongSo_LoaiChiTieu]
GO
ALTER TABLE [dbo].[ThongSoMoiTruong]  WITH CHECK ADD  CONSTRAINT [FK_ThongSo_LoaiPhanTich] FOREIGN KEY([LoaiPhanTichID])
REFERENCES [dbo].[LoaiPhanTich] ([LoaiPhanTichID])
GO
ALTER TABLE [dbo].[ThongSoMoiTruong] CHECK CONSTRAINT [FK_ThongSo_LoaiPhanTich]
GO
ALTER TABLE [dbo].[ThongSoMoiTruong]  WITH CHECK ADD  CONSTRAINT [FK_ThongSo_NguoiPhanTich] FOREIGN KEY([NguoiPhanTichID])
REFERENCES [dbo].[NguoiDung] ([NguoiDungID])
GO
ALTER TABLE [dbo].[ThongSoMoiTruong] CHECK CONSTRAINT [FK_ThongSo_NguoiPhanTich]
GO
ALTER TABLE [dbo].[ThongSoMoiTruong]  WITH CHECK ADD  CONSTRAINT [FK_ThongSo_ThauPhu] FOREIGN KEY([ThauPhuID])
REFERENCES [dbo].[NguoiDung] ([NguoiDungID])
GO
ALTER TABLE [dbo].[ThongSoMoiTruong] CHECK CONSTRAINT [FK_ThongSo_ThauPhu]
GO
ALTER TABLE [dbo].[ThongSoMoiTruong]  WITH CHECK ADD  CONSTRAINT [FK_ThongSo_ViTri] FOREIGN KEY([ViTriID])
REFERENCES [dbo].[ViTriLayMau] ([ViTriID])
GO
ALTER TABLE [dbo].[ThongSoMoiTruong] CHECK CONSTRAINT [FK_ThongSo_ViTri]
GO
ALTER TABLE [dbo].[ViTriLayMau]  WITH CHECK ADD  CONSTRAINT [FK_ViTriLayMau_DonHang] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([DonHangID])
GO
ALTER TABLE [dbo].[ViTriLayMau] CHECK CONSTRAINT [FK_ViTriLayMau_DonHang]
GO
ALTER TABLE [dbo].[ViTriLayMau]  WITH CHECK ADD  CONSTRAINT [FK_ViTriLayMau_LoaiViTri] FOREIGN KEY([LoaiViTriID])
REFERENCES [dbo].[LoaiViTri] ([LoaiViTriID])
GO
ALTER TABLE [dbo].[ViTriLayMau] CHECK CONSTRAINT [FK_ViTriLayMau_LoaiViTri]
GO
/****** Object:  StoredProcedure [dbo].[sp_DangNhapNguoiDung]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_DangNhapNguoiDung]
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
/****** Object:  StoredProcedure [dbo].[sp_GetKhachHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_GetKhachHang]
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
/****** Object:  StoredProcedure [dbo].[sp_GhiNhatKy]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 9. GHI NHẬT KÝ =====
CREATE PROCEDURE [dbo].[sp_GhiNhatKy]
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
/****** Object:  StoredProcedure [dbo].[sp_KhachHang_Update]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_KhachHang_Update]
    @KhachHangID NVARCHAR(50),
    @MaKhachHang NVARCHAR(50),
    @TenCongTy NVARCHAR(200),
    @MaSoThue NVARCHAR(50)=NULL,
    @NguoiDaiDien NVARCHAR(200)=NULL,
    @DienThoai NVARCHAR(20)=NULL,
    @Email NVARCHAR(100)=NULL,
    @DiaChi NVARCHAR(500)=NULL,
    @GhiChu NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        UPDATE KhachHang
        SET MaKhachHang=@MaKhachHang,
            TenCongTy=@TenCongTy,
            MaSoThue=@MaSoThue,
            NguoiDaiDien=@NguoiDaiDien,
            DienThoai=@DienThoai,
            Email=@Email,
            DiaChi=@DiaChi,
            GhiChu=@GhiChu
        WHERE KhachHangID=@KhachHangID AND IsDeleted=0;

        IF @@ROWCOUNT=0 THROW 50010, N'Cập nhật KH thất bại (không tìm thấy).', 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_LayDanhSachKhachHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   PROCEDURE [dbo].[sp_LayDanhSachKhachHang]
AS
BEGIN
  SET NOCOUNT ON;
  SELECT KhachHangID, MaKhachHang, TenCongTy, MaSoThue, NguoiDaiDien,
         DienThoai, Email, DiaChi, GhiChu
  FROM KhachHang
  WHERE IsDeleted = 0
  ORDER BY TenCongTy;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDonHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 4. THÊM ĐƠN HÀNG =====
CREATE PROCEDURE [dbo].[sp_ThemDonHang]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemDuDoan]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 12. THÊM DỰ ĐOÁN =====
CREATE PROCEDURE [dbo].[sp_ThemDuDoan]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemHopDong]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ===== 3. THÊM HỢP ĐỒNG =====
CREATE PROCEDURE [dbo].[sp_ThemHopDong]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemKhachHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 2. THÊM KHÁCH HÀNG =====
CREATE PROCEDURE [dbo].[sp_ThemKhachHang]
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

        -- Kiểm tra mã khách hàng trùng
        IF EXISTS (SELECT 1 FROM KhachHang WHERE MaKhachHang = @MaKhachHang AND IsDeleted = 0)
        BEGIN
            RAISERROR(N'Mã khách hàng đã tồn tại!', 16, 1);
            RETURN;
        END;

        -- ===== Tự động sinh KhachHangID dạng KH0001 =====
        DECLARE @NewID NVARCHAR(50);
        DECLARE @MaxID INT;

        SELECT @MaxID = 
            MAX(CAST(SUBSTRING(KhachHangID, 3, LEN(KhachHangID) - 2) AS INT))
        FROM KhachHang
        WHERE KhachHangID LIKE 'KH%';

        IF @MaxID IS NULL
            SET @MaxID = 0;

        SET @NewID = 'KH' + RIGHT('0000' + CAST(@MaxID + 1 AS NVARCHAR(4)), 4);

        -- Thêm dữ liệu
        INSERT INTO KhachHang (
            KhachHangID, MaKhachHang, TenCongTy, MaSoThue,
            NguoiDaiDien, DienThoai, Email, DiaChi, GhiChu
        )
        VALUES (
            @NewID, @MaKhachHang, @TenCongTy, @MaSoThue,
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
/****** Object:  StoredProcedure [dbo].[sp_ThemMoHinhAI]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 10. THÊM MÔ HÌNH AI =====
CREATE PROCEDURE [dbo].[sp_ThemMoHinhAI]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemNguoiDung]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ThemNguoiDung]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemPhienBanMoHinh]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 11. THÊM PHIÊN BẢN MÔ HÌNH =====
CREATE PROCEDURE [dbo].[sp_ThemPhienBanMoHinh]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemTapKetQua]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 7. THÊM TẬP KẾT QUẢ =====
CREATE PROCEDURE [dbo].[sp_ThemTapKetQua]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemThongBao]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 8. THÊM THÔNG BÁO =====
CREATE PROCEDURE [dbo].[sp_ThemThongBao]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemThongSoMoiTruong]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 6. THÊM THÔNG SỐ MÔI TRƯỜNG =====
CREATE PROCEDURE [dbo].[sp_ThemThongSoMoiTruong]
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
/****** Object:  StoredProcedure [dbo].[sp_ThemViTriLayMau]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===== 5. THÊM VỊ TRÍ LẤY MẪU =====
CREATE PROCEDURE [dbo].[sp_ThemViTriLayMau]
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
/****** Object:  StoredProcedure [dbo].[sp_XoaKhachHang]    Script Date: 10/23/2025 4:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_XoaKhachHang]
    @KhachHangID NVARCHAR(50),
    @XoaCung BIT = 0,
    @Cascade BIT = 1,
    @NguoiDungID NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra tồn tại
        IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE KhachHangID = @KhachHangID)
        BEGIN
            RAISERROR(N'Khách hàng không tồn tại!', 16, 1);
        END

        IF @XoaCung = 1
        BEGIN
            -- Chặn xóa cứng nếu còn Hợp đồng
            IF EXISTS (SELECT 1 FROM dbo.HopDong WHERE KhachHangID = @KhachHangID)
            BEGIN
                RAISERROR(N'Không thể xóa cứng vì còn Hợp đồng liên quan. Hãy xóa Hợp đồng trước.', 16, 1);
            END

            DELETE FROM dbo.KhachHang
            WHERE KhachHangID = @KhachHangID;

            IF @NguoiDungID IS NOT NULL
            BEGIN
                INSERT INTO dbo.NhatKy (NhatKyID,NguoiDungID,HanhDong,TenThucThe,ThucTheID,ChiTiet,CreatedAt)
                VALUES (NEWID(), @NguoiDungID, N'Xóa KH (cứng)', N'KhachHang', @KhachHangID,
                        N'Xóa cứng khách hàng ' + @KhachHangID, GETDATE());
            END
        END
        ELSE
        BEGIN
            -- XÓA MỀM KH
            UPDATE dbo.KhachHang
            SET IsDeleted = 1,
                DeletedAt = GETDATE()
            WHERE KhachHangID = @KhachHangID;

            IF @Cascade = 1
            BEGIN
                -- XÓA MỀM HỢP ĐỒNG TRỰC TIẾP THEO KH
                UPDATE dbo.HopDong
                SET IsDeleted = 1,
                    DeletedAt = GETDATE()
                WHERE KhachHangID = @KhachHangID;

                -- XÓA MỀM ĐƠN HÀNG THEO CÁC HỢP ĐỒNG CỦA KH
                UPDATE dbo.DonHang
                SET IsDeleted = 1,
                    DeletedAt = GETDATE()
                WHERE HopDongID IN (
                    SELECT HD.HopDongID FROM dbo.HopDong AS HD WHERE HD.KhachHangID = @KhachHangID
                );
            END

            IF @NguoiDungID IS NOT NULL
            BEGIN
                INSERT INTO dbo.NhatKy (NhatKyID,NguoiDungID,HanhDong,TenThucThe,ThucTheID,ChiTiet,CreatedAt)
                VALUES (NEWID(), @NguoiDungID, N'Xóa KH (mềm)', N'KhachHang', @KhachHangID,
                        N'Xóa mềm khách hàng ' + @KhachHangID + N' (Cascade=' + CAST(@Cascade AS NVARCHAR(1)) + N')',
                        GETDATE());
            END
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        DECLARE @Err NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Err, 16, 1);
    END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [SQL1] SET  READ_WRITE 
GO
