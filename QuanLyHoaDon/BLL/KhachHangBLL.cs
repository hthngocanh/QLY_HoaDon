using System.Collections.Generic;
using QuanLyHoaDon.DAL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL
{
    public class KhachHangBLL
    {
        private readonly KhachHangDAL _dal = new();

        public List<KhachHangDTO> LayDanhSach(string? tuKhoa = null)
        {
            return _dal.LayDanhSach(tuKhoa);
        }

        public KetQuaThaoTacDTO ThemKhachHang(KhachHangDTO kh)
        {
            // Phân quyền: Nhân viên được thêm, Quản lý chỉ được xem
            if (!Session.LaNhanVien)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng thêm mới khách hàng chỉ dành cho tài khoản Nhân viên bán hàng.");
            }

            if (string.IsNullOrWhiteSpace(kh.HoTen))
            {
                return KetQuaThaoTacDTO.ThatBai("Họ tên khách hàng không được để trống.");
            }

            if (!string.IsNullOrWhiteSpace(kh.SoDienThoai) && !ValidationHelper.KiemTraSoDienThoai(kh.SoDienThoai))
            {
                return KetQuaThaoTacDTO.ThatBai("Số điện thoại không hợp lệ (cần 10 chữ số).");
            }

            if (!string.IsNullOrWhiteSpace(kh.Email) && !ValidationHelper.KiemTraEmail(kh.Email))
            {
                return KetQuaThaoTacDTO.ThatBai("Email khách hàng không đúng định dạng.");
            }

            bool kq = _dal.ThemKhachHang(kh);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Thêm khách hàng thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể thêm khách hàng vào hệ thống.");
        }

        public KetQuaThaoTacDTO CapNhatKhachHang(KhachHangDTO kh)
        {
            // Phân quyền: Nhân viên được cập nhật, Quản lý chỉ được xem
            if (!Session.LaNhanVien)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng cập nhật thông tin khách hàng chỉ dành cho tài khoản Nhân viên bán hàng.");
            }

            if (kh.MaKhachHang <= 0)
            {
                return KetQuaThaoTacDTO.ThatBai("Mã khách hàng không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(kh.HoTen))
            {
                return KetQuaThaoTacDTO.ThatBai("Họ tên khách hàng không được để trống.");
            }

            if (!string.IsNullOrWhiteSpace(kh.SoDienThoai) && !ValidationHelper.KiemTraSoDienThoai(kh.SoDienThoai))
            {
                return KetQuaThaoTacDTO.ThatBai("Số điện thoại không hợp lệ.");
            }

            if (!string.IsNullOrWhiteSpace(kh.Email) && !ValidationHelper.KiemTraEmail(kh.Email))
            {
                return KetQuaThaoTacDTO.ThatBai("Email khách hàng không đúng định dạng.");
            }

            bool kq = _dal.CapNhatKhachHang(kh);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Cập nhật thông tin khách hàng thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể cập nhật khách hàng.");
        }
    }
}
