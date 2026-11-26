using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class FormXacNhanDangXuat : Form
    {
        public FormXacNhanDangXuat()
        {
            this.Size = new Size(460, 240);
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
                Padding = new Padding(28, 24, 28, 24)
            };
            pnlMain.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            };

            // Icon Đăng xuất nổi bật
            var pnlIcon = new Panel
            {
                Size = new Size(52, 52),
                Location = new Point(28, 24),
                BackColor = ColorTranslator.FromHtml("#FEF2F2")
            };
            pnlIcon.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Nền tròn đỏ nhạt
                using (var brush = new SolidBrush(ColorTranslator.FromHtml("#FEE2E2")))
                {
                    g.FillEllipse(brush, 0, 0, 51, 51);
                }

                // Vẽ icon cửa mở / mũi tên đăng xuất màu đỏ
                using var pen = new Pen(ColorTranslator.FromHtml("#DC2626"), 2.2f);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                // Khung cửa bên trái
                g.DrawLine(pen, 28, 14, 16, 14);
                g.DrawLine(pen, 16, 14, 16, 38);
                g.DrawLine(pen, 16, 38, 28, 38);

                // Mũi tên hướng ra phải
                g.DrawLine(pen, 22, 26, 36, 26);
                g.DrawLine(pen, 31, 21, 36, 26);
                g.DrawLine(pen, 31, 31, 36, 26);
            };

            var lblTieuDe = new Label
            {
                Text = "Xác nhận đăng xuất",
                Font = new Font("Segoe UI", 13.5f, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#172033"),
                Location = new Point(94, 24),
                AutoSize = true
            };

            var lblNoiDung = new Label
            {
                Text = "Bạn có chắc chắn muốn kết thúc phiên làm việc hiện tại và đăng xuất khỏi hệ thống Quản lý hóa đơn không?",
                Font = new Font("Segoe UI", 10.5f, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#475569"),
                Location = new Point(94, 56),
                Size = new Size(335, 75)
            };

            // Nút Xác nhận đăng xuất (Đỏ)
            var btnDangXuat = new RoundedButton
            {
                Text = "Đăng xuất",
                Size = new Size(130, 42),
                Location = new Point(this.Width - 158, this.Height - 64),
                BorderRadius = 8,
                NormalColor = ColorTranslator.FromHtml("#DC2626"),
                HoverColor = ColorTranslator.FromHtml("#B91C1C"),
                PressedColor = ColorTranslator.FromHtml("#991B1B"),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                DialogResult = DialogResult.Yes
            };

            // Nút Hủy bỏ (Trắng xám)
            var btnHuyBo = new RoundedButton
            {
                Text = "Hủy bỏ",
                Size = new Size(110, 42),
                Location = new Point(this.Width - 276, this.Height - 64),
                BorderRadius = 8,
                NormalColor = ColorTranslator.FromHtml("#F1F5F9"),
                HoverColor = ColorTranslator.FromHtml("#E2E8F0"),
                PressedColor = ColorTranslator.FromHtml("#CBD5E1"),
                TextColor = ColorTranslator.FromHtml("#334155"),
                BorderColor = ColorTranslator.FromHtml("#CBD5E1"),
                BorderWidth = 1,
                Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                DialogResult = DialogResult.No
            };

            pnlMain.Controls.Add(pnlIcon);
            pnlMain.Controls.Add(lblTieuDe);
            pnlMain.Controls.Add(lblNoiDung);
            pnlMain.Controls.Add(btnDangXuat);
            pnlMain.Controls.Add(btnHuyBo);

            this.Controls.Add(pnlMain);
            this.AcceptButton = btnDangXuat;
            this.CancelButton = btnHuyBo;
        }
    }
}

