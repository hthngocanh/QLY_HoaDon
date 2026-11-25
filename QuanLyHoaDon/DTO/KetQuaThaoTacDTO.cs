namespace QuanLyHoaDon.DTO
{
    public class KetQuaThaoTacDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public object? DuLieu { get; set; }

        public static KetQuaThaoTacDTO TaoThanhCong(string thongBao = "Thao tác thành công.")
        {
            return new KetQuaThaoTacDTO { ThanhCong = true, ThongBao = thongBao };
        }

        public static KetQuaThaoTacDTO TaoThatBai(string thongBao)
        {
            return new KetQuaThaoTacDTO { ThanhCong = false, ThongBao = thongBao };
        }

        public static KetQuaThaoTacDTO ThanhCongKq(string thongBao = "Thao tác thành công.", object? duLieu = null)
        {
            return new KetQuaThaoTacDTO { ThanhCong = true, ThongBao = thongBao, DuLieu = duLieu };
        }

        public static KetQuaThaoTacDTO ThatBai(string thongBao)
        {
            return new KetQuaThaoTacDTO { ThanhCong = false, ThongBao = thongBao };
        }
    }
}
