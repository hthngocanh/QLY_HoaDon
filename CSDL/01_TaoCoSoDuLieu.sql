
-- =========================================================
-- HE THONG QUAN LY HOA DON
-- Script 01: Tao co so du lieu va 7 bang
-- DBMS: Microsoft SQL Server
-- =========================================================

IF DB_ID(N'QuanLyHoaDon') IS NULL
BEGIN
    CREATE DATABASE QuanLyHoaDon;
END
GO

USE QuanLyHoaDon;
GO

-- Xoa cac bang cu theo thu tu phu thuoc
DROP TABLE IF EXISTS dbo.ChiTietHoaDon;
DROP TABLE IF EXISTS dbo.HoaDon;
DROP TABLE IF EXISTS dbo.SanPham;
DROP TABLE IF EXISTS dbo.KhachHang;
DROP TABLE IF EXISTS dbo.TaiKhoan;
DROP TABLE IF EXISTS dbo.NhanVien;
DROP TABLE IF EXISTS dbo.VaiTro;
GO

-- 1. BANG VAITRO
CREATE TABLE dbo.VaiTro (
    MaVaiTro INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro NVARCHAR(50) NOT NULL,

    CONSTRAINT UQ_VaiTro_TenVaiTro
        UNIQUE (TenVaiTro),

    CONSTRAINT CK_VaiTro_TenVaiTro
        CHECK (TenVaiTro IN (N'QuanLy', N'NhanVien'))
);
GO

-- 2. BANG NHANVIEN
CREATE TABLE dbo.NhanVien (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NULL,
    Email VARCHAR(100) NULL,
    DiaChi NVARCHAR(255) NULL,
    NgayVaoLam DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'DangHoatDong',

    CONSTRAINT UQ_NhanVien_SoDienThoai
        UNIQUE (SoDienThoai),

    CONSTRAINT UQ_NhanVien_Email
        UNIQUE (Email),

    CONSTRAINT CK_NhanVien_TrangThai
        CHECK (TrangThai IN (N'DangHoatDong', N'NgungHoatDong'))
);
GO

-- 3. BANG TAIKHOAN
CREATE TABLE dbo.TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    MaNhanVien INT NOT NULL,
    MaVaiTro INT NOT NULL,
    TenDangNhap VARCHAR(50) NOT NULL,
    MatKhauHash VARCHAR(256) NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'HoatDong',
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT UQ_TaiKhoan_TenDangNhap
        UNIQUE (TenDangNhap),

    CONSTRAINT UQ_TaiKhoan_MaNhanVien
        UNIQUE (MaNhanVien),

    CONSTRAINT CK_TaiKhoan_TrangThai
        CHECK (TrangThai IN (N'HoatDong', N'BiKhoa')),

    CONSTRAINT FK_TaiKhoan_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES dbo.NhanVien(MaNhanVien),

    CONSTRAINT FK_TaiKhoan_VaiTro
        FOREIGN KEY (MaVaiTro)
        REFERENCES dbo.VaiTro(MaVaiTro)
);
GO

-- 4. BANG KHACHHANG
CREATE TABLE dbo.KhachHang (
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NULL,
    Email VARCHAR(100) NULL,
    DiaChi NVARCHAR(255) NULL
);
GO

-- 5. BANG SANPHAM
CREATE TABLE dbo.SanPham (
    MaSanPham INT IDENTITY(1,1) PRIMARY KEY,
    TenSanPham NVARCHAR(150) NOT NULL,
    MoTa NVARCHAR(500) NULL,
    DonViTinh NVARCHAR(50) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'DangKinhDoanh',

    CONSTRAINT CK_SanPham_DonGia
        CHECK (DonGia >= 0),

    CONSTRAINT CK_SanPham_TrangThai
        CHECK (TrangThai IN (N'DangKinhDoanh', N'NgungKinhDoanh'))
);
GO

-- 6. BANG HOADON
CREATE TABLE dbo.HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT NULL,
    MaNhanVien INT NOT NULL,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'DangLap',
    GhiChu NVARCHAR(255) NULL,

    CONSTRAINT CK_HoaDon_TrangThai
        CHECK (TrangThai IN (N'DangLap', N'DaXacNhan', N'DaHuy')),

    CONSTRAINT FK_HoaDon_KhachHang
        FOREIGN KEY (MaKhachHang)
        REFERENCES dbo.KhachHang(MaKhachHang),

    CONSTRAINT FK_HoaDon_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES dbo.NhanVien(MaNhanVien)
);
GO

-- 7. BANG CHITIETHOADON
CREATE TABLE dbo.ChiTietHoaDon (
    MaChiTietHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,

    CONSTRAINT UQ_ChiTietHoaDon_HoaDon_SanPham
        UNIQUE (MaHoaDon, MaSanPham),

    CONSTRAINT CK_ChiTietHoaDon_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_ChiTietHoaDon_DonGia
        CHECK (DonGia >= 0),

    CONSTRAINT FK_ChiTietHoaDon_HoaDon
        FOREIGN KEY (MaHoaDon)
        REFERENCES dbo.HoaDon(MaHoaDon),

    CONSTRAINT FK_ChiTietHoaDon_SanPham
        FOREIGN KEY (MaSanPham)
        REFERENCES dbo.SanPham(MaSanPham)
);
GO

PRINT N'Tao co so du lieu va 7 bang thanh cong!';
GO



USE QuanLyHoaDon;
GO

SELECT
    TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;



USE QuanLyHoaDon;
GO

SELECT
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
ORDER BY TABLE_NAME, ORDINAL_POSITION;