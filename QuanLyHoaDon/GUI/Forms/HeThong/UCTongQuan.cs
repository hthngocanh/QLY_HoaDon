using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QuanLyHoaDon.DAL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    /// <summary>
    /// UserControl Tổng quan (Dashboard) chuẩn hóa theo mô hình hỗ trợ ra quyết định (Decision Support Dashboard).
    /// Phân biệt theo vai trò: Quản lý (Executive / Giám sát toàn diện) vs Nhân viên (Operational / Tác nghiệp cá nhân).
    /// 100% Số liệu từ SQL Server LocalDB thực tế. Toàn bộ Card và Bảng hỗ trợ Click-to-Action điều hướng kèm bộ lọc.
    /// </summary>
    public class UCTongQuan : UserControl
    {
        private readonly DashboardDAL _dashboardDAL = new();
        private readonly ThongKeDAL _thongKeDAL = new();

        private RoundedPanel pnlBanner = null!;
        private Label lblLoiChao = null!;
        private Label lblThoiGian = null!;
        private Label lblVaiTroBadge = null!;
        private ComboBox cboThoiGian = null!;

        private TableLayoutPanel tlpKpiContainer = null!;
        private TableLayoutPanel tlpCenterLayout = null!;
        private RoundedPanel pnlTrai = null!;
        private RoundedPanel pnlPhai = null!;

        public event Action<string, object?>? OnChuyenChucNang;

        public UCTongQuan()
        {
            KhoiTaoGiaoDien();
            TaiLaiDuLieu();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.FontNoiDung;
            this.AutoScroll = true;
            this.Padding = new Padding(24, 18, 24, 20);
        }

        public void TaiLaiDuLieu()
        {
            this.SuspendLayout();
            this.Controls.Clear();

            // 1. Container chính bố cục dọc
            var pnlMainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            pnlMainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Xác định khoảng thời gian lọc (nếu có)
            DateTime? tuNgay = null;
            DateTime? denNgay = null;
            int kyLocIndex = cboThoiGian?.SelectedIndex ?? 0;

            if (kyLocIndex == 1) // Hôm nay
            {
                tuNgay = DateTime.Today;
                denNgay = DateTime.Today.AddDays(1);
            }
            else if (kyLocIndex == 2) // Tháng này
            {
                tuNgay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                denNgay = tuNgay.Value.AddMonths(1);
            }
            else if (kyLocIndex == 3) // Năm nay
            {
                tuNgay = new DateTime(DateTime.Today.Year, 1, 1);
                denNgay = tuNgay.Value.AddYears(1);
            }

            int? maNVLoc = Session.LaQuanLy ? null : Session.MaNhanVien;
            var soLieu = _dashboardDAL.LaySoLieuTongQuan(maNVLoc, tuNgay, denNgay);
            var dsHoaDonGanDay = _dashboardDAL.LayHoaDonGanDay(maNVLoc, 5);

            // ==========================================
            // 1. BANNER CHÀO MỪNG (Hero Card)
            // ==========================================
            pnlBanner = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Height = 84,
                BackColor = ThemeHelper.MauNenNoiDung,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(20, 14, 20, 14)
            };

            lblLoiChao = new Label
            {
                Text = $"Xin chào, {Session.HoTen}!",
                Font = ThemeHelper.FontTieuDeTrang,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 14),
                AutoSize = true
            };

            lblThoiGian = new Label
            {
                Text = Session.LaQuanLy
                    ? $"Hôm nay là {DateTime.Now:dddd, dd/MM/yyyy} • Bảng điều hành giám sát số liệu kinh doanh toàn hệ thống."
                    : $"Hôm nay là {DateTime.Now:dddd, dd/MM/yyyy} • Bảng tác nghiệp bán hàng cá nhân của #{Session.MaNhanVien:D3}.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(20, 46),
                AutoSize = true
            };

            lblVaiTroBadge = new Label
            {
                Text = Session.LaQuanLy ? "🛡 VAI TRÒ: QUẢN LÝ DOANH NGHIỆP" : "👤 VAI TRÒ: NHÂN VIÊN BÁN HÀNG",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = Session.LaQuanLy ? ThemeHelper.MauChinh : ThemeHelper.MauThanhCong,
                BackColor = Session.LaQuanLy ? ThemeHelper.MauChinhNhe : ThemeHelper.MauThanhCongNhe,
                Padding = new Padding(12, 6, 12, 6),
                AutoSize = true
            };

            // Dropdown chọn kỳ thời gian
            var lblLocKy = new Label
            {
                Text = "Kỳ báo cáo:",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChuPhu,
                AutoSize = true
            };

            cboThoiGian = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = ThemeHelper.FontChuThich,
                Width = 125,
                Height = 28
            };
            cboThoiGian.Items.AddRange(new object[] { "Tất cả thời gian", "Hôm nay", "Tháng này", "Năm nay" });
            cboThoiGian.SelectedIndex = kyLocIndex;
            cboThoiGian.SelectedIndexChanged += (s, e) => TaiLaiDuLieu();

            void CanhChinhBanner()
            {
                int right = pnlBanner.Width - 20;
                cboThoiGian.Location = new Point(right - cboThoiGian.Width, 26);
                lblLocKy.Location = new Point(cboThoiGian.Left - lblLocKy.Width - 8, 30);
                lblVaiTroBadge.Location = new Point(lblLocKy.Left - lblVaiTroBadge.Width - 16, 24);
            }
            pnlBanner.SizeChanged += (s, e) => CanhChinhBanner();
            CanhChinhBanner();

            pnlBanner.Controls.Add(lblLoiChao);
            pnlBanner.Controls.Add(lblThoiGian);
            pnlBanner.Controls.Add(lblVaiTroBadge);
            pnlBanner.Controls.Add(lblLocKy);
            pnlBanner.Controls.Add(cboThoiGian);

            // ==========================================
            // 2. KHU VỰC THẺ THỐNG KÊ KPI (4 Cột Tự Co Giãn)
            // ==========================================
            tlpKpiContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Height = 114,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(0)
            };
            tlpKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpKpiContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpKpiContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            if (Session.LaQuanLy)
            {
                // KPI Quản lý:
                // Card 1: Doanh thu thực tế (click -> Báo cáo)
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Doanh thu xác nhận",
                    $"{soLieu.TongDoanhThu:N0} đ",
                    "VNĐ",
                    "Doanh thu tích lũy hợp lệ",
                    ThemeHelper.MauChinh,
                    ThemeHelper.MauChinhNhe,
                    new Padding(0, 0, 8, 0),
                    () => OnChuyenChucNang?.Invoke("BaoCao", null)), 0, 0);

                // Card 2: Hóa đơn đã xác nhận / Tổng HĐ (click -> Hóa đơn)
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Hóa đơn hoàn tất",
                    $"{soLieu.HoaDonDaXacNhan} / {soLieu.TongHoaDon}",
                    "HĐ",
                    $"Đã hủy: {soLieu.HoaDonDaHuy} đơn",
                    ThemeHelper.MauThanhCong,
                    ThemeHelper.MauThanhCongNhe,
                    new Padding(8, 0, 8, 0),
                    () => OnChuyenChucNang?.Invoke("HoaDon", "DaXacNhan")), 1, 0);

                // Card 3: Khách hàng & Đối tác (click -> Khách hàng)
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Khách hàng đối tác",
                    $"{soLieu.TongKhachHang} khách",
                    "KH",
                    "Danh bạ khách hàng hiện có",
                    ThemeHelper.MauCanhBao,
                    ThemeHelper.MauCanhBaoNhe,
                    new Padding(8, 0, 8, 0),
                    () => OnChuyenChucNang?.Invoke("KhachHang", null)), 2, 0);

                // Card 4: Sản phẩm đang kinh doanh (click -> Sản phẩm)
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Sản phẩm kinh doanh",
                    $"{soLieu.SanPhamDangKinhDoanh} / {soLieu.TongSanPham}",
                    "SP",
                    $"Ngừng KD: {soLieu.SanPhamNgungKinhDoanh} mặt hàng",
                    ThemeHelper.MauTim,
                    ThemeHelper.MauTimNhe,
                    new Padding(8, 0, 0, 0),
                    () => OnChuyenChucNang?.Invoke("SanPham", null)), 3, 0);
            }
            else
            {
                // KPI Nhân viên bán hàng:
                // Card 1: Doanh số cá nhân đã chốt
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Doanh số của tôi",
                    $"{soLieu.TongDoanhThu:N0} đ",
                    "VNĐ",
                    "Tổng tiền các đơn đã chốt",
                    ThemeHelper.MauChinh,
                    ThemeHelper.MauChinhNhe,
                    new Padding(0, 0, 8, 0),
                    () => OnChuyenChucNang?.Invoke("HoaDon", "DaXacNhan")), 0, 0);

                // Card 2: Đơn hàng cần xử lý (Đang lập)
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Đơn đang lập dở",
                    $"{soLieu.HoaDonDangLap} đơn",
                    "LẬP",
                    "Nhấn để kiểm tra và xác nhận",
                    ThemeHelper.MauCanhBao,
                    ThemeHelper.MauCanhBaoNhe,
                    new Padding(8, 0, 8, 0),
                    () => OnChuyenChucNang?.Invoke("HoaDon", "DangLap")), 1, 0);

                // Card 3: Đơn đã xác nhận thành công
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Đơn đã chốt",
                    $"{soLieu.HoaDonDaXacNhan} đơn",
                    "XN",
                    $"Đã hủy: {soLieu.HoaDonDaHuy} đơn",
                    ThemeHelper.MauThanhCong,
                    ThemeHelper.MauThanhCongNhe,
                    new Padding(8, 0, 8, 0),
                    () => OnChuyenChucNang?.Invoke("HoaDon", "DaXacNhan")), 2, 0);

                // Card 4: Tổng khách hàng đối tác
                tlpKpiContainer.Controls.Add(TaoCardThongKeInteractive(
                    "Khách hàng hệ thống",
                    $"{soLieu.TongKhachHang} đối tác",
                    "KH",
                    "Tra cứu thông tin liên hệ",
                    ThemeHelper.MauTim,
                    ThemeHelper.MauTimNhe,
                    new Padding(8, 0, 0, 0),
                    () => OnChuyenChucNang?.Invoke("KhachHang", null)), 3, 0);
            }

            // ==========================================
            // 3. KHU VỰC CHI TIẾT TÁC NGHIỆP (Layout 60% / 40%)
            // ==========================================
            tlpCenterLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Height = 440,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            tlpCenterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58f));
            tlpCenterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42f));
            tlpCenterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            pnlTrai = TaoPanelTrai(soLieu, dsHoaDonGanDay);
            pnlPhai = TaoPanelPhai(soLieu);

            tlpCenterLayout.Controls.Add(pnlTrai, 0, 0);
            tlpCenterLayout.Controls.Add(pnlPhai, 1, 0);

            // Đưa toàn bộ vào Layout dọc
            pnlMainContainer.Controls.Add(pnlBanner, 0, 0);
            pnlMainContainer.Controls.Add(tlpKpiContainer, 0, 1);
            pnlMainContainer.Controls.Add(tlpCenterLayout, 0, 2);

            this.Controls.Add(pnlMainContainer);
            this.ResumeLayout(true);
        }

        private RoundedPanel TaoPanelTrai(DashboardSoLieuDTO soLieu, List<HoaDonGanDayDTO> dsHoaDonGanDay)
        {
            var pnl = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.MauNenNoiDung,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = new Padding(0, 0, 8, 0),
                Padding = new Padding(20, 16, 20, 16)
            };

            var lblTieuDe = new Label
            {
                Text = Session.LaQuanLy ? "📋 Hóa đơn phát sinh gần đây" : "📋 Hóa đơn của bạn gần đây",
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 16),
                AutoSize = true
            };

            var lblHuongDan = new Label
            {
                Text = "Nhấp đúp chuột vào bất kỳ dòng nào để mở màn hình quản lý hóa đơn tương ứng",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(20, 42),
                AutoSize = true
            };

            var dgv = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(pnl.Width - 40, pnl.Height - 90),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Cursor = Cursors.Hand
            };
            ThemeHelper.ChuanHoaDataGridView(dgv);

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaHD", HeaderText = "Mã HĐ", FillWeight = 20 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "KhachHang", HeaderText = "Khách hàng", FillWeight = 40 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayLap", HeaderText = "Thời gian", FillWeight = 30 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongTien", HeaderText = "Tổng tiền (đ)", FillWeight = 30 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng thái", FillWeight = 25 });

            foreach (var hd in dsHoaDonGanDay)
            {
                string ttHienThi = hd.TrangThai switch
                {
                    "DangLap" => "Đang lập",
                    "DaXacNhan" => "Đã xác nhận",
                    "DaHuy" => "Đã hủy",
                    _ => hd.TrangThai
                };
                dgv.Rows.Add($"#{hd.MaHoaDon:D5}", hd.TenKhachHang, hd.NgayLap.ToString("dd/MM HH:mm"), $"{hd.TongTien:N0}", ttHienThi);
                dgv.Rows[dgv.Rows.Count - 1].Tag = hd.TrangThai;
            }

            dgv.CellDoubleClick += (s, e) =>
            {
                if (dgv.CurrentRow != null && dgv.CurrentRow.Tag is string tt)
                {
                    OnChuyenChucNang?.Invoke("HoaDon", tt);
                }
            };

            pnl.Controls.Add(lblTieuDe);
            pnl.Controls.Add(lblHuongDan);
            pnl.Controls.Add(dgv);

            return pnl;
        }

        private RoundedPanel TaoPanelPhai(DashboardSoLieuDTO soLieu)
        {
            var pnl = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.MauNenNoiDung,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = new Padding(8, 0, 0, 0),
                Padding = new Padding(20, 16, 20, 16)
            };

            var lblTieuDe = new Label
            {
                Text = Session.LaQuanLy ? "⚡ Điểm chú ý & Tác vụ cần xử lý" : "⚡ Lối tắt tác nghiệp bán hàng",
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(20, 16),
                AutoSize = true
            };

            var pnlContainer = new Panel
            {
                Location = new Point(20, 52),
                Size = new Size(pnl.Width - 40, pnl.Height - 72),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.Transparent
            };

            int topOffset = 0;
            int gap = 64;

            if (Session.LaQuanLy)
            {
                // Hành động 1: Cấp lại mật khẩu chờ duyệt
                string badgeYeuCau = soLieu.YeuCauCapLaiMKChoDuyet > 0 
                    ? $"{soLieu.YeuCauCapLaiMKChoDuyet} yêu cầu mới" 
                    : "0 yêu cầu";
                Color mauYeuCau = soLieu.YeuCauCapLaiMKChoDuyet > 0 ? ThemeHelper.MauNguyHiem : ThemeHelper.MauThanhCong;
                var btnYeuCauMK = TaoNutTacVu("Duyệt yêu cầu cấp lại mật khẩu", badgeYeuCau, topOffset, mauYeuCau,
                    () => OnChuyenChucNang?.Invoke("YeuCauCapLaiMK", "ChoDuyet"));
                pnlContainer.Controls.Add(btnYeuCauMK);
                topOffset += gap;

                // Hành động 2: Hóa đơn đang lập dở toàn hệ thống
                string badgeDangLap = $"{soLieu.HoaDonDangLap} hóa đơn";
                var btnDangLap = TaoNutTacVu("Kiểm tra hóa đơn đang lập dở", badgeDangLap, topOffset, ThemeHelper.MauCanhBao,
                    () => OnChuyenChucNang?.Invoke("HoaDon", "DangLap"));
                pnlContainer.Controls.Add(btnDangLap);
                topOffset += gap;

                // Hành động 3: Sản phẩm ngừng kinh doanh cần xem xét
                string badgeSP = $"{soLieu.SanPhamNgungKinhDoanh} ngừng KD";
                var btnSanPham = TaoNutTacVu("Rà soát danh mục mặt hàng", badgeSP, topOffset, ThemeHelper.MauTim,
                    () => OnChuyenChucNang?.Invoke("SanPham", null));
                pnlContainer.Controls.Add(btnSanPham);
                topOffset += gap;

                // Hành động 4: Báo cáo phân tích doanh thu
                var btnBaoCao = TaoNutTacVu("Xem báo cáo & Top sản phẩm", "Chi tiết", topOffset, ThemeHelper.MauChinh,
                    () => OnChuyenChucNang?.Invoke("BaoCao", null));
                pnlContainer.Controls.Add(btnBaoCao);
                topOffset += gap;

                // Hành động 5: Quản lý đội ngũ nhân viên
                string badgeNV = $"{soLieu.NhanVienDangHoatDong} nhân sự";
                var btnNhanVien = TaoNutTacVu("Quản lý tài khoản & nhân sự", badgeNV, topOffset, ThemeHelper.MauChuPhu,
                    () => OnChuyenChucNang?.Invoke("NhanVien", null));
                pnlContainer.Controls.Add(btnNhanVien);
            }
            else
            {
                // Nhân viên:
                // Hành động 1: Lập hóa đơn bán hàng mới ngay lập tức
                var btnLapHD = TaoNutTacVu("✍ Tạo mới hóa đơn bán hàng", "Mở giao diện lập", topOffset, ThemeHelper.MauThanhCong,
                    () => OnChuyenChucNang?.Invoke("HoaDon", "LapHoaDon"));
                pnlContainer.Controls.Add(btnLapHD);
                topOffset += gap;

                // Hành động 2: Các hóa đơn của mình đang lập dở
                string badgeDangLapNV = $"{soLieu.HoaDonDangLap} đơn dở";
                var btnDangLapNV = TaoNutTacVu("Hoàn thiện đơn đang lập dở", badgeDangLapNV, topOffset, ThemeHelper.MauCanhBao,
                    () => OnChuyenChucNang?.Invoke("HoaDon", "DangLap"));
                pnlContainer.Controls.Add(btnDangLapNV);
                topOffset += gap;

                // Hành động 3: Tra cứu danh bạ khách hàng
                var btnTraCuuKH = TaoNutTacVu("Tra cứu danh bạ khách hàng", $"{soLieu.TongKhachHang} đối tác", topOffset, ThemeHelper.MauChinh,
                    () => OnChuyenChucNang?.Invoke("KhachHang", null));
                pnlContainer.Controls.Add(btnTraCuuKH);
                topOffset += gap;

                // Hành động 4: Tra cứu bảng giá & sản phẩm
                var btnBangGia = TaoNutTacVu("Tra cứu bảng giá sản phẩm", $"{soLieu.SanPhamDangKinhDoanh} mặt hàng", topOffset, ThemeHelper.MauTim,
                    () => OnChuyenChucNang?.Invoke("SanPham", null));
                pnlContainer.Controls.Add(btnBangGia);
                topOffset += gap;

                // Hành động 5: Thông tin tài khoản & Đổi mật khẩu
                var btnHoSo = TaoNutTacVu("Hồ sơ & Đổi mật khẩu", "Bảo mật", topOffset, ThemeHelper.MauChuPhu,
                    () => OnChuyenChucNang?.Invoke("DoiMatKhau", null));
                pnlContainer.Controls.Add(btnHoSo);
            }

            pnl.Controls.Add(lblTieuDe);
            pnl.Controls.Add(pnlContainer);

            return pnl;
        }

        private Control TaoCardThongKeInteractive(
            string tieuDe,
            string giaTri,
            string badgeText,
            string moTa,
            Color mauChuDao,
            Color mauNenBadge,
            Padding margin,
            Action onClick)
        {
            var pnl = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.MauNenNoiDung,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = margin,
                Padding = new Padding(16, 14, 16, 14),
                Cursor = Cursors.Hand
            };

            var lblBadge = new Label
            {
                Text = badgeText,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = mauChuDao,
                BackColor = mauNenBadge,
                Size = new Size(38, 38),
                Location = new Point(14, 16),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            lblBadge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = ThemeHelper.TaoDuongBoGoc(new RectangleF(0, 0, lblBadge.Width - 1, lblBadge.Height - 1), ThemeHelper.RadiusMedium);
                using var brush = new SolidBrush(mauNenBadge);
                e.Graphics.FillPath(brush, path);
                TextRenderer.DrawText(e.Graphics, badgeText, lblBadge.Font, lblBadge.ClientRectangle, mauChuDao, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            var lblTieuDe = new Label
            {
                Text = tieuDe,
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(58, 14),
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            var lblGiaTri = new Label
            {
                Text = giaTri,
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(58, 34),
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            var lblMoTa = new Label
            {
                Text = moTa,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(58, 62),
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            // Hiệu ứng Hover Soft UI
            void OnEnter()
            {
                pnl.BackColor = ThemeHelper.MauChinhNhe;
                pnl.BorderColor = ThemeHelper.MauChinh;
            }
            void OnLeave()
            {
                pnl.BackColor = ThemeHelper.MauNenNoiDung;
                pnl.BorderColor = ThemeHelper.MauDuongVien;
            }

            pnl.MouseEnter += (s, e) => OnEnter();
            lblBadge.MouseEnter += (s, e) => OnEnter();
            lblTieuDe.MouseEnter += (s, e) => OnEnter();
            lblGiaTri.MouseEnter += (s, e) => OnEnter();
            lblMoTa.MouseEnter += (s, e) => OnEnter();

            pnl.MouseLeave += (s, e) => OnLeave();
            lblBadge.MouseLeave += (s, e) => OnLeave();
            lblTieuDe.MouseLeave += (s, e) => OnLeave();
            lblGiaTri.MouseLeave += (s, e) => OnLeave();
            lblMoTa.MouseLeave += (s, e) => OnLeave();

            pnl.Click += (s, e) => onClick();
            lblBadge.Click += (s, e) => onClick();
            lblTieuDe.Click += (s, e) => onClick();
            lblGiaTri.Click += (s, e) => onClick();
            lblMoTa.Click += (s, e) => onClick();

            pnl.Controls.Add(lblBadge);
            pnl.Controls.Add(lblTieuDe);
            pnl.Controls.Add(lblGiaTri);
            pnl.Controls.Add(lblMoTa);

            return pnl;
        }

        private Control TaoNutTacVu(string tieuDe, string badgeText, int top, Color mauBadge, Action onClick)
        {
            var pnl = new RoundedPanel
            {
                Location = new Point(0, top),
                Height = 54,
                BackColor = ThemeHelper.MauNenUngDung,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusMedium,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };

            var lblTieuDe = new Label
            {
                Text = tieuDe,
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(14, 16),
                AutoSize = true,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            var lblBadge = new Label
            {
                Text = badgeText,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = Color.White,
                BackColor = mauBadge,
                Padding = new Padding(8, 4, 8, 4),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };

            var lblArrow = new Label
            {
                Text = "→",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuPhu,
                Size = new Size(24, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };

            void CanhChinhNut()
            {
                pnl.Width = pnl.Parent?.Width ?? pnl.Width;
                lblArrow.Location = new Point(pnl.Width - 32, 14);
                lblBadge.Location = new Point(lblArrow.Left - lblBadge.Width - 8, 14);
            }
            pnl.SizeChanged += (s, e) => CanhChinhNut();
            CanhChinhNut();

            void OnEnter()
            {
                pnl.BackColor = ThemeHelper.MauChinhNhe;
                pnl.BorderColor = ThemeHelper.MauChinh;
                lblTieuDe.ForeColor = ThemeHelper.MauChinh;
                lblArrow.ForeColor = ThemeHelper.MauChinh;
            }
            void OnLeave()
            {
                pnl.BackColor = ThemeHelper.MauNenUngDung;
                pnl.BorderColor = ThemeHelper.MauDuongVien;
                lblTieuDe.ForeColor = ThemeHelper.MauChuChinh;
                lblArrow.ForeColor = ThemeHelper.MauChuPhu;
            }

            pnl.MouseEnter += (s, e) => OnEnter();
            lblTieuDe.MouseEnter += (s, e) => OnEnter();
            lblBadge.MouseEnter += (s, e) => OnEnter();
            lblArrow.MouseEnter += (s, e) => OnEnter();

            pnl.MouseLeave += (s, e) => OnLeave();
            lblTieuDe.MouseLeave += (s, e) => OnLeave();
            lblBadge.MouseLeave += (s, e) => OnLeave();
            lblArrow.MouseLeave += (s, e) => OnLeave();

            pnl.Click += (s, e) => onClick();
            lblTieuDe.Click += (s, e) => onClick();
            lblBadge.Click += (s, e) => onClick();
            lblArrow.Click += (s, e) => onClick();

            pnl.Controls.Add(lblTieuDe);
            pnl.Controls.Add(lblBadge);
            pnl.Controls.Add(lblArrow);

            return pnl;
        }
    }
}
