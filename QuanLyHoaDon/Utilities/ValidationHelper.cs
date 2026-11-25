using System.Text.RegularExpressions;

namespace QuanLyHoaDon.Utilities
{
    public static class ValidationHelper
    {
        public static bool KiemTraRong(string giaTri)
        {
            return string.IsNullOrWhiteSpace(giaTri);
        }

        public static bool KiemTraEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public static bool KiemTraSoDienThoai(string sdt)
        {
            if (string.IsNullOrWhiteSpace(sdt))
            {
                return false;
            }

            string pattern = @"^(0|\+84)[0-9]{9,10}$";
            return Regex.IsMatch(sdt, pattern);
        }

        public static bool KiemTraSoNguyenDuong(int so)
        {
            return so > 0;
        }

        public static bool KiemTraSoThucDuong(decimal so)
        {
            return so >= 0;
        }
    }
}

