using System;

namespace QuanLyHoaDon.DTO
{
    public class NhanVienQuanLyDTO
    {
        public int MaNhanVien { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string TrangThaiNhanVien { get; set; } = "DangHoatDong";

        // Thong tin tai khoan kem theo
        public int? MaTaiKhoan { get; set; }
        public string? TenDangNhap { get; set; }
        public int? MaVaiTro { get; set; }
        public string? TenVaiTro { get; set; }
        public string? TrangThaiTaiKhoan { get; set; } // HoatDong / BiKhoa
    }
}

