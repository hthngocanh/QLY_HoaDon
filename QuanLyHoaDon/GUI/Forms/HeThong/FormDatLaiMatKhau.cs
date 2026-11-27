using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class FormDatLaiMatKhau : Form
    {
        private readonly YeuCauCapLaiMatKhauBLL _yeuCauBLL;
        private readonly YeuCauCapLaiMatKhauDTO _yeuCau;

        private TextBox txtMatKhauMoi = null!;
        private TextBox txtXacNhanMatKhau = null!;
        private CheckBox chkHienMatKhau = null!;
        private Label lblThongBaoLoi = null!;
        private RoundedButton btnXacNhan = null!;
        private RoundedButton btnHuy = null!;

        public FormDatLaiMatKhau(YeuCauCapLaiMatKhauDTO yeuCau)
        {
            _yeuCau = yeuCau;
            _yeuCauBLL = new YeuCauCapLaiMatKhauBLL();

            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Đặt lại mật khẩu";
            this.Size = new Size(460, 440);
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
                Text = "Đặt Lại Mật Khẩu Mới",
                Font = ThemeHelper.FontTieuDeTrang,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(28, 16)
            };

            var lblPhuDe = new Label
            {
                Text = $"Tài khoản: {_yeuCau.TenDangNhap} ({_yeuCau.HoTenNhanVien})\nYêu cầu của bạn đã được Quản lý phê duyệt. Vui lòng thiết lập mật khẩu mới.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Size = new Size(390, 36),
                Location = new Point(28, 48)
            };

            // Ô Mật khẩu mới
            var lblMK = new Label
            {
                Text = "Mật khẩu mới * (tối thiểu 6 ký tự)",
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(28, 96)
            };

            var pnlKhungMK = new RoundedPanel
            {
                Location = new Point(28, 122),
                Size = new Size(390, 40),
                BorderColor = ThemeHelper.MauDuongVien,
                BorderRadius = 8,
                BackColor = Color.White,
                Padding = new Padding(10, 8, 10, 8)
            };

            txtMatKhauMoi = new TextBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = ThemeHelper.FontNoiDung,
                UseSystemPasswordChar = true
            };
            pnlKhungMK.Controls.Add(txtMatKhauMoi);

            // Ô Xác nhận mật khẩu
            var lblXN = new Label
            {
                Text = "Xác nhận mật khẩu mới *",
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(28, 172)
            };

            var pnlKhungXN = new RoundedPanel
            {
                Location = new Point(28, 198),
                Size = new Size(390, 40),
                BorderColor = ThemeHelper.MauDuongVien,
                BorderRadius = 8,
                BackColor = Color.White,
                Padding = new Padding(10, 8, 10, 8)
            };

            txtXacNhanMatKhau = new TextBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = ThemeHelper.FontNoiDung,
                UseSystemPasswordChar = true
            };
            pnlKhungXN.Controls.Add(txtXacNhanMatKhau);

            // Checkbox hiển thị mật khẩu
            chkHienMatKhau = new CheckBox
            {
                Text = "Hiển thị mật khẩu",
                Location = new Point(28, 248),
                AutoSize = true,
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Cursor = Cursors.Hand
            };
            chkHienMatKhau.CheckedChanged += (s, e) =>
            {
                bool useSys = !chkHienMatKhau.Checked;
                txtMatKhauMoi.UseSystemPasswordChar = useSys;
                txtXacNhanMatKhau.UseSystemPasswordChar = useSys;
            };

            // Nhãn báo lỗi
            lblThongBaoLoi = new Label
            {
                Text = "",
                ForeColor = ThemeHelper.MauNguyHiem,
                Font = ThemeHelper.FontChuThich,
                Location = new Point(28, 276),
                Size = new Size(390, 36),
                Visible = false
            };

            // Nút bấm
            btnXacNhan = new RoundedButton
            {
                Text = "Lưu mật khẩu mới",
                Size = new Size(160, 42),
                Location = new Point(258, 326),
                NormalColor = ThemeHelper.MauChinh,
                HoverColor = ThemeHelper.MauChinhHover,
                ForeColor = Color.White,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8
            };
            btnXacNhan.Click += BtnXacNhan_Click;

            btnHuy = new RoundedButton
            {
                Text = "Đóng",
                Size = new Size(100, 42),
                Location = new Point(148, 326),
                NormalColor = ColorTranslator.FromHtml("#F1F5F9"),
                HoverColor = ColorTranslator.FromHtml("#E2E8F0"),
                ForeColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDung,
                BorderRadius = 8,
                DialogResult = DialogResult.Cancel
            };
            btnHuy.Click += (s, e) => this.Close();

            pnlContainer.Controls.Add(lblTieuDe);
            pnlContainer.Controls.Add(lblPhuDe);
            pnlContainer.Controls.Add(lblMK);
            pnlContainer.Controls.Add(pnlKhungMK);
            pnlContainer.Controls.Add(lblXN);
            pnlContainer.Controls.Add(pnlKhungXN);
            pnlContainer.Controls.Add(chkHienMatKhau);
            pnlContainer.Controls.Add(lblThongBaoLoi);
            pnlContainer.Controls.Add(btnHuy);
            pnlContainer.Controls.Add(btnXacNhan);

            this.Controls.Add(pnlContainer);
            this.AcceptButton = btnXacNhan;
            this.CancelButton = btnHuy;
        }

        private void BtnXacNhan_Click(object? sender, EventArgs e)
        {
            lblThongBaoLoi.Visible = false;
            string mkMoi = txtMatKhauMoi.Text;
            string xnMk = txtXacNhanMatKhau.Text;

            var res = _yeuCauBLL.DatLaiMatKhau(_yeuCau.MaYeuCau, _yeuCau.TenDangNhap, mkMoi, xnMk);
            if (!res.ThanhCong)
            {
                lblThongBaoLoi.Text = res.ThongBao;
                lblThongBaoLoi.Visible = true;
                return;
            }

            MessageBox.Show(res.ThongBao, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
