using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.KhachHang
{
    public class UCKhachHang : UserControl
    {
        private readonly KhachHangBLL _bll = new();

        private Panel pnlTop = null!;
        private TextBox txtTimKiem = null!;
        private Button btnTimKiem = null!;
        private Button btnLamMoi = null!;

        private SplitContainer splitContainer = null!;
        private DataGridView dgvKhachHang = null!;

        private Panel pnlFormChiTiet = null!;
        private Label lblFormTieuDe = null!;
        private Label lblQuyenGhiChu = null!;

        private TextBox txtMaKhachHang = null!;
        private TextBox txtHoTen = null!;
        private TextBox txtSoDienThoai = null!;
        private TextBox txtEmail = null!;
        private TextBox txtDiaChi = null!;

        private Button btnThem = null!;
        private Button btnCapNhat = null!;
        private Button btnBoChon = null!;

        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;

        public UCKhachHang()
        {
            KhoiTaoGiaoDien();
            TaiDanhSach();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.FontNoiDung;
            this.Padding = new Padding(24, 20, 24, 20);

            // ==========================================
            // 1. Panel tìm kiếm & nút chức năng
            // ==========================================
            pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = ThemeHelper.MauNenNoiDung,
                Padding = new Padding(16, 10, 16, 10)
            };
            pnlTop.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlTop.Width - 1, pnlTop.Height - 1);
            };

            var lblTim = new Label
            {
                Text = "🔍 Tìm kiếm:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(16, 18),
                AutoSize = true
            };

            txtTimKiem = new TextBox
            {
                Location = new Point(125, 14),
                Size = new Size(320, 32),
                Font = ThemeHelper.FontNoiDung
            };
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) TaiDanhSach(); };

            btnTimKiem = new Button
            {
                Text = "Tìm kiếm",
                Location = new Point(455, 12),
                Size = new Size(110, 36)
            };
            ThemeHelper.ApDungButtonChinh(btnTimKiem);
            btnTimKiem.Height = 36;
            btnTimKiem.Click += (s, e) => TaiDanhSach();

            btnLamMoi = new Button
            {
                Text = "Tải lại",
                Location = new Point(575, 12),
                Size = new Size(100, 36)
            };
            ThemeHelper.ApDungButtonPhu(btnLamMoi);
            btnLamMoi.Height = 36;
            btnLamMoi.Click += (s, e) => { txtTimKiem.Clear(); TaiDanhSach(); LamMoiForm(); };

            pnlTop.Controls.Add(lblTim);
            pnlTop.Controls.Add(txtTimKiem);
            pnlTop.Controls.Add(btnTimKiem);
            pnlTop.Controls.Add(btnLamMoi);

            // ==========================================
            // 2. SplitContainer: Trái là Bảng, Phải là Form
            // ==========================================
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 650,
                SplitterWidth = 14,
                BackColor = ThemeHelper.MauNenUngDung,
                Margin = new Padding(0, 16, 0, 0)
            };

            // Bảng khách hàng (Bên trái)
            dgvKhachHang = new DataGridView
            {
                Dock = DockStyle.Fill
            };
            ThemeHelper.ChuanHoaDataGridView(dgvKhachHang);

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKhachHang", HeaderText = "Mã KH", FillWeight = 20 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "Họ và tên", FillWeight = 40 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDienThoai", HeaderText = "Số điện thoại", FillWeight = 30 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", FillWeight = 35 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiaChi", HeaderText = "Địa chỉ", FillWeight = 45 });

            dgvKhachHang.SelectionChanged += DgvKhachHang_SelectionChanged;

            var pnlBangContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            pnlBangContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBangContainer.Width - 1, pnlBangContainer.Height - 1);
            };
            pnlBangContainer.Controls.Add(dgvKhachHang);
            splitContainer.Panel1.Controls.Add(pnlBangContainer);

            // Form chi tiết & thao tác (Bên phải)
            pnlFormChiTiet = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.MauNenNoiDung,
                Padding = new Padding(20)
            };
            pnlFormChiTiet.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlFormChiTiet.Width - 1, pnlFormChiTiet.Height - 1);
            };

            lblFormTieuDe = new Label
            {
                Text = "THÔNG TIN KHÁCH HÀNG",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 16),
                AutoSize = true
            };

            lblQuyenGhiChu = new Label
            {
                Text = Session.LaQuanLy
                    ? "🔒 Chế độ xem: Tài khoản Quản lý chỉ được xem và tìm kiếm khách hàng."
                    : "✏ Phân quyền: Nhân viên bán hàng được phép thêm mới và cập nhật khách hàng.",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = Session.LaQuanLy ? ThemeHelper.MauCanhBao : ThemeHelper.MauThanhCong,
                Location = new Point(20, 44),
                AutoSize = true
            };

            pnlThongBao = new Panel
            {
                Location = new Point(20, 70),
                Size = new Size(380, 36),
                Visible = false
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontChuThichDam,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 8, 0)
            };
            pnlThongBao.Controls.Add(lblThongBao);

            int top = 115;
            ThemInput(pnlFormChiTiet, "Mã khách hàng:", ref txtMaKhachHang, ref top, laReadOnly: true);
            ThemInput(pnlFormChiTiet, "Họ và tên *:", ref txtHoTen, ref top);
            ThemInput(pnlFormChiTiet, "Số điện thoại:", ref txtSoDienThoai, ref top);
            ThemInput(pnlFormChiTiet, "Email liên hệ:", ref txtEmail, ref top);
            ThemInput(pnlFormChiTiet, "Địa chỉ liên hệ:", ref txtDiaChi, ref top);

            btnThem = new Button
            {
                Text = "Thêm mới",
                Location = new Point(20, top + 10),
                Size = new Size(110, ThemeHelper.ChieuCaoButtonChuan),
                Enabled = Session.LaNhanVien
            };
            ThemeHelper.ApDungButtonThanhCong(btnThem);
            btnThem.Click += BtnThem_Click;

            btnCapNhat = new Button
            {
                Text = "Lưu cập nhật",
                Location = new Point(140, top + 10),
                Size = new Size(130, ThemeHelper.ChieuCaoButtonChuan),
                Enabled = Session.LaNhanVien
            };
            ThemeHelper.ApDungButtonChinh(btnCapNhat);
            btnCapNhat.Click += BtnCapNhat_Click;

            btnBoChon = new Button
            {
                Text = "Bỏ chọn",
                Location = new Point(280, top + 10),
                Size = new Size(100, ThemeHelper.ChieuCaoButtonChuan)
            };
            ThemeHelper.ApDungButtonPhu(btnBoChon);
            btnBoChon.Click += (s, e) => LamMoiForm();

            pnlFormChiTiet.Controls.Add(lblFormTieuDe);
            pnlFormChiTiet.Controls.Add(lblQuyenGhiChu);
            pnlFormChiTiet.Controls.Add(pnlThongBao);
            pnlFormChiTiet.Controls.Add(btnThem);
            pnlFormChiTiet.Controls.Add(btnCapNhat);
            pnlFormChiTiet.Controls.Add(btnBoChon);

            splitContainer.Panel2.Controls.Add(pnlFormChiTiet);

            var pnlDem = new Panel { Dock = DockStyle.Top, Height = 14 };

            this.Controls.Add(splitContainer);
            this.Controls.Add(pnlDem);
            this.Controls.Add(pnlTop);
        }

        private void ThemInput(Panel parent, string labelText, ref TextBox txt, ref int top, bool laReadOnly = false)
        {
            var lbl = new Label
            {
                Text = labelText,
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, top),
                AutoSize = true
            };

            txt = new TextBox
            {
                Location = new Point(20, top + 22),
                Size = new Size(360, 32),
                Font = ThemeHelper.FontNoiDung
            };
            ThemeHelper.ChuanHoaTextBox(txt, laReadOnly || Session.LaQuanLy);

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            top += 58;
        }

        private void TaiDanhSach()
        {
            dgvKhachHang.Rows.Clear();
            var ds = _bll.LayDanhSach(txtTimKiem.Text);
            foreach (var kh in ds)
            {
                dgvKhachHang.Rows.Add(kh.MaKhachHang, kh.HoTen, kh.SoDienThoai ?? "", kh.Email ?? "", kh.DiaChi ?? "");
            }
        }

        private void DgvKhachHang_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null || dgvKhachHang.CurrentRow.Index < 0) return;

            var r = dgvKhachHang.CurrentRow;
            txtMaKhachHang.Text = r.Cells["MaKhachHang"].Value?.ToString() ?? "";
            txtHoTen.Text = r.Cells["HoTen"].Value?.ToString() ?? "";
            txtSoDienThoai.Text = r.Cells["SoDienThoai"].Value?.ToString() ?? "";
            txtEmail.Text = r.Cells["Email"].Value?.ToString() ?? "";
            txtDiaChi.Text = r.Cells["DiaChi"].Value?.ToString() ?? "";
        }

        private void LamMoiForm()
        {
            txtMaKhachHang.Clear();
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            dgvKhachHang.ClearSelection();
            AnThongBao();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            var kh = new KhachHangDTO
            {
                HoTen = txtHoTen.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text
            };

            var kq = _bll.ThemKhachHang(kh);
            HienThongBao(kq.ThongBao, !kq.ThanhCong);
            if (kq.ThanhCong)
            {
                TaiDanhSach();
                LamMoiForm();
            }
        }

        private void BtnCapNhat_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMaKhachHang.Text, out int maKH))
            {
                HienThongBao("Vui lòng chọn khách hàng trong danh sách trước khi cập nhật.", true);
                return;
            }

            var kh = new KhachHangDTO
            {
                MaKhachHang = maKH,
                HoTen = txtHoTen.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text
            };

            var kq = _bll.CapNhatKhachHang(kh);
            HienThongBao(kq.ThongBao, !kq.ThanhCong);
            if (kq.ThanhCong)
            {
                TaiDanhSach();
            }
        }

        private void HienThongBao(string thongBao, bool laLoi)
        {
            pnlThongBao.Visible = true;
            pnlThongBao.BackColor = laLoi ? ColorTranslator.FromHtml("#FEF2F2") : ColorTranslator.FromHtml("#F0FDF4");
            lblThongBao.ForeColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.Text = (laLoi ? "⚠ " : "✓ ") + thongBao;
        }

        private void AnThongBao()
        {
            pnlThongBao.Visible = false;
            lblThongBao.Text = string.Empty;
        }
    }
}

