using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    public class UCThongTinCaNhan : UserControl
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;

        // Container chính
        private Panel pnlHeaderCard = null!;
        private Panel pnlBodyCard = null!;
        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;

        // Cac truong nhap lieu duoc phep sua
        private TextBox txtSoDienThoai = null!;
        private TextBox txtEmail = null!;
        private TextBox txtDiaChi = null!;

        // Nut Luu thay doi
        private Button btnLuuThayDoi = null!;

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

            // ====================================================
            // 1. Header Card (Avatar + Họ tên + Vai trò)
            // ====================================================
            pnlHeaderCard = new Panel
            {
                Location = new Point(30, 24),
                Size = new Size(1000, 100),
                BackColor = ThemeHelper.MauNenNoiDung,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlHeaderCard.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlHeaderCard.Width - 1, pnlHeaderCard.Height - 1);
            };

            var pnlAvatar = new Panel
            {
                Size = new Size(64, 64),
                Location = new Point(24, 18),
                BackColor = ThemeHelper.MauChinh
            };
            var lblAvatarText = new Label
            {
                Text = LayChuCaiDau(Session.HoTen),
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlAvatar.Controls.Add(lblAvatarText);

            var lblHoTen = new Label
            {
                Text = Session.HoTen,
                Font = ThemeHelper.FontTieuDeKhuVuc,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(106, 20),
                AutoSize = true
            };

            var lblVaiTroBadge = new Label
            {
                Text = Session.LaQuanLy ? "VAI TRÒ: QUẢN LÝ HỆ THỐNG" : "VAI TRÒ: NHÂN VIÊN BÁN HÀNG",
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = Session.LaQuanLy ? ThemeHelper.MauChinh : ThemeHelper.MauThanhCong,
                BackColor = Session.LaQuanLy ? ColorTranslator.FromHtml("#EFF6FF") : ColorTranslator.FromHtml("#F0FDF4"),
                Padding = new Padding(10, 5, 10, 5),
                Location = new Point(106, 54),
                AutoSize = true
            };

            pnlHeaderCard.Controls.Add(pnlAvatar);
            pnlHeaderCard.Controls.Add(lblHoTen);
            pnlHeaderCard.Controls.Add(lblVaiTroBadge);

            // ====================================================
            // 2. Body Card (Chia 2 nhom thong tin)
            // ====================================================
            pnlBodyCard = new Panel
            {
                Location = new Point(30, 140),
                Size = new Size(1000, 680),
                BackColor = ThemeHelper.MauNenNoiDung,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlBodyCard.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlBodyCard.Width - 1, pnlBodyCard.Height - 1);
            };

            // Banner thong bao ket qua
            pnlThongBao = new Panel
            {
                Location = new Point(24, 20),
                Size = new Size(950, 46),
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontNoiDungDam,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 14, 0)
            };
            pnlThongBao.Controls.Add(lblThongBao);
            pnlBodyCard.Controls.Add(pnlThongBao);

            // ----------------------------------------------------
            // Nhóm 1: THÔNG TIN CHỈ XEM (Read-only)
            // ----------------------------------------------------
            var pnlNhomChiXem = TaoNhomThongTin(
                "🔒  THÔNG TIN HỆ THỐNG (CHỈ XEM, KHÔNG THỂ CHỈNH SỬA)",
                "Các thông tin định danh và phân quyền do quản trị viên cấp phát.",
                new Point(24, 80),
                new Size(950, 240),
                ColorTranslator.FromHtml("#F8FAFC"));

            // Grid 2 cot cho 6 truong chi xem
            ThemFieldChiXem(pnlNhomChiXem, "Mã nhân viên", $"NV{Session.MaNhanVien:D4}", 60, 20, 430);
            ThemFieldChiXem(pnlNhomChiXem, "Họ và tên", Session.HoTen, 60, 480, 430);

            ThemFieldChiXem(pnlNhomChiXem, "Tên đăng nhập", Session.TenDangNhap, 120, 20, 430);
            ThemFieldChiXem(pnlNhomChiXem, "Chức vụ / Vai trò", Session.TenVaiTro, 120, 480, 430);

            string ngayVaoLamStr = Session.NgayVaoLam.HasValue && Session.NgayVaoLam.Value > DateTime.MinValue
                ? Session.NgayVaoLam.Value.ToString("dd/MM/yyyy")
                : "Chưa cập nhật";
            ThemFieldChiXem(pnlNhomChiXem, "Ngày vào làm", ngayVaoLamStr, 175, 20, 430);
            ThemFieldChiXem(pnlNhomChiXem, "Trạng thái nhân sự", Session.TrangThaiNhanVien, 175, 480, 430);

            pnlBodyCard.Controls.Add(pnlNhomChiXem);

            // ----------------------------------------------------
            // Nhóm 2: THÔNG TIN ĐƯỢC PHÉP CHỈNH SỬA
            // ----------------------------------------------------
            var pnlNhomChinhSua = TaoNhomThongTin(
                "✏  THÔNG TIN LIÊN HỆ (ĐƯỢC PHÉP CHỈNH SỬA)",
                "Cập nhật số điện thoại, email và địa chỉ liên lạc của bạn khi có thay đổi.",
                new Point(24, 335),
                new Size(950, 255),
                Color.White);

            // Truong So dien thoai
            txtSoDienThoai = ThemFieldChinhSua(pnlNhomChinhSua, "Số điện thoại", 65, 20, 430);

            // Truong Email
            txtEmail = ThemFieldChinhSua(pnlNhomChinhSua, "Email liên hệ", 65, 480, 430);

            // Truong Dia chi
            txtDiaChi = ThemFieldChinhSua(pnlNhomChinhSua, "Địa chỉ liên hệ", 150, 20, 890);

            pnlBodyCard.Controls.Add(pnlNhomChinhSua);

            // ----------------------------------------------------
            // Nút Lưu thay đổi (Chuẩn hoá theo ThemeHelper)
            // ----------------------------------------------------
            btnLuuThayDoi = new Button
            {
                Text = "💾  Lưu thay đổi",
                Location = new Point(24, 608),
                Size = new Size(180, ThemeHelper.ChieuCaoButtonChuan)
            };
            ThemeHelper.ApDungButtonChinh(btnLuuThayDoi);
            btnLuuThayDoi.Click += BtnLuuThayDoi_Click;
            pnlBodyCard.Controls.Add(btnLuuThayDoi);

            var lblHuongDanLuu = new Label
            {
                Text = "Dữ liệu sau khi nhấn Lưu thay đổi sẽ được cập nhật trực tiếp vào cơ sở dữ liệu và đồng bộ vào phiên làm việc hiện tại.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(220, 618),
                AutoSize = true
            };
            pnlBodyCard.Controls.Add(lblHuongDanLuu);

            this.Controls.Add(pnlBodyCard);
            this.Controls.Add(pnlHeaderCard);
        }

        private Panel TaoNhomThongTin(string tieuDe, string moTa, Point viTri, Size kichThuoc, Color mauNen)
        {
            var pnl = new Panel
            {
                Location = viTri,
                Size = kichThuoc,
                BackColor = mauNen,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            var lblT = new Label
            {
                Text = tieuDe,
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(16, 12),
                AutoSize = true
            };

            var lblM = new Label
            {
                Text = moTa,
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(18, 36),
                AutoSize = true
            };

            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblM);
            return pnl;
        }

        private void ThemFieldChiXem(Panel parent, string labelText, string valueText, int top, int left, int width)
        {
            var lbl = new Label
            {
                Text = labelText,
                Font = ThemeHelper.FontChuThichDam,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(left, top),
                AutoSize = true
            };

            var txt = new TextBox
            {
                Text = valueText,
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                BackColor = ThemeHelper.MauNenInputReadOnly,
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                Location = new Point(left, top + 22),
                Size = new Size(width, 32)
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
        }

        private TextBox ThemFieldChinhSua(Panel parent, string labelText, int top, int left, int width)
        {
            var lbl = new Label
            {
                Text = labelText,
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(left, top),
                AutoSize = true
            };

            var txt = new TextBox
            {
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(left, top + 24),
                Size = new Size(width, 32)
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            return txt;
        }

        private void NapDuLieuTuSession()
        {
            txtSoDienThoai.Text = Session.SoDienThoai;
            txtEmail.Text = Session.Email;
            txtDiaChi.Text = Session.DiaChi;
        }

        private void BtnLuuThayDoi_Click(object? sender, EventArgs e)
        {
            AnThongBao();

            btnLuuThayDoi.Enabled = false;
            try
            {
                var ketQua = _taiKhoanBLL.CapNhatThongTinLienHe(
                    Session.MaNhanVien,
                    txtSoDienThoai.Text,
                    txtEmail.Text,
                    txtDiaChi.Text);

                if (ketQua.ThanhCong)
                {
                    HienThongBao(ketQua.ThongBao, laLoi: false);
                    NapDuLieuTuSession();
                }
                else
                {
                    HienThongBao(ketQua.ThongBao, laLoi: true);
                }
            }
            finally
            {
                btnLuuThayDoi.Enabled = true;
            }
        }

        private void HienThongBao(string thongBao, bool laLoi)
        {
            pnlThongBao.Visible = true;
            pnlThongBao.BackColor = laLoi ? ColorTranslator.FromHtml("#FEF2F2") : ColorTranslator.FromHtml("#F0FDF4");
            lblThongBao.ForeColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.Text = (laLoi ? "⚠  " : "✓  ") + thongBao;
        }

        private void AnThongBao()
        {
            pnlThongBao.Visible = false;
            lblThongBao.Text = string.Empty;
        }

        private string LayChuCaiDau(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                return "U";
            }
            var parts = hoTen.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                return parts[0][0].ToString().ToUpper();
            }
            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
        }
    }
}

