using System;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyHoaDon.Utilities
{
    public static class PasswordHelper
    {
        public static string BamMatKhau(string matKhau)
        {
            if (string.IsNullOrEmpty(matKhau))
            {
                return string.Empty;
            }

            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(matKhau));
            var builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public static bool XacThucMatKhau(string matKhau, string hashLuuTru)
        {
            if (string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(hashLuuTru))
            {
                return false;
            }

            string hashMoi = BamMatKhau(matKhau);
            return string.Equals(hashMoi, hashLuuTru, StringComparison.OrdinalIgnoreCase);
        }
    }
}

