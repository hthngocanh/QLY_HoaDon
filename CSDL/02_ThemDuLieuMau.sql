
-- =========================================================
-- HE THONG QUAN LY HOA DON
-- Script 02: THEM DU LIEU MAU
-- DBMS: Microsoft SQL Server
--
-- QUY MO DU LIEU:
-- 2 vai tro
-- 7 nhan vien (2 quan ly, 5 nhan vien)
-- 7 tai khoan
-- 12 khach hang
-- 22 san pham banh keo
-- 18 hoa don
-- 36 dong chi tiet hoa don
-- =========================================================

USE QuanLyHoaDon;
GO

SET NOCOUNT ON;

-- Kiem tra database da co du lieu hay chua
-- Neu da co du lieu, dung script de tranh trung lap
IF EXISTS (SELECT 1 FROM dbo.VaiTro)
   OR EXISTS (SELECT 1 FROM dbo.NhanVien)
   OR EXISTS (SELECT 1 FROM dbo.TaiKhoan)
   OR EXISTS (SELECT 1 FROM dbo.KhachHang)
   OR EXISTS (SELECT 1 FROM dbo.SanPham)
   OR EXISTS (SELECT 1 FROM dbo.HoaDon)
   OR EXISTS (SELECT 1 FROM dbo.ChiTietHoaDon)
BEGIN
    THROW 50001,
        N'Database da co du lieu. Hay chay lai Script 01 truoc khi nap du lieu mau.',
        1;
END;

BEGIN TRY
    BEGIN TRANSACTION;

    -- =====================================================
    -- 1. BANG VAITRO
    -- =====================================================

    INSERT INTO dbo.VaiTro (TenVaiTro)
    VALUES
        (N'QuanLy'),
        (N'NhanVien');


    -- =====================================================
    -- 2. BANG NHANVIEN
    -- 2 QUAN LY + 5 NHAN VIEN
    -- =====================================================

    INSERT INTO dbo.NhanVien
        (HoTen, SoDienThoai, Email, DiaChi, NgayVaoLam, TrangThai)
    VALUES
        (N'Nguyễn Hoàng Minh', '0901000001',
         'minh.nguyen@qlhoadon.com', N'Ba Đình, Hà Nội',
         '2023-01-10', N'DangHoatDong'),

        (N'Trần Thu Hương', '0901000002',
         'huong.tran@qlhoadon.com', N'Cầu Giấy, Hà Nội',
         '2023-03-15', N'DangHoatDong'),

        (N'Lê Văn Nam', '0901000003',
         'nam.le@qlhoadon.com', N'Đống Đa, Hà Nội',
         '2024-02-01', N'DangHoatDong'),

        (N'Phạm Thị Lan', '0901000004',
         'lan.pham@qlhoadon.com', N'Hai Bà Trưng, Hà Nội',
         '2024-04-12', N'DangHoatDong'),

        (N'Hoàng Đức Anh', '0901000005',
         'anh.hoang@qlhoadon.com', N'Long Biên, Hà Nội',
         '2024-06-20', N'DangHoatDong'),

        (N'Đỗ Minh Quân', '0901000006',
         'quan.do@qlhoadon.com', N'Thanh Xuân, Hà Nội',
         '2025-01-05', N'DangHoatDong'),

        (N'Vũ Thị Mai', '0901000007',
         'mai.vu@qlhoadon.com', N'Nam Từ Liêm, Hà Nội',
         '2025-03-10', N'DangHoatDong');


    -- =====================================================
    -- 3. BANG TAIKHOAN
    -- MAT KHAU DEMO: Demo@123
    -- SHA-256 DE TUONG THICH VOI PasswordHelper HIEN TAI
    -- =====================================================

    INSERT INTO dbo.TaiKhoan
        (MaNhanVien, MaVaiTro, TenDangNhap, MatKhauHash, TrangThai)
    SELECT
        nv.MaNhanVien,
        vt.MaVaiTro,
        tk.TenDangNhap,
        CONVERT(VARCHAR(64),
            HASHBYTES('SHA2_256', 'Demo@123'), 2),
        N'HoatDong'
    FROM
    (
        VALUES
            ('minh.nguyen@qlhoadon.com', 'quanly01', N'QuanLy'),
            ('huong.tran@qlhoadon.com', 'quanly02', N'QuanLy'),
            ('nam.le@qlhoadon.com', 'nhanvien01', N'NhanVien'),
            ('lan.pham@qlhoadon.com', 'nhanvien02', N'NhanVien'),
            ('anh.hoang@qlhoadon.com', 'nhanvien03', N'NhanVien'),
            ('quan.do@qlhoadon.com', 'nhanvien04', N'NhanVien'),
            ('mai.vu@qlhoadon.com', 'nhanvien05', N'NhanVien')
    ) AS tk(Email, TenDangNhap, TenVaiTro)
    INNER JOIN dbo.NhanVien nv
        ON nv.Email = tk.Email
    INNER JOIN dbo.VaiTro vt
        ON vt.TenVaiTro = tk.TenVaiTro;


    -- =====================================================
    -- 4. BANG KHACHHANG
    -- 12 KHACH HANG
    -- =====================================================

    INSERT INTO dbo.KhachHang
        (HoTen, SoDienThoai, Email, DiaChi)
    VALUES
        (N'Nguyễn Minh Tuấn', '0912000001',
         'tuan.nguyen@gmail.com', N'Ba Đình, Hà Nội'),

        (N'Lê Thu Hà', '0912000002',
         'ha.le@gmail.com', N'Cầu Giấy, Hà Nội'),

        (N'Trần Quốc Bảo', '0912000003',
         'bao.tran@gmail.com', N'Đống Đa, Hà Nội'),

        (N'Phạm Ngọc Anh', '0912000004',
         'anh.pham@gmail.com', N'Hai Bà Trưng, Hà Nội'),

        (N'Hoàng Thanh Tùng', '0912000005',
         'tung.hoang@gmail.com', N'Long Biên, Hà Nội'),

        (N'Vũ Thị Hạnh', '0912000006',
         'hanh.vu@gmail.com', N'Thanh Xuân, Hà Nội'),

        (N'Đỗ Đức Thành', '0912000007',
         'thanh.do@gmail.com', N'Nam Từ Liêm, Hà Nội'),

        (N'Bùi Phương Thảo', '0912000008',
         'thao.bui@gmail.com', N'Hoàng Mai, Hà Nội'),

        (N'Đặng Văn Hùng', '0912000009',
         'hung.dang@gmail.com', N'Hà Đông, Hà Nội'),

        (N'Ngô Mai Phương', '0912000010',
         'phuong.ngo@gmail.com', N'Tây Hồ, Hà Nội'),

        (N'Đinh Tiến Đạt', '0912000011',
         'dat.dinh@gmail.com', N'Gia Lâm, Hà Nội'),

        (N'Mai Khánh Linh', '0912000012',
         'linh.mai@gmail.com', N'Bắc Từ Liêm, Hà Nội');


    -- =====================================================
    -- 5. BANG SANPHAM
    -- 22 SAN PHAM BANH KEO
    -- =====================================================

    INSERT INTO dbo.SanPham
        (TenSanPham, MoTa, DonViTinh, DonGia, TrangThai)
    VALUES
        (N'Bánh ChocoPie Orion',
         N'Bánh socola nhân marshmallow, hộp 12 cái',
         N'Hộp', 65000, N'DangKinhDoanh'),

        (N'Bánh Oreo Original',
         N'Bánh quy socola nhân kem vani',
         N'Gói', 18000, N'DangKinhDoanh'),

        (N'Bánh Custas',
         N'Bánh bông lan nhân kem trứng',
         N'Hộp', 42000, N'DangKinhDoanh'),

        (N'Bánh Solite',
         N'Bánh bông lan cuộn kem',
         N'Hộp', 38000, N'DangKinhDoanh'),

        (N'Bánh quy Cosy',
         N'Bánh quy bơ giòn',
         N'Hộp', 32000, N'DangKinhDoanh'),

        (N'Bánh AFC',
         N'Bánh cracker vị rau cải',
         N'Gói', 22000, N'DangKinhDoanh'),

        (N'Bánh gạo One One',
         N'Bánh gạo vị ngọt dịu',
         N'Gói', 15000, N'DangKinhDoanh'),

        (N'Bánh snack Poca',
         N'Khoai tây lát vị tự nhiên',
         N'Gói', 12000, N'DangKinhDoanh'),

        (N'Snack Oishi',
         N'Snack bắp vị phô mai',
         N'Gói', 8000, N'DangKinhDoanh'),

        (N'Bánh que Pocky',
         N'Bánh que phủ socola',
         N'Hộp', 25000, N'DangKinhDoanh'),

        (N'Kẹo Alpenliebe',
         N'Kẹo caramel sữa',
         N'Gói', 10000, N'DangKinhDoanh'),

        (N'Kẹo Mentos',
         N'Kẹo bạc hà trái cây',
         N'Gói', 12000, N'DangKinhDoanh'),

        (N'Kẹo Chupa Chups',
         N'Kẹo mút nhiều hương vị',
         N'Chiếc', 5000, N'DangKinhDoanh'),

        (N'Kẹo dẻo Haribo',
         N'Kẹo dẻo hình gấu',
         N'Gói', 35000, N'DangKinhDoanh'),

        (N'Kẹo Dynamite',
         N'Kẹo gum bạc hà',
         N'Gói', 9000, N'DangKinhDoanh'),

        (N'Socola KitKat',
         N'Bánh xốp phủ socola',
         N'Thanh', 18000, N'DangKinhDoanh'),

        (N'Socola Hersheys',
         N'Socola sữa',
         N'Thanh', 32000, N'DangKinhDoanh'),

        (N'Socola M&M',
         N'Kẹo socola bọc đường',
         N'Gói', 28000, N'DangKinhDoanh'),

        (N'Bánh Choco Chip',
         N'Bánh quy socola chip',
         N'Hộp', 45000, N'DangKinhDoanh'),

        (N'Bánh gấu nhân kem',
         N'Bánh gấu nhân kem socola',
         N'Gói', 20000, N'DangKinhDoanh'),

        (N'Kẹo bạc hà',
         N'Kẹo cứng hương bạc hà',
         N'Gói', 10000, N'NgungKinhDoanh'),

        (N'Bánh quy bơ',
         N'Bánh quy bơ truyền thống',
         N'Hộp', 30000, N'NgungKinhDoanh');


    -- =====================================================
    -- 6. BANG HOADON
    -- 18 HOA DON
    -- 10 DA XAC NHAN, 5 DANG LAP, 3 DA HUY
    -- CO HOA DON KHACH LE
    -- =====================================================

    INSERT INTO dbo.HoaDon
        (MaKhachHang, MaNhanVien, NgayLap, TrangThai, GhiChu)
    SELECT
        kh.MaKhachHang,
        nv.MaNhanVien,
        hd.NgayLap,
        hd.TrangThai,
        hd.MaThamChieu
    FROM
    (
        VALUES
        (N'tuan.nguyen@gmail.com', 'nam.le@qlhoadon.com',
         '2026-10-01T09:00:00', N'DaXacNhan', N'MAU_HD_001'),

        (N'ha.le@gmail.com', 'lan.pham@qlhoadon.com',
         '2026-10-02T10:15:00', N'DaXacNhan', N'MAU_HD_002'),

        (N'bao.tran@gmail.com', 'anh.hoang@qlhoadon.com',
         '2026-10-03T14:20:00', N'DaXacNhan', N'MAU_HD_003'),

        (N'anh.pham@gmail.com', 'quan.do@qlhoadon.com',
         '2026-10-05T08:30:00', N'DaXacNhan', N'MAU_HD_004'),

        (N'tung.hoang@gmail.com', 'mai.vu@qlhoadon.com',
         '2026-10-07T11:00:00', N'DaXacNhan', N'MAU_HD_005'),

        (N'hanh.vu@gmail.com', 'nam.le@qlhoadon.com',
         '2026-10-09T15:45:00', N'DaXacNhan', N'MAU_HD_006'),

        (N'thanh.do@gmail.com', 'lan.pham@qlhoadon.com',
         '2026-10-11T09:30:00', N'DaXacNhan', N'MAU_HD_007'),

        (N'thao.bui@gmail.com', 'anh.hoang@qlhoadon.com',
         '2026-10-13T13:10:00', N'DaXacNhan', N'MAU_HD_008'),

        (N'hung.dang@gmail.com', 'quan.do@qlhoadon.com',
         '2026-10-15T16:00:00', N'DaXacNhan', N'MAU_HD_009'),

        (N'phuong.ngo@gmail.com', 'mai.vu@qlhoadon.com',
         '2026-10-17T10:00:00', N'DaXacNhan', N'MAU_HD_010'),

        (N'dat.dinh@gmail.com', 'nam.le@qlhoadon.com',
         '2026-10-19T09:15:00', N'DangLap', N'MAU_HD_011'),

        (N'linh.mai@gmail.com', 'lan.pham@qlhoadon.com',
         '2026-10-20T14:00:00', N'DangLap', N'MAU_HD_012'),

        (N'tuan.nguyen@gmail.com', 'anh.hoang@qlhoadon.com',
         '2026-10-21T11:30:00', N'DangLap', N'MAU_HD_013'),

        (N'ha.le@gmail.com', 'quan.do@qlhoadon.com',
         '2026-10-22T15:20:00', N'DangLap', N'MAU_HD_014'),

        (N'bao.tran@gmail.com', 'mai.vu@qlhoadon.com',
         '2026-10-23T08:45:00', N'DaHuy', N'MAU_HD_015'),

        (N'anh.pham@gmail.com', 'nam.le@qlhoadon.com',
         '2026-10-24T10:30:00', N'DaHuy', N'MAU_HD_016'),

        (NULL, 'lan.pham@qlhoadon.com',
         '2026-10-25T13:00:00', N'DaXacNhan', N'MAU_HD_017_KhachLe'),

        (NULL, 'anh.hoang@qlhoadon.com',
         '2026-10-26T16:30:00', N'DangLap', N'MAU_HD_018_KhachLe')
    ) AS hd(EmailKhachHang, EmailNhanVien, NgayLap, TrangThai, MaThamChieu)
    INNER JOIN dbo.NhanVien nv
        ON nv.Email = hd.EmailNhanVien
    LEFT JOIN dbo.KhachHang kh
        ON kh.Email = hd.EmailKhachHang;


    -- =====================================================
    -- 7. BANG CHITIETHOADON
    -- 36 DONG CHI TIET
    -- MOI HOA DON CO 2 SAN PHAM
    -- DON GIA DUOC LUU TAI THOI DIEM LAP HOA DON
    -- =====================================================

    INSERT INTO dbo.ChiTietHoaDon
        (MaHoaDon, MaSanPham, SoLuong, DonGia)
    SELECT
        hd.MaHoaDon,
        sp.MaSanPham,
        ct.SoLuong,
        ct.DonGia
    FROM
    (
        VALUES
        (N'MAU_HD_001', N'Bánh ChocoPie Orion', 2, 65000),
        (N'MAU_HD_001', N'Bánh Oreo Original', 3, 18000),

        (N'MAU_HD_002', N'Bánh Custas', 2, 42000),
        (N'MAU_HD_002', N'Bánh Solite', 1, 38000),

        (N'MAU_HD_003', N'Bánh quy Cosy', 2, 32000),
        (N'MAU_HD_003', N'Bánh AFC', 3, 22000),

        (N'MAU_HD_004', N'Bánh gạo One One', 4, 15000),
        (N'MAU_HD_004', N'Bánh snack Poca', 3, 12000),

        (N'MAU_HD_005', N'Snack Oishi', 5, 8000),
        (N'MAU_HD_005', N'Bánh que Pocky', 2, 25000),

        (N'MAU_HD_006', N'Kẹo Alpenliebe', 4, 10000),
        (N'MAU_HD_006', N'Kẹo Mentos', 3, 12000),

        (N'MAU_HD_007', N'Kẹo Chupa Chups', 10, 5000),
        (N'MAU_HD_007', N'Kẹo dẻo Haribo', 2, 35000),

        (N'MAU_HD_008', N'Kẹo Dynamite', 5, 9000),
        (N'MAU_HD_008', N'Socola KitKat', 3, 18000),

        (N'MAU_HD_009', N'Socola Hersheys', 2, 32000),
        (N'MAU_HD_009', N'Socola M&M', 2, 28000),

        (N'MAU_HD_010', N'Bánh Choco Chip', 2, 45000),
        (N'MAU_HD_010', N'Bánh gấu nhân kem', 3, 20000),

        (N'MAU_HD_011', N'Bánh ChocoPie Orion', 1, 65000),
        (N'MAU_HD_011', N'Kẹo Alpenliebe', 3, 10000),

        (N'MAU_HD_012', N'Bánh Oreo Original', 2, 18000),
        (N'MAU_HD_012', N'Bánh quy Cosy', 1, 32000),

        (N'MAU_HD_013', N'Bánh Custas', 2, 42000),
        (N'MAU_HD_013', N'Snack Oishi', 4, 8000),

        (N'MAU_HD_014', N'Bánh que Pocky', 2, 25000),
        (N'MAU_HD_014', N'Kẹo dẻo Haribo', 1, 35000),

        (N'MAU_HD_015', N'Socola KitKat', 3, 18000),
        (N'MAU_HD_015', N'Socola M&M', 2, 28000),

        (N'MAU_HD_016', N'Bánh gạo One One', 3, 15000),
        (N'MAU_HD_016', N'Bánh AFC', 2, 22000),

        (N'MAU_HD_017_KhachLe', N'Kẹo Chupa Chups', 5, 5000),
        (N'MAU_HD_017_KhachLe', N'Bánh gấu nhân kem', 2, 20000),

        (N'MAU_HD_018_KhachLe', N'Socola Hersheys', 1, 32000),
        (N'MAU_HD_018_KhachLe', N'Kẹo Mentos', 3, 12000)

    ) AS ct(MaThamChieu, TenSanPham, SoLuong, DonGia)
    INNER JOIN dbo.HoaDon hd
        ON hd.GhiChu = ct.MaThamChieu
    INNER JOIN dbo.SanPham sp
        ON sp.TenSanPham = ct.TenSanPham;


    -- =====================================================
    -- 8. KIEM TRA SO LUONG DU LIEU
    -- =====================================================

    IF (SELECT COUNT(*) FROM dbo.VaiTro) <> 2
       OR (SELECT COUNT(*) FROM dbo.NhanVien) <> 7
       OR (SELECT COUNT(*) FROM dbo.TaiKhoan) <> 7
       OR (SELECT COUNT(*) FROM dbo.KhachHang) <> 12
       OR (SELECT COUNT(*) FROM dbo.SanPham) <> 22
       OR (SELECT COUNT(*) FROM dbo.HoaDon) <> 18
       OR (SELECT COUNT(*) FROM dbo.ChiTietHoaDon) <> 36
    BEGIN
        THROW 50002, N'So luong du lieu mau khong dung nhu yeu cau.', 1;
    END;

    COMMIT TRANSACTION;

    PRINT N'THEM DU LIEU MAU THANH CONG!';

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO


-- =========================================================
-- 9. HIEN THI SO LUONG DU LIEU SAU KHI THEM
-- =========================================================

SELECT N'VaiTro' AS TenBang, COUNT(*) AS SoLuong
FROM dbo.VaiTro

UNION ALL

SELECT N'NhanVien', COUNT(*)
FROM dbo.NhanVien

UNION ALL

SELECT N'TaiKhoan', COUNT(*)
FROM dbo.TaiKhoan

UNION ALL

SELECT N'KhachHang', COUNT(*)
FROM dbo.KhachHang

UNION ALL

SELECT N'SanPham', COUNT(*)
FROM dbo.SanPham

UNION ALL

SELECT N'HoaDon', COUNT(*)
FROM dbo.HoaDon

UNION ALL

SELECT N'ChiTietHoaDon', COUNT(*)
FROM dbo.ChiTietHoaDon;
GO


-- =========================================================
-- 10. KIEM TRA DANH SACH TAI KHOAN
-- =========================================================

SELECT
    tk.TenDangNhap,
    nv.HoTen,
    vt.TenVaiTro,
    tk.TrangThai
FROM dbo.TaiKhoan tk
INNER JOIN dbo.NhanVien nv
    ON nv.MaNhanVien = tk.MaNhanVien
INNER JOIN dbo.VaiTro vt
    ON vt.MaVaiTro = tk.MaVaiTro
ORDER BY vt.TenVaiTro, tk.TenDangNhap;
GO


-- =========================================================
-- 11. KIEM TRA DANH SACH HOA DON VA TONG TIEN
-- Tong tien duoc tinh tu chi tiet hoa don
-- =========================================================

SELECT
    hd.MaHoaDon,
    hd.NgayLap,
    ISNULL(kh.HoTen, N'Khách lẻ') AS KhachHang,
    nv.HoTen AS NhanVienLap,
    hd.TrangThai,
    COUNT(ct.MaChiTietHoaDon) AS SoDongSanPham,
    ISNULL(SUM(ct.SoLuong * ct.DonGia), 0) AS TongTien
FROM dbo.HoaDon hd
LEFT JOIN dbo.KhachHang kh
    ON kh.MaKhachHang = hd.MaKhachHang
INNER JOIN dbo.NhanVien nv
    ON nv.MaNhanVien = hd.MaNhanVien
LEFT JOIN dbo.ChiTietHoaDon ct
    ON ct.MaHoaDon = hd.MaHoaDon
GROUP BY
    hd.MaHoaDon,
    hd.NgayLap,
    kh.HoTen,
    nv.HoTen,
    hd.TrangThai
ORDER BY hd.MaHoaDon;
GO










-- check sluong
USE QuanLyHoaDon;
GO

SELECT N'VaiTro' AS TenBang, COUNT(*) AS SoLuong FROM VaiTro
UNION ALL
SELECT N'NhanVien', COUNT(*) FROM NhanVien
UNION ALL
SELECT N'TaiKhoan', COUNT(*) FROM TaiKhoan
UNION ALL
SELECT N'KhachHang', COUNT(*) FROM KhachHang
UNION ALL
SELECT N'SanPham', COUNT(*) FROM SanPham
UNION ALL
SELECT N'HoaDon', COUNT(*) FROM HoaDon
UNION ALL
SELECT N'ChiTietHoaDon', COUNT(*) FROM ChiTietHoaDon;



SELECT
    @@SERVERNAME AS ServerName,
    SERVERPROPERTY('InstanceName') AS InstanceName,
    DB_NAME() AS CurrentDatabase;