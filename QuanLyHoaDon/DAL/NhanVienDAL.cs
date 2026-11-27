using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.DAL
{
    public class NhanVienDAL
    {
        /// <summary>
        /// Lấy danh sách nhân viên.
        /// Khi chiLayNhanVien = true: Chỉ lấy các tài khoản có vai trò 'NhanVien' (loại bỏ toàn bộ Quản lý và chính tài khoản Quản lý đang đăng nhập).
        /// </summary>
        public List<NhanVienQuanLyDTO> LayDanhSach(string? tuKhoa = null, bool chiLayNhanVien = false, int? maTaiKhoanLoaiTru = null)
        {
            var ds = new List<NhanVienQuanLyDTO>();
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"
                SELECT
                    nv.MaNhanVien, nv.HoTen, nv.SoDienThoai, nv.Email, nv.DiaChi, nv.NgayVaoLam, nv.TrangThai,
                    tk.MaTaiKhoan, tk.TenDangNhap, tk.MaVaiTro, vt.TenVaiTro, tk.TrangThai AS TrangThaiTaiKhoan
                FROM dbo.NhanVien nv
                LEFT JOIN dbo.TaiKhoan tk ON tk.MaNhanVien = nv.MaNhanVien
                LEFT JOIN dbo.VaiTro vt ON vt.MaVaiTro = tk.MaVaiTro
                WHERE 1 = 1";

            if (chiLayNhanVien)
            {
                // Chỉ lấy vai trò NhanVien (MaVaiTro = 2 hoặc vt.TenVaiTro = 'NhanVien')
                // và không phải tài khoản đang đăng nhập
                sql += " AND vt.TenVaiTro = N'NhanVien'";
            }

            if (maTaiKhoanLoaiTru.HasValue)
            {
                sql += " AND (tk.MaTaiKhoan IS NULL OR tk.MaTaiKhoan != @MaTaiKhoanLoaiTru)";
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                sql += @" AND (
                            nv.HoTen LIKE @TuKhoa
                            OR nv.SoDienThoai LIKE @TuKhoa
                            OR tk.TenDangNhap LIKE @TuKhoa
                            OR ('NV' + RIGHT('0000' + CAST(nv.MaNhanVien AS VARCHAR(10)), 4)) LIKE @TuKhoa
                            OR CAST(nv.MaNhanVien AS VARCHAR(10)) LIKE @TuKhoa
                        )";
            }

            sql += " ORDER BY nv.MaNhanVien DESC";

            using var cmd = new SqlCommand(sql, conn);
            if (maTaiKhoanLoaiTru.HasValue)
            {
                cmd.Parameters.AddWithValue("@MaTaiKhoanLoaiTru", maTaiKhoanLoaiTru.Value);
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa.Trim() + "%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new NhanVienQuanLyDTO
                {
                    MaNhanVien = reader.GetInt32(0),
                    HoTen = reader.GetString(1),
                    SoDienThoai = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                    DiaChi = reader.IsDBNull(4) ? null : reader.GetString(4),
                    NgayVaoLam = reader.GetDateTime(5),
                    TrangThaiNhanVien = reader.GetString(6),
                    MaTaiKhoan = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                    TenDangNhap = reader.IsDBNull(8) ? null : reader.GetString(8),
                    MaVaiTro = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                    TenVaiTro = reader.IsDBNull(10) ? null : reader.GetString(10),
                    TrangThaiTaiKhoan = reader.IsDBNull(11) ? null : reader.GetString(11)
                });
            }
            return ds;
        }

        /// <summary>
        /// Lấy mã nhân viên tự tăng tiếp theo để hiển thị dự kiến trên giao diện và đồng bộ username
        /// </summary>
        public int LayMaNhanVienTiepTheo()
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            // Lấy max MaNhanVien hiện tại hoặc IDENT_CURRENT
            string sql = @"
                SELECT ISNULL(MAX(MaNhanVien), 0) + 1 FROM dbo.NhanVien";

            using var cmd = new SqlCommand(sql, conn);
            object? res = cmd.ExecuteScalar();
            return (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 1;
        }

        public bool KiemTraTrungTenDangNhap(string tenDangNhap, int? maTaiKhoanBoQua = null)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = "SELECT COUNT(1) FROM dbo.TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            if (maTaiKhoanBoQua.HasValue)
            {
                sql += " AND MaTaiKhoan != @MaTaiKhoanBoQua";
            }

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap.Trim());
            if (maTaiKhoanBoQua.HasValue)
            {
                cmd.Parameters.AddWithValue("@MaTaiKhoanBoQua", maTaiKhoanBoQua.Value);
            }

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool KiemTraTrungMaNhanVien(int maNhanVien)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = "SELECT COUNT(1) FROM dbo.NhanVien WHERE MaNhanVien = @MaNhanVien";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        /// <summary>
        /// Lấy thông tin tài khoản để kiểm tra vai trò thực tế từ CSDL
        /// </summary>
        public (int MaTaiKhoan, int MaNhanVien, string TenVaiTro, string TrangThai)? LayThongTinTaiKhoan(int maTaiKhoan)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = @"
SELECT tk.MaTaiKhoan, tk.MaNhanVien, vt.TenVaiTro, tk.TrangThai
                FROM dbo.TaiKhoan tk
                INNER JOIN dbo.VaiTro vt ON vt.MaVaiTro = tk.MaVaiTro
                WHERE tk.MaTaiKhoan = @MaTaiKhoan";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return (
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetString(3)
                );
            }
            return null;
        }

        /// <summary>
        /// Tạo hồ sơ nhân viên và tài khoản trong cùng 1 Transaction.
        /// Đảm bảo tính toàn vẹn: nếu 1 trong 2 thất bại, rollback toàn bộ.
        /// </summary>
        public bool ThemNhanVienVaTaiKhoan(NhanVienQuanLyDTO nv, string matKhau)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                // 1. Insert NhanVien (Không chèn Email theo yêu cầu chuẩn hóa)
                string sqlNV = @"
                    INSERT INTO dbo.NhanVien (HoTen, SoDienThoai, Email, DiaChi, NgayVaoLam, TrangThai)
                    OUTPUT INSERTED.MaNhanVien
                    VALUES (@HoTen, @SoDienThoai, @Email, @DiaChi, @NgayVaoLam, @TrangThai)";

                using var cmdNV = new SqlCommand(sqlNV, conn, tran);
                cmdNV.Parameters.AddWithValue("@HoTen", nv.HoTen.Trim());
                cmdNV.Parameters.AddWithValue("@SoDienThoai", (object?)nv.SoDienThoai?.Trim() ?? DBNull.Value);
                cmdNV.Parameters.AddWithValue("@Email", (object?)nv.Email?.Trim() ?? DBNull.Value);
                cmdNV.Parameters.AddWithValue("@DiaChi", (object?)nv.DiaChi?.Trim() ?? DBNull.Value);
                cmdNV.Parameters.AddWithValue("@NgayVaoLam", nv.NgayVaoLam);
                cmdNV.Parameters.AddWithValue("@TrangThai", nv.TrangThaiNhanVien ?? "DangHoatDong");

                int maNhanVien = (int)cmdNV.ExecuteScalar()!;
                nv.MaNhanVien = maNhanVien;

                // Đồng bộ tên đăng nhập nếu chưa có hoặc khớp với mã nhân viên vừa sinh
                if (string.IsNullOrWhiteSpace(nv.TenDangNhap))
                {
                    nv.TenDangNhap = $"nv{maNhanVien:D4}".ToLowerInvariant();
                }

                // 2. Insert TaiKhoan (bắt buộc vai trò Nhân viên: MaVaiTro = 2)
                string matKhauHash = PasswordHelper.BamMatKhau(matKhau);
                string sqlTK = @"
                    INSERT INTO dbo.TaiKhoan (MaNhanVien, MaVaiTro, TenDangNhap, MatKhauHash, TrangThai)
                    VALUES (@MaNhanVien, @MaVaiTro, @TenDangNhap, @MatKhauHash, @TrangThai)";
                using var cmdTK = new SqlCommand(sqlTK, conn, tran);
                cmdTK.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmdTK.Parameters.AddWithValue("@MaVaiTro", nv.MaVaiTro ?? 2);
                cmdTK.Parameters.AddWithValue("@TenDangNhap", nv.TenDangNhap.Trim().ToLowerInvariant());
                cmdTK.Parameters.AddWithValue("@MatKhauHash", matKhauHash);
                cmdTK.Parameters.AddWithValue("@TrangThai", nv.TrangThaiTaiKhoan ?? "HoatDong");

                cmdTK.ExecuteNonQuery();

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public bool CapNhatNhanVien(NhanVienQuanLyDTO nv)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                string sqlNV = @"
                    UPDATE dbo.NhanVien
                    SET HoTen = @HoTen,
                        SoDienThoai = @SoDienThoai,
                        Email = @Email,
                        DiaChi = @DiaChi,
                        TrangThai = @TrangThai
                    WHERE MaNhanVien = @MaNhanVien";

                using var cmdNV = new SqlCommand(sqlNV, conn, tran);
                cmdNV.Parameters.AddWithValue("@MaNhanVien", nv.MaNhanVien);
                cmdNV.Parameters.AddWithValue("@HoTen", nv.HoTen.Trim());
                cmdNV.Parameters.AddWithValue("@SoDienThoai", (object?)nv.SoDienThoai?.Trim() ?? DBNull.Value);
                cmdNV.Parameters.AddWithValue("@Email", (object?)nv.Email?.Trim() ?? DBNull.Value);
                cmdNV.Parameters.AddWithValue("@DiaChi", (object?)nv.DiaChi?.Trim() ?? DBNull.Value);
                cmdNV.Parameters.AddWithValue("@TrangThai", nv.TrangThaiNhanVien);
                cmdNV.ExecuteNonQuery();

                if (nv.MaTaiKhoan.HasValue)
                {
                    string sqlTK = @"
                        UPDATE dbo.TaiKhoan
                        SET MaVaiTro = @MaVaiTro,
                            TrangThai = @TrangThaiTK
                        WHERE MaTaiKhoan = @MaTaiKhoan";

                    using var cmdTK = new SqlCommand(sqlTK, conn, tran);
                    cmdTK.Parameters.AddWithValue("@MaTaiKhoan", nv.MaTaiKhoan.Value);
                    cmdTK.Parameters.AddWithValue("@MaVaiTro", nv.MaVaiTro ?? 2);
                    cmdTK.Parameters.AddWithValue("@TrangThaiTK", nv.TrangThaiTaiKhoan ?? "HoatDong");
                    cmdTK.ExecuteNonQuery();
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

        public bool ThayDoiTrangThaiTaiKhoan(int maTaiKhoan, string trangThaiMoi)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = "UPDATE dbo.TaiKhoan SET TrangThai = @TrangThai WHERE MaTaiKhoan = @MaTaiKhoan";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
            cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CapLaiMatKhau(int maTaiKhoan, string matKhauMoiHash)
        {
            using var conn = KetNoiCSDL.TaoKetNoi();
            conn.Open();

            string sql = "UPDATE dbo.TaiKhoan SET MatKhauHash = @MatKhauHash WHERE MaTaiKhoan = @MaTaiKhoan";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
            cmd.Parameters.AddWithValue("@MatKhauHash", matKhauMoiHash);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

