
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
        private readonly KhachHangBLL _khachHangBLL = new();

        private TextBox txtTimKiem = null!;
        private DataGridView dgvKhachHang = null!;
        private Label lblSoLuong = null!;

        private readonly Font fontNoiDung = new("Segoe UI", 10F);
        private readonly Font fontNut = new("Segoe UI", 9.5F, FontStyle.Bold);

        private static readonly Color MauNen = Color.FromArgb(245, 247, 250);
        private static readonly Color MauVien = Color.FromArgb(203, 213, 225);
        private static readonly Color MauChu = Color.FromArgb(30, 41, 59);
        private static readonly Color MauPhu = Color.FromArgb(100, 116, 139);
        private static readonly Color MauChinh = Color.FromArgb(37, 99, 235);

        public UCKhachHang()
        {
            KhoiTaoGiaoDien();
            TaiDanhSachKhachHang();
        }

        private void KhoiTaoGiaoDien()
        {
            Dock = DockStyle.Fill;
            BackColor = MauNen;
            Padding = new Padding(20);
            AutoScaleMode = AutoScaleMode.Dpi;

            // Bố cục chính: thanh công cụ - bảng - thanh cuối.
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

            // THANH CÔNG CỤ
            var pnlCongCu = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(12, 10, 12, 10),
                Margin = new Padding(0, 0, 0, 12)
            };

            var flowCongCu = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            flowCongCu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165));
            flowCongCu.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            txtTimKiem = new TextBox
            {
                PlaceholderText = "Nhập tên hoặc số điện thoại khách hàng...",
                Font = new Font("Segoe UI", 9.5F),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = false,
                Dock = DockStyle.Fill,
                Margin = new Padding(2, 3, 10, 3)
            };

            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    TaiDanhSachKhachHang(txtTimKiem.Text.Trim());
                }
            };

            var btnTimKiem = TaoNut("Tìm kiếm");
            btnTimKiem.Dock = DockStyle.Fill;
            btnTimKiem.Margin = new Padding(3, 1, 3, 1);
            btnTimKiem.Click += (s, e) =>
                TaiDanhSachKhachHang(txtTimKiem.Text.Trim());

            var btnLamMoi = TaoNut("Làm mới");
            btnLamMoi.Dock = DockStyle.Fill;
            btnLamMoi.Margin = new Padding(3, 1, 3, 1);
            btnLamMoi.Click += (s, e) =>
            {
                txtTimKiem.Clear();
                TaiDanhSachKhachHang();
                txtTimKiem.Focus();
            };

            var btnThem = TaoNut("＋ Thêm khách hàng", true);
            btnThem.Dock = DockStyle.Fill;
            btnThem.Margin = new Padding(3, 1, 3, 1);
            btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Nhân viên được thêm khách hàng, Quản lý chỉ xem.
            btnThem.Visible = Session.LaNhanVien;
            btnThem.Click += (s, e) => ThemKhachHang();

            flowCongCu.Controls.Add(txtTimKiem, 0, 0);
            flowCongCu.Controls.Add(btnTimKiem, 1, 0);
            flowCongCu.Controls.Add(btnLamMoi, 2, 0);
            flowCongCu.Controls.Add(btnThem, 3, 0);

            pnlCongCu.Controls.Add(flowCongCu);

            // BẢNG KHÁCH HÀNG
            dgvKhachHang = new DataGridView
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
                ColumnHeadersHeight = 44,
                ColumnHeadersHeightSizeMode =
                    DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 44 },
                EnableHeadersVisualStyles = false
            };

            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(241, 245, 249);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = MauChu;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Padding =
                new Padding(12, 0, 10, 0);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.False;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(241, 245, 249);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.SelectionForeColor = MauChu;

            dgvKhachHang.DefaultCellStyle.BackColor = Color.White;
            dgvKhachHang.DefaultCellStyle.ForeColor =
                Color.FromArgb(51, 65, 85);
            dgvKhachHang.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = MauChu;
            dgvKhachHang.DefaultCellStyle.Padding =
                new Padding(12, 0, 10, 0);
            dgvKhachHang.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvKhachHang.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);

            dgvKhachHang.Columns.Add("MaKhachHang", "Mã KH");
            dgvKhachHang.Columns.Add("HoTen", "Họ và tên");
            dgvKhachHang.Columns.Add("SoDienThoai", "Số điện thoại");
            dgvKhachHang.Columns.Add("Email", "Email");
            dgvKhachHang.Columns.Add("DiaChi", "Địa chỉ");

            dgvKhachHang.Columns["MaKhachHang"]!.FillWeight = 75;
            dgvKhachHang.Columns["HoTen"]!.FillWeight = 145;
            dgvKhachHang.Columns["SoDienThoai"]!.FillWeight = 115;
            dgvKhachHang.Columns["Email"]!.FillWeight = 150;
            dgvKhachHang.Columns["DiaChi"]!.FillWeight = 180;

            dgvKhachHang.Columns["MaKhachHang"]!.MinimumWidth = 75;
            dgvKhachHang.Columns["HoTen"]!.MinimumWidth = 130;
            dgvKhachHang.Columns["SoDienThoai"]!.MinimumWidth = 115;
            dgvKhachHang.Columns["Email"]!.MinimumWidth = 140;
            dgvKhachHang.Columns["DiaChi"]!.MinimumWidth = 150;

            foreach (DataGridViewColumn col in dgvKhachHang.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                col.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }

            dgvKhachHang.Columns["MaKhachHang"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvKhachHang.Columns["SoDienThoai"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

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
                Text = "Tổng số: 0 khách hàng",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(2, 0, 0, 0)
            };

            var btnSua = TaoNut("✎  Sửa thông tin", true);
            btnSua.Dock = DockStyle.Fill;
            btnSua.Margin = new Padding(4, 2, 0, 2);

            // Nhân viên được cập nhật khách hàng.
            btnSua.Visible = Session.LaNhanVien;
            btnSua.Click += (s, e) => SuaKhachHang();

            pnlDuoi.Controls.Add(lblSoLuong, 0, 0);
            pnlDuoi.Controls.Add(btnSua, 1, 0);

            layout.Controls.Add(pnlCongCu, 0, 0);
            layout.Controls.Add(dgvKhachHang, 0, 1);
            layout.Controls.Add(pnlDuoi, 0, 2);

            Controls.Add(layout);
        }

        private Button TaoNut(string noiDung, bool nutChinh = false)
        {
            var btn = new Button
            {
                Text = noiDung,
                Size = new Size(115, 38),
                Font = fontNut,
                BackColor = nutChinh
                    ? MauChinh
                    : Color.FromArgb(241, 245, 249),
                ForeColor = nutChinh
                    ? Color.White
                    : MauChu,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(0),
                UseVisualStyleBackColor = false,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };

            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = nutChinh
                ? MauChinh
                : MauVien;

            return btn;
        }

        private void TaiDanhSachKhachHang(string tuKhoa = "")
        {
            try
            {
                var danhSach = _khachHangBLL.LayDanhSach(tuKhoa);

                dgvKhachHang.Rows.Clear();

                foreach (var kh in danhSach)
                {
                    dgvKhachHang.Rows.Add(
                        kh.MaKhachHang,
                        kh.HoTen,
                        kh.SoDienThoai ?? "",
                        kh.Email ?? "",
                        kh.DiaChi ?? ""
                    );
                }

                lblSoLuong.Text =
                    $"Tổng số: {danhSach.Count} khách hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách khách hàng.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ThemKhachHang()
        {
            HienThiHopThoaiKhachHang(null);
        }

        private void SuaKhachHang()
        {
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần sửa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            var kh = new KhachHangDTO
            {
                MaKhachHang = Convert.ToInt32(
                    dgvKhachHang.CurrentRow.Cells["MaKhachHang"].Value
                ),
                HoTen = dgvKhachHang.CurrentRow.Cells["HoTen"].Value?.ToString() ?? "",
                SoDienThoai = dgvKhachHang.CurrentRow.Cells["SoDienThoai"].Value?.ToString(),
                Email = dgvKhachHang.CurrentRow.Cells["Email"].Value?.ToString(),
                DiaChi = dgvKhachHang.CurrentRow.Cells["DiaChi"].Value?.ToString()
            };

            HienThiHopThoaiKhachHang(kh);
        }

        private void HienThiHopThoaiKhachHang(KhachHangDTO? khachHang)
        {
            bool laThemMoi = khachHang == null;

            using var form = new Form
            {
                Text = laThemMoi ? "Thêm khách hàng" : "Cập nhật khách hàng",
                Size = new Size(500, 480),
                MinimumSize = new Size(500, 480),
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
                Padding = new Padding(26, 18, 26, 16),
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.White
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 22));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 22));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 22));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 22));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));

            var txtHoTen = TaoONhap(
                layout, 0, "Họ và tên:",
                khachHang?.HoTen ?? ""
            );

            var txtSoDienThoai = TaoONhap(
                layout, 1, "Số điện thoại:",
                khachHang?.SoDienThoai ?? ""
            );

            var txtEmail = TaoONhap(
                layout, 2, "Email:",
                khachHang?.Email ?? ""
            );

            var txtDiaChi = TaoONhap(
                layout, 3, "Địa chỉ:",
                khachHang?.DiaChi ?? ""
            );

            var pnlNut = TaoKhungNut();

            var btnHuy = TaoNutTrongDialog("Hủy");
            btnHuy.DialogResult = DialogResult.Cancel;

            var btnLuu = TaoNutTrongDialog(
                laThemMoi ? "Thêm mới" : "Cập nhật",
                true
            );

            btnLuu.Click += (s, e) =>
            {
                var kh = new KhachHangDTO
                {
                    MaKhachHang = khachHang?.MaKhachHang ?? 0,
                    HoTen = txtHoTen.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim()
                };

                try
                {
                    KetQuaThaoTacDTO ketQua = laThemMoi
                        ? _khachHangBLL.ThemKhachHang(kh)
                        : _khachHangBLL.CapNhatKhachHang(kh);

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
                        "Không thể lưu thông tin khách hàng.\n" + ex.Message,
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
                TaiDanhSachKhachHang(txtTimKiem.Text.Trim());
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
                ForeColor = nutChinh
                    ? Color.White
                    : MauChu,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 0, 0, 0),
                UseVisualStyleBackColor = false
            };

            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = nutChinh
                ? MauChinh
                : MauVien;

            return btn;
        }
    }
}
