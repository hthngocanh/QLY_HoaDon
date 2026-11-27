using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    /// <summary>
    /// UserControl Cập nhật mật khẩu - Thiết kế lại toàn diện theo Modern Desktop Soft UI.
    /// Đặc điểm: 
    /// - Không còn trùng tiêu đề: Topbar của FormChinh hiển thị tiêu đề trang "Cập nhật mật khẩu",
    ///   trong khi Form con UCDoiMatKhau tập trung vào khung hướng dẫn và các trường nhập liệu chuẩn.
    /// - Icon con mắt kích thước 36x36 nằm gọn gàng bên trong vùng Input, phản hồi hover mượt mà và không che chữ.
    /// - Bố cục dọc tinh tế, cân đối, khoảng cách hợp lý, nút "Lưu thay đổi" và "Hủy / Quay lại" nổi bật.
    /// </summary>
    public class UCDoiMatKhau : UserControl
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;

        private TableLayoutPanel pnlMainContainer = null!;
        private RoundedPanel pnlCard = null!;
        private RoundedPanel pnlThongBao = null!;
        private Label lblThongBao = null!;

        private RoundedTextBox txtMatKhauHienTai = null!;
        private Label lblLoiMatKhauHienTai = null!;

        private RoundedTextBox txtMatKhauMoi = null!;
        private Label lblLoiMatKhauMoi = null!;

        private RoundedTextBox txtXacNhanMatKhau = null!;
        private Label lblLoiXacNhan = null!;

        private RoundedButton btnLuuThayDoi = null!;
        private RoundedButton btnHuy = null!;

        public event Action? OnQuayLai;

        public UCDoiMatKhau()
        {
            _taiKhoanBLL = new TaiKhoanBLL();
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.FontNoiDung;
            this.AutoScroll = true;
            this.Padding = new Padding(24, 20, 24, 24);

            pnlMainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            pnlMainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Card chính bao bọc toàn bộ form đổi mật khẩu (chiều rộng 760px, căn chỉnh chuẩn Soft UI)
            pnlCard = new RoundedPanel
            {
                Dock = DockStyle.Top,
                MaximumSize = new Size(760, 0),
                BackColor = ThemeHelper.MauNenNoiDung,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = new Padding(0),
                Padding = new Padding(32, 24, 32, 28),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var tlpCardLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 6,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            tlpCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tlpCardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 0: Khung mô tả / lưu ý
            tlpCardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 1: Banner thông báo kết quả
            tlpCardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 2: Mật khẩu hiện tại
            tlpCardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 3: Mật khẩu mới
            tlpCardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 4: Xác nhận mật khẩu mới
            tlpCardLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 5: Buttons thao tác

            // ==========================================
            // 1. KHUNG HƯỚNG DẪN BẢO MẬT (KHÔNG LẶP LẠI TIÊU ĐỀ)
            // ==========================================
            var pnlHuongDan = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Height = 64,
                BackColor = ThemeHelper.MauChinhNhe,
                BorderColor = ColorTranslator.FromHtml("#BFDBFE"), // Blue-200
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusMedium,
                Margin = new Padding(0, 0, 0, 20),
                Padding = new Padding(16, 12, 16, 12)
            };

            var lblIconInfo = new Label
            {
                Text = "🛡",
                Font = new Font("Segoe UI", 13f, FontStyle.Regular),
                Location = new Point(14, 16),
                Size = new Size(28, 28),
                BackColor = Color.Transparent
            };

            var lblHuongDanText = new Label
            {
                Text = "Để bảo vệ an toàn tài khoản và dữ liệu doanh nghiệp, vui lòng định kỳ thay đổi mật khẩu đăng nhập.\nMật khẩu mới phải có tối thiểu 6 ký tự và khác mật khẩu hiện tại.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChinhHover,
                Location = new Point(48, 12),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            pnlHuongDan.Controls.Add(lblIconInfo);
            pnlHuongDan.Controls.Add(lblHuongDanText);
            tlpCardLayout.Controls.Add(pnlHuongDan, 0, 0);

            // ==========================================
            // 2. BANNER THÔNG BÁO KẾT QUẢ
            // ==========================================
            pnlThongBao = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Height = 44,
                Visible = false,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(14, 0, 14, 0),
                BorderRadius = ThemeHelper.RadiusMedium,
                BorderWidth = 1
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontNoiDungDam,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            pnlThongBao.Controls.Add(lblThongBao);
            tlpCardLayout.Controls.Add(pnlThongBao, 0, 1);

            // ==========================================
            // 3. FIELD: MẬT KHẨU HIỆN TẠI
            // ==========================================
            var pnlHienTai = new Panel { Dock = DockStyle.Fill, AutoSize = true, Margin = new Padding(0, 0, 0, 18) };
            var lblHienTai = new Label
            {
                Text = "Mật khẩu hiện tại *",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(0, 0),
                AutoSize = true
            };
            txtMatKhauHienTai = new RoundedTextBox
            {
                Location = new Point(0, 24),
                Size = new Size(696, 44),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BorderRadius = ThemeHelper.RadiusMedium,
                IconType = InputIconType.Lock,
                IsPassword = true,
                PlaceholderText = "Nhập mật khẩu đang sử dụng"
            };
            lblLoiMatKhauHienTai = new Label
            {
                Location = new Point(2, 72),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };
            pnlHienTai.Controls.Add(lblHienTai);
            pnlHienTai.Controls.Add(txtMatKhauHienTai);
            pnlHienTai.Controls.Add(lblLoiMatKhauHienTai);
            tlpCardLayout.Controls.Add(pnlHienTai, 0, 2);

            // ==========================================
            // 4. FIELD: MẬT KHẨU MỚI
            // ==========================================
            var pnlMoi = new Panel { Dock = DockStyle.Fill, AutoSize = true, Margin = new Padding(0, 0, 0, 18) };
            var lblMoi = new Label
            {
                Text = "Mật khẩu mới (Tối thiểu 6 ký tự) *",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(0, 0),
                AutoSize = true
            };
            txtMatKhauMoi = new RoundedTextBox
            {
                Location = new Point(0, 24),
                Size = new Size(696, 44),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BorderRadius = ThemeHelper.RadiusMedium,
                IconType = InputIconType.Lock,
                IsPassword = true,
                PlaceholderText = "Nhập mật khẩu mới an toàn"
            };
            lblLoiMatKhauMoi = new Label
            {
                Location = new Point(2, 72),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };
            pnlMoi.Controls.Add(lblMoi);
            pnlMoi.Controls.Add(txtMatKhauMoi);
            pnlMoi.Controls.Add(lblLoiMatKhauMoi);
            tlpCardLayout.Controls.Add(pnlMoi, 0, 3);

            // ==========================================
            // 5. FIELD: XÁC NHẬN MẬT KHẨU MỚI
            // ==========================================
            var pnlXacNhan = new Panel { Dock = DockStyle.Fill, AutoSize = true, Margin = new Padding(0, 0, 0, 26) };
            var lblXacNhan = new Label
            {
                Text = "Xác nhận mật khẩu mới *",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(0, 0),
                AutoSize = true
            };
            txtXacNhanMatKhau = new RoundedTextBox
            {
                Location = new Point(0, 24),
                Size = new Size(696, 44),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BorderRadius = ThemeHelper.RadiusMedium,
                IconType = InputIconType.Lock,
                IsPassword = true,
                PlaceholderText = "Nhập lại chính xác mật khẩu mới"
            };
            lblLoiXacNhan = new Label
            {
                Location = new Point(2, 72),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };
            pnlXacNhan.Controls.Add(lblXacNhan);
            pnlXacNhan.Controls.Add(txtXacNhanMatKhau);
            pnlXacNhan.Controls.Add(lblLoiXacNhan);
            tlpCardLayout.Controls.Add(pnlXacNhan, 0, 4);

            // ==========================================
            // 6. BUTTONS: LƯU THAY ĐỔI & HỦY / QUAY LẠI
            // ==========================================
            var pnlButtons = new Panel { Dock = DockStyle.Fill, Height = 48, Margin = new Padding(0, 4, 0, 0) };

            btnLuuThayDoi = new RoundedButton
            {
                Text = "Lưu thay đổi",
                Location = new Point(0, 4),
                Size = new Size(160, ThemeHelper.ChieuCaoButtonChuan),
                BorderRadius = ThemeHelper.RadiusMedium,
                NormalColor = ThemeHelper.MauChinh,
                HoverColor = ThemeHelper.MauChinhHover,
                Font = ThemeHelper.FontButton
            };
            btnLuuThayDoi.Click += BtnLuuThayDoi_Click;

            btnHuy = new RoundedButton
            {
                Text = "Hủy",
                Location = new Point(172, 4),
                Size = new Size(110, ThemeHelper.ChieuCaoButtonChuan),
                BorderRadius = ThemeHelper.RadiusMedium,
                NormalColor = ThemeHelper.MauNutPhu,
                HoverColor = ThemeHelper.MauNutPhuHover,
                BorderColor = ThemeHelper.MauVienNutPhu,
                BorderWidth = 1,
                TextColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDungDam
            };
            btnHuy.Click += (s, e) =>
            {
                XoaTatCaLoi();
                txtMatKhauHienTai.Clear();
                txtMatKhauMoi.Clear();
                txtXacNhanMatKhau.Clear();
                OnQuayLai?.Invoke();
            };

            pnlButtons.Controls.Add(btnLuuThayDoi);
            pnlButtons.Controls.Add(btnHuy);
            tlpCardLayout.Controls.Add(pnlButtons, 0, 5);

            pnlCard.Controls.Add(tlpCardLayout);
            pnlMainContainer.Controls.Add(pnlCard, 0, 0);
            this.Controls.Add(pnlMainContainer);
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
                HienThongBao("Cập nhật mật khẩu thành công! Mật khẩu mới đã được kích hoạt.", laLoi: false);
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
            pnlThongBao.BackColor = laLoi ? ThemeHelper.MauLoiNhe : ThemeHelper.MauThanhCongNhe;
            pnlThongBao.BorderColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.ForeColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.Text = (laLoi ? "⚠  " : "✓  ") + thongBao;
        }
    }
}
