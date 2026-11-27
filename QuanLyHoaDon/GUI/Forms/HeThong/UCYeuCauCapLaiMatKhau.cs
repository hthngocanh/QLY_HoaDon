using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class UCYeuCauCapLaiMatKhau : UserControl
    {
        private readonly YeuCauCapLaiMatKhauBLL _yeuCauBLL;

        // KPI thống kê nhanh
        private Label lblKpiChoDuyet = null!;
        private Label lblKpiDaDuyet = null!;
        private Label lblKpiDaTuChoi = null!;
        private Label lblKpiDaHoanTat = null!;

        // Danh sách và bộ lọc
        private DataGridView dgvDanhSach = null!;
        private ComboBox cboLocTrangThai = null!;
        private RoundedTextBox txtTimKiem = null!;
        private RoundedButton btnDuyet = null!;
        private RoundedButton btnTuChoi = null!;
        private RoundedButton btnXemChiTiet = null!;
        private RoundedButton btnLamMoi = null!;
        private Label lblSoLuong = null!;

        public UCYeuCauCapLaiMatKhau()
        {
            _yeuCauBLL = new YeuCauCapLaiMatKhauBLL();
            KhoiTaoGiaoDien();
            TaiDanhSach();
        }

        public void ApDungLocTrangThai(string trangThai)
        {
            if (cboLocTrangThai != null)
            {
                cboLocTrangThai.SelectedIndex = trangThai switch
                {
                    "ChoDuyet" => 1,
                    "DaDuyet" => 2,
                    "DaTuChoi" => 3,
                    "DaHoanTat" => 4,
                    _ => 0
                };
            }
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.FontNoiDung;
            this.Padding = new Padding(24, 16, 24, 20);

            // Container chứa 3 phần: KPI Thống Kê, Thanh Công Cụ Lọc, Bảng Danh Sách
            var pnlMain = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };
            pnlMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 94));  // Row 0: 4 thẻ KPI
            pnlMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 68));  // Row 1: Thanh tìm kiếm & lọc & thao tác
            pnlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Row 2: Bảng danh sách

            // 1. KHU VỰC THẺ THỐNG KÊ (KPI CARDS)
            var pnlKpiContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 12),
                BackColor = Color.Transparent
            };
            pnlKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            pnlKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            pnlKpiContainer.Controls.Add(TaoTheKpi("⏳ Chờ phê duyệt", "0", ThemeHelper.MauCanhBao, out lblKpiChoDuyet), 0, 0);
            pnlKpiContainer.Controls.Add(TaoTheKpi("✅ Đã phê duyệt", "0", ThemeHelper.MauThanhCong, out lblKpiDaDuyet), 1, 0);
            pnlKpiContainer.Controls.Add(TaoTheKpi("❌ Đã từ chối", "0", ThemeHelper.MauNguyHiem, out lblKpiDaTuChoi), 2, 0);
            pnlKpiContainer.Controls.Add(TaoTheKpi("🎉 Đã hoàn tất", "0", ThemeHelper.MauChinh, out lblKpiDaHoanTat), 3, 0);

            // 2. KHU VỰC THANH CÔNG CỤ (TÌM KIẾM, LỌC VÀ THAO TÁC)
            var pnlFilterCard = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderRadius = 10,
                Padding = new Padding(14, 12, 14, 12),
                Margin = new Padding(0, 0, 0, 12)
            };

            // Ô tìm kiếm gọn
            txtTimKiem = new RoundedTextBox
            {
                Location = new Point(14, 13),
                Size = new Size(250, 38),
                Font = ThemeHelper.FontNoiDung,
                PlaceholderText = "🔍 Tìm tài khoản, nhân viên..."
            };
            txtTimKiem.TextChanged += (s, e) => TaiDanhSach();

            // Bộ lọc trạng thái
            cboLocTrangThai = new ComboBox
            {
                Location = new Point(276, 17),
                Size = new Size(160, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = ThemeHelper.FontNoiDung
            };
            cboLocTrangThai.Items.AddRange(new object[]
            {
                "Tất cả trạng thái",
                "Chờ duyệt",
                "Đã duyệt",
                "Đã từ chối",
                "Đã hoàn tất"
            });
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (s, e) => TaiDanhSach();

            // Nút Thao tác
            btnLamMoi = new RoundedButton
            {
                Text = "🔄 Làm mới",
                Size = new Size(100, 36),
                Location = new Point(450, 14),
                NormalColor = ColorTranslator.FromHtml("#F1F5F9"),
                HoverColor = ColorTranslator.FromHtml("#E2E8F0"),
                ForeColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDung,
                BorderRadius = 8
            };
            btnLamMoi.Click += (s, e) => TaiDanhSach();

            btnXemChiTiet = new RoundedButton
            {
                Text = "👁️ Xem chi tiết",
                Size = new Size(120, 36),
                Location = new Point(560, 14),
                NormalColor = ColorTranslator.FromHtml("#EFF6FF"),
                HoverColor = ColorTranslator.FromHtml("#DBEAFE"),
                ForeColor = ThemeHelper.MauChinh,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8
            };
            btnXemChiTiet.Click += (s, e) => XemChiTiet();

            btnTuChoi = new RoundedButton
            {
                Text = "❌ Từ chối",
                Size = new Size(110, 36),
                Location = new Point(690, 14),
                NormalColor = ColorTranslator.FromHtml("#FEF2F2"),
                HoverColor = ColorTranslator.FromHtml("#FEE2E2"),
                ForeColor = ThemeHelper.MauNguyHiem,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8
            };
            btnTuChoi.Click += (s, e) => TuChoiYeuCau();

            btnDuyet = new RoundedButton
            {
                Text = "✓ Duyệt yêu cầu",
                Size = new Size(130, 36),
                Location = new Point(810, 14),
                NormalColor = ThemeHelper.MauThanhCong,
                HoverColor = ColorTranslator.FromHtml("#15803D"),
                ForeColor = Color.White,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8
            };
            btnDuyet.Click += (s, e) => DuyetYeuCau();

            pnlFilterCard.Controls.Add(txtTimKiem);
            pnlFilterCard.Controls.Add(cboLocTrangThai);
            pnlFilterCard.Controls.Add(btnLamMoi);
            pnlFilterCard.Controls.Add(btnXemChiTiet);
            pnlFilterCard.Controls.Add(btnTuChoi);
            pnlFilterCard.Controls.Add(btnDuyet);

            // 3. KHU VỰC BẢNG DỮ LIỆU CHÍNH (GRID CARD)
            var pnlGridCard = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderRadius = 10,
                Padding = new Padding(12),
                Margin = new Padding(0)
            };

            lblSoLuong = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Text = "Tổng số yêu cầu: 0"
            };

            dgvDanhSach = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                Font = ThemeHelper.FontNoiDung
            };

            ThemeHelper.ChuanHoaDataGridView(dgvDanhSach);
            dgvDanhSach.SelectionChanged += DgvDanhSach_SelectionChanged;
            dgvDanhSach.CellDoubleClick += (s, e) => XemChiTiet();

            KhoiTaoCotBang();

            pnlGridCard.Controls.Add(dgvDanhSach);
            pnlGridCard.Controls.Add(lblSoLuong);

            pnlMain.Controls.Add(pnlKpiContainer, 0, 0);
            pnlMain.Controls.Add(pnlFilterCard, 0, 1);
            pnlMain.Controls.Add(pnlGridCard, 0, 2);

            this.Controls.Add(pnlMain);
        }

        private Control TaoTheKpi(string tieuDe, string soLuong, Color mauNhan, out Label lblGiaTri)
        {
            var pnlCard = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderRadius = 10,
                Margin = new Padding(4, 0, 4, 0),
                Padding = new Padding(16, 12, 16, 12)
            };

            var lblTitle = new Label
            {
                Text = tieuDe,
                Dock = DockStyle.Top,
                Height = 22,
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuPhu
            };

            lblGiaTri = new Label
            {
                Text = soLuong,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = mauNhan,
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlCard.Controls.Add(lblGiaTri);
            pnlCard.Controls.Add(lblTitle);
            return pnlCard;
        }

        private void KhoiTaoCotBang()
        {
            dgvDanhSach.Columns.Clear();

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaYeuCau",
                HeaderText = "Mã YC",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTenNhanVien",
                HeaderText = "Nhân viên",
                Width = 180
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDangNhap",
                HeaderText = "Tên đăng nhập",
                Width = 140
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThoiGianYeuCau",
                HeaderText = "Thời gian gửi",
                Width = 140,
                DefaultCellStyle = { Format = "HH:mm dd/MM/yyyy" }
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenHienThiTrangThai",
                HeaderText = "Trạng thái",
                Width = 120
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LyDoYeuCau",
                HeaderText = "Lý do đề xuất",
                Width = 220
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenNguoiDuyet",
                HeaderText = "Người xử lý",
                Width = 160
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThoiGianDuyet",
                HeaderText = "Thời gian xử lý",
                Width = 140,
                DefaultCellStyle = { Format = "HH:mm dd/MM/yyyy" }
            });

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LyDoTuChoi",
                HeaderText = "Lý do từ chối",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void TaiDanhSach()
        {
            string? trangThai = cboLocTrangThai.SelectedIndex switch
            {
                1 => "ChoDuyet",
                2 => "DaDuyet",
                3 => "DaTuChoi",
                4 => "DaHoanTat",
                _ => null
            };

            string? tuKhoa = string.IsNullOrWhiteSpace(txtTimKiem.Text) ? null : txtTimKiem.Text.Trim();

            try
            {
                // Tải danh sách toàn bộ để tính thẻ KPI
                var dsTatCa = _yeuCauBLL.LayDanhSach(null, null);
                int choDuyet = 0, daDuyet = 0, daTuChoi = 0, daHoanTat = 0;
                foreach (var item in dsTatCa)
                {
                    switch (item.TrangThai)
                    {
                        case "ChoDuyet": choDuyet++; break;
                        case "DaDuyet": daDuyet++; break;
                        case "DaTuChoi": daTuChoi++; break;
                        case "DaHoanTat": daHoanTat++; break;
                    }
                }
                lblKpiChoDuyet.Text = choDuyet.ToString();
                lblKpiDaDuyet.Text = daDuyet.ToString();
                lblKpiDaTuChoi.Text = daTuChoi.ToString();
                lblKpiDaHoanTat.Text = daHoanTat.ToString();

                // Lấy danh sách hiển thị theo bộ lọc
                var ds = _yeuCauBLL.LayDanhSach(trangThai, tuKhoa);
                dgvDanhSach.DataSource = ds;
                lblSoLuong.Text = $"Hiển thị {ds.Count} / Tổng số {dsTatCa.Count} yêu cầu";

                CapNhatTrangThaiNut();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách yêu cầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDanhSach_SelectionChanged(object? sender, EventArgs e)
        {
            CapNhatTrangThaiNut();
        }

        private void CapNhatTrangThaiNut()
        {
            var yc = LayYeuCauDangChon();
            if (yc == null)
            {
                btnXemChiTiet.Enabled = false;
                btnDuyet.Enabled = false;
                btnTuChoi.Enabled = false;
                return;
            }

            btnXemChiTiet.Enabled = true;

            // Chỉ cho phép duyệt hoặc từ chối khi trạng thái là Chờ duyệt
            bool coTheXuLy = (yc.TrangThai == "ChoDuyet");
            btnDuyet.Enabled = coTheXuLy;
            btnTuChoi.Enabled = coTheXuLy;
        }

        private YeuCauCapLaiMatKhauDTO? LayYeuCauDangChon()
        {
            if (dgvDanhSach.CurrentRow?.DataBoundItem is YeuCauCapLaiMatKhauDTO yc)
            {
                return yc;
            }
            return null;
        }

        private void DuyetYeuCau()
        {
            var yc = LayYeuCauDangChon();
            if (yc == null) return;

            if (yc.TrangThai != "ChoDuyet")
            {
                MessageBox.Show($"Yêu cầu này đã ở trạng thái '{yc.TenHienThiTrangThai}', không thể duyệt lại.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dr = MessageBox.Show(
                $"Xác nhận PHÊ DUYỆT yêu cầu cấp lại mật khẩu cho tài khoản '{yc.TenDangNhap}' ({yc.HoTenNhanVien})?\n\nSau khi duyệt, nhân viên có thể tự đặt lại mật khẩu mới.",
                "Xác Nhận Phê Duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                var res = _yeuCauBLL.DuyetYeuCau(yc.MaYeuCau, Session.MaNhanVien);
                if (res.ThanhCong)
                {
                    MessageBox.Show(res.ThongBao, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                }
                else
                {
                    MessageBox.Show(res.ThongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TuChoiYeuCau()
        {
            var yc = LayYeuCauDangChon();
            if (yc == null) return;

            if (yc.TrangThai != "ChoDuyet")
            {
                MessageBox.Show($"Yêu cầu này đã ở trạng thái '{yc.TenHienThiTrangThai}', không thể từ chối lại.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var frmTuChoi = new FormTuChoiYeuCau(yc.TenDangNhap, yc.HoTenNhanVien);
            if (frmTuChoi.ShowDialog(this) == DialogResult.OK)
            {
                var res = _yeuCauBLL.TuChoiYeuCau(yc.MaYeuCau, Session.MaNhanVien, frmTuChoi.LyDoTuChoi);
                if (res.ThanhCong)
                {
                    MessageBox.Show(res.ThongBao, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                }
                else
                {
                    MessageBox.Show(res.ThongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XemChiTiet()
        {
            var yc = LayYeuCauDangChon();
            if (yc == null) return;

            string chiTiet = 
                $"MÃ YÊU CẦU: #{yc.MaYeuCau}\n" +
                $"----------------------------------------\n" +
                $"Tài khoản: {yc.TenDangNhap}\n" +
                $"Họ và tên: {yc.HoTenNhanVien}\n" +
                $"Email: {yc.Email ?? "Chưa cập nhật"}\n" +
                $"Số điện thoại: {yc.SoDienThoai ?? "Chưa cập nhật"}\n" +
                $"Thời gian gửi: {yc.ThoiGianYeuCau:HH:mm:ss dd/MM/yyyy}\n" +
                $"Lý do yêu cầu: {yc.LyDoYeuCau ?? "(Không có)"}\n\n" +
                $"TRẠNG THÁI: {yc.TenHienThiTrangThai.ToUpper()}\n" +
                $"----------------------------------------\n" +
                $"Người xử lý: {yc.TenNguoiDuyet ?? "(Chưa xử lý)"}\n" +
                $"Thời gian xử lý: {(yc.ThoiGianDuyet.HasValue ? yc.ThoiGianDuyet.Value.ToString("HH:mm:ss dd/MM/yyyy") : "(Chưa có)")}\n" +
                $"Lý do từ chối: {yc.LyDoTuChoi ?? "(Không có)"}\n" +
                $"Thời gian hoàn tất: {(yc.ThoiGianHoanTat.HasValue ? yc.ThoiGianHoanTat.Value.ToString("HH:mm:ss dd/MM/yyyy") : "(Chưa hoàn tất)")}";

            MessageBox.Show(chiTiet, $"Chi Tiết Yêu Cầu #{yc.MaYeuCau}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
