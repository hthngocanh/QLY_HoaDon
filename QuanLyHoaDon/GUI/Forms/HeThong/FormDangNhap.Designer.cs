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
            lblLogoIcon = new System.Windows.Forms.Label();
            lblTieuDe = new System.Windows.Forms.Label();
            lblPhuDe = new System.Windows.Forms.Label();
            lblTenDangNhap = new System.Windows.Forms.Label();
            pnlTenDangNhap = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            txtTenDangNhap = new System.Windows.Forms.TextBox();
            lblMatKhau = new System.Windows.Forms.Label();
            pnlMatKhau = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            txtMatKhau = new System.Windows.Forms.TextBox();
            chkHienMatKhau = new System.Windows.Forms.CheckBox();
            lblQuenMatKhau = new System.Windows.Forms.Label();
            btnDangNhap = new QuanLyHoaDon.GUI.Controls.RoundedButton();
            pnlThongBaoLoi = new QuanLyHoaDon.GUI.Controls.RoundedPanel();
            lblThongBaoLoi = new System.Windows.Forms.Label();
            pnlCard.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlTenDangNhap.SuspendLayout();
            pnlMatKhau.SuspendLayout();
            pnlThongBaoLoi.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = System.Drawing.Color.White;
            pnlCard.BorderColor = System.Drawing.Color.FromArgb(227, 234, 244);
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
            pnlCard.Location = new System.Drawing.Point(40, 30);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new System.Drawing.Size(400, 540);
            pnlCard.TabIndex = 0;
            // 
            // pnlLogo
            // 
            pnlLogo.BackColor = System.Drawing.Color.FromArgb(232, 240, 254);
            pnlLogo.BorderColor = System.Drawing.Color.FromArgb(214, 226, 251);
            pnlLogo.BorderRadius = 14;
            pnlLogo.BorderWidth = 1;
            pnlLogo.Controls.Add(lblLogoIcon);
            pnlLogo.Location = new System.Drawing.Point(172, 28);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new System.Drawing.Size(56, 56);
            pnlLogo.TabIndex = 0;
            // 
            // lblLogoIcon
            // 
            lblLogoIcon.BackColor = System.Drawing.Color.Transparent;
            lblLogoIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            lblLogoIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblLogoIcon.Name = "lblLogoIcon";
            lblLogoIcon.TabIndex = 0;
            lblLogoIcon.Text = "🧾";
            lblLogoIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTieuDe
            // 
            lblTieuDe.BackColor = System.Drawing.Color.Transparent;
            lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(15, 42, 67);
            lblTieuDe.Location = new System.Drawing.Point(0, 98);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new System.Drawing.Size(400, 38);
            lblTieuDe.TabIndex = 1;
            lblTieuDe.Text = "ĐĂNG NHẬP HỆ THỐNG";
            lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPhuDe
            // 
            lblPhuDe.BackColor = System.Drawing.Color.Transparent;
            lblPhuDe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblPhuDe.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPhuDe.Location = new System.Drawing.Point(0, 138);
            lblPhuDe.Name = "lblPhuDe";
            lblPhuDe.Size = new System.Drawing.Size(400, 26);
            lblPhuDe.TabIndex = 2;
            lblPhuDe.Text = "Phần mềm Quản lý Hóa đơn Bán hàng";
            lblPhuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.BackColor = System.Drawing.Color.Transparent;
            lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(15, 42, 67);
            lblTenDangNhap.Location = new System.Drawing.Point(32, 184);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.TabIndex = 3;
            lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // pnlTenDangNhap
            // 
            pnlTenDangNhap.BackColor = System.Drawing.Color.White;
            pnlTenDangNhap.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            pnlTenDangNhap.BorderRadius = 10;
            pnlTenDangNhap.BorderWidth = 1;
            pnlTenDangNhap.Controls.Add(txtTenDangNhap);
            pnlTenDangNhap.Location = new System.Drawing.Point(32, 212);
            pnlTenDangNhap.Name = "pnlTenDangNhap";
            pnlTenDangNhap.Size = new System.Drawing.Size(336, 44);
            pnlTenDangNhap.TabIndex = 0;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.BackColor = System.Drawing.Color.White;
            txtTenDangNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(15, 42, 67);
            txtTenDangNhap.Location = new System.Drawing.Point(14, 11);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập";
            txtTenDangNhap.Size = new System.Drawing.Size(308, 20);
            txtTenDangNhap.TabIndex = 0;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.BackColor = System.Drawing.Color.Transparent;
            lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblMatKhau.ForeColor = System.Drawing.Color.FromArgb(15, 42, 67);
            lblMatKhau.Location = new System.Drawing.Point(32, 274);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.TabIndex = 5;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // pnlMatKhau
            // 
            pnlMatKhau.BackColor = System.Drawing.Color.White;
            pnlMatKhau.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            pnlMatKhau.BorderRadius = 10;
            pnlMatKhau.BorderWidth = 1;
            pnlMatKhau.Controls.Add(txtMatKhau);
            pnlMatKhau.Location = new System.Drawing.Point(32, 302);
            pnlMatKhau.Name = "pnlMatKhau";
            pnlMatKhau.Size = new System.Drawing.Size(336, 44);
            pnlMatKhau.TabIndex = 1;
            // 
            // txtMatKhau
            // 
            txtMatKhau.BackColor = System.Drawing.Color.White;
            txtMatKhau.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtMatKhau.ForeColor = System.Drawing.Color.FromArgb(15, 42, 67);
            txtMatKhau.Location = new System.Drawing.Point(14, 11);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PlaceholderText = "Nhập mật khẩu";
            txtMatKhau.Size = new System.Drawing.Size(308, 20);
            txtMatKhau.TabIndex = 0;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.BackColor = System.Drawing.Color.Transparent;
            chkHienMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            chkHienMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            chkHienMatKhau.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            chkHienMatKhau.Location = new System.Drawing.Point(32, 362);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.TabIndex = 2;
            chkHienMatKhau.Text = "Hiển thị mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = false;
            // 
            // lblQuenMatKhau
            // 
            lblQuenMatKhau.BackColor = System.Drawing.Color.Transparent;
            lblQuenMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            lblQuenMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblQuenMatKhau.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblQuenMatKhau.Location = new System.Drawing.Point(208, 361);
            lblQuenMatKhau.Name = "lblQuenMatKhau";
            lblQuenMatKhau.Size = new System.Drawing.Size(160, 24);
            lblQuenMatKhau.TabIndex = 3;
            lblQuenMatKhau.Text = "Quên mật khẩu?";
            lblQuenMatKhau.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDangNhap.ForeColor = System.Drawing.Color.White;
            btnDangNhap.Location = new System.Drawing.Point(32, 404);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new System.Drawing.Size(336, 50);
            btnDangNhap.TabIndex = 4;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            // 
            // pnlThongBaoLoi
            // 
            pnlThongBaoLoi.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            pnlThongBaoLoi.BorderColor = System.Drawing.Color.FromArgb(254, 202, 202);
            pnlThongBaoLoi.BorderRadius = 10;
            pnlThongBaoLoi.BorderWidth = 1;
            pnlThongBaoLoi.Controls.Add(lblThongBaoLoi);
            pnlThongBaoLoi.Location = new System.Drawing.Point(32, 470);
            pnlThongBaoLoi.Name = "pnlThongBaoLoi";
            pnlThongBaoLoi.Size = new System.Drawing.Size(336, 48);
            pnlThongBaoLoi.TabIndex = 5;
            pnlThongBaoLoi.Visible = false;
            // 
            // lblThongBaoLoi
            // 
            lblThongBaoLoi.BackColor = System.Drawing.Color.Transparent;
            lblThongBaoLoi.Dock = System.Windows.Forms.DockStyle.Fill;
            lblThongBaoLoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblThongBaoLoi.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblThongBaoLoi.Name = "lblThongBaoLoi";
            lblThongBaoLoi.Padding = new System.Windows.Forms.Padding(12, 0, 8, 0);
            lblThongBaoLoi.TabIndex = 0;
            lblThongBaoLoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormDangNhap
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.FromArgb(245, 248, 253);
            ClientSize = new System.Drawing.Size(480, 600);
            Controls.Add(pnlCard);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormDangNhap";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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