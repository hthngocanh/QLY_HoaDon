using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;

namespace QuanLyHoaDon.DAL
{
    public class HoaDonDAL
    {
        public List<HoaDonDTO> LayDanhSach(int? maNhanVienLoc = null, string? tuKhoa = null, string? trangThaiLoc = null)
        {
            var ds = new List<HoaDonDTO>();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"
                SELECT
                    hd.MaHoaDon, hd.MaKhachHang, kh.HoTen AS TenKhachHang, kh.SoDienThoai,
                    hd.MaNhanVien, nv.HoTen AS TenNhanVien, hd.NgayLap, hd.TrangThai, hd.GhiChu,
                    ISNULL(SUM(ct.SoLuong * ct.DonGia), 0) AS TongTien,
                    ISNULL(SUM(ct.SoLuong), 0) AS TongSoLuong
                FROM dbo.HoaDon hd
                LEFT JOIN dbo.KhachHang kh ON kh.MaKhachHang = hd.MaKhachHang
                INNER JOIN dbo.NhanVien nv ON nv.MaNhanVien = hd.MaNhanVien
                LEFT JOIN dbo.ChiTietHoaDon ct ON ct.MaHoaDon = hd.MaHoaDon
                WHERE 1 = 1";

            if (maNhanVienLoc.HasValue)
            {
                sql += " AND hd.MaNhanVien = @MaNhanVienLoc";
            }

            if (!string.IsNullOrWhiteSpace(trangThaiLoc) && trangThaiLoc != "TatCa")
            {
                sql += " AND hd.TrangThai = @TrangThaiLoc";
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                sql += " AND (CAST(hd.MaHoaDon AS VARCHAR) LIKE @TuKhoa OR kh.HoTen LIKE @TuKhoa OR kh.SoDienThoai LIKE @TuKhoa OR nv.HoTen LIKE @TuKhoa)";
            }

            sql += @" GROUP BY hd.MaHoaDon, hd.MaKhachHang, kh.HoTen, kh.SoDienThoai,
                               hd.MaNhanVien, nv.HoTen, hd.NgayLap, hd.TrangThai, hd.GhiChu
                      ORDER BY hd.MaHoaDon DESC";

            using var cmd = new SqlCommand(sql, conn);
            if (maNhanVienLoc.HasValue)
            {
                cmd.Parameters.AddWithValue("@MaNhanVienLoc", maNhanVienLoc.Value);
            }
            if (!string.IsNullOrWhiteSpace(trangThaiLoc) && trangThaiLoc != "TatCa")
            {
                cmd.Parameters.AddWithValue("@TrangThaiLoc", trangThaiLoc.Trim());
            }
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa.Trim() + "%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new HoaDonDTO
                {
                    MaHoaDon = reader.GetInt32(0),
                    MaKhachHang = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    TenKhachHang = reader.IsDBNull(2) ? "Khách vãng lai" : reader.GetString(2),
                    SoDienThoaiKhachHang = reader.IsDBNull(3) ? null : reader.GetString(3),
                    MaNhanVien = reader.GetInt32(4),
                    TenNhanVien = reader.GetString(5),
                    NgayLap = reader.GetDateTime(6),
                    TrangThai = reader.GetString(7),
                    GhiChu = reader.IsDBNull(8) ? null : reader.GetString(8),
                    TongTien = reader.GetDecimal(9),
                    TongSoLuong = reader.GetInt32(10)
                });
            }
            return ds;
        }

        public List<ChiTietHoaDonDTO> LayChiTietHoaDon(int maHoaDon)
        {
            var ds = new List<ChiTietHoaDonDTO>();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"
                SELECT ct.MaChiTietHoaDon, ct.MaHoaDon, ct.MaSanPham, sp.TenSanPham, sp.DonViTinh, ct.SoLuong, ct.DonGia
                FROM dbo.ChiTietHoaDon ct
                INNER JOIN dbo.SanPham sp ON sp.MaSanPham = ct.MaSanPham
                WHERE ct.MaHoaDon = @MaHoaDon
                ORDER BY ct.MaChiTietHoaDon ASC";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new ChiTietHoaDonDTO
                {
                    MaChiTietHoaDon = reader.GetInt32(0),
                    MaHoaDon = reader.GetInt32(1),
                    MaSanPham = reader.GetInt32(2),
                    TenSanPham = reader.GetString(3),
                    DonViTinh = reader.GetString(4),
                    SoLuong = reader.GetInt32(5),
                    DonGia = reader.GetDecimal(6)
                });
            }
            return ds;
        }

        public int TaoHoaDon(int maNhanVien, int? maKhachHang, string? ghiChu, List<ChiTietHoaDonDTO> chiTietList)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                string sqlHD = @"
                    INSERT INTO dbo.HoaDon (MaKhachHang, MaNhanVien, NgayLap, TrangThai, GhiChu)
                    OUTPUT INSERTED.MaHoaDon
                    VALUES (@MaKhachHang, @MaNhanVien, GETDATE(), N'DangLap', @GhiChu)";

                using var cmdHD = new SqlCommand(sqlHD, conn, tran);
                cmdHD.Parameters.AddWithValue("@MaKhachHang", (object?)maKhachHang ?? DBNull.Value);
                cmdHD.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmdHD.Parameters.AddWithValue("@GhiChu", (object?)ghiChu?.Trim() ?? DBNull.Value);

                int maHoaDon = (int)cmdHD.ExecuteScalar()!;

                string sqlCT = @"
                    INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, DonGia)
                    VALUES (@MaHoaDon, @MaSanPham, @SoLuong, @DonGia)";

                foreach (var ct in chiTietList)
                {
                    using var cmdCT = new SqlCommand(sqlCT, conn, tran);
                    cmdCT.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdCT.Parameters.AddWithValue("@MaSanPham", ct.MaSanPham);
                    cmdCT.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                    cmdCT.Parameters.AddWithValue("@DonGia", ct.DonGia);
                    cmdCT.ExecuteNonQuery();
                }

                tran.Commit();
                return maHoaDon;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public bool CapNhatTrangThaiHoaDon(int maHoaDon, string trangThaiMoi)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = "UPDATE dbo.HoaDon SET TrangThai = @TrangThai WHERE MaHoaDon = @MaHoaDon";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
            cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

