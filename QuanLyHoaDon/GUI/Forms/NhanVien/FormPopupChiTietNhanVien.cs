using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.NhanVien
{
    /// <summary>
    /// Modal Popup chỉ xem thông tin chi tiết nhân viên (Read-only).
    /// Hiển thị đầy đủ 9 trường thông tin: Mã NV, Họ tên, SĐT, Email, Địa chỉ, Trạng thái NV, Tài khoản, Vai trò, Trạng thái TK.
    /// Có nút Đóng. Không có chức năng chỉnh sửa hay cấp lại mật khẩu.
    /// </summary>
    public class FormPopupChiTietNhanVien : Form
    {
        private readonly NhanVienQuanLyDTO _nv;

        public FormPopupChiTietNhanVien(NhanVienQuanLyDTO nv)
        {
            _nv = nv ?? throw new ArgumentNullException(nameof(nv));
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Thông Tin Chi Tiết Nhân Viên";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(540, 580);
            this.BackColor = ThemeHelper.MauNenNoiDung;
            this.Font = ThemeHelper.FontNoiDung;

            // Header Top Panel
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 68,
                BackColor = ThemeHelper.MauNenUngDung,
                Padding = new Padding(24, 14, 24, 12)
            };
            pnlHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            var lblTieuDe = new Label
            {
                Text = "HỒ SƠ NHÂN VIÊN",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 12),
                AutoSize = true
            };

            var lblPhu = new Label
            {
                Text = $"Mã nhân viên: NV{_nv.MaNhanVien:D4}  •  Ngày vào làm: {_nv.NgayVaoLam:dd/MM/yyyy}",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(24, 38),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblPhu);
            this.Controls.Add(pnlHeader);

            // Container nội dung
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 16, 24, 16),
                AutoScroll = true
            };

            int top = 12;
            ThemDongThongTin(pnlBody, "Mã nhân viên:", $"NV{_nv.MaNhanVien:D4}", ref top);
            ThemDongThongTin(pnlBody, "Họ và tên:", _nv.HoTen, ref top, laDam: true);
            ThemDongThongTin(pnlBody, "Số điện thoại:", string.IsNullOrWhiteSpace(_nv.SoDienThoai) ? "(Chưa cập nhật)" : _nv.SoDienThoai, ref top);
            ThemDongThongTin(pnlBody, "Địa chỉ cư trú:", string.IsNullOrWhiteSpace(_nv.DiaChi) ? "(Chưa cập nhật)" : _nv.DiaChi, ref top);

            // Phân cách nhẹ
            var pnlLine = new Panel
            {
                Location = new Point(24, top + 6),
                Size = new Size(pnlBody.Width - 48, 1),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = ThemeHelper.MauDuongVien
            };
            pnlBody.Controls.Add(pnlLine);
            top += 18;

            ThemDongThongTin(pnlBody, "Trạng thái nhân sự:", _nv.TenTrangThaiNhanVien, ref top,
                mauSac: _nv.TrangThaiNhanVien == "DangHoatDong" ? ThemeHelper.MauThanhCong : ThemeHelper.MauLoi);

            ThemDongThongTin(pnlBody, "Tên tài khoản:", string.IsNullOrWhiteSpace(_nv.TenDangNhap) ? "(Chưa có tài khoản)" : _nv.TenDangNhap, ref top, laDam: true);
            ThemDongThongTin(pnlBody, "Vai trò tài khoản:", string.IsNullOrWhiteSpace(_nv.TenVaiTro) ? "(Chưa phân vai trò)" : _nv.TenVaiTro, ref top);
            ThemDongThongTin(pnlBody, "Trạng thái tài khoản:", _nv.TenTrangThaiTaiKhoan, ref top,
                mauSac: _nv.TrangThaiTaiKhoan == "HoatDong" ? ThemeHelper.MauThanhCong : (_nv.TrangThaiTaiKhoan == "BiKhoa" ? ThemeHelper.MauLoi : ThemeHelper.MauChuPhu));

            this.Controls.Add(pnlBody);

            // Bottom Actions Panel
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = ThemeHelper.MauNenUngDung,
                Padding = new Padding(24, 10, 24, 10)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawLine(pen, 0, 0, pnlFooter.Width, 0);
            };

            var btnDong = new RoundedButton
            {
                Text = "Đóng",
                Location = new Point(pnlFooter.Width - 110 - 24, 11),
                Size = new Size(110, ThemeHelper.ChieuCaoButtonChuan),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                NormalColor = ThemeHelper.MauNutPhu,
                HoverColor = ThemeHelper.MauNutPhuHover,
                BorderColor = ThemeHelper.MauVienNutPhu,
                BorderWidth = 1,
                TextColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = ThemeHelper.RadiusMedium
            };
            btnDong.Click += (s, e) => this.Close();

            pnlFooter.Controls.Add(btnDong);
            this.Controls.Add(pnlFooter);
        }

        private static void ThemDongThongTin(Panel parent, string label, string value, ref int top, bool laDam = false, Color? mauSac = null)
        {
            var lblNhan = new Label
            {
                Text = label,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(24, top),
                Size = new Size(160, 24),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblGiaTri = new Label
            {
                Text = value,
                Font = laDam ? ThemeHelper.FontNoiDungDam : ThemeHelper.FontNoiDung,
                ForeColor = mauSac ?? ThemeHelper.MauChuChinh,
                Location = new Point(190, top),
                Size = new Size(parent.Width - 214, 24),
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoEllipsis = true
            };

            parent.Controls.Add(lblNhan);
            parent.Controls.Add(lblGiaTri);
            top += 32;
        }
    }
}
