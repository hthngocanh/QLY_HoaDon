namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    partial class FormChinh
    {
        private System.ComponentModel.IContainer components = null;

        // Layout Containers
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlSidebarBrand;
        private System.Windows.Forms.Label lblBrandLogo;
        private System.Windows.Forms.Label lblBrandTieuDe;
        private System.Windows.Forms.Label lblBrandPhanHe;
        private System.Windows.Forms.FlowLayoutPanel flpSidebarMenu;

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTopbarTieuDe;
        private System.Windows.Forms.Label lblTopbarBreadcrumb;
        private System.Windows.Forms.Panel pnlNguoiDung;
        private System.Windows.Forms.Panel pnlAvatarCircle;
        private System.Windows.Forms.Label lblAvatarKyHieu;
        private System.Windows.Forms.Label lblTenNguoiDung;
        private System.Windows.Forms.Label lblVaiTroBadge;

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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlSidebarBrand = new System.Windows.Forms.Panel();
            this.lblBrandLogo = new System.Windows.Forms.Label();
            this.lblBrandTieuDe = new System.Windows.Forms.Label();
            this.lblBrandPhanHe = new System.Windows.Forms.Label();
            this.flpSidebarMenu = new System.Windows.Forms.FlowLayoutPanel();

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
            this.pnlSidebarBrand.SuspendLayout();
            this.pnlTopbar.SuspendLayout();
            this.pnlNguoiDung.SuspendLayout();
            this.pnlAvatarCircle.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // pnlSidebar (Ben trai - #172B4D)
            // ==========================================
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width = 260;
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(23, 43, 77);
            this.pnlSidebar.Controls.Add(this.flpSidebarMenu);
            this.pnlSidebar.Controls.Add(this.pnlSidebarBrand);

            // pnlSidebarBrand
            this.pnlSidebarBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSidebarBrand.Height = 85;
            this.pnlSidebarBrand.BackColor = System.Drawing.Color.FromArgb(17, 33, 60);
            this.pnlSidebarBrand.Controls.Add(this.lblBrandLogo);
            this.pnlSidebarBrand.Controls.Add(this.lblBrandTieuDe);
            this.pnlSidebarBrand.Controls.Add(this.lblBrandPhanHe);

            // lblBrandLogo
            this.lblBrandLogo.Size = new System.Drawing.Size(42, 42);
            this.lblBrandLogo.Location = new System.Drawing.Point(18, 22);
            this.lblBrandLogo.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblBrandLogo.ForeColor = System.Drawing.Color.White;
            this.lblBrandLogo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBrandLogo.Text = "QL";
            this.lblBrandLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblBrandTieuDe
            this.lblBrandTieuDe.AutoSize = true;
            this.lblBrandTieuDe.Location = new System.Drawing.Point(68, 22);
            this.lblBrandTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblBrandTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBrandTieuDe.Text = "QUẢN LÝ HÓA ĐƠN";

            // lblBrandPhanHe
            this.lblBrandPhanHe.AutoSize = true;
            this.lblBrandPhanHe.Location = new System.Drawing.Point(70, 46);
            this.lblBrandPhanHe.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblBrandPhanHe.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBrandPhanHe.Text = "Doanh Nghiệp • WinForms";

            // flpSidebarMenu
            this.flpSidebarMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSidebarMenu.BackColor = System.Drawing.Color.Transparent;
            this.flpSidebarMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSidebarMenu.WrapContents = false;
            this.flpSidebarMenu.AutoScroll = true;
            this.flpSidebarMenu.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);

            // ==========================================
            // pnlTopbar (Phia tren - #FFFFFF)
            // ==========================================
            this.pnlTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopbar.Height = 70;
            this.pnlTopbar.BackColor = System.Drawing.Color.White;
            this.pnlTopbar.Controls.Add(this.lblTopbarTieuDe);
            this.pnlTopbar.Controls.Add(this.lblTopbarBreadcrumb);
            this.pnlTopbar.Controls.Add(this.pnlNguoiDung);

            // lblTopbarBreadcrumb
            this.lblTopbarBreadcrumb.AutoSize = true;
            this.lblTopbarBreadcrumb.Location = new System.Drawing.Point(24, 15);
            this.lblTopbarBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTopbarBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTopbarBreadcrumb.Text = "Trang chủ / Tổng quan";

            // lblTopbarTieuDe
            this.lblTopbarTieuDe.AutoSize = true;
            this.lblTopbarTieuDe.Location = new System.Drawing.Point(22, 34);
            this.lblTopbarTieuDe.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTopbarTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTopbarTieuDe.Text = "Tổng quan hệ thống";

            // pnlNguoiDung (Nhan vao de xem ho so)
            this.pnlNguoiDung.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.pnlNguoiDung.Size = new System.Drawing.Size(280, 56);
            this.pnlNguoiDung.Location = new System.Drawing.Point(620, 8);
            this.pnlNguoiDung.BackColor = System.Drawing.Color.Transparent;
            this.pnlNguoiDung.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlNguoiDung.Controls.Add(this.pnlAvatarCircle);
            this.pnlNguoiDung.Controls.Add(this.lblTenNguoiDung);
            this.pnlNguoiDung.Controls.Add(this.lblVaiTroBadge);

            // pnlAvatarCircle
            this.pnlAvatarCircle.Size = new System.Drawing.Size(42, 42);
            this.pnlAvatarCircle.Location = new System.Drawing.Point(225, 7);
            this.pnlAvatarCircle.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.pnlAvatarCircle.Controls.Add(this.lblAvatarKyHieu);

            // lblAvatarKyHieu
            this.lblAvatarKyHieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarKyHieu.ForeColor = System.Drawing.Color.White;
            this.lblAvatarKyHieu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAvatarKyHieu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatarKyHieu.Text = "QL";

            // lblTenNguoiDung
            this.lblTenNguoiDung.AutoSize = false;
            this.lblTenNguoiDung.Size = new System.Drawing.Size(210, 20);
            this.lblTenNguoiDung.Location = new System.Drawing.Point(10, 10);
            this.lblTenNguoiDung.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTenNguoiDung.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenNguoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTenNguoiDung.Text = "Người dùng";

            // lblVaiTroBadge
            this.lblVaiTroBadge.AutoSize = false;
            this.lblVaiTroBadge.Size = new System.Drawing.Size(210, 18);
            this.lblVaiTroBadge.Location = new System.Drawing.Point(10, 31);
            this.lblVaiTroBadge.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblVaiTroBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblVaiTroBadge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVaiTroBadge.Text = "Quản lý";

            // ==========================================
            // pnlNoiDungChinh (Vung trung tam - #F4F7FB)
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
            this.pnlSidebarBrand.ResumeLayout(false);
            this.pnlSidebarBrand.PerformLayout();
            this.pnlTopbar.ResumeLayout(false);
            this.pnlTopbar.PerformLayout();
            this.pnlNguoiDung.ResumeLayout(false);
            this.pnlAvatarCircle.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
