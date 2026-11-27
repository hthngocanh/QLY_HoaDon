using System;
using System.Collections.Generic;
using QuanLyHoaDon.DAL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL
{
    public class NhanVienBLL
    {
        private readonly NhanVienDAL _dal = new();

        /// <summary>
        /// Lấy danh sách nhân viên cho màn hình Quản lý.
        /// Nghiệp vụ: Quản lý chỉ được xem danh sách nhân viên có vai trò 'NhanVien'.
        /// Tuyệt đối không hiển thị tài khoản của Quản lý khác hoặc tài khoản của chính mình.
        /// </summary>
        public List<NhanVienQuanLyDTO> LayDanhSach(string? tuKhoa = null)
        {
            // Chỉ Quản lý được truy cập
            if (!Session.LaQuanLy)
            {
                return new List<NhanVienQuanLyDTO>();
            }

            int? maTKHienTai = Session.DaDangNhap ? Session.MaTaiKhoan : null;
            return _dal.LayDanhSach(tuKhoa, chiLayNhanVien: true, maTaiKhoanLoaiTru: maTKHienTai);
        }

        /// <summary>
        /// Lấy mã nhân viên tiếp theo và sinh tên đăng nhập tương ứng
        /// </summary>
        public (int MaNV, string MaNVFormatted, string TenDangNhap) SinhMaVaTenDangNhapTiepTheo()
        {
            int maNV = _dal.LayMaNhanVienTiepTheo();
            string maNVFormatted = $"NV{maNV:D4}";
            string tenDangNhap = $"nv{maNV:D4}".ToLowerInvariant();
            return (maNV, maNVFormatted, tenDangNhap);
        }

        /// <summary>
        /// Thêm mới nhân viên và tài khoản với các ràng buộc:
        /// - Họ tên: bắt buộc
        /// - Mã nhân viên: hệ thống tự động sinh, chỉ đọc
        /// - Tên đăng nhập: sinh từ mã nhân viên (chữ thường, không dấu)
        /// - Mật khẩu khởi tạo: cố định Demo@123, băm SHA-256
        /// - Vai trò: cố định Nhân viên (MaVaiTro = 2)
        /// - Trạng thái nhân viên: mặc định DangHoatDong
        /// - Trạng thái tài khoản: mặc định HoatDong
        /// </summary>
        public KetQuaThaoTacDTO ThemNhanVien(NhanVienQuanLyDTO nv, string matKhau = "Demo@123")
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng quản lý nhân sự chỉ dành cho Quản lý hệ thống.");
            }

            if (string.IsNullOrWhiteSpace(nv.HoTen))
            {
                return KetQuaThaoTacDTO.ThatBai("Họ tên nhân viên không được để trống.");
            }

            // Định dạng số điện thoại nếu người dùng có nhập
            if (!string.IsNullOrWhiteSpace(nv.SoDienThoai) && !ValidationHelper.KiemTraSoDienThoai(nv.SoDienThoai))
            {
                return KetQuaThaoTacDTO.ThatBai("Số điện thoại không hợp lệ (cần đúng 10 chữ số).");
            }

            // Sinh mã nhân viên và tên đăng nhập
            var (maNV, maNVFormatted, tenDangNhapTuDong) = SinhMaVaTenDangNhapTiepTheo();

            // Nếu giao diện không truyền hoặc tên đăng nhập để trống, gán bằng tenDangNhapTuDong
            if (string.IsNullOrWhiteSpace(nv.TenDangNhap))
            {
                nv.TenDangNhap = tenDangNhapTuDong;
            }
            else
            {
                nv.TenDangNhap = nv.TenDangNhap.Trim().ToLowerInvariant();
            }

            // Kiểm tra trùng lặp trên cơ sở dữ liệu
            if (_dal.KiemTraTrungTenDangNhap(nv.TenDangNhap))
            {
                return KetQuaThaoTacDTO.ThatBai($"Tên đăng nhập '{nv.TenDangNhap}' đã tồn tại trong hệ thống. Vui lòng thử lại.");
            }

            if (string.IsNullOrWhiteSpace(matKhau) || matKhau.Length < 6)
            {
                matKhau = "Demo@123";
            }

            // Ràng buộc cố định trạng thái & vai trò theo đúng quy định
            nv.TrangThaiNhanVien = "DangHoatDong";
            nv.TrangThaiTaiKhoan = "HoatDong";
            nv.MaVaiTro = 2; // Cố định vai trò Nhân viên (MaVaiTro = 2)
            nv.NgayVaoLam = DateTime.Today;
            nv.Email = null; // Loại bỏ Email do không sử dụng trong bảng NhanVien theo thiết kế

            bool kq = _dal.ThemNhanVienVaTaiKhoan(nv, matKhau);
            return kq
                ? KetQuaThaoTacDTO.ThanhCongKq($"Thêm mới nhân viên '{nv.HoTen}' thành công! (Mã NV: NV{nv.MaNhanVien:D4}, Tên đăng nhập: {nv.TenDangNhap})")
                : KetQuaThaoTacDTO.ThatBai("Không thể tạo nhân viên.");
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
                return KetQuaThaoTacDTO.ThatBai("Số điện thoại không hợp lệ (cần đúng 10 chữ số).");
            }

            // Tuyệt đối không cho đổi vai trò thành Quản lý thông qua cập nhật
            nv.MaVaiTro = 2;

            bool kq = _dal.CapNhatNhanVien(nv);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Cập nhật thông tin nhân viên thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể cập nhật nhân viên.");
        }

        /// <summary>
        /// Khóa hoặc Mở khóa tài khoản nhân viên với cơ chế bảo vệ phân quyền ở tầng nghiệp vụ:
        /// 1. Chỉ Quản lý mới được thực hiện.
        /// 2. Không được khóa tài khoản của chính mình.
        /// 3. Không được khóa/mở khóa tài khoản của Quản lý khác (kiểm tra vai trò trực tiếp từ CSDL).
        /// </summary>
        public KetQuaThaoTacDTO KhoaMoKhoaTaiKhoan(int maTaiKhoan, bool muonKhoa)
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng khóa/mở khóa tài khoản chỉ dành cho Quản lý hệ thống.");
            }

            // 1. Kiểm tra không được tự khóa chính mình
            if (Session.DaDangNhap && maTaiKhoan == Session.MaTaiKhoan)
            {
                return KetQuaThaoTacDTO.ThatBai("Bạn không thể tự khóa tài khoản đang đăng nhập của chính mình.");
            }

            // 2. Kiểm tra vai trò thực tế từ CSDL để chống bypass UI
            var infoTK = _dal.LayThongTinTaiKhoan(maTaiKhoan);
            if (!infoTK.HasValue)
            {
                return KetQuaThaoTacDTO.ThatBai("Không tìm thấy thông tin tài khoản cần xử lý trong hệ thống.");
            }

            if (!string.Equals(infoTK.Value.TenVaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase))
            {
                return KetQuaThaoTacDTO.ThatBai("Từ chối thao tác: Bạn chỉ có quyền khóa hoặc mở khóa tài khoản của nhân viên, không thể can thiệp tài khoản Quản lý.");
            }

            string trangThaiMoi = muonKhoa ? "BiKhoa" : "HoatDong";
            bool kq = _dal.ThayDoiTrangThaiTaiKhoan(maTaiKhoan, trangThaiMoi);
            return kq
                ? KetQuaThaoTacDTO.ThanhCongKq($"{(muonKhoa ? "Khóa" : "Mở khóa")} tài khoản thành công!")
                : KetQuaThaoTacDTO.ThatBai("Không thể cập nhật trạng thái tài khoản.");
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

            // Kiểm tra bảo vệ không cấp lại MK cho Quản lý khác
            var infoTK = _dal.LayThongTinTaiKhoan(maTaiKhoan);
            if (infoTK.HasValue && !string.Equals(infoTK.Value.TenVaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase))
            {
                return KetQuaThaoTacDTO.ThatBai("Từ chối thao tác: Chỉ được cấp lại mật khẩu cho tài khoản Nhân viên.");
            }
            string hashMoi = PasswordHelper.BamMatKhau(matKhauMoi);
            bool kq = _dal.CapLaiMatKhau(maTaiKhoan, hashMoi);
            return kq
                ? KetQuaThaoTacDTO.ThanhCongKq($"Đã cấp lại mật khẩu thành công! Mật khẩu mới được thiết lập về: {matKhauMoi}")
                : KetQuaThaoTacDTO.ThatBai("Không thể cấp lại mật khẩu.");
        }
    }
}
