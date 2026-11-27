using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyHoaDon.GUI.Controls
{
    /// <summary>
    /// Panel bo góc: nền = BackColor, có viền tùy chọn. Dùng làm thẻ (card) và khung ô nhập.
    /// </summary>
    public class RoundedPanel : Panel
    {
        private int _borderRadius = 12;
        private int _borderWidth = 1;
        private Color _borderColor = ColorTranslator.FromHtml("#E2E8F0");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderRadius { get => _borderRadius; set { _borderRadius = value; Invalidate(); } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderWidth { get => _borderWidth; set { _borderWidth = value; Invalidate(); } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        public RoundedPanel()
        {
            SetStyle(ControlStyles.UserPaint
                   | ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.ResizeRedraw
                   | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.White;
        }

        private Color LayMauNenCha()
        {
            for (Control? c = Parent; c != null; c = c.Parent)
            {
                if (c.BackColor.A == 255) return c.BackColor;
            }
            return SystemColors.Control;
        }

        protected override void OnParentChanged(EventArgs e) { base.OnParentChanged(e); Invalidate(); }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var brushCha = new SolidBrush(LayMauNenCha()))
                g.FillRectangle(brushCha, ClientRectangle);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            float bw = Math.Max(0, _borderWidth);
            var rect = new RectangleF(bw / 2f, bw / 2f, Width - bw - 1, Height - bw - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            float r = Math.Min(_borderRadius, Math.Min(rect.Width, rect.Height) / 2f);
            float d = r * 2f;

            using var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            using (var brush = new SolidBrush(BackColor))
                g.FillPath(brush, path);

            if (bw > 0 && _borderColor != Color.Transparent)
            {
                using var pen = new Pen(_borderColor, bw);
                g.DrawPath(pen, path);
            }
        }
    }
}