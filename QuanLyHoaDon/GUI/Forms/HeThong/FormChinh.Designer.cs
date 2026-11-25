namespace QuanLyHoaDon.GUI.Forms.HeThong
{
    partial class FormChinh
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Panel pnlCardThongTin;
        private System.Windows.Forms.Label lblCardTieuDe;
        private System.Windows.Forms.Label lblCardNoiDung;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblTrangThai;

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
            pnlHeader = new System.Windows.Forms.Panel();
            lblTieuDe = new System.Windows.Forms.Label();
            lblMoTa = new System.Windows.Forms.Label();
            pnlCardThongTin = new System.Windows.Forms.Panel();
            lblCardTieuDe = new System.Windows.Forms.Label();
            lblCardNoiDung = new System.Windows.Forms.Label();
            btnKiemTra = new System.Windows.Forms.Button();
            statusStrip = new System.Windows.Forms.StatusStrip();
            lblTrangThai = new System.Windows.Forms.ToolStripStatusLabel();

            pnlHeader.SuspendLayout();
            pnlCardThongTin.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();

            // pnlHeader
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Height = 85;
            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblMoTa);

            // lblTieuDe
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new System.Drawing.Point(24, 18);
            lblTieuDe.Text = "HỆ THỐNG QUẢN LÝ HÓA ĐƠN";

            // lblMoTa
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new System.Drawing.Point(26, 48);
            lblMoTa.Text = "Đồ án Lập trình Windows - Khởi tạo bộ khung kiến trúc 3-Layer";

            // pnlCardThongTin
            pnlCardThongTin.Location = new System.Drawing.Point(40, 115);
            pnlCardThongTin.Size = new System.Drawing.Size(720, 260);
            pnlCardThongTin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlCardThongTin.Controls.Add(lblCardTieuDe);
            pnlCardThongTin.Controls.Add(lblCardNoiDung);
            pnlCardThongTin.Controls.Add(btnKiemTra);

            // lblCardTieuDe
            lblCardTieuDe.AutoSize = true;
            lblCardTieuDe.Location = new System.Drawing.Point(25, 20);
            lblCardTieuDe.Text = "TRẠNG THÁI KHỞI TẠO BỘ KHUNG HỆ THỐNG";

            // lblCardNoiDung
            lblCardNoiDung.Location = new System.Drawing.Point(25, 55);
            lblCardNoiDung.Size = new System.Drawing.Size(670, 120);
            lblCardNoiDung.Text = "• Kiến trúc: 3-Layer + DTO + Utilities\n" +
                               "• Tầng phân tách: GUI (Forms, Controls) -> BLL -> DAL -> DTO\n" +
                               "• Nền tảng: C# / Windows Forms (.NET 10 LTS)\n" +
                               "• Môi trường phát triển: Visual Studio Community 2026\n" +
                               "• Cơ sở dữ liệu: Microsoft SQL Server (QuanLyHoaDon - 7 Bảng)\n" +
                               "• Quy chuẩn: Tiếng Việt không dấu cho mã nguồn và cấu trúc bảng CSDL";

            // btnKiemTra
            btnKiemTra.Location = new System.Drawing.Point(28, 190);
            btnKiemTra.Size = new System.Drawing.Size(200, 42);
            btnKiemTra.Text = "Kiểm Tra Trạng Thái";
            btnKiemTra.UseVisualStyleBackColor = false;
            btnKiemTra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnKiemTra.FlatAppearance.BorderSize = 0;
            btnKiemTra.Click += BtnKiemTra_Click;

            // statusStrip
            statusStrip.Items.Add(lblTrangThai);
            lblTrangThai.Text = "Sẵn sàng | .NET 10 (LTS) | Visual Studio 2026 | Git: main";

            // FormChinh
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(pnlCardThongTin);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormChinh";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Hệ Thống Quản Lý Hóa Đơn";

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlCardThongTin.ResumeLayout(false);
            pnlCardThongTin.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

