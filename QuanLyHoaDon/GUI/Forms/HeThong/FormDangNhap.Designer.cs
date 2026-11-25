namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    partial class FormDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.Panel pnlBranding;
        private System.Windows.Forms.Label lblBrandLogo;
        private System.Windows.Forms.Label lblBrandTieuDe;
        private System.Windows.Forms.Label lblBrandMoTa;
        private System.Windows.Forms.Label lblBrandDiemNoiBat1;
        private System.Windows.Forms.Label lblBrandDiemNoiBat2;
        private System.Windows.Forms.Label lblBrandDiemNoiBat3;
        private System.Windows.Forms.Label lblBrandPhienBan;

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblTieuDeForm;
        private System.Windows.Forms.Label lblMoTaForm;
        private System.Windows.Forms.Panel pnlThongBaoLoi;
        private System.Windows.Forms.Label lblThongBaoLoi;

        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.Panel pnlTenDangNhapBorder;
        private System.Windows.Forms.TextBox txtTenDangNhap;

        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Panel pnlMatKhauBorder;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnHienMatKhau;

        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label lblGoiYDemo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlBranding = new System.Windows.Forms.Panel();
            this.lblBrandLogo = new System.Windows.Forms.Label();
            this.lblBrandTieuDe = new System.Windows.Forms.Label();
            this.lblBrandMoTa = new System.Windows.Forms.Label();
            this.lblBrandDiemNoiBat1 = new System.Windows.Forms.Label();
            this.lblBrandDiemNoiBat2 = new System.Windows.Forms.Label();
            this.lblBrandDiemNoiBat3 = new System.Windows.Forms.Label();
            this.lblBrandPhienBan = new System.Windows.Forms.Label();

            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblTieuDeForm = new System.Windows.Forms.Label();
            this.lblMoTaForm = new System.Windows.Forms.Label();
            this.pnlThongBaoLoi = new System.Windows.Forms.Panel();
            this.lblThongBaoLoi = new System.Windows.Forms.Label();

            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.pnlTenDangNhapBorder = new System.Windows.Forms.Panel();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();

            this.lblMatKhau = new System.Windows.Forms.Label();
            this.pnlMatKhauBorder = new System.Windows.Forms.Panel();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnHienMatKhau = new System.Windows.Forms.Button();

            this.btnDangNhap = new System.Windows.Forms.Button();
            this.lblGoiYDemo = new System.Windows.Forms.Label();

            this.pnlBranding.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.pnlThongBaoLoi.SuspendLayout();
            this.pnlTenDangNhapBorder.SuspendLayout();
            this.pnlMatKhauBorder.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // pnlBranding (Ben trai - Modern Dark Blue)
            // ==========================================
            this.pnlBranding.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBranding.Width = 360;
            this.pnlBranding.BackColor = System.Drawing.Color.FromArgb(23, 43, 77);
            this.pnlBranding.Controls.Add(this.lblBrandLogo);
            this.pnlBranding.Controls.Add(this.lblBrandTieuDe);
            this.pnlBranding.Controls.Add(this.lblBrandMoTa);
            this.pnlBranding.Controls.Add(this.lblBrandDiemNoiBat1);
            this.pnlBranding.Controls.Add(this.lblBrandDiemNoiBat2);
            this.pnlBranding.Controls.Add(this.lblBrandDiemNoiBat3);
            this.pnlBranding.Controls.Add(this.lblBrandPhienBan);

            // lblBrandLogo
            this.lblBrandLogo.AutoSize = false;
            this.lblBrandLogo.Size = new System.Drawing.Size(60, 60);
            this.lblBrandLogo.Location = new System.Drawing.Point(40, 50);
            this.lblBrandLogo.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblBrandLogo.ForeColor = System.Drawing.Color.White;
            this.lblBrandLogo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBrandLogo.Text = "QL";
            this.lblBrandLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblBrandTieuDe
            this.lblBrandTieuDe.AutoSize = true;
            this.lblBrandTieuDe.Location = new System.Drawing.Point(38, 125);
            this.lblBrandTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblBrandTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblBrandTieuDe.Text = "QUẢN LÝ HÓA ĐƠN";

            // lblBrandMoTa
            this.lblBrandMoTa.Size = new System.Drawing.Size(280, 50);
            this.lblBrandMoTa.Location = new System.Drawing.Point(40, 168);
            this.lblBrandMoTa.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.lblBrandMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBrandMoTa.Text = "Hệ thống quản lý khách hàng, sản phẩm và hóa đơn bán hàng chuyên nghiệp.";

            // Bullet points
            this.lblBrandDiemNoiBat1.AutoSize = true;
            this.lblBrandDiemNoiBat1.Location = new System.Drawing.Point(40, 245);
            this.lblBrandDiemNoiBat1.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.lblBrandDiemNoiBat1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBrandDiemNoiBat1.Text = "✓  Phân quyền Quản lý & Nhân viên rõ ràng";

            this.lblBrandDiemNoiBat2.AutoSize = true;
            this.lblBrandDiemNoiBat2.Location = new System.Drawing.Point(40, 280);
            this.lblBrandDiemNoiBat2.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.lblBrandDiemNoiBat2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBrandDiemNoiBat2.Text = "✓  Quản lý khách hàng, kho và lập hóa đơn";

            this.lblBrandDiemNoiBat3.AutoSize = true;
            this.lblBrandDiemNoiBat3.Location = new System.Drawing.Point(40, 315);
            this.lblBrandDiemNoiBat3.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.lblBrandDiemNoiBat3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBrandDiemNoiBat3.Text = "✓  Báo cáo doanh thu & kiểm soát an toàn";

            // lblBrandPhienBan
            this.lblBrandPhienBan.AutoSize = true;
            this.lblBrandPhienBan.Location = new System.Drawing.Point(40, 475);
            this.lblBrandPhienBan.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblBrandPhienBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBrandPhienBan.Text = "Phiên bản 1.0.0 • .NET 10 LTS";

            // ==========================================
            // pnlForm (Ben phai - White Form Card)
            // ==========================================
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Padding = new System.Windows.Forms.Padding(48, 40, 48, 40);
            this.pnlForm.Controls.Add(this.lblTieuDeForm);
            this.pnlForm.Controls.Add(this.lblMoTaForm);
            this.pnlForm.Controls.Add(this.pnlThongBaoLoi);
            this.pnlForm.Controls.Add(this.lblTenDangNhap);
            this.pnlForm.Controls.Add(this.pnlTenDangNhapBorder);
            this.pnlForm.Controls.Add(this.lblMatKhau);
            this.pnlForm.Controls.Add(this.pnlMatKhauBorder);
            this.pnlForm.Controls.Add(this.btnDangNhap);
            this.pnlForm.Controls.Add(this.lblGoiYDemo);

            // lblTieuDeForm
            this.lblTieuDeForm.AutoSize = true;
            this.lblTieuDeForm.Location = new System.Drawing.Point(48, 45);
            this.lblTieuDeForm.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTieuDeForm.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeForm.Text = "Đăng nhập hệ thống";

            // lblMoTaForm
            this.lblMoTaForm.AutoSize = true;
            this.lblMoTaForm.Location = new System.Drawing.Point(50, 82);
            this.lblMoTaForm.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblMoTaForm.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMoTaForm.Text = "Nhập thông tin tài khoản được cấp để tiếp tục làm việc";

            // pnlThongBaoLoi
            this.pnlThongBaoLoi.Location = new System.Drawing.Point(50, 115);
            this.pnlThongBaoLoi.Size = new System.Drawing.Size(420, 36);
            this.pnlThongBaoLoi.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.pnlThongBaoLoi.Visible = false;
            this.pnlThongBaoLoi.Controls.Add(this.lblThongBaoLoi);

            // lblThongBaoLoi
            this.lblThongBaoLoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblThongBaoLoi.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblThongBaoLoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblThongBaoLoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThongBaoLoi.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);

            // lblTenDangNhap
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new System.Drawing.Point(48, 160);
            this.lblTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenDangNhap.Text = "Tên đăng nhập";

            // pnlTenDangNhapBorder
            this.pnlTenDangNhapBorder.Location = new System.Drawing.Point(50, 185);
            this.pnlTenDangNhapBorder.Size = new System.Drawing.Size(420, 42);
            this.pnlTenDangNhapBorder.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlTenDangNhapBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTenDangNhapBorder.Controls.Add(this.txtTenDangNhap);

            // txtTenDangNhap
            this.txtTenDangNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTenDangNhap.Location = new System.Drawing.Point(12, 11);
            this.txtTenDangNhap.Size = new System.Drawing.Size(395, 20);
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);

            // lblMatKhau
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(48, 245);
            this.lblMatKhau.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMatKhau.Text = "Mật khẩu";

            // pnlMatKhauBorder
            this.pnlMatKhauBorder.Location = new System.Drawing.Point(50, 270);
            this.pnlMatKhauBorder.Size = new System.Drawing.Size(420, 42);
            this.pnlMatKhauBorder.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlMatKhauBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMatKhauBorder.Controls.Add(this.txtMatKhau);
            this.pnlMatKhauBorder.Controls.Add(this.btnHienMatKhau);

            // txtMatKhau
            this.txtMatKhau.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMatKhau.Location = new System.Drawing.Point(12, 11);
            this.txtMatKhau.Size = new System.Drawing.Size(335, 20);
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtMatKhau.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtMatKhau.UseSystemPasswordChar = true;

            // btnHienMatKhau
            this.btnHienMatKhau.Location = new System.Drawing.Point(355, 4);
            this.btnHienMatKhau.Size = new System.Drawing.Size(58, 32);
            this.btnHienMatKhau.Text = "Hiện";
            this.btnHienMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHienMatKhau.FlatAppearance.BorderSize = 0;
            this.btnHienMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.btnHienMatKhau.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnHienMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.btnHienMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHienMatKhau.Click += BtnHienMatKhau_Click;

            // btnDangNhap
            this.btnDangNhap.Location = new System.Drawing.Point(50, 345);
            this.btnDangNhap.Size = new System.Drawing.Size(420, 46);
            this.btnDangNhap.Text = "ĐĂNG NHẬP";
            this.btnDangNhap.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhap.FlatAppearance.BorderSize = 0;
            this.btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangNhap.Click += BtnDangNhap_Click;

            // lblGoiYDemo
            this.lblGoiYDemo.Size = new System.Drawing.Size(420, 45);
            this.lblGoiYDemo.Location = new System.Drawing.Point(50, 410);
            this.lblGoiYDemo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblGoiYDemo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblGoiYDemo.Text = "💡 Tài khoản demo: quanly01 (Quản lý) | nhanvien01 (Nhân viên)\nMật khẩu dùng chung: Demo@123";
            this.lblGoiYDemo.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            // ==========================================
            // FormDangNhap
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 520);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlBranding);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập - Hệ Thống Quản Lý Hóa Đơn";

            this.pnlBranding.ResumeLayout(false);
            this.pnlBranding.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlThongBaoLoi.ResumeLayout(false);
            this.pnlTenDangNhapBorder.ResumeLayout(false);
            this.pnlTenDangNhapBorder.PerformLayout();
            this.pnlMatKhauBorder.ResumeLayout(false);
            this.pnlMatKhauBorder.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

