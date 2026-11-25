using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class UCDoiMatKhau : UserControl
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;

        private Panel pnlCard = null!;
        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;
        private Label lblMoTa = null!;

        private Panel pnlMatKhauHienTaiBorder = null!;
        private TextBox txtMatKhauHienTai = null!;
        private Button btnHienHienTai = null!;
        private Label lblLoiMatKhauHienTai = null!;

        private Panel pnlMatKhauMoiBorder = null!;
        private TextBox txtMatKhauMoi = null!;
        private Button btnHienMoi = null!;
        private Label lblLoiMatKhauMoi = null!;

        private Panel pnlXacNhanBorder = null!;
        private TextBox txtXacNhanMatKhau = null!;
        private Button btnHienXacNhan = null!;
        private Label lblLoiXacNhan = null!;

        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;

        private Button btnLuuThayDoi = null!;
        private Button btnQuayLai = null!;

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

            pnlCard = new Panel
            {
                Location = new Point(36, 26),
                Size = new Size(820, 620),
                BackColor = ThemeHelper.MauNenNoiDung,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlCard.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            };

            // Header Banner của trang
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = ThemeHelper.MauSidebarHeader
            };

            lblTieuDe = new Label
            {
                Text = "CẬP NHẬT MẬT KHẨU TÀI KHOẢN",
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = Color.White,
                Location = new Point(24, 18),
                AutoSize = true
            };

            lblMoTa = new Label
            {
                Text = "Dành riêng cho tài khoản Nhân viên để đảm bảo an toàn truy cập dữ liệu hệ thống.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhuSidebar,
                Location = new Point(26, 48),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblMoTa);

            // Banner thông báo kết quả
            pnlThongBao = new Panel
            {
                Location = new Point(30, 105),
                Size = new Size(760, 44),
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontNoiDungDam,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 12, 0)
            };
            pnlThongBao.Controls.Add(lblThongBao);

            // 1. Mật khẩu hiện tại
            var lblHienTai = new Label
            {
                Text = "Mật khẩu hiện tại *",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(30, 160),
                AutoSize = true
            };

            pnlMatKhauHienTaiBorder = new Panel
            {
                Location = new Point(30, 186),
                Size = new Size(540, 44),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            txtMatKhauHienTai = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(12, 11),
                Size = new Size(450, 22),
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                UseSystemPasswordChar = true
            };

            btnHienHienTai = new Button
            {
                Text = "Hiện",
                Location = new Point(470, 5),
                Size = new Size(58, 32),
                FlatStyle = FlatStyle.Flat,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChuPhu,
                Cursor = Cursors.Hand
            };
            btnHienHienTai.FlatAppearance.BorderSize = 0;
            btnHienHienTai.Click += (s, e) => ToggleHienMatKhau(txtMatKhauHienTai, btnHienHienTai);

            pnlMatKhauHienTaiBorder.Controls.Add(txtMatKhauHienTai);
            pnlMatKhauHienTaiBorder.Controls.Add(btnHienHienTai);

            lblLoiMatKhauHienTai = new Label
            {
                Location = new Point(30, 234),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };

            // 2. Mật khẩu mới
            var lblMoi = new Label
            {
                Text = "Mật khẩu mới (Tối thiểu 6 ký tự) *",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(30, 265),
                AutoSize = true
            };

            pnlMatKhauMoiBorder = new Panel
            {
                Location = new Point(30, 291),
                Size = new Size(540, 44),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            txtMatKhauMoi = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(12, 11),
                Size = new Size(450, 22),
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                UseSystemPasswordChar = true
            };

            btnHienMoi = new Button
            {
                Text = "Hiện",
                Location = new Point(470, 5),
                Size = new Size(58, 32),
                FlatStyle = FlatStyle.Flat,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChuPhu,
                Cursor = Cursors.Hand
            };
            btnHienMoi.FlatAppearance.BorderSize = 0;
            btnHienMoi.Click += (s, e) => ToggleHienMatKhau(txtMatKhauMoi, btnHienMoi);

            pnlMatKhauMoiBorder.Controls.Add(txtMatKhauMoi);
            pnlMatKhauMoiBorder.Controls.Add(btnHienMoi);

            lblLoiMatKhauMoi = new Label
            {
                Location = new Point(30, 339),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };

            // 3. Xác nhận mật khẩu mới
            var lblXacNhan = new Label
            {
                Text = "Xác nhận mật khẩu mới *",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(30, 370),
                AutoSize = true
            };

            pnlXacNhanBorder = new Panel
            {
                Location = new Point(30, 396),
                Size = new Size(540, 44),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            txtXacNhanMatKhau = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(12, 11),
                Size = new Size(450, 22),
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                UseSystemPasswordChar = true
            };

            btnHienXacNhan = new Button
            {
                Text = "Hiện",
                Location = new Point(470, 5),
                Size = new Size(58, 32),
                FlatStyle = FlatStyle.Flat,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChuPhu,
                Cursor = Cursors.Hand
            };
            btnHienXacNhan.FlatAppearance.BorderSize = 0;
            btnHienXacNhan.Click += (s, e) => ToggleHienMatKhau(txtXacNhanMatKhau, btnHienXacNhan);

            pnlXacNhanBorder.Controls.Add(txtXacNhanMatKhau);
            pnlXacNhanBorder.Controls.Add(btnHienXacNhan);

            lblLoiXacNhan = new Label
            {
                Location = new Point(30, 444),
                AutoSize = true,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauLoi,
                Visible = false
            };

            // Button Thao tác
            btnLuuThayDoi = new Button
            {
                Text = "💾  Lưu mật khẩu mới",
                Location = new Point(30, 485),
                Size = new Size(200, ThemeHelper.ChieuCaoButtonChuan)
            };
            ThemeHelper.ApDungButtonChinh(btnLuuThayDoi);
            btnLuuThayDoi.Click += BtnLuuThayDoi_Click;

            btnQuayLai = new Button
            {
                Text = "Quay lại",
                Location = new Point(245, 485),
                Size = new Size(130, ThemeHelper.ChieuCaoButtonChuan)
            };
            ThemeHelper.ApDungButtonPhu(btnQuayLai);
            btnQuayLai.Click += (s, e) => OnQuayLai?.Invoke();

            // Focus highlight
            txtMatKhauHienTai.Enter += (s, e) => pnlMatKhauHienTaiBorder.BackColor = Color.White;
            txtMatKhauHienTai.Leave += (s, e) => pnlMatKhauHienTaiBorder.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            txtMatKhauMoi.Enter += (s, e) => pnlMatKhauMoiBorder.BackColor = Color.White;
            txtMatKhauMoi.Leave += (s, e) => pnlMatKhauMoiBorder.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            txtXacNhanMatKhau.Enter += (s, e) => pnlXacNhanBorder.BackColor = Color.White;
            txtXacNhanMatKhau.Leave += (s, e) => pnlXacNhanBorder.BackColor = ColorTranslator.FromHtml("#F8FAFC");

            pnlCard.Controls.Add(pnlHeader);
            pnlCard.Controls.Add(pnlThongBao);
            pnlCard.Controls.Add(lblHienTai);
            pnlCard.Controls.Add(pnlMatKhauHienTaiBorder);
            pnlCard.Controls.Add(lblLoiMatKhauHienTai);

            pnlCard.Controls.Add(lblMoi);
            pnlCard.Controls.Add(pnlMatKhauMoiBorder);
            pnlCard.Controls.Add(lblLoiMatKhauMoi);

            pnlCard.Controls.Add(lblXacNhan);
            pnlCard.Controls.Add(pnlXacNhanBorder);
            pnlCard.Controls.Add(lblLoiXacNhan);

            pnlCard.Controls.Add(btnLuuThayDoi);
            pnlCard.Controls.Add(btnQuayLai);

            this.Controls.Add(pnlCard);
        }

        private void ToggleHienMatKhau(TextBox txt, Button btn)
        {
            txt.UseSystemPasswordChar = !txt.UseSystemPasswordChar;
            btn.Text = txt.UseSystemPasswordChar ? "Hiện" : "Ẩn";
            txt.Focus();
            txt.SelectionStart = txt.Text.Length;
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

