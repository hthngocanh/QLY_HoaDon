
using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace QuanLyHoaDon.DAL
{
    public static class KetNoiCSDL
    {
        private const string TenChuoiKetNoi = "QuanLyHoaDonConnection";

        public static string LayChuoiKetNoi()
        {
            var chuoiKetNoi = ConfigurationManager
                .ConnectionStrings[TenChuoiKetNoi]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(chuoiKetNoi))
            {
                throw new ConfigurationErrorsException(
                    $"Không tìm thấy connection string '{TenChuoiKetNoi}' trong file cấu hình."
                );
            }

            return chuoiKetNoi;
        }

        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(LayChuoiKetNoi());
        }
    }
}
