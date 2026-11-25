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
            Application.Run(new FormDangNhap());
        }
    }
}
