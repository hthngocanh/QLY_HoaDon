using System;
using QuanLyHoaDon.DAL.HeThong;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL.HeThong
{
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL _taiKhoanDAL;

        public TaiKhoanBLL()
        {
            _taiKhoanDAL = new TaiKhoanDAL();
        }

        public TaiKhoanBLL(TaiKhoanDAL taiKhoanDAL)
        {
            _taiKhoanDAL = taiKhoanDAL;
        }

        public KetQuaThaoTacDTO CapNhatThongTinLienHe(int maNhanVien, string soDienThoai, string email, string diaChi)
        {
            soDienThoai = soDienThoai?.Trim() ?? string.Empty;
            email = email?.Trim() ?? string.Empty;
            diaChi = diaChi?.Trim() ?? string.Empty;

            // 1. Kiểm tra định dạng số điện thoại
            if (!string.IsNullOrEmpty(soDienThoai) && !ValidationHelper.KiemTraSoDienThoai(soDienThoai))
            {
                return KetQuaThaoTacDTO.TaoThatBai("Số điện thoại không hợp lệ (cần gồm 10 chữ số, bắt đầu bằng 0 hoặc +84).");
            }

            // 2. Kiểm tra định dạng email
            if (!string.IsNullOrEmpty(email) && !ValidationHelper.KiemTraEmail(email))
            {
                return KetQuaThaoTacDTO.TaoThatBai("Email không đúng định dạng (ví dụ: hoten@domain.com).");
            }

            try
            {
                // 3. Kiểm tra trùng lặp số điện thoại
                if (!string.IsNullOrEmpty(soDienThoai) && _taiKhoanDAL.KiemTraTrungSoDienThoai(soDienThoai, maNhanVien))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Số điện thoại này đã được sử dụng bởi nhân sự khác.");
                }

                // 4. Kiểm tra trùng lặp email
                if (!string.IsNullOrEmpty(email) && _taiKhoanDAL.KiemTraTrungEmail(email, maNhanVien))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Email này đã được sử dụng bởi nhân sự khác.");
                }

                // 5. Cập nhật cơ sở dữ liệu
                bool ketQua = _taiKhoanDAL.CapNhatThongTinNhanVien(maNhanVien, soDienThoai, email, diaChi);
                if (!ketQua)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Không tìm thấy thông tin nhân viên để cập nhật.");
                }

                // 6. Đồng bộ dữ liệu vào Session
                Session.CapNhatThongTinLienHe(soDienThoai, email, diaChi);

                return KetQuaThaoTacDTO.TaoThanhCong("Cập nhật thông tin cá nhân thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaThaoTacDTO.TaoThatBai($"Lỗi cập nhật dữ liệu: {ex.Message}");
            }
        }

        public KetQuaDoiMatKhauDTO DoiMatKhau(int maTaiKhoan, string matKhauHienTai, string matKhauMoi, string xacNhanMatKhau)
        {
            // 1. Kiểm tra phân quyền: Chức năng chỉ dành cho Nhân viên
            if (!Session.LaNhanVien)
            {
                return KetQuaDoiMatKhauDTO.TaoThatBai("Chức năng cập nhật mật khẩu chỉ dành cho tài khoản Nhân viên.");
            }

            string loiHienTai = string.Empty;
            string loiMoi = string.Empty;
            string loiXacNhan = string.Empty;

            // 2. Kiểm tra từng trường
            if (string.IsNullOrWhiteSpace(matKhauHienTai))
            {
                loiHienTai = "Vui lòng nhập mật khẩu hiện tại.";
            }

            if (string.IsNullOrWhiteSpace(matKhauMoi))
            {
                loiMoi = "Vui lòng nhập mật khẩu mới.";
            }
            else if (matKhauMoi.Length < 6)
            {
                loiMoi = "Mật khẩu mới phải có tối thiểu 6 ký tự.";
            }
            else if (matKhauMoi == matKhauHienTai)
            {
                loiMoi = "Mật khẩu mới không được trùng với mật khẩu hiện tại.";
            }

            if (string.IsNullOrWhiteSpace(xacNhanMatKhau))
            {
                loiXacNhan = "Vui lòng xác nhận mật khẩu mới.";
            }
            else if (matKhauMoi != xacNhanMatKhau)
            {
                loiXacNhan = "Xác nhận mật khẩu mới không trùng khớp.";
            }

            if (!string.IsNullOrEmpty(loiHienTai) || !string.IsNullOrEmpty(loiMoi) || !string.IsNullOrEmpty(loiXacNhan))
            {
                return KetQuaDoiMatKhauDTO.TaoThatBai("Vui lòng kiểm tra lại thông tin nhập liệu.", loiHienTai, loiMoi, loiXacNhan);
            }

            try
            {
                // 3. Kiểm tra mật khẩu hiện tại trong CSDL
                string? hashHienTai = _taiKhoanDAL.LayMatKhauHashHienTai(maTaiKhoan);
                if (string.IsNullOrEmpty(hashHienTai) || !PasswordHelper.XacThucMatKhau(matKhauHienTai, hashHienTai))
                {
                    return KetQuaDoiMatKhauDTO.TaoThatBai(
                        "Mật khẩu hiện tại không chính xác.",
                        loiHienTai: "Mật khẩu hiện tại không chính xác. Vui lòng thử lại.");
                }

                // 4. Băm mật khẩu mới và cập nhật CSDL
                string hashMoi = PasswordHelper.BamMatKhau(matKhauMoi);
                bool capNhat = _taiKhoanDAL.CapNhatMatKhau(maTaiKhoan, hashMoi);
                if (!capNhat)
                {
                    return KetQuaDoiMatKhauDTO.TaoThatBai("Không thể cập nhật mật khẩu vào hệ thống.");
                }

                return KetQuaDoiMatKhauDTO.TaoThanhCong("Cập nhật mật khẩu thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaDoiMatKhauDTO.TaoThatBai($"Lỗi cập nhật mật khẩu: {ex.Message}");
            }
        }
    }
}
