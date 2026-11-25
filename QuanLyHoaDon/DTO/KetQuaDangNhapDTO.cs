namespace QuanLyHoaDon.DTO
{
    public class KetQuaDangNhapDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public TaiKhoanDTO? TaiKhoan { get; set; }

        public static KetQuaDangNhapDTO TaoThanhCong(TaiKhoanDTO taiKhoan, string thongBao = "Đăng nhập thành công.")
        {
            return new KetQuaDangNhapDTO
            {
                ThanhCong = true,
                ThongBao = thongBao,
                TaiKhoan = taiKhoan
            };
        }

        public static KetQuaDangNhapDTO TaoThatBai(string thongBao)
        {
            return new KetQuaDangNhapDTO
            {
                ThanhCong = false,
                ThongBao = thongBao,
                TaiKhoan = null
            };
        }
    }
}

