using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Controls
{
    public class UCDangPhatTrien : UserControl
    {
        private Panel pnlCard = null!;
        private Label lblIcon = null!;
        private Label lblTieuDe = null!;
        private Label lblBadge = null!;
        private Label lblPhuTrach = null!;
        private Label lblMoTa = null!;
        private Label lblHuongDanTichHop = null!;

        public UCDangPhatTrien(string tieuDe, string nguoiPhuTrach, string moTa, string iconKyHieu = "📦")
        {
            KhoiTaoGiaoDien(tieuDe, nguoiPhuTrach, moTa, iconKyHieu);
        }

        private void KhoiTaoGiaoDien(string tieuDe, string nguoiPhuTrach, string moTa, string iconKyHieu)
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.TaoFont(9.5f);

            pnlCard = new Panel
            {
                Size = new Size(820, 480),
                BackColor = ThemeHelper.MauNenNoiDung,
                Location = new Point(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            pnlCard.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            };

            lblIcon = new Label
            {
                Text = iconKyHieu,
                Font = new Font("Segoe UI Emoji", 48f),
                Size = new Size(100, 90),
                Location = new Point(40, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblBadge = new Label
            {
                Text = "ĐANG PHÁT TRIỂN / CHỜ TÍCH HỢP",
                Font = ThemeHelper.TaoFont(8.5f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChinh,
                BackColor = Color.FromArgb(239, 246, 255),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(160, 45)
            };

            lblTieuDe = new Label
            {
                Text = tieuDe,
                Font = ThemeHelper.TaoFont(18f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(158, 80)
            };

            lblPhuTrach = new Label
            {
                Text = "👤 Thành viên phụ trách: " + nguoiPhuTrach,
                Font = ThemeHelper.TaoFont(10.5f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuPhu,
                AutoSize = true,
                Location = new Point(160, 125)
            };

            lblMoTa = new Label
            {
                Text = moTa,
                Font = ThemeHelper.TaoFont(10f),
                ForeColor = ThemeHelper.MauChuPhu,
                Size = new Size(620, 60),
                Location = new Point(160, 165)
            };

            var pnlBoxHuongDan = new Panel
            {
                Location = new Point(40, 250),
                Size = new Size(740, 180),
                BackColor = Color.FromArgb(248, 250, 252),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlBoxHuongDan.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBoxHuongDan.Width - 1, pnlBoxHuongDan.Height - 1);
            };

            var lblTieuDeHuongDan = new Label
            {
                Text = "📌 Hướng dẫn tích hợp cho thành viên phụ trách phân hệ này:",
                Font = ThemeHelper.TaoFont(10f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 18),
                AutoSize = true
            };

            lblHuongDanTichHop = new Label
            {
                Text = "1. Xây dựng Form nghiệp vụ hoặc UserControl trong thư mục GUI/Forms tương ứng.\n" +
                       "2. Tận dụng DTO, BLL, DAL đã được phân tầng chuẩn để truy xuất cơ sở dữ liệu.\n" +
                       "3. Gọi phương thức FormChinh.DieuHuong(yourControl, tieuDe, breadcrumb) để hiển thị trong khu vực chính.\n" +
                       "4. Không cần tạo lại FormChinh mới hay can thiệp vào cơ chế đăng nhập và phân quyền.",
                Font = ThemeHelper.TaoFont(9.5f),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(20, 50),
                Size = new Size(700, 110)
            };

            pnlBoxHuongDan.Controls.Add(lblTieuDeHuongDan);
            pnlBoxHuongDan.Controls.Add(lblHuongDanTichHop);

            pnlCard.Controls.Add(lblIcon);
            pnlCard.Controls.Add(lblBadge);
            pnlCard.Controls.Add(lblTieuDe);
            pnlCard.Controls.Add(lblPhuTrach);
            pnlCard.Controls.Add(lblMoTa);
            pnlCard.Controls.Add(pnlBoxHuongDan);

            this.Controls.Add(pnlCard);
        }
    }
}
