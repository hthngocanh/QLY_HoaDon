using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyHoaDon.GUI.Controls
{
    public class RoundedButton : Button
    {
        private bool _isHovered;
        private bool _isPressed;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get; set; } = 8;

        // Màu thường = BackColor để code cũ đổi BackColor vẫn hoạt động
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color NormalColor
        {
            get => BackColor;
            set => BackColor = value;
        }

        // Color.Empty = tự tính từ BackColor
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color HoverColor { get; set; } = Color.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PressedColor { get; set; } = Color.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color DisabledColor { get; set; } = ColorTranslator.FromHtml("#9DB4CE");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.Transparent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderWidth { get; set; } = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color TextColor
        {
            get => ForeColor;
            set => ForeColor = value;
        }

        // Thanh nhấn nhỏ bên trái (dùng cho mục menu đang chọn). Color.Empty = không vẽ
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color AccentBarColor { get; set; } = Color.Empty;

        public RoundedButton()
        {
            SetStyle(ControlStyles.UserPaint
                   | ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.ResizeRedraw
                   | ControlStyles.SupportsTransparentBackColor, true);

            Height = 40;
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            ForeColor = Color.White;
            BackColor = ColorTranslator.FromHtml("#2563EB");
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            TextAlign = ContentAlignment.MiddleCenter;
        }

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; _isPressed = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { _isPressed = true; Invalidate(); }
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) { _isPressed = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { base.OnEnabledChanged(e); Invalidate(); }
        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;

            // 1. Tô nền của control cha để góc bo không bị đen
            Color nenCha = LayMauNenCha();
            using (var brushCha = new SolidBrush(nenCha))
                g.FillRectangle(brushCha, ClientRectangle);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 2. Chọn màu theo trạng thái
            Color mauNen;
            if (!Enabled) mauNen = DisabledColor;
            else if (_isPressed) mauNen = PressedColor != Color.Empty ? PressedColor : ControlPaint.Dark(BackColor, 0.15f);
            else if (_isHovered) mauNen = HoverColor != Color.Empty ? HoverColor : ControlPaint.Dark(BackColor, 0.08f);
            else mauNen = BackColor;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int r = Math.Min(BorderRadius, Math.Min(rect.Width, rect.Height) / 2);

            using (var path = TaoDuongDanBoGoc(rect, r))
            {
                using (var brush = new SolidBrush(mauNen))
                    g.FillPath(brush, path);

                if (BorderWidth > 0 && BorderColor != Color.Transparent)
                {
                    using var pen = new Pen(BorderColor, BorderWidth);
                    g.DrawPath(pen, path);
                }
            }

            if (AccentBarColor != Color.Empty && Enabled)
            {
                using var brushBar = new SolidBrush(AccentBarColor);
                g.FillRectangle(brushBar, 0, r, 4, Math.Max(1, Height - 1 - 2 * r));
            }

            // 3. Vẽ chữ (không cắt, có dấu ... nếu quá dài)
            var vungChu = new Rectangle(
                Padding.Left, 0,
                Math.Max(1, Width - Padding.Horizontal),
                Height);

            TextFormatFlags flags = TextFormatFlags.VerticalCenter
                                  | TextFormatFlags.SingleLine
                                  | TextFormatFlags.EndEllipsis
                                  | TextFormatFlags.NoPadding;

            flags |= (TextAlign == ContentAlignment.MiddleLeft ||
                      TextAlign == ContentAlignment.TopLeft ||
                      TextAlign == ContentAlignment.BottomLeft)
                ? TextFormatFlags.Left
                : TextFormatFlags.HorizontalCenter;

            Color mauChu = Enabled ? ForeColor : Color.White;
            TextRenderer.DrawText(g, Text, Font, vungChu, mauChu, flags);
        }

        // Tìm màu nền thật (đục hoàn toàn) của các control cha, tránh góc bị đen khi cha trong suốt
        private Color LayMauNenCha()
        {
            for (Control? c = Parent; c != null; c = c.Parent)
            {
                if (c.BackColor.A == 255) return c.BackColor;
            }
            return SystemColors.Control;
        }

        protected override void OnParentChanged(EventArgs e) { base.OnParentChanged(e); Invalidate(); }
        protected override void OnParentBackColorChanged(EventArgs e) { base.OnParentBackColorChanged(e); Invalidate(); }

        private static GraphicsPath TaoDuongDanBoGoc(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            float d = radius * 2f;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}