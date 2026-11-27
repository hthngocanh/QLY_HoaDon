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

        public string TenTrangThaiNhanVien => TrangThaiNhanVien switch
        {
            "DangHoatDong" => "Đang hoạt động",
            "NgungHoatDong" => "Ngừng hoạt động",
            _ => TrangThaiNhanVien
        };

        // Thong tin tai khoan kem theo
        public int? MaTaiKhoan { get; set; }
        public string? TenDangNhap { get; set; }
        public int? MaVaiTro { get; set; }
        public string? TenVaiTro { get; set; }
        public string? TrangThaiTaiKhoan { get; set; } // HoatDong / BiKhoa

        public string TenTrangThaiTaiKhoan => TrangThaiTaiKhoan switch
        {
            "HoatDong" => "Hoạt động",
            "BiKhoa" => "Bị khóa",
            null or "" => "(Chưa có)",
            _ => TrangThaiTaiKhoan
        };
    }
}

