using System;
using System.Collections.Generic;

namespace QuanLyHoaDon.DTO
{
    public class HoaDonDTO
    {
        public int MaHoaDon { get; set; }
        public int? MaKhachHang { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SoDienThoaiKhachHang { get; set; }
        public int MaNhanVien { get; set; }
        public string? TenNhanVien { get; set; }
        public DateTime NgayLap { get; set; }
        public string TrangThai { get; set; } = "DangLap"; // DangLap, DaXacNhan, DaHuy
        public string? GhiChu { get; set; }
        public decimal TongTien { get; set; }
        public int TongSoLuong { get; set; }
    }

    public class ChiTietHoaDonDTO
    {
        public int MaChiTietHoaDon { get; set; }
        public int MaHoaDon { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string DonViTinh { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}

