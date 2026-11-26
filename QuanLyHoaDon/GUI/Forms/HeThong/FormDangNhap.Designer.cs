using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    partial class FormDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        // Vùng bên trái - Branding & Illustration
        private Panel pnlLeft;
        private Panel pnlLogoBadge;
        private Label lblLogoText;
        private Label lblBrandTieuDe;
        private Label lblBrandSlogan;
        private Panel pnlIllustrationCard;
        private Panel pnlBullet1;
        private Panel pnlBullet2;
        private Panel pnlBullet3;
        private Label lblBullet1Text;
        private Label lblBullet2Text;
        private Label lblBullet3Text;
        private Label lblBrandVersion;

        // Vùng bên phải - Login Form
        private Panel pnlRight;
        private Panel pnlFormContainer;
        private Label lblTieuDe;
        private Label lblMoTa;
        private Panel pnlThongBaoLoi;
        private Label lblThongBaoLoi;
        private Label lblLabelTenDangNhap;
        private RoundedTextBox txtTenDangNhap;
        private Label lblLabelMatKhau;
        private RoundedTextBox txtMatKhau;
        private Label lblQuenMatKhau;
        private RoundedButton btnDangNhap;
        private Panel pnlDemoHint;
        private Label lblDemoHint;
        private Label lblFooterCopyright;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLeft = new Panel();
            this.pnlLogoBadge = new Panel();
            this.lblLogoText = new Label();
            this.lblBrandTieuDe = new Label();
            this.lblBrandSlogan = new Label();
            this.pnlIllustrationCard = new Panel();
            this.pnlBullet1 = new Panel();
            this.lblBullet1Text = new Label();
            this.pnlBullet2 = new Panel();
            this.lblBullet2Text = new Label();
            this.pnlBullet3 = new Panel();
            this.lblBullet3Text = new Label();
            this.lblBrandVersion = new Label();

            this.pnlRight = new Panel();
            this.pnlFormContainer = new Panel();
            this.lblTieuDe = new Label();
            this.lblMoTa = new Label();
            this.pnlThongBaoLoi = new Panel();
            this.lblThongBaoLoi = new Label();
            this.lblLabelTenDangNhap = new Label();
            this.txtTenDangNhap = new RoundedTextBox();
            this.lblLabelMatKhau = new Label();
            this.txtMatKhau = new RoundedTextBox();
            this.lblQuenMatKhau = new Label();
            this.btnDangNhap = new RoundedButton();
            this.pnlDemoHint = new Panel();
            this.lblDemoHint = new Label();
            this.lblFooterCopyright = new Label();

            this.pnlLeft.SuspendLayout();
            this.pnlLogoBadge.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlFormContainer.SuspendLayout();
            this.pnlThongBaoLoi.SuspendLayout();
            this.pnlDemoHint.SuspendLayout();
            this.SuspendLayout();

            // =========================================================
            // 1. PANEL TRÁI - BRANDING & ILLUSTRATION (Navy #172F50 -> #1E3A5F)
            // =========================================================
            this.pnlLeft.Dock = DockStyle.Left;
            this.pnlLeft.Width = 510;
            this.pnlLeft.BackColor = ColorTranslator.FromHtml("#172F50");
            this.pnlLeft.Padding = new Padding(46, 42, 46, 36);

            // Vẽ gradient nền và các họa tiết hình học sang trọng
            this.pnlLeft.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Gradient nền
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    this.pnlLeft.ClientRectangle,
                    ColorTranslator.FromHtml("#132644"),
                    ColorTranslator.FromHtml("#1E3A5F"),
                    45f))
                {
                    g.FillRectangle(brush, this.pnlLeft.ClientRectangle);
                }

                // Vòng tròn họa tiết mờ tạo chiều sâu
                using (var penCir = new Pen(Color.FromArgb(18, 255, 255, 255), 1.5f))
                {
                    g.DrawEllipse(penCir, -40, -40, 220, 220);
                    g.DrawEllipse(penCir, 320, 360, 280, 280);
                }
                using (var brushDot = new SolidBrush(Color.FromArgb(12, 59, 130, 246)))
                {
                    g.FillEllipse(brushDot, 280, 70, 180, 180);
                }
            };

            // Logo Badge
            this.pnlLogoBadge.Size = new Size(48, 48);
            this.pnlLogoBadge.Location = new Point(46, 42);
            this.pnlLogoBadge.BackColor = ColorTranslator.FromHtml("#2563EB");
            this.pnlLogoBadge.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var path = new System.Drawing.Drawing2D.GraphicsPath();
                int r = 10;
                path.AddArc(0, 0, r * 2, r * 2, 180, 90);
                path.AddArc(48 - r * 2, 0, r * 2, r * 2, 270, 90);
                path.AddArc(48 - r * 2, 48 - r * 2, r * 2, r * 2, 0, 90);
                path.AddArc(0, 48 - r * 2, r * 2, r * 2, 90, 90);
                path.CloseFigure();
                using var b = new SolidBrush(ColorTranslator.FromHtml("#2563EB"));
                g.FillPath(b, path);

                // Icon hóa đơn vẽ vector trắng
                using var pen = new Pen(Color.White, 2f);
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                g.DrawRectangle(pen, 13, 10, 22, 28);
                g.DrawLine(pen, 18, 17, 27, 17);
                g.DrawLine(pen, 18, 23, 30, 23);
                g.DrawLine(pen, 18, 29, 25, 29);
            };

            // Tên phần mềm
            this.lblBrandTieuDe.Text = "QUẢN LÝ HÓA ĐƠN";
            this.lblBrandTieuDe.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
            this.lblBrandTieuDe.ForeColor = Color.White;
            this.lblBrandTieuDe.Location = new Point(106, 43);
            this.lblBrandTieuDe.AutoSize = true;

            // Slogan
            this.lblBrandSlogan.Text = "Quản lý hóa đơn đơn giản, hiệu quả và chính xác.";
            this.lblBrandSlogan.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            this.lblBrandSlogan.ForeColor = ColorTranslator.FromHtml("#94A3B8");
            this.lblBrandSlogan.Location = new Point(106, 70);
            this.lblBrandSlogan.Size = new Size(360, 24);

            // =========================================================
            // CARD MINH HỌA HÓA ĐƠN DOANH NGHIỆP (Illustration Card)
            // =========================================================
            this.pnlIllustrationCard.Size = new Size(418, 260);
            this.pnlIllustrationCard.Location = new Point(46, 128);
            this.pnlIllustrationCard.BackColor = Color.White;
            this.pnlIllustrationCard.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                var rect = new Rectangle(0, 0, this.pnlIllustrationCard.Width - 1, this.pnlIllustrationCard.Height - 1);
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int r = 12;
                    path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
                    path.AddArc(rect.Right - r * 2, rect.Y, r * 2, r * 2, 270, 90);
                    path.AddArc(rect.Right - r * 2, rect.Bottom - r * 2, r * 2, r * 2, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - r * 2, r * 2, r * 2, 90, 90);
                    path.CloseFigure();

                    // Nền card trắng bóng bẩy
                    using (var brush = new SolidBrush(Color.White))
                    {
                        g.FillPath(brush, path);
                    }
                    // Viền tinh tế
                    using (var pen = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1f))
                    {
                        g.DrawPath(pen, path);
                    }
                }

                // Tiêu đề card HÓA ĐƠN GTGT
                using (var fontHead = new Font("Segoe UI", 11.5f, FontStyle.Bold))
                using (var brushHead = new SolidBrush(ColorTranslator.FromHtml("#172033")))
                {
                    g.DrawString("HÓA ĐƠN BÁN HÀNG", fontHead, brushHead, new Point(22, 18));
                }

                // Mã số hóa đơn
                using (var fontCode = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                using (var brushCode = new SolidBrush(ColorTranslator.FromHtml("#64748B")))
                {
                    g.DrawString("Số: HD-2026-0889 • Ngày: 25/11/2026", fontCode, brushCode, new Point(22, 42));
                }

                // Badge trạng thái "ĐÃ THANH TOÁN"
                using (var badgePath = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    var bRect = new Rectangle(270, 18, 126, 26);
                    int br = 6;
                    badgePath.AddArc(bRect.X, bRect.Y, br * 2, br * 2, 180, 90);
                    badgePath.AddArc(bRect.Right - br * 2, bRect.Y, br * 2, br * 2, 270, 90);
                    badgePath.AddArc(bRect.Right - br * 2, bRect.Bottom - br * 2, br * 2, br * 2, 0, 90);
                    badgePath.AddArc(bRect.X, bRect.Bottom - br * 2, br * 2, br * 2, 90, 90);
                    badgePath.CloseFigure();

                    using var bBg = new SolidBrush(ColorTranslator.FromHtml("#DCFCE7"));
                    g.FillPath(bBg, badgePath);

                    using var bFont = new Font("Segoe UI", 9f, FontStyle.Bold);
                    using var bText = new SolidBrush(ColorTranslator.FromHtml("#15803D"));
                    g.DrawString("✓ ĐÃ XÁC NHẬN", bFont, bText, new Point(282, 23));
                }

                // Đường phân cách
                using (var penDiv = new Pen(ColorTranslator.FromHtml("#F1F5F9"), 1.5f))
                {
                    g.DrawLine(penDiv, 22, 74, 396, 74);
                }

                // Thông tin khách hàng
                using (var fontSub = new Font("Segoe UI", 9.5f, FontStyle.Bold))
                using (var brushSub = new SolidBrush(ColorTranslator.FromHtml("#334155")))
                {
                    g.DrawString("Khách hàng: Công ty TNHH Giải Pháp Số Ánh Dương", fontSub, brushSub, new Point(22, 86));
                }

                // Bảng dòng chi tiết hóa đơn
                using (var fontRow = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                using (var brushRow = new SolidBrush(ColorTranslator.FromHtml("#475569")))
                using (var brushVal = new SolidBrush(ColorTranslator.FromHtml("#1E293B")))
                {
                    g.DrawString("1. Phần mềm Quản lý Hóa đơn v1.0", fontRow, brushRow, new Point(22, 116));
                    g.DrawString("12.500.000 đ", fontRow, brushVal, new Point(310, 116));

                    g.DrawString("2. Gói hỗ trợ & cập nhật 12 tháng", fontRow, brushRow, new Point(22, 142));
                    g.DrawString("2.500.000 đ", fontRow, brushVal, new Point(318, 142));

                    g.DrawString("3. Thuế suất GTGT (VAT 8%)", fontRow, brushRow, new Point(22, 168));
                    g.DrawString("1.200.000 đ", fontRow, brushVal, new Point(318, 168));
                }

                // Đường phân cách dưới
                using (var penDiv2 = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1f))
                {
                    g.DrawLine(penDiv2, 22, 202, 396, 202);
                }

                // Tổng tiền
                using (var fontTotalLbl = new Font("Segoe UI", 10.5f, FontStyle.Bold))
                using (var brushTotalLbl = new SolidBrush(ColorTranslator.FromHtml("#1E3A5F")))
                {
                    g.DrawString("TỔNG CỘNG:", fontTotalLbl, brushTotalLbl, new Point(22, 218));
                }

                using (var fontTotalVal = new Font("Segoe UI", 13.5f, FontStyle.Bold))
                using (var brushTotalVal = new SolidBrush(ColorTranslator.FromHtml("#2563EB")))
                {
                    g.DrawString("16.200.000 đ", fontTotalVal, brushTotalVal, new Point(278, 214));
                }
            };

            // Bullet features
            this.lblBullet1Text.Text = "✓   Tự động hóa lập và kiểm soát hóa đơn bán hàng";
            this.lblBullet1Text.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            this.lblBullet1Text.ForeColor = Color.White;
            this.lblBullet1Text.Location = new Point(48, 412);
            this.lblBullet1Text.AutoSize = true;

            this.lblBullet2Text.Text = "✓   Phân quyền bảo mật hai tầng giữa Quản lý & Nhân viên";
            this.lblBullet2Text.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            this.lblBullet2Text.ForeColor = Color.White;
            this.lblBullet2Text.Location = new Point(48, 448);
            this.lblBullet2Text.AutoSize = true;

            this.lblBullet3Text.Text = "✓   Báo cáo thống kê & quản trị doanh thu trực quan";
            this.lblBullet3Text.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            this.lblBullet3Text.ForeColor = Color.White;
            this.lblBullet3Text.Location = new Point(48, 484);
            this.lblBullet3Text.AutoSize = true;

            // Version info footer
            this.lblBrandVersion.Text = "Phiên bản 1.0.0 • Nền tảng .NET 10 LTS";
            this.lblBrandVersion.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            this.lblBrandVersion.ForeColor = ColorTranslator.FromHtml("#94A3B8");
            this.lblBrandVersion.Location = new Point(48, 595);
            this.lblBrandVersion.AutoSize = true;

            this.pnlLeft.Controls.Add(this.pnlLogoBadge);
            this.pnlLeft.Controls.Add(this.lblBrandTieuDe);
            this.pnlLeft.Controls.Add(this.lblBrandSlogan);
            this.pnlLeft.Controls.Add(this.pnlIllustrationCard);
            this.pnlLeft.Controls.Add(this.lblBullet1Text);
            this.pnlLeft.Controls.Add(this.lblBullet2Text);
            this.pnlLeft.Controls.Add(this.lblBullet3Text);
            this.pnlLeft.Controls.Add(this.lblBrandVersion);

            // =========================================================
            // 2. PANEL PHẢI - LOGIN FORM (Light Clean #F8FAFC)
            // =========================================================
            this.pnlRight.Dock = DockStyle.Fill;
            this.pnlRight.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            this.pnlRight.Resize += (s, e) =>
            {
                // Canh giữa dọc và ngang
                int x = (this.pnlRight.Width - this.pnlFormContainer.Width) / 2;
                int y = (this.pnlRight.Height - this.pnlFormContainer.Height) / 2;
                this.pnlFormContainer.Location = new Point(Math.Max(20, x), Math.Max(20, y));
            };

            // Container chứa toàn bộ form nhập liệu (rộng 430px)
            this.pnlFormContainer.Size = new Size(430, 560);
            this.pnlFormContainer.BackColor = Color.Transparent;

            // 1. Tiêu đề
            this.lblTieuDe.Text = "Chào mừng trở lại!";
            this.lblTieuDe.Font = new Font("Segoe UI", 24f, FontStyle.Bold);
            this.lblTieuDe.ForeColor = ColorTranslator.FromHtml("#172033");
            this.lblTieuDe.Location = new Point(0, 8);
            this.lblTieuDe.AutoSize = true;

            // 2. Mô tả
            this.lblMoTa.Text = "Đăng nhập để tiếp tục sử dụng hệ thống.";
            this.lblMoTa.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            this.lblMoTa.ForeColor = ColorTranslator.FromHtml("#475569");
            this.lblMoTa.Location = new Point(2, 54);
            this.lblMoTa.AutoSize = true;

            // 3. Panel Thông báo lỗi
            this.pnlThongBaoLoi.Size = new Size(430, 42);
            this.pnlThongBaoLoi.Location = new Point(0, 92);
            this.pnlThongBaoLoi.BackColor = ColorTranslator.FromHtml("#FEF2F2");
            this.pnlThongBaoLoi.Visible = false;
            this.pnlThongBaoLoi.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#FECACA"), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, this.pnlThongBaoLoi.Width - 1, this.pnlThongBaoLoi.Height - 1);
            };

            this.lblThongBaoLoi.Dock = DockStyle.Fill;
            this.lblThongBaoLoi.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.lblThongBaoLoi.ForeColor = ColorTranslator.FromHtml("#B91C1C");
            this.lblThongBaoLoi.TextAlign = ContentAlignment.MiddleLeft;
            this.lblThongBaoLoi.Padding = new Padding(12, 0, 12, 0);

            // 4. Label Tên đăng nhập
            this.lblLabelTenDangNhap.Text = "TÊN ĐĂNG NHẬP";
            this.lblLabelTenDangNhap.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblLabelTenDangNhap.ForeColor = ColorTranslator.FromHtml("#172033");
            this.lblLabelTenDangNhap.Location = new Point(2, 146);
            this.lblLabelTenDangNhap.AutoSize = true;

            // 5. Input Tên đăng nhập
            this.txtTenDangNhap.Location = new Point(0, 172);
            this.txtTenDangNhap.Size = new Size(430, 50);
            this.txtTenDangNhap.IconType = InputIconType.User;
            this.txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập của bạn...";

            // 6. Label Mật khẩu
            this.lblLabelMatKhau.Text = "MẬT KHẨU";
            this.lblLabelMatKhau.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblLabelMatKhau.ForeColor = ColorTranslator.FromHtml("#172033");
            this.lblLabelMatKhau.Location = new Point(2, 238);
            this.lblLabelMatKhau.AutoSize = true;

            // 7. Input Mật khẩu
            this.txtMatKhau.Location = new Point(0, 264);
            this.txtMatKhau.Size = new Size(430, 50);
            this.txtMatKhau.IconType = InputIconType.Lock;
            this.txtMatKhau.UseSystemPasswordChar = true;
            this.txtMatKhau.PlaceholderText = "Nhập mật khẩu...";

            // 8. Link Quên mật khẩu? (Căn phải)
            this.lblQuenMatKhau.Text = "Quên mật khẩu?";
            this.lblQuenMatKhau.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.lblQuenMatKhau.ForeColor = ColorTranslator.FromHtml("#2563EB");
            this.lblQuenMatKhau.Cursor = Cursors.Hand;
            this.lblQuenMatKhau.AutoSize = true;
            this.lblQuenMatKhau.Location = new Point(316, 324);
            this.lblQuenMatKhau.MouseEnter += (s, e) => this.lblQuenMatKhau.ForeColor = ColorTranslator.FromHtml("#1D4ED8");
            this.lblQuenMatKhau.MouseLeave += (s, e) => this.lblQuenMatKhau.ForeColor = ColorTranslator.FromHtml("#2563EB");

            // 9. Nút Đăng nhập
            this.btnDangNhap.Location = new Point(0, 356);
            this.btnDangNhap.Size = new Size(430, 50);
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.BorderRadius = 10;
            this.btnDangNhap.NormalColor = ColorTranslator.FromHtml("#2563EB");
            this.btnDangNhap.HoverColor = ColorTranslator.FromHtml("#1D4ED8");
            this.btnDangNhap.PressedColor = ColorTranslator.FromHtml("#1E40AF");
            this.btnDangNhap.Font = new Font("Segoe UI", 11f, FontStyle.Bold);

            // 10. Card gợi ý tài khoản Demo
            this.pnlDemoHint.Size = new Size(430, 52);
            this.pnlDemoHint.Location = new Point(0, 422);
            this.pnlDemoHint.BackColor = Color.White;
            this.pnlDemoHint.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, this.pnlDemoHint.Width - 1, this.pnlDemoHint.Height - 1);
            };

            this.lblDemoHint.Dock = DockStyle.Fill;
            this.lblDemoHint.Text = "💡  Tài khoản demo: quanly01 | nhanvien01 (Mật khẩu: Demo@123)";
            this.lblDemoHint.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            this.lblDemoHint.ForeColor = ColorTranslator.FromHtml("#64748B");
            this.lblDemoHint.TextAlign = ContentAlignment.MiddleCenter;

            // 11. Footer Copyright
            this.lblFooterCopyright.Text = "Hệ thống Quản lý Hóa đơn © 2026 • Đồ án Lập trình Windows";
            this.lblFooterCopyright.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            this.lblFooterCopyright.ForeColor = ColorTranslator.FromHtml("#94A3B8");
            this.lblFooterCopyright.Location = new Point(0, 500);
            this.lblFooterCopyright.Size = new Size(430, 24);
            this.lblFooterCopyright.TextAlign = ContentAlignment.MiddleCenter;

            this.pnlThongBaoLoi.Controls.Add(this.lblThongBaoLoi);
            this.pnlDemoHint.Controls.Add(this.lblDemoHint);

            this.pnlFormContainer.Controls.Add(this.lblTieuDe);
            this.pnlFormContainer.Controls.Add(this.lblMoTa);
            this.pnlFormContainer.Controls.Add(this.pnlThongBaoLoi);
            this.pnlFormContainer.Controls.Add(this.lblLabelTenDangNhap);
            this.pnlFormContainer.Controls.Add(this.txtTenDangNhap);
            this.pnlFormContainer.Controls.Add(this.lblLabelMatKhau);
            this.pnlFormContainer.Controls.Add(this.txtMatKhau);
            this.pnlFormContainer.Controls.Add(this.lblQuenMatKhau);
            this.pnlFormContainer.Controls.Add(this.btnDangNhap);
            this.pnlFormContainer.Controls.Add(this.pnlDemoHint);
            this.pnlFormContainer.Controls.Add(this.lblFooterCopyright);

            this.pnlRight.Controls.Add(this.pnlFormContainer);

            // =========================================================
            // FORM CHÍNH
            // =========================================================
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 680);
            this.MinimumSize = new Size(1100, 680);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormDangNhap";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập - Hệ Thống Quản Lý Hóa Đơn";

            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);

            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlLogoBadge.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlFormContainer.ResumeLayout(false);
            this.pnlFormContainer.PerformLayout();
            this.pnlThongBaoLoi.ResumeLayout(false);
            this.pnlDemoHint.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
