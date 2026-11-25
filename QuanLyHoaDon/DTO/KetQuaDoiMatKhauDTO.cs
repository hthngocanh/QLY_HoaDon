namespace QuanLyHoaDon.DTO
{
    public class KetQuaDoiMatKhauDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBaoChung { get; set; } = string.Empty;

        // Loi cu the tung truong
        public string LoiMatKhauHienTai { get; set; } = string.Empty;
        public string LoiMatKhauMoi { get; set; } = string.Empty;
        public string LoiXacNhanMatKhau { get; set; } = string.Empty;

        public bool CoLoi => !string.IsNullOrEmpty(LoiMatKhauHienTai) ||
                             !string.IsNullOrEmpty(LoiMatKhauMoi) ||
                             !string.IsNullOrEmpty(LoiXacNhanMatKhau);

        public static KetQuaDoiMatKhauDTO TaoThanhCong(string thongBao = "Cập nhật mật khẩu thành công!")
        {
            return new KetQuaDoiMatKhauDTO
            {
                ThanhCong = true,
                ThongBaoChung = thongBao
            };
        }

        public static KetQuaDoiMatKhauDTO TaoThatBai(
            string thongBaoChung,
            string loiHienTai = "",
            string loiMoi = "",
            string loiXacNhan = "")
        {
            return new KetQuaDoiMatKhauDTO
            {
                ThanhCong = false,
                ThongBaoChung = thongBaoChung,
                LoiMatKhauHienTai = loiHienTai,
                LoiMatKhauMoi = loiMoi,
                LoiXacNhanMatKhau = loiXacNhan
            };
        }
    }
}

