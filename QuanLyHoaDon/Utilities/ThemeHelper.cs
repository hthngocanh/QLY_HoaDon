using System.Drawing;

namespace QuanLyHoaDon.Utilities
{
    public static class ThemeHelper
    {
        // ===== Bang mau xanh duong sang theo giao dien mau =====
        public static readonly Color MauSidebar = ColorTranslator.FromHtml("#2B7DE0");
        public static readonly Color MauSidebarHover = ColorTranslator.FromHtml("#3A8BEA");
        public static readonly Color MauSidebarActive = ColorTranslator.FromHtml("#1D5DB8");
        public static readonly Color MauChuSidebar = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color MauChuSidebarPhu = ColorTranslator.FromHtml("#CFE3FB");

        public static readonly Color MauChinh = ColorTranslator.FromHtml("#2B7DE0");
        public static readonly Color MauChinhHover = ColorTranslator.FromHtml("#1D5DB8");
        public static readonly Color MauTieuDe = ColorTranslator.FromHtml("#1D6FD1");

        public static readonly Color MauNenUngDung = ColorTranslator.FromHtml("#F1F5F9");
        public static readonly Color MauNenNoiDung = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color MauTieuDeBang = ColorTranslator.FromHtml("#E8F1FC");

        public static readonly Color MauChuChinh = ColorTranslator.FromHtml("#1E293B");
        public static readonly Color MauChuPhu = ColorTranslator.FromHtml("#64748B");
        public static readonly Color MauDuongVien = ColorTranslator.FromHtml("#E2E8F0");

        public static readonly Color MauThanhCong = ColorTranslator.FromHtml("#16955A");
        public static readonly Color MauCanhBao = ColorTranslator.FromHtml("#F59E0B");
        public static readonly Color MauLoi = ColorTranslator.FromHtml("#DC2626");

        // ===== Mau cho cac the so lieu (KPI) =====
        public static readonly Color MauKpiXanhDuong = ColorTranslator.FromHtml("#2563C9");
        public static readonly Color MauKpiXanhLa = ColorTranslator.FromHtml("#16955A");
        public static readonly Color MauKpiCam = ColorTranslator.FromHtml("#F59E0B");

        // ===== Mau theo trang thai hoa don =====
        public static readonly Color MauDaXacNhan = ColorTranslator.FromHtml("#0EA5E9");
        public static readonly Color MauDangLap = ColorTranslator.FromHtml("#F59E0B");
        public static readonly Color MauDaHuy = ColorTranslator.FromHtml("#EF4444");

        // ===== Mau nut chuc nang noi bat =====
        public static readonly Color MauNutXuatExcel = ColorTranslator.FromHtml("#16955A");
        public static readonly Color MauNutIn = ColorTranslator.FromHtml("#F59E0B");
        public static readonly Color MauNutLamMoi = ColorTranslator.FromHtml("#64748B");

        // ===== Huy chuong top 1-3 =====
        public static readonly Color MauVang = ColorTranslator.FromHtml("#D4A017");
        public static readonly Color MauBac = ColorTranslator.FromHtml("#8A97A8");
        public static readonly Color MauDong = ColorTranslator.FromHtml("#B8672E");

        // ===== Giu nguyen cac alias tuong thich code cu =====
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