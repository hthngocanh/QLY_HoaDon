namespace QuanLyHoaDon.DTO
{
    public class SanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public string DonViTinh { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string TrangThai { get; set; } = "DangKinhDoanh";
    }
}

