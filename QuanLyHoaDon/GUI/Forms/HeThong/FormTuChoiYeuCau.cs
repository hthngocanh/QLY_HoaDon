using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class FormTuChoiYeuCau : Form
    {
        private TextBox txtLyDo = null!;
        private RoundedButton btnXacNhan = null!;
        private RoundedButton btnHuy = null!;
        private Label lblLoi = null!;

        public string LyDoTuChoi { get; private set; } = string.Empty;

        public FormTuChoiYeuCau(string tenDangNhap, string tenNhanVien)
        {
            KhoiTaoGiaoDien(tenDangNhap, tenNhanVien);
        }

        private void KhoiTaoGiaoDien(string tenDangNhap, string tenNhanVien)
        {
            this.Text = "Từ chối yêu cầu cấp lại mật khẩu";
            this.Size = new Size(460, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = ThemeHelper.FontNoiDung;

            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 20, 24, 20),
                BackColor = Color.White
            };

            var lblTieuDe = new Label
            {
                Text = "Từ Chối Yêu Cầu Cấp Lại Mật Khẩu",
                Font = ThemeHelper.FontTieuDeTrang,
                ForeColor = ThemeHelper.MauNguyHiem,
                AutoSize = true,
                Location = new Point(24, 16)
            };

            var lblMoTa = new Label
            {
                Text = $"Tài khoản: {tenDangNhap} ({tenNhanVien})\nVui lòng nhập lý do từ chối (bắt buộc):",
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 46),
                Size = new Size(395, 40)
            };

            var pnlBorder = new RoundedPanel
            {
                Location = new Point(24, 92),
                Size = new Size(395, 90),
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
                ScrollBars = ScrollBars.Vertical
            };
            pnlBorder.Controls.Add(txtLyDo);

            lblLoi = new Label
            {
                Text = "",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauNguyHiem,
                Location = new Point(24, 186),
                Size = new Size(395, 24),
                Visible = false
            };

            btnXacNhan = new RoundedButton
            {
                Text = "Xác nhận từ chối",
                Size = new Size(150, 40),
                Location = new Point(269, 218),
                NormalColor = ThemeHelper.MauNguyHiem,
                HoverColor = ColorTranslator.FromHtml("#B91C1C"),
                ForeColor = Color.White,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8
            };
            btnXacNhan.Click += (s, e) =>
            {
                string lyDo = txtLyDo.Text.Trim();
                if (string.IsNullOrWhiteSpace(lyDo))
                {
                    lblLoi.Text = "Lý do từ chối không được để trống!";
                    lblLoi.Visible = true;
                    txtLyDo.Focus();
                    return;
                }

                LyDoTuChoi = lyDo;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            btnHuy = new RoundedButton
            {
                Text = "Hủy",
                Size = new Size(80, 40),
                Location = new Point(179, 218),
                NormalColor = ColorTranslator.FromHtml("#F1F5F9"),
                HoverColor = ColorTranslator.FromHtml("#E2E8F0"),
                ForeColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDung,
                BorderRadius = 8,
                DialogResult = DialogResult.Cancel
            };
            btnHuy.Click += (s, e) => this.Close();

            pnl.Controls.Add(lblTieuDe);
            pnl.Controls.Add(lblMoTa);
            pnl.Controls.Add(pnlBorder);
            pnl.Controls.Add(lblLoi);
            pnl.Controls.Add(btnHuy);
            pnl.Controls.Add(btnXacNhan);

            this.Controls.Add(pnl);
            this.AcceptButton = btnXacNhan;
            this.CancelButton = btnHuy;
        }
    }
}
