using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public partial class FormDangNhap : Form
    {
        private readonly DangNhapBLL _dangNhapBLL;

        public FormDangNhap()
        {
            InitializeComponent();
            _dangNhapBLL = new DangNhapBLL();
            ApDungGiaoDien();
            CaiDatSuKien();
        }

        private void ApDungGiaoDien()
        {
            btnDangNhap.BorderRadius = 10;
            btnDangNhap.HoverColor = ThemeHelper.MauChinhHover;

            // Viền xanh khi ô nhập đang được chọn
            GanHieuUngFocus(txtTenDangNhap, pnlTenDangNhap);
            GanHieuUngFocus(txtMatKhau, pnlMatKhau);

            // Hiện / ẩn mật khẩu
            chkHienMatKhau.CheckedChanged += (s, e) =>
                txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;

            // Link quên mật khẩu: gạch chân khi rê chuột
            var fontThuong = lblQuenMatKhau.Font;
            var fontGachChan = new Font(fontThuong, FontStyle.Underline);
            lblQuenMatKhau.MouseEnter += (s, e) => lblQuenMatKhau.Font = fontGachChan;
            lblQuenMatKhau.MouseLeave += (s, e) => lblQuenMatKhau.Font = fontThuong;
        }

        private static void GanHieuUngFocus(TextBox txt, RoundedPanel khung)
        {
            Color vienThuong = khung.BorderColor;
            txt.Enter += (s, e) => { khung.BorderColor = ThemeHelper.MauDuongVienFocus; khung.BorderWidth = 2; };
            txt.Leave += (s, e) => { khung.BorderColor = vienThuong; khung.BorderWidth = 1; };
            khung.Click += (s, e) => txt.Focus();
        }

        private void CaiDatSuKien()
        {
            // Phím Enter điều hướng giữa 2 ô nhập và kích hoạt đăng nhập
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

            // Sự kiện đăng nhập
            btnDangNhap.Click += BtnDangNhap_Click;

            // Sự kiện quên mật khẩu
            lblQuenMatKhau.Click += (s, e) =>
            {
                using var frmQuenMK = new FormQuenMatKhau();
                frmQuenMK.ShowDialog(this);
            };

            // Tự động focus vào ô Tên đăng nhập khi mở form
            this.Shown += (s, e) => txtTenDangNhap.Focus();
        }

        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            AnThongBaoLoi();

            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            // Chuyển nút sang trạng thái đang xử lý (ngăn bấm liên tục)
            btnDangNhap.Enabled = false;
            btnDangNhap.Text = "Đang kiểm tra...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Gọi BLL để xử lý nghiệp vụ đăng nhập
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
            finally
            {
                // Phục hồi trạng thái nút đăng nhập
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "ĐĂNG NHẬP";
                this.Cursor = Cursors.Default;
            }
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

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {

        }
    }
}