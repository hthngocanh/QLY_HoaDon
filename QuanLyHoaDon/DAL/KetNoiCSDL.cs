using System.Configuration;
using Microsoft.Data.SqlClient;

namespace QuanLyHoaDon.DAL
{
    public static class KetNoiCSDL
    {
        private const string TenChuoiKetNoi = "QuanLyHoaDonConnection";

        public static string LayChuoiKetNoi()
        {
            var chuoiKetNoi = ConfigurationManager.ConnectionStrings[TenChuoiKetNoi]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(chuoiKetNoi))
            {
                return "Data Source=.;Initial Catalog=QuanLyHoaDon;Integrated Security=True;TrustServerCertificate=True;";
            }
            return chuoiKetNoi;
        }

        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(LayChuoiKetNoi());
        }
    }
}

