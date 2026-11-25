using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace QuanLyHoaDon.DAL
{
    public static class KetNoiCSDL
    {
        private const string TenChuoiKetNoi = "QuanLyHoaDonConnection";
        private const string ChuoiKetNoiMacDinh = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLyHoaDon;Integrated Security=True;TrustServerCertificate=True;";

        public static string LayChuoiKetNoi()
        {
            try
            {
                var chuoiKetNoi = ConfigurationManager.ConnectionStrings[TenChuoiKetNoi]?.ConnectionString;
                if (!string.IsNullOrWhiteSpace(chuoiKetNoi))
                {
                    return chuoiKetNoi;
                }
            }
            catch
            {
                // Fallback an toan khi gap bat ky loi doc file cau hinh
            }

            return ChuoiKetNoiMacDinh;
        }

        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(LayChuoiKetNoi());
        }
    }
}
