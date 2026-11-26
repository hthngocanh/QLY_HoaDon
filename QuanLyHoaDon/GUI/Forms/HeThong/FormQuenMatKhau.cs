using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class FormQuenMatKhau : Form
    {
        public FormQuenMatKhau()
        {
            this.Size = new Size(440, 260);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10f, FontStyle.Regular);

            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            var pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(28, 24, 28, 20)
            };
            pnlMain.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            };

            // Icon biểu tượng trợ giúp
            var pnlIcon = new Panel
            {
                Size = new Size(48, 48),
                Location = new Point(28, 22),
                BackColor = ColorTranslator.FromHtml("#EFF6FF")
            };
            pnlIcon.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Bo tròn
                using (var brush = new SolidBrush(ColorTranslator.FromHtml("#DBEAFE")))
                {
                    g.FillEllipse(brush, 0, 0, 47, 47);
                }

                // Vẽ chìa khóa / icon info
                using (var pen = new Pen(ColorTranslator.FromHtml("#2563EB"), 2f))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;

                    // Thân chìa
                    g.DrawEllipse(pen, 13, 14, 10, 10);
                    g.DrawLine(pen, 23, 24, 33, 34);
                    g.DrawLine(pen, 29, 30, 32, 27);
                    g.DrawLine(pen, 31, 32, 34, 29);
                }
            };

            var lblTieuDe = new Label
            {
                Text = "Hỗ trợ cấp lại mật khẩu",
                Font = new Font("Segoe UI", 13.5f, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#172033"),
                Location = new Point(88, 24),
                AutoSize = true
            };

            var lblNoiDung = new Label
            {
                Text = "Bạn không nhớ mật khẩu đăng nhập?\n\nVì lý do an toàn và bảo mật thông tin doanh nghiệp, vui lòng liên hệ trực tiếp Quản lý hệ thống để được xác minh và hỗ trợ cấp lại mật khẩu mới.",
                Font = new Font("Segoe UI", 10.5f, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#475569"),
                Location = new Point(28, 86),
                Size = new Size(384, 90)
            };

            var btnDongY = new RoundedButton
            {
                Text = "Đã hiểu",
                Size = new Size(130, 42),
                Location = new Point(this.Width - 158, this.Height - 62),
                BorderRadius = 8,
                NormalColor = ColorTranslator.FromHtml("#2563EB"),
                HoverColor = ColorTranslator.FromHtml("#1D4ED8"),
                PressedColor = ColorTranslator.FromHtml("#1E40AF"),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            btnDongY.Click += (s, e) => this.Close();

            pnlMain.Controls.Add(pnlIcon);
            pnlMain.Controls.Add(lblTieuDe);
            pnlMain.Controls.Add(lblNoiDung);
            pnlMain.Controls.Add(btnDongY);

            this.Controls.Add(pnlMain);
            this.AcceptButton = btnDongY;
        }
    }
}

