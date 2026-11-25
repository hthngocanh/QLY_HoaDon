using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;

namespace QuanLyHoaDon.DAL
{
    /// <summary>
    /// Cac truy van thong ke. Quy uoc chung:
    ///  - tuNgay: bao gom; denNgayLoaiTru: KHONG bao gom (la ngay cuoi + 1 ngay) vi NgayLap la DATETIME
    ///  - maNhanVien = null: toan he thong; co gia tri: chi hoa don do nhan vien do lap
    ///  - Doanh thu chi tinh hoa don DaXacNhan, = SUM(SoLuong * DonGia) tu ChiTietHoaDon
    /// </summary>
    public class ThongKeDAL
    {
        private const string DieuKienLoc = @"
            hd.NgayLap >= @TuNgay
            AND hd.NgayLap < @DenNgay
            AND (@MaNhanVien IS NULL OR hd.MaNhanVien = @MaNhanVien)";

        private static void ThemThamSoLoc(SqlCommand cmd, DateTime tuNgay, DateTime denNgayLoaiTru, int? maNhanVien)
        {
            cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime).Value = tuNgay;
            cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime).Value = denNgayLoaiTru;
            cmd.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = (object?)maNhanVien ?? DBNull.Value;
        }

        // 1. Dem hoa don theo trang thai (chi bang HoaDon, de hoa don DangLap chua co chi tiet van duoc dem)
        public ThongKeTrangThaiDTO LayThongKeTrangThai(DateTime tuNgay, DateTime denNgayLoaiTru, int? maNhanVien)
        {
            var kq = new ThongKeTrangThaiDTO();
            string sql = @"
                SELECT hd.TrangThai, COUNT(*) AS SoLuong
                FROM dbo.HoaDon hd
                WHERE " + DieuKienLoc + @"
                GROUP BY hd.TrangThai;";

            using var conn = KetNoiCSDL.TaoKetNoi();
            using var cmd = new SqlCommand(sql, conn);
            ThemThamSoLoc(cmd, tuNgay, denNgayLoaiTru, maNhanVien);
            conn.Open();

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                string trangThai = rd.GetString(0);
                int soLuong = rd.GetInt32(1);
                switch (trangThai)
                {
                    case "DaXacNhan": kq.SoDaXacNhan = soLuong; break;
                    case "DangLap": kq.SoDangLap = soLuong; break;
                    case "DaHuy": kq.SoDaHuy = soLuong; break;
                }
            }
            return kq;
        }

        // 2. Tong doanh thu + so hoa don da xac nhan
        public ThongKeTongQuanDTO LayTongQuan(DateTime tuNgay, DateTime denNgayLoaiTru, int? maNhanVien)
        {
            var kq = new ThongKeTongQuanDTO();
            string sql = @"
                SELECT
                    ISNULL(SUM(ct.SoLuong * ct.DonGia), CAST(0 AS DECIMAL(18,2))) AS DoanhThu,
                    COUNT(DISTINCT hd.MaHoaDon) AS SoHoaDon
                FROM dbo.HoaDon hd
                JOIN dbo.ChiTietHoaDon ct ON ct.MaHoaDon = hd.MaHoaDon
                WHERE hd.TrangThai = N'DaXacNhan' AND " + DieuKienLoc + ";";

            using var conn = KetNoiCSDL.TaoKetNoi();
            using var cmd = new SqlCommand(sql, conn);
            ThemThamSoLoc(cmd, tuNgay, denNgayLoaiTru, maNhanVien);
            conn.Open();

            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                kq.DoanhThu = rd.GetDecimal(0);
                kq.SoHoaDonDaXacNhan = rd.GetInt32(1);
            }
            return kq;
        }

        // 3. Doanh thu cua mot khoang (dung de so voi ky truoc)
        public decimal LayDoanhThu(DateTime tuNgay, DateTime denNgayLoaiTru, int? maNhanVien)
        {
            string sql = @"
                SELECT ISNULL(SUM(ct.SoLuong * ct.DonGia), CAST(0 AS DECIMAL(18,2)))
                FROM dbo.HoaDon hd
                JOIN dbo.ChiTietHoaDon ct ON ct.MaHoaDon = hd.MaHoaDon
                WHERE hd.TrangThai = N'DaXacNhan' AND " + DieuKienLoc + ";";

            using var conn = KetNoiCSDL.TaoKetNoi();
            using var cmd = new SqlCommand(sql, conn);
            ThemThamSoLoc(cmd, tuNgay, denNgayLoaiTru, maNhanVien);
            conn.Open();

            object? kq = cmd.ExecuteScalar();
            return kq == null || kq == DBNull.Value ? 0m : Convert.ToDecimal(kq);
        }

        // 4. Doanh thu theo tung ngay (BLL se gom tiep theo tuan / thang)
        public List<DoanhThuNgayDTO> LayDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgayLoaiTru, int? maNhanVien)
        {
            var ds = new List<DoanhThuNgayDTO>();
            string sql = @"
                SELECT CAST(hd.NgayLap AS DATE) AS Ngay,
                       SUM(ct.SoLuong * ct.DonGia) AS DoanhThu,
                       COUNT(DISTINCT hd.MaHoaDon) AS SoHoaDon
                FROM dbo.HoaDon hd
                JOIN dbo.ChiTietHoaDon ct ON ct.MaHoaDon = hd.MaHoaDon
                WHERE hd.TrangThai = N'DaXacNhan' AND " + DieuKienLoc + @"
                GROUP BY CAST(hd.NgayLap AS DATE)
                ORDER BY Ngay;";

            using var conn = KetNoiCSDL.TaoKetNoi();
            using var cmd = new SqlCommand(sql, conn);
            ThemThamSoLoc(cmd, tuNgay, denNgayLoaiTru, maNhanVien);
            conn.Open();

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ds.Add(new DoanhThuNgayDTO
                {
                    Ngay = rd.GetDateTime(0),
                    DoanhThu = rd.GetDecimal(1),
                    SoHoaDon = rd.GetInt32(2)
                });
            }
            return ds;
        }

        // 5. Top san pham theo doanh thu. Ty trong tinh tren tong doanh thu cua ca ky (khong chi top)
        public List<TopSanPhamDTO> LayTopSanPham(DateTime tuNgay, DateTime denNgayLoaiTru, int? maNhanVien, int top)
        {
            var ds = new List<TopSanPhamDTO>();
            string sql = @"
                SELECT TOP (@Top)
                    sp.MaSanPham,
                    sp.TenSanPham,
                    SUM(ct.SoLuong) AS TongSoLuong,
                    SUM(ct.SoLuong * ct.DonGia) AS TongDoanhThu,
                    CAST(100.0 * SUM(ct.SoLuong * ct.DonGia)
                         / NULLIF(SUM(SUM(ct.SoLuong * ct.DonGia)) OVER (), 0)
                         AS DECIMAL(5,1)) AS TyTrong
                FROM dbo.HoaDon hd
                JOIN dbo.ChiTietHoaDon ct ON ct.MaHoaDon = hd.MaHoaDon
                JOIN dbo.SanPham sp ON sp.MaSanPham = ct.MaSanPham
                WHERE hd.TrangThai = N'DaXacNhan' AND " + DieuKienLoc + @"
                GROUP BY sp.MaSanPham, sp.TenSanPham
                ORDER BY TongDoanhThu DESC, sp.MaSanPham;";

            using var conn = KetNoiCSDL.TaoKetNoi();
            using var cmd = new SqlCommand(sql, conn);
            ThemThamSoLoc(cmd, tuNgay, denNgayLoaiTru, maNhanVien);
            cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;
            conn.Open();

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ds.Add(new TopSanPhamDTO
                {
                    MaSanPham = rd.GetInt32(0),
                    TenSanPham = rd.GetString(1),
                    SoLuong = rd.GetInt32(2),
                    DoanhThu = rd.GetDecimal(3),
                    TyTrongPhanTram = rd.IsDBNull(4) ? 0 : rd.GetDecimal(4)
                });
            }
            return ds;
        }

        // 6. Danh sach nhan vien cho o loc cua Quan ly (lay ca nguoi da nghi de van xem duoc lich su)
        public List<NhanVienLocDTO> LayDanhSachNhanVien()
        {
            var ds = new List<NhanVienLocDTO>();
            const string sql = "SELECT MaNhanVien, HoTen, TrangThai FROM dbo.NhanVien ORDER BY HoTen;";

            using var conn = KetNoiCSDL.TaoKetNoi();
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ds.Add(new NhanVienLocDTO
                {
                    MaNhanVien = rd.GetInt32(0),
                    HoTen = rd.GetString(1),
                    TrangThai = rd.GetString(2)
                });
            }
            return ds;
        }
    }
}
