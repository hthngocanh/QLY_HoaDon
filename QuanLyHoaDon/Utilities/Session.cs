using System;

namespace QuanLyHoaDon.Utilities
{
    public static class Session
    {
        public static int MaTaiKhoan { get; private set; }
        public static int MaNhanVien { get; private set; }
        public static string HoTen { get; private set; } = string.Empty;
        public static string TenDangNhap { get; private set; } = string.Empty;
        public static int MaVaiTro { get; private set; }
        public static string TenVaiTro { get; private set; } = string.Empty;
        public static string SoDienThoai { get; private set; } = string.Empty;
        public static string Email { get; private set; } = string.Empty;
        public static string DiaChi { get; private set; } = string.Empty;
        public static DateTime? NgayVaoLam { get; private set; }
        public static string TrangThaiNhanVien { get; private set; } = string.Empty;
        public static string TrangThaiTaiKhoan { get; private set; } = string.Empty;
        public static bool DaDangNhap { get; private set; }

        public static bool LaQuanLy => string.Equals(TenVaiTro, "QuanLy", StringComparison.OrdinalIgnoreCase);
        public static bool LaNhanVien => string.Equals(TenVaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase);

        public static void DangNhap(
            int maTaiKhoan,
            int maNhanVien,
            string hoTen,
            string tenDangNhap,
            int maVaiTro,
            string tenVaiTro,
            string soDienThoai = "",
            string email = "",
            string diaChi = "",
            DateTime? ngayVaoLam = null,
            string trangThaiNhanVien = "DangHoatDong",
            string trangThaiTaiKhoan = "HoatDong")
        {
            MaTaiKhoan = maTaiKhoan;
            MaNhanVien = maNhanVien;
            HoTen = hoTen ?? string.Empty;
            TenDangNhap = tenDangNhap ?? string.Empty;
            MaVaiTro = maVaiTro;
            TenVaiTro = tenVaiTro ?? string.Empty;
            SoDienThoai = soDienThoai ?? string.Empty;
            Email = email ?? string.Empty;
            DiaChi = diaChi ?? string.Empty;
            NgayVaoLam = ngayVaoLam;
            TrangThaiNhanVien = trangThaiNhanVien ?? string.Empty;
            TrangThaiTaiKhoan = trangThaiTaiKhoan ?? string.Empty;
            DaDangNhap = true;
        }

        public static void CapNhatThongTinLienHe(string soDienThoai, string email, string diaChi)
        {
            SoDienThoai = soDienThoai ?? string.Empty;
            Email = email ?? string.Empty;
            DiaChi = diaChi ?? string.Empty;
        }

        public static void DangXuat()
        {
            MaTaiKhoan = 0;
            MaNhanVien = 0;
            HoTen = string.Empty;
            TenDangNhap = string.Empty;
            MaVaiTro = 0;
            TenVaiTro = string.Empty;
            SoDienThoai = string.Empty;
            Email = string.Empty;
            DiaChi = string.Empty;
            NgayVaoLam = null;
            TrangThaiNhanVien = string.Empty;
            TrangThaiTaiKhoan = string.Empty;
            DaDangNhap = false;
        }
    }
}
