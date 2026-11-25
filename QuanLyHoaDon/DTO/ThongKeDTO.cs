using System;
using System.Collections.Generic;

namespace QuanLyHoaDon.DTO
{
    // So luong hoa don theo 3 trang thai (dem tren bang HoaDon)
    public class ThongKeTrangThaiDTO
    {
        public int SoDaXacNhan { get; set; }
        public int SoDangLap { get; set; }
        public int SoDaHuy { get; set; }
        public int Tong => SoDaXacNhan + SoDangLap + SoDaHuy;

        public decimal TyLeDaXacNhan => TinhTyLe(SoDaXacNhan);
        public decimal TyLeDangLap => TinhTyLe(SoDangLap);
        public decimal TyLeDaHuy => TinhTyLe(SoDaHuy);

        private decimal TinhTyLe(int so)
        {
            return Tong == 0 ? 0 : Math.Round(100m * so / Tong, 1);
        }
    }

    // Doanh thu (chi hoa don DaXacNhan)
    public class ThongKeTongQuanDTO
    {
        public decimal DoanhThu { get; set; }
        public int SoHoaDonDaXacNhan { get; set; }
        public decimal GiaTriTrungBinh => SoHoaDonDaXacNhan == 0 ? 0 : DoanhThu / SoHoaDonDaXacNhan;
    }

    // Doanh thu theo tung ngay (du lieu tho tu DAL)
    public class DoanhThuNgayDTO
    {
        public DateTime Ngay { get; set; }
        public decimal DoanhThu { get; set; }
        public int SoHoaDon { get; set; }
    }

    // Mot cot tren bieu do (da gom theo ngay / tuan / thang)
    public class DiemBieuDoDTO
    {
        public string Nhan { get; set; } = string.Empty;
        public decimal DoanhThu { get; set; }
    }

    public class TopSanPhamDTO
    {
        public int Hang { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal TyTrongPhanTram { get; set; }

        // Hien thi dang SP001
        public string MaHienThi => "SP" + MaSanPham.ToString("000");
    }

    // Nhan vien cho o loc cua Quan ly
    public class NhanVienLocDTO
    {
        public int MaNhanVien { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;
        public override string ToString() => HoTen;
    }

    // Ket qua tong hop tra ve cho giao dien
    public class ThongKeKetQuaDTO
    {
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string PhamVi { get; set; } = string.Empty;

        public ThongKeTrangThaiDTO TrangThai { get; set; } = new ThongKeTrangThaiDTO();
        public ThongKeTongQuanDTO TongQuan { get; set; } = new ThongKeTongQuanDTO();

        public decimal DoanhThuKyTruoc { get; set; }
        // null khi ky truoc khong co doanh thu (khong chia duoc cho 0), giao dien hien "--"
        public decimal? PhanTramSoVoiKyTruoc { get; set; }

        public List<DiemBieuDoDTO> BieuDo { get; set; } = new List<DiemBieuDoDTO>();
        public List<TopSanPhamDTO> TopSanPham { get; set; } = new List<TopSanPhamDTO>();
    }
}
