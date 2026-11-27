using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyHoaDon.BLL;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.ThongKe
{
    /// <summary>
    /// Man hinh Thong ke bao cao. Toan bo giao dien duoc dung bang code (khong can file Designer).
    /// Quan ly: thay toan he thong, loc duoc theo nhan vien. Nhan vien: chi thay hoa don cua minh (BLL ep).
    /// </summary>
    public class UCThongKe : UserControl
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        // Chu den dong bo toan trang (lay tu ThemeHelper de khop cac trang khac)
        private static readonly Color MauChuDen = ThemeHelper.MauChuChinh;

        // true = giu mau xanh/cam/do cho 3 so lieu KPI dau trang; false = tat ca chu mau den
        private const bool GiuMauSoLieu = false;

        private readonly ThongKeBLL _bll = new ThongKeBLL();

        private readonly Button[] _btnNhanh = new Button[5];
        private DateTimePicker _dtTu = null!;
        private DateTimePicker _dtDen = null!;
        private DateTimePicker _dtThang = null!;
        private ComboBox _cboNhanVien = null!;
        private Label _lblNhanVien = null!;
        private Label _lblKy = null!;
        private Label _lblLoi = null!;

        private Label _lblDoanhThu = null!, _lblDoanhThuPhu = null!;
        private Label _lblTongHoaDon = null!, _lblTongHoaDonPhu = null!;
        private Label _lblTrungBinh = null!, _lblTrungBinhPhu = null!;
        private Label _lblXacNhan = null!, _lblDangLap = null!, _lblDaHuy = null!;

        private BieuDoCot _bieuDo = null!;
        private ThanhTrangThai _thanhTrangThai = null!;
        private DataGridView _dgvTop = null!;
        private Label _lblTopRong = null!;

        private ThongKeKetQuaDTO? _ketQua;
        private bool _dangCapNhat;
        private bool _dangTai;
        private bool _daNapNhanVien;

        public UCThongKe()
        {
            Dock = DockStyle.Fill;
            BackColor = ThemeHelper.MauNenUngDung;
            Font = Chu(9.5f);
            ForeColor = MauChuDen;
            DoubleBuffered = true;

            XayDungGiaoDien();
            VisibleChanged += async (s, e) =>
            {
                if (Visible && !DesignMode)
                    await TaiBaoCaoAsync();
            };
        }

        // =====================================================================
        // XAY DUNG GIAO DIEN
        // =====================================================================
        private void XayDungGiaoDien()
        {
            var root = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = ThemeHelper.MauNenUngDung,
                Padding = new Padding(16)
            };

            var tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            ThemHang(tbl, TaoTheBoLoc());
            ThemHang(tbl, TaoHang3(
                TaoTheKpi("Tổng doanh thu (đã xác nhận)", ThemeHelper.MauKpiXanhDuong, false, out _lblDoanhThu, out _lblDoanhThuPhu),
                TaoTheKpi("Tổng số hóa đơn", ThemeHelper.MauKpiXanhLa, false, out _lblTongHoaDon, out _lblTongHoaDonPhu),
                TaoTheKpi("Giá trị trung bình / hóa đơn", ThemeHelper.MauKpiCam, false, out _lblTrungBinh, out _lblTrungBinhPhu),
                Co(96)));
            ThemHang(tbl, TaoHang3(
                TaoTheKpi("Đã xác nhận", ThemeHelper.MauDaXacNhan, true, out _lblXacNhan, out _),
                TaoTheKpi("Đang lập", ThemeHelper.MauDangLap, true, out _lblDangLap, out _),
                TaoTheKpi("Đã hủy", ThemeHelper.MauDaHuy, true, out _lblDaHuy, out _),
                Co(76)));
            ThemHang(tbl, TaoHangBieuDo());
            ThemHang(tbl, TaoTheTopSanPham());

            root.Controls.Add(tbl);
            Controls.Add(root);
        }

        private static void ThemHang(TableLayoutPanel tbl, Control c)
        {
            c.Dock = DockStyle.Fill;
            tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tbl.Controls.Add(c, 0, tbl.RowStyles.Count - 1);
        }

        private Control TaoTheBoLoc()
        {
            var card = new TheCard { Height = Co(176), Margin = new Padding(0, 0, 0, 10), Padding = new Padding(12, 8, 12, 8) };

            // Hang 1: chon nhanh
            var hang1 = new FlowLayoutPanel { Dock = DockStyle.Top, Height = Co(38), WrapContents = false };
            hang1.Controls.Add(Nhan("Chọn nhanh:", true));
            string[] ten = { "Hôm nay", "7 ngày qua", "Tháng này", "Tháng trước", "Năm nay" };
            for (int i = 0; i < ten.Length; i++)
            {
                int idx = i;
                var b = TaoNut(ten[i], Color.White, ThemeHelper.MauChinh, 104);
                b.Click += async (s, e) => await ChonNhanhAsync(idx);
                _btnNhanh[i] = b;
                hang1.Controls.Add(b);
            }

            // Hang 2: chon ngay / thang
            var hang2 = new FlowLayoutPanel { Dock = DockStyle.Top, Height = Co(38), WrapContents = false };
            hang2.Controls.Add(Nhan("Chọn thời gian:", true));
            hang2.Controls.Add(Nhan("Từ ngày"));
            _dtTu = TaoNgay("dd/MM/yyyy", 110);
            hang2.Controls.Add(_dtTu);
            hang2.Controls.Add(Nhan("Đến ngày"));
            _dtDen = TaoNgay("dd/MM/yyyy", 110);
            hang2.Controls.Add(_dtDen);
            hang2.Controls.Add(Nhan("Hoặc cả tháng"));
            _dtThang = TaoNgay("MM/yyyy", 90);
            _dtThang.ShowUpDown = true;
            _dtThang.ValueChanged += async (s, e) => await ChonThangAsync();
            hang2.Controls.Add(_dtThang);

            // Hang 3: nhan vien + nut chuc nang
            var hang3 = new FlowLayoutPanel { Dock = DockStyle.Top, Height = Co(38), WrapContents = false };
            _lblNhanVien = Nhan("Nhân viên");
            _cboNhanVien = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = Co(180),
                Margin = new Padding(0, Co(3), Co(12), 0),
                ForeColor = MauChuDen
            };
            hang3.Controls.Add(_lblNhanVien);
            hang3.Controls.Add(_cboNhanVien);

            var btnApDung = TaoNut("Áp dụng", ThemeHelper.MauChinh, Color.White, 96);
            btnApDung.Click += async (s, e) => { DanhDauNut(-1); await TaiBaoCaoAsync(); };
            var btnLamMoi = TaoNut("Làm mới", ThemeHelper.MauNutLamMoi, Color.White, 96);
            btnLamMoi.Click += async (s, e) => await ChonNhanhAsync(2);
            var btnXuat = TaoNut("Xuất CSV", ThemeHelper.MauNutXuatExcel, Color.White, 96);
            btnXuat.Click += (s, e) => XuatCsv();
            hang3.Controls.Add(btnApDung);
            hang3.Controls.Add(btnLamMoi);
            hang3.Controls.Add(btnXuat);

            // Hang 4: thong tin ky dang xem + loi
            var hang4 = new FlowLayoutPanel { Dock = DockStyle.Top, Height = Co(28), WrapContents = false };
            _lblKy = new Label { AutoSize = true, ForeColor = MauChuDen, Margin = new Padding(0, 6, 16, 0) };
            _lblLoi = new Label { AutoSize = true, ForeColor = ThemeHelper.MauLoi, Margin = new Padding(0, 6, 0, 0) };
            hang4.Controls.Add(_lblKy);
            hang4.Controls.Add(_lblLoi);

            // Thu tu them: hang cuoi truoc de hang 1 nam tren cung
            card.Controls.Add(hang4);
            card.Controls.Add(hang3);
            card.Controls.Add(hang2);
            card.Controls.Add(hang1);

            // Gia tri ban dau: tu dau thang den hom nay
            DatNgay(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
            DanhDauNut(2);
            return card;
        }

        private Control TaoHang3(Control a, Control b, Control c, int cao)
        {
            var row = new TableLayoutPanel
            {
                Height = cao,
                ColumnCount = 3,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.Transparent
            };
            for (int i = 0; i < 3; i++)
                row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333f));
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            a.Margin = new Padding(0, 0, 10, 0);
            b.Margin = new Padding(0, 0, 10, 0);
            c.Margin = new Padding(0);
            row.Controls.Add(a, 0, 0);
            row.Controls.Add(b, 1, 0);
            row.Controls.Add(c, 2, 0);
            return row;
        }

        private Control TaoTheKpi(string tieuDe, Color mau, bool vachTren, out Label giaTri, out Label phu)
        {
            var card = new TheCard { Dock = DockStyle.Fill, Padding = new Padding(14, 8, 14, 6) };

            var lblTieuDe = new Label
            {
                Dock = DockStyle.Top,
                Height = Co(20),
                Text = tieuDe.ToUpper(Vi),
                Font = Chu(8.5f, FontStyle.Bold),
                ForeColor = MauChuDen,
                TextAlign = ContentAlignment.MiddleLeft
            };
            giaTri = new Label
            {
                Dock = DockStyle.Top,
                Height = Co(34),
                Text = "--",
                Font = Chu(17f, FontStyle.Bold),
                ForeColor = GiuMauSoLieu ? mau : MauChuDen,
                TextAlign = ContentAlignment.MiddleLeft
            };
            phu = new Label
            {
                Dock = DockStyle.Top,
                Height = Co(20),
                Font = Chu(8.5f),
                ForeColor = MauChuDen,
                TextAlign = ContentAlignment.MiddleLeft
            };

            card.Controls.Add(phu);
            card.Controls.Add(giaTri);
            card.Controls.Add(lblTieuDe);
            // Vach mau tren dau the trang thai van giu de phan biet Da xac nhan / Dang lap / Da huy
            if (vachTren)
                card.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 3, BackColor = mau });
            return card;
        }

        private Control TaoHangBieuDo()
        {
            var row = new TableLayoutPanel
            {
                Height = Co(250),
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.Transparent
            };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var cardBieuDo = new TheCard { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 10, 0), Padding = new Padding(12, 8, 12, 8) };
            _bieuDo = new BieuDoCot { Dock = DockStyle.Fill };
            cardBieuDo.Controls.Add(_bieuDo);
            cardBieuDo.Controls.Add(TaoTieuDeThe("Doanh thu theo thời gian (triệu đồng)"));

            var cardTrangThai = new TheCard { Dock = DockStyle.Fill, Margin = new Padding(0), Padding = new Padding(12, 8, 12, 8) };
            _thanhTrangThai = new ThanhTrangThai { Dock = DockStyle.Fill };
            cardTrangThai.Controls.Add(_thanhTrangThai);
            cardTrangThai.Controls.Add(TaoTieuDeThe("Tỷ lệ trạng thái hóa đơn"));

            row.Controls.Add(cardBieuDo, 0, 0);
            row.Controls.Add(cardTrangThai, 1, 0);
            return row;
        }

        private Control TaoTheTopSanPham()
        {
            var card = new TheCard { Height = Co(290), Margin = new Padding(0, 0, 0, 10), Padding = new Padding(12, 8, 12, 8) };

            _dgvTop = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(232, 238, 247),
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = Co(36),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = Co(34) },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            _dgvTop.ColumnHeadersDefaultCellStyle.BackColor = ThemeHelper.MauTieuDeBang;
            _dgvTop.ColumnHeadersDefaultCellStyle.ForeColor = MauChuDen;
            _dgvTop.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeHelper.MauTieuDeBang;
            _dgvTop.ColumnHeadersDefaultCellStyle.SelectionForeColor = MauChuDen;
            _dgvTop.ColumnHeadersDefaultCellStyle.Font = Chu(9.5f, FontStyle.Bold);
            _dgvTop.DefaultCellStyle.Font = Chu(9.5f);
            _dgvTop.DefaultCellStyle.ForeColor = MauChuDen;
            _dgvTop.DefaultCellStyle.SelectionBackColor = ThemeHelper.MauTieuDeBang;
            _dgvTop.DefaultCellStyle.SelectionForeColor = MauChuDen;

            _dgvTop.Columns.Add("Hang", "Hạng");
            _dgvTop.Columns.Add("MaSP", "Mã SP");
            _dgvTop.Columns.Add("TenSP", "Tên sản phẩm");
            _dgvTop.Columns.Add("SoLuong", "Số lượng bán");
            _dgvTop.Columns.Add("DoanhThu", "Doanh thu (VNĐ)");
            _dgvTop.Columns.Add("TyTrong", "Tỷ trọng");
            _dgvTop.Columns["Hang"]!.FillWeight = 12;
            _dgvTop.Columns["MaSP"]!.FillWeight = 12;
            _dgvTop.Columns["TenSP"]!.FillWeight = 36;
            _dgvTop.Columns["SoLuong"]!.FillWeight = 14;
            _dgvTop.Columns["DoanhThu"]!.FillWeight = 20;
            _dgvTop.Columns["TyTrong"]!.FillWeight = 12;
            foreach (var tenCot in new[] { "SoLuong", "DoanhThu", "TyTrong" })
            {
                _dgvTop.Columns[tenCot]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                _dgvTop.Columns[tenCot]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            _lblTopRong = new Label
            {
                Dock = DockStyle.Bottom,
                Height = Co(40),
                Text = "Không có dữ liệu trong kỳ này",
                ForeColor = MauChuDen,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            card.Controls.Add(_dgvTop);
            card.Controls.Add(_lblTopRong);
            card.Controls.Add(TaoTieuDeThe("TOP SẢN PHẨM BÁN CHẠY THEO DOANH THU"));
            return card;
        }

        private static Label TaoTieuDeThe(string text)
        {
            return new Label
            {
                Dock = DockStyle.Top,
                Height = Co(28),
                Text = text,
                Font = Chu(10f, FontStyle.Bold),
                ForeColor = MauChuDen,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static Label Nhan(string text, bool dam = false)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(0, Co(9), Co(6), 0),
                Font = Chu(9.5f, FontStyle.Bold),
                ForeColor = MauChuDen
            };
        }

        private static DateTimePicker TaoNgay(string dinhDang, int rong)
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = dinhDang,
                Width = Co(rong),
                MaxDate = DateTime.Today,
                Margin = new Padding(0, Co(4), Co(12), 0)
            };
        }

        private static Button TaoNut(string text, Color nen, Color chu, int rong)
        {
            var b = new Button
            {
                Text = text,
                Width = Co(rong),
                Height = Co(30),
                FlatStyle = FlatStyle.Flat,
                BackColor = nen,
                ForeColor = chu,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, Co(3), Co(8), 0),
                UseVisualStyleBackColor = false
            };
            b.FlatAppearance.BorderColor = ThemeHelper.MauChinh;
            b.FlatAppearance.BorderSize = nen == Color.White ? 1 : 0;
            return b;
        }

        // =====================================================================
        // XU LY LOC THOI GIAN
        // =====================================================================
        private void DatNgay(DateTime tu, DateTime den)
        {
            _dangCapNhat = true;
            try
            {
                _dtTu.Value = tu.Date;
                _dtDen.Value = den.Date;
                _dtThang.Value = den.Date;
            }
            finally
            {
                _dangCapNhat = false;
            }
        }

        private void DanhDauNut(int chiSo)
        {
            for (int i = 0; i < _btnNhanh.Length; i++)
            {
                bool bat = i == chiSo;
                _btnNhanh[i].BackColor = bat ? ThemeHelper.MauChinh : Color.White;
                _btnNhanh[i].ForeColor = bat ? Color.White : ThemeHelper.MauChinh;
            }
        }

        private async Task ChonNhanhAsync(int chiSo)
        {
            DateTime homNay = DateTime.Today;
            DateTime tu, den = homNay;

            if (chiSo == 0) tu = homNay;
            else if (chiSo == 1) tu = homNay.AddDays(-6);
            else if (chiSo == 2) tu = new DateTime(homNay.Year, homNay.Month, 1);
            else if (chiSo == 3)
            {
                tu = new DateTime(homNay.Year, homNay.Month, 1).AddMonths(-1);
                den = tu.AddMonths(1).AddDays(-1);
            }
            else tu = new DateTime(homNay.Year, 1, 1);

            DatNgay(tu, den);
            DanhDauNut(chiSo);
            await TaiBaoCaoAsync();
        }

        private async Task ChonThangAsync()
        {
            if (_dangCapNhat) return;

            DateTime m = _dtThang.Value;
            DateTime tu = new DateTime(m.Year, m.Month, 1);
            DateTime den = tu.AddMonths(1).AddDays(-1);
            if (den > DateTime.Today) den = DateTime.Today;

            DatNgay(tu, den);
            DanhDauNut(-1);
            await TaiBaoCaoAsync();
        }

        // =====================================================================
        // TAI VA HIEN THI DU LIEU
        // =====================================================================
        private async Task TaiBaoCaoAsync()
        {
            if (_dangTai || DesignMode) return;

            DateTime tu = _dtTu.Value.Date;
            DateTime den = _dtDen.Value.Date;

            string? loi = _bll.KiemTraBoLoc(tu, den);
            _lblLoi.Text = loi ?? string.Empty;
            if (loi != null) return;

            _dangTai = true;
            Cursor = Cursors.WaitCursor;
            try
            {
                if (!_daNapNhanVien)
                {
                    await NapNhanVienAsync();
                    _daNapNhanVien = true;
                }

                int? maNhanVien = null;
                if (_bll.ChoPhepLocTheoNhanVien
                    && _cboNhanVien.SelectedItem is NhanVienLocDTO nv
                    && nv.MaNhanVien > 0)
                {
                    maNhanVien = nv.MaNhanVien;
                }

                var ketQua = await Task.Run(() => _bll.LayBaoCao(tu, den, maNhanVien));
                HienThi(ketQua);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được báo cáo thống kê.\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dangTai = false;
                Cursor = Cursors.Default;
            }
        }

        private async Task NapNhanVienAsync()
        {
            bool choPhep = _bll.ChoPhepLocTheoNhanVien;
            _cboNhanVien.Visible = choPhep;
            _lblNhanVien.Visible = choPhep;
            if (!choPhep) return;

            var ds = await Task.Run(() => _bll.LayDanhSachNhanVienLoc());
            _cboNhanVien.Items.Clear();
            _cboNhanVien.Items.Add(new NhanVienLocDTO { MaNhanVien = 0, HoTen = "Tất cả nhân viên" });
            foreach (var nv in ds)
                _cboNhanVien.Items.Add(nv);
            _cboNhanVien.SelectedIndex = 0;
        }

        private void HienThi(ThongKeKetQuaDTO kq)
        {
            _ketQua = kq;
            int soNgay = (int)(kq.DenNgay - kq.TuNgay).TotalDays + 1;
            _lblKy.Text = $"Đang xem: {kq.TuNgay:dd/MM/yyyy} đến {kq.DenNgay:dd/MM/yyyy} ({soNgay} ngày)  •  Phạm vi: {kq.PhamVi}";

            _lblDoanhThu.Text = TienTe(kq.TongQuan.DoanhThu);
            if (kq.PhanTramSoVoiKyTruoc is decimal pt)
            {
                bool tang = pt >= 0;
                _lblDoanhThuPhu.Text = $"{(tang ? "▲" : "▼")} {Math.Abs(pt).ToString("0.0", Vi)}% so với kỳ trước";
                // Giu xanh/do cho mui ten tang/giam vi mang y nghia
                _lblDoanhThuPhu.ForeColor = tang ? ThemeHelper.MauThanhCong : ThemeHelper.MauLoi;
            }
            else
            {
                _lblDoanhThuPhu.Text = "-- so với kỳ trước (kỳ trước chưa có doanh thu)";
                _lblDoanhThuPhu.ForeColor = MauChuDen;
            }

            _lblTongHoaDon.Text = kq.TrangThai.Tong.ToString("N0", Vi) + " đơn";
            _lblTongHoaDonPhu.Text = "Gồm cả đang lập và đã hủy";
            _lblTrungBinh.Text = TienTe(kq.TongQuan.GiaTriTrungBinh);
            _lblTrungBinhPhu.Text = "Chỉ tính hóa đơn đã xác nhận";

            _lblXacNhan.Text = kq.TrangThai.SoDaXacNhan.ToString("N0", Vi) + " đơn";
            _lblDangLap.Text = kq.TrangThai.SoDangLap.ToString("N0", Vi) + " đơn";
            _lblDaHuy.Text = kq.TrangThai.SoDaHuy.ToString("N0", Vi) + " đơn";

            _bieuDo.DuLieu = kq.BieuDo;
            _thanhTrangThai.DuLieu = kq.TrangThai;

            _dgvTop.Rows.Clear();
            foreach (var sp in kq.TopSanPham)
            {
                int dong = _dgvTop.Rows.Add(
                    "Top " + sp.Hang,
                    sp.MaHienThi,
                    sp.TenSanPham,
                    sp.SoLuong.ToString("N0", Vi),
                    TienTe(sp.DoanhThu),
                    sp.TyTrongPhanTram.ToString("0.0", Vi) + "%");

                var hang = _dgvTop.Rows[dong];
                hang.Cells["DoanhThu"].Style.Font = Chu(9.5f, FontStyle.Bold);
                if (sp.Hang >= 1 && sp.Hang <= 3)
                    hang.Cells["Hang"].Style.Font = Chu(9.5f, FontStyle.Bold);
                if (sp.Hang == 1)
                    hang.Cells["TenSP"].Style.Font = Chu(9.5f, FontStyle.Bold);
            }
            _lblTopRong.Visible = kq.TopSanPham.Count == 0;
        }

        // Co chu rieng cua trang Thong ke (khong dung chung ThemeHelper de khong anh huong trang khac).
        // Muon to/nho hon: doi HeSoChu. Chieu cao khung tu nong theo cung he so.
        private const float HeSoChu = 1.2f;
        private static Font Chu(float pt, FontStyle kieu = FontStyle.Regular) => new Font("Segoe UI", pt * HeSoChu, kieu);
        private static int Co(int px) => (int)Math.Round(px * HeSoChu);

        private static string TienTe(decimal so)
        {
            return so.ToString("N0", Vi) + " đ";
        }

        // =====================================================================
        // XUAT CSV (mo duoc bang Excel)
        // =====================================================================
        private void XuatCsv()
        {
            if (_ketQua == null)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất. Hãy chọn kỳ báo cáo trước.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"BaoCaoThongKe_{_ketQua.TuNgay:yyyyMMdd}_{_ketQua.DenNgay:yyyyMMdd}.csv"
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var kq = _ketQua;
            var sb = new StringBuilder();
            sb.AppendLine("BÁO CÁO THỐNG KÊ");
            sb.AppendLine($"Từ ngày;{kq.TuNgay:dd/MM/yyyy}");
            sb.AppendLine($"Đến ngày;{kq.DenNgay:dd/MM/yyyy}");
            sb.AppendLine("Phạm vi;" + Csv(kq.PhamVi));
            sb.AppendLine();
            sb.AppendLine("Tổng doanh thu (VNĐ);" + Math.Round(kq.TongQuan.DoanhThu, 0));
            sb.AppendLine("Giá trị trung bình / hóa đơn (VNĐ);" + Math.Round(kq.TongQuan.GiaTriTrungBinh, 0));
            sb.AppendLine("Số hóa đơn đã xác nhận;" + kq.TrangThai.SoDaXacNhan);
            sb.AppendLine("Số hóa đơn đang lập;" + kq.TrangThai.SoDangLap);
            sb.AppendLine("Số hóa đơn đã hủy;" + kq.TrangThai.SoDaHuy);
            sb.AppendLine();
            sb.AppendLine("Thời gian;Doanh thu (VNĐ)");
            foreach (var d in kq.BieuDo)
                sb.AppendLine(Csv(d.Nhan) + ";" + Math.Round(d.DoanhThu, 0));
            sb.AppendLine();
            sb.AppendLine("Hạng;Mã SP;Tên sản phẩm;Số lượng;Doanh thu (VNĐ);Tỷ trọng (%)");
            foreach (var sp in kq.TopSanPham)
            {
                sb.AppendLine(string.Join(";",
                    sp.Hang, sp.MaHienThi, Csv(sp.TenSanPham), sp.SoLuong,
                    Math.Round(sp.DoanhThu, 0), sp.TyTrongPhanTram.ToString("0.0", Vi)));
            }

            try
            {
                File.WriteAllText(dlg.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show("Đã xuất báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không ghi được file.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Csv(string s)
        {
            if (s.Contains(';') || s.Contains('"') || s.Contains('\n'))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        // =====================================================================
        // CAC CONTROL VE TAY
        // =====================================================================
        private sealed class TheCard : Panel
        {
            public TheCard()
            {
                DoubleBuffered = true;
                BackColor = Color.White;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                using var pen = new Pen(ThemeHelper.MauDuongVien);
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }

        private sealed class BieuDoCot : Control
        {
            private List<DiemBieuDoDTO> _duLieu = new List<DiemBieuDoDTO>();

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public List<DiemBieuDoDTO> DuLieu
            {
                get => _duLieu;
                set { _duLieu = value ?? new List<DiemBieuDoDTO>(); Invalidate(); }
            }

            public BieuDoCot()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                         | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
                BackColor = Color.White;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(BackColor);

                using var fontNho = Chu(8.5f);
                using var chuDen = new SolidBrush(MauChuDen);
                var giua = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                if (_duLieu.Count == 0)
                {
                    g.DrawString("Không có dữ liệu trong kỳ này", fontNho, chuDen,
                        new RectangleF(0, 0, Width, Height), giua);
                    return;
                }

                int trai = 6, phai = Width - 6, tren = 28, day = Height - 32;
                using (var truc = new Pen(ThemeHelper.MauDuongVien))
                    g.DrawLine(truc, trai, day, phai, day);

                decimal max = _duLieu.Max(x => x.DoanhThu);
                float rongO = (phai - trai) / (float)_duLieu.Count;
                float rongCot = Math.Min(rongO * 0.6f, 64f);
                using var cot = new SolidBrush(ThemeHelper.MauChinh);

                for (int i = 0; i < _duLieu.Count; i++)
                {
                    var d = _duLieu[i];
                    float cao = max > 0 ? (float)(d.DoanhThu / max) * (day - tren) : 0f;
                    cao = Math.Max(cao, 2f);
                    float x = trai + rongO * i + (rongO - rongCot) / 2f;
                    g.FillRectangle(cot, x, day - cao, rongCot, cao);

                    if (d.DoanhThu > 0)
                    {
                        string giaTri = (d.DoanhThu / 1_000_000m).ToString("N1", Vi);
                        g.DrawString(giaTri, fontNho, chuDen,
                            new RectangleF(x - 14, day - cao - 22, rongCot + 28, 20), giua);
                    }

                    g.DrawString(d.Nhan, fontNho, chuDen,
                        new RectangleF(trai + rongO * i, day + 4, rongO, 24), giua);
                }
            }
        }

        private sealed class ThanhTrangThai : Control
        {
            private ThongKeTrangThaiDTO _duLieu = new ThongKeTrangThaiDTO();

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ThongKeTrangThaiDTO DuLieu
            {
                get => _duLieu;
                set { _duLieu = value ?? new ThongKeTrangThaiDTO(); Invalidate(); }
            }

            public ThanhTrangThai()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                         | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
                BackColor = Color.White;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                var g = e.Graphics;
                g.Clear(BackColor);

                using var font = Chu(9.5f);
                using var chu = new SolidBrush(MauChuDen);
                using var nenThanh = new SolidBrush(Color.FromArgb(232, 238, 247));
                var phai = new StringFormat { Alignment = StringAlignment.Far };

                var muc = new (string Ten, int So, decimal TyLe, Color Mau)[]
                {
                    ("Đã xác nhận", _duLieu.SoDaXacNhan, _duLieu.TyLeDaXacNhan, ThemeHelper.MauDaXacNhan),
                    ("Đang lập", _duLieu.SoDangLap, _duLieu.TyLeDangLap, ThemeHelper.MauDangLap),
                    ("Đã hủy", _duLieu.SoDaHuy, _duLieu.TyLeDaHuy, ThemeHelper.MauDaHuy)
                };

                int cao = Math.Max(Height / 3, 56);
                for (int i = 0; i < muc.Length; i++)
                {
                    int y = 6 + i * cao;
                    g.DrawString(muc[i].Ten, font, chu, 0, y);
                    g.DrawString($"{muc[i].TyLe.ToString("0.0", Vi)}% ({muc[i].So})", font, chu,
                        new RectangleF(0, y, Width - 2, 26), phai);

                    g.FillRectangle(nenThanh, 0, y + 30, Width - 2, 10);
                    float rong = (Width - 2) * (float)muc[i].TyLe / 100f;
                    if (rong > 0)
                    {
                        using var to = new SolidBrush(muc[i].Mau);
                        g.FillRectangle(to, 0, y + 30, rong, 10);
                    }
                }
            }
        }
    }
}