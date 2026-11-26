using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class UCThongTinCaNhan : UserControl
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;

        // Container chính
        private Panel pnlContainer = null!;
        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;

        // Các trường nhập liệu được phép sửa
        private RoundedTextBox txtSoDienThoai = null!;
        private RoundedTextBox txtEmail = null!;
        private RoundedTextBox txtDiaChi = null!;

        // Nút Lưu thay đổi
        private RoundedButton btnLuuThayDoi = null!;

        public UCThongTinCaNhan()
        {
            _taiKhoanBLL = new TaiKhoanBLL();
            KhoiTaoGiaoDien();
            NapDuLieuTuSession();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            this.Font = ThemeHelper.FontNoiDung;
            this.AutoScroll = true;

            pnlContainer = new Panel
            {
                Location = new Point(24, 20),
                Size = new Size(960, 720),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Banner thông báo kết quả
            pnlThongBao = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(960, 48),
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlThongBao.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlThongBao.Width - 1, pnlThongBao.Height - 1);
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontNoiDungDam,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 14, 0)
            };
            pnlThongBao.Controls.Add(lblThongBao);
            pnlContainer.Controls.Add(pnlThongBao);

            // ====================================================
            // 1. Nhóm 1: THÔNG TIN HỆ THỐNG (CHỈ XEM - HOÀN TOÀN KHÔNG CHO NHẬP/XÓA)
            // ====================================================
            var pnlNhomChiXem = TaoCardNhom(
                "🔒  THÔNG TIN HỆ THỐNG (CHỈ XEM)",
                "Thông tin định danh và phân quyền do Quản trị viên cấp phát, không thể tự ý sửa đổi.",
                new Point(0, 56),
                new Size(960, 240));

            ThemBadgeChiXem(pnlNhomChiXem, "MÃ NHÂN VIÊN", $"NV{Session.MaNhanVien:D4}", 75, 24, 440);
            ThemBadgeChiXem(pnlNhomChiXem, "HỌ VÀ TÊN", Session.HoTen, 75, 488, 440);

            ThemBadgeChiXem(pnlNhomChiXem, "TÊN ĐĂNG NHẬP", Session.TenDangNhap, 148, 24, 440);
            ThemBadgeChiXem(pnlNhomChiXem, "VAI TRÒ / CHỨC VỤ", Session.TenVaiTro, 148, 488, 440);

            string ngayVaoLamStr = Session.NgayVaoLam.HasValue && Session.NgayVaoLam.Value > DateTime.MinValue
                ? Session.NgayVaoLam.Value.ToString("dd/MM/yyyy")
                : "Chưa cập nhật";
            ThemBadgeChiXem(pnlNhomChiXem, "NGÀY VÀO LÀM", ngayVaoLamStr, 221, 24, 440);
            ThemBadgeChiXem(pnlNhomChiXem, "TRẠNG THÁI NHÂN SỰ", Session.TrangThaiNhanVien, 221, 488, 440);

            pnlNhomChiXem.Height = 295;
            pnlContainer.Controls.Add(pnlNhomChiXem);

            // ====================================================
            // 2. Nhóm 2: THÔNG TIN LIÊN HỆ (ĐƯỢC PHÉP CHỈNH SỬA)
            // ====================================================
            var pnlNhomChinhSua = TaoCardNhom(
                "✏  THÔNG TIN LIÊN HỆ (ĐƯỢC PHÉP CHỈNH SỬA)",
                "Cập nhật số điện thoại, email và địa chỉ liên hệ của bạn khi có sự thay đổi.",
                new Point(0, 365),
                new Size(960, 275));

            // SĐT
            var lblSdt = new Label
            {
                Text = "Số điện thoại:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 75),
                AutoSize = true
            };
            txtSoDienThoai = new RoundedTextBox
            {
                Location = new Point(24, 102),
                Size = new Size(440, 44),
                BorderRadius = 8,
                PlaceholderText = "Nhập số điện thoại liên hệ"
            };
            pnlNhomChinhSua.Controls.Add(lblSdt);
            pnlNhomChinhSua.Controls.Add(txtSoDienThoai);

            // Email
            var lblEmail = new Label
            {
                Text = "Email liên hệ:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(488, 75),
                AutoSize = true
            };
            txtEmail = new RoundedTextBox
            {
                Location = new Point(488, 102),
                Size = new Size(440, 44),
                BorderRadius = 8,
                PlaceholderText = "Nhập địa chỉ email liên hệ"
            };
            pnlNhomChinhSua.Controls.Add(lblEmail);
            pnlNhomChinhSua.Controls.Add(txtEmail);

            // Địa chỉ
            var lblDiaChi = new Label
            {
                Text = "Địa chỉ liên hệ:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 158),
                AutoSize = true
            };
            txtDiaChi = new RoundedTextBox
            {
                Location = new Point(24, 185),
                Size = new Size(904, 44),
                BorderRadius = 8,
                PlaceholderText = "Nhập số nhà, tên đường, phường/xã, quận/huyện, tỉnh/thành phố"
            };
            pnlNhomChinhSua.Controls.Add(lblDiaChi);
            pnlNhomChinhSua.Controls.Add(txtDiaChi);

            pnlNhomChinhSua.Height = 250;
            pnlContainer.Controls.Add(pnlNhomChinhSua);

            // ====================================================
            // 3. Thanh thao tác Lưu thay đổi
            // ====================================================
            btnLuuThayDoi = new RoundedButton
            {
                Text = "💾  Lưu thay đổi",
                Location = new Point(0, 630),
                Size = new Size(180, 44),
                BorderRadius = 8,
                NormalColor = ThemeHelper.MauChinh,
                HoverColor = ColorTranslator.FromHtml("#1D4ED8"),
                PressedColor = ColorTranslator.FromHtml("#1E40AF"),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold)
            };
            btnLuuThayDoi.Click += BtnLuuThayDoi_Click;
            pnlContainer.Controls.Add(btnLuuThayDoi);

            var lblGhiChuLuu = new Label
            {
                Text = "Dữ liệu sau khi lưu sẽ được cập nhật trực tiếp vào cơ sở dữ liệu và đồng bộ vào phiên làm việc hiện tại.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(196, 642),
                AutoSize = true
            };
            pnlContainer.Controls.Add(lblGhiChuLuu);

            this.Controls.Add(pnlContainer);
        }

        private Panel TaoCardNhom(string tieuDe, string moTa, Point viTri, Size kichThuoc)
        {
            var pnl = new Panel
            {
                Location = viTri,
                Size = kichThuoc,
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1.2f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            var lblT = new Label
            {
                Text = tieuDe,
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(22, 16),
                AutoSize = true
            };

            var lblM = new Label
            {
                Text = moTa,
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(24, 42),
                AutoSize = true
            };

            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblM);
            return pnl;
        }

        /// <summary>
        /// Tạo vùng hiển thị dữ liệu tĩnh dạng 2 tầng hoàn toàn bằng Panel + Label.
        /// Triệt tiêu 100% khả năng nhập, xóa, click focus hoặc ấn phím space/text của người dùng.
        /// </summary>
        private void ThemBadgeChiXem(Panel parent, string labelText, string valueText, int top, int left, int width)
        {
            var pnlBadge = new Panel
            {
                Location = new Point(left, top),
                Size = new Size(width, 58),
                BackColor = ColorTranslator.FromHtml("#F1F5F9"),
                Padding = new Padding(12, 6, 12, 6)
            };
            pnlBadge.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBadge.Width - 1, pnlBadge.Height - 1);
            };

            var lblLabel = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#64748B"),
                Location = new Point(12, 8),
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = valueText,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#1E293B"),
                Location = new Point(12, 28),
                AutoSize = true
            };

            pnlBadge.Controls.Add(lblLabel);
            pnlBadge.Controls.Add(lblValue);
            parent.Controls.Add(pnlBadge);
        }

        private void NapDuLieuTuSession()
        {
            txtSoDienThoai.Text = Session.SoDienThoai ?? string.Empty;
            txtEmail.Text = Session.Email ?? string.Empty;
            txtDiaChi.Text = Session.DiaChi ?? string.Empty;
        }

        private void BtnLuuThayDoi_Click(object? sender, EventArgs e)
        {
            AnThongBao();

            btnLuuThayDoi.Enabled = false;
            try
            {
                var ketQua = _taiKhoanBLL.CapNhatThongTinLienHe(
                    Session.MaNhanVien,
                    txtSoDienThoai.Text,
                    txtEmail.Text,
                    txtDiaChi.Text);

                if (ketQua.ThanhCong)
                {
                    HienThongBao(ketQua.ThongBao, laLoi: false);
                    NapDuLieuTuSession();
                }
                else
                {
                    HienThongBao(ketQua.ThongBao, laLoi: true);
                }
            }
            finally
            {
                btnLuuThayDoi.Enabled = true;
            }
        }

        private void HienThongBao(string thongBao, bool laLoi)
        {
            pnlThongBao.Visible = true;
            pnlThongBao.BackColor = laLoi ? ColorTranslator.FromHtml("#FEF2F2") : ColorTranslator.FromHtml("#F0FDF4");
            lblThongBao.ForeColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.Text = (laLoi ? "⚠  " : "✓  ") + thongBao;
        }

        private void AnThongBao()
        {
            pnlThongBao.Visible = false;
            lblThongBao.Text = string.Empty;
        }
    }
}
