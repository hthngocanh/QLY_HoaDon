-- ===================================================================
-- SCRIPT BỔ SUNG BẢNG YÊU CẦU CẤP LẠI MẬT KHẨU (YeuCauCapLaiMatKhau)
-- Hệ thống: Quản lý hóa đơn - .NET 10 WinForms
-- Tương thích: SQL Server / SQL Server LocalDB
-- ===================================================================

USE QuanLyHoaDon;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'YeuCauCapLaiMatKhau')
BEGIN
    CREATE TABLE dbo.YeuCauCapLaiMatKhau (
        MaYeuCau INT IDENTITY(1,1) PRIMARY KEY,
        MaTaiKhoan INT NOT NULL,
        LyDoYeuCau NVARCHAR(500) NULL,
        TrangThai NVARCHAR(30) NOT NULL DEFAULT N'ChoDuyet',
        ThoiGianYeuCau DATETIME NOT NULL DEFAULT GETDATE(),
        NguoiDuyet INT NULL,
        ThoiGianDuyet DATETIME NULL,
        LyDoTuChoi NVARCHAR(500) NULL,
        ThoiGianHoanTat DATETIME NULL,

        -- Khóa ngoại tới TaiKhoan (người yêu cầu)
        CONSTRAINT FK_YeuCau_TaiKhoan FOREIGN KEY (MaTaiKhoan)
            REFERENCES dbo.TaiKhoan(MaTaiKhoan),

        -- Khóa ngoại tới NhanVien (người quản lý duyệt/từ chối)
        CONSTRAINT FK_YeuCau_NguoiDuyet FOREIGN KEY (NguoiDuyet)
            REFERENCES dbo.NhanVien(MaNhanVien),

        -- Ràng buộc trạng thái hợp lệ
        CONSTRAINT CK_YeuCau_TrangThai CHECK (TrangThai IN (N'ChoDuyet', N'DaDuyet', N'DaTuChoi', N'DaHoanTat'))
    );

    -- Index hỗ trợ tìm kiếm nhanh theo tài khoản và trạng thái
    CREATE INDEX IX_YeuCau_MaTaiKhoan_TrangThai ON dbo.YeuCauCapLaiMatKhau(MaTaiKhoan, TrangThai);
    CREATE INDEX IX_YeuCau_TrangThai ON dbo.YeuCauCapLaiMatKhau(TrangThai);

    PRINT N'Tạo bảng YeuCauCapLaiMatKhau thành công!';
END
ELSE
BEGIN
    PRINT N'Bảng YeuCauCapLaiMatKhau đã tồn tại.';
END
GO
