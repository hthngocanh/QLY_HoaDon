using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class FormQuenMatKhau : Form
    {
        private readonly YeuCauCapLaiMatKhauBLL _yeuCauBLL;

        private RoundedTextBox txtTenDangNhap = null!;
        private TextBox txtLyDo = null!;
        private Label lblThongBao = null!;
        private Label lblTrangThaiYeuCau = null!;
        private RoundedButton btnGuiYeuCau = null!;
        private RoundedButton btnDatLaiMatKhau = null!;
        private RoundedButton btnDong = null!;
        private YeuCauCapLaiMatKhauDTO? _yeuCauHienTai;

        public FormQuenMatKhau()
        {
            _yeuCauBLL = new YeuCauCapLaiMatKhauBLL();
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Yêu cầu cấp lại mật khẩu";
            this.Size = new Size(500, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = ThemeHelper.FontNoiDung;

            var pnlContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(28, 20, 28, 20),
                BackColor = Color.White
            };

            var lblTieuDe = new Label
            {
                Text = "Yêu Cầu Cấp Lại Mật Khẩu",
                Font = ThemeHelper.FontTieuDeTrang,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(28, 16)
            };

            var lblMoTa = new Label
            {
                Text = "Nhập tên đăng nhập của bạn để gửi yêu cầu tới Quản lý hệ thống. Sau khi được duyệt, bạn sẽ có thể đặt lại mật khẩu mới.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Size = new Size(430, 36),
                Location = new Point(28, 48)
            };

            // Ô Tên đăng nhập
            var lblTenDN = new Label
            {
                Text = "Tên đăng nhập *",
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(28, 96)
            };

            txtTenDangNhap = new RoundedTextBox
            {
                Location = new Point(28, 122),
                Size = new Size(430, 40),
                Font = ThemeHelper.FontNoiDung,
                PlaceholderText = "Ví dụ: nhanvien01, lan.pham..."
            };
            txtTenDangNhap.TextChanged += TxtTenDangNhap_TextChanged;

            // Ô Lý do yêu cầu
            var lblLyDo = new Label
            {
                Text = "Lý do yêu cầu (tùy chọn)",
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(28, 172)
            };

            var pnlKhungLyDo = new RoundedPanel
            {
                Location = new Point(28, 198),
                Size = new Size(430, 80),
                BorderColor = ThemeHelper.MauDuongVien,
                BorderRadius = 8,
                BackColor = Color.White,
                Padding = new Padding(8)
            };

            txtLyDo = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                BorderStyle = BorderStyle.None,
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                ScrollBars = ScrollBars.Vertical
            };
            pnlKhungLyDo.Controls.Add(txtLyDo);

            // Nhãn hiển thị trạng thái yêu cầu gần nhất
            lblTrangThaiYeuCau = new Label
            {
                Location = new Point(28, 288),
                Size = new Size(430, 48),
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Text = "Nhập tên đăng nhập để kiểm tra trạng thái yêu cầu gần nhất...",
                Visible = true
            };

            // Nhãn thông báo kết quả
            lblThongBao = new Label
            {
                Location = new Point(28, 342),
                Size = new Size(430, 40),
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChinh,
                Visible = false
            };

            // Nút Gửi yêu cầu
            btnGuiYeuCau = new RoundedButton
            {
                Text = "Gửi yêu cầu",
                Size = new Size(130, 42),
                Location = new Point(328, 410),
                NormalColor = ThemeHelper.MauChinh,
                HoverColor = ThemeHelper.MauChinhHover,
                ForeColor = Color.White,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8
            };
            btnGuiYeuCau.Click += BtnGuiYeuCau_Click;

            // Nút Đặt lại mật khẩu (chỉ hiện khi yêu cầu đã được duyệt)
            btnDatLaiMatKhau = new RoundedButton
            {
                Text = "🔑 Đặt mật khẩu mới",
                Size = new Size(160, 42),
                Location = new Point(158, 410),
                NormalColor = ThemeHelper.MauThanhCong,
                HoverColor = ColorTranslator.FromHtml("#15803D"),
                ForeColor = Color.White,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8,
                Visible = false
            };
            btnDatLaiMatKhau.Click += BtnDatLaiMatKhau_Click;

            // Nút Đóng / Hủy
            btnDong = new RoundedButton
            {
                Text = "Hủy",
                Size = new Size(90, 42),
                Location = new Point(228, 410),
                NormalColor = ColorTranslator.FromHtml("#F1F5F9"),
                HoverColor = ColorTranslator.FromHtml("#E2E8F0"),
                ForeColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDung,
                BorderRadius = 8,
                DialogResult = DialogResult.Cancel
            };
            btnDong.Click += (s, e) => this.Close();

            pnlContainer.Controls.Add(lblTieuDe);
            pnlContainer.Controls.Add(lblMoTa);
            pnlContainer.Controls.Add(lblTenDN);
            pnlContainer.Controls.Add(txtTenDangNhap);
            pnlContainer.Controls.Add(lblLyDo);
            pnlContainer.Controls.Add(pnlKhungLyDo);
            pnlContainer.Controls.Add(lblTrangThaiYeuCau);
            pnlContainer.Controls.Add(lblThongBao);
            pnlContainer.Controls.Add(btnDatLaiMatKhau);
            pnlContainer.Controls.Add(btnDong);
            pnlContainer.Controls.Add(btnGuiYeuCau);

            this.Controls.Add(pnlContainer);
            this.AcceptButton = btnGuiYeuCau;
            this.CancelButton = btnDong;
        }

        private void TxtTenDangNhap_TextChanged(object? sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            string tenDN = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDN))
            {
                lblTrangThaiYeuCau.Text = "Nhập tên đăng nhập để kiểm tra trạng thái yêu cầu gần nhất...";
                lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauChuPhu;
                btnDatLaiMatKhau.Visible = false;
                btnDong.Location = new Point(228, 410);
                btnGuiYeuCau.Visible = true;
                return;
            }

            try
            {
                _yeuCauHienTai = _yeuCauBLL.LayYeuCauMoiNhat(tenDN);
                if (_yeuCauHienTai == null)
                {
                    lblTrangThaiYeuCau.Text = "Tài khoản chưa có yêu cầu cấp lại mật khẩu nào.";
                    lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauChuPhu;
                    btnDatLaiMatKhau.Visible = false;
                    btnDong.Location = new Point(228, 410);
                    btnGuiYeuCau.Visible = true;
                }
                else
                {
                    switch (_yeuCauHienTai.TrangThai)
                    {
                        case "ChoDuyet":
                            lblTrangThaiYeuCau.Text = $"⏳ Yêu cầu gửi lúc {_yeuCauHienTai.ThoiGianYeuCau:HH:mm dd/MM/yyyy} đang Chờ Quản lý phê duyệt.";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauCanhBao;
                            btnDatLaiMatKhau.Visible = false;
                            btnDong.Location = new Point(228, 410);
                            btnGuiYeuCau.Visible = false; // Tránh gửi lặp
                            break;

                        case "DaDuyet":
                            lblTrangThaiYeuCau.Text = $"✅ Yêu cầu đã được Quản lý duyệt lúc {_yeuCauHienTai.ThoiGianDuyet:HH:mm dd/MM/yyyy}! Bạn có thể nhấn 'Đặt mật khẩu mới' bên dưới.";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauThanhCong;
                            btnDatLaiMatKhau.Visible = true;
                            btnDong.Location = new Point(58, 410);
                            btnGuiYeuCau.Visible = false;
                            break;

                        case "DaTuChoi":
                            lblTrangThaiYeuCau.Text = $"❌ Yêu cầu trước đó bị từ chối lúc {_yeuCauHienTai.ThoiGianDuyet:HH:mm dd/MM/yyyy}.\nLý do: {_yeuCauHienTai.LyDoTuChoi}";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauNguyHiem;
                            btnDatLaiMatKhau.Visible = false;
                            btnDong.Location = new Point(228, 410);
                            btnGuiYeuCau.Visible = true; // Cho phép gửi yêu cầu mới
                            break;

                        case "DaHoanTat":
                            lblTrangThaiYeuCau.Text = $"Yêu cầu gần nhất đã hoàn tất lúc {_yeuCauHienTai.ThoiGianHoanTat:HH:mm dd/MM/yyyy}.";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauChuPhu;
                            btnDatLaiMatKhau.Visible = false;
                            btnDong.Location = new Point(228, 410);
                            btnGuiYeuCau.Visible = true;
                            break;
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi kết nối trong khi gõ
            }
        }

        private void BtnGuiYeuCau_Click(object? sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            string tenDN = txtTenDangNhap.Text.Trim();
            string? lyDo = txtLyDo.Text.Trim();

            btnGuiYeuCau.Enabled = false;
            btnGuiYeuCau.Text = "Đang gửi...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var ketQua = _yeuCauBLL.GuiYeuCau(tenDN, lyDo);
                if (ketQua.ThanhCong)
                {
                    lblThongBao.ForeColor = ThemeHelper.MauThanhCong;
                    lblThongBao.Text = ketQua.ThongBao;
                    lblThongBao.Visible = true;

                    TxtTenDangNhap_TextChanged(null, EventArgs.Empty);
                    MessageBox.Show(ketQua.ThongBao, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblThongBao.ForeColor = ThemeHelper.MauNguyHiem;
                    lblThongBao.Text = ketQua.ThongBao;
                    lblThongBao.Visible = true;
                }
            }
            finally
            {
                btnGuiYeuCau.Enabled = true;
                btnGuiYeuCau.Text = "Gửi yêu cầu";
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnDatLaiMatKhau_Click(object? sender, EventArgs e)
        {
            if (_yeuCauHienTai == null || _yeuCauHienTai.TrangThai != "DaDuyet")
            {
                MessageBox.Show("Chưa có yêu cầu nào được duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FormDatLaiMatKhau(_yeuCauHienTai);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                TxtTenDangNhap_TextChanged(null, EventArgs.Empty);
            }
        }
    }
}
