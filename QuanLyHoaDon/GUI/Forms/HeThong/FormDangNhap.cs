using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public partial class FormDangNhap : Form
    {
        private readonly DangNhapBLL _dangNhapBLL;
        private bool _dangHienMatKhau = false;

        public FormDangNhap()
        {
            InitializeComponent();
            _dangNhapBLL = new DangNhapBLL();
            CaiDatGiaoDienVaSuKien();
        }

        private void CaiDatGiaoDienVaSuKien()
        {
            // Áp dụng theme
            this.Font = ThemeHelper.FontNoiDung;
            this.BackColor = ThemeHelper.MauNenNoiDung;

            // Chuẩn hóa button Đăng Nhập theo ThemeHelper
            ThemeHelper.ApDungButtonChinh(btnDangNhap);
            btnDangNhap.Font = ThemeHelper.FontButton;

            // Focus effect cho input
            txtTenDangNhap.Font = ThemeHelper.FontNoiDung;
            txtTenDangNhap.ForeColor = ThemeHelper.MauChuChinh;
            txtTenDangNhap.Enter += (s, e) => pnlTenDangNhapBorder.BackColor = Color.White;
            txtTenDangNhap.Leave += (s, e) => pnlTenDangNhapBorder.BackColor = ColorTranslator.FromHtml("#F8FAFC");

            txtMatKhau.Font = ThemeHelper.FontNoiDung;
            txtMatKhau.ForeColor = ThemeHelper.MauChuChinh;
            txtMatKhau.Enter += (s, e) => pnlMatKhauBorder.BackColor = Color.White;
            txtMatKhau.Leave += (s, e) => pnlMatKhauBorder.BackColor = ColorTranslator.FromHtml("#F8FAFC");

            // Sự kiện nhấn Enter
            txtTenDangNhap.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtMatKhau.Focus();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            txtMatKhau.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnDangNhap.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void BtnHienMatKhau_Click(object? sender, EventArgs e)
        {
            _dangHienMatKhau = !_dangHienMatKhau;
            txtMatKhau.UseSystemPasswordChar = !_dangHienMatKhau;
            btnHienMatKhau.Text = _dangHienMatKhau ? "Ẩn" : "Hiện";
            txtMatKhau.Focus();
            txtMatKhau.SelectionStart = txtMatKhau.Text.Length;
        }

        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            AnThongBaoLoi();

            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            // Gọi BLL để xử lý đăng nhập
            var ketQua = _dangNhapBLL.DangNhap(tenDangNhap, matKhau);

            if (!ketQua.ThanhCong)
            {
                HienThongBaoLoi(ketQua.ThongBao);
                txtMatKhau.SelectAll();
                txtMatKhau.Focus();
                return;
            }

            // Đăng nhập thành công -> Mở FormChinh
            MoFormChinh();
        }

        private void MoFormChinh()
        {
            this.Hide();

            var formChinh = new FormChinh();
            formChinh.FormClosed += (s, args) =>
            {
                // Khi FormChinh đóng (ví dụ đăng xuất), hiển thị lại FormDangNhap
                txtMatKhau.Clear();
                AnThongBaoLoi();
                this.Show();
                txtTenDangNhap.Focus();
                txtTenDangNhap.SelectAll();
            };

            formChinh.Show();
        }

        private void HienThongBaoLoi(string thongBao)
        {
            lblThongBaoLoi.Text = "⚠  " + thongBao;
            pnlThongBaoLoi.Visible = true;
        }

        private void AnThongBaoLoi()
        {
            pnlThongBaoLoi.Visible = false;
            lblThongBaoLoi.Text = string.Empty;
        }
    }
}
