namespace QuanLyHoaDon.Utilities
{
    public static class Session
    {
        public static int MaTaiKhoan { get; private set; }
        public static string TenDangNhap { get; private set; } = string.Empty;
        public static int MaNhanVien { get; private set; }
        public static string TenNhanVien { get; private set; } = string.Empty;
        public static int MaVaiTro { get; private set; }
        public static string TenVaiTro { get; private set; } = string.Empty;
        public static bool DaDangNhap { get; private set; }

        public static void DangNhap(int maTaiKhoan, string tenDangNhap, int maNhanVien, string tenNhanVien, int maVaiTro, string tenVaiTro)
        {
            MaTaiKhoan = maTaiKhoan;
            TenDangNhap = tenDangNhap;
            MaNhanVien = maNhanVien;
            TenNhanVien = tenNhanVien;
            MaVaiTro = maVaiTro;
            TenVaiTro = tenVaiTro;
            DaDangNhap = true;
        }

        public static void DangXuat()
        {
            MaTaiKhoan = 0;
            TenDangNhap = string.Empty;
            MaNhanVien = 0;
            TenNhanVien = string.Empty;
            MaVaiTro = 0;
            TenVaiTro = string.Empty;
            DaDangNhap = false;
        }
    }
}

