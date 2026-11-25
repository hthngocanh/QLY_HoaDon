using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyHoaDon.Utilities
{
    /// <summary>
    /// Hệ thống thiết kế chuẩn hóa toàn bộ ứng dụng Quản lý hóa đơn
    /// Định hướng: Light Corporate - High Contrast (Tối ưu trình chiếu máy chiếu giảng đường)
    /// </summary>
    public static class ThemeHelper
    {
        #region 1. BẢNG MÀU CHUẨN (LIGHT CORPORATE - HIGH CONTRAST)

        // Sidebar: Xanh Navy chuẩn, sáng rõ, tương phản cao với chữ trắng
        public static readonly Color MauSidebar = ColorTranslator.FromHtml("#1E3A5F");
        public static readonly Color MauSidebarHover = ColorTranslator.FromHtml("#2A4E7F");
        public static readonly Color MauSidebarActive = ColorTranslator.FromHtml("#1D4ED8");
        public static readonly Color MauSidebarBorder = ColorTranslator.FromHtml("#2E5385");
        public static readonly Color MauSidebarHeader = ColorTranslator.FromHtml("#152C48");
        public static readonly Color MauSidebarAccount = ColorTranslator.FromHtml("#152C48");
        public static readonly Color MauSidebarAccountHover = ColorTranslator.FromHtml("#23436D");

        // Nền ứng dụng & Card
        public static readonly Color MauNenUngDung = ColorTranslator.FromHtml("#F4F7FB"); // Nền xám nhạt dịu mắt
        public static readonly Color MauNenNoiDung = ColorTranslator.FromHtml("#FFFFFF"); // Card trắng nổi bật
        public static readonly Color MauNenInputReadOnly = ColorTranslator.FromHtml("#EDF2F7"); // Input chỉ xem rõ ràng
        public static readonly Color MauTieuDeBang = ColorTranslator.FromHtml("#EDF2F7");

        // Typography Colors (Tương phản tuyệt đối khi chiếu xa)
        public static readonly Color MauChuChinh = ColorTranslator.FromHtml("#172033"); // Đen xanh đậm cực sắc nét (Contrast > 12:1)
        public static readonly Color MauChuPhu = ColorTranslator.FromHtml("#475569");   // Xám trung tính đậm rõ ràng (Contrast > 7:1)
        public static readonly Color MauChuTrenSidebar = Color.White;                    // Trắng tuyết trên nền #1E3A5F
        public static readonly Color MauChuSidebar = Color.White;
        public static readonly Color MauChuPhuSidebar = ColorTranslator.FromHtml("#CBD5E1"); // Trắng bạc dịu mắt
        public static readonly Color MauChuSidebarPhu = MauChuPhuSidebar;

        // Đường viền (Borders & Dividers)
        public static readonly Color MauDuongVien = ColorTranslator.FromHtml("#CBD5E1");       // Viền chuẩn rõ nét
        public static readonly Color MauDuongVienFocus = ColorTranslator.FromHtml("#1D4ED8");  // Viền xanh đậm khi focus

        // Màu sắc chức năng (Functional Colors - Rõ ràng, không gây nhầm lẫn)
        public static readonly Color MauChinh = ColorTranslator.FromHtml("#1D4ED8");           // Primary: Xanh dương đậm
        public static readonly Color MauChinhHover = ColorTranslator.FromHtml("#1E40AF");      // Primary Hover
        public static readonly Color MauTieuDe = MauChinh;
        public static readonly Color MauThanhCong = ColorTranslator.FromHtml("#15803D");       // Success: Xanh lá cây đậm
        public static readonly Color MauThanhCongHover = ColorTranslator.FromHtml("#166534");  // Success Hover
        public static readonly Color MauCanhBao = ColorTranslator.FromHtml("#B45309");         // Warning: Cam cháy đậm
        public static readonly Color MauCanhBaoHover = ColorTranslator.FromHtml("#92400E");    // Warning Hover
        public static readonly Color MauLoi = ColorTranslator.FromHtml("#B91C1C");             // Danger: Đỏ cờ đậm
        public static readonly Color MauLoiHover = ColorTranslator.FromHtml("#991B1B");        // Danger Hover

        // KPI & Phân hệ Thống kê tương thích
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

        // Nút phụ (Secondary) & Nút vô hiệu hóa (Disabled)
        public static readonly Color MauNutPhu = ColorTranslator.FromHtml("#F8FAFC");
        public static readonly Color MauNutPhuHover = ColorTranslator.FromHtml("#E2E8F0");
        public static readonly Color MauVienNutPhu = ColorTranslator.FromHtml("#CBD5E1");
        public static readonly Color MauNutNguyHiem = MauLoi;
        public static readonly Color MauNutNguyHiemHover = MauLoiHover;
        public static readonly Color MauVoHieuHoa = ColorTranslator.FromHtml("#94A3B8");
        public static readonly Color MauNenVoHieuHoa = ColorTranslator.FromHtml("#E2E8F0");

        // Giữ tương thích ngược với các alias cũ
        public static readonly Color MauChuDao = MauSidebar;
        public static readonly Color MauThaoTacChinh = MauChinh;
        public static readonly Color MauNen = MauNenUngDung;
        public static readonly Color MauNenCard = MauNenNoiDung;
        public static readonly Color MauNguyHiem = MauLoi;

        #endregion

        #region 2. TYPOGRAPHY CHUẨN MÁY CHIẾU (SEGOE UI)

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

        #region 3. QUY CHUẨN BUTTON (40-44PX, SEMIBOLD/BOLD, TƯƠNG PHẢN CAO)

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

        private static void ApDungDinhDangButton(Button btn, Color mauNen, Color mauHover, Color mauChu, int doDayVien, Color mauVien)
        {
            btn.Height = ChieuCaoButtonChuan;
            btn.Font = FontButton;
            btn.BackColor = mauNen;
            btn.ForeColor = mauChu;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = doDayVien;
            if (doDayVien > 0)
            {
                btn.FlatAppearance.BorderColor = mauVien;
            }
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(18, 0, 18, 0);
            btn.TextAlign = ContentAlignment.MiddleCenter;

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
            var btn = new Button { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonChinh(btn);
            return btn;
        }

        public static Button TaoButtonPhu(string text, int chieuRong = 150, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new Button { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonPhu(btn);
            return btn;
        }

        public static Button TaoButtonThanhCong(string text, int chieuRong = 150, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new Button { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonThanhCong(btn);
            return btn;
        }

        public static Button TaoButtonNguyHiem(string text, int chieuRong = 150, int chieuCao = ChieuCaoButtonChuan)
        {
            var btn = new Button { Text = text, Size = new Size(chieuRong, chieuCao) };
            ApDungButtonNguyHiem(btn);
            return btn;
        }

        #endregion

        #region 4. CHUẨN HÓA BẢNG DỮ LIỆU (DATAGRIDVIEW CHO MÀN HÌNH MÁY CHIẾU)

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

            dgv.ColumnHeadersDefaultCellStyle.Font = FontBangTieuDeCot;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#EDF2F7");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = MauChuChinh;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 10, 10, 10);
            dgv.ColumnHeadersHeight = 44;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle.Font = FontBangDuLieu;
            dgv.DefaultCellStyle.BackColor = MauNenNoiDung;
            dgv.DefaultCellStyle.ForeColor = MauChuChinh;
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#DBEAFE");
            dgv.DefaultCellStyle.SelectionForeColor = MauChinh;
            dgv.RowTemplate.Height = 38;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
        }

        #endregion

        #region 5. CHUẨN HÓA Ô NHẬP LIỆU (TEXTBOX / COMBOBOX)

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

        #region 6. CHUẨN HÓA THẺ THỐNG KÊ & CARD

        public static void VeVienCard(Graphics g, Rectangle bounds)
        {
            using var pen = new Pen(MauDuongVien, 1);
            g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
        }

        #endregion
    }
}
