
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
        private readonly SanPhamBLL _sanPhamBLL = new();

        private TextBox txtTimKiem = null!;
        private DataGridView dgvSanPham = null!;
        private Label lblSoLuong = null!;

        private readonly Font fontNoiDung = new("Segoe UI", 10F);
        private readonly Font fontNut = new("Segoe UI", 9.5F, FontStyle.Bold);

        private static readonly Color MauNen = Color.FromArgb(245, 247, 250);
        private static readonly Color MauVien = Color.FromArgb(203, 213, 225);
        private static readonly Color MauChu = Color.FromArgb(30, 41, 59);
        private static readonly Color MauPhu = Color.FromArgb(100, 116, 139);
        private static readonly Color MauChinh = Color.FromArgb(37, 99, 235);

        public UCSanPham()
        {
            KhoiTaoGiaoDien();
            TaiDanhSachSanPham();
        }

        private void KhoiTaoGiaoDien()
        {
            Dock = DockStyle.Fill;
            BackColor = MauNen;
            Padding = new Padding(24);
            AutoScaleMode = AutoScaleMode.Dpi;

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));

            // TIÊU ĐỀ
            var pnlTieuDe = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0),
                BackColor = Color.Transparent
            };

            pnlTieuDe.RowStyles.Add(new RowStyle(SizeType.Percent, 62));
            pnlTieuDe.RowStyles.Add(new RowStyle(SizeType.Percent, 38));

            var lblTieuDe = new Label
            {
                Text = "QUẢN LÝ SẢN PHẨM",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                ForeColor = MauChu,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0)
            };

            var lblMoTa = new Label
            {
                Text = "Danh sách và thông tin sản phẩm kinh doanh",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MauPhu,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(2, 0, 0, 0)
            };

            pnlTieuDe.Controls.Add(lblTieuDe, 0, 0);
            pnlTieuDe.Controls.Add(lblMoTa, 0, 1);

            // THANH CÔNG CỤ
            var pnlCongCu = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 10)
            };

            var flowCongCu = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105));
            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165));
            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175));
            flowCongCu.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            txtTimKiem = new TextBox
            {
                PlaceholderText = "Nhập tên sản phẩm...",
                Font = new Font("Segoe UI", 9.5F),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = false,
                Dock = DockStyle.Fill,
                Margin = new Padding(4, 3, 8, 3)
            };

            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    TaiDanhSachSanPham(txtTimKiem.Text.Trim());
                }
            };

            var btnTimKiem = TaoNut("Tìm kiếm");
            btnTimKiem.Dock = DockStyle.Fill;
            btnTimKiem.Margin = new Padding(3, 1, 3, 1);
            btnTimKiem.Click += (s, e) =>
                TaiDanhSachSanPham(txtTimKiem.Text.Trim());

            var btnLamMoi = TaoNut("Làm mới");
            btnLamMoi.Dock = DockStyle.Fill;
            btnLamMoi.Margin = new Padding(3, 1, 3, 1);
            btnLamMoi.Click += (s, e) =>
            {
                txtTimKiem.Clear();
                TaiDanhSachSanPham();
                txtTimKiem.Focus();
            };

            var btnThem = TaoNut("＋ Thêm sản phẩm", true);
            btnThem.Dock = DockStyle.Fill;
            btnThem.Margin = new Padding(3, 1, 3, 1);
            btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Chỉ Quản lý được thêm sản phẩm
            btnThem.Visible = Session.LaQuanLy;
            btnThem.Click += (s, e) => ThemSanPham();

            var btnNgung = TaoNut("Ngừng kinh doanh");
            btnNgung.Dock = DockStyle.Fill;
            btnNgung.Visible = Session.LaQuanLy;
            btnNgung.Margin = new Padding(3, 1, 3, 1);
            btnNgung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNgung.BackColor = Color.FromArgb(220, 38, 38);
            btnNgung.ForeColor = Color.White;
            btnNgung.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
            btnNgung.Click += (s, e) => NgungKinhDoanh();

            flowCongCu.Controls.Add(txtTimKiem, 0, 0);
            flowCongCu.Controls.Add(btnTimKiem, 1, 0);
            flowCongCu.Controls.Add(btnLamMoi, 2, 0);
            flowCongCu.Controls.Add(btnThem, 3, 0);
            flowCongCu.Controls.Add(btnNgung, 4, 0);

            pnlCongCu.Controls.Add(flowCongCu);

            // BẢNG SẢN PHẨM
            dgvSanPham = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                ScrollBars = ScrollBars.Both,
                Font = fontNoiDung,
                GridColor = MauVien,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersHeight = 46,
                ColumnHeadersHeightSizeMode =
                    DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 46 },
                EnableHeadersVisualStyles = false
            };

            dgvSanPham.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(241, 245, 249);
            dgvSanPham.ColumnHeadersDefaultCellStyle.ForeColor = MauChu;
            dgvSanPham.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSanPham.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvSanPham.ColumnHeadersDefaultCellStyle.Padding =
                new Padding(10, 0, 10, 0);
            dgvSanPham.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.False;
            dgvSanPham.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(241, 245, 249);
            dgvSanPham.ColumnHeadersDefaultCellStyle.SelectionForeColor = MauChu;

            dgvSanPham.DefaultCellStyle.BackColor = Color.White;
            dgvSanPham.DefaultCellStyle.ForeColor =
                Color.FromArgb(51, 65, 85);
            dgvSanPham.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);
            dgvSanPham.DefaultCellStyle.SelectionForeColor = MauChu;
            dgvSanPham.DefaultCellStyle.Padding =
                new Padding(10, 0, 10, 0);
            dgvSanPham.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvSanPham.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvSanPham.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);

            dgvSanPham.Columns.Add("MaSanPham", "Mã SP");
            dgvSanPham.Columns.Add("TenSanPham", "Tên sản phẩm");
            dgvSanPham.Columns.Add("DonViTinh", "Đơn vị tính");
            dgvSanPham.Columns.Add("DonGia", "Đơn giá");
            dgvSanPham.Columns.Add("TrangThai", "Trạng thái");

            dgvSanPham.Columns["MaSanPham"]!.FillWeight = 75;
            dgvSanPham.Columns["TenSanPham"]!.FillWeight = 155;
            dgvSanPham.Columns["DonViTinh"]!.FillWeight = 120;
            dgvSanPham.Columns["DonGia"]!.FillWeight = 110;
            dgvSanPham.Columns["TrangThai"]!.FillWeight = 125;

            dgvSanPham.Columns["MaSanPham"]!.MinimumWidth = 75;
            dgvSanPham.Columns["TenSanPham"]!.MinimumWidth = 150;
            dgvSanPham.Columns["DonViTinh"]!.MinimumWidth = 125;
            dgvSanPham.Columns["DonGia"]!.MinimumWidth = 110;
            dgvSanPham.Columns["TrangThai"]!.MinimumWidth = 125;

            foreach (DataGridViewColumn col in dgvSanPham.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                col.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }

            dgvSanPham.Columns["MaSanPham"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvSanPham.Columns["DonGia"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvSanPham.Columns["TrangThai"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // HIỂN THỊ MÀU TRẠNG THÁI
            dgvSanPham.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 ||
                    dgvSanPham.Columns[e.ColumnIndex].Name != "TrangThai")
                    return;

                string trangThai = e.Value?.ToString() ?? "";

                if (trangThai == "Đang kinh doanh")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    e.CellStyle.BackColor = Color.FromArgb(220, 252, 231);
                    e.CellStyle.SelectionForeColor = Color.FromArgb(22, 101, 52);
                    e.CellStyle.SelectionBackColor = Color.FromArgb(187, 247, 208);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (trangThai == "Ngừng kinh doanh")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.SelectionForeColor = Color.FromArgb(153, 27, 27);
                    e.CellStyle.SelectionBackColor = Color.FromArgb(254, 202, 202);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            };

            // THANH CUỐI BẢNG
            var pnlDuoi = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 5, 0, 0)
            };

            pnlDuoi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pnlDuoi.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155));
            pnlDuoi.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            lblSoLuong = new Label
            {
                Text = "Tổng số: 0 sản phẩm",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(2, 0, 0, 0)
            };

            var btnSua = TaoNut("✎  Cập nhật giá", true);
            btnSua.Dock = DockStyle.Fill;
            btnSua.Margin = new Padding(4, 2, 0, 2);

            // Chỉ Quản lý được cập nhật giá
            btnSua.Visible = Session.LaQuanLy;
            btnSua.Click += (s, e) => CapNhatGia();

            pnlDuoi.Controls.Add(lblSoLuong, 0, 0);
            pnlDuoi.Controls.Add(btnSua, 1, 0);

            layout.Controls.Add(pnlTieuDe, 0, 0);
            layout.Controls.Add(pnlCongCu, 0, 1);
            layout.Controls.Add(dgvSanPham, 0, 2);
            layout.Controls.Add(pnlDuoi, 0, 3);

            Controls.Add(layout);
        }

        private Button TaoNut(string noiDung, bool nutChinh = false)
        {
            var btn = new Button
            {
                Text = noiDung,
                Size = new Size(115, 38),
                Font = fontNut,
                BackColor = nutChinh ? MauChinh : Color.FromArgb(241, 245, 249),
                ForeColor = nutChinh ? Color.White : MauChu,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(0),
                UseVisualStyleBackColor = false,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };

            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = nutChinh ? MauChinh : MauVien;

            return btn;
        }

        private void TaiDanhSachSanPham(string tuKhoa = "")
        {
            try
            {
                var danhSach = _sanPhamBLL.LayDanhSach(tuKhoa);

                dgvSanPham.Rows.Clear();

                foreach (var sp in danhSach)
                {
                    int rowIndex = dgvSanPham.Rows.Add(
                        sp.MaSanPham,
                        sp.TenSanPham,
                        sp.DonViTinh,
                        sp.DonGia.ToString("N0") + " đ",
                        sp.TrangThai == "DangKinhDoanh"
                            ? "Đang kinh doanh"
                            : "Ngừng kinh doanh"
                    );

                    dgvSanPham.Rows[rowIndex].Tag = sp.DonGia;
                }

                lblSoLuong.Text = $"Tổng số: {danhSach.Count} sản phẩm";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách sản phẩm.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ThemSanPham()
        {
            using var form = new Form
            {
                Text = "Thêm sản phẩm",
                Size = new Size(500, 470),
                MinimumSize = new Size(500, 470),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White,
                Font = fontNoiDung,
                AutoScaleMode = AutoScaleMode.Dpi
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(26, 20, 26, 16),
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.White
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < 4; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));

            var txtTen = TaoONhap(layout, 0, "Tên sản phẩm:", "");
            var txtMoTa = TaoONhap(layout, 1, "Mô tả:", "");
            var txtDonVi = TaoONhap(layout, 2, "Đơn vị tính:", "");
            var txtGia = TaoONhap(layout, 3, "Đơn giá:", "");

            var pnlNut = TaoKhungNut();

            var btnHuy = TaoNutTrongDialog("Hủy");
            btnHuy.DialogResult = DialogResult.Cancel;

            var btnLuu = TaoNutTrongDialog("Lưu", true);

            btnLuu.Click += (s, e) =>
            {
                if (!decimal.TryParse(txtGia.Text.Trim(), out decimal gia) || gia < 0)
                {
                    MessageBox.Show(
                        "Đơn giá phải là số không âm.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtGia.Focus();
                    return;
                }

                try
                {
                    var sp = new SanPhamDTO
                    {
                        TenSanPham = txtTen.Text.Trim(),
                        MoTa = txtMoTa.Text.Trim(),
                        DonViTinh = txtDonVi.Text.Trim(),
                        DonGia = gia
                    };

                    KetQuaThaoTacDTO ketQua = _sanPhamBLL.ThemSanPham(sp);

                    MessageBox.Show(
                        ketQua.ThongBao,
                        ketQua.ThanhCong ? "Thành công" : "Thông báo",
                        MessageBoxButtons.OK,
                        ketQua.ThanhCong
                            ? MessageBoxIcon.Information
                            : MessageBoxIcon.Warning
                    );

                    if (ketQua.ThanhCong)
                    {
                        form.DialogResult = DialogResult.OK;
                        form.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Không thể thêm sản phẩm.\n" + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            };

            pnlNut.Controls.Add(btnHuy);
            pnlNut.Controls.Add(btnLuu);
            layout.Controls.Add(pnlNut, 0, 4);

            form.Controls.Add(layout);
            form.AcceptButton = btnLuu;
            form.CancelButton = btnHuy;

            if (form.ShowDialog() == DialogResult.OK)
                TaiDanhSachSanPham(txtTimKiem.Text.Trim());
        }

        private void CapNhatGia()
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.");
                return;
            }

            int ma = Convert.ToInt32(
                dgvSanPham.CurrentRow.Cells["MaSanPham"].Value
            );

            string ten = dgvSanPham.CurrentRow.Cells["TenSanPham"].Value?.ToString() ?? "";

            string giaHienTai =
                dgvSanPham.CurrentRow.Cells["DonGia"].Value?.ToString() ?? "";

            string trangThaiHienTai =
                dgvSanPham.CurrentRow.Cells["TrangThai"].Value?.ToString() ?? "";

            string trangThaiDb = trangThaiHienTai == "Đang kinh doanh"
                ? "DangKinhDoanh"
                : "NgungKinhDoanh";

            if (!HienThiHopThoaiCapNhatGia(ten, giaHienTai, out decimal donGia))
                return;

            try
            {
                KetQuaThaoTacDTO ketQua =
                    _sanPhamBLL.CapNhatGiaVaTrangThai(ma, donGia, trangThaiDb);

                MessageBox.Show(
                    ketQua.ThongBao,
                    ketQua.ThanhCong ? "Thành công" : "Thông báo",
                    MessageBoxButtons.OK,
                    ketQua.ThanhCong
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning
                );

                if (ketQua.ThanhCong)
                    TaiDanhSachSanPham(txtTimKiem.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật giá sản phẩm.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool HienThiHopThoaiCapNhatGia(
            string ten,
            string giaHienTai,
            out decimal donGia)
        {
            donGia = 0;
            decimal giaNhap = 0;

            using var form = new Form
            {
                Text = "Cập nhật giá sản phẩm",
                Size = new Size(460, 260),
                MinimumSize = new Size(460, 260),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White,
                Font = fontNoiDung,
                AutoScaleMode = AutoScaleMode.Dpi
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.White
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

            var lblTen = new Label
            {
                Text = "Sản phẩm: " + ten,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = MauChu,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblGiaCu = new Label
            {
                Text = "Giá hiện tại: " + giaHienTai,
                Dock = DockStyle.Fill,
                Font = fontNoiDung,
                ForeColor = MauPhu,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var pnlNhap = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0, 4, 0, 4)
            };

            pnlNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            pnlNhap.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var lblNhap = new Label
            {
                Text = "Nhập đơn giá mới:",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = MauChu,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var txtGia = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = fontNoiDung,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 2, 0, 2),
                AutoSize = false
            };

            pnlNhap.Controls.Add(lblNhap, 0, 0);
            pnlNhap.Controls.Add(txtGia, 0, 1);

            var pnlNut = TaoKhungNut();

            var btnHuy = TaoNutTrongDialog("Hủy");
            btnHuy.DialogResult = DialogResult.Cancel;

            var btnLuu = TaoNutTrongDialog("Cập nhật", true);

            btnLuu.Click += (s, e) =>
            {
                if (!decimal.TryParse(txtGia.Text.Trim(), out decimal gia) || gia < 0)
                {
                    MessageBox.Show(
                        "Đơn giá phải là số không âm.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtGia.Focus();
                    return;
                }

                giaNhap = gia;
                form.DialogResult = DialogResult.OK;
                form.Close();
            };

            pnlNut.Controls.Add(btnHuy);
            pnlNut.Controls.Add(btnLuu);

            layout.Controls.Add(lblTen, 0, 0);
            layout.Controls.Add(lblGiaCu, 0, 1);
            layout.Controls.Add(pnlNhap, 0, 2);
            layout.Controls.Add(pnlNut, 0, 3);

            form.Controls.Add(layout);
            form.AcceptButton = btnLuu;
            form.CancelButton = btnHuy;

            if (form.ShowDialog() != DialogResult.OK)
                return false;

            donGia = giaNhap;
            return true;
        }

        private void NgungKinhDoanh()
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần ngừng kinh doanh.");
                return;
            }

            int ma = Convert.ToInt32(
                dgvSanPham.CurrentRow.Cells["MaSanPham"].Value
            );

            string ten =
                dgvSanPham.CurrentRow.Cells["TenSanPham"].Value?.ToString() ?? "";

            string trangThaiHienTai =
                dgvSanPham.CurrentRow.Cells["TrangThai"].Value?.ToString() ?? "";

            if (trangThaiHienTai == "Ngừng kinh doanh")
            {
                MessageBox.Show(
                    "Sản phẩm này đã ngừng kinh doanh.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            var xacNhan = MessageBox.Show(
                $"Bạn có chắc muốn ngừng kinh doanh sản phẩm '{ten}' không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (xacNhan != DialogResult.Yes)
                return;

            // Lấy giá gốc đã lưu trong Tag của dòng sản phẩm
            decimal donGiaHienTai =
                Convert.ToDecimal(dgvSanPham.CurrentRow.Tag);

            try
            {
                KetQuaThaoTacDTO ketQua =
                    _sanPhamBLL.CapNhatGiaVaTrangThai(
                        ma,
                        donGiaHienTai,
                        "NgungKinhDoanh"
                    );

                MessageBox.Show(
                    ketQua.ThongBao,
                    ketQua.ThanhCong ? "Thành công" : "Thông báo",
                    MessageBoxButtons.OK,
                    ketQua.ThanhCong
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning
                );

                if (ketQua.ThanhCong)
                    TaiDanhSachSanPham(txtTimKiem.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật trạng thái sản phẩm.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private TextBox TaoONhap(
            TableLayoutPanel layout,
            int dong,
            string nhan,
            string giaTri)
        {
            var pnl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0, 3, 0, 3),
                Padding = new Padding(0),
                BackColor = Color.White
            };

            pnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            pnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var lbl = new Label
            {
                Text = nhan,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = MauChu,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0)
            };

            var txt = new TextBox
            {
                Text = giaTri,
                Dock = DockStyle.Fill,
                Font = fontNoiDung,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 2, 0, 2),
                AutoSize = false
            };

            pnl.Controls.Add(lbl, 0, 0);
            pnl.Controls.Add(txt, 0, 1);
            layout.Controls.Add(pnl, 0, dong);

            return txt;
        }

        private FlowLayoutPanel TaoKhungNut()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = Color.White,
                Padding = new Padding(0, 6, 0, 0),
                Margin = new Padding(0)
            };
        }

        private Button TaoNutTrongDialog(
            string noiDung,
            bool nutChinh = false)
        {
            var btn = new Button
            {
                Text = noiDung,
                Size = new Size(105, 36),
                Font = fontNut,
                BackColor = nutChinh
                    ? MauChinh
                    : Color.FromArgb(241, 245, 249),
                ForeColor = nutChinh ? Color.White : MauChu,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 0, 0, 0),
                UseVisualStyleBackColor = false
            };

            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = nutChinh ? MauChinh : MauVien;

            return btn;
        }
    }
}
