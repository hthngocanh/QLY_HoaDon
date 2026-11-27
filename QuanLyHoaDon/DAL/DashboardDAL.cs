using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;

namespace QuanLyHoaDon.DAL
{
    public class DashboardSoLieuDTO
    {
        public int TongHoaDon { get; set; }
        public int HoaDonDangLap { get; set; }
        public int HoaDonDaXacNhan { get; set; }
        public int HoaDonDaHuy { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int TongKhachHang { get; set; }
        public int TongSanPham { get; set; }
        public int SanPhamDangKinhDoanh { get; set; }
        public int SanPhamNgungKinhDoanh { get; set; }
        public int TongNhanVien { get; set; }
        public int NhanVienDangHoatDong { get; set; }
        public int YeuCauCapLaiMKChoDuyet { get; set; }
    }

    public class HoaDonGanDayDTO
    {
        public int MaHoaDon { get; set; }
        public string TenKhachHang { get; set; } = "";
        public string TenNhanVien { get; set; } = "";
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; } = "";
    }

    public class DashboardDAL
    {
        public DashboardSoLieuDTO LaySoLieuTongQuan(int? maNhanVienLoc = null, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var dto = new DashboardSoLieuDTO();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            // 1. Thống kê hóa đơn
            string sqlHD = @"
                SELECT 
                    COUNT(*) AS TongHD,
                    SUM(CASE WHEN hd.TrangThai = 'DangLap' THEN 1 ELSE 0 END) AS DangLap,
                    SUM(CASE WHEN hd.TrangThai = 'DaXacNhan' THEN 1 ELSE 0 END) AS DaXacNhan,
                    SUM(CASE WHEN hd.TrangThai = 'DaHuy' THEN 1 ELSE 0 END) AS DaHuy
                FROM dbo.HoaDon hd
                WHERE (@MaNV IS NULL OR hd.MaNhanVien = @MaNV)
                  AND (@TuNgay IS NULL OR hd.NgayLap >= @TuNgay)
                  AND (@DenNgay IS NULL OR hd.NgayLap < @DenNgay)";

            using (var cmd = new SqlCommand(sqlHD, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", (object?)maNhanVienLoc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TuNgay", (object?)tuNgay ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DenNgay", (object?)denNgay ?? DBNull.Value);
                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    dto.TongHoaDon = rd.IsDBNull(0) ? 0 : rd.GetInt32(0);
                    dto.HoaDonDangLap = rd.IsDBNull(1) ? 0 : rd.GetInt32(1);
                    dto.HoaDonDaXacNhan = rd.IsDBNull(2) ? 0 : rd.GetInt32(2);
                    dto.HoaDonDaHuy = rd.IsDBNull(3) ? 0 : rd.GetInt32(3);
                }
            }

            // 2. Doanh thu từ hóa đơn DaXacNhan
            string sqlDT = @"
                SELECT ISNULL(SUM(ct.SoLuong * ct.DonGia), 0)
                FROM dbo.HoaDon hd
                JOIN dbo.ChiTietHoaDon ct ON ct.MaHoaDon = hd.MaHoaDon
                WHERE hd.TrangThai = N'DaXacNhan'
                  AND (@MaNV IS NULL OR hd.MaNhanVien = @MaNV)
                  AND (@TuNgay IS NULL OR hd.NgayLap >= @TuNgay)
                  AND (@DenNgay IS NULL OR hd.NgayLap < @DenNgay)";

            using (var cmd = new SqlCommand(sqlDT, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", (object?)maNhanVienLoc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TuNgay", (object?)tuNgay ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DenNgay", (object?)denNgay ?? DBNull.Value);
                object? res = cmd.ExecuteScalar();
                dto.TongDoanhThu = (res == null || res == DBNull.Value) ? 0m : Convert.ToDecimal(res);
            }

            // 3. Số khách hàng
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.KhachHang", conn))
            {
                dto.TongKhachHang = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }

            // 4. Số sản phẩm (tổng và theo trạng thái)
            string sqlSP = @"
                SELECT 
                    COUNT(*) AS TongSP,
                    SUM(CASE WHEN TrangThai = 'DangKinhDoanh' THEN 1 ELSE 0 END) AS DangKD,
                    SUM(CASE WHEN TrangThai = 'NgungKinhDoanh' THEN 1 ELSE 0 END) AS NgungKD
                FROM dbo.SanPham";
            using (var cmd = new SqlCommand(sqlSP, conn))
            {
                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    dto.TongSanPham = rd.IsDBNull(0) ? 0 : rd.GetInt32(0);
                    dto.SanPhamDangKinhDoanh = rd.IsDBNull(1) ? 0 : rd.GetInt32(1);
                    dto.SanPhamNgungKinhDoanh = rd.IsDBNull(2) ? 0 : rd.GetInt32(2);
                }
            }

            // 5. Số nhân viên
            string sqlNV = @"
                SELECT 
                    COUNT(*) AS TongNV,
                    SUM(CASE WHEN TrangThai = N'DangHoatDong' THEN 1 ELSE 0 END) AS DangHD
                FROM dbo.NhanVien";
            using (var cmd = new SqlCommand(sqlNV, conn))
            {
                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    dto.TongNhanVien = rd.IsDBNull(0) ? 0 : rd.GetInt32(0);
                    dto.NhanVienDangHoatDong = rd.IsDBNull(1) ? 0 : rd.GetInt32(1);
                }
            }

            // 6. Yêu cầu cấp lại mật khẩu chờ duyệt
            try
            {
                using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.YeuCauCapLaiMatKhau WHERE TrangThai = 'ChoDuyet'", conn);
                dto.YeuCauCapLaiMKChoDuyet = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }
            catch
            {
                dto.YeuCauCapLaiMKChoDuyet = 0;
            }

            return dto;
        }

        public List<HoaDonGanDayDTO> LayHoaDonGanDay(int? maNhanVienLoc = null, int top = 5)
        {
            var ds = new List<HoaDonGanDayDTO>();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = $@"
                SELECT TOP ({top})
                    hd.MaHoaDon,
                    ISNULL(kh.HoTen, N'Khách vãng lai') AS TenKhachHang,
                    nv.HoTen AS TenNhanVien,
                    hd.NgayLap,
                    ISNULL(SUM(ct.SoLuong * ct.DonGia), 0) AS TongTien,
                    hd.TrangThai
                FROM dbo.HoaDon hd
                LEFT JOIN dbo.KhachHang kh ON kh.MaKhachHang = hd.MaKhachHang
                INNER JOIN dbo.NhanVien nv ON nv.MaNhanVien = hd.MaNhanVien
                LEFT JOIN dbo.ChiTietHoaDon ct ON ct.MaHoaDon = hd.MaHoaDon
                WHERE (@MaNV IS NULL OR hd.MaNhanVien = @MaNV)
                GROUP BY hd.MaHoaDon, kh.HoTen, nv.HoTen, hd.NgayLap, hd.TrangThai
                ORDER BY hd.MaHoaDon DESC";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaNV", (object?)maNhanVienLoc ?? DBNull.Value);
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ds.Add(new HoaDonGanDayDTO
                {
                    MaHoaDon = rd.GetInt32(0),
                    TenKhachHang = rd.GetString(1),
                    TenNhanVien = rd.GetString(2),
                    NgayLap = rd.GetDateTime(3),
                    TongTien = rd.GetDecimal(4),
                    TrangThai = rd.GetString(5)
                });
            }

            return ds;
        }
    }
}
