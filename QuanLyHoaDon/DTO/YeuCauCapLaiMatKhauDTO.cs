using System;

namespace QuanLyHoaDon.DTO
{
    public class YeuCauCapLaiMatKhauDTO
    {
        public int MaYeuCau { get; set; }
        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string HoTenNhanVien { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? LyDoYeuCau { get; set; }
        public string TrangThai { get; set; } = "ChoDuyet";
        public DateTime ThoiGianYeuCau { get; set; }
        public int? NguoiDuyet { get; set; }
        public string? TenNguoiDuyet { get; set; }
        public DateTime? ThoiGianDuyet { get; set; }
        public string? LyDoTuChoi { get; set; }
        public DateTime? ThoiGianHoanTat { get; set; }

        public string TenHienThiTrangThai => TrangThai switch
        {
            "ChoDuyet" => "Chờ duyệt",
            "DaDuyet" => "Đã duyệt",
            "DaTuChoi" => "Đã từ chối",
            "DaHoanTat" => "Đã hoàn tất",
            _ => TrangThai
        };
    }
}
