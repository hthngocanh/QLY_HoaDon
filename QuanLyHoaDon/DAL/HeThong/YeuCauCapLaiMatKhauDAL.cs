using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;

namespace QuanLyHoaDon.DAL.HeThong
{
    public class YeuCauCapLaiMatKhauDAL
    {
        public List<YeuCauCapLaiMatKhauDTO> LayDanhSach(string? trangThai = null, string? tuKhoa = null)
        {
            var ds = new List<YeuCauCapLaiMatKhauDTO>();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"
                SELECT 
                    yc.MaYeuCau,
                    yc.MaTaiKhoan,
                    tk.TenDangNhap,
                    nv.HoTen AS HoTenNhanVien,
                    nv.Email,
                    nv.SoDienThoai,
                    yc.LyDoYeuCau,
                    yc.TrangThai,
                    yc.ThoiGianYeuCau,
                    yc.NguoiDuyet,
                    nd.HoTen AS TenNguoiDuyet,
                    yc.ThoiGianDuyet,
                    yc.LyDoTuChoi,
                    yc.ThoiGianHoanTat
                FROM dbo.YeuCauCapLaiMatKhau yc
                INNER JOIN dbo.TaiKhoan tk ON tk.MaTaiKhoan = yc.MaTaiKhoan
                INNER JOIN dbo.NhanVien nv ON nv.MaNhanVien = tk.MaNhanVien
                LEFT JOIN dbo.NhanVien nd ON nd.MaNhanVien = yc.NguoiDuyet
                WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "TatCa")
            {
                sql += " AND yc.TrangThai = @TrangThai";
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                sql += @" AND (tk.TenDangNhap LIKE @TuKhoa 
                             OR nv.HoTen LIKE @TuKhoa 
                             OR nv.Email LIKE @TuKhoa 
                             OR nv.SoDienThoai LIKE @TuKhoa
                             OR yc.LyDoYeuCau LIKE @TuKhoa)";
            }

            sql += " ORDER BY yc.MaYeuCau DESC";

            using var cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "TatCa")
            {
                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = trangThai.Trim();
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                cmd.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 100).Value = "%" + tuKhoa.Trim() + "%";
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new YeuCauCapLaiMatKhauDTO
                {
                    MaYeuCau = reader.GetInt32(0),
                    MaTaiKhoan = reader.GetInt32(1),
                    TenDangNhap = reader.GetString(2),
                    HoTenNhanVien = reader.GetString(3),
                    Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                    SoDienThoai = reader.IsDBNull(5) ? null : reader.GetString(5),
                    LyDoYeuCau = reader.IsDBNull(6) ? null : reader.GetString(6),
                    TrangThai = reader.GetString(7),
                    ThoiGianYeuCau = reader.GetDateTime(8),
                    NguoiDuyet = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                    TenNguoiDuyet = reader.IsDBNull(10) ? null : reader.GetString(10),
                    ThoiGianDuyet = reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                    LyDoTuChoi = reader.IsDBNull(12) ? null : reader.GetString(12),
                    ThoiGianHoanTat = reader.IsDBNull(13) ? null : reader.GetDateTime(13)
                });
            }

            return ds;
        }

        public YeuCauCapLaiMatKhauDTO? LayTheoMa(int maYeuCau)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            const string sql = @"
                SELECT 
                    yc.MaYeuCau,
                    yc.MaTaiKhoan,
                    tk.TenDangNhap,
                    nv.HoTen AS HoTenNhanVien,
                    nv.Email,
                    nv.SoDienThoai,
                    yc.LyDoYeuCau,
                    yc.TrangThai,
                    yc.ThoiGianYeuCau,
                    yc.NguoiDuyet,
                    nd.HoTen AS TenNguoiDuyet,
                    yc.ThoiGianDuyet,
                    yc.LyDoTuChoi,
                    yc.ThoiGianHoanTat
                FROM dbo.YeuCauCapLaiMatKhau yc
                INNER JOIN dbo.TaiKhoan tk ON tk.MaTaiKhoan = yc.MaTaiKhoan
                INNER JOIN dbo.NhanVien nv ON nv.MaNhanVien = tk.MaNhanVien
                LEFT JOIN dbo.NhanVien nd ON nd.MaNhanVien = yc.NguoiDuyet
                WHERE yc.MaYeuCau = @MaYeuCau";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaYeuCau", SqlDbType.Int).Value = maYeuCau;

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new YeuCauCapLaiMatKhauDTO
                {
                    MaYeuCau = reader.GetInt32(0),
                    MaTaiKhoan = reader.GetInt32(1),
                    TenDangNhap = reader.GetString(2),
                    HoTenNhanVien = reader.GetString(3),
                    Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                    SoDienThoai = reader.IsDBNull(5) ? null : reader.GetString(5),
                    LyDoYeuCau = reader.IsDBNull(6) ? null : reader.GetString(6),
                    TrangThai = reader.GetString(7),
                    ThoiGianYeuCau = reader.GetDateTime(8),
                    NguoiDuyet = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                    TenNguoiDuyet = reader.IsDBNull(10) ? null : reader.GetString(10),
                    ThoiGianDuyet = reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                    LyDoTuChoi = reader.IsDBNull(12) ? null : reader.GetString(12),
                    ThoiGianHoanTat = reader.IsDBNull(13) ? null : reader.GetDateTime(13)
                };
            }

            return null;
        }

        public YeuCauCapLaiMatKhauDTO? LayYeuCauChoDuyetCuaTaiKhoan(int maTaiKhoan)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            const string sql = @"
                SELECT TOP 1
                    yc.MaYeuCau,
                    yc.MaTaiKhoan,
                    tk.TenDangNhap,
                    nv.HoTen AS HoTenNhanVien,
                    nv.Email,
                    nv.SoDienThoai,
                    yc.LyDoYeuCau,
                    yc.TrangThai,
                    yc.ThoiGianYeuCau,
                    yc.NguoiDuyet,
                    NULL AS TenNguoiDuyet,
                    yc.ThoiGianDuyet,
                    yc.LyDoTuChoi,
                    yc.ThoiGianHoanTat
                FROM dbo.YeuCauCapLaiMatKhau yc
                INNER JOIN dbo.TaiKhoan tk ON tk.MaTaiKhoan = yc.MaTaiKhoan
                INNER JOIN dbo.NhanVien nv ON nv.MaNhanVien = tk.MaNhanVien
                WHERE yc.MaTaiKhoan = @MaTaiKhoan AND yc.TrangThai = N'ChoDuyet'
                ORDER BY yc.MaYeuCau DESC";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaTaiKhoan", SqlDbType.Int).Value = maTaiKhoan;

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new YeuCauCapLaiMatKhauDTO
                {
                    MaYeuCau = reader.GetInt32(0),
                    MaTaiKhoan = reader.GetInt32(1),
                    TenDangNhap = reader.GetString(2),
                    HoTenNhanVien = reader.GetString(3),
                    Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                    SoDienThoai = reader.IsDBNull(5) ? null : reader.GetString(5),
                    LyDoYeuCau = reader.IsDBNull(6) ? null : reader.GetString(6),
                    TrangThai = reader.GetString(7),
                    ThoiGianYeuCau = reader.GetDateTime(8),
                    NguoiDuyet = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                    TenNguoiDuyet = null,
                    ThoiGianDuyet = reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                    LyDoTuChoi = reader.IsDBNull(12) ? null : reader.GetString(12),
                    ThoiGianHoanTat = reader.IsDBNull(13) ? null : reader.GetDateTime(13)
                };
            }

            return null;
        }

        public YeuCauCapLaiMatKhauDTO? LayYeuCauMoiNhatCuaTaiKhoan(string tenDangNhap)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            const string sql = @"
                SELECT TOP 1
                    yc.MaYeuCau,
                    yc.MaTaiKhoan,
                    tk.TenDangNhap,
                    nv.HoTen AS HoTenNhanVien,
                    nv.Email,
                    nv.SoDienThoai,
                    yc.LyDoYeuCau,
                    yc.TrangThai,
                    yc.ThoiGianYeuCau,
                    yc.NguoiDuyet,
                    nd.HoTen AS TenNguoiDuyet,
                    yc.ThoiGianDuyet,
                    yc.LyDoTuChoi,
                    yc.ThoiGianHoanTat
                FROM dbo.YeuCauCapLaiMatKhau yc
                INNER JOIN dbo.TaiKhoan tk ON tk.MaTaiKhoan = yc.MaTaiKhoan
                INNER JOIN dbo.NhanVien nv ON nv.MaNhanVien = tk.MaNhanVien
                LEFT JOIN dbo.NhanVien nd ON nd.MaNhanVien = yc.NguoiDuyet
                WHERE tk.TenDangNhap = @TenDangNhap
                ORDER BY yc.MaYeuCau DESC";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = tenDangNhap.Trim();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new YeuCauCapLaiMatKhauDTO
                {
                    MaYeuCau = reader.GetInt32(0),
                    MaTaiKhoan = reader.GetInt32(1),
                    TenDangNhap = reader.GetString(2),
                    HoTenNhanVien = reader.GetString(3),
                    Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                    SoDienThoai = reader.IsDBNull(5) ? null : reader.GetString(5),
                    LyDoYeuCau = reader.IsDBNull(6) ? null : reader.GetString(6),
                    TrangThai = reader.GetString(7),
                    ThoiGianYeuCau = reader.GetDateTime(8),
                    NguoiDuyet = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                    TenNguoiDuyet = reader.IsDBNull(10) ? null : reader.GetString(10),
                    ThoiGianDuyet = reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                    LyDoTuChoi = reader.IsDBNull(12) ? null : reader.GetString(12),
                    ThoiGianHoanTat = reader.IsDBNull(13) ? null : reader.GetDateTime(13)
                };
            }

            return null;
        }

        public bool TaoYeuCau(int maTaiKhoan, string? lyDoYeuCau)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            const string sql = @"
                INSERT INTO dbo.YeuCauCapLaiMatKhau (MaTaiKhoan, LyDoYeuCau, TrangThai, ThoiGianYeuCau)
                VALUES (@MaTaiKhoan, @LyDoYeuCau, N'ChoDuyet', GETDATE())";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaTaiKhoan", SqlDbType.Int).Value = maTaiKhoan;
            cmd.Parameters.Add("@LyDoYeuCau", SqlDbType.NVarChar, 500).Value = 
                string.IsNullOrWhiteSpace(lyDoYeuCau) ? DBNull.Value : lyDoYeuCau.Trim();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DuyetYeuCau(int maYeuCau, int maNguoiDuyet)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            // Chỉ cập nhật nếu trạng thái hiện tại là ChoDuyet để tránh race condition
            const string sql = @"
                UPDATE dbo.YeuCauCapLaiMatKhau
                SET TrangThai = N'DaDuyet',
                    NguoiDuyet = @NguoiDuyet,
                    ThoiGianDuyet = GETDATE()
                WHERE MaYeuCau = @MaYeuCau AND TrangThai = N'ChoDuyet'";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaYeuCau", SqlDbType.Int).Value = maYeuCau;
            cmd.Parameters.Add("@NguoiDuyet", SqlDbType.Int).Value = maNguoiDuyet;

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool TuChoiYeuCau(int maYeuCau, int maNguoiDuyet, string lyDoTuChoi)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            // Chỉ cập nhật nếu trạng thái hiện tại là ChoDuyet
            const string sql = @"
                UPDATE dbo.YeuCauCapLaiMatKhau
                SET TrangThai = N'DaTuChoi',
                    NguoiDuyet = @NguoiDuyet,
                    ThoiGianDuyet = GETDATE(),
                    LyDoTuChoi = @LyDoTuChoi
                WHERE MaYeuCau = @MaYeuCau AND TrangThai = N'ChoDuyet'";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaYeuCau", SqlDbType.Int).Value = maYeuCau;
            cmd.Parameters.Add("@NguoiDuyet", SqlDbType.Int).Value = maNguoiDuyet;
            cmd.Parameters.Add("@LyDoTuChoi", SqlDbType.NVarChar, 500).Value = lyDoTuChoi.Trim();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool HoanTatDatLaiMatKhau(int maYeuCau, int maTaiKhoan, string matKhauHashMoi)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                // 1. Cập nhật mật khẩu mới cho tài khoản
                const string sqlTK = @"
                    UPDATE dbo.TaiKhoan
                    SET MatKhauHash = @MatKhauHash
                    WHERE MaTaiKhoan = @MaTaiKhoan";

                using var cmdTK = new SqlCommand(sqlTK, conn, tran);
                cmdTK.Parameters.Add("@MaTaiKhoan", SqlDbType.Int).Value = maTaiKhoan;
                cmdTK.Parameters.Add("@MatKhauHash", SqlDbType.VarChar, 256).Value = matKhauHashMoi;
                int rowsTK = cmdTK.ExecuteNonQuery();

                if (rowsTK <= 0)
                {
                    tran.Rollback();
                    return false;
                }

                // 2. Chuyển trạng thái yêu cầu sang DaHoanTat
                const string sqlYC = @"
                    UPDATE dbo.YeuCauCapLaiMatKhau
                    SET TrangThai = N'DaHoanTat',
                        ThoiGianHoanTat = GETDATE()
                    WHERE MaYeuCau = @MaYeuCau AND TrangThai = N'DaDuyet'";

                using var cmdYC = new SqlCommand(sqlYC, conn, tran);
                cmdYC.Parameters.Add("@MaYeuCau", SqlDbType.Int).Value = maYeuCau;
                int rowsYC = cmdYC.ExecuteNonQuery();

                if (rowsYC <= 0)
                {
                    tran.Rollback();
                    return false;
                }

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
