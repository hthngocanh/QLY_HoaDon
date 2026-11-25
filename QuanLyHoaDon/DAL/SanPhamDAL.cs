using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;

namespace QuanLyHoaDon.DAL
{
    public class SanPhamDAL
    {
        public List<SanPhamDTO> LayDanhSach(string? tuKhoa = null)
        {
            var ds = new List<SanPhamDTO>();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = "SELECT MaSanPham, TenSanPham, MoTa, DonViTinh, DonGia, TrangThai FROM dbo.SanPham";
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                sql += " WHERE TenSanPham LIKE @TuKhoa OR DonViTinh LIKE @TuKhoa OR TrangThai LIKE @TuKhoa";
            }
            sql += " ORDER BY MaSanPham DESC";

            using var cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa.Trim() + "%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new SanPhamDTO
                {
                    MaSanPham = reader.GetInt32(0),
                    TenSanPham = reader.GetString(1),
                    MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
                    DonViTinh = reader.GetString(3),
                    DonGia = reader.GetDecimal(4),
                    TrangThai = reader.GetString(5)
                });
            }
            return ds;
        }

        public bool ThemSanPham(SanPhamDTO sp)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"INSERT INTO dbo.SanPham (TenSanPham, MoTa, DonViTinh, DonGia, TrangThai)
                           VALUES (@TenSanPham, @MoTa, @DonViTinh, @DonGia, @TrangThai)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham.Trim());
            cmd.Parameters.AddWithValue("@MoTa", (object?)sp.MoTa?.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DonViTinh", sp.DonViTinh.Trim());
            cmd.Parameters.AddWithValue("@DonGia", sp.DonGia);
            cmd.Parameters.AddWithValue("@TrangThai", sp.TrangThai);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CapNhatGiaVaTrangThai(int maSanPham, decimal donGia, string trangThai)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"UPDATE dbo.SanPham
                           SET DonGia = @DonGia,
                               TrangThai = @TrangThai
                           WHERE MaSanPham = @MaSanPham";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
            cmd.Parameters.AddWithValue("@DonGia", donGia);
            cmd.Parameters.AddWithValue("@TrangThai", trangThai);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

