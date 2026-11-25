using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class UCTongQuan : UserControl
    {
        private Panel pnlBanner = null!;
        private Label lblLoiChao = null!;
        private Label lblThoiGian = null!;
        private Label lblVaiTroBadge = null!;

        private FlowLayoutPanel flpStats = null!;
        private Panel pnlChiTietTrai = null!;
        private Panel pnlChiTietPhai = null!;

        public event Action<string>? OnChuyenChucNang;

        public UCTongQuan()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.TaoFont(9.5f);
            this.AutoScroll = true;

            // ==========================================
            // 1. Banner chao mung
            // ==========================================
            pnlBanner = new Panel
            {
                Location = new Point(30, 24),
                Size = new Size(1000, 85),
                BackColor = ThemeHelper.MauNenNoiDung,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlBanner.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBanner.Width - 1, pnlBanner.Height - 1);
            };

            lblLoiChao = new Label
            {
                Text = $"Xin chào, {Session.HoTen}!",
                Font = ThemeHelper.TaoFont(16f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 18),
                AutoSize = true
            };

            lblThoiGian = new Label
            {
                Text = $"Hôm nay là {DateTime.Now:dddd, dd/MM/yyyy} • Hệ thống Quản lý hóa đơn sẵn sàng làm việc.",
                Font = ThemeHelper.TaoFont(9.5f),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(26, 50),
                AutoSize = true
            };

            lblVaiTroBadge = new Label
            {
                Text = Session.LaQuanLy ? "VAI TRÒ: QUẢN LÝ" : "VAI TRÒ: NHÂN VIÊN",
                Font = ThemeHelper.TaoFont(8.5f, FontStyle.Bold),
                ForeColor = Session.LaQuanLy ? Color.FromArgb(37, 99, 235) : Color.FromArgb(22, 163, 74),
                BackColor = Session.LaQuanLy ? Color.FromArgb(239, 246, 255) : Color.FromArgb(240, 253, 244),
                Padding = new Padding(10, 5, 10, 5),
                AutoSize = true,
                Location = new Point(830, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            pnlBanner.Controls.Add(lblLoiChao);
            pnlBanner.Controls.Add(lblThoiGian);
            pnlBanner.Controls.Add(lblVaiTroBadge);

            // ==========================================
            // 2. Thẻ KPI Thống kê
            // ==========================================
            flpStats = new FlowLayoutPanel
            {
                Location = new Point(30, 125),
                Size = new Size(1000, 115),
                BackColor = Color.Transparent,
                WrapContents = false,
                AutoScroll = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            flpStats.Controls.Add(TaoCardThongKe("Doanh thu mẫu", "18.500.000 đ", "💰", ThemeHelper.MauChinh));
            flpStats.Controls.Add(TaoCardThongKe("Tổng hóa đơn", "18 đơn", "🧾", ThemeHelper.MauThanhCong));
            flpStats.Controls.Add(TaoCardThongKe("Khách hàng", "12 đối tác", "👥", Color.FromArgb(217, 119, 6)));
            flpStats.Controls.Add(TaoCardThongKe("Sản phẩm", "22 mặt hàng", "📦", Color.FromArgb(124, 58, 237)));

            // ==========================================
            // 3. Hai panel chi tiet ben duoi
            // ==========================================
            pnlChiTietTrai = new Panel
            {
                Location = new Point(30, 260),
                Size = new Size(580, 360),
                BackColor = ThemeHelper.MauNenNoiDung,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlChiTietTrai.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlChiTietTrai.Width - 1, pnlChiTietTrai.Height - 1);
            };

            var lblTieuDeTrai = new Label
            {
                Text = "Trạng thái vận hành & Kết nối CSDL",
                Font = ThemeHelper.TaoFont(12f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 20),
                AutoSize = true
            };

            var lblNoiDungTrai = new Label
            {
                Text = $"• Cơ sở dữ liệu: Microsoft SQL Server (QuanLyHoaDon)\n" +
                       $"• Máy chủ kết nối: (localdb)\\MSSQLLocalDB (Đã kết nối)\n" +
                       $"• Tài khoản đăng nhập: {Session.TenDangNhap} (Mã: #{Session.MaTaiKhoan})\n" +
                       $"• Nhân viên: {Session.HoTen} (Mã: #{Session.MaNhanVien})\n" +
                       $"• Quyền hạn: {Session.TenVaiTro}\n" +
                       $"• Nền tảng: Windows Forms / .NET 10 LTS (Visual Studio 2026)\n" +
                       $"• Kiến trúc: 3-Layer (GUI ➔ BLL ➔ DAL ➔ DTO)\n\n" +
                       $"Tất cả các dịch vụ xác thực và phân quyền đang hoạt động bình thường.",
                Font = ThemeHelper.TaoFont(9.5f),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(24, 60),
                Size = new Size(530, 260)
            };

            pnlChiTietTrai.Controls.Add(lblTieuDeTrai);
            pnlChiTietTrai.Controls.Add(lblNoiDungTrai);

            // Panel phai: Loi tat
            pnlChiTietPhai = new Panel
            {
                Location = new Point(630, 260),
                Size = new Size(400, 360),
                BackColor = ThemeHelper.MauNenNoiDung,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            pnlChiTietPhai.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlChiTietPhai.Width - 1, pnlChiTietPhai.Height - 1);
            };

            var lblTieuDePhai = new Label
            {
                Text = "Lối tắt thao tác nhanh",
                Font = ThemeHelper.TaoFont(12f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 20),
                AutoSize = true
            };

            pnlChiTietPhai.Controls.Add(lblTieuDePhai);

            var btnLoiTatHoaDon = TaoNutLoiTat("🧾 Lập và tra cứu Hóa đơn", 60, () => OnChuyenChucNang?.Invoke("HoaDon"));
            var btnLoiTatKhachHang = TaoNutLoiTat("👥 Quản lý Khách hàng", 120, () => OnChuyenChucNang?.Invoke("KhachHang"));
            var btnLoiTatSanPham = TaoNutLoiTat("📦 Quản lý Sản phẩm", 180, () => OnChuyenChucNang?.Invoke("SanPham"));
            var btnLoiTatHoSo = TaoNutLoiTat("👤 Xem Hồ sơ cá nhân", 240, () => OnChuyenChucNang?.Invoke("HoSoCaNhan"));

            pnlChiTietPhai.Controls.Add(btnLoiTatHoaDon);
            pnlChiTietPhai.Controls.Add(btnLoiTatKhachHang);
            pnlChiTietPhai.Controls.Add(btnLoiTatSanPham);
            pnlChiTietPhai.Controls.Add(btnLoiTatHoSo);

            this.Controls.Add(pnlBanner);
            this.Controls.Add(flpStats);
            this.Controls.Add(pnlChiTietTrai);
            this.Controls.Add(pnlChiTietPhai);
        }

        private Panel TaoCardThongKe(string tieuDe, string giaTri, string icon, Color mauChuDao)
        {
            var pnl = new Panel
            {
                Size = new Size(235, 100),
                BackColor = ThemeHelper.MauNenNoiDung,
                Margin = new Padding(0, 0, 16, 0)
            };
            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 20f),
                Size = new Size(45, 45),
                Location = new Point(16, 26),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblT = new Label
            {
                Text = tieuDe,
                Font = ThemeHelper.TaoFont(9f),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(70, 22),
                AutoSize = true
            };

            var lblG = new Label
            {
                Text = giaTri,
                Font = ThemeHelper.TaoFont(14f, FontStyle.Bold),
                ForeColor = mauChuDao,
                Location = new Point(68, 46),
                AutoSize = true
            };

            pnl.Controls.Add(lblIcon);
            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblG);

            return pnl;
        }

        private Button TaoNutLoiTat(string text, int top, Action action)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(24, top),
                Size = new Size(350, 44),
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.TaoFont(9.5f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = ThemeHelper.MauDuongVien;
            btn.FlatAppearance.BorderSize = 1;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(239, 246, 255);
                btn.ForeColor = ThemeHelper.MauChinh;
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(248, 250, 252);
                btn.ForeColor = ThemeHelper.MauChuChinh;
            };
            btn.Click += (s, e) => action();

            return btn;
        }
    }
}
