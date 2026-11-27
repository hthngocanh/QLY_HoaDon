namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    partial class FormDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlCard = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            pnlLogo = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            lblLogoIcon = new Label();
            lblTieuDe = new Label();
            lblPhuDe = new Label();
            lblTenDangNhap = new Label();
            pnlTenDangNhap = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            txtTenDangNhap = new TextBox();
            lblMatKhau = new Label();
            pnlMatKhau = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            txtMatKhau = new TextBox();
            chkHienMatKhau = new CheckBox();
            lblQuenMatKhau = new Label();
            btnDangNhap = new QuanLyHoaDon.GUI.Controls.RoundedButton();
            pnlThongBaoLoi = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            lblThongBaoLoi = new Label();
            pnlCard.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlTenDangNhap.SuspendLayout();
            pnlMatKhau.SuspendLayout();
            pnlThongBaoLoi.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.BorderColor = Color.FromArgb(227, 234, 244);
            pnlCard.BorderRadius = 16;
            pnlCard.BorderWidth = 1;
            pnlCard.Controls.Add(pnlLogo);
            pnlCard.Controls.Add(lblTieuDe);
            pnlCard.Controls.Add(lblPhuDe);
            pnlCard.Controls.Add(lblTenDangNhap);
            pnlCard.Controls.Add(pnlTenDangNhap);
            pnlCard.Controls.Add(lblMatKhau);
            pnlCard.Controls.Add(pnlMatKhau);
            pnlCard.Controls.Add(chkHienMatKhau);
            pnlCard.Controls.Add(lblQuenMatKhau);
            pnlCard.Controls.Add(btnDangNhap);
            pnlCard.Controls.Add(pnlThongBaoLoi);
            pnlCard.Location = new Point(50, 38);
            pnlCard.Margin = new Padding(4, 4, 4, 4);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(500, 675);
            pnlCard.TabIndex = 0;
            // 
            // pnlLogo
            // 
            pnlLogo.BackColor = Color.FromArgb(232, 240, 254);
            pnlLogo.BorderColor = Color.FromArgb(214, 226, 251);
            pnlLogo.BorderRadius = 14;
            pnlLogo.BorderWidth = 1;
            pnlLogo.Controls.Add(lblLogoIcon);
            pnlLogo.Location = new Point(215, 35);
            pnlLogo.Margin = new Padding(4, 4, 4, 4);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(70, 70);
            pnlLogo.TabIndex = 0;
            // 
            // lblLogoIcon
            // 
            lblLogoIcon.BackColor = Color.Transparent;
            lblLogoIcon.Dock = DockStyle.Fill;
            lblLogoIcon.Font = new Font("Segoe UI Emoji", 18F);
            lblLogoIcon.Location = new Point(0, 0);
            lblLogoIcon.Margin = new Padding(4, 0, 4, 0);
            lblLogoIcon.Name = "lblLogoIcon";
            lblLogoIcon.Size = new Size(70, 70);
            lblLogoIcon.TabIndex = 0;
            lblLogoIcon.Text = "\U0001f9fe";
            lblLogoIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTieuDe
            // 
            lblTieuDe.BackColor = Color.Transparent;
            lblTieuDe.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(15, 42, 67);
            lblTieuDe.Location = new Point(0, 122);
            lblTieuDe.Margin = new Padding(4, 0, 4, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(500, 48);
            lblTieuDe.TabIndex = 1;
            lblTieuDe.Text = "ĐĂNG NHẬP HỆ THỐNG";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPhuDe
            // 
            lblPhuDe.BackColor = Color.Transparent;
            lblPhuDe.Font = new Font("Segoe UI", 10F);
            lblPhuDe.ForeColor = Color.FromArgb(100, 116, 139);
            lblPhuDe.Location = new Point(0, 172);
            lblPhuDe.Margin = new Padding(4, 0, 4, 0);
            lblPhuDe.Name = "lblPhuDe";
            lblPhuDe.Size = new Size(500, 32);
            lblPhuDe.TabIndex = 2;
            lblPhuDe.Text = "Phần mềm Quản lý Hóa đơn Bán hàng";
            lblPhuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.BackColor = Color.Transparent;
            lblTenDangNhap.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTenDangNhap.ForeColor = Color.FromArgb(15, 42, 67);
            lblTenDangNhap.Location = new Point(40, 230);
            lblTenDangNhap.Margin = new Padding(4, 0, 4, 0);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(143, 25);
            lblTenDangNhap.TabIndex = 3;
            lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // pnlTenDangNhap
            // 
            pnlTenDangNhap.BackColor = Color.White;
            pnlTenDangNhap.BorderColor = Color.FromArgb(203, 213, 225);
            pnlTenDangNhap.BorderRadius = 10;
            pnlTenDangNhap.BorderWidth = 1;
            pnlTenDangNhap.Controls.Add(txtTenDangNhap);
            pnlTenDangNhap.Location = new Point(40, 265);
            pnlTenDangNhap.Margin = new Padding(4, 4, 4, 4);
            pnlTenDangNhap.Name = "pnlTenDangNhap";
            pnlTenDangNhap.Size = new Size(420, 55);
            pnlTenDangNhap.TabIndex = 0;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.BackColor = Color.White;
            txtTenDangNhap.BorderStyle = BorderStyle.None;
            txtTenDangNhap.Font = new Font("Segoe UI", 11F);
            txtTenDangNhap.ForeColor = Color.FromArgb(15, 42, 67);
            txtTenDangNhap.Location = new Point(18, 14);
            txtTenDangNhap.Margin = new Padding(4, 4, 4, 4);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập";
            txtTenDangNhap.Size = new Size(385, 25);
            txtTenDangNhap.TabIndex = 0;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.BackColor = Color.Transparent;
            lblMatKhau.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblMatKhau.ForeColor = Color.FromArgb(15, 42, 67);
            lblMatKhau.Location = new Point(40, 342);
            lblMatKhau.Margin = new Padding(4, 0, 4, 0);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(98, 25);
            lblMatKhau.TabIndex = 5;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // pnlMatKhau
            // 
            pnlMatKhau.BackColor = Color.White;
            pnlMatKhau.BorderColor = Color.FromArgb(203, 213, 225);
            pnlMatKhau.BorderRadius = 10;
            pnlMatKhau.BorderWidth = 1;
            pnlMatKhau.Controls.Add(txtMatKhau);
            pnlMatKhau.Location = new Point(40, 378);
            pnlMatKhau.Margin = new Padding(4, 4, 4, 4);
            pnlMatKhau.Name = "pnlMatKhau";
            pnlMatKhau.Size = new Size(420, 55);
            pnlMatKhau.TabIndex = 1;
            // 
            // txtMatKhau
            // 
            txtMatKhau.BackColor = Color.White;
            txtMatKhau.BorderStyle = BorderStyle.None;
            txtMatKhau.Font = new Font("Segoe UI", 11F);
            txtMatKhau.ForeColor = Color.FromArgb(15, 42, 67);
            txtMatKhau.Location = new Point(18, 14);
            txtMatKhau.Margin = new Padding(4, 4, 4, 4);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PlaceholderText = "Nhập mật khẩu";
            txtMatKhau.Size = new Size(385, 25);
            txtMatKhau.TabIndex = 0;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.BackColor = Color.Transparent;
            chkHienMatKhau.Cursor = Cursors.Hand;
            chkHienMatKhau.Font = new Font("Segoe UI", 10F);
            chkHienMatKhau.ForeColor = Color.FromArgb(51, 65, 85);
            chkHienMatKhau.Location = new Point(40, 452);
            chkHienMatKhau.Margin = new Padding(4, 4, 4, 4);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(169, 27);
            chkHienMatKhau.TabIndex = 2;
            chkHienMatKhau.Text = "Hiển thị mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = false;
            // 
            // lblQuenMatKhau
            // 
            lblQuenMatKhau.BackColor = Color.Transparent;
            lblQuenMatKhau.Cursor = Cursors.Hand;
            lblQuenMatKhau.Font = new Font("Segoe UI", 10F);
            lblQuenMatKhau.ForeColor = Color.FromArgb(37, 99, 235);
            lblQuenMatKhau.Location = new Point(260, 451);
            lblQuenMatKhau.Margin = new Padding(4, 0, 4, 0);
            lblQuenMatKhau.Name = "lblQuenMatKhau";
            lblQuenMatKhau.Size = new Size(200, 30);
            lblQuenMatKhau.TabIndex = 3;
            lblQuenMatKhau.Text = "Quên mật khẩu?";
            lblQuenMatKhau.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(37, 99, 235);
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(40, 505);
            btnDangNhap.Margin = new Padding(4, 4, 4, 4);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(420, 62);
            btnDangNhap.TabIndex = 4;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click_1;
            // 
            // pnlThongBaoLoi
            // 
            pnlThongBaoLoi.BackColor = Color.FromArgb(254, 242, 242);
            pnlThongBaoLoi.BorderColor = Color.FromArgb(254, 202, 202);
            pnlThongBaoLoi.BorderRadius = 10;
            pnlThongBaoLoi.BorderWidth = 1;
            pnlThongBaoLoi.Controls.Add(lblThongBaoLoi);
            pnlThongBaoLoi.Location = new Point(40, 588);
            pnlThongBaoLoi.Margin = new Padding(4, 4, 4, 4);
            pnlThongBaoLoi.Name = "pnlThongBaoLoi";
            pnlThongBaoLoi.Size = new Size(420, 60);
            pnlThongBaoLoi.TabIndex = 5;
            pnlThongBaoLoi.Visible = false;
            // 
            // lblThongBaoLoi
            // 
            lblThongBaoLoi.BackColor = Color.Transparent;
            lblThongBaoLoi.Dock = DockStyle.Fill;
            lblThongBaoLoi.Font = new Font("Segoe UI", 9.5F);
            lblThongBaoLoi.ForeColor = Color.FromArgb(185, 28, 28);
            lblThongBaoLoi.Location = new Point(0, 0);
            lblThongBaoLoi.Margin = new Padding(4, 0, 4, 0);
            lblThongBaoLoi.Name = "lblThongBaoLoi";
            lblThongBaoLoi.Padding = new Padding(15, 0, 10, 0);
            lblThongBaoLoi.Size = new Size(420, 60);
            lblThongBaoLoi.TabIndex = 0;
            lblThongBaoLoi.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormDangNhap
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(245, 248, 253);
            ClientSize = new Size(600, 750);
            Controls.Add(pnlCard);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "FormDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống - Quản lý Hóa đơn Bán hàng";
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            pnlLogo.ResumeLayout(false);
            pnlTenDangNhap.ResumeLayout(false);
            pnlTenDangNhap.PerformLayout();
            pnlMatKhau.ResumeLayout(false);
            pnlMatKhau.PerformLayout();
            pnlThongBaoLoi.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private QuanLyHoaDon.GUI.Controls.RoundedPanel pnlCard;
        private QuanLyHoaDon.GUI.Controls.RoundedPanel pnlLogo;
        private System.Windows.Forms.Label lblLogoIcon;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblPhuDe;
        private System.Windows.Forms.Label lblTenDangNhap;
        private QuanLyHoaDon.GUI.Controls.RoundedPanel pnlTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private QuanLyHoaDon.GUI.Controls.RoundedPanel pnlMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.Label lblQuenMatKhau;
        private QuanLyHoaDon.GUI.Controls.RoundedButton btnDangNhap;
        private QuanLyHoaDon.GUI.Controls.RoundedPanel pnlThongBaoLoi;
        private System.Windows.Forms.Label lblThongBaoLoi;
    }
}