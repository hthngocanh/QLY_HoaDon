using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.SanPham
{
    public class UCSanPham : UserControl
    {
        private readonly SanPhamBLL _bll = new();

        private Panel pnlTop = null!;
        private TextBox txtTimKiem = null!;
        private Button btnTimKiem = null!;
        private Button btnLamMoi = null!;

        private SplitContainer splitContainer = null!;
        private DataGridView dgvSanPham = null!;

        private Panel pnlFormChiTiet = null!;
        private Label lblFormTieuDe = null!;
        private Label lblQuyenGhiChu = null!;

        private TextBox txtMaSanPham = null!;
        private TextBox txtTenSanPham = null!;
        private TextBox txtDonViTinh = null!;
        private TextBox txtDonGia = null!;
        private ComboBox cboTrangThai = null!;
        private TextBox txtMoTa = null!;

        private Button btnThem = null!;
        private Button btnCapNhat = null!;
        private Button btnBoChon = null!;

        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;

        public UCSanPham()
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

            // 1. Panel tìm kiếm
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

            // 2. SplitContainer: Trái bảng, Phải form
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 650,
                SplitterWidth = 14,
                BackColor = ThemeHelper.MauNenUngDung,
                Margin = new Padding(0, 16, 0, 0)
            };

            // Bảng sản phẩm
            dgvSanPham = new DataGridView { Dock = DockStyle.Fill };
            ThemeHelper.ChuanHoaDataGridView(dgvSanPham);

            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSanPham", HeaderText = "Mã SP", FillWeight = 20 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSanPham", HeaderText = "Tên mặt hàng", FillWeight = 50 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonViTinh", HeaderText = "ĐVT", FillWeight = 20 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Đơn giá (đ)", FillWeight = 30 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng thái", FillWeight = 35 });

            dgvSanPham.SelectionChanged += DgvSanPham_SelectionChanged;

            var pnlBangContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlBangContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBangContainer.Width - 1, pnlBangContainer.Height - 1);
            };
            pnlBangContainer.Controls.Add(dgvSanPham);
            splitContainer.Panel1.Controls.Add(pnlBangContainer);

            // Form sản phẩm
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
                Text = "QUẢN LÝ THÔNG TIN SẢN PHẨM",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 16),
                AutoSize = true
            };

            lblQuyenGhiChu = new Label
            {
                Text = Session.LaQuanLy
                    ? "✏ Quản lý: Được thêm sản phẩm, cập nhật giá và ngừng kinh doanh."
                    : "🔒 Nhân viên: Chỉ có quyền xem danh mục sản phẩm và tra cứu đơn giá.",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = Session.LaQuanLy ? ThemeHelper.MauThanhCong : ThemeHelper.MauCanhBao,
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
            ThemInput(pnlFormChiTiet, "Mã sản phẩm:", ref txtMaSanPham, ref top, laReadOnly: true);
            ThemInput(pnlFormChiTiet, "Tên sản phẩm *:", ref txtTenSanPham, ref top, laReadOnly: !Session.LaQuanLy);
            ThemInput(pnlFormChiTiet, "Đơn vị tính *:", ref txtDonViTinh, ref top, laReadOnly: !Session.LaQuanLy);
            ThemInput(pnlFormChiTiet, "Đơn giá (VNĐ) *:", ref txtDonGia, ref top, laReadOnly: !Session.LaQuanLy);

            var lblTT = new Label
            {
                Text = "Trạng thái kinh doanh:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, top),
                AutoSize = true
            };
            cboTrangThai = new ComboBox
            {
                Location = new Point(20, top + 22),
                Size = new Size(360, 32),
                Font = ThemeHelper.FontNoiDung,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = Session.LaQuanLy
            };
            cboTrangThai.Items.Add("DangKinhDoanh");
            cboTrangThai.Items.Add("NgungKinhDoanh");
            cboTrangThai.SelectedIndex = 0;

            pnlFormChiTiet.Controls.Add(lblTT);
            pnlFormChiTiet.Controls.Add(cboTrangThai);
            top += 58;

            ThemInput(pnlFormChiTiet, "Mô tả sản phẩm:", ref txtMoTa, ref top, laReadOnly: !Session.LaQuanLy);

            btnThem = new Button
            {
                Text = "Thêm mới",
                Location = new Point(20, top + 10),
                Size = new Size(110, ThemeHelper.ChieuCaoButtonChuan),
                Enabled = Session.LaQuanLy
            };
            ThemeHelper.ApDungButtonThanhCong(btnThem);
            btnThem.Click += BtnThem_Click;

            btnCapNhat = new Button
            {
                Text = "Lưu giá & TT",
                Location = new Point(140, top + 10),
                Size = new Size(130, ThemeHelper.ChieuCaoButtonChuan),
                Enabled = Session.LaQuanLy
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
            ThemeHelper.ChuanHoaTextBox(txt, laReadOnly);

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            top += 58;
        }

        private void TaiDanhSach()
        {
            dgvSanPham.Rows.Clear();
            var ds = _bll.LayDanhSach(txtTimKiem.Text);
            foreach (var sp in ds)
            {
                dgvSanPham.Rows.Add(sp.MaSanPham, sp.TenSanPham, sp.DonViTinh, $"{sp.DonGia:N0}", sp.TrangThai);
            }
        }

        private void DgvSanPham_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null || dgvSanPham.CurrentRow.Index < 0) return;

            var r = dgvSanPham.CurrentRow;
            txtMaSanPham.Text = r.Cells["MaSanPham"].Value?.ToString() ?? "";
            txtTenSanPham.Text = r.Cells["TenSanPham"].Value?.ToString() ?? "";
            txtDonViTinh.Text = r.Cells["DonViTinh"].Value?.ToString() ?? "";
            string dg = r.Cells["DonGia"].Value?.ToString() ?? "0";
            txtDonGia.Text = dg.Replace(",", "").Replace(".", "");
            string tt = r.Cells["TrangThai"].Value?.ToString() ?? "DangKinhDoanh";
            cboTrangThai.SelectedItem = tt;
        }

        private void LamMoiForm()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtDonViTinh.Clear();
            txtDonGia.Clear();
            txtMoTa.Clear();
            cboTrangThai.SelectedIndex = 0;
            dgvSanPham.ClearSelection();
            AnThongBao();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                HienThongBao("Đơn giá sản phẩm không hợp lệ.", true);
                return;
            }

            var sp = new SanPhamDTO
            {
                TenSanPham = txtTenSanPham.Text,
                MoTa = txtMoTa.Text,
                DonViTinh = txtDonViTinh.Text,
                DonGia = donGia,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "DangKinhDoanh"
            };

            var kq = _bll.ThemSanPham(sp);
            HienThongBao(kq.ThongBao, !kq.ThanhCong);
            if (kq.ThanhCong)
            {
                TaiDanhSach();
                LamMoiForm();
            }
        }

        private void BtnCapNhat_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMaSanPham.Text, out int maSP))
            {
                HienThongBao("Vui lòng chọn sản phẩm trong danh sách trước khi cập nhật.", true);
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                HienThongBao("Đơn giá sản phẩm không hợp lệ.", true);
                return;
            }

            string tt = cboTrangThai.SelectedItem?.ToString() ?? "DangKinhDoanh";
            var kq = _bll.CapNhatGiaVaTrangThai(maSP, donGia, tt);
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

