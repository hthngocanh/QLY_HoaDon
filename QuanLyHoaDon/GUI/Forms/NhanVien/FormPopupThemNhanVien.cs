using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.NhanVien
{
    /// <summary>
    /// Modal Popup thêm mới nhân viên theo thiết kế chuẩn:
    /// - Họ và tên: Bắt buộc nhập
    /// - Mã nhân viên: Hệ thống tự động sinh, chỉ đọc
    /// - Số điện thoại: Không bắt buộc (kiểm tra định dạng nếu có nhập)
    /// - Địa chỉ: Không bắt buộc
    /// - Tên đăng nhập: Hệ thống tự động sinh theo mã NV (chữ thường, không dấu), chỉ đọc
    /// - Mật khẩu khởi tạo: Cố định Demo@123, chỉ đọc
    /// - Vai trò: Cố định Nhân viên (hiển thị dạng label/text chỉ đọc, không cho chọn)
    /// - Trạng thái nhân viên: Mặc định Đang hoạt động (chỉ đọc, không cho chọn)
    /// - Trạng thái tài khoản: Mặc định Hoạt động (chỉ đọc, không cho chọn)
    /// - Loại bỏ hoàn toàn trường Email
    /// - Căn giữa màn hình, đồng bộ ThemeHelper
    /// </summary>
    public class FormPopupThemNhanVien : Form
    {
        private readonly NhanVienBLL _bll;

        private TextBox txtHoTen = null!;
        private TextBox txtMaNV = null!;
        private TextBox txtSoDienThoai = null!;
        private TextBox txtDiaChi = null!;
        private TextBox txtTenDangNhap = null!;
        private TextBox txtMatKhau = null!;
        private TextBox txtVaiTro = null!;
        private TextBox txtTrangThaiNV = null!;
        private TextBox txtTrangThaiTK = null!;

        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;
        private RoundedButton btnLuu = null!;
        private RoundedButton btnHuy = null!;

        public FormPopupThemNhanVien(NhanVienBLL bll)
        {
            _bll = bll ?? throw new ArgumentNullException(nameof(bll));
            KhoiTaoGiaoDien();
            NapDuLieuKhoiTao();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Thêm Mới Nhân Viên";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(560, 680);
            this.BackColor = ThemeHelper.MauNenNoiDung;
            this.Font = ThemeHelper.FontNoiDung;

            // 1. Header Top Panel
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
                Text = "THÊM MỚI NHÂN VIÊN",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 12),
                AutoSize = true
            };

            var lblPhu = new Label
            {
                Text = "Tạo hồ sơ nhân viên và cấp phát tài khoản nhân viên mặc định.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(24, 38),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblPhu);
            this.Controls.Add(pnlHeader);

            // 2. Container Form Body (AutoScroll)
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 16, 24, 16),
                AutoScroll = true
            };

            // Banner thông báo lỗi / thành công
            pnlThongBao = new Panel
            {
                Location = new Point(24, 8),
                Size = new Size(494, 34),
                Visible = false
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontChuThichDam,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 8, 0)
            };
            pnlThongBao.Controls.Add(lblThongBao);
            pnlBody.Controls.Add(pnlThongBao);

            int top = 48;

            // Họ và tên * (Bắt buộc)
            ThemDongInput(pnlBody, "Họ và tên *:", ref txtHoTen, ref top, placeholder: "Nhập họ tên đầy đủ (Ví dụ: Lê Thị Hằng)");

            // Mã nhân viên (Tự động sinh, chỉ đọc)
            ThemDongInput(pnlBody, "Mã nhân viên (Hệ thống tự động sinh):", ref txtMaNV, ref top, laReadOnly: true);

            // Số điện thoại (Không bắt buộc)
            ThemDongInput(pnlBody, "Số điện thoại:", ref txtSoDienThoai, ref top, placeholder: "10 chữ số (Ví dụ: 0912345678)");

            // Địa chỉ (Không bắt buộc)
            ThemDongInput(pnlBody, "Địa chỉ cư trú:", ref txtDiaChi, ref top, placeholder: "Số nhà, đường, quận/huyện");

            // Đường kẻ phân cách nhẹ
            var pnlDivider = new Panel
            {
                Location = new Point(24, top + 4),
                Size = new Size(494, 1),
                BackColor = ThemeHelper.MauDuongVien
            };
            pnlBody.Controls.Add(pnlDivider);
            top += 16;

            // Tên đăng nhập (Hệ thống tự động sinh từ mã NV, chỉ đọc)
            ThemDongInput(pnlBody, "Tên đăng nhập (Tự động sinh từ mã NV):", ref txtTenDangNhap, ref top, laReadOnly: true);

            // Mật khẩu khởi tạo (Cố định Demo@123, chỉ đọc)
            ThemDongInput(pnlBody, "Mật khẩu khởi tạo mặc định:", ref txtMatKhau, ref top, laReadOnly: true);

            // Vai trò tài khoản (Cố định Nhân viên, chỉ đọc)
            ThemDongInput(pnlBody, "Vai trò tài khoản:", ref txtVaiTro, ref top, laReadOnly: true);

            // Trạng thái nhân sự & tài khoản (Hiển thị 2 ô nhỏ song song)
            int halfWidth = 240;
            var lblTTNV = new Label { Text = "Trạng thái nhân sự:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(24, top), AutoSize = true };
            txtTrangThaiNV = new TextBox { Location = new Point(24, top + 22), Size = new Size(halfWidth, 32), Font = ThemeHelper.FontNoiDung };
            ThemeHelper.ChuanHoaTextBox(txtTrangThaiNV, true);

            var lblTTTK = new Label { Text = "Trạng thái tài khoản:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(24 + halfWidth + 14, top), AutoSize = true };
            txtTrangThaiTK = new TextBox { Location = new Point(24 + halfWidth + 14, top + 22), Size = new Size(halfWidth, 32), Font = ThemeHelper.FontNoiDung };
            ThemeHelper.ChuanHoaTextBox(txtTrangThaiTK, true);

            pnlBody.Controls.Add(lblTTNV);
            pnlBody.Controls.Add(txtTrangThaiNV);
            pnlBody.Controls.Add(lblTTTK);
            pnlBody.Controls.Add(txtTrangThaiTK);
            top += 64;

            this.Controls.Add(pnlBody);

            // 3. Footer Bottom Panel
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 64,
                BackColor = ThemeHelper.MauNenUngDung,
                Padding = new Padding(24, 12, 24, 12)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawLine(pen, 0, 0, pnlFooter.Width, 0);
            };

            btnLuu = new RoundedButton
            {
                Text = "💾 Lưu nhân viên",
                Location = new Point(pnlFooter.Width - 250 - 24, 12),
                Size = new Size(130, ThemeHelper.ChieuCaoButtonChuan),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                NormalColor = ThemeHelper.MauThanhCong,
                HoverColor = ThemeHelper.MauThanhCongHover,
                Font = ThemeHelper.FontButton,
                BorderRadius = ThemeHelper.RadiusMedium
            };
            btnLuu.Click += BtnLuu_Click;

            btnHuy = new RoundedButton
            {
                Text = "Hủy",
                Location = new Point(pnlFooter.Width - 110 - 24, 12),
                Size = new Size(100, ThemeHelper.ChieuCaoButtonChuan),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                NormalColor = ThemeHelper.MauNutPhu,
                HoverColor = ThemeHelper.MauNutPhuHover,
                BorderColor = ThemeHelper.MauVienNutPhu,
                BorderWidth = 1,
                TextColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = ThemeHelper.RadiusMedium
            };
            btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            pnlFooter.Controls.Add(btnLuu);
            pnlFooter.Controls.Add(btnHuy);
            this.Controls.Add(pnlFooter);

            this.AcceptButton = btnLuu;
            this.CancelButton = btnHuy;
        }

        private static void ThemDongInput(Panel parent, string label, ref TextBox txt, ref int top, bool laReadOnly = false, string placeholder = "")
        {
            var lbl = new Label
            {
                Text = label,
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, top),
                AutoSize = true
            };
            txt = new TextBox
            {
                Location = new Point(24, top + 22),
                Size = new Size(494, 32),
                Font = ThemeHelper.FontNoiDung,
                PlaceholderText = placeholder
            };
            ThemeHelper.ChuanHoaTextBox(txt, laReadOnly);
            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            top += 56;
        }

        private void NapDuLieuKhoiTao()
        {
            var (maNV, maNVFormatted, tenDangNhap) = _bll.SinhMaVaTenDangNhapTiepTheo();
            txtMaNV.Text = maNVFormatted;
            txtTenDangNhap.Text = tenDangNhap;
            txtMatKhau.Text = "Demo@123";
            txtVaiTro.Text = "Nhân viên";
            txtTrangThaiNV.Text = "Đang hoạt động";
            txtTrangThaiTK.Text = "Hoạt động";

            txtHoTen.Focus();
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            AnThongBao();

            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                HienThongBao("Họ và tên nhân viên không được để trống.", true);
                txtHoTen.Focus();
                return;
            }

            string sdt = txtSoDienThoai.Text.Trim();
            if (!string.IsNullOrEmpty(sdt) && !ValidationHelper.KiemTraSoDienThoai(sdt))
            {
                HienThongBao("Số điện thoại không hợp lệ (cần đúng 10 chữ số).", true);
                txtSoDienThoai.Focus();
                return;
            }

            var nv = new NhanVienQuanLyDTO
            {
                HoTen = hoTen,
                SoDienThoai = string.IsNullOrEmpty(sdt) ? null : sdt,
                Email = null, // Loại bỏ Email
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                NgayVaoLam = DateTime.Today,
                TrangThaiNhanVien = "DangHoatDong",
                TenDangNhap = txtTenDangNhap.Text.Trim().ToLowerInvariant(),
                MaVaiTro = 2, // Cố định vai trò Nhân viên
                TrangThaiTaiKhoan = "HoatDong"
            };

            btnLuu.Enabled = false;
            try
            {
                // Thực hiện lưu với transaction bảo toàn
                var kq = _bll.ThemNhanVien(nv, "Demo@123");
                if (kq.ThanhCong)
                {
                    MessageBox.Show(kq.ThongBao, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    HienThongBao(kq.ThongBao, true);
                }
            }
            finally
            {
                btnLuu.Enabled = true;
            }
        }

        private void HienThongBao(string thongBao, bool laLoi)
        {
            pnlThongBao.Visible = true;
            pnlThongBao.BackColor = laLoi ? ColorTranslator.FromHtml("#FEF2F2") : ColorTranslator.FromHtml("#F0FDF4");
            lblThongBao.ForeColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.Text = (laLoi ? "⚠ " : "✓ ") + thongBao;
        }

        private void AnThongBao()
        {
            pnlThongBao.Visible = false;
            lblThongBao.Text = string.Empty;
        }
    }
}
