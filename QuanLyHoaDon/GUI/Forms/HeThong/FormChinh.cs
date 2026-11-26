using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.GUI.Forms.HoaDon;
using QuanLyHoaDon.GUI.Forms.KhachHang;
using QuanLyHoaDon.GUI.Forms.NhanVien;
using QuanLyHoaDon.GUI.Forms.SanPham;
using QuanLyHoaDon.GUI.Forms.ThongKe;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public partial class FormChinh : Form
    {
        // Danh sách các button menu để quản lý trạng thái active
        private readonly Dictionary<string, Button> _menuButtons = new();
        private string _maChucNangHienTai = string.Empty;

        // Cache các màn hình nội dung đã tạo
        private readonly Dictionary<string, Control> _manHinhCache = new();

        public FormChinh()
        {
            InitializeComponent();
            ApDungGiaoDienVaKhoiTao();
        }

        private void ApDungGiaoDienVaKhoiTao()
        {
            // Thiết lập font chữ và màu sắc chuẩn theo ThemeHelper
            this.Font = ThemeHelper.FontNoiDung;
            this.BackColor = ThemeHelper.MauNenUngDung;

            // Header Sidebar
            ApDungGiaoDienSidebarSang();
            pnlAvatarSidebar.BackColor = ThemeHelper.MauChinh;
            lblBrandLogo.BackColor = ThemeHelper.MauChinh;

            // Bo góc tròn cho logo và avatar Sidebar
            pnlAvatarSidebar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, pnlAvatarSidebar.Width - 1, pnlAvatarSidebar.Height - 1);
                pnlAvatarSidebar.Region = new Region(path);
            };

            lblBrandLogo.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, lblBrandLogo.Width - 1, lblBrandLogo.Height - 1);
                lblBrandLogo.Region = new Region(path);
            };

            pnlAvatarCircle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, pnlAvatarCircle.Width - 1, pnlAvatarCircle.Height - 1);
                pnlAvatarCircle.Region = new Region(path);
            };

            // Topbar
            pnlTopbar.BackColor = ThemeHelper.MauNenNoiDung;
            pnlTopbar.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 1, pnlTopbar.Width, pnlTopbar.Height - 1);
            };

            lblTopbarTieuDe.Font = ThemeHelper.FontTieuDeTrang;
            lblTopbarTieuDe.ForeColor = ThemeHelper.MauChuChinh;

            lblTopbarBreadcrumb.Font = ThemeHelper.FontChuThich;
            lblTopbarBreadcrumb.ForeColor = ThemeHelper.MauChuPhu;

            // Sự kiện click khu vực người dùng Topbar -> Mở Thông tin cá nhân
            pnlNguoiDung.Click += (s, e) => DieuHuongTheoMa("ThongTinCaNhan");
            lblTenNguoiDung.Click += (s, e) => DieuHuongTheoMa("ThongTinCaNhan");
            lblVaiTroBadge.Click += (s, e) => DieuHuongTheoMa("ThongTinCaNhan");
            pnlAvatarCircle.Click += (s, e) => DieuHuongTheoMa("ThongTinCaNhan");
            lblAvatarKyHieu.Click += (s, e) => DieuHuongTheoMa("ThongTinCaNhan");

            // Hiển thị thông tin người dùng từ Session
            lblTenNguoiDung.Text = Session.HoTen;
            lblVaiTroBadge.Text = Session.LaQuanLy ? "Quản lý" : "Nhân viên";

            lblTenNguoiDungSidebar.Text = Session.HoTen;
            lblVaiTroSidebar.Text = Session.LaQuanLy ? "Quản lý" : "Nhân viên";

            string kyHieuAvatar = LayKyHieuAvatar(Session.HoTen);
            lblAvatarKyHieu.Text = kyHieuAvatar;
            lblAvatarSidebar.Text = kyHieuAvatar;

            // Xử lý sự kiện click tài khoản ở Sidebar -> Mở context menu hướng lên
            pnlTaiKhoanSidebar.Click += (s, e) => MoMenuTaiKhoan();
            lblTenNguoiDungSidebar.Click += (s, e) => MoMenuTaiKhoan();
            lblVaiTroSidebar.Click += (s, e) => MoMenuTaiKhoan();
            lblChevronSidebar.Click += (s, e) => MoMenuTaiKhoan();
            pnlAvatarSidebar.Click += (s, e) => MoMenuTaiKhoan();
            lblAvatarSidebar.Click += (s, e) => MoMenuTaiKhoan();

            // Hover effect cho pnlTaiKhoanSidebar
            pnlTaiKhoanSidebar.MouseEnter += (s, e) => pnlTaiKhoanSidebar.BackColor = ThemeHelper.MauSidebarAccountHover;
            pnlTaiKhoanSidebar.MouseLeave += (s, e) => pnlTaiKhoanSidebar.BackColor = ThemeHelper.MauSidebarAccount;

            // Context Menu Styling
            cmsTaiKhoan.Font = ThemeHelper.FontNoiDung;
            cmsTaiKhoan.BackColor = Color.White;
            cmsTaiKhoan.ShowImageMargin = false;

            tsmiThongTinCaNhan.Font = ThemeHelper.FontNoiDung;
            tsmiThongTinCaNhan.ForeColor = ThemeHelper.MauChuChinh;
            tsmiThongTinCaNhan.Click += (s, e) => DieuHuongTheoMa("ThongTinCaNhan");

            tsmiDoiMatKhau.Font = ThemeHelper.FontNoiDung;
            tsmiDoiMatKhau.ForeColor = ThemeHelper.MauChuChinh;
            tsmiDoiMatKhau.Click += (s, e) => DieuHuongTheoMa("DoiMatKhau");

            tsmiDangXuat.Font = ThemeHelper.FontNoiDung;
            tsmiDangXuat.ForeColor = ThemeHelper.MauNguyHiem;
            tsmiDangXuat.Click += (s, e) => XacNhanVaDangXuat();

            // Phân quyền cho Context Menu: Chỉ Nhân viên mới có mục Đổi mật khẩu
            tsmiDoiMatKhau.Visible = Session.LaNhanVien;

            // Khởi tạo danh sách menu theo phân quyền
            KhoiTaoSidebarTheoPhanQuyen();

            // Mặc định nạp màn hình Tổng quan
            DieuHuongTheoMa("TongQuan");
        }

        /// <summary>
        /// Sidebar nền trắng: đổi nền các panel và chuyển chữ trắng thành chữ tối
        /// </summary>
        private void ApDungGiaoDienSidebarSang()
        {
            var sidebar = flpSidebarMenu.Parent;
            if (sidebar != null && sidebar != this)
            {
                sidebar.BackColor = ThemeHelper.MauSidebar;
                sidebar.Paint += (s, e) =>
                {
                    using var pen = new Pen(ThemeHelper.MauSidebarBorder);
                    e.Graphics.DrawLine(pen, sidebar.Width - 1, 0, sidebar.Width - 1, sidebar.Height);
                };
            }

            flpSidebarMenu.BackColor = ThemeHelper.MauSidebar;
            pnlSidebarBrand.BackColor = ThemeHelper.MauSidebarHeader;
            pnlTaiKhoanSidebar.BackColor = ThemeHelper.MauSidebarAccount;

            DoiMauChuSidebarSang(pnlSidebarBrand);
            DoiMauChuSidebarSang(pnlTaiKhoanSidebar);
        }

        private void DoiMauChuSidebarSang(Control goc)
        {
            foreach (Control c in goc.Controls)
            {
                // Bỏ qua chữ nằm trong logo/avatar tròn màu xanh (giữ chữ trắng)
                if (c is Label lb && c != lblBrandLogo && c != lblAvatarSidebar && lb.ForeColor.GetBrightness() > 0.6f)
                {
                    lb.ForeColor = lb.Font.Bold ? ThemeHelper.MauChuChinh : ThemeHelper.MauChuPhuSidebar;
                }
                if (c.HasChildren) DoiMauChuSidebarSang(c);
            }
        }

        private void MoMenuTaiKhoan()
        {
            // Dropdown hiển thị hướng lên trên từ vị trí pnlTaiKhoanSidebar
            int chieuCaoMenu = cmsTaiKhoan.Height;
            if (chieuCaoMenu <= 0)
            {
                chieuCaoMenu = Session.LaNhanVien ? 115 : 85;
            }

            Point diemHienThi = new Point(10, -chieuCaoMenu - 4);
            cmsTaiKhoan.Show(pnlTaiKhoanSidebar, diemHienThi);
        }

        private string LayKyHieuAvatar(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                return "QL";
            }
            var parts = hoTen.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                return parts[0][0].ToString().ToUpper();
            }
            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
        }

        /// <summary>
        /// Khởi tạo các mục Sidebar dựa trên vai trò của người dùng
        /// - QuanLy: Được hiển thị toàn bộ các phân hệ quản lý
        /// - NhanVien: KHÔNG hiển thị "HeThong" và "NhanVien"
        /// </summary>
        private void KhoiTaoSidebarTheoPhanQuyen()
        {
            flpSidebarMenu.Controls.Clear();
            _menuButtons.Clear();

            ThemItemSidebar("TongQuan", "📊  Tổng quan", () => DieuHuongTheoMa("TongQuan"));

            if (Session.LaQuanLy)
            {
                ThemItemSidebar("NhanVien", "👤  Người dùng", () => DieuHuongTheoMa("NhanVien"));
            }

            ThemItemSidebar("KhachHang", "👥  Khách hàng", () => DieuHuongTheoMa("KhachHang"));
            ThemItemSidebar("SanPham", "📦  Sản phẩm", () => DieuHuongTheoMa("SanPham"));
            ThemItemSidebar("HoaDon", "🧾  Hóa đơn", () => DieuHuongTheoMa("HoaDon"));
            ThemItemSidebar("BaoCao", "📈  Thống kê & Báo cáo", () => DieuHuongTheoMa("BaoCao"));

            if (Session.LaQuanLy)
            {
                ThemItemSidebar("HeThong", "⚙️  Hệ thống & Cấu hình", () => DieuHuongTheoMa("HeThong"));
            }
        }

        private void ThemItemSidebar(string maChucNang, string textHienThi, Action hanhDong)
        {
            var btn = new RoundedButton
            {
                Name = "btnMenu_" + maChucNang,
                Text = textHienThi,
                Size = new Size(250, 48),
                BackColor = ThemeHelper.MauSidebar,
                HoverColor = ThemeHelper.MauSidebarHover,
                ForeColor = ThemeHelper.MauChuTrenSidebar,
                Font = ThemeHelper.FontNoiDung,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                Margin = new Padding(10, 3, 10, 3),
                BorderRadius = 10
            };

            btn.Click += (s, e) => hanhDong();
            _menuButtons[maChucNang] = btn;

            flpSidebarMenu.Controls.Add(btn);
        }

        /// <summary>
        /// Điều hướng tập trung theo mã chức năng có kiểm tra phân quyền
        /// </summary>
        public void DieuHuongTheoMa(string maChucNang)
        {
            // KIỂM TRA PHÂN QUYỀN TẠI TẦNG ĐIỀU HƯỚNG
            if ((maChucNang == "HeThong" || maChucNang == "NhanVien") && !Session.LaQuanLy)
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập vào phân hệ này.\nChức năng chỉ dành riêng cho tài khoản Quản lý.",
                    "Từ Chối Truy Cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (maChucNang == "DoiMatKhau" && !Session.LaNhanVien)
            {
                MessageBox.Show(
                    "Chức năng cập nhật mật khẩu chỉ dành cho tài khoản Nhân viên.",
                    "Từ Chối Truy Cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            _maChucNangHienTai = maChucNang;
            CapNhatTrangThaiNutActive(maChucNang);

            switch (maChucNang)
            {
                case "TongQuan":
                    if (!_manHinhCache.TryGetValue("TongQuan", out var ucTongQuan))
                    {
                        var uc = new UCTongQuan();
                        uc.OnChuyenChucNang += DieuHuongTheoMa;
                        ucTongQuan = uc;
                        _manHinhCache["TongQuan"] = ucTongQuan;
                    }
                    DieuHuong(ucTongQuan, "Tổng quan hệ thống", "Trang chủ / Tổng quan");
                    break;

                case "HeThong":
                    if (!_manHinhCache.TryGetValue("HeThong", out var ucHeThong))
                    {
                        ucHeThong = new UCDangPhatTrien(
                            "Phân Hệ Quản Lý Hệ Thống & Cấu Hình",
                            "Quản Trị Viên (Admin)",
                            "Cung cấp các công cụ cấu hình sao lưu cơ sở dữ liệu và giám sát hệ thống.",
                            "⚙️");
                        _manHinhCache["HeThong"] = ucHeThong;
                    }
                    DieuHuong(ucHeThong, "Hệ thống & Cấu hình", "Trang chủ / Quản trị hệ thống");
                    break;

                case "NhanVien":
                    if (!_manHinhCache.TryGetValue("NhanVien", out var ucNhanVien))
                    {
                        ucNhanVien = new UCNhanVien();
                        _manHinhCache["NhanVien"] = ucNhanVien;
                    }
                    DieuHuong(ucNhanVien, "Quản lý nhân sự & tài khoản", "Trang chủ / Quản lý nhân viên");
                    break;

                case "KhachHang":
                    if (!_manHinhCache.TryGetValue("KhachHang", out var ucKhachHang))
                    {
                        ucKhachHang = new UCKhachHang();
                        _manHinhCache["KhachHang"] = ucKhachHang;
                    }
                    DieuHuong(ucKhachHang, "Quản lý khách hàng", "Trang chủ / Danh mục khách hàng");
                    break;

                case "SanPham":
                    if (!_manHinhCache.TryGetValue("SanPham", out var ucSanPham))
                    {
                        ucSanPham = new UCSanPham();
                        _manHinhCache["SanPham"] = ucSanPham;
                    }
                    DieuHuong(ucSanPham, "Quản lý sản phẩm", "Trang chủ / Danh mục sản phẩm");
                    break;

                case "HoaDon":
                    if (!_manHinhCache.TryGetValue("HoaDon", out var ucHoaDon))
                    {
                        ucHoaDon = new UCHoaDon();
                        _manHinhCache["HoaDon"] = ucHoaDon;
                    }
                    DieuHuong(ucHoaDon, "Quản lý hóa đơn bán hàng", "Trang chủ / Quản lý bán hàng / Hóa đơn");
                    break;

                case "BaoCao":
                    if (!_manHinhCache.TryGetValue("BaoCao", out var ucBaoCao))
                    {
                        ucBaoCao = new UCThongKe();
                        _manHinhCache["BaoCao"] = ucBaoCao;
                    }
                    DieuHuong(ucBaoCao, "Thống kê & Báo cáo doanh thu", "Trang chủ / Báo cáo doanh thu");
                    break;

                case "ThongTinCaNhan":
                case "HoSoCaNhan":
                    var ucThongTin = new UCThongTinCaNhan();
                    DieuHuong(ucThongTin, "Thông tin cá nhân", "Trang chủ / Người dùng / Thông tin cá nhân");
                    break;

                case "DoiMatKhau":
                    var ucDoiMK = new UCDoiMatKhau();
                    ucDoiMK.OnQuayLai += () => DieuHuongTheoMa("TongQuan");
                    DieuHuong(ucDoiMK, "Cập nhật mật khẩu", "Trang chủ / Người dùng / Đổi mật khẩu");
                    break;
            }
        }

        /// <summary>
        /// Phương thức điều hướng tổng quát dành cho các thành viên khác tích hợp
        /// </summary>
        public void DieuHuong(Control control, string tieuDe, string breadcrumb)
        {
            lblTopbarTieuDe.Text = tieuDe;
            lblTopbarBreadcrumb.Text = breadcrumb;

            pnlNoiDungChinh.SuspendLayout();
            pnlNoiDungChinh.Controls.Clear();

            control.Dock = DockStyle.Fill;
            pnlNoiDungChinh.Controls.Add(control);
            pnlNoiDungChinh.ResumeLayout(true);

            // Bo góc các nút thường trong màn hình vừa nạp
            ThemeHelper.BoGocTatCaNut(control);
        }

        private void CapNhatTrangThaiNutActive(string maChucNang)
        {
            foreach (var kvp in _menuButtons)
            {
                var rb = (RoundedButton)kvp.Value;
                bool active = kvp.Key == maChucNang;

                rb.BackColor = active ? ThemeHelper.MauSidebarActive : ThemeHelper.MauSidebar;
                rb.HoverColor = active ? ThemeHelper.MauSidebarActive : ThemeHelper.MauSidebarHover;
                rb.Font = active ? ThemeHelper.FontNoiDungDam : ThemeHelper.FontNoiDung;
                // Mục đang chọn: nền trắng, chữ xanh. Mục còn lại: nền xanh, chữ trắng
                rb.ForeColor = active ? ThemeHelper.MauChinhHover : ThemeHelper.MauChuTrenSidebar;
                rb.AccentBarColor = active ? ThemeHelper.MauChinh : Color.Empty;
                rb.Invalidate();
            }
        }

        private void XacNhanVaDangXuat()
        {
            using var dlg = new FormXacNhanDangXuat();
            var luaChon = dlg.ShowDialog(this);

            if (luaChon == DialogResult.Yes)
            {
                Session.DangXuat();
                this.Close();
            }
        }
    }
}