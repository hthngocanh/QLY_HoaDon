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
        private Button btnLamMoi = null!;
        private Button btnThemNhanVien = null!;

        private Panel pnlBangContainer = null!;
        private DataGridView dgvNhanVien = null!;

        // Debounce timer cho Live Search (300ms)
        private readonly System.Windows.Forms.Timer _debounceTimer = new();

        public UCNhanVien()
        {
            KhoiTaoGiaoDien();
            KhoiTaoLiveSearchTimer();
            TaiDanhSach();
        }

        private void KhoiTaoLiveSearchTimer()
        {
            _debounceTimer.Interval = 300;
            _debounceTimer.Tick += (s, e) =>
            {
                _debounceTimer.Stop();
                TaiDanhSach();
            };
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.FontNoiDung;
            this.Padding = new Padding(24, 20, 24, 20);

            // 1. Top Panel: Toolbar trên cùng 1 hàng
            // Gồm: Ô tìm kiếm Live Search -> Nút Làm mới -> Nút + Thêm mới nhân viên
            pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = ThemeHelper.MauNenNoiDung,
                Padding = new Padding(16, 12, 16, 12)
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
                Location = new Point(16, 20),
                AutoSize = true
            };

            txtTimKiem = new TextBox
            {
                Location = new Point(115, 16),
                Size = new Size(340, 32),
                Font = ThemeHelper.FontNoiDung,
                PlaceholderText = "Tìm theo mã NV, họ tên, username..."
            };
            // Live Search: tự động lọc sau 300ms ngừng gõ
            txtTimKiem.TextChanged += (s, e) =>
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            };

            btnLamMoi = new Button
            {
                Text = "Tải lại",
                Location = new Point(470, 14),
                Size = new Size(95, 36)
            };
            ThemeHelper.ApDungButtonPhu(btnLamMoi);
            btnLamMoi.Height = 36;
            btnLamMoi.Click += (s, e) =>
            {
                _debounceTimer.Stop();
                txtTimKiem.Clear();
                TaiDanhSach();
            };

            btnThemNhanVien = new Button
            {
                Text = "+ Thêm mới nhân viên",
                Location = new Point(578, 14),
                Size = new Size(185, 36)
            };
            ThemeHelper.ApDungButtonThanhCong(btnThemNhanVien);
            btnThemNhanVien.Height = 36;
            btnThemNhanVien.Click += BtnThemNhanVien_Click;

            pnlTop.Controls.Add(lblTim);
            pnlTop.Controls.Add(txtTimKiem);
            pnlTop.Controls.Add(btnLamMoi);
            pnlTop.Controls.Add(btnThemNhanVien);

            var pnlDem = new Panel { Dock = DockStyle.Top, Height = 14 };

            // 2. DataGridView & Container: chiếm toàn bộ diện tích còn lại
            pnlBangContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            pnlBangContainer.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBangContainer.Width - 1, pnlBangContainer.Height - 1);
            };

            dgvNhanVien = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = false, // Cho phép bấm DataGridViewButtonColumn
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            ThemeHelper.ChuanHoaDataGridView(dgvNhanVien);

            KhoiTaoCacCotDataGridView();

            dgvNhanVien.CellClick += DgvNhanVien_CellClick;
            dgvNhanVien.CellContentClick += DgvNhanVien_CellContentClick;
            dgvNhanVien.CellFormatting += DgvNhanVien_CellFormatting;

            pnlBangContainer.Controls.Add(dgvNhanVien);

            this.Controls.Add(pnlBangContainer);
            this.Controls.Add(pnlDem);
            this.Controls.Add(pnlTop);
        }

        private void KhoiTaoCacCotDataGridView()
        {
            dgvNhanVien.Columns.Clear();
            dgvNhanVien.AutoGenerateColumns = false;

            // 1. Mã NV
            var colMaNV = new DataGridViewTextBoxColumn
            {
                Name = "MaNhanVien",
                HeaderText = "Mã NV",
                FillWeight = 14,
                ReadOnly = true
            };

            // 2. Họ và tên
            var colHoTen = new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ và tên",
                FillWeight = 24,
                ReadOnly = true
            };

            // 3. Tài khoản
            var colTenDangNhap = new DataGridViewTextBoxColumn
            {
                Name = "TenDangNhap",
                HeaderText = "Tài khoản",
                FillWeight = 18,
                ReadOnly = true
            };

            // 4. Vai trò
            var colTenVaiTro = new DataGridViewTextBoxColumn
            {
                Name = "TenVaiTro",
                HeaderText = "Vai trò",
                FillWeight = 14,
                ReadOnly = true
            };

            // 5. Trạng thái nhân viên
            var colTrangThaiNV = new DataGridViewTextBoxColumn
            {
                Name = "TrangThaiNV",
                HeaderText = "Trạng thái nhân viên",
                FillWeight = 18,
                ReadOnly = true
            };

            // 6. Trạng thái tài khoản
            var colTrangThaiTK = new DataGridViewTextBoxColumn
            {
                Name = "TrangThaiTK",
                HeaderText = "Trạng thái tài khoản",
                FillWeight = 18,
                ReadOnly = true
            };

            // 7. Thao tác (Button Khóa/Mở khóa)
            var colThaoTac = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                FillWeight = 16,
                FlatStyle = FlatStyle.Flat,
                UseColumnTextForButtonValue = false
            };

            dgvNhanVien.Columns.Add(colMaNV);
            dgvNhanVien.Columns.Add(colHoTen);
            dgvNhanVien.Columns.Add(colTenDangNhap);
            dgvNhanVien.Columns.Add(colTenVaiTro);
            dgvNhanVien.Columns.Add(colTrangThaiNV);
            dgvNhanVien.Columns.Add(colTrangThaiTK);
            dgvNhanVien.Columns.Add(colThaoTac);
        }

        private void TaiDanhSach()
        {
            dgvNhanVien.Rows.Clear();
            // Lấy danh sách qua BLL: Đã tự động lọc theo quyền (chỉ lấy vai trò Nhân viên, loại bỏ Quản lý và tài khoản đang đăng nhập)
            var ds = _bll.LayDanhSach(txtTimKiem.Text);

            foreach (var nv in ds)
            {
                string buttonText = "";
                if (nv.MaTaiKhoan.HasValue)
                {
                    buttonText = (nv.TrangThaiTaiKhoan == "BiKhoa") ? "Mở khóa" : "Khóa";
                }

                int rowIdx = dgvNhanVien.Rows.Add(
                    $"NV{nv.MaNhanVien:D4}",
                    nv.HoTen,
                    nv.TenDangNhap ?? "(Chưa có)",
                    nv.TenVaiTro ?? "",
                    nv.TenTrangThaiNhanVien,
                    nv.TenTrangThaiTaiKhoan,
                    buttonText);

                dgvNhanVien.Rows[rowIdx].Tag = nv;
            }

            dgvNhanVien.ClearSelection();
        }
        private void DgvNhanVien_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhanVien.Rows.Count) return;

            // Định dạng màu sắc cột Thao tác
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                if (row.Tag is NhanVienQuanLyDTO nv && nv.MaTaiKhoan.HasValue)
                {
                    if (nv.TrangThaiTaiKhoan == "BiKhoa")
                    {
                        // Tài khoản bị khóa: Nút Mở khóa màu xanh lá
                        e.CellStyle.ForeColor = ThemeHelper.MauThanhCong;
                        e.CellStyle.SelectionForeColor = ThemeHelper.MauThanhCong;
                    }
                    else
                    {
                        // Tài khoản đang hoạt động: Nút Khóa màu đỏ
                        e.CellStyle.ForeColor = ThemeHelper.MauLoi;
                        e.CellStyle.SelectionForeColor = ThemeHelper.MauLoi;
                    }
                    e.CellStyle.Font = ThemeHelper.FontChuThichDam;
                }
            }
        }

        private void DgvNhanVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua header hoặc cột Thao tác
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "ThaoTac") return;

            var row = dgvNhanVien.Rows[e.RowIndex];
            if (row.Tag is NhanVienQuanLyDTO nv)
            {
                using var popup = new FormPopupChiTietNhanVien(nv);
                popup.ShowDialog(this.FindForm());
            }
        }

        private void DgvNhanVien_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                if (row.Tag is not NhanVienQuanLyDTO nv || !nv.MaTaiKhoan.HasValue)
                {
                    MessageBox.Show("Nhân viên này chưa có tài khoản hệ thống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 1. Kiểm tra chặn quản lý tự khóa tài khoản của chính mình (cấp UI)
                if (Session.DaDangNhap && Session.MaTaiKhoan == nv.MaTaiKhoan.Value)
                {
                    MessageBox.Show(
                        "Bạn không thể tự khóa tài khoản đang đăng nhập của chính mình.",
                        "Cảnh Báo Phân Quyền",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                bool dangBiKhoa = nv.TrangThaiTaiKhoan == "BiKhoa";
                string hanhDong = dangBiKhoa ? "MỞ KHÓA" : "KHÓA";
                string message = $"Bạn có chắc chắn muốn {hanhDong} tài khoản '{nv.TenDangNhap}' ({nv.HoTen}) không?";

                var confirm = MessageBox.Show(
                    message,
                    $"Xác Nhận {hanhDong} Tài Khoản",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                // 2. Thực hiện Khóa hoặc Mở khóa qua BLL (BLL sẽ kiểm tra vai trò thực tế từ CSDL để chống bypass)
                var kq = _bll.KhoaMoKhoaTaiKhoan(nv.MaTaiKhoan.Value, !dangBiKhoa);

                if (kq.ThanhCong)
                {
                    MessageBox.Show(kq.ThongBao, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                }
                else
                {
                    MessageBox.Show(kq.ThongBao, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnThemNhanVien_Click(object? sender, EventArgs e)
        {
            using var popupThem = new FormPopupThemNhanVien(_bll);
            if (popupThem.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                TaiDanhSach();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _debounceTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
