using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyHoaDon.DAL.HeThong
{
    public class TaiKhoanDAL
    {
        public bool CapNhatThongTinNhanVien(int maNhanVien, string soDienThoai, string email, string diaChi)
        {
            const string sql = @"
                UPDATE dbo.NhanVien
                SET SoDienThoai = @SoDienThoai,
                    Email = @Email,
                    DiaChi = @DiaChi
                WHERE MaNhanVien = @MaNhanVien;";

            using var connection = KetNoiCSDL.TaoKetNoi();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = maNhanVien;
            command.Parameters.Add("@SoDienThoai", SqlDbType.VarChar, 15).Value = string.IsNullOrWhiteSpace(soDienThoai) ? DBNull.Value : soDienThoai.Trim();
            command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = string.IsNullOrWhiteSpace(email) ? DBNull.Value : email.Trim();
            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value = string.IsNullOrWhiteSpace(diaChi) ? DBNull.Value : diaChi.Trim();

            connection.Open();
            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public bool KiemTraTrungSoDienThoai(string soDienThoai, int maNhanVienHienTai)
        {
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                return false;
            }

            const string sql = @"
                SELECT COUNT(1)
                FROM dbo.NhanVien
                WHERE SoDienThoai = @SoDienThoai AND MaNhanVien != @MaNhanVien;";

            using var connection = KetNoiCSDL.TaoKetNoi();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@SoDienThoai", SqlDbType.VarChar, 15).Value = soDienThoai.Trim();
            command.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = maNhanVienHienTai;

            connection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        public bool KiemTraTrungEmail(string email, int maNhanVienHienTai)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            const string sql = @"
                SELECT COUNT(1)
                FROM dbo.NhanVien
                WHERE Email = @Email AND MaNhanVien != @MaNhanVien;";

            using var connection = KetNoiCSDL.TaoKetNoi();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email.Trim();
            command.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = maNhanVienHienTai;

            connection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        public string? LayMatKhauHashHienTai(int maTaiKhoan)
        {
            const string sql = "SELECT MatKhauHash FROM dbo.TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan;";

            using var connection = KetNoiCSDL.TaoKetNoi();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@MaTaiKhoan", SqlDbType.Int).Value = maTaiKhoan;

            connection.Open();
            object? result = command.ExecuteScalar();
            return result?.ToString();
        }

        public bool CapNhatMatKhau(int maTaiKhoan, string matKhauHashMoi)
        {
            const string sql = @"
                UPDATE dbo.TaiKhoan
                SET MatKhauHash = @MatKhauHash
                WHERE MaTaiKhoan = @MaTaiKhoan;";

            using var connection = KetNoiCSDL.TaoKetNoi();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@MaTaiKhoan", SqlDbType.Int).Value = maTaiKhoan;
            command.Parameters.Add("@MatKhauHash", SqlDbType.VarChar, 256).Value = matKhauHashMoi;

            connection.Open();
            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}

