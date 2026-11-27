using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoaDon.BLL.HeThong;
using QuanLyHoaDon.DTO;
using QuanLyHoaDon.GUI.Controls;
using QuanLyHoaDon.Utilities;

namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    /// <summary>
    /// Form Yêu cầu cấp lại mật khẩu được tinh chỉnh UI/UX chuyên nghiệp:
    /// - Sử dụng RoundedTextBox mềm mại, bo góc 8px, viền mảnh #E2E8F0, khi Focus chuyển sang #2563EB.
    /// - Trường lý do sử dụng RoundedPanel bao bọc TextBox đa dòng đồng bộ viền bo góc và hiệu ứng focus.
    /// - Nhãn phân cấp rõ ràng (Label #172033, Dấu sao #DC2626).
    /// - Bố cục thông thoáng, khoảng cách nhãn - ô nhập hợp lý (6px), khoảng cách giữa các khối (16px).
    /// - Nút bấm RoundedButton bo góc đồng bộ, hiệu ứng hover mượt mà.
    /// - Đầy đủ 5 trường thông tin theo thứ tự quy định và logic xác minh đối chiếu hồ sơ.
    /// </summary>
    public class FormQuenMatKhau : Form
    {
        private readonly YeuCauCapLaiMatKhauBLL _yeuCauBLL;

        private RoundedTextBox txtTenDangNhap = null!;
        private RoundedTextBox txtMaNhanVien = null!;
        private RoundedTextBox txtHoTen = null!;
        private RoundedTextBox txtSoDienThoai = null!;
        private TextBox txtLyDo = null!;
        private RoundedPanel pnlKhungLyDo = null!;

        private Panel pnlThongBao = null!;
        private Label lblThongBao = null!;
        private Label lblTrangThaiYeuCau = null!;
        private RoundedButton btnGuiYeuCau = null!;
        private RoundedButton btnDatLaiMatKhau = null!;
        private RoundedButton btnHuy = null!;

        private YeuCauCapLaiMatKhauDTO? _yeuCauHienTai;

        public FormQuenMatKhau()
        {
            _yeuCauBLL = new YeuCauCapLaiMatKhauBLL();
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Yêu Cầu Cấp Lại Mật Khẩu";
            this.Size = new Size(520, 740);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ThemeHelper.MauNenNoiDung;
            this.Font = ThemeHelper.FontNoiDung;

            // 1. Header Top Panel
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 76,
                BackColor = ThemeHelper.MauNenNoiDung,
                Padding = new Padding(28, 16, 28, 14)
            };
            pnlHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            var lblTieuDe = new Label
            {
                Text = "YÊU CẦU CẤP LẠI MẬT KHẨU",
                Font = ThemeHelper.FontTieuDeCard,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(28, 14),
                AutoSize = true
            };

            var lblPhu = new Label
            {
                Text = "Nhập thông tin xác minh hồ sơ nhân viên để gửi yêu cầu tới Quản lý xét duyệt.",
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,
                Location = new Point(28, 42),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblPhu);
            this.Controls.Add(pnlHeader);

            // 2. Container Form Body (AutoScroll)
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(28, 16, 28, 16),
                BackColor = ThemeHelper.MauNenNoiDung,
                AutoScroll = true
            };

            // Banner thông báo lỗi / thành công mềm mại
            pnlThongBao = new Panel
            {
                Location = new Point(28, 8),
                Size = new Size(446, 38),
                Visible = false
            };
            pnlThongBao.Paint += (s, e) =>
            {
                using var pen = new Pen(pnlThongBao.Tag as Color? ?? ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlThongBao.Width - 1, pnlThongBao.Height - 1);
            };
            lblThongBao = new Label
            {
                Dock = DockStyle.Fill,
                Font = ThemeHelper.FontChuThichDam,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 12, 0)
            };
            pnlThongBao.Controls.Add(lblThongBao);
            pnlBody.Controls.Add(pnlThongBao);

            int top = 50;

            // 1. Tên đăng nhập * (Bắt buộc)
            ThemDongInputBoGoc(pnlBody, "Tên đăng nhập", ref txtTenDangNhap, ref top, placeholder: "Ví dụ: nhanvien01, nv0008...", batBuoc: true);
            txtTenDangNhap.InnerTextBox.Leave += TxtTenDangNhap_Leave;

            // 2. Mã nhân viên * (Bắt buộc)
            ThemDongInputBoGoc(pnlBody, "Mã nhân viên", ref txtMaNhanVien, ref top, placeholder: "Ví dụ: NV0003 hoặc 3", batBuoc: true);

            // 3. Họ và tên * (Bắt buộc)
            ThemDongInputBoGoc(pnlBody, "Họ và tên theo hồ sơ", ref txtHoTen, ref top, placeholder: "Ví dụ: Lê Văn Nam", batBuoc: true);

            // 4. Số điện thoại đã đăng ký * (Bắt buộc)
            ThemDongInputBoGoc(pnlBody, "Số điện thoại đã đăng ký", ref txtSoDienThoai, ref top, placeholder: "10 chữ số đã đăng ký với cửa hàng", batBuoc: true);

            // 5. Lý do yêu cầu cấp lại mật khẩu (Không bắt buộc) - Khung bo góc đồng bộ
            var lblLyDo = new Label
            {
                Text = "Lý do yêu cầu (tùy chọn)",
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                Location = new Point(28, top),
                AutoSize = true
            };

            pnlKhungLyDo = new RoundedPanel
            {
                Location = new Point(28, top + 24),
                Size = new Size(446, 76),
                BorderRadius = 8,
                BorderWidth = 1,
                BorderColor = ColorTranslator.FromHtml("#E2E8F0"),
                BackColor = Color.White,
                Padding = new Padding(10, 8, 10, 8)
            };

            txtLyDo = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                BorderStyle = BorderStyle.None,
                Font = ThemeHelper.FontNoiDung,
                ForeColor = ThemeHelper.MauChuChinh,
                PlaceholderText = "Mô tả ngắn gọn lý do cần cấp lại mật khẩu (quên mật khẩu, đổi ca...)",
                ScrollBars = ScrollBars.Vertical
            };
            txtLyDo.Enter += (s, e) =>
            {
                pnlKhungLyDo.BorderColor = ThemeHelper.MauChinh;
                pnlKhungLyDo.BorderWidth = 2;
                pnlKhungLyDo.Invalidate();
            };
            txtLyDo.Leave += (s, e) =>
            {
                pnlKhungLyDo.BorderColor = ColorTranslator.FromHtml("#E2E8F0");
                pnlKhungLyDo.BorderWidth = 1;
                pnlKhungLyDo.Invalidate();
            };
            pnlKhungLyDo.Controls.Add(txtLyDo);

            pnlBody.Controls.Add(lblLyDo);
            pnlBody.Controls.Add(pnlKhungLyDo);
            top += 108;

            // Nhãn hiển thị trạng thái yêu cầu gần nhất
            lblTrangThaiYeuCau = new Label
            {
                Location = new Point(28, top),
                Size = new Size(446, 40),
                Font = ThemeHelper.FontChuThich,
                ForeColor = ThemeHelper.MauChuPhu,

            };
            pnlBody.Controls.Add(lblTrangThaiYeuCau);
            top += 46;

            this.Controls.Add(pnlBody);

            // 3. Footer Bottom Panel
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 68,
                BackColor = ThemeHelper.MauNenUngDung,
                Padding = new Padding(28, 13, 28, 13)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.MauDuongVien, 1);
                e.Graphics.DrawLine(pen, 0, 0, pnlFooter.Width, 0);
            };

            btnGuiYeuCau = new RoundedButton
            {
                Text = "Gửi yêu cầu",
                Location = new Point(pnlFooter.Width - 140 - 28, 13),
                Size = new Size(140, 42),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                NormalColor = ThemeHelper.MauChinh,
                HoverColor = ThemeHelper.MauChinhHover,
                Font = ThemeHelper.FontButton,
                BorderRadius = 8
            };
            btnGuiYeuCau.Click += BtnGuiYeuCau_Click;

            // Nút Đặt lại mật khẩu mới (khi yêu cầu đã được duyệt)
            btnDatLaiMatKhau = new RoundedButton
            {
                Text = "🔑 Đặt mật khẩu mới",
                Location = new Point(pnlFooter.Width - 315 - 28, 13),
                Size = new Size(165, 42),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                NormalColor = ThemeHelper.MauThanhCong,
                HoverColor = ThemeHelper.MauThanhCongHover,
                Font = ThemeHelper.FontButton,
                BorderRadius = 8,
                Visible = false
            };
            btnDatLaiMatKhau.Click += BtnDatLaiMatKhau_Click;

            btnHuy = new RoundedButton
            {
                Text = "Hủy",
                Location = new Point(28, 13),
                Size = new Size(95, 42),
                NormalColor = ThemeHelper.MauNutPhu,
                HoverColor = ThemeHelper.MauNutPhuHover,
                BorderColor = ThemeHelper.MauVienNutPhu,
                BorderWidth = 1,
                TextColor = ThemeHelper.MauChuChinh,
                Font = ThemeHelper.FontNoiDungDam,
                BorderRadius = 8
            };
            btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            pnlFooter.Controls.Add(btnDatLaiMatKhau);
            pnlFooter.Controls.Add(btnGuiYeuCau);
            pnlFooter.Controls.Add(btnHuy);
            this.Controls.Add(pnlFooter);

            this.AcceptButton = btnGuiYeuCau;
            this.CancelButton = btnHuy;
        }

        private static void ThemDongInputBoGoc(Panel parent, string labelText, ref RoundedTextBox txt, ref int top, string placeholder = "", bool batBuoc = false)
        {
            var pnlLabel = new FlowLayoutPanel
            {
                Location = new Point(28, top),
                Size = new Size(446, 22),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            var lbl = new Label
            {
                Text = labelText,
                Font = ThemeHelper.FontLabel,
                ForeColor = ThemeHelper.MauChuChinh,
                AutoSize = true,
                Margin = new Padding(0, 0, 4, 0)
            };
            pnlLabel.Controls.Add(lbl);

            if (batBuoc)
            {
                var lblStar = new Label
                {
                    Text = "*",
                    Font = ThemeHelper.FontLabel,
                    ForeColor = ThemeHelper.MauLoi,
                    AutoSize = true,
                    Margin = Padding.Empty
                };
                pnlLabel.Controls.Add(lblStar);
            }

            txt = new RoundedTextBox
            {
                Location = new Point(28, top + 24),
                Size = new Size(446, 42),
                Height = 42,
                Font = ThemeHelper.FontNoiDung,
                PlaceholderText = placeholder,
                BorderRadius = 8,
                BorderColor = ColorTranslator.FromHtml("#E2E8F0"),
                FocusBorderColor = ThemeHelper.MauChinh,
                PlaceholderColor = ColorTranslator.FromHtml("#94A3B8")
            };

            parent.Controls.Add(pnlLabel);
            parent.Controls.Add(txt);
            top += 74;
        }

        private void TxtTenDangNhap_Leave(object? sender, EventArgs e)
        {
            KiemTraTrangThaiYeuCau();
        }

        private void KiemTraTrangThaiYeuCau()
        {
            string tenDN = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDN))
            {
                lblTrangThaiYeuCau.Text = "Nhập tên đăng nhập để kiểm tra trạng thái yêu cầu gần nhất...";
                lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauChuPhu;
                btnDatLaiMatKhau.Visible = false;
                btnGuiYeuCau.Visible = true;
                return;
            }

            try
            {
                _yeuCauHienTai = _yeuCauBLL.LayYeuCauMoiNhat(tenDN);
                if (_yeuCauHienTai == null)
                {
                    lblTrangThaiYeuCau.Text = "Tài khoản chưa có yêu cầu cấp lại mật khẩu nào.";
                    lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauChuPhu;
                    btnDatLaiMatKhau.Visible = false;
                    btnGuiYeuCau.Visible = true;
                }
                else
                {
                    switch (_yeuCauHienTai.TrangThai)
                    {
                        case "ChoDuyet":
                            lblTrangThaiYeuCau.Text = $"⏳ Yêu cầu gửi lúc {_yeuCauHienTai.ThoiGianYeuCau:HH:mm dd/MM/yyyy} đang chờ Quản lý phê duyệt.";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauCanhBao;
                            btnDatLaiMatKhau.Visible = false;
                            btnGuiYeuCau.Visible = false; // Chống gửi trùng lặp
                            break;

                        case "DaDuyet":
                            lblTrangThaiYeuCau.Text = $"✅ Yêu cầu đã được Quản lý duyệt lúc {_yeuCauHienTai.ThoiGianDuyet:HH:mm dd/MM/yyyy}! Bạn có thể nhấn 'Đặt mật khẩu mới' bên dưới.";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauThanhCong;
                            btnDatLaiMatKhau.Visible = true;
                            btnGuiYeuCau.Visible = false;
                            break;

                        case "DaTuChoi":
                            lblTrangThaiYeuCau.Text = $"❌ Yêu cầu trước đó bị từ chối lúc {_yeuCauHienTai.ThoiGianDuyet:HH:mm dd/MM/yyyy}.\nLý do: {_yeuCauHienTai.LyDoTuChoi}";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauNguyHiem;
                            btnDatLaiMatKhau.Visible = false;
                            btnGuiYeuCau.Visible = true;
                            break;

                        case "DaHoanTat":
                            lblTrangThaiYeuCau.Text = $"Yêu cầu gần nhất đã hoàn tất lúc {_yeuCauHienTai.ThoiGianHoanTat:HH:mm dd/MM/yyyy}.";
                            lblTrangThaiYeuCau.ForeColor = ThemeHelper.MauChuPhu;
                            btnDatLaiMatKhau.Visible = false;
                            btnGuiYeuCau.Visible = true;
                            break;
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi kết nối tạm thời
            }
        }

        private void BtnGuiYeuCau_Click(object? sender, EventArgs e)
        {
            AnThongBao();

            string tenDN = txtTenDangNhap.Text.Trim();
            string maNV = txtMaNhanVien.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string lyDo = txtLyDo.Text.Trim();

            // 1. Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(tenDN))
            {
                HienThongBao("Vui lòng nhập Tên đăng nhập.", true);
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(maNV))
            {
                HienThongBao("Vui lòng nhập Mã nhân viên.", true);
                txtMaNhanVien.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                HienThongBao("Vui lòng nhập Họ và tên theo hồ sơ.", true);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sdt))
            {
                HienThongBao("Vui lòng nhập Số điện thoại đã đăng ký.", true);
                txtSoDienThoai.Focus();
                return;
            }

            btnGuiYeuCau.Enabled = false;
            btnGuiYeuCau.Text = "Đang kiểm tra...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Thực hiện gửi yêu cầu kèm đối chiếu 4 thông tin hồ sơ
                var kq = _yeuCauBLL.GuiYeuCau(tenDN, maNV, hoTen, sdt, lyDo, batBuocDoiChieu: true);

                if (kq.ThanhCong)
                {
                    HienThongBao(kq.ThongBao, false);
                    MessageBox.Show(kq.ThongBao, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KiemTraTrangThaiYeuCau();
                }
                else
                {
                    HienThongBao(kq.ThongBao, true);
                }
            }
            finally
            {
                btnGuiYeuCau.Enabled = true;
                btnGuiYeuCau.Text = "Gửi yêu cầu";
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnDatLaiMatKhau_Click(object? sender, EventArgs e)
        {
            if (_yeuCauHienTai == null || _yeuCauHienTai.TrangThai != "DaDuyet")
            {
                MessageBox.Show("Chưa có yêu cầu nào được Quản lý duyệt.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FormDatLaiMatKhau(_yeuCauHienTai);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                KiemTraTrangThaiYeuCau();
            }
        }

        private void HienThongBao(string thongBao, bool laLoi)
        {
            pnlThongBao.Visible = true;
            pnlThongBao.BackColor = laLoi ? ColorTranslator.FromHtml("#FEF2F2") : ColorTranslator.FromHtml("#F0FDF4");
            pnlThongBao.Tag = laLoi ? ColorTranslator.FromHtml("#FCA5A5") : ColorTranslator.FromHtml("#86EFAC");
            lblThongBao.ForeColor = laLoi ? ThemeHelper.MauLoi : ThemeHelper.MauThanhCong;
            lblThongBao.Text = (laLoi ? "⚠ " : "✓ ") + thongBao;
            pnlThongBao.Invalidate();
        }

        private void AnThongBao()
        {
            pnlThongBao.Visible = false;
            lblThongBao.Text = string.Empty;
        }
    }
}
