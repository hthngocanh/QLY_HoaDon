using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyHoaDon.DAL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL
{
    public class ThongKeBLL
    {
        private const int SoSanPhamTop = 5;
        private readonly ThongKeDAL _dal = new ThongKeDAL();

        // Chi Quan ly moi duoc loc theo tung nhan vien
        public bool ChoPhepLocTheoNhanVien => Session.DaDangNhap && Session.LaQuanLy;

        public List<NhanVienLocDTO> LayDanhSachNhanVienLoc()
        {
            if (!ChoPhepLocTheoNhanVien)
                return new List<NhanVienLocDTO>();

            return _dal.LayDanhSachNhanVien();
        }

        // Tra ve thong bao loi, hoac null neu bo loc hop le (giao dien co the goi truoc khi tai)
        public string? KiemTraBoLoc(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay.Date > denNgay.Date)
                return "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc";

            if (denNgay.Date > DateTime.Today)
                return "Không chọn ngày trong tương lai";

            return null;
        }

        public ThongKeKetQuaDTO LayBaoCao(DateTime tuNgay, DateTime denNgay, int? maNhanVienLoc = null)
        {
            string? loi = KiemTraBoLoc(tuNgay, denNgay);
            if (loi != null)
                throw new ArgumentException(loi);

            // Phan quyen du lieu o tang nghiep vu, khong tin tham so tu giao dien
            int? maNhanVien = XacDinhPhamVi();

            if (Session.LaQuanLy)
                maNhanVien = maNhanVienLoc;

            DateTime tu = tuNgay.Date;
            DateTime den = denNgay.Date;
            DateTime denLoaiTru = den.AddDays(1);

            int soNgay = (int)(den - tu).TotalDays + 1;
            DateTime tuTruoc = tu.AddDays(-soNgay);
            DateTime denTruocLoaiTru = tu;

            var ketQua = new ThongKeKetQuaDTO
            {
                TuNgay = tu,
                DenNgay = den,
                PhamVi = MoTaPhamVi(maNhanVien)
            };

            ketQua.TrangThai = _dal.LayThongKeTrangThai(tu, denLoaiTru, maNhanVien);
            ketQua.TongQuan = _dal.LayTongQuan(tu, denLoaiTru, maNhanVien);
            ketQua.DoanhThuKyTruoc = _dal.LayDoanhThu(tuTruoc, denTruocLoaiTru, maNhanVien);

            if (ketQua.DoanhThuKyTruoc > 0)
            {
                ketQua.PhanTramSoVoiKyTruoc = Math.Round(
                    (ketQua.TongQuan.DoanhThu - ketQua.DoanhThuKyTruoc) / ketQua.DoanhThuKyTruoc * 100m, 1);
            }

            var theoNgay = _dal.LayDoanhThuTheoNgay(tu, denLoaiTru, maNhanVien);
            ketQua.BieuDo = GomBieuDo(theoNgay, tu, den);

            ketQua.TopSanPham = _dal.LayTopSanPham(tu, denLoaiTru, maNhanVien, SoSanPhamTop);
            for (int i = 0; i < ketQua.TopSanPham.Count; i++)
                ketQua.TopSanPham[i].Hang = i + 1;

            return ketQua;
        }

        // Quan ly: null (toan he thong). Nhan vien: ma cua chinh minh. Con lai: tu choi.
        private int? XacDinhPhamVi()
        {
            if (!Session.DaDangNhap)
                throw new UnauthorizedAccessException("Bạn cần đăng nhập để xem thống kê.");

            if (Session.LaQuanLy)
                return null;

            if (Session.LaNhanVien)
                return Session.MaNhanVien;

            throw new UnauthorizedAccessException("Vai trò hiện tại không được xem thống kê.");
        }

        private string MoTaPhamVi(int? maNhanVien)
        {
            if (Session.LaNhanVien)
                return "Chỉ hóa đơn của tôi";

            return maNhanVien.HasValue ? "Theo nhân viên được chọn" : "Toàn hệ thống";
        }

        // Gom doanh thu theo ngay (<=10 ngay), tuan (<=45 ngay) hoac thang (dai hon)
        private List<DiemBieuDoDTO> GomBieuDo(List<DoanhThuNgayDTO> theoNgay, DateTime tu, DateTime den)
        {
            var map = theoNgay
                .GroupBy(x => x.Ngay.Date)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.DoanhThu));

            decimal Tong(DateTime a, DateTime b)
            {
                decimal s = 0;
                for (DateTime d = a; d <= b; d = d.AddDays(1))
                    if (map.TryGetValue(d, out decimal v)) s += v;
                return s;
            }

            int soNgay = (int)(den - tu).TotalDays + 1;
            var ds = new List<DiemBieuDoDTO>();

            if (soNgay <= 10)
            {
                for (DateTime d = tu; d <= den; d = d.AddDays(1))
                    ds.Add(new DiemBieuDoDTO { Nhan = d.ToString("dd/MM"), DoanhThu = Tong(d, d) });
            }
            else if (soNgay <= 45)
            {
                int soTuan = (int)Math.Ceiling(soNgay / 7.0);
                for (int i = 0; i < soTuan; i++)
                {
                    DateTime a = tu.AddDays(i * 7);
                    DateTime b = a.AddDays(6) > den ? den : a.AddDays(6);
                    ds.Add(new DiemBieuDoDTO { Nhan = "Tuần " + (i + 1), DoanhThu = Tong(a, b) });
                }
            }
            else
            {
                DateTime thang = new DateTime(tu.Year, tu.Month, 1);
                while (thang <= den)
                {
                    DateTime a = thang < tu ? tu : thang;
                    DateTime cuoiThang = thang.AddMonths(1).AddDays(-1);
                    DateTime b = cuoiThang > den ? den : cuoiThang;
                    ds.Add(new DiemBieuDoDTO { Nhan = thang.ToString("MM/yyyy"), DoanhThu = Tong(a, b) });
                    thang = thang.AddMonths(1);
                }
            }

            return ds;
        }
    }
}
