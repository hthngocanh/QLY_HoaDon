using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyHoaDon.GUI.Controls
{
    public class RoundedButton : Button
    {
        private bool _isHovered = false;
        private bool _isPressed = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get; set; } = 10;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color NormalColor { get; set; } = ColorTranslator.FromHtml("#2563EB");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color HoverColor { get; set; } = ColorTranslator.FromHtml("#1D4ED8");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PressedColor { get; set; } = ColorTranslator.FromHtml("#1E40AF");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color DisabledColor { get; set; } = ColorTranslator.FromHtml("#94A3B8");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.Transparent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderWidth { get; set; } = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color TextColor
        {
            get => this.ForeColor;
            set => this.ForeColor = value;
        }

        public RoundedButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.Height = 50;
            this.Cursor = Cursors.Hand;
            this.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            this.ForeColor = Color.White;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Xác định màu nền theo trạng thái
            Color mauNen;
            if (!this.Enabled)
            {
                mauNen = DisabledColor;
            }
            else if (_isPressed)
            {
                mauNen = PressedColor;
            }
            else if (_isHovered)
            {
                mauNen = HoverColor;
            }
            else
            {
                mauNen = NormalColor;
            }

            var rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using var path = TaoDuongDanBoGoc(rect, BorderRadius);

            // Vẽ nền bo góc
            using (var brush = new SolidBrush(mauNen))
            {
                g.FillPath(brush, path);
            }

            // Vẽ viền nếu có
            if (BorderWidth > 0 && BorderColor != Color.Transparent)
            {
                using var penBorder = new Pen(BorderColor, BorderWidth);
                g.DrawPath(penBorder, path);
            }

            // Vẽ chữ căn giữa
            TextRenderer.DrawText(
                g,
                this.Text,
                this.Font,
                rect,
                this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

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
