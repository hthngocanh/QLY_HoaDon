using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;

namespace QuanLyHoaDon.DAL
{
    public class KhachHangDAL
    {
        public List<KhachHangDTO> LayDanhSach(string? tuKhoa = null)
        {
            var ds = new List<KhachHangDTO>();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = "SELECT MaKhachHang, HoTen, SoDienThoai, Email, DiaChi FROM dbo.KhachHang";
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                sql += " WHERE HoTen LIKE @TuKhoa OR SoDienThoai LIKE @TuKhoa OR Email LIKE @TuKhoa OR DiaChi LIKE @TuKhoa";
            }
            sql += " ORDER BY MaKhachHang DESC";

            using var cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa.Trim() + "%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new KhachHangDTO
                {
                    MaKhachHang = reader.GetInt32(0),
                    HoTen = reader.GetString(1),
                    SoDienThoai = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                    DiaChi = reader.IsDBNull(4) ? null : reader.GetString(4)
                });
            }
            return ds;
        }

        public bool ThemKhachHang(KhachHangDTO kh)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"INSERT INTO dbo.KhachHang (HoTen, SoDienThoai, Email, DiaChi)
                           VALUES (@HoTen, @SoDienThoai, @Email, @DiaChi)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HoTen", kh.HoTen.Trim());
            cmd.Parameters.AddWithValue("@SoDienThoai", (object?)kh.SoDienThoai?.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)kh.Email?.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DiaChi", (object?)kh.DiaChi?.Trim() ?? DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CapNhatKhachHang(KhachHangDTO kh)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"UPDATE dbo.KhachHang
                           SET HoTen = @HoTen,
                               SoDienThoai = @SoDienThoai,
                               Email = @Email,
                               DiaChi = @DiaChi
                           WHERE MaKhachHang = @MaKhachHang";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaKhachHang", kh.MaKhachHang);
            cmd.Parameters.AddWithValue("@HoTen", kh.HoTen.Trim());
            cmd.Parameters.AddWithValue("@SoDienThoai", (object?)kh.SoDienThoai?.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)kh.Email?.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DiaChi", (object?)kh.DiaChi?.Trim() ?? DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

