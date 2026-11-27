using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    /// <summary>
    /// UserControl Thông tin cá nhân - Thiết kế theo chuẩn Profile Dashboard hiện đại.
    /// Phân chia trực quan: Profile Summary Card (Avatar, Họ tên, Username, Role, Status)
    /// và 2 Section rõ ràng: Thông tin định danh (chỉ xem) & Thông tin liên hệ (chỉnh sửa).
    /// Hỗ trợ đầy đủ các nút: Chỉnh sửa thông tin, Lưu thay đổi, Hủy chỉnh sửa.
    /// </summary>
    public class UCThongTinCaNhan : UserControl
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;

        // Container chính
        private TableLayoutPanel pnlMainContainer = null!;
        private RoundedPanel pnlThongBao = null!;
        private Label lblThongBao = null!;

        // Lưu trữ giá trị ban đầu để so sánh thay đổi thực tế
        private string _sdtBanDau = string.Empty;
        private string _emailBanDau = string.Empty;
        private string _diaChiBanDau = string.Empty;
        private bool _dangNapDuLieu = false;
        private bool _dangChinhSua = false;

        // Các trường nhập liệu được phép sửa
        private RoundedTextBox txtSoDienThoai = null!;
        private RoundedTextBox txtEmail = null!;
        private RoundedTextBox txtDiaChi = null!;

        // Nút thao tác
        private RoundedButton btnChinhSua = null!;
        private RoundedButton btnLuuThayDoi = null!;
        private RoundedButton btnHuyChinhSua = null!;

        // Các nhãn tóm tắt Profile Header
        private Label lblProfileTen = null!;
        private Label lblProfileSub = null!;

        public UCThongTinCaNhan()
        {
            _taiKhoanBLL = new TaiKhoanBLL();
            KhoiTaoGiaoDien();
            NapDuLieuTuSession();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.MauNenUngDung;
            this.Font = ThemeHelper.FontNoiDung;
            this.AutoScroll = true;
            this.Padding = new Padding(24, 16, 24, 24);

            pnlMainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            pnlMainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 0: Thông báo
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 1: Profile Header Summary Card
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 2: Nhóm định danh & tài khoản (Chỉ xem)
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 3: Nhóm liên hệ (Chỉnh sửa)
            pnlMainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 4: Thanh nút thao tác

            // Banner thông báo kết quả
            pnlThongBao = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Height = 44,
                Visible = false,
                Margin = new Padding(0, 0, 0, 14),
                Padding = new Padding(16, 0, 16, 0),
                BorderRadius = ThemeHelper.RadiusMedium,
                BorderWidth = 1
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontNoiDungDam,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            pnlThongBao.Controls.Add(lblThongBao);
            pnlMainContainer.Controls.Add(pnlThongBao, 0, 0);

            // ====================================================
            // 1. PROFILE HEADER CARD (Tổng quan hồ sơ)
            // ====================================================
            var pnlProfileHeader = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Height = 96,
                BackColor = Color.White,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(20, 14, 20, 14)
            };

            // Avatar tròn lớn
            var pnlAvatar = new Panel
            {
                Size = new Size(64, 64),
                Location = new Point(20, 16),
                BackColor = ThemeHelper.MauChinh
            };
            pnlAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, 63, 63);
                pnlAvatar.Region = new Region(path);
            };

            string kyHieuAvatar = LayKyHieuAvatar(Session.HoTen);
            var lblAvatarText = new Label
            {
                Dock = DockStyle.Fill,
                Text = kyHieuAvatar,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlAvatar.Controls.Add(lblAvatarText);

            lblProfileTen = new Label
            {
                Text = Session.HoTen,
                Font = ThemeHelper.FontTieuDeTrang,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(96, 18),
                AutoSize = true
            };

            lblProfileSub = new Label
            {
                Text = $"@{Session.TenDangNhap}  •  Vai trò: {Session.TenVaiTro}  •  Mã nhân viên: #{Session.MaNhanVien:D4}",
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(98, 48),
                AutoSize = true
            };

            // Badge trạng thái bên phải Header
            var pnlStatusBadge = new RoundedPanel
            {
                Size = new Size(140, 36),
                Location = new Point(pnlProfileHeader.Width - 165, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = ThemeHelper.MauThanhCongNhe,
                BorderColor = ThemeHelper.MauThanhCong,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusMedium
            };
            var lblStatusText = new Label
            {
                Dock = DockStyle.Fill,
                Text = "● Đang hoạt động",
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = ThemeHelper.MauThanhCong,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlStatusBadge.Controls.Add(lblStatusText);

            pnlProfileHeader.Controls.Add(pnlAvatar);
            pnlProfileHeader.Controls.Add(lblProfileTen);
            pnlProfileHeader.Controls.Add(lblProfileSub);
            pnlProfileHeader.Controls.Add(pnlStatusBadge);

            pnlMainContainer.Controls.Add(pnlProfileHeader, 0, 1);

            // ====================================================
            // 2. NHÓM 1: THÔNG TIN HỆ THỐNG & ĐỊNH DANH (CHỈ XEM)
            // ====================================================
            var pnlNhomChiXem = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Height = 240,
                BackColor = Color.White,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(24, 18, 24, 18)
            };

            var lblTieuDeChiXem = new Label
            {
                Text = "🔒 Thông tin hệ thống & Tài khoản (Chỉ xem)",
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 16),
                AutoSize = true
            };

            var lblMoTaChiXem = new Label
            {
                Text = "Các thông tin định danh và phân quyền do Quản lý hệ thống cấp phép, không thể tự sửa đổi.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(24, 42),
                AutoSize = true
            };

            pnlNhomChiXem.Controls.Add(lblTieuDeChiXem);
            pnlNhomChiXem.Controls.Add(lblMoTaChiXem);

            // Bố cục 2 cột x 3 hàng
            var tlpChiXem = new TableLayoutPanel
            {
                Location = new Point(24, 70),
                Size = new Size(pnlNhomChiXem.Width - 48, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ColumnCount = 2,
                RowCount = 3,
                BackColor = Color.Transparent
            };
            tlpChiXem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpChiXem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpChiXem.RowStyles.Add(new RowStyle(SizeType.Absolute, 48f));
            tlpChiXem.RowStyles.Add(new RowStyle(SizeType.Absolute, 48f));
            tlpChiXem.RowStyles.Add(new RowStyle(SizeType.Absolute, 48f));

            string ngayVaoLamStr = Session.NgayVaoLam.HasValue && Session.NgayVaoLam.Value > DateTime.MinValue
                ? Session.NgayVaoLam.Value.ToString("dd/MM/yyyy")
                : "Chưa cập nhật";

            tlpChiXem.Controls.Add(TaoBadgeChiXem("MÃ NHÂN VIÊN", $"NV{Session.MaNhanVien:D4}", new Padding(0, 0, 8, 6)), 0, 0);
            tlpChiXem.Controls.Add(TaoBadgeChiXem("HỌ VÀ TÊN", Session.HoTen, new Padding(8, 0, 0, 6)), 1, 0);

            tlpChiXem.Controls.Add(TaoBadgeChiXem("TÊN ĐĂNG NHẬP", Session.TenDangNhap, new Padding(0, 0, 8, 6)), 0, 1);
            tlpChiXem.Controls.Add(TaoBadgeChiXem("VAI TRÒ / QUYỀN HẠN", Session.TenVaiTro, new Padding(8, 0, 0, 6)), 1, 1);

            tlpChiXem.Controls.Add(TaoBadgeChiXem("NGÀY VÀO LÀM", ngayVaoLamStr, new Padding(0, 0, 8, 0)), 0, 2);
            tlpChiXem.Controls.Add(TaoBadgeChiXem("TRẠNG THÁI TÀI KHOẢN", "Đang hoạt động", new Padding(8, 0, 0, 0), ThemeHelper.MauThanhCong), 1, 2);

            pnlNhomChiXem.Controls.Add(tlpChiXem);
            pnlMainContainer.Controls.Add(pnlNhomChiXem, 0, 2);

            // ====================================================
            // 3. NHÓM 2: THÔNG TIN LIÊN HỆ CÁ NHÂN (CHỈNH SỬA)
            // ====================================================
            var pnlNhomChinhSua = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Height = 240,
                BackColor = Color.White,
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusLarge,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(24, 18, 24, 18)
            };

            var lblTieuDeChinhSua = new Label
            {
                Text = "📝 Thông tin liên hệ cá nhân",
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(24, 16),
                AutoSize = true
            };

            var lblMoTaChinhSua = new Label
            {
                Text = "Cập nhật số điện thoại, hòm thư điện tử và địa chỉ cư trú của bạn khi có sự thay đổi.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(24, 42),
                AutoSize = true
            };

            pnlNhomChinhSua.Controls.Add(lblTieuDeChinhSua);
            pnlNhomChinhSua.Controls.Add(lblMoTaChinhSua);

            var tlpInputs = new TableLayoutPanel
            {
                Location = new Point(24, 70),
                Size = new Size(pnlNhomChinhSua.Width - 48, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.Transparent
            };
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 70f));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 75f));

            // SĐT
            var pnlSdt = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 8, 0) };
            var lblSdt = new Label { Text = "Số điện thoại:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(0, 2), AutoSize = true };
            txtSoDienThoai = new RoundedTextBox
            {
                Location = new Point(0, 24),
                Height = ThemeHelper.ChieuCaoInputChuan,
                Dock = DockStyle.Bottom,
                BorderRadius = ThemeHelper.RadiusMedium,
                PlaceholderText = "Nhập số điện thoại liên hệ"
            };
            txtSoDienThoai.TextChanged += (s, e) => KiemTraThayDoiDuLieu();
            pnlSdt.Controls.Add(lblSdt);
            pnlSdt.Controls.Add(txtSoDienThoai);
            tlpInputs.Controls.Add(pnlSdt, 0, 0);

            // Email
            var pnlEmail = new Panel { Dock = DockStyle.Fill, Margin = new Padding(8, 0, 0, 0) };
            var lblEmail = new Label { Text = "Email liên hệ:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(0, 2), AutoSize = true };
            txtEmail = new RoundedTextBox
            {
                Location = new Point(0, 24),
                Height = ThemeHelper.ChieuCaoInputChuan,
                Dock = DockStyle.Bottom,
                BorderRadius = ThemeHelper.RadiusMedium,
                PlaceholderText = "Nhập địa chỉ email liên hệ"
            };
            txtEmail.TextChanged += (s, e) => KiemTraThayDoiDuLieu();
            pnlEmail.Controls.Add(lblEmail);
            pnlEmail.Controls.Add(txtEmail);
            tlpInputs.Controls.Add(pnlEmail, 1, 0);

            // Địa chỉ
            var pnlDiaChi = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 6, 0, 0) };
            var lblDiaChi = new Label { Text = "Địa chỉ cư trú:", Font = ThemeHelper.FontLabel, ForeColor = ThemeHelper.MauChuChinh, Location = new Point(0, 2), AutoSize = true };
            txtDiaChi = new RoundedTextBox
            {
                Location = new Point(0, 24),
                Height = ThemeHelper.ChieuCaoInputChuan,
                Dock = DockStyle.Bottom,
                BorderRadius = ThemeHelper.RadiusMedium,
                PlaceholderText = "Nhập địa chỉ cư trú đầy đủ"
            };
            txtDiaChi.TextChanged += (s, e) => KiemTraThayDoiDuLieu();
            pnlDiaChi.Controls.Add(lblDiaChi);
            pnlDiaChi.Controls.Add(txtDiaChi);
            tlpInputs.Controls.Add(pnlDiaChi, 0, 1);
            tlpInputs.SetColumnSpan(pnlDiaChi, 2);

            pnlNhomChinhSua.Controls.Add(tlpInputs);
            pnlMainContainer.Controls.Add(pnlNhomChinhSua, 0, 3);

            // ====================================================
            // 4. THANH NÚT THAO TÁC (CHỈNH SỬA / LƯU / HỦY)
            // ====================================================
            var pnlActions = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 52,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 4, 0, 0)
            };

            btnChinhSua = new RoundedButton
            {
                Text = "✏️ Chỉnh sửa thông tin",
                Location = new Point(0, 4),
                Size = new Size(180, ThemeHelper.ChieuCaoButtonChuan),
                BorderRadius = ThemeHelper.RadiusMedium,
                NormalColor = ColorTranslator.FromHtml("#EFF6FF"),
                HoverColor = ColorTranslator.FromHtml("#DBEAFE"),
                ForeColor = ThemeHelper.MauChinh,
                Font = ThemeHelper.FontButton
            };
            btnChinhSua.Click += (s, e) => BatCheDoChinhSua(true);

            btnLuuThayDoi = new RoundedButton
            {
                Text = "✓ Lưu thay đổi",
                Location = new Point(190, 4),
                Size = new Size(140, ThemeHelper.ChieuCaoButtonChuan),
                BorderRadius = ThemeHelper.RadiusMedium,
                NormalColor = ThemeHelper.MauChinh,
                HoverColor = ThemeHelper.MauChinhHover,
                DisabledColor = ThemeHelper.MauVoHieuHoa,
                Font = ThemeHelper.FontButton,
                Enabled = false
            };
            btnLuuThayDoi.Click += BtnLuuThayDoi_Click;

            btnHuyChinhSua = new RoundedButton
            {
                Text = "✕ Hủy chỉnh sửa",
                Location = new Point(340, 4),
                Size = new Size(140, ThemeHelper.ChieuCaoButtonChuan),
                BorderRadius = ThemeHelper.RadiusMedium,
                NormalColor = ColorTranslator.FromHtml("#F1F5F9"),
                HoverColor = ColorTranslator.FromHtml("#E2E8F0"),
                ForeColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontButton,
                Visible = false
            };
            btnHuyChinhSua.Click += (s, e) =>
            {
                NapDuLieuTuSession();
                BatCheDoChinhSua(false);
                AnThongBao();
            };

            var lblGhiChuLuu = new Label
            {
                Text = "Dữ liệu sau khi lưu thành công sẽ tự động đồng bộ vào CSDL và phiên đăng nhập hiện tại.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(495, 15),
                AutoSize = true
            };

            pnlActions.Controls.Add(btnChinhSua);
            pnlActions.Controls.Add(btnLuuThayDoi);
            pnlActions.Controls.Add(btnHuyChinhSua);
            pnlActions.Controls.Add(lblGhiChuLuu);

            pnlMainContainer.Controls.Add(pnlActions, 0, 4);

            this.Controls.Add(pnlMainContainer);
        }

        private Control TaoBadgeChiXem(string labelText, string valueText, Padding margin, Color? mauGiaTri = null)
        {
            var pnlBadge = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorTranslator.FromHtml("#F8FAFC"),
                BorderColor = ThemeHelper.MauDuongVien,
                BorderWidth = 1,
                BorderRadius = ThemeHelper.RadiusMedium,
                Margin = margin,
                Padding = new Padding(12, 5, 12, 5)
            };

            var lblLabel = new Label
            {
                Text = labelText,
                Font = ThemeHelper.TaoFont(8f, FontStyle.Bold),
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(10, 4),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblValue = new Label
            {
                Text = valueText,
                Font = ThemeHelper.FontNoiDungDam,
                ForeColor = mauGiaTri ?? ThemeHelper.MauChuChinh,
                Location = new Point(10, 20),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            pnlBadge.Controls.Add(lblLabel);
            pnlBadge.Controls.Add(lblValue);
            return pnlBadge;
        }

        private void NapDuLieuTuSession()
        {
            _dangNapDuLieu = true;
            try
            {
                _sdtBanDau = Session.SoDienThoai?.Trim() ?? string.Empty;
                _emailBanDau = Session.Email?.Trim() ?? string.Empty;
                _diaChiBanDau = Session.DiaChi?.Trim() ?? string.Empty;

                txtSoDienThoai.Text = _sdtBanDau;
                txtEmail.Text = _emailBanDau;
                txtDiaChi.Text = _diaChiBanDau;

                lblProfileTen.Text = Session.HoTen;
                lblProfileSub.Text = $"@{Session.TenDangNhap}  •  Vai trò: {Session.TenVaiTro}  •  Mã nhân viên: #{Session.MaNhanVien:D4}";

                // Ban đầu không có thay đổi -> nút Lưu Disabled
                btnLuuThayDoi.Enabled = false;
                BatCheDoChinhSua(false);
            }
            finally
            {
                _dangNapDuLieu = false;
            }
        }

        private void BatCheDoChinhSua(bool dangSua)
        {
            _dangChinhSua = dangSua;
            btnChinhSua.Visible = !dangSua;
            btnHuyChinhSua.Visible = dangSua;

            txtSoDienThoai.InnerTextBox.ReadOnly = !dangSua;
            txtEmail.InnerTextBox.ReadOnly = !dangSua;
            txtDiaChi.InnerTextBox.ReadOnly = !dangSua;

            Color nenInput = dangSua ? Color.White : ColorTranslator.FromHtml("#F8FAFC");
            txtSoDienThoai.BackColor = nenInput;
            txtEmail.BackColor = nenInput;
            txtDiaChi.BackColor = nenInput;

            if (dangSua)
            {
                txtSoDienThoai.Focus();
            }
        }

        private void KiemTraThayDoiDuLieu()
        {
            if (_dangNapDuLieu) return;

            string sdtHienTai = txtSoDienThoai.Text.Trim();
            string emailHienTai = txtEmail.Text.Trim();
            string diaChiHienTai = txtDiaChi.Text.Trim();

            bool coThayDoi = (sdtHienTai != _sdtBanDau)
                          || (emailHienTai != _emailBanDau)
                          || (diaChiHienTai != _diaChiBanDau);

            btnLuuThayDoi.Enabled = coThayDoi;
            if (coThayDoi && !_dangChinhSua)
            {
                BatCheDoChinhSua(true);
            }
        }

        private void BtnLuuThayDoi_Click(object? sender, EventArgs e)
        {
            string sdtHienTai = txtSoDienThoai.Text.Trim();
            string emailHienTai = txtEmail.Text.Trim();
            string diaChiHienTai = txtDiaChi.Text.Trim();

            if (sdtHienTai == _sdtBanDau && emailHienTai == _emailBanDau && diaChiHienTai == _diaChiBanDau)
            {
                btnLuuThayDoi.Enabled = false;
                return;
            }

            AnThongBao();
            btnLuuThayDoi.Enabled = false;

            try
            {
                var ketQua = _taiKhoanBLL.CapNhatThongTinLienHe(
                    Session.MaNhanVien,
                    sdtHienTai,
                    emailHienTai,
                    diaChiHienTai);

                if (ketQua.ThanhCong)
                {
                    HienThongBao(ketQua.ThongBao, laLoi: false);

                    _sdtBanDau = sdtHienTai;
                    _emailBanDau = emailHienTai;
                    _diaChiBanDau = diaChiHienTai;

                    btnLuuThayDoi.Enabled = false;
                    BatCheDoChinhSua(false);
                }
                else
                {
                    HienThongBao(ketQua.ThongBao, laLoi: true);
                    btnLuuThayDoi.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HienThongBao("Đã xảy ra lỗi trong quá trình lưu thông tin: " + ex.Message, laLoi: true);
                btnLuuThayDoi.Enabled = true;
            }
        }

        private void HienThongBao(string thongBao, bool laLoi)
        {
            pnlThongBao.Visible = true;
            pnlThongBao.BackColor = laLoi ? ThemeHelper.MauLoiNhe : ThemeHelper.MauThanhCongNhe;
            pnlThongBao.BorderColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.ForeColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.Text = (laLoi ? "⚠  " : "✓  ") + thongBao;
        }

        private void AnThongBao()
        {
            pnlThongBao.Visible = false;
            lblThongBao.Text = string.Empty;
        }

        private string LayKyHieuAvatar(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen)) return "ND";
            var parts = hoTen.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1) return parts[0][0].ToString().ToUpper();
            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
        }
    }
}
