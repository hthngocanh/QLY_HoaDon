using System;
using System.Collections.Generic;
using QuanLyHoaDon.DAL.HeThong;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL.HeThong
{
    public class YeuCauCapLaiMatKhauBLL
    {
        private readonly YeuCauCapLaiMatKhauDAL _yeuCauDAL;
        private readonly DangNhapDAL _dangNhapDAL;

        public YeuCauCapLaiMatKhauBLL()
        {
            _yeuCauDAL = new YeuCauCapLaiMatKhauDAL();
            _dangNhapDAL = new DangNhapDAL();
        }

        public YeuCauCapLaiMatKhauBLL(YeuCauCapLaiMatKhauDAL yeuCauDAL, DangNhapDAL dangNhapDAL)
        {
            _yeuCauDAL = yeuCauDAL;
            _dangNhapDAL = dangNhapDAL;
        }

        public List<YeuCauCapLaiMatKhauDTO> LayDanhSach(string? trangThai = null, string? tuKhoa = null)
        {
            return _yeuCauDAL.LayDanhSach(trangThai, tuKhoa);
        }

        public YeuCauCapLaiMatKhauDTO? LayTheoMa(int maYeuCau)
        {
            return _yeuCauDAL.LayTheoMa(maYeuCau);
        }

        public YeuCauCapLaiMatKhauDTO? LayYeuCauMoiNhat(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap)) return null;
            return _yeuCauDAL.LayYeuCauMoiNhatCuaTaiKhoan(tenDangNhap.Trim());
        }

        /// <summary>
        /// Nhân viên gửi yêu cầu cấp lại mật khẩu từ Form đăng nhập
        /// Tương thích ngược: tự động gọi overload đối chiếu nếu có dữ liệu
        /// </summary>
        public KetQuaThaoTacDTO GuiYeuCau(string tenDangNhap, string? lyDo)
        {
            return GuiYeuCau(tenDangNhap, null, null, null, lyDo, batBuocDoiChieu: false);
        }

        /// <summary>
        /// Nhân viên gửi yêu cầu cấp lại mật khẩu kèm đối chiếu thông tin hồ sơ:
        /// 1. Tên đăng nhập (bắt buộc)
        /// 2. Mã nhân viên (bắt buộc)
        /// 3. Họ và tên (bắt buộc)
        /// 4. Số điện thoại đã đăng ký (bắt buộc)
        /// 5. Lý do (tùy chọn)
        /// </summary>
        public KetQuaThaoTacDTO GuiYeuCau(
            string tenDangNhap,
            string? maNhanVienStr,
            string? hoTen,
            string? soDienThoai,
            string? lyDo,
            bool batBuocDoiChieu = true)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                return KetQuaThaoTacDTO.TaoThatBai("Vui lòng nhập tên đăng nhập.");
            }

            tenDangNhap = tenDangNhap.Trim();

            if (batBuocDoiChieu)
            {
                if (string.IsNullOrWhiteSpace(maNhanVienStr))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Vui lòng nhập mã nhân viên.");
                }

                if (string.IsNullOrWhiteSpace(hoTen))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Vui lòng nhập họ và tên.");
                }

                if (string.IsNullOrWhiteSpace(soDienThoai))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Vui lòng nhập số điện thoại đã đăng ký.");
                }
            }

            try
            {
                // 1. Kiểm tra tài khoản có tồn tại không
                var taiKhoan = _dangNhapDAL.LayThongTinDangNhap(tenDangNhap);
                if (taiKhoan == null)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Tên đăng nhập không tồn tại trên hệ thống.");
                }

                // 2. Đối chiếu thông tin hồ sơ nhân viên nếu bật chế độ đối chiếu
                if (batBuocDoiChieu)
                {
                    // Đối chiếu Mã nhân viên
                    string maNVChuanHoa = maNhanVienStr!.Trim().ToUpperInvariant();
                    string maNVTrongDB = $"NV{taiKhoan.MaNhanVien:D4}";
                    string maNVSoTrongDB = taiKhoan.MaNhanVien.ToString();

                    if (!maNVChuanHoa.Equals(maNVTrongDB, StringComparison.OrdinalIgnoreCase) &&
                        !maNVChuanHoa.Equals(maNVSoTrongDB, StringComparison.OrdinalIgnoreCase))
                    {
                        return KetQuaThaoTacDTO.TaoThatBai("Mã nhân viên không khớp với tài khoản đã cung cấp.");
                    }

                    // Đối chiếu Họ và tên
                    string hoTenNhap = hoTen!.Trim();
                    if (!string.Equals(hoTenNhap, taiKhoan.HoTen?.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        return KetQuaThaoTacDTO.TaoThatBai("Họ và tên không khớp với hồ sơ nhân viên trong hệ thống.");
                    }

                    // Đối chiếu Số điện thoại
                    string sdtNhap = soDienThoai!.Trim();
                    string sdtTrongDB = taiKhoan.SoDienThoai?.Trim() ?? string.Empty;

                    if (string.IsNullOrEmpty(sdtTrongDB) || !string.Equals(sdtNhap, sdtTrongDB, StringComparison.OrdinalIgnoreCase))
                    {
                        return KetQuaThaoTacDTO.TaoThatBai("Số điện thoại không khớp với hồ sơ nhân viên trong hệ thống.");
                    }
                }

                // 3. Kiểm tra tài khoản có đang hoạt động không
                if (!string.Equals(taiKhoan.TrangThai, "HoatDong", StringComparison.OrdinalIgnoreCase))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Tài khoản của bạn hiện đang bị khóa hoặc ngừng hoạt động. Vui lòng liên hệ trực tiếp Quản trị viên.");
                }
                if (!string.Equals(taiKhoan.TrangThaiNhanVien, "DangHoatDong", StringComparison.OrdinalIgnoreCase))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Nhân viên liên kết với tài khoản này đã ngừng hoạt động.");
                }

                // 4. Kiểm tra xem đã có yêu cầu nào đang chờ xử lý chưa (chống gửi trùng)
                var yeuCauDangCho = _yeuCauDAL.LayYeuCauChoDuyetCuaTaiKhoan(taiKhoan.MaTaiKhoan);
                if (yeuCauDangCho != null)
                {
                    return KetQuaThaoTacDTO.TaoThatBai(
                        $"Bạn đã gửi một yêu cầu vào lúc {yeuCauDangCho.ThoiGianYeuCau:HH:mm dd/MM/yyyy} và đang chờ Quản lý phê duyệt. Vui lòng không gửi lặp lại!");
                }

                // 5. Lưu yêu cầu vào Database
                bool ketQua = _yeuCauDAL.TaoYeuCau(taiKhoan.MaTaiKhoan, lyDo);
                if (!ketQua)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Không thể tạo yêu cầu cấp lại mật khẩu. Vui lòng thử lại sau.");
                }

                return KetQuaThaoTacDTO.TaoThanhCong(
                    "Gửi yêu cầu cấp lại mật khẩu thành công! Quản lý hệ thống sẽ xem xét và phê duyệt yêu cầu của bạn.");
            }
            catch (Exception ex)
            {
                return KetQuaThaoTacDTO.TaoThatBai($"Lỗi hệ thống khi gửi yêu cầu: {ex.Message}");
            }
        }

        /// <summary>
        /// Quản lý duyệt yêu cầu
        /// </summary>
        public KetQuaThaoTacDTO DuyetYeuCau(int maYeuCau, int maNguoiDuyet)
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.TaoThatBai("Bạn không có quyền thực hiện thao tác này. Chức năng chỉ dành cho Quản lý.");
            }

            try
            {
                var yc = _yeuCauDAL.LayTheoMa(maYeuCau);
                if (yc == null)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Yêu cầu không tồn tại trên hệ thống.");
                }

                if (yc.TrangThai != "ChoDuyet")
                {
                    return KetQuaThaoTacDTO.TaoThatBai($"Yêu cầu này đã được xử lý trước đó (Trạng thái: {yc.TenHienThiTrangThai}). Không thể duyệt lại.");
                }

                bool ketQua = _yeuCauDAL.DuyetYeuCau(maYeuCau, maNguoiDuyet);
                if (!ketQua)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Thao tác duyệt không thành công hoặc yêu cầu đã được xử lý bởi quản lý khác.");
                }

                return KetQuaThaoTacDTO.TaoThanhCong($"Đã phê duyệt yêu cầu cấp lại mật khẩu cho tài khoản '{yc.TenDangNhap}'. Nhân viên hiện có thể thực hiện đặt lại mật khẩu mới.");
            }
            catch (Exception ex)
            {
                return KetQuaThaoTacDTO.TaoThatBai($"Lỗi hệ thống khi duyệt yêu cầu: {ex.Message}");
            }
        }

        /// <summary>
        /// Quản lý từ chối yêu cầu (bắt buộc có lý do)
        /// </summary>
        public KetQuaThaoTacDTO TuChoiYeuCau(int maYeuCau, int maNguoiDuyet, string lyDoTuChoi)
        {
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.TaoThatBai("Bạn không có quyền thực hiện thao tác này. Chức năng chỉ dành cho Quản lý.");
            }

            if (string.IsNullOrWhiteSpace(lyDoTuChoi))
            {
                return KetQuaThaoTacDTO.TaoThatBai("Vui lòng nhập lý do từ chối yêu cầu.");
            }

            try
            {
                var yc = _yeuCauDAL.LayTheoMa(maYeuCau);
                if (yc == null)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Yêu cầu không tồn tại trên hệ thống.");
                }

                if (yc.TrangThai != "ChoDuyet")
                {
                    return KetQuaThaoTacDTO.TaoThatBai($"Yêu cầu này đã được xử lý trước đó (Trạng thái: {yc.TenHienThiTrangThai}). Không thể từ chối lại.");
                }

                bool ketQua = _yeuCauDAL.TuChoiYeuCau(maYeuCau, maNguoiDuyet, lyDoTuChoi);
                if (!ketQua)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Thao tác từ chối không thành công hoặc yêu cầu đã được xử lý bởi quản lý khác.");
                }

                return KetQuaThaoTacDTO.TaoThanhCong($"Đã từ chối yêu cầu cấp lại mật khẩu của tài khoản '{yc.TenDangNhap}'.");
            }
            catch (Exception ex)
            {
                return KetQuaThaoTacDTO.TaoThatBai($"Lỗi hệ thống khi từ chối yêu cầu: {ex.Message}");
            }
        }

        /// <summary>
        /// Nhân viên thực hiện đặt mật khẩu mới sau khi yêu cầu đã được Quản lý duyệt
        /// </summary>
        public KetQuaThaoTacDTO DatLaiMatKhau(int maYeuCau, string tenDangNhap, string matKhauMoi, string xacNhanMatKhau)
        {
            if (string.IsNullOrWhiteSpace(matKhauMoi))
            {
                return KetQuaThaoTacDTO.TaoThatBai("Vui lòng nhập mật khẩu mới.");
            }

            if (matKhauMoi.Length < 6)
            {
                return KetQuaThaoTacDTO.TaoThatBai("Mật khẩu mới phải có tối thiểu 6 ký tự.");
            }

            if (string.IsNullOrWhiteSpace(xacNhanMatKhau))
            {
                return KetQuaThaoTacDTO.TaoThatBai("Vui lòng xác nhận mật khẩu mới.");
            }

            if (matKhauMoi != xacNhanMatKhau)
            {
                return KetQuaThaoTacDTO.TaoThatBai("Xác nhận mật khẩu mới không trùng khớp.");
            }

            try
            {
                var yc = _yeuCauDAL.LayTheoMa(maYeuCau);
                if (yc == null)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Yêu cầu cấp lại mật khẩu không tồn tại.");
                }

                if (!string.Equals(yc.TenDangNhap, tenDangNhap.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Tài khoản không khớp với yêu cầu được duyệt.");
                }

                if (yc.TrangThai != "DaDuyet")
                {
                    return KetQuaThaoTacDTO.TaoThatBai($"Yêu cầu này hiện ở trạng thái '{yc.TenHienThiTrangThai}'. Chỉ yêu cầu 'Đã duyệt' mới được phép đặt lại mật khẩu.");
                }

                string hashMoi = PasswordHelper.BamMatKhau(matKhauMoi);
                bool ketQua = _yeuCauDAL.HoanTatDatLaiMatKhau(yc.MaYeuCau, yc.MaTaiKhoan, hashMoi);
                if (!ketQua)
                {
                    return KetQuaThaoTacDTO.TaoThatBai("Không thể hoàn tất đặt lại mật khẩu. Vui lòng thử lại sau.");
                }

                return KetQuaThaoTacDTO.TaoThanhCong("Đặt lại mật khẩu thành công! Bây giờ bạn có thể đăng nhập bằng mật khẩu mới.");
            }
            catch (Exception ex)
            {
                return KetQuaThaoTacDTO.TaoThatBai($"Lỗi hệ thống khi đặt lại mật khẩu: {ex.Message}");
            }
        }
    }
}