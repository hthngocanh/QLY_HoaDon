using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;

namespace QuanLyHoaDon.Utilities
{
    /// <summary>
    /// Hệ thống thiết kế chuẩn hóa toàn bộ ứng dụng Quản lý hóa đơn
    /// Định hướng: Light Corporate - tông slate + teal, dịu mắt nhưng vẫn tương phản tốt khi chiếu
    /// </summary>
    public static class ThemeHelper
    {
        #region 1. BẢNG MÀU CHUẨN

        // Sidebar: xanh slate dịu hơn navy cũ
        public static readonly Color MauSidebar = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color MauSidebarHover = ColorTranslator.FromHtml("#F0F5FC");
        public static readonly Color MauSidebarActive = ColorTranslator.FromHtml("#E6EEFC");
        public static readonly Color MauSidebarBorder = ColorTranslator.FromHtml("#E3EAF4");
        public static readonly Color MauSidebarHeader = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color MauSidebarAccount = ColorTranslator.FromHtml("#F3F6FB");
        public static readonly Color MauSidebarAccountHover = ColorTranslator.FromHtml("#E8EEF8");

        // Nền ứng dụng & Card
        public static readonly Color MauNenUngDung = ColorTranslator.FromHtml("#F5F8FD");
        public static readonly Color MauNenNoiDung = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color MauNenInputReadOnly = ColorTranslator.FromHtml("#EEF4FB");
        public static readonly Color MauTieuDeBang = ColorTranslator.FromHtml("#E8F1FB");

        // Chữ
        public static readonly Color MauChuChinh = ColorTranslator.FromHtml("#0F2A43");
        public static readonly Color MauChuPhu = ColorTranslator.FromHtml("#4A6076");
        public static readonly Color MauChuTrenSidebar = ColorTranslator.FromHtml("#1F2D3D");
        public static readonly Color MauChuSidebar = ColorTranslator.FromHtml("#1F2D3D");
        public static readonly Color MauChuPhuSidebar = ColorTranslator.FromHtml("#64748B");
        public static readonly Color MauChuSidebarPhu = MauChuPhuSidebar;

        // Viền
        public static readonly Color MauDuongVien = ColorTranslator.FromHtml("#D6E4F5");
        public static readonly Color MauDuongVienFocus = ColorTranslator.FromHtml("#2563EB");

        // Màu chức năng
        public static readonly Color MauChinh = ColorTranslator.FromHtml("#2563EB");
        public static readonly Color MauChinhHover = ColorTranslator.FromHtml("#1D4ED8");
        public static readonly Color MauTieuDe = MauChinh;
        public static readonly Color MauThanhCong = ColorTranslator.FromHtml("#2E7D32");
        public static readonly Color MauThanhCongHover = ColorTranslator.FromHtml("#1B5E20");
        public static readonly Color MauCanhBao = ColorTranslator.FromHtml("#B45309");
        public static readonly Color MauCanhBaoHover = ColorTranslator.FromHtml("#92400E");
        public static readonly Color MauLoi = ColorTranslator.FromHtml("#B91C1C");
        public static readonly Color MauLoiHover = ColorTranslator.FromHtml("#991B1B");

        // KPI
        public static readonly Color MauKpiXanhDuong = MauChinh;
        public static readonly Color MauKpiXanhLa = MauThanhCong;
        public static readonly Color MauKpiCam = MauCanhBao;

        // Trạng thái hóa đơn
        public static readonly Color MauDaXacNhan = MauThanhCong;
        public static readonly Color MauDangLap = MauCanhBao;
        public static readonly Color MauDaHuy = MauLoi;

        // Nút chức năng nổi bật
        public static readonly Color MauNutXuatExcel = MauThanhCong;
        public static readonly Color MauNutIn = MauCanhBao;
        public static readonly Color MauNutLamMoi = MauChuPhu;

        // Huy chương Top
        public static readonly Color MauVang = ColorTranslator.FromHtml("#D4A017");
        public static readonly Color MauBac = ColorTranslator.FromHtml("#8A97A8");
        public static readonly Color MauDong = ColorTranslator.FromHtml("#B8672E");

        // Nút phụ & vô hiệu hóa
        public static readonly Color MauNutPhu = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color MauNutPhuHover = ColorTranslator.FromHtml("#E8F1FB");
        public static readonly Color MauVienNutPhu = ColorTranslator.FromHtml("#BBD3EE");
        public static readonly Color MauNutNguyHiem = MauLoi;
        public static readonly Color MauNutNguyHiemHover = MauLoiHover;
        public static readonly Color MauVoHieuHoa = ColorTranslator.FromHtml("#94A3B8");
        public static readonly Color MauNenVoHieuHoa = ColorTranslator.FromHtml("#E3ECF7");

        // Alias cũ
        public static readonly Color MauChuDao = MauSidebar;
        public static readonly Color MauThaoTacChinh = MauChinh;
        public static readonly Color MauNen = MauNenUngDung;
        public static readonly Color MauNenCard = MauNenNoiDung;
        public static readonly Color MauNguyHiem = MauLoi;

        #endregion

        #region 2. TYPOGRAPHY (SEGOE UI)

        public const string TenFontMacDinh = "Segoe UI";

        public static readonly Font FontTieuDeTrang = new Font(TenFontMacDinh, 18.5f, FontStyle.Bold);
        public static readonly Font FontTieuDeKhuVuc = new Font(TenFontMacDinh, 14.5f, FontStyle.Bold);
        public static readonly Font FontTieuDeCard = new Font(TenFontMacDinh, 12.5f, FontStyle.Bold);
        public static readonly Font FontNoiDung = new Font(TenFontMacDinh, 11.25f, FontStyle.Regular);
        public static readonly Font FontNoiDungDam = new Font(TenFontMacDinh, 11.25f, FontStyle.Bold);
        public static readonly Font FontLabel = new Font(TenFontMacDinh, 11.25f, FontStyle.Bold);
        public static readonly Font FontButton = new Font(TenFontMacDinh, 11.25f, FontStyle.Bold);
        public static readonly Font FontBangDuLieu = new Font(TenFontMacDinh, 11f, FontStyle.Regular);
        public static readonly Font FontBangTieuDeCot = new Font(TenFontMacDinh, 11f, FontStyle.Bold);
        public static readonly Font FontChuThich = new Font(TenFontMacDinh, 10f, FontStyle.Regular);
        public static readonly Font FontChuThichDam = new Font(TenFontMacDinh, 10f, FontStyle.Bold);

        public static Font TaoFont(float kichThuocPt, FontStyle kieuFont = FontStyle.Regular)
        {
            return new Font(TenFontMacDinh, kichThuocPt, kieuFont);
        }

        #endregion

        #region 3. QUY CHUẨN BUTTON (BO GÓC)

        public const int ChieuCaoButtonChuan = 42;

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
                // RoundedButton tự xử lý hover/pressed/disabled
                rb.HoverColor = mauHover;
                rb.BorderRadius = 10;
                rb.BorderWidth = doDayVien;
                rb.BorderColor = doDayVien > 0 ? mauVien : Color.Transparent;
                return;
            }

            // Button thường: giữ cách cũ
            btn.FlatAppearance.BorderSize = doDayVien;
            if (doDayVien > 0)
            {
                btn.FlatAppearance.BorderColor = mauVien;
            }
            btn.Padding = new Padding(18, 0, 18, 0);

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

        public static Button TaoButtonChinh(string text, int chieuRong = 150, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonChinh(btn);
            return btn;
        }

        public static Button TaoButtonPhu(string text, int chieuRong = 150, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonPhu(btn);
            return btn;
        }

        public static Button TaoButtonThanhCong(string text, int chieuRong = 150, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonThanhCong(btn);
            return btn;
        }

        public static Button TaoButtonNguyHiem(string text, int chieuRong = 150, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new RoundedButton { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonNguyHiem(btn);
            return btn;
        }

        #endregion

        #region 4. CHUẨN HÓA BẢNG DỮ LIỆU

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
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E8F1FB");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = MauChuChinh;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 10, 10, 10);
            dgv.ColumnHeadersHeight = 44;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle.Font = FontBangDuLieu;
            dgv.DefaultCellStyle.BackColor = MauNenNoiDung;
            dgv.DefaultCellStyle.ForeColor = MauChuChinh;
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#DCEBFA");
            dgv.DefaultCellStyle.SelectionForeColor = MauChuChinh;
            dgv.RowTemplate.Height = 38;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F7FAFE");
        }

        #endregion

        #region 5. CHUẨN HÓA Ô NHẬP LIỆU

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

        #region 6. THẺ THỐNG KÊ & CARD

        public static void VeVienCard(Graphics g, Rectangle bounds)
        {
            using var pen = new Pen(MauDuongVien, 1);
            g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
        }

        #endregion

        #region 7. BO GÓC MƯỢT CHO NÚT THƯỜNG (Button)

        private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Button, object> _nutDaBoGoc = new();

        /// <summary>
        /// Bo góc mọi Button thường (không phải RoundedButton) trong control gốc, kể cả trong Tab/Panel.
        /// Dùng vẽ chồng có khử răng cưa thay vì cắt Region nên mép nhẵn và giữ nguyên viền.
        /// </summary>
        public static void BoGocTatCaNut(Control goc, int banKinh = 10)
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

            // Màu nền thật của control cha
            Color nenCha = SystemColors.Control;
            for (Control? c = b.Parent; c != null; c = c.Parent)
            {
                if (c.BackColor.A == 255) { nenCha = c.BackColor; break; }
            }

            int r = Math.Min(banKinh, Math.Min(w, h) / 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Tô phần góc ngoài hình bo bằng màu nền cha (khử răng cưa ở mép)
            using (var boBen = TaoDuongBoGoc(new RectangleF(0, 0, w, h), r))
            using (var vungNgoai = new GraphicsPath())
            using (var brush = new SolidBrush(nenCha))
            {
                vungNgoai.AddRectangle(new RectangleF(-2, -2, w + 4, h + 4));
                vungNgoai.AddPath(boBen, false);
                g.FillPath(brush, vungNgoai);
            }

            // Vẽ lại viền theo đường bo
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

        private static GraphicsPath TaoDuongBoGoc(RectangleF rect, int banKinh)
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

        #endregion
    }
}