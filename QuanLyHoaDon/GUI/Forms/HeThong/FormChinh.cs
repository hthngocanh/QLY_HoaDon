using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public partial class FormChinh : Form
    {
        // Danh sach cac button menu de quan ly trang thai active
        private readonly Dictionary<string, Button> _menuButtons = new();
        private string _maChucNangHienTai = string.Empty;

        // Cache cac man hinh noi dung da tao
        private readonly Dictionary<string, Control> _manHinhCache = new();

        public FormChinh()
        {
            InitializeComponent();
            ApDungGiaoDienVaKhoiTao();
        }

        private void ApDungGiaoDienVaKhoiTao()
        {
            this.Font = ThemeHelper.TaoFont(9.5f);
            this.BackColor = ThemeHelper.MauNenUngDung;

            // Ve duong vien phan tach duoi Topbar
            pnlTopbar.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 1, pnlTopbar.Width, pnlTopbar.Height - 1);
            };

            // Hien thi thong tin nguoi dung tren Topbar
            CapNhatThongTinNguoiDungTopbar();

            // Click vao khu vuc user de xem ho so
            pnlNguoiDung.Click += (s, e) => DieuHuongTheoMa("HoSoCaNhan");
            lblTenNguoiDung.Click += (s, e) => DieuHuongTheoMa("HoSoCaNhan");
            lblVaiTroBadge.Click += (s, e) => DieuHuongTheoMa("HoSoCaNhan");
            pnlAvatarCircle.Click += (s, e) => DieuHuongTheoMa("HoSoCaNhan");
            lblAvatarKyHieu.Click += (s, e) => DieuHuongTheoMa("HoSoCaNhan");

            // Khoi tao danh sach menu Sidebar theo phan quyen
            KhoiTaoSidebarTheoPhanQuyen();

            // Mac dinh mo man hinh Tong quan
            DieuHuongTheoMa("TongQuan");
        }

        private void CapNhatThongTinNguoiDungTopbar()
        {
            lblTenNguoiDung.Text = string.IsNullOrWhiteSpace(Session.HoTen) ? Session.TenDangNhap : Session.HoTen;
            lblVaiTroBadge.Text = Session.LaQuanLy ? "Quản lý hệ thống" : "Nhân viên bán hàng";
            lblVaiTroBadge.ForeColor = Session.LaQuanLy ? ThemeHelper.MauChinh : ThemeHelper.MauThanhCong;
            lblAvatarKyHieu.Text = LayKyHieuAvatar(lblTenNguoiDung.Text);
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
        /// Khoi tao cac muc Sidebar dua tren vai tro cua nguoi dung
        /// - QuanLy: Duoc hien thi toan bo 9 muc
        /// - NhanVien: KHONG hien thi "HeThong" va "NhanVien"
        /// </summary>
        private void KhoiTaoSidebarTheoPhanQuyen()
        {
            flpSidebarMenu.Controls.Clear();
            _menuButtons.Clear();

            // 1. Tong quan (Tat ca)
            ThemItemSidebar("TongQuan", "📊  Tổng quan", () => DieuHuongTheoMa("TongQuan"));

            // 2. He thong & Tai khoan (Chi QuanLy)
            if (Session.LaQuanLy)
            {
                ThemItemSidebar("HeThong", "⚙️  Hệ thống & Tài khoản", () => DieuHuongTheoMa("HeThong"));
            }

            // 3. Nhan vien (Chi QuanLy)
            if (Session.LaQuanLy)
            {
                ThemItemSidebar("NhanVien", "👔  Nhân viên", () => DieuHuongTheoMa("NhanVien"));
            }

            // 4. Khach hang (Tat ca)
            ThemItemSidebar("KhachHang", "👥  Khách hàng", () => DieuHuongTheoMa("KhachHang"));

            // 5. San pham (Tat ca)
            ThemItemSidebar("SanPham", "📦  Sản phẩm", () => DieuHuongTheoMa("SanPham"));

            // 6. Hoa don (Tat ca)
            ThemItemSidebar("HoaDon", "🧾  Hóa đơn", () => DieuHuongTheoMa("HoaDon"));

            // 7. Thong ke & Bao cao (Tat ca)
            ThemItemSidebar("BaoCao", "📈  Thống kê & Báo cáo", () => DieuHuongTheoMa("BaoCao"));

            // Duong phan cach nhe
            var pnlDivider = new Panel
            {
                Size = new Size(240, 1),
                BackColor = Color.FromArgb(40, 60, 95),
                Margin = new Padding(10, 10, 10, 10)
            };
            flpSidebarMenu.Controls.Add(pnlDivider);

            // 8. Ho so ca nhan (Tat ca)
            ThemItemSidebar("HoSoCaNhan", "👤  Hồ sơ cá nhân", () => DieuHuongTheoMa("HoSoCaNhan"));

            // 9. Dang xuat (Tat ca)
            ThemItemSidebar("DangXuat", "🚪  Đăng xuất", XacNhanVaDangXuat, laNutNguyHiem: true);
        }

        private void ThemItemSidebar(string maChucNang, string textHienThi, Action hanhDong, bool laNutNguyHiem = false)
        {
            var btn = new Button
            {
                Name = "btnMenu_" + maChucNang,
                Text = textHienThi,
                Size = new Size(240, 46),
                BackColor = ThemeHelper.MauSidebar,
                ForeColor = laNutNguyHiem ? Color.FromArgb(248, 113, 113) : Color.FromArgb(226, 232, 240),
                Font = ThemeHelper.TaoFont(9.5f, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                Cursor = Cursors.Hand,
                Margin = new Padding(10, 3, 10, 3)
            };
            btn.FlatAppearance.BorderSize = 0;

            // Hover effect
            btn.MouseEnter += (s, e) =>
            {
                if (_maChucNangHienTai != maChucNang)
                {
                    btn.BackColor = laNutNguyHiem ? Color.FromArgb(153, 27, 27) : ThemeHelper.MauSidebarHover;
                }
            };

            btn.MouseLeave += (s, e) =>
            {
                if (_maChucNangHienTai != maChucNang)
                {
                    btn.BackColor = ThemeHelper.MauSidebar;
                }
            };

            btn.Click += (s, e) => hanhDong();

            if (!laNutNguyHiem)
            {
                _menuButtons[maChucNang] = btn;
            }

            flpSidebarMenu.Controls.Add(btn);
        }

        /// <summary>
        /// Dieu huong tap trung theo ma chuc nang co kiem tra phan quyen
        /// </summary>
        public void DieuHuongTheoMa(string maChucNang)
        {
            // KIEM TRA PHAN QUYEN TAI TANG DIEU HUONG
            if ((maChucNang == "HeThong" || maChucNang == "NhanVien") && !Session.LaQuanLy)
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập vào phân hệ này.\nChức năng chỉ dành riêng cho tài khoản Quản lý.",
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
                            "Phân Hệ Quản Lý Hệ Thống & Tài Khoản",
                            "Quản Trị Viên (Admin)",
                            "Cung cấp các công cụ cấu hình hệ thống, quản lý tài khoản người dùng, phân quyền truy cập và sao lưu cơ sở dữ liệu.",
                            "⚙️");
                        _manHinhCache["HeThong"] = ucHeThong;
                    }
                    DieuHuong(ucHeThong, "Hệ thống & Tài khoản", "Trang chủ / Quản trị hệ thống");
                    break;

                case "NhanVien":
                    if (!_manHinhCache.TryGetValue("NhanVien", out var ucNhanVien))
                    {
                        ucNhanVien = new UCDangPhatTrien(
                            "Phân Hệ Quản Lý Nhân Viên",
                            "Quản Trị Viên (Admin)",
                            "Quản lý hồ sơ nhân sự, danh sách nhân viên bán hàng, thông tin liên hệ và trạng thái hoạt động.",
                            "👔");
                        _manHinhCache["NhanVien"] = ucNhanVien;
                    }
                    DieuHuong(ucNhanVien, "Quản lý nhân sự", "Trang chủ / Quản lý nhân viên");
                    break;

                case "KhachHang":
                    if (!_manHinhCache.TryGetValue("KhachHang", out var ucKhachHang))
                    {
                        ucKhachHang = new UCDangPhatTrien(
                            "Phân Hệ Quản Lý Khách Hàng",
                            "Thành viên 2 (Khách hàng & Sản phẩm)",
                            "Quản lý thông tin danh sách khách hàng, số điện thoại, email, địa chỉ và lịch sử giao dịch mua hàng.",
                            "👥");
                        _manHinhCache["KhachHang"] = ucKhachHang;
                    }
                    DieuHuong(ucKhachHang, "Quản lý khách hàng", "Trang chủ / Danh mục khách hàng");
                    break;

                case "SanPham":
                    if (!_manHinhCache.TryGetValue("SanPham", out var ucSanPham))
                    {
                        ucSanPham = new UCDangPhatTrien(
                            "Phân Hệ Quản Lý Sản Phẩm",
                            "Thành viên 2 (Khách hàng & Sản phẩm)",
                            "Quản lý danh mục mặt hàng bánh kẹo, đơn vị tính, đơn giá bán và số lượng tồn kho theo thời gian thực.",
                            "📦");
                        _manHinhCache["SanPham"] = ucSanPham;
                    }
                    DieuHuong(ucSanPham, "Quản lý sản phẩm", "Trang chủ / Danh mục sản phẩm");
                    break;

                case "HoaDon":
                    if (!_manHinhCache.TryGetValue("HoaDon", out var ucHoaDon))
                    {
                        ucHoaDon = new UCDangPhatTrien(
                            "Phân Hệ Lập & Quản Lý Hóa Đơn",
                            "Thành viên 3 (Hóa đơn)",
                            "Lập hóa đơn bán hàng mới, chọn khách hàng, thêm các dòng sản phẩm, tự động tính tổng tiền và in phiếu bán hàng.",
                            "🧾");
                        _manHinhCache["HoaDon"] = ucHoaDon;
                    }
                    DieuHuong(ucHoaDon, "Quản lý hóa đơn", "Trang chủ / Quản lý bán hàng / Hóa đơn");
                    break;

                case "BaoCao":
                    if (!_manHinhCache.TryGetValue("BaoCao", out var ucBaoCao))
                    {
                        ucBaoCao = new UCDangPhatTrien(
                            "Phân Hệ Thống Kê & Báo Cáo",
                            "Thành viên 4 (Thống kê & Báo cáo)",
                            "Báo cáo tổng hợp doanh thu theo ngày/tháng, thống kê top sản phẩm bán chạy và khách hàng tiềm năng.",
                            "📈");
                        _manHinhCache["BaoCao"] = ucBaoCao;
                    }
                    DieuHuong(ucBaoCao, "Thống kê & Báo cáo", "Trang chủ / Báo cáo doanh thu");
                    break;

                case "HoSoCaNhan":
                    var ucHoSo = new UCHoSoCaNhan();
                    DieuHuong(ucHoSo, "Hồ sơ cá nhân", "Trang chủ / Người dùng / Hồ sơ cá nhân");
                    break;
            }
        }

        /// <summary>
        /// Phuong thuc dieu huong tong quat danh cho cac thanh vien khac tich hop
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
        }

        private void CapNhatTrangThaiNutActive(string maChucNang)
        {
            foreach (var kvp in _menuButtons)
            {
                if (kvp.Key == maChucNang)
                {
                    kvp.Value.BackColor = ThemeHelper.MauSidebarActive;
                    kvp.Value.ForeColor = Color.White;
                    kvp.Value.Font = ThemeHelper.TaoFont(9.5f, FontStyle.Bold);
                }
                else
                {
                    kvp.Value.BackColor = ThemeHelper.MauSidebar;
                    kvp.Value.ForeColor = Color.FromArgb(226, 232, 240);
                    kvp.Value.Font = ThemeHelper.TaoFont(9.5f, FontStyle.Regular);
                }
            }
        }

        private void XacNhanVaDangXuat()
        {
            var luaChon = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống không?",
                "Xác Nhận Đăng Xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (luaChon == DialogResult.Yes)
            {
                Session.DangXuat();
                this.Close();
            }
        }
    }
}
