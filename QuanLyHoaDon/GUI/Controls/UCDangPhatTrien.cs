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
            this.Font = ThemeHelper.FontNoiDung;

            pnlCard = new Panel
            {
                Size = new Size(880, 520),
                BackColor = ThemeHelper.MauNenNoiDung,
                Location = new Point(36, 30),
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
                Location = new Point(40, 36),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblBadge = new Label
            {
                Text = "ĐANG PHÁT TRIỂN / CHỜ TÍCH HỢP",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChinh,
                BackColor = ColorTranslator.FromHtml("#EFF6FF"),
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Location = new Point(160, 40)
            };

            lblTieuDe = new Label
            {
                Text = tieuDe,
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Location = new Point(158, 76)
            };

            lblPhuTrach = new Label
            {
                Text = "👤 Thành viên phụ trách: " + nguoiPhuTrach,
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuPhu,
                AutoSize = true,
                Location = new Point(160, 118)
            };

            lblMoTa = new Label
            {
                Text = moTa,
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuPhu,
                Size = new Size(680, 56),
                Location = new Point(160, 154)
            };

            var pnlBoxHuongDan = new Panel
            {
                Location = new Point(40, 230),
                Size = new Size(800, 250),
                BackColor = ColorTranslator.FromHtml("#F8FAFC"),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlBoxHuongDan.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBoxHuongDan.Width - 1, pnlBoxHuongDan.Height - 1);
            };

            var lblTieuDeHuongDan = new Label
            {
                Text = "📌 Hướng dẫn tích hợp cho thành viên phụ trách phân hệ này:",
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 18),
                AutoSize = true
            };

            lblHuongDanTichHop = new Label
            {
                Text = "1. Xây dựng Form nghiệp vụ hoặc UserControl trong thư mục GUI/Forms tương ứng.\n" +
                       "2. Tận dụng DTO, BLL, DAL đã được phân tầng chuẩn để truy xuất cơ sở dữ liệu.\n" +
                       "3. Gọi phương thức FormChinh.DieuHuong(yourControl, tieuDe, breadcrumb) để hiển thị trong khu vực chính.\n" +
                       "4. Áp dụng ThemeHelper (FontBangDuLieu, ChuanHoaDataGridView, ApDungButtonChinh...) để giao diện đồng bộ toàn hệ thống.\n" +
                       "5. Không cần tạo lại FormChinh mới hay can thiệp vào cơ chế đăng nhập và phân quyền.",
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(20, 52),
                Size = new Size(760, 175)
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
