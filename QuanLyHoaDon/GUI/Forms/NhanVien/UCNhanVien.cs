using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.NhanVien
{
    public class UCNhanVien : UserControl
    {
        private readonly NhanVienBLL _bll = new();

        private Panel pnlTop = null!;
        private TextBox txtTimKiem = null!;
        private Button btnTimKiem = null!;
        private Button btnLamMoi = null!;

        private SplitContainer splitContainer = null!;
        private DataGridView dgvNhanVien = null!;

        private Panel pnlFormChiTiet = null!;
        private Label lblFormTieuDe = null!;
        private Label lblQuyenGhiChu = null!;

        private TextBox txtMaNhanVien = null!;
        private TextBox txtHoTen = null!;
        private TextBox txtSoDienThoai = null!;
        private TextBox txtEmail = null!;
        private TextBox txtDiaChi = null!;
        private ComboBox cboTrangThaiNV = null!;

        // Tai khoan
        private TextBox txtMaTaiKhoan = null!;
        private TextBox txtTenDangNhap = null!;
        private TextBox txtMatKhau = null!;
        private ComboBox cboVaiTro = null!;
        private ComboBox cboTrangThaiTK = null!;

        private Button btnThem = null!;
        private Button btnCapNhat = null!;
        private Button btnKhoaMoKhoa = null!;
        private Button btnCapLaiMatKhau = null!;
        private Button btnBoChon = null!;

        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;

        public UCNhanVien()
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

            // 1. Top Panel
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

            // 2. SplitContainer
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 640,
                SplitterWidth = 14,
                BackColor = ThemeHelper.MauNenUngDung,
                Margin = new Padding(0, 16, 0, 0)
            };

            // Bảng nhân viên
            dgvNhanVien = new DataGridView { Dock = DockStyle.Fill };
            ThemeHelper.ChuanHoaDataGridView(dgvNhanVien);

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNhanVien", HeaderText = "Mã NV", FillWeight = 20 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "Họ và tên", FillWeight = 40 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenDangNhap", HeaderText = "Tài khoản", FillWeight = 25 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenVaiTro", HeaderText = "Vai trò", FillWeight = 25 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThaiNV", HeaderText = "Trạng thái NV", FillWeight = 30 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThaiTK", HeaderText = "Tài khoản", FillWeight = 25 });

            dgvNhanVien.SelectionChanged += DgvNhanVien_SelectionChanged;

            var pnlBangContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlBangContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBangContainer.Width - 1, pnlBangContainer.Height - 1);
            };
            pnlBangContainer.Controls.Add(dgvNhanVien);
            splitContainer.Panel1.Controls.Add(pnlBangContainer);

            // Form chi tiết
            pnlFormChiTiet = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.MauNenNoiDung,
                Padding = new Padding(18),
                AutoScroll = true
            };
            pnlFormChiTiet.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlFormChiTiet.Width - 1, pnlFormChiTiet.Height - 1);
            };

            lblFormTieuDe = new Label
            {
                Text = "QUẢN LÝ NHÂN SỰ & TÀI KHOẢN",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(18, 14),
                AutoSize = true
            };

            lblQuyenGhiChu = new Label
            {
                Text = "🛡 Phân quyền Quản lý: Thêm mới, cập nhật hồ sơ, phân vai trò, khóa/mở khóa tài khoản.",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChinh,
                Location = new Point(18, 40),
                AutoSize = true
            };

            pnlThongBao = new Panel
            {
                Location = new Point(18, 64),
                Size = new Size(390, 34),
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

            int top = 105;
            ThemInput(pnlFormChiTiet, "Mã nhân viên:", ref txtMaNhanVien, ref top, laReadOnly: true);
            ThemInput(pnlFormChiTiet, "Họ và tên *:", ref txtHoTen, ref top);
            ThemInput(pnlFormChiTiet, "Số điện thoại:", ref txtSoDienThoai, ref top);
            ThemInput(pnlFormChiTiet, "Email:", ref txtEmail, ref top);
            ThemInput(pnlFormChiTiet, "Địa chỉ:", ref txtDiaChi, ref top);

            // Trạng thái nhân viên
            var lblTTNV = new Label { Text = "Trạng thái nhân sự:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(18, top), AutoSize = true };
            cboTrangThaiNV = new ComboBox { Location = new Point(18, top + 22), Size = new Size(380, 32), Font = ThemeHelper.FontNoiDung, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThaiNV.Items.Add("DangHoatDong");
            cboTrangThaiNV.Items.Add("NgungHoatDong");
            cboTrangThaiNV.SelectedIndex = 0;
            pnlFormChiTiet.Controls.Add(lblTTNV);
            pnlFormChiTiet.Controls.Add(cboTrangThaiNV);
            top += 56;

            ThemInput(pnlFormChiTiet, "Tên đăng nhập *:", ref txtTenDangNhap, ref top);
            ThemInput(pnlFormChiTiet, "Mật khẩu khởi tạo (khi thêm) *:", ref txtMatKhau, ref top);

            // Vai trò
            var lblVT = new Label { Text = "Vai trò tài khoản:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(18, top), AutoSize = true };
            cboVaiTro = new ComboBox { Location = new Point(18, top + 22), Size = new Size(380, 32), Font = ThemeHelper.FontNoiDung, DropDownStyle = ComboBoxStyle.DropDownList };
            cboVaiTro.Items.Add("NhanVien");
            cboVaiTro.Items.Add("QuanLy");
            cboVaiTro.SelectedIndex = 0;
            pnlFormChiTiet.Controls.Add(lblVT);
            pnlFormChiTiet.Controls.Add(cboVaiTro);
            top += 56;

            // Trạng thái tài khoản
            var lblTTTK = new Label { Text = "Trạng thái tài khoản:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(18, top), AutoSize = true };
            cboTrangThaiTK = new ComboBox { Location = new Point(18, top + 22), Size = new Size(380, 32), Font = ThemeHelper.FontNoiDung, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThaiTK.Items.Add("HoatDong");
            cboTrangThaiTK.Items.Add("BiKhoa");
            cboTrangThaiTK.SelectedIndex = 0;
            pnlFormChiTiet.Controls.Add(lblTTTK);
            pnlFormChiTiet.Controls.Add(cboTrangThaiTK);
            top += 62;

            // Mã tài khoản ẩn
            txtMaTaiKhoan = new TextBox { Visible = false };
            pnlFormChiTiet.Controls.Add(txtMaTaiKhoan);

            // Buttons
            btnThem = new Button { Text = "Thêm NV", Location = new Point(18, top), Size = new Size(90, ThemeHelper.ChieuCaoButtonChuan) };
            ThemeHelper.ApDungButtonThanhCong(btnThem);
            btnThem.Click += BtnThem_Click;

            btnCapNhat = new Button { Text = "Lưu hồ sơ", Location = new Point(114, top), Size = new Size(95, ThemeHelper.ChieuCaoButtonChuan) };
            ThemeHelper.ApDungButtonChinh(btnCapNhat);
            btnCapNhat.Click += BtnCapNhat_Click;

            btnKhoaMoKhoa = new Button { Text = "Khóa/Mở TK", Location = new Point(215, top), Size = new Size(100, ThemeHelper.ChieuCaoButtonChuan) };
            ThemeHelper.ApDungButtonNguyHiem(btnKhoaMoKhoa);
            btnKhoaMoKhoa.Click += BtnKhoaMoKhoa_Click;

            btnCapLaiMatKhau = new Button { Text = "🔑 Cấp lại MK", Location = new Point(18, top + 42), Size = new Size(125, ThemeHelper.ChieuCaoButtonChuan) };
            ThemeHelper.ApDungButtonCanhBao(btnCapLaiMatKhau);
            btnCapLaiMatKhau.Click += BtnCapLaiMatKhau_Click;

            btnBoChon = new Button { Text = "Bỏ chọn", Location = new Point(150, top + 42), Size = new Size(85, ThemeHelper.ChieuCaoButtonChuan) };
            ThemeHelper.ApDungButtonPhu(btnBoChon);
            btnBoChon.Click += (s, e) => LamMoiForm();

            pnlFormChiTiet.Controls.Add(lblFormTieuDe);
            pnlFormChiTiet.Controls.Add(lblQuyenGhiChu);
            pnlFormChiTiet.Controls.Add(pnlThongBao);
            pnlFormChiTiet.Controls.Add(btnThem);
            pnlFormChiTiet.Controls.Add(btnCapNhat);
            pnlFormChiTiet.Controls.Add(btnKhoaMoKhoa);
            pnlFormChiTiet.Controls.Add(btnCapLaiMatKhau);
            pnlFormChiTiet.Controls.Add(btnBoChon);

            splitContainer.Panel2.Controls.Add(pnlFormChiTiet);

            var pnlDem = new Panel { Dock = DockStyle.Top, Height = 14 };

            this.Controls.Add(splitContainer);
            this.Controls.Add(pnlDem);
            this.Controls.Add(pnlTop);
        }

        private void ThemInput(Panel parent, string labelText, ref TextBox txt, ref int top, bool laReadOnly = false)
        {
            var lbl = new Label { Text = labelText, Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(18, top), AutoSize = true };
            txt = new TextBox { Location = new Point(18, top + 22), Size = new Size(380, 32), Font = ThemeHelper.FontNoiDung };
            ThemeHelper.ChuanHoaTextBox(txt, laReadOnly);
            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            top += 56;
        }

        private void TaiDanhSach()
        {
            dgvNhanVien.Rows.Clear();
            var ds = _bll.LayDanhSach(txtTimKiem.Text);
            foreach (var nv in ds)
            {
                dgvNhanVien.Rows.Add(
                    $"NV{nv.MaNhanVien:D4}",
                    nv.HoTen,
                    nv.TenDangNhap ?? "(Chưa có)",
                    nv.TenVaiTro ?? "",
                    nv.TrangThaiNhanVien,
                    nv.TrangThaiTaiKhoan ?? "");
            }
        }

        private void DgvNhanVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null || dgvNhanVien.CurrentRow.Index < 0) return;

            string maNVStr = dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maNVStr)) return;

            int maNV = int.Parse(maNVStr.Replace("NV", ""));
            var ds = _bll.LayDanhSach();
            var nv = ds.Find(x => x.MaNhanVien == maNV);
            if (nv == null) return;

            txtMaNhanVien.Text = $"NV{nv.MaNhanVien:D4}";
            txtHoTen.Text = nv.HoTen;
            txtSoDienThoai.Text = nv.SoDienThoai ?? "";
            txtEmail.Text = nv.Email ?? "";
            txtDiaChi.Text = nv.DiaChi ?? "";
            cboTrangThaiNV.SelectedItem = nv.TrangThaiNhanVien;

            txtMaTaiKhoan.Text = nv.MaTaiKhoan?.ToString() ?? "";
            txtTenDangNhap.Text = nv.TenDangNhap ?? "";
            txtTenDangNhap.ReadOnly = true; // Không sửa tên đăng nhập đã tạo
            txtTenDangNhap.BackColor = ThemeHelper.MauNenInputReadOnly;
            txtMatKhau.Clear();

            cboVaiTro.SelectedItem = nv.TenVaiTro ?? "NhanVien";
            cboTrangThaiTK.SelectedItem = nv.TrangThaiTaiKhoan ?? "HoatDong";

            btnKhoaMoKhoa.Text = (nv.TrangThaiTaiKhoan == "BiKhoa") ? "Mở khóa TK" : "Khóa TK";
        }

        private void LamMoiForm()
        {
            txtMaNhanVien.Clear();
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            cboTrangThaiNV.SelectedIndex = 0;

            txtMaTaiKhoan.Clear();
            txtTenDangNhap.Clear();
            txtTenDangNhap.ReadOnly = false;
            txtTenDangNhap.BackColor = Color.White;
            txtMatKhau.Clear();
            cboVaiTro.SelectedIndex = 0;
            cboTrangThaiTK.SelectedIndex = 0;

            btnKhoaMoKhoa.Text = "Khóa/Mở TK";
            dgvNhanVien.ClearSelection();
            AnThongBao();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            int maVT = (cboVaiTro.SelectedItem?.ToString() == "QuanLy") ? 1 : 2;

            var nv = new NhanVienQuanLyDTO
            {
                HoTen = txtHoTen.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text,
                NgayVaoLam = DateTime.Now,
                TrangThaiNhanVien = cboTrangThaiNV.SelectedItem?.ToString() ?? "DangHoatDong",
                TenDangNhap = txtTenDangNhap.Text,
                MaVaiTro = maVT,
                TrangThaiTaiKhoan = cboTrangThaiTK.SelectedItem?.ToString() ?? "HoatDong"
            };

            var kq = _bll.ThemNhanVien(nv, txtMatKhau.Text);
            HienThongBao(kq.ThongBao, !kq.ThanhCong);
            if (kq.ThanhCong)
            {
                TaiDanhSach();
                LamMoiForm();
            }
        }

        private void BtnCapNhat_Click(object? sender, EventArgs e)
        {
            string maNVStr = txtMaNhanVien.Text.Replace("NV", "");
            if (!int.TryParse(maNVStr, out int maNV))
            {
                HienThongBao("Vui lòng chọn nhân viên trong danh sách trước khi cập nhật.", true);
                return;
            }

            int? maTK = int.TryParse(txtMaTaiKhoan.Text, out int tkId) ? tkId : null;
            int maVT = (cboVaiTro.SelectedItem?.ToString() == "QuanLy") ? 1 : 2;

            var nv = new NhanVienQuanLyDTO
            {
                MaNhanVien = maNV,
                HoTen = txtHoTen.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text,
                TrangThaiNhanVien = cboTrangThaiNV.SelectedItem?.ToString() ?? "DangHoatDong",
                MaTaiKhoan = maTK,
                MaVaiTro = maVT,
                TrangThaiTaiKhoan = cboTrangThaiTK.SelectedItem?.ToString() ?? "HoatDong"
            };

            var kq = _bll.CapNhatNhanVien(nv);
            HienThongBao(kq.ThongBao, !kq.ThanhCong);
            if (kq.ThanhCong)
            {
                TaiDanhSach();
            }
        }

        private void BtnKhoaMoKhoa_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMaTaiKhoan.Text, out int maTK))
            {
                HienThongBao("Nhân viên này chưa có tài khoản hoặc chưa được chọn.", true);
                return;
            }

            bool dangBiKhoa = cboTrangThaiTK.SelectedItem?.ToString() == "BiKhoa";
            var kq = _bll.KhoaMoKhoaTaiKhoan(maTK, !dangBiKhoa);
            HienThongBao(kq.ThongBao, !kq.ThanhCong);
            if (kq.ThanhCong)
            {
                TaiDanhSach();
            }
        }

        private void BtnCapLaiMatKhau_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMaTaiKhoan.Text, out int maTK) || maTK <= 0)
            {
                HienThongBao("Nhân viên này chưa có tài khoản hoặc chưa được chọn trong danh sách.", true);
                return;
            }

            string tenNV = txtHoTen.Text.Trim();
            string tenTK = txtTenDangNhap.Text.Trim();

            var xacNhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn cấp lại mật khẩu cho tài khoản '{tenTK}' ({tenNV})?\n\nMật khẩu sẽ được đặt lại về mật khẩu mặc định: Demo@123",
                "Xác Nhận Cấp Lại Mật Khẩu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
            {
                return;
            }

            var kq = _bll.CapLaiMatKhau(maTK, "Demo@123");
            HienThongBao(kq.ThongBao, !kq.ThanhCong);
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

