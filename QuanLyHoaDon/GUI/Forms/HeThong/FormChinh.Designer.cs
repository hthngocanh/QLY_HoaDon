namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    partial class FormChinh
    {
        private System.ComponentModel.IContainer components = null;

        // Khung điều hướng bên trái (Sidebar)
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.FlowLayoutPanel flpSidebarMenu;
        private System.Windows.Forms.Panel pnlTaiKhoanSidebar;
        private System.Windows.Forms.Panel pnlAvatarSidebar;
        private System.Windows.Forms.Label lblAvatarSidebar;
        private System.Windows.Forms.Label lblTenNguoiDungSidebar;
        private System.Windows.Forms.Label lblVaiTroSidebar;
        private System.Windows.Forms.Label lblChevronSidebar;
        private System.Windows.Forms.Panel pnlSidebarBrand;
        private System.Windows.Forms.Label lblBrandLogo;
        private System.Windows.Forms.Label lblBrandTieuDe;
        private System.Windows.Forms.Label lblBrandPhanHe;

        // Context Menu khi bấm vào khu vực người dùng
        private System.Windows.Forms.ContextMenuStrip cmsTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem tsmiThongTinCaNhan;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoiMatKhau;
        private System.Windows.Forms.ToolStripSeparator tsmiPhanCach;
        private System.Windows.Forms.ToolStripMenuItem tsmiDangXuat;

        // Khung tiêu đề trên (Topbar)
        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTopbarTieuDe;
        private System.Windows.Forms.Label lblTopbarBreadcrumb;
        private System.Windows.Forms.Panel pnlNguoiDung;
        private System.Windows.Forms.Panel pnlAvatarCircle;
        private System.Windows.Forms.Label lblAvatarKyHieu;
        private System.Windows.Forms.Label lblTenNguoiDung;
        private System.Windows.Forms.Label lblVaiTroBadge;

        // Vùng làm việc chính
        private System.Windows.Forms.Panel pnlNoiDungChinh;

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
            this.components = new System.ComponentModel.Container();

            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.flpSidebarMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTaiKhoanSidebar = new System.Windows.Forms.Panel();
            this.pnlAvatarSidebar = new System.Windows.Forms.Panel();
            this.lblAvatarSidebar = new System.Windows.Forms.Label();
            this.lblTenNguoiDungSidebar = new System.Windows.Forms.Label();
            this.lblVaiTroSidebar = new System.Windows.Forms.Label();
            this.lblChevronSidebar = new System.Windows.Forms.Label();
            this.pnlSidebarBrand = new System.Windows.Forms.Panel();
            this.lblBrandLogo = new System.Windows.Forms.Label();
            this.lblBrandTieuDe = new System.Windows.Forms.Label();
            this.lblBrandPhanHe = new System.Windows.Forms.Label();

            this.cmsTaiKhoan = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiThongTinCaNhan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPhanCach = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDangXuat = new System.Windows.Forms.ToolStripMenuItem();

            this.pnlTopbar = new System.Windows.Forms.Panel();
            this.lblTopbarTieuDe = new System.Windows.Forms.Label();
            this.lblTopbarBreadcrumb = new System.Windows.Forms.Label();
            this.pnlNguoiDung = new System.Windows.Forms.Panel();
            this.pnlAvatarCircle = new System.Windows.Forms.Panel();
            this.lblAvatarKyHieu = new System.Windows.Forms.Label();
            this.lblTenNguoiDung = new System.Windows.Forms.Label();
            this.lblVaiTroBadge = new System.Windows.Forms.Label();

            this.pnlNoiDungChinh = new System.Windows.Forms.Panel();

            this.pnlSidebar.SuspendLayout();
            this.pnlTaiKhoanSidebar.SuspendLayout();
            this.pnlAvatarSidebar.SuspendLayout();
            this.pnlSidebarBrand.SuspendLayout();
            this.cmsTaiKhoan.SuspendLayout();
            this.pnlTopbar.SuspendLayout();
            this.pnlNguoiDung.SuspendLayout();
            this.pnlAvatarCircle.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // pnlSidebar (Bên trái - #1E3A5F)
            // ==========================================
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width = 270;
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.pnlSidebar.Controls.Add(this.flpSidebarMenu);
            this.pnlSidebar.Controls.Add(this.pnlTaiKhoanSidebar);
            this.pnlSidebar.Controls.Add(this.pnlSidebarBrand);

            // pnlSidebarBrand (Top)
            this.pnlSidebarBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSidebarBrand.Height = 90;
            this.pnlSidebarBrand.BackColor = System.Drawing.Color.FromArgb(21, 44, 72);
            this.pnlSidebarBrand.Controls.Add(this.lblBrandLogo);
            this.pnlSidebarBrand.Controls.Add(this.lblBrandTieuDe);
            this.pnlSidebarBrand.Controls.Add(this.lblBrandPhanHe);

            // lblBrandLogo
            this.lblBrandLogo.AutoSize = false;
            this.lblBrandLogo.Size = new System.Drawing.Size(46, 46);
            this.lblBrandLogo.Location = new System.Drawing.Point(16, 22);
            this.lblBrandLogo.BackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.lblBrandLogo.ForeColor = System.Drawing.Color.White;
            this.lblBrandLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBrandLogo.Text = "QL";
            this.lblBrandLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblBrandTieuDe
            this.lblBrandTieuDe.AutoSize = true;
            this.lblBrandTieuDe.Location = new System.Drawing.Point(72, 22);
            this.lblBrandTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblBrandTieuDe.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblBrandTieuDe.Text = "QUẢN LÝ HÓA ĐƠN";

            // lblBrandPhanHe
            this.lblBrandPhanHe.AutoSize = true;
            this.lblBrandPhanHe.Location = new System.Drawing.Point(74, 48);
            this.lblBrandPhanHe.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.lblBrandPhanHe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBrandPhanHe.Text = "Doanh Nghiệp • WinForms";

            // ==========================================
            // pnlTaiKhoanSidebar (Dưới cùng Sidebar)
            // ==========================================
            this.pnlTaiKhoanSidebar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTaiKhoanSidebar.Height = 82;
            this.pnlTaiKhoanSidebar.BackColor = System.Drawing.Color.FromArgb(21, 44, 72);
            this.pnlTaiKhoanSidebar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlTaiKhoanSidebar.Controls.Add(this.pnlAvatarSidebar);
            this.pnlTaiKhoanSidebar.Controls.Add(this.lblTenNguoiDungSidebar);
            this.pnlTaiKhoanSidebar.Controls.Add(this.lblVaiTroSidebar);
            this.pnlTaiKhoanSidebar.Controls.Add(this.lblChevronSidebar);

            // pnlAvatarSidebar
            this.pnlAvatarSidebar.Size = new System.Drawing.Size(44, 44);
            this.pnlAvatarSidebar.Location = new System.Drawing.Point(14, 19);
            this.pnlAvatarSidebar.BackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.pnlAvatarSidebar.Controls.Add(this.lblAvatarSidebar);

            // lblAvatarSidebar
            this.lblAvatarSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarSidebar.ForeColor = System.Drawing.Color.White;
            this.lblAvatarSidebar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvatarSidebar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatarSidebar.Text = "QL";

            // lblTenNguoiDungSidebar
            this.lblTenNguoiDungSidebar.AutoSize = false;
            this.lblTenNguoiDungSidebar.Size = new System.Drawing.Size(165, 24);
            this.lblTenNguoiDungSidebar.Location = new System.Drawing.Point(66, 17);
            this.lblTenNguoiDungSidebar.ForeColor = System.Drawing.Color.White;
            this.lblTenNguoiDungSidebar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTenNguoiDungSidebar.AutoEllipsis = true;
            this.lblTenNguoiDungSidebar.Text = "Người dùng";

            // lblVaiTroSidebar
            this.lblVaiTroSidebar.AutoSize = false;
            this.lblVaiTroSidebar.Size = new System.Drawing.Size(165, 20);
            this.lblVaiTroSidebar.Location = new System.Drawing.Point(66, 42);
            this.lblVaiTroSidebar.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.lblVaiTroSidebar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVaiTroSidebar.Text = "Quản lý";

            // lblChevronSidebar
            this.lblChevronSidebar.Size = new System.Drawing.Size(20, 20);
            this.lblChevronSidebar.Location = new System.Drawing.Point(240, 31);
            this.lblChevronSidebar.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.lblChevronSidebar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChevronSidebar.Text = "▲";
            this.lblChevronSidebar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // flpSidebarMenu (Giữa)
            this.flpSidebarMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSidebarMenu.BackColor = System.Drawing.Color.Transparent;
            this.flpSidebarMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSidebarMenu.WrapContents = false;
            this.flpSidebarMenu.AutoScroll = true;
            this.flpSidebarMenu.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);

            // ==========================================
            // cmsTaiKhoan (Dropdown mở lên phía trên)
            // ==========================================
            this.cmsTaiKhoan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmiThongTinCaNhan,
                this.tsmiDoiMatKhau,
                this.tsmiPhanCach,
                this.tsmiDangXuat
            });
            this.cmsTaiKhoan.Name = "cmsTaiKhoan";
            this.cmsTaiKhoan.Size = new System.Drawing.Size(230, 110);

            // tsmiThongTinCaNhan
            this.tsmiThongTinCaNhan.Name = "tsmiThongTinCaNhan";
            this.tsmiThongTinCaNhan.Size = new System.Drawing.Size(229, 28);
            this.tsmiThongTinCaNhan.Text = "👤  Thông tin cá nhân";

            // tsmiDoiMatKhau
            this.tsmiDoiMatKhau.Name = "tsmiDoiMatKhau";
            this.tsmiDoiMatKhau.Size = new System.Drawing.Size(229, 28);
            this.tsmiDoiMatKhau.Text = "🔑  Cập nhật mật khẩu";

            // tsmiPhanCach
            this.tsmiPhanCach.Name = "tsmiPhanCach";
            this.tsmiPhanCach.Size = new System.Drawing.Size(226, 6);

            // tsmiDangXuat
            this.tsmiDangXuat.Name = "tsmiDangXuat";
            this.tsmiDangXuat.Size = new System.Drawing.Size(229, 28);
            this.tsmiDangXuat.Text = "🚪  Đăng xuất";

            // ==========================================
            // pnlTopbar (Phía trên - #FFFFFF)
            // ==========================================
            this.pnlTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopbar.Height = 74;
            this.pnlTopbar.BackColor = System.Drawing.Color.White;
            this.pnlTopbar.Controls.Add(this.lblTopbarTieuDe);
            this.pnlTopbar.Controls.Add(this.lblTopbarBreadcrumb);
            this.pnlTopbar.Controls.Add(this.pnlNguoiDung);

            // lblTopbarBreadcrumb
            this.lblTopbarBreadcrumb.AutoSize = true;
            this.lblTopbarBreadcrumb.Location = new System.Drawing.Point(24, 13);
            this.lblTopbarBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTopbarBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTopbarBreadcrumb.Text = "Trang chủ / Tổng quan";

            // lblTopbarTieuDe
            this.lblTopbarTieuDe.AutoSize = true;
            this.lblTopbarTieuDe.Location = new System.Drawing.Point(22, 34);
            this.lblTopbarTieuDe.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblTopbarTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTopbarTieuDe.Text = "Tổng quan hệ thống";

            // pnlNguoiDung (Topbar)
            this.pnlNguoiDung.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.pnlNguoiDung.Size = new System.Drawing.Size(320, 60);
            this.pnlNguoiDung.Location = new System.Drawing.Point(580, 7);
            this.pnlNguoiDung.BackColor = System.Drawing.Color.Transparent;
            this.pnlNguoiDung.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlNguoiDung.Controls.Add(this.pnlAvatarCircle);
            this.pnlNguoiDung.Controls.Add(this.lblTenNguoiDung);
            this.pnlNguoiDung.Controls.Add(this.lblVaiTroBadge);

            // pnlAvatarCircle
            this.pnlAvatarCircle.Size = new System.Drawing.Size(46, 46);
            this.pnlAvatarCircle.Location = new System.Drawing.Point(260, 7);
            this.pnlAvatarCircle.BackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.pnlAvatarCircle.Controls.Add(this.lblAvatarKyHieu);

            // lblAvatarKyHieu
            this.lblAvatarKyHieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarKyHieu.ForeColor = System.Drawing.Color.White;
            this.lblAvatarKyHieu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvatarKyHieu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatarKyHieu.Text = "QL";

            // lblTenNguoiDung
            this.lblTenNguoiDung.AutoSize = false;
            this.lblTenNguoiDung.Size = new System.Drawing.Size(245, 22);
            this.lblTenNguoiDung.Location = new System.Drawing.Point(10, 10);
            this.lblTenNguoiDung.ForeColor = System.Drawing.Color.FromArgb(23, 32, 51);
            this.lblTenNguoiDung.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTenNguoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTenNguoiDung.Text = "Người dùng";

            // lblVaiTroBadge
            this.lblVaiTroBadge.AutoSize = false;
            this.lblVaiTroBadge.Size = new System.Drawing.Size(245, 20);
            this.lblVaiTroBadge.Location = new System.Drawing.Point(10, 33);
            this.lblVaiTroBadge.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.lblVaiTroBadge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblVaiTroBadge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVaiTroBadge.Text = "Quản lý";

            // ==========================================
            // pnlNoiDungChinh (Vùng trung tâm - #F4F7FB)
            // ==========================================
            this.pnlNoiDungChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoiDungChinh.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);

            // ==========================================
            // FormChinh
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.MinimumSize = new System.Drawing.Size(1080, 680);
            this.Controls.Add(this.pnlNoiDungChinh);
            this.Controls.Add(this.pnlTopbar);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "FormChinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Hóa Đơn - Bán Hàng & Doanh Nghiệp";

            this.pnlSidebar.ResumeLayout(false);
            this.pnlTaiKhoanSidebar.ResumeLayout(false);
            this.pnlAvatarSidebar.ResumeLayout(false);
            this.pnlSidebarBrand.ResumeLayout(false);
            this.pnlSidebarBrand.PerformLayout();
            this.cmsTaiKhoan.ResumeLayout(false);
            this.pnlTopbar.ResumeLayout(false);
            this.pnlTopbar.PerformLayout();
            this.pnlNguoiDung.ResumeLayout(false);
            this.pnlAvatarCircle.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
