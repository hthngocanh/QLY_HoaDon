using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;

namespace QuanLyHoaDon.Utilities
{
    /// <summary>
    /// Hệ thống thiết kế chuẩn hóa toàn bộ ứng dụng Quản lý hóa đơn
    /// Phong cách: Modern Desktop Enterprise. Bảng màu: Xanh dương chủ đạo + Cam làm điểm nhấn
    /// </summary>
    public static class ThemeHelper
    {
        #region 1. BẢNG MÀU CHUẨN (APP COLORS)

        // Màu chính (Brand & Primary Actions)
        public static readonly Color MauChinh = ColorTranslator.FromHtml("#2563EB");           // Blue 600
        public static readonly Color MauChinhHover = ColorTranslator.FromHtml("#1D4ED8");      // Blue 700
        public static readonly Color MauChinhNhe = ColorTranslator.FromHtml("#E6F0FF");        // Blue nhạt

        // Màu nhấn (Accent) - dùng ÍT, đúng chỗ: nút hành động quan trọng, số liệu cần chú ý
        public static readonly Color MauNhan = ColorTranslator.FromHtml("#F59E0B");            // Cam
        public static readonly Color MauNhanHover = ColorTranslator.FromHtml("#D97706");       // Cam đậm
        public static readonly Color MauNhanNhe = ColorTranslator.FromHtml("#FFF7E6");         // Cam nhạt

        // Nền ứng dụng & Card (Surfaces)
        public static readonly Color MauNenUngDung = ColorTranslator.FromHtml("#F3F6FB");      // Xám xanh rất nhạt
        public static readonly Color MauNenNoiDung = ColorTranslator.FromHtml("#FFFFFF");      // White
        public static readonly Color MauNenInputReadOnly = ColorTranslator.FromHtml("#EEF3FB");
        public static readonly Color MauTieuDeBang = ColorTranslator.FromHtml("#DBE7F8");      // Tiêu đề bảng xanh nhạt

        // Chữ (Typography Colors) - đậm hơn để dễ đọc khi chiếu
        public static readonly Color MauChuChinh = ColorTranslator.FromHtml("#0F2A43");        // Navy gần đen
        public static readonly Color MauChuPhu = ColorTranslator.FromHtml("#3F5470");          // Xám xanh đậm
        public static readonly Color MauChuPlaceholder = ColorTranslator.FromHtml("#94A3B8");  // Slate 400

        // Viền (Borders & Dividers)
        public static readonly Color MauDuongVien = ColorTranslator.FromHtml("#D5DFEE");
        public static readonly Color MauDuongVienNhe = ColorTranslator.FromHtml("#EAF0F8");
        public static readonly Color MauDuongVienFocus = MauChinh;

        // Trạng thái (Feedback Colors)
        public static readonly Color MauThanhCong = ColorTranslator.FromHtml("#16A34A");       // Green 600
        public static readonly Color MauThanhCongHover = ColorTranslator.FromHtml("#15803D");  // Green 700
        public static readonly Color MauThanhCongNhe = ColorTranslator.FromHtml("#F0FDF4");    // Green 50

        public static readonly Color MauCanhBao = ColorTranslator.FromHtml("#D97706");         // Amber 600
        public static readonly Color MauCanhBaoHover = ColorTranslator.FromHtml("#B45309");    // Amber 700
        public static readonly Color MauCanhBaoNhe = ColorTranslator.FromHtml("#FFFBEB");      // Amber 50

        public static readonly Color MauLoi = ColorTranslator.FromHtml("#DC2626");             // Red 600
        public static readonly Color MauLoiHover = ColorTranslator.FromHtml("#B91C1C");         // Red 700
        public static readonly Color MauLoiNhe = ColorTranslator.FromHtml("#FEF2F2");           // Red 50

        public static readonly Color MauTim = ColorTranslator.FromHtml("#7C3AED");             // Violet 600
        public static readonly Color MauTimNhe = ColorTranslator.FromHtml("#F5F3FF");           // Violet 50

        // Sidebar
        public static readonly Color MauSidebar = Color.White;
        public static readonly Color MauSidebarHover = ColorTranslator.FromHtml("#EAF1FC");
        public static readonly Color MauSidebarActive = ColorTranslator.FromHtml("#D6E4FA");   // Đậm hơn bản cũ để nổi bật
        public static readonly Color MauSidebarBorder = MauDuongVien;
        public static readonly Color MauSidebarHeader = Color.White;
        public static readonly Color MauSidebarAccount = ColorTranslator.FromHtml("#F3F6FB");
        public static readonly Color MauSidebarAccountHover = ColorTranslator.FromHtml("#E6EEF9");
        public static readonly Color MauChuTrenSidebar = MauChuChinh;
        public static readonly Color MauChuSidebar = MauChuChinh;
        public static readonly Color MauChuPhuSidebar = MauChuPhu;
        public static readonly Color MauChuSidebarPhu = MauChuPhu;

        // KPI
        public static readonly Color MauKpiXanhDuong = MauChinh;
        public static readonly Color MauKpiXanhLa = MauThanhCong;
        public static readonly Color MauKpiCam = MauNhan;          // Cam nhấn cho số liệu cần chú ý
        public static readonly Color MauKpiTim = MauTim;

        // Trạng thái hóa đơn
        public static readonly Color MauDaXacNhan = MauThanhCong;
        public static readonly Color MauDangLap = MauCanhBao;
        public static readonly Color MauDaHuy = MauLoi;

        // Nút chức năng nổi bật
        public static readonly Color MauNutXuatExcel = MauThanhCong;
        public static readonly Color MauNutIn = MauNhan;
        public static readonly Color MauNutLamMoi = MauChuPhu;

        // Huy chương Top (cho UCThongKe)
        public static readonly Color MauVang = ColorTranslator.FromHtml("#D4A017");
        public static readonly Color MauBac = ColorTranslator.FromHtml("#8A97A8");
        public static readonly Color MauDong = ColorTranslator.FromHtml("#B8672E");

        // Nút phụ & vô hiệu hóa
        public static readonly Color MauNutPhu = Color.White;
        public static readonly Color MauNutPhuHover = ColorTranslator.FromHtml("#EEF3FB");
        public static readonly Color MauVienNutPhu = MauDuongVien;
        public static readonly Color MauNutNguyHiem = MauLoi;
        public static readonly Color MauNutNguyHiemHover = MauLoiHover;
        public static readonly Color MauVoHieuHoa = ColorTranslator.FromHtml("#94A3B8");
        public static readonly Color MauNenVoHieuHoa = ColorTranslator.FromHtml("#E2E8F0");

        // Alias tương thích ngược
        public static readonly Color MauTieuDe = MauChinh;
        public static readonly Color MauChuDao = MauSidebar;
        public static readonly Color MauThaoTacChinh = MauChinh;
        public static readonly Color MauNen = MauNenUngDung;
        public static readonly Color MauNenCard = MauNenNoiDung;
        public static readonly Color MauNguyHiem = MauLoi;

        #endregion

        #region 2. TYPOGRAPHY (APP FONTS)

        public const string TenFontMacDinh = "Segoe UI";

        public static readonly Font FontTieuDeTrang = new Font(TenFontMacDinh, 16f, FontStyle.Bold);       // Page Title 16pt Bold
        public static readonly Font FontTieuDeKhuVuc = new Font(TenFontMacDinh, 13f, FontStyle.Bold);     // Section Title 13pt Bold
        public static readonly Font FontTieuDeCard = new Font(TenFontMacDinh, 12f, FontStyle.Bold);        // Card Title 12pt Bold
        public static readonly Font FontNoiDung = new Font(TenFontMacDinh, 10.5f, FontStyle.Regular);       // Body 10.5pt Regular
        public static readonly Font FontNoiDungDam = new Font(TenFontMacDinh, 10.5f, FontStyle.Bold);      // Body Bold 10.5pt Bold
        public static readonly Font FontLabel = new Font(TenFontMacDinh, 10.5f, FontStyle.Bold);           // Label 10.5pt Bold
        public static readonly Font FontButton = new Font(TenFontMacDinh, 10.5f, FontStyle.Bold);          // Button 10.5pt Bold
        public static readonly Font FontBangDuLieu = new Font(TenFontMacDinh, 10.5f, FontStyle.Regular);   // Table Data 10.5pt
        public static readonly Font FontBangTieuDeCot = new Font(TenFontMacDinh, 10.5f, FontStyle.Bold);   // Table Header 10.5pt Bold
        public static readonly Font FontChuThich = new Font(TenFontMacDinh, 9.5f, FontStyle.Regular);      // Caption 9.5pt Regular
        public static readonly Font FontChuThichDam = new Font(TenFontMacDinh, 9.5f, FontStyle.Bold);     // Caption Bold 9.5pt Bold

        public static Font TaoFont(float kichThuocPt, FontStyle kieuFont = FontStyle.Regular)
        {
            return new Font(TenFontMacDinh, kichThuocPt, kieuFont);
        }

        #endregion

        #region 3. RADIUS & SPACING (APP RADIUS & SPACING)

        // Bo góc chuẩn
        public const int RadiusSmall = 6;      // Tag, Badge, Small button
        public const int RadiusMedium = 8;     // Input, Standard button, Dropdown
        public const int RadiusLarge = 12;     // Panel, Card, Dialog

        // Kích thước chuẩn controls
        public const int ChieuCaoInputChuan = 40;
        public const int ChieuCaoButtonChuan = 40;
        public const int ChieuCaoButtonNho = 32;

        #endregion

        #region 4. QUY CHUẨN BUTTON (BO GÓC)

        public static void ApDungButtonChinh(Button btn)
        {
            ApDungDinhDangButton(btn, MauChinh, MauChinhHover, Color.White, 0, Color.Empty);
        }

        public static void ApDungButtonPhu(Button btn)
        {
            ApDungDinhDangButton(btn, MauNutPhu, MauNutPhuHover, MauChuChinh, 1, MauVienNutPhu);
        }

        public static void ApDungButtonThanhCong(Button btn)
        {
            ApDungDinhDangButton(btn, MauThanhCong, MauThanhCongHover, Color.White, 0, Color.Empty);
        }

        public static void ApDungButtonNguyHiem(Button btn)
        {
            ApDungDinhDangButton(btn, MauNutNguyHiem, MauNutNguyHiemHover, Color.White, 0, Color.Empty);
        }

        public static void ApDungButtonCanhBao(Button btn)
        {
            ApDungDinhDangButton(btn, MauCanhBao, MauCanhBaoHover, Color.White, 0, Color.Empty);
        }

        /// <summary>
        /// Nút nhấn màu cam (chữ tối để dễ đọc). Dùng cho 1 hành động nổi bật nhất của màn hình,
        /// ví dụ "Lập hóa đơn mới".
        /// </summary>
        public static void ApDungButtonNhan(Button btn)
        {
            ApDungDinhDangButton(btn, MauNhan, MauNhanHover, MauChuChinh, 0, Color.Empty);
        }

        private static void ApDungDinhDangButton(Button btn, Color mauNen, Color mauHover, Color mauChu, int doDayVien, Color mauVien)
        {
            btn.Height = ChieuCaoButtonChuan;
            btn.Font = FontButton;
            btn.ForeColor = mauChu;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.BackColor = mauNen;

            if (btn is RoundedButton rb)
            {
                rb.NormalColor = mauNen;
                rb.HoverColor = mauHover;
                rb.BorderRadius = RadiusMedium;
                rb.BorderWidth = doDayVien;
                rb.BorderColor = doDayVien > 0 ? mauVien : Color.Transparent;
                rb.TextColor = mauChu;
                return;
            }

            // Button thường
            btn.FlatAppearance.BorderSize = doDayVien;
            if (doDayVien > 0)
            {
                btn.FlatAppearance.BorderColor = mauVien;
            }
            btn.Padding = new Padding(16, 0, 16, 0);

            btn.MouseEnter += (s, e) => { if (btn.Enabled) btn.BackColor = mauHover; };
            btn.MouseLeave += (s, e) => { if (btn.Enabled) btn.BackColor = mauNen; };
            btn.EnabledChanged += (s, e) =>
            {
                if (btn.Enabled)
                {
                    btn.BackColor = mauNen;
                    btn.ForeColor = mauChu;
                    btn.Cursor = Cursors.Hand;
                }
                else
                {
                    btn.BackColor = MauNenVoHieuHoa;
                    btn.ForeColor = MauVoHieuHoa;
                    btn.Cursor = Cursors.Default;
                }
            };
        }

        public static Button TaoButtonChinh(string text, int chieuRong = 140, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonChinh(btn);
            return btn;
        }

        public static Button TaoButtonPhu(string text, int chieuRong = 140, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonPhu(btn);
            return btn;
        }

        public static Button TaoButtonThanhCong(string text, int chieuRong = 140, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonThanhCong(btn);
            return btn;
        }

        public static Button TaoButtonNguyHiem(string text, int chieuRong = 140, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonNguyHiem(btn);
            return btn;
        }

        public static Button TaoButtonNhan(string text, int chieuRong = 140, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonNhan(btn);
            return btn;
        }

        #endregion

        #region 5. CHUẨN HÓA BẢNG DỮ LIỆU (DATAGRIDVIEW)

        public static void ChuanHoaDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = MauNenNoiDung;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = MauDuongVien;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBangTieuDeCot;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = MauTieuDeBang;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = MauChuChinh;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = MauTieuDeBang;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = MauChuChinh;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 10, 12, 10);
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle.Font = FontBangDuLieu;
            dgv.DefaultCellStyle.BackColor = MauNenNoiDung;
            dgv.DefaultCellStyle.ForeColor = MauChuChinh;
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
            // Dòng đang chọn: nền xanh nhạt đậm hơn, chữ vẫn đen để dễ đọc khi chiếu
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#CFE0FA");
            dgv.DefaultCellStyle.SelectionForeColor = MauChuChinh;
            dgv.RowTemplate.Height = 40;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F5F9FF");
        }

        #endregion

        #region 6. CHUẨN HÓA Ô NHẬP LIỆU (INPUTS)

        public static void ChuanHoaTextBox(TextBox txt, bool laReadOnly = false)
        {
            txt.Font = FontNoiDung;
            txt.BorderStyle = BorderStyle.FixedSingle;
            if (laReadOnly)
            {
                txt.ReadOnly = true;
                txt.BackColor = MauNenInputReadOnly;
                txt.ForeColor = MauChuChinh;
                txt.Cursor = Cursors.Default;
            }
            else
            {
                txt.ReadOnly = false;
                txt.BackColor = Color.White;
                txt.ForeColor = MauChuChinh;
            }
        }

        public static void ChuanHoaComboBox(ComboBox cbo)
        {
            cbo.Font = FontNoiDung;
            cbo.BackColor = Color.White;
            cbo.ForeColor = MauChuChinh;
            cbo.FlatStyle = FlatStyle.Flat;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #endregion

        #region 7. VẼ ĐƯỜNG BO GÓC & CARD

        public static GraphicsPath TaoDuongBoGoc(RectangleF rect, int banKinh)
        {
            float d = banKinh * 2f;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void VeCardBoGoc(Graphics g, Rectangle rect, int banKinh = RadiusLarge, Color? mauNen = null, Color? mauVien = null, int doDayVien = 1)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rectF = new RectangleF(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            using var path = TaoDuongBoGoc(rectF, banKinh);

            using var brush = new SolidBrush(mauNen ?? MauNenNoiDung);
            g.FillPath(brush, path);

            if (doDayVien > 0)
            {
                using var pen = new Pen(mauVien ?? MauDuongVien, doDayVien);
                g.DrawPath(pen, path);
            }
        }

        #endregion
        #region 8. BO GÓC NÚT THƯỜNG (BUTTON)

        private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Button, object> _nutDaBoGoc = new();

        public static void BoGocTatCaNut(Control goc, int banKinh = RadiusMedium)
        {
            foreach (Control c in goc.Controls)
            {
                if (c is Button b && c is not RoundedButton && !_nutDaBoGoc.TryGetValue(b, out _))
                {
                    _nutDaBoGoc.Add(b, new object());
                    b.Region = null;
                    b.Paint += (s, e) => VeBoGocMuot(b, e.Graphics, banKinh);
                    b.Invalidate();
                }
                if (c.HasChildren) BoGocTatCaNut(c, banKinh);
            }
        }

        private static void VeBoGocMuot(Button b, Graphics g, int banKinh)
        {
            int w = b.Width, h = b.Height;
            if (w <= 4 || h <= 4) return;

            Color nenCha = SystemColors.Control;
            for (Control? c = b.Parent; c != null; c = c.Parent)
            {
                if (c.BackColor.A == 255) { nenCha = c.BackColor; break; }
            }

            int r = Math.Min(banKinh, Math.Min(w, h) / 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var boBen = TaoDuongBoGoc(new RectangleF(0, 0, w, h), r))
            using (var vungNgoai = new GraphicsPath())
            using (var brush = new SolidBrush(nenCha))
            {
                vungNgoai.AddRectangle(new RectangleF(-2, -2, w + 4, h + 4));
                vungNgoai.AddPath(boBen, false);
                g.FillPath(brush, vungNgoai);
            }

            int doDayVien = b.FlatAppearance.BorderSize;
            if (doDayVien > 0)
            {
                Color mauVien = b.FlatAppearance.BorderColor.IsEmpty ? MauDuongVien : b.FlatAppearance.BorderColor;
                float nua = doDayVien / 2f;
                using var duongVien = TaoDuongBoGoc(new RectangleF(nua, nua, w - doDayVien, h - doDayVien), Math.Max(1, r - 1));
                using var pen = new Pen(mauVien, doDayVien);
                g.DrawPath(pen, duongVien);
            }
        }

        #endregion
    }
}
