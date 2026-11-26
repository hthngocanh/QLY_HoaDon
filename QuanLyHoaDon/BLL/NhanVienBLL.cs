using System.Collections.Generic;
using QuanLyHoaDon.DAL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL
{
    public class NhanVienBLL
    {
        private readonly NhanVienDAL _dal = new();

        public List<NhanVienQuanLyDTO> LayDanhSach(string? tuKhoa = null)
        {
            // Chỉ Quản lý được truy cập
            if (!Session.LaQuanLy)
            {
                return new List<NhanVienQuanLyDTO>();
            }
            return _dal.LayDanhSach(tuKhoa);
        }

        public KetQuaThaoTacDTO ThemNhanVien(NhanVienQuanLyDTO nv, string matKhau)
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng quản lý nhân sự chỉ dành cho Quản lý hệ thống.");
            }

            if (string.IsNullOrWhiteSpace(nv.HoTen))
            {
                return KetQuaThaoTacDTO.ThatBai("Họ tên nhân viên không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(nv.TenDangNhap))
            {
                return KetQuaThaoTacDTO.ThatBai("Tên đăng nhập tài khoản không được để trống.");
            }

            if (_dal.KiemTraTrungTenDangNhap(nv.TenDangNhap))
            {
                return KetQuaThaoTacDTO.ThatBai("Tên đăng nhập đã tồn tại trong hệ thống.");
            }

            if (string.IsNullOrWhiteSpace(matKhau) || matKhau.Length < 6)
            {
                return KetQuaThaoTacDTO.ThatBai("Mật khẩu khởi tạo phải từ 6 ký tự trở lên.");
            }

            if (!string.IsNullOrWhiteSpace(nv.SoDienThoai) && !ValidationHelper.KiemTraSoDienThoai(nv.SoDienThoai))
            {
                return KetQuaThaoTacDTO.ThatBai("Số điện thoại không hợp lệ.");
            }

            if (!string.IsNullOrWhiteSpace(nv.Email) && !ValidationHelper.KiemTraEmail(nv.Email))
            {
                return KetQuaThaoTacDTO.ThatBai("Email nhân viên không hợp lệ.");
            }

            bool kq = _dal.ThemNhanVienVaTaiKhoan(nv, matKhau);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Thêm mới nhân viên và tài khoản thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể tạo nhân viên.");
        }

        public KetQuaThaoTacDTO CapNhatNhanVien(NhanVienQuanLyDTO nv)
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng quản lý nhân sự chỉ dành cho Quản lý hệ thống.");
            }

            if (nv.MaNhanVien <= 0)
            {
                return KetQuaThaoTacDTO.ThatBai("Mã nhân viên không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(nv.HoTen))
            {
                return KetQuaThaoTacDTO.ThatBai("Họ tên nhân viên không được để trống.");
            }

            if (!string.IsNullOrWhiteSpace(nv.SoDienThoai) && !ValidationHelper.KiemTraSoDienThoai(nv.SoDienThoai))
            {
                return KetQuaThaoTacDTO.ThatBai("Số điện thoại không hợp lệ.");
            }

            if (!string.IsNullOrWhiteSpace(nv.Email) && !ValidationHelper.KiemTraEmail(nv.Email))
            {
                return KetQuaThaoTacDTO.ThatBai("Email không hợp lệ.");
            }

            bool kq = _dal.CapNhatNhanVien(nv);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Cập nhật thông tin nhân viên thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể cập nhật nhân viên.");
        }

        public KetQuaThaoTacDTO KhoaMoKhoaTaiKhoan(int maTaiKhoan, bool muonKhoa)
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng khóa/mở khóa tài khoản chỉ dành cho Quản lý hệ thống.");
            }

            if (maTaiKhoan == Session.MaTaiKhoan)
            {
                return KetQuaThaoTacDTO.ThatBai("Bạn không thể tự khóa tài khoản của chính mình.");
            }

            string trangThaiMoi = muonKhoa ? "BiKhoa" : "HoatDong";
            bool kq = _dal.ThayDoiTrangThaiTaiKhoan(maTaiKhoan, trangThaiMoi);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq($"{(muonKhoa ? "Khóa" : "Mở khóa")} tài khoản thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể cập nhật trạng thái tài khoản.");
        }

        public KetQuaThaoTacDTO CapLaiMatKhau(int maTaiKhoan, string matKhauMoi = "Demo@123")
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng cấp lại mật khẩu chỉ dành cho Quản lý hệ thống.");
            }

            if (maTaiKhoan <= 0)
            {
                return KetQuaThaoTacDTO.ThatBai("Tài khoản không hợp lệ hoặc chưa được khởi tạo.");
            }

            string hashMoi = PasswordHelper.BamMatKhau(matKhauMoi);
            bool kq = _dal.CapLaiMatKhau(maTaiKhoan, hashMoi);
            return kq
                ? KetQuaThaoTacDTO.ThanhCongKq($"Đã cấp lại mật khẩu thành công! Mật khẩu mới được thiết lập về: {matKhauMoi}")
                : KetQuaThaoTacDTO.ThatBai("Không thể cấp lại mật khẩu.");
        }
    }
}
