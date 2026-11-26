using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class UCDoiMatKhau : UserControl
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;

        private Panel pnlCard = null!;
        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;

        private RoundedTextBox txtMatKhauHienTai = null!;
        private Label lblLoiMatKhauHienTai = null!;

        private RoundedTextBox txtMatKhauMoi = null!;
        private Label lblLoiMatKhauMoi = null!;

        private RoundedTextBox txtXacNhanMatKhau = null!;
        private Label lblLoiXacNhan = null!;

        private RoundedButton btnLuuThayDoi = null!;
        private RoundedButton btnQuayLai = null!;

        public event Action? OnQuayLai;

        public UCDoiMatKhau()
        {
            _taiKhoanBLL = new TaiKhoanBLL();
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            this.Font = ThemeHelper.FontNoiDung;
            this.AutoScroll = true;

            pnlCard = new Panel
            {
                Location = new Point(24, 20),
                Size = new Size(760, 560),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            pnlCard.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1.2f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            };

            // Tiêu đề & mô tả gọn gàng bên trong card (không dùng banner đen to đè lên Topbar)
            var lblTieuDeCard = new Label
            {
                Text = "🔑  CẬP NHẬT MẬT KHẨU TÀI KHOẢN",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(28, 24),
                AutoSize = true
            };

            var lblMoTaCard = new Label
            {
                Text = "Dành cho tài khoản người dùng để định kỳ cập nhật mật khẩu, nâng cao bảo mật hệ thống.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(30, 52),
                AutoSize = true
            };

            pnlCard.Controls.Add(lblTieuDeCard);
            pnlCard.Controls.Add(lblMoTaCard);

            // Banner thông báo kết quả
            pnlThongBao = new Panel
            {
                Location = new Point(28, 82),
                Size = new Size(700, 44),
                Visible = false
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
                Padding = new Padding(12, 0, 12, 0)
            };
            pnlThongBao.Controls.Add(lblThongBao);
            pnlCard.Controls.Add(pnlThongBao);

            int top = 140;

            // 1. Mật khẩu hiện tại
            var lblHienTai = new Label
            {
                Text = "Mật khẩu hiện tại *:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(28, top),
                AutoSize = true
            };
            txtMatKhauHienTai = new RoundedTextBox
            {
                Location = new Point(28, top + 24),
                Size = new Size(540, 44),
                BorderRadius = 8,
                IconType = InputIconType.Lock,
                IsPassword = true,
                PlaceholderText = "Nhập mật khẩu đang sử dụng"
            };
            lblLoiMatKhauHienTai = new Label
            {
                Location = new Point(30, top + 70),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };
            pnlCard.Controls.Add(lblHienTai);
            pnlCard.Controls.Add(txtMatKhauHienTai);
            pnlCard.Controls.Add(lblLoiMatKhauHienTai);

            top += 90;

            // 2. Mật khẩu mới
            var lblMoi = new Label
            {
                Text = "Mật khẩu mới (Tối thiểu 6 ký tự, không trùng mật khẩu cũ) *:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(28, top),
                AutoSize = true
            };
            txtMatKhauMoi = new RoundedTextBox
            {
                Location = new Point(28, top + 24),
                Size = new Size(540, 44),
                BorderRadius = 8,
                IconType = InputIconType.Lock,
                IsPassword = true,
                PlaceholderText = "Nhập mật khẩu mới"
            };
            lblLoiMatKhauMoi = new Label
            {
                Location = new Point(30, top + 70),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };
            pnlCard.Controls.Add(lblMoi);
            pnlCard.Controls.Add(txtMatKhauMoi);
            pnlCard.Controls.Add(lblLoiMatKhauMoi);

            top += 90;

            // 3. Xác nhận mật khẩu mới
            var lblXacNhan = new Label
            {
                Text = "Xác nhận mật khẩu mới *:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(28, top),
                AutoSize = true
            };
            txtXacNhanMatKhau = new RoundedTextBox
            {
                Location = new Point(28, top + 24),
                Size = new Size(540, 44),
                BorderRadius = 8,
                IconType = InputIconType.Lock,
                IsPassword = true,
                PlaceholderText = "Nhập lại chính xác mật khẩu mới"
            };
            lblLoiXacNhan = new Label
            {
                Location = new Point(30, top + 70),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };
            pnlCard.Controls.Add(lblXacNhan);
            pnlCard.Controls.Add(txtXacNhanMatKhau);
            pnlCard.Controls.Add(lblLoiXacNhan);

            top += 95;

            // Buttons
            btnLuuThayDoi = new RoundedButton
            {
                Text = "💾  Lưu mật khẩu mới",
                Location = new Point(28, top),
                Size = new Size(210, 44),
                BorderRadius = 8,
                NormalColor = ThemeHelper.MauChinh,
                HoverColor = ColorTranslator.FromHtml("#1D4ED8"),
                PressedColor = ColorTranslator.FromHtml("#1E40AF"),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold)
            };
            btnLuuThayDoi.Click += BtnLuuThayDoi_Click;

            btnQuayLai = new RoundedButton
            {
                Text = "Quay lại",
                Location = new Point(252, top),
                Size = new Size(130, 44),
                BorderRadius = 8,
                NormalColor = ColorTranslator.FromHtml("#F1F5F9"),
                HoverColor = ColorTranslator.FromHtml("#E2E8F0"),
                PressedColor = ColorTranslator.FromHtml("#CBD5E1"),
                TextColor = ThemeHelper.MauChuChinh,
                BorderColor = ColorTranslator.FromHtml("#CBD5E1"),
                BorderWidth = 1,
                Font = new Font("Segoe UI", 10f, FontStyle.Regular)
            };
            btnQuayLai.Click += (s, e) => OnQuayLai?.Invoke();

            pnlCard.Controls.Add(btnLuuThayDoi);
            pnlCard.Controls.Add(btnQuayLai);

            this.Controls.Add(pnlCard);
        }

        private void XoaTatCaLoi()
        {
            pnlThongBao.Visible = false;
            lblThongBao.Text = string.Empty;

            lblLoiMatKhauHienTai.Visible = false;
            lblLoiMatKhauHienTai.Text = string.Empty;

            lblLoiMatKhauMoi.Visible = false;
            lblLoiMatKhauMoi.Text = string.Empty;

            lblLoiXacNhan.Visible = false;
            lblLoiXacNhan.Text = string.Empty;
        }

        private void BtnLuuThayDoi_Click(object? sender, EventArgs e)
        {
            XoaTatCaLoi();

            btnLuuThayDoi.Enabled = false;
            try
            {
                var ketQua = _taiKhoanBLL.DoiMatKhau(
                    Session.MaTaiKhoan,
                    txtMatKhauHienTai.Text,
                    txtMatKhauMoi.Text,
                    txtXacNhanMatKhau.Text);

                if (!ketQua.ThanhCong)
                {
                    if (!string.IsNullOrEmpty(ketQua.LoiMatKhauHienTai))
                    {
                        lblLoiMatKhauHienTai.Text = "⚠  " + ketQua.LoiMatKhauHienTai;
                        lblLoiMatKhauHienTai.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(ketQua.LoiMatKhauMoi))
                    {
                        lblLoiMatKhauMoi.Text = "⚠  " + ketQua.LoiMatKhauMoi;
                        lblLoiMatKhauMoi.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(ketQua.LoiXacNhanMatKhau))
                    {
                        lblLoiXacNhan.Text = "⚠  " + ketQua.LoiXacNhanMatKhau;
                        lblLoiXacNhan.Visible = true;
                    }

                    if (!ketQua.CoLoi && !string.IsNullOrEmpty(ketQua.ThongBaoChung))
                    {
                        HienThongBao(ketQua.ThongBaoChung, laLoi: true);
                    }
                    return;
                }

                // Cập nhật thành công
                HienThongBao("Cập nhật mật khẩu thành công! Hãy ghi nhớ mật khẩu mới cho các phiên làm việc tiếp theo.", laLoi: false);
                txtMatKhauHienTai.Clear();
                txtMatKhauMoi.Clear();
                txtXacNhanMatKhau.Clear();
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
    }
}
