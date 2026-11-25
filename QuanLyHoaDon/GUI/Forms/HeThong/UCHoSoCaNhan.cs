using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class UCHoSoCaNhan : UserControl
    {
        private Panel pnlCard = null!;

        public UCHoSoCaNhan()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.TaoFont(9.5f);
            this.AutoScroll = true;

            pnlCard = new Panel
            {
                Location = new Point(30, 24),
                Size = new Size(880, 560),
                BackColor = ThemeHelper.MauNenNoiDung,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlCard.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            };

            // Avatar circle placeholder
            var pnlAvatar = new Panel
            {
                Size = new Size(80, 80),
                Location = new Point(36, 32),
                BackColor = ThemeHelper.MauChinh
            };
            var lblAvatarText = new Label
            {
                Text = LayChuCaiDau(Session.HoTen),
                Font = ThemeHelper.TaoFont(22f, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlAvatar.Controls.Add(lblAvatarText);

            var lblHoTen = new Label
            {
                Text = Session.HoTen,
                Font = ThemeHelper.TaoFont(18f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(135, 34),
                AutoSize = true
            };

            var lblVaiTroBadge = new Label
            {
                Text = Session.LaQuanLy ? "VAI TRÒ: QUẢN LÝ HỆ THỐNG" : "VAI TRÒ: NHÂN VIÊN BÁN HÀNG",
                Font = ThemeHelper.TaoFont(9f, FontStyle.Bold),
                ForeColor = Session.LaQuanLy ? Color.FromArgb(37, 99, 235) : Color.FromArgb(22, 163, 74),
                BackColor = Session.LaQuanLy ? Color.FromArgb(239, 246, 255) : Color.FromArgb(240, 253, 244),
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(135, 75),
                AutoSize = true
            };

            var pnlDivider = new Panel
            {
                Location = new Point(36, 135),
                Size = new Size(808, 1),
                BackColor = ThemeHelper.MauDuongVien,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblTieuDeChiTiet = new Label
            {
                Text = "THÔNG TIN TÀI KHOẢN VÀ NHÂN SỰ",
                Font = ThemeHelper.TaoFont(11f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(36, 155),
                AutoSize = true
            };

            // Bang 2 cot thong tin chi tiet
            ThemDongThongTin(pnlCard, "Tên đăng nhập:", Session.TenDangNhap, 200, 36, 420);
            ThemDongThongTin(pnlCard, "Mã tài khoản:", $"#{Session.MaTaiKhoan}", 200, 460, 420);

            ThemDongThongTin(pnlCard, "Mã nhân viên:", $"NV{Session.MaNhanVien:D4}", 250, 36, 420);
            ThemDongThongTin(pnlCard, "Chức vụ / Vai trò:", Session.TenVaiTro, 250, 460, 420);

            ThemDongThongTin(pnlCard, "Số điện thoại:", string.IsNullOrEmpty(Session.SoDienThoai) ? "Chưa cập nhật" : Session.SoDienThoai, 300, 36, 420);
            ThemDongThongTin(pnlCard, "Email liên hệ:", string.IsNullOrEmpty(Session.Email) ? "Chưa cập nhật" : Session.Email, 300, 460, 420);

            ThemDongThongTin(pnlCard, "Địa chỉ:", string.IsNullOrEmpty(Session.DiaChi) ? "Chưa cập nhật" : Session.DiaChi, 350, 36, 420);
            ThemDongThongTin(pnlCard, "Ngày vào làm:", Session.NgayVaoLam.HasValue && Session.NgayVaoLam.Value > DateTime.MinValue ? Session.NgayVaoLam.Value.ToString("dd/MM/yyyy") : "Chưa xác định", 350, 460, 420);

            ThemDongThongTin(pnlCard, "Trạng thái tài khoản:", Session.TrangThaiTaiKhoan, 400, 36, 420);
            ThemDongThongTin(pnlCard, "Trạng thái nhân sự:", Session.TrangThaiNhanVien, 400, 460, 420);

            var pnlGhiChu = new Panel
            {
                Location = new Point(36, 465),
                Size = new Size(808, 65),
                BackColor = Color.FromArgb(248, 250, 252),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlGhiChu.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlGhiChu.Width - 1, pnlGhiChu.Height - 1);
            };

            var lblGhiChu = new Label
            {
                Text = "🔒 Để bảo mật an toàn cho dữ liệu bán hàng, mọi thay đổi về quyền hạn, mật khẩu hoặc thông tin cá nhân vui lòng liên hệ trực tiếp Quản lý hệ thống.",
                Font = ThemeHelper.TaoFont(9f),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(16, 14),
                Size = new Size(770, 40)
            };
            pnlGhiChu.Controls.Add(lblGhiChu);

            pnlCard.Controls.Add(pnlAvatar);
            pnlCard.Controls.Add(lblHoTen);
            pnlCard.Controls.Add(lblVaiTroBadge);
            pnlCard.Controls.Add(pnlDivider);
            pnlCard.Controls.Add(lblTieuDeChiTiet);
            pnlCard.Controls.Add(pnlGhiChu);

            this.Controls.Add(pnlCard);
        }

        private void ThemDongThongTin(Panel parent, string nhan, string giaTri, int top, int left, int width)
        {
            var pnlDong = new Panel
            {
                Location = new Point(left, top),
                Size = new Size(width, 42),
                BackColor = Color.Transparent
            };

            var lblNhan = new Label
            {
                Text = nhan,
                Font = ThemeHelper.TaoFont(9f),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(0, 0),
                AutoSize = true
            };

            var lblGiaTri = new Label
            {
                Text = giaTri,
                Font = ThemeHelper.TaoFont(10f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(0, 18),
                AutoSize = true
            };

            pnlDong.Controls.Add(lblNhan);
            pnlDong.Controls.Add(lblGiaTri);
            parent.Controls.Add(pnlDong);
        }

        private string LayChuCaiDau(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                return "U";
            }
            var parts = hoTen.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                return parts[0][0].ToString().ToUpper();
            }
            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
        }
    }
}
