using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HoaDon
{
    public class UCHoaDon : UserControl
    {
        private readonly HoaDonBLL _hoaDonBLL = new();
        private readonly KhachHangBLL _khachHangBLL = new();
        private readonly SanPhamBLL _sanPhamBLL = new();

        private Panel pnlTop = null!;
        private TextBox txtTimKiem = null!;
        private ComboBox cboLocTrangThai = null!;
        private Button btnTimKiem = null!;
        private Button btnLamMoi = null!;
        private string? _trangThaiLocHienTai = null;

        private TabControl tabHoaDon = null!;
        private TabPage tabDanhSach = null!;
        private TabPage tabLapHoaDon = null!;

        // Tab Danh Sách
        private DataGridView dgvHoaDon = null!;
        private DataGridView dgvChiTiet = null!;
        private SplitContainer splitDS = null!;
        private Button btnXacNhanHD = null!;
        private Button btnHuyHD = null!;

        // Tab Lập Hóa Đơn
        private ComboBox cboKhachHang = null!;
        private TextBox txtGhiChu = null!;
        private ComboBox cboSanPham = null!;
        private NumericUpDown numSoLuong = null!;
        private Button btnThemSanPham = null!;
        private Button btnXoaDong = null!;
        private DataGridView dgvDongChiTietMoi = null!;
        private Label lblTongTienLap = null!;
        private Button btnLuuHoaDon = null!;

        private List<KhachHangDTO> _dsKhachHang = new();
        private List<SanPhamDTO> _dsSanPham = new();
        private List<ChiTietHoaDonDTO> _dsChiTietMoi = new();

        public UCHoaDon()
        {
            KhoiTaoGiaoDien();
            TaiDanhSachHoaDon();
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
                Text = "🔍 Tra cứu hóa đơn:",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(16, 18),
                AutoSize = true
            };

            txtTimKiem = new TextBox
            {
                Location = new Point(175, 14),
                Size = new Size(220, 32),
                Font = ThemeHelper.FontNoiDung,
                PlaceholderText = "Mã HĐ, Khách hàng, SĐT..."
            };
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) TaiDanhSachHoaDon(); };

            cboLocTrangThai = new ComboBox
            {
                Location = new Point(405, 14),
                Size = new Size(150, 32),
                Font = ThemeHelper.FontNoiDung,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboLocTrangThai.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang lập", "Đã xác nhận", "Đã hủy" });
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (s, e) =>
            {
                _trangThaiLocHienTai = cboLocTrangThai.SelectedIndex switch
                {
                    1 => "DangLap",
                    2 => "DaXacNhan",
                    3 => "DaHuy",
                    _ => null
                };
                TaiDanhSachHoaDon();
            };

            btnTimKiem = new Button { Text = "Tra cứu", Location = new Point(565, 12), Size = new Size(90, 36) };
            ThemeHelper.ApDungButtonChinh(btnTimKiem);
            btnTimKiem.Height = 36;
            btnTimKiem.Click += (s, e) => TaiDanhSachHoaDon();

            btnLamMoi = new Button { Text = "Tải lại", Location = new Point(665, 12), Size = new Size(85, 36) };
            ThemeHelper.ApDungButtonPhu(btnLamMoi);
            btnLamMoi.Height = 36;
            btnLamMoi.Click += (s, e) => { txtTimKiem.Clear(); cboLocTrangThai.SelectedIndex = 0; _trangThaiLocHienTai = null; TaiDanhSachHoaDon(); };

            var lblPhanQuyenHD = new Label
            {
                Text = Session.LaQuanLy 
                    ? "🛡 Quản lý: Toàn bộ hóa đơn"
                    : "👤 Cá nhân: Hóa đơn của tôi",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = Session.LaQuanLy ? ThemeHelper.MauChinh : ThemeHelper.MauThanhCong,
                Location = new Point(760, 19),
                AutoSize = true
            };

            pnlTop.Controls.Add(lblTim);
            pnlTop.Controls.Add(txtTimKiem);
            pnlTop.Controls.Add(cboLocTrangThai);
            pnlTop.Controls.Add(btnTimKiem);
            pnlTop.Controls.Add(btnLamMoi);
            pnlTop.Controls.Add(lblPhanQuyenHD);

            // 2. TabControl
            tabHoaDon = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontNoiDungDam,
                Margin = new Padding(0, 16, 0, 0)
            };

            tabDanhSach = new TabPage("📋  Danh sách & Xem chi tiết hóa đơn");
            tabLapHoaDon = new TabPage("✍  Lập hóa đơn bán hàng mới");

            KhoiTaoTabDanhSach();
            KhoiTaoTabLapHoaDon();

            tabHoaDon.TabPages.Add(tabDanhSach);
            tabHoaDon.TabPages.Add(tabLapHoaDon);

            var pnlDem = new Panel { Dock = DockStyle.Top, Height = 14 };

            this.Controls.Add(tabHoaDon);
            this.Controls.Add(pnlDem);
            this.Controls.Add(pnlTop);
        }

        private void KhoiTaoTabDanhSach()
        {
            tabDanhSach.BackColor = ThemeHelper.MauNenUngDung;
            tabDanhSach.Padding = new Padding(12);

            splitDS = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 320,
                SplitterWidth = 12
            };

            // Phần trên: Danh sách hóa đơn
            dgvHoaDon = new DataGridView { Dock = DockStyle.Fill };
            ThemeHelper.ChuanHoaDataGridView(dgvHoaDon);

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaHoaDon", HeaderText = "Mã HĐ", FillWeight = 20 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenKhachHang", HeaderText = "Khách hàng", FillWeight = 40 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenNhanVien", HeaderText = "Nhân viên lập", FillWeight = 35 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayLap", HeaderText = "Ngày lập", FillWeight = 30 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongSoLuong", HeaderText = "Số lượng", FillWeight = 20 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongTien", HeaderText = "Tổng tiền (đ)", FillWeight = 35 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng thái", FillWeight = 25 });

            dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;

            var pnlHDContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlHDContainer.Controls.Add(dgvHoaDon);
            splitDS.Panel1.Controls.Add(pnlHDContainer);

            // Phần dưới: Chi tiết mặt hàng của hóa đơn đang chọn + Nút thao tác
            var pnlChiTietContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(12) };
            pnlChiTietContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlChiTietContainer.Width - 1, pnlChiTietContainer.Height - 1);
            };

            var pnlThaoTacHD = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.Transparent };
            btnXacNhanHD = new Button { Text = "✓ Xác nhận hoàn tất", Location = new Point(0, 8), Size = new Size(180, 38) };
            ThemeHelper.ApDungButtonThanhCong(btnXacNhanHD);
            btnXacNhanHD.Height = 38;
            btnXacNhanHD.Click += BtnXacNhanHD_Click;

            btnHuyHD = new Button { Text = "✕ Hủy hóa đơn", Location = new Point(195, 8), Size = new Size(150, 38) };
            ThemeHelper.ApDungButtonNguyHiem(btnHuyHD);
            btnHuyHD.Height = 38;
            btnHuyHD.Click += BtnHuyHD_Click;

            pnlThaoTacHD.Controls.Add(btnXacNhanHD);
            pnlThaoTacHD.Controls.Add(btnHuyHD);

            dgvChiTiet = new DataGridView { Dock = DockStyle.Fill };
            ThemeHelper.ChuanHoaDataGridView(dgvChiTiet);
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSP", HeaderText = "Mã SP", FillWeight = 20 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSanPham", HeaderText = "Tên mặt hàng", FillWeight = 50 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonViTinh", HeaderText = "ĐVT", FillWeight = 20 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "SL", FillWeight = 20 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Đơn giá (đ)", FillWeight = 30 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "Thành tiền (đ)", FillWeight = 35 });

            var lblTieuDeCT = new Label
            {
                Text = "CHI TIẾT MẶT HÀNG TRONG HÓA ĐƠN ĐANG CHỌN:",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChuPhu,
                Dock = DockStyle.Top,
                Height = 26
            };

            pnlChiTietContainer.Controls.Add(dgvChiTiet);
            pnlChiTietContainer.Controls.Add(lblTieuDeCT);
            pnlChiTietContainer.Controls.Add(pnlThaoTacHD);

            splitDS.Panel2.Controls.Add(pnlChiTietContainer);
            tabDanhSach.Controls.Add(splitDS);
        }

        private void KhoiTaoTabLapHoaDon()
        {
            tabLapHoaDon.BackColor = ThemeHelper.MauNenUngDung;
            tabLapHoaDon.Padding = new Padding(16);

            var pnlLapContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.MauNenNoiDung,
                Padding = new Padding(24),
                AutoScroll = true
            };
            pnlLapContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlLapContainer.Width - 1, pnlLapContainer.Height - 1);
            };

            var lblLapTieuDe = new Label
            {
                Text = "TẠO HÓA ĐƠN BÁN HÀNG",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 16),
                AutoSize = true
            };

            // Chọn khách hàng
            var lblKH = new Label { Text = "Khách hàng mua:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(20, 56), AutoSize = true };
            cboKhachHang = new ComboBox { Location = new Point(20, 80), Size = new Size(340, 32), Font = ThemeHelper.FontNoiDung, DropDownStyle = ComboBoxStyle.DropDownList };

            // Ghi chú
            var lblGC = new Label { Text = "Ghi chú hóa đơn:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(380, 56), AutoSize = true };
            txtGhiChu = new TextBox { Location = new Point(380, 80), Size = new Size(420, 32), Font = ThemeHelper.FontNoiDung };
            ThemeHelper.ChuanHoaTextBox(txtGhiChu);

            // Thêm mặt hàng
            var lblChonSP = new Label { Text = "Chọn sản phẩm:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(20, 126), AutoSize = true };
            cboSanPham = new ComboBox { Location = new Point(20, 150), Size = new Size(340, 32), Font = ThemeHelper.FontNoiDung, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblSL = new Label { Text = "Số lượng:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(380, 126), AutoSize = true };
            numSoLuong = new NumericUpDown { Location = new Point(380, 150), Size = new Size(110, 32), Font = ThemeHelper.FontNoiDung, Minimum = 1, Maximum = 1000, Value = 1 };

            btnThemSanPham = new Button { Text = "+ Thêm vào đơn", Location = new Point(505, 148), Size = new Size(140, 34) };
            ThemeHelper.ApDungButtonChinh(btnThemSanPham);
            btnThemSanPham.Height = 34;
            btnThemSanPham.Click += BtnThemSanPham_Click;

            btnXoaDong = new Button { Text = "✕ Xóa dòng chọn", Location = new Point(655, 148), Size = new Size(145, 34) };
            ThemeHelper.ApDungButtonPhu(btnXoaDong);
            btnXoaDong.Height = 34;
            btnXoaDong.Click += BtnXoaDong_Click;

            // Bảng danh sách dòng hóa đơn mới
            dgvDongChiTietMoi = new DataGridView
            {
                Location = new Point(20, 195),
                Size = new Size(780, 260),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            ThemeHelper.ChuanHoaDataGridView(dgvDongChiTietMoi);
            dgvDongChiTietMoi.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSanPham", HeaderText = "Mã SP", FillWeight = 20 });
            dgvDongChiTietMoi.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSanPham", HeaderText = "Tên sản phẩm", FillWeight = 50 });
            dgvDongChiTietMoi.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonViTinh", HeaderText = "ĐVT", FillWeight = 20 });
            dgvDongChiTietMoi.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "Số lượng", FillWeight = 20 });
            dgvDongChiTietMoi.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Đơn giá", FillWeight = 30 });
            dgvDongChiTietMoi.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "Thành tiền", FillWeight = 35 });

            // Tổng tiền & Lưu
            lblTongTienLap = new Label
            {
                Text = "TỔNG TIỀN: 0 đ",
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = ThemeHelper.MauChinh,
                Location = new Point(20, 470),
                AutoSize = true
            };

            btnLuuHoaDon = new Button
            {
                Text = "💾  Lưu và Xuất hóa đơn",
                Location = new Point(560, 465),
                Size = new Size(240, ThemeHelper.ChieuCaoButtonChuan)
            };
            ThemeHelper.ApDungButtonThanhCong(btnLuuHoaDon);
            btnLuuHoaDon.Click += BtnLuuHoaDon_Click;

            pnlLapContainer.Controls.Add(lblLapTieuDe);
            pnlLapContainer.Controls.Add(lblKH);
            pnlLapContainer.Controls.Add(cboKhachHang);
            pnlLapContainer.Controls.Add(lblGC);
            pnlLapContainer.Controls.Add(txtGhiChu);
            pnlLapContainer.Controls.Add(lblChonSP);
            pnlLapContainer.Controls.Add(cboSanPham);
            pnlLapContainer.Controls.Add(lblSL);
            pnlLapContainer.Controls.Add(numSoLuong);
            pnlLapContainer.Controls.Add(btnThemSanPham);
            pnlLapContainer.Controls.Add(btnXoaDong);
            pnlLapContainer.Controls.Add(dgvDongChiTietMoi);
            pnlLapContainer.Controls.Add(lblTongTienLap);
            pnlLapContainer.Controls.Add(btnLuuHoaDon);

            tabLapHoaDon.Controls.Add(pnlLapContainer);

            // Nạp danh sách Khách hàng & Sản phẩm vào ComboBox
            NapComboKhachHangVaSanPham();
        }

        private void NapComboKhachHangVaSanPham()
        {
            _dsKhachHang = _khachHangBLL.LayDanhSach();
            cboKhachHang.Items.Clear();
            cboKhachHang.Items.Add("-- Khách vãng lai --");
            foreach (var kh in _dsKhachHang)
            {
                cboKhachHang.Items.Add($"{kh.HoTen} ({kh.SoDienThoai ?? "Không có SĐT"})");
            }
            cboKhachHang.SelectedIndex = 0;

            _dsSanPham = _sanPhamBLL.LayDanhSach();
            cboSanPham.Items.Clear();
            foreach (var sp in _dsSanPham)
            {
                if (sp.TrangThai == "DangKinhDoanh")
                {
                    cboSanPham.Items.Add($"[{sp.MaSanPham}] {sp.TenSanPham} - {sp.DonGia:N0}đ/{sp.DonViTinh}");
                }
            }
            if (cboSanPham.Items.Count > 0) cboSanPham.SelectedIndex = 0;
        }

        private void TaiDanhSachHoaDon()
        {
            dgvHoaDon.Rows.Clear();
            var ds = _hoaDonBLL.LayDanhSach(txtTimKiem.Text, _trangThaiLocHienTai);
            foreach (var hd in ds)
            {
                dgvHoaDon.Rows.Add(
                    $"#{hd.MaHoaDon:D5}",
                    hd.TenKhachHang ?? "Khách vãng lai",
                    hd.TenNhanVien ?? "",
                    hd.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                    hd.TongSoLuong,
                    $"{hd.TongTien:N0}",
                    hd.TenTrangThai);
                dgvHoaDon.Rows[dgvHoaDon.Rows.Count - 1].Tag = hd.TrangThai;
            }
        }

        /// <summary>
        /// Cho phép Dashboard hoặc màn hình khác kích hoạt bộ lọc và tab tương ứng
        /// </summary>
        public void ApDungBoLoc(string? trangThai = null, string? tuKhoa = null, bool moTabLapHoaDon = false)
        {
            if (moTabLapHoaDon)
            {
                tabHoaDon.SelectedTab = tabLapHoaDon;
                return;
            }

            tabHoaDon.SelectedTab = tabDanhSach;

            if (tuKhoa != null)
            {
                txtTimKiem.Text = tuKhoa;
            }

            if (!string.IsNullOrEmpty(trangThai))
            {
                _trangThaiLocHienTai = trangThai;
                cboLocTrangThai.SelectedIndex = trangThai switch
                {
                    "DangLap" => 1,
                    "DaXacNhan" => 2,
                    "DaHuy" => 3,
                    _ => 0
                };
            }
            else
            {
                _trangThaiLocHienTai = null;
                cboLocTrangThai.SelectedIndex = 0;
            }

            TaiDanhSachHoaDon();
        }

        private void DgvHoaDon_SelectionChanged(object? sender, EventArgs e)
        {
            dgvChiTiet.Rows.Clear();
            if (dgvHoaDon.CurrentRow == null || dgvHoaDon.CurrentRow.Index < 0) return;

            string maHDStr = dgvHoaDon.CurrentRow.Cells["MaHoaDon"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maHDStr)) return;

            int maHD = int.Parse(maHDStr.Replace("#", ""));
            var dsCT = _hoaDonBLL.LayChiTietHoaDon(maHD);
            foreach (var ct in dsCT)
            {
                dgvChiTiet.Rows.Add(ct.MaSanPham, ct.TenSanPham, ct.DonViTinh, ct.SoLuong, $"{ct.DonGia:N0}", $"{ct.ThanhTien:N0}");
            }

            string rawTrangThai = dgvHoaDon.CurrentRow.Tag?.ToString() ?? "";
            string trangThaiText = dgvHoaDon.CurrentRow.Cells["TrangThai"].Value?.ToString() ?? "";
            bool laDangLap = rawTrangThai == "DangLap" || trangThaiText == "Đang lập" || trangThaiText == "DangLap";

            btnXacNhanHD.Enabled = laDangLap;
            btnHuyHD.Enabled = laDangLap;
        }

        private void BtnThemSanPham_Click(object? sender, EventArgs e)
        {
            if (cboSanPham.SelectedIndex < 0) return;

            // Tìm sản phẩm đang chọn
            string itemStr = cboSanPham.SelectedItem?.ToString() ?? "";
            int startIdx = itemStr.IndexOf('[');
            int endIdx = itemStr.IndexOf(']');
            if (startIdx < 0 || endIdx < 0) return;

            int maSP = int.Parse(itemStr.Substring(startIdx + 1, endIdx - startIdx - 1));
            var sp = _dsSanPham.Find(x => x.MaSanPham == maSP);
            if (sp == null) return;

            int sl = (int)numSoLuong.Value;
            var dongCoSan = _dsChiTietMoi.Find(x => x.MaSanPham == maSP);
            if (dongCoSan != null)
            {
                dongCoSan.SoLuong += sl;
            }
            else
            {
                _dsChiTietMoi.Add(new ChiTietHoaDonDTO
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    DonViTinh = sp.DonViTinh,
                    SoLuong = sl,
                    DonGia = sp.DonGia
                });
            }

            CapNhatBangDongChiTietMoi();
        }

        private void BtnXoaDong_Click(object? sender, EventArgs e)
        {
            if (dgvDongChiTietMoi.CurrentRow == null || dgvDongChiTietMoi.CurrentRow.Index < 0) return;
            int idx = dgvDongChiTietMoi.CurrentRow.Index;
            if (idx >= 0 && idx < _dsChiTietMoi.Count)
            {
                _dsChiTietMoi.RemoveAt(idx);
                CapNhatBangDongChiTietMoi();
            }
        }

        private void CapNhatBangDongChiTietMoi()
        {
            dgvDongChiTietMoi.Rows.Clear();
            decimal tongTien = 0;
            foreach (var ct in _dsChiTietMoi)
            {
                dgvDongChiTietMoi.Rows.Add(ct.MaSanPham, ct.TenSanPham, ct.DonViTinh, ct.SoLuong, $"{ct.DonGia:N0}", $"{ct.ThanhTien:N0}");
                tongTien += ct.ThanhTien;
            }
            lblTongTienLap.Text = $"TỔNG TIỀN: {tongTien:N0} đ";
        }

        private void BtnLuuHoaDon_Click(object? sender, EventArgs e)
        {
            if (_dsChiTietMoi.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một mặt hàng vào hóa đơn.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? maKH = null;
            if (cboKhachHang.SelectedIndex > 0 && cboKhachHang.SelectedIndex - 1 < _dsKhachHang.Count)
            {
                maKH = _dsKhachHang[cboKhachHang.SelectedIndex - 1].MaKhachHang;
            }

            var kq = _hoaDonBLL.LapHoaDon(maKH, txtGhiChu.Text, _dsChiTietMoi);
            if (kq.ThanhCong)
            {
                MessageBox.Show(kq.ThongBao, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dsChiTietMoi.Clear();
                CapNhatBangDongChiTietMoi();
                txtGhiChu.Clear();
                cboKhachHang.SelectedIndex = 0;
                TaiDanhSachHoaDon();
                tabHoaDon.SelectedTab = tabDanhSach;
            }
            else
            {
                MessageBox.Show(kq.ThongBao, "Lỗi Lưu Hóa Đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXacNhanHD_Click(object? sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;
            string? val = dgvHoaDon.CurrentRow.Cells["MaHoaDon"].Value?.ToString();
            if (string.IsNullOrEmpty(val)) return;

            int maHD = int.Parse(val.Replace("#", ""));
            var kq = _hoaDonBLL.XacNhanHoaDon(maHD);
            MessageBox.Show(kq.ThongBao, "Thông Báo", MessageBoxButtons.OK, kq.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (kq.ThanhCong) TaiDanhSachHoaDon();
        }

        private void BtnHuyHD_Click(object? sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;
            string? val = dgvHoaDon.CurrentRow.Cells["MaHoaDon"].Value?.ToString();
            if (string.IsNullOrEmpty(val)) return;

            int maHD = int.Parse(val.Replace("#", ""));
            var kq = _hoaDonBLL.HuyHoaDon(maHD);
            MessageBox.Show(kq.ThongBao, "Thông Báo", MessageBoxButtons.OK, kq.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (kq.ThanhCong) TaiDanhSachHoaDon();
        }
    }
}
