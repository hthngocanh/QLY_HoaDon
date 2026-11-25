using System;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;

namespace QuanLyHoaDon.DAL.HeThong
{
    public class DangNhapDAL
    {
        public TaiKhoanDTO? LayThongTinDangNhap(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                return null;
            }

            const string sql = @"
                SELECT 
                    t.MaTaiKhoan,
                    t.MaNhanVien,
                    t.MaVaiTro,
                    t.TenDangNhap,
                    t.MatKhauHash,
                    t.TrangThai AS TrangThaiTaiKhoan,
                    t.NgayTao,
                    v.TenVaiTro,
                    n.HoTen,
                    n.SoDienThoai,
                    n.Email,
                    n.DiaChi,
                    n.NgayVaoLam,
                    n.TrangThai AS TrangThaiNhanVien
                FROM dbo.TaiKhoan t
                INNER JOIN dbo.VaiTro v ON t.MaVaiTro = v.MaVaiTro
                INNER JOIN dbo.NhanVien n ON t.MaNhanVien = n.MaNhanVien
                WHERE t.TenDangNhap = @TenDangNhap;";

            using var connection = KetNoiCSDL.TaoKetNoi();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = tenDangNhap.Trim();

            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new TaiKhoanDTO
                {
                    MaTaiKhoan = reader.GetInt32(reader.GetOrdinal("MaTaiKhoan")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaVaiTro = reader.GetInt32(reader.GetOrdinal("MaVaiTro")),
                    TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                    MatKhauHash = reader.GetString(reader.GetOrdinal("MatKhauHash")),
                    TrangThai = reader.GetString(reader.GetOrdinal("TrangThaiTaiKhoan")),
                    NgayTao = reader.GetDateTime(reader.GetOrdinal("NgayTao")),
                    TenVaiTro = reader.GetString(reader.GetOrdinal("TenVaiTro")),
                    HoTen = reader.GetString(reader.GetOrdinal("HoTen")),
                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? string.Empty : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? string.Empty : reader.GetString(reader.GetOrdinal("DiaChi")),
                    NgayVaoLam = reader.IsDBNull(reader.GetOrdinal("NgayVaoLam")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("NgayVaoLam")),
                    TrangThaiNhanVien = reader.GetString(reader.GetOrdinal("TrangThaiNhanVien"))
                };
            }

            return null;
        }
    }
}

