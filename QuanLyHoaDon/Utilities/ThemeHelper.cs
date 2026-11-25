using System.Drawing;

namespace QuanLyHoaDon.Utilities
{
    public static class ThemeHelper
    {
        // Bang mau Corporate hien dai theo dac ta
        public static readonly Color MauSidebar = ColorTranslator.FromHtml("#172B4D");
        public static readonly Color MauSidebarHover = ColorTranslator.FromHtml("#243B68");
        public static readonly Color MauSidebarActive = ColorTranslator.FromHtml("#2563EB");

        public static readonly Color MauChinh = ColorTranslator.FromHtml("#2563EB");
        public static readonly Color MauChinhHover = ColorTranslator.FromHtml("#1D4ED8");

        public static readonly Color MauNenUngDung = ColorTranslator.FromHtml("#F4F7FB");
        public static readonly Color MauNenNoiDung = ColorTranslator.FromHtml("#FFFFFF");

        public static readonly Color MauChuChinh = ColorTranslator.FromHtml("#1E293B");
        public static readonly Color MauChuPhu = ColorTranslator.FromHtml("#64748B");
        public static readonly Color MauDuongVien = ColorTranslator.FromHtml("#E2E8F0");

        public static readonly Color MauThanhCong = ColorTranslator.FromHtml("#16A34A");
        public static readonly Color MauCanhBao = ColorTranslator.FromHtml("#D97706");
        public static readonly Color MauLoi = ColorTranslator.FromHtml("#DC2626");

        // Giu nguyen cac alias tuong thich code cu
        public static readonly Color MauChuDao = MauSidebar;
        public static readonly Color MauThaoTacChinh = MauChinh;
        public static readonly Color MauNen = MauNenUngDung;
        public static readonly Color MauNenCard = MauNenNoiDung;
        public static readonly Color MauNguyHiem = MauLoi;

        // Font chu mac dinh he thong
        public const string TenFontMacDinh = "Segoe UI";

        public static Font TaoFont(float kichThuoc, FontStyle kieuFont = FontStyle.Regular)
        {
            return new Font(TenFontMacDinh, kichThuoc, kieuFont);
        }
    }
}
