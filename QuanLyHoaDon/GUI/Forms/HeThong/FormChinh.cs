using System;
using System.Windows.Forms;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public partial class FormChinh : Form
    {
        public FormChinh()
        {
            InitializeComponent();
            ApDungGiaoDien();
        }

        private void ApDungGiaoDien()
        {
            this.Font = ThemeHelper.TaoFont(9.5f);
            this.BackColor = ThemeHelper.MauNen;

            if (pnlHeader != null)
            {
                pnlHeader.BackColor = ThemeHelper.MauChuDao;
            }

            if (lblTieuDe != null)
            {
                lblTieuDe.Font = ThemeHelper.TaoFont(16f, System.Drawing.FontStyle.Bold);
                lblTieuDe.ForeColor = System.Drawing.Color.White;
            }

            if (lblMoTa != null)
            {
                lblMoTa.Font = ThemeHelper.TaoFont(10f);
                lblMoTa.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            }

            if (pnlCardThongTin != null)
            {
                pnlCardThongTin.BackColor = ThemeHelper.MauNenCard;
            }

            if (lblCardTieuDe != null)
            {
                lblCardTieuDe.Font = ThemeHelper.TaoFont(12f, System.Drawing.FontStyle.Bold);
                lblCardTieuDe.ForeColor = ThemeHelper.MauChuChinh;
            }

            if (lblCardNoiDung != null)
            {
                lblCardNoiDung.Font = ThemeHelper.TaoFont(9.5f);
                lblCardNoiDung.ForeColor = ThemeHelper.MauChuPhu;
            }

            if (btnKiemTra != null)
            {
                btnKiemTra.BackColor = ThemeHelper.MauThaoTacChinh;
                btnKiemTra.ForeColor = System.Drawing.Color.White;
                btnKiemTra.Font = ThemeHelper.TaoFont(10f, System.Drawing.FontStyle.Bold);
            }

            if (statusStrip != null)
            {
                statusStrip.BackColor = System.Drawing.Color.White;
                lblTrangThai.ForeColor = ThemeHelper.MauChuPhu;
            }
        }

        private void BtnKiemTra_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "Bộ khung hệ thống Quản lý hóa đơn đã được khởi tạo thành công!\n\n" +
                "Kiến trúc: 3-Layer (GUI - BLL - DAL - DTO - Utilities)\n" +
                "Nền tảng: .NET 10 (LTS) - Windows Forms (Visual Studio 2026)\n" +
                "Database: SQL Server (QuanLyHoaDon - 7 Bảng)",
                "Thông Báo Khởi Tạo Dự Án",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}

