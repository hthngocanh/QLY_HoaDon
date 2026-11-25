using System.Collections.Generic;
using QuanLyHoaDon.DAL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.BLL
{
    public class HoaDonBLL
    {
        private readonly HoaDonDAL _dal = new();

        public List<HoaDonDTO> LayDanhSach(string? tuKhoa = null)
        {
            // Phân quyền: Nhân viên chỉ xem hóa đơn của mình, Quản lý xem tất cả
            int? maNhanVienLoc = Session.LaQuanLy ? null : Session.MaNhanVien;
            return _dal.LayDanhSach(maNhanVienLoc, tuKhoa);
        }

        public List<ChiTietHoaDonDTO> LayChiTietHoaDon(int maHoaDon)
        {
            return _dal.LayChiTietHoaDon(maHoaDon);
        }

        public KetQuaThaoTacDTO LapHoaDon(int? maKhachHang, string? ghiChu, List<ChiTietHoaDonDTO> chiTietList)
        {
            if (chiTietList == null || chiTietList.Count == 0)
            {
                return KetQuaThaoTacDTO.ThatBai("Hóa đơn phải có ít nhất một mặt hàng sản phẩm.");
            }

            foreach (var ct in chiTietList)
            {
                if (ct.SoLuong <= 0)
                {
                    return KetQuaThaoTacDTO.ThatBai($"Số lượng sản phẩm '{ct.TenSanPham}' phải lớn hơn 0.");
                }
            }

            int maHD = _dal.TaoHoaDon(Session.MaNhanVien, maKhachHang, ghiChu, chiTietList);
            return maHD > 0
                ? KetQuaThaoTacDTO.ThanhCongKq($"Lập hóa đơn #{maHD:D5} thành công!", maHD)
                : KetQuaThaoTacDTO.ThatBai("Không thể lưu hóa đơn.");
        }

        public KetQuaThaoTacDTO XacNhanHoaDon(int maHoaDon)
        {
            bool kq = _dal.CapNhatTrangThaiHoaDon(maHoaDon, "DaXacNhan");
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Xác nhận hoàn tất hóa đơn thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể xác nhận hóa đơn.");
        }

        public KetQuaThaoTacDTO HuyHoaDon(int maHoaDon)
        {
            bool kq = _dal.CapNhatTrangThaiHoaDon(maHoaDon, "DaHuy");
            return kq ? KetQuaThaoTacDTO.ThanhCongKq("Hủy hóa đơn thành công!") : KetQuaThaoTacDTO.ThatBai("Không thể hủy hóa đơn.");
        }
    }
}

