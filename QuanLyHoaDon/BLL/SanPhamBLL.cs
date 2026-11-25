using System.Collections.Generic;
using QuanLyHoaDon.DAL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL
{
    public class SanPhamBLL
    {
        private readonly SanPhamDAL _dal = new();

        public List<SanPhamDTO> LayDanhSach(string? tuKhoa = null)
        {
            return _dal.LayDanhSach(tuKhoa);
        }

        public KetQuaThaoTacDTO ThemSanPham(SanPhamDTO sp)
        {
            // Phân quyền: Quản lý được thêm, Nhân viên chỉ được xem
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng thêm mới sản phẩm chỉ dành cho Quản lý hệ thống.");
            }

            if (string.IsNullOrWhiteSpace(sp.TenSanPham))
            {
                return KetQuaThaoTacDTO.ThatBai("Tên sản phẩm không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(sp.DonViTinh))
            {
                return KetQuaThaoTacDTO.ThatBai("Đơn vị tính không được để trống.");
            }

            if (sp.DonGia < 0)
            {
                return KetQuaThaoTacDTO.ThatBai("Đơn giá sản phẩm không được nhỏ hơn 0.");
            }

            bool kq = _dal.ThemSanPham(sp);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Thêm mới sản phẩm thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể thêm sản phẩm.");
        }

        public KetQuaThaoTacDTO CapNhatGiaVaTrangThai(int maSanPham, decimal donGia, string trangThai)
        {
            // Phân quyền: Quản lý được cập nhật giá và ngừng kinh doanh, Nhân viên chỉ xem
            if (!Session.LaQuanLy)
            {
                return KetQuaThaoTacDTO.ThatBai("Chức năng cập nhật giá và trạng thái kinh doanh chỉ dành cho Quản lý hệ thống.");
            }

            if (maSanPham <= 0)
            {
                return KetQuaThaoTacDTO.ThatBai("Mã sản phẩm không hợp lệ.");
            }

            if (donGia < 0)
            {
                return KetQuaThaoTacDTO.ThatBai("Đơn giá không được nhỏ hơn 0.");
            }

            if (trangThai != "DangKinhDoanh" && trangThai != "NgungKinhDoanh")
            {
                return KetQuaThaoTacDTO.ThatBai("Trạng thái kinh doanh không hợp lệ.");
            }

            bool kq = _dal.CapNhatGiaVaTrangThai(maSanPham, donGia, trangThai);
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Cập nhật thông tin sản phẩm thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể cập nhật sản phẩm.");
        }
    }
}

