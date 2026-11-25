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
            // Ap dung theme
            this.Font = ThemeHelper.TaoFont(9.5f);
            this.BackColor = ThemeHelper.MauNenNoiDung;

            // Hover effect cho button Dang Nhap
            btnDangNhap.MouseEnter += (s, e) => btnDangNhap.BackColor = ThemeHelper.MauChinhHover;
            btnDangNhap.MouseLeave += (s, e) => btnDangNhap.BackColor = ThemeHelper.MauChinh;

            // Focus effect cho input
            txtTenDangNhap.Enter += (s, e) => pnlTenDangNhapBorder.BackColor = Color.White;
            txtTenDangNhap.Leave += (s, e) => pnlTenDangNhapBorder.BackColor = Color.FromArgb(248, 250, 252);

            txtMatKhau.Enter += (s, e) => pnlMatKhauBorder.BackColor = Color.White;
            txtMatKhau.Leave += (s, e) => pnlMatKhauBorder.BackColor = Color.FromArgb(248, 250, 252);

            // Su kien nhan Enter
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

            // Goi BLL de xu ly dang nhap
            var ketQua = _dangNhapBLL.DangNhap(tenDangNhap, matKhau);

            if (!ketQua.ThanhCong)
            {
                HienThongBaoLoi(ketQua.ThongBao);
                txtMatKhau.SelectAll();
                txtMatKhau.Focus();
                return;
            }

            // Dang nhap thanh cong -> Mo FormChinh
            MoFormChinh();
        }

        private void MoFormChinh()
        {
            this.Hide();

            var formChinh = new FormChinh();
            formChinh.FormClosed += (s, args) =>
            {
                // Khi FormChinh dong (vi du dang xuat), hien thi lai FormDangNhap
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

