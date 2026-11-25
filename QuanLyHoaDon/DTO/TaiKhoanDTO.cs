using System;

namespace QuanLyHoaDon.DTO
{
    public class TaiKhoanDTO
    {
        public int MaTaiKhoan { get; set; }
        public int MaNhanVien { get; set; }
        public int MaVaiTro { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhauHash { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }

        // Thong tin lien ket tu NhanVien & VaiTro
        public string TenVaiTro { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public DateTime NgayVaoLam { get; set; }
        public string TrangThaiNhanVien { get; set; } = string.Empty;
    }
}

