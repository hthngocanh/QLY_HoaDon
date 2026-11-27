using System;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Forms.HeThong;

namespace QuanLyHoaDon
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Thiết lập font Segoe UI 10.5pt làm font mặc định toàn ứng dụng để đảm bảo hiển thị tiếng Việt hoàn hảo
            Application.SetDefaultFont(new System.Drawing.Font("Segoe UI", 10.5f, System.Drawing.FontStyle.Regular));
            Application.Run(new FormDangNhap());
        }
    }
}