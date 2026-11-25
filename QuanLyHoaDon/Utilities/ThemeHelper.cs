using System.Drawing;

namespace QuanLyHoaDon.Utilities
{
    public static class ThemeHelper
    {
        // Bang mau chuan he thong
        public static readonly Color MauChuDao = ColorTranslator.FromHtml("#1E3A5F");
        public static readonly Color MauThaoTacChinh = ColorTranslator.FromHtml("#2563EB");
        public static readonly Color MauNen = ColorTranslator.FromHtml("#F8FAFC");
        public static readonly Color MauNenCard = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color MauChuChinh = ColorTranslator.FromHtml("#334155");
        public static readonly Color MauChuPhu = ColorTranslator.FromHtml("#64748B");
        public static readonly Color MauThanhCong = ColorTranslator.FromHtml("#16A34A");
        public static readonly Color MauCanhBao = ColorTranslator.FromHtml("#F59E0B");
        public static readonly Color MauNguyHiem = ColorTranslator.FromHtml("#DC2626");

        // Font chu mac dinh
        public const string TenFontMacDinh = "Segoe UI";

        public static Font TaoFont(float kichThuoc, FontStyle kieuFont = FontStyle.Regular)
        {
            return new Font(TenFontMacDinh, kichThuoc, kieuFont);
        }
    }
}

