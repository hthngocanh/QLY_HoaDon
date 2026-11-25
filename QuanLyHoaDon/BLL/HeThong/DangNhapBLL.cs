using System;
using QuanLyHoaDon.DAL.HeThong;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL.HeThong
{
    public class DangNhapBLL
    {
        private readonly DangNhapDAL _dangNhapDAL;

        public DangNhapBLL()
        {
            _dangNhapDAL = new DangNhapDAL();
        }

        public DangNhapBLL(DangNhapDAL dangNhapDAL)
        {
            _dangNhapDAL = dangNhapDAL;
        }

        public KetQuaDangNhapDTO DangNhap(string tenDangNhap, string matKhau)
        {
            // 1. Kiem tra du lieu dau vao
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                return KetQuaDangNhapDTO.TaoThatBai("Vui lòng nhập tên đăng nhập.");
            }

            if (string.IsNullOrWhiteSpace(matKhau))
            {
                return KetQuaDangNhapDTO.TaoThatBai("Vui lòng nhập mật khẩu.");
            }

            try
            {
                // 2. Truy van thong tin tai khoan tu DAL
                var taiKhoan = _dangNhapDAL.LayThongTinDangNhap(tenDangNhap);
                if (taiKhoan == null)
                {
                    return KetQuaDangNhapDTO.TaoThatBai("Tên đăng nhập không tồn tại trên hệ thống.");
                }

                // 3. Xac thuc mat khau bang PasswordHelper
                if (!PasswordHelper.XacThucMatKhau(matKhau, taiKhoan.MatKhauHash))
                {
                    return KetQuaDangNhapDTO.TaoThatBai("Mật khẩu không chính xác. Vui lòng thử lại.");
                }

                // 4. Kiem tra trang thai tai khoan
                if (!string.Equals(taiKhoan.TrangThai, "HoatDong", StringComparison.OrdinalIgnoreCase))
                {
                    return KetQuaDangNhapDTO.TaoThatBai("Tài khoản của bạn đang bị khóa. Vui lòng liên hệ Quản lý.");
                }

                // 5. Kiem tra trang thai nhan vien
                if (!string.Equals(taiKhoan.TrangThaiNhanVien, "DangHoatDong", StringComparison.OrdinalIgnoreCase))
                {
                    return KetQuaDangNhapDTO.TaoThatBai("Nhân viên liên kết đã ngừng hoạt động trên hệ thống.");
                }

                // 6. Luu thong tin vao Session
                Session.DangNhap(
                    taiKhoan.MaTaiKhoan,
                    taiKhoan.MaNhanVien,
                    taiKhoan.HoTen,
                    taiKhoan.TenDangNhap,
                    taiKhoan.MaVaiTro,
                    taiKhoan.TenVaiTro,
                    taiKhoan.SoDienThoai,
                    taiKhoan.Email,
                    taiKhoan.DiaChi,
                    taiKhoan.NgayVaoLam,
                    taiKhoan.TrangThaiNhanVien,
                    taiKhoan.TrangThai
                );

                return KetQuaDangNhapDTO.TaoThanhCong(taiKhoan, "Đăng nhập thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaDangNhapDTO.TaoThatBai($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
        }

        public void DangXuat()
        {
            Session.DangXuat();
        }
    }
}

