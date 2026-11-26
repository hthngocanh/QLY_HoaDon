using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyHoaDon.GUI.Controls
{
    public enum InputIconType
    {
        None,
        User,
        Lock
    }

    public class RoundedTextBox : UserControl
    {
        private readonly TextBox _textBox;
        private readonly Button? _btnToggleEye;
        private bool _isFocused = false;
        private bool _isPassword = false;
        private bool _showPassword = false;
        private string _placeholderText = string.Empty;
        private InputIconType _iconType = InputIconType.None;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get; set; } = 10;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = ColorTranslator.FromHtml("#DCE3ED");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color FocusBorderColor { get; set; } = ColorTranslator.FromHtml("#2563EB");

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PlaceholderColor { get; set; } = ColorTranslator.FromHtml("#94A3B8");

        public new event KeyEventHandler? KeyDown;

        [AllowNull]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get => _textBox?.Text ?? string.Empty;
            set
            {
                if (_textBox != null)
                {
                    _textBox.Text = value ?? string.Empty;
                }
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value ?? string.Empty;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseSystemPasswordChar
        {
            get => _isPassword;
            set
            {
                _isPassword = value;
                _showPassword = !value;
                _textBox.UseSystemPasswordChar = value;
                if (_btnToggleEye != null)
                {
                    _btnToggleEye.Visible = value;
                }
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPassword
        {
            get => UseSystemPasswordChar;
            set => UseSystemPasswordChar = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public InputIconType IconType
        {
            get => _iconType;
            set
            {
                _iconType = value;
                CapNhatViTriTextBox();
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox InnerTextBox => _textBox;

        public RoundedTextBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.Height = 50;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);

            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                ForeColor = ColorTranslator.FromHtml("#172033"),
                Font = this.Font,
                Location = new Point(44, 15),
                Width = this.Width - 55,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            _textBox.Enter += (s, e) =>
            {
                _isFocused = true;
                this.Invalidate();
            };

            _textBox.Leave += (s, e) =>
            {
                _isFocused = false;
                this.Invalidate();
            };

            _textBox.TextChanged += (s, e) =>
            {
                this.Invalidate();
                this.OnTextChanged(e);
            };

            _textBox.KeyDown += (s, e) =>
            {
                this.KeyDown?.Invoke(this, e);
            };

            // Nút con mắt ẩn/hiện mật khẩu
            _btnToggleEye = new Button
            {
                Size = new Size(34, 34),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Visible = false,
                Anchor = AnchorStyles.Right
            };
            _btnToggleEye.FlatAppearance.BorderSize = 0;
            _btnToggleEye.FlatAppearance.MouseDownBackColor = Color.Transparent;
            _btnToggleEye.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 243, 248);
            _btnToggleEye.Paint += VeIconConMat;
            _btnToggleEye.Click += (s, e) =>
            {
                _showPassword = !_showPassword;
                _textBox.UseSystemPasswordChar = !_showPassword;
                _btnToggleEye.Invalidate();
                _textBox.Focus();
                _textBox.SelectionStart = _textBox.Text.Length;
            };

            this.Controls.Add(_textBox);
            this.Controls.Add(_btnToggleEye);

            this.SizeChanged += (s, e) => CapNhatViTriTextBox();
            this.Click += (s, e) => _textBox.Focus();
        }

        private void CapNhatViTriTextBox()
        {
            int left = (_iconType != InputIconType.None) ? 44 : 16;
            int right = (_btnToggleEye != null && _btnToggleEye.Visible) ? 44 : 16;
            int top = (this.Height - _textBox.PreferredHeight) / 2;

            _textBox.Location = new Point(left, top);
            _textBox.Width = Math.Max(20, this.Width - left - right);

            if (_btnToggleEye != null)
            {
                _btnToggleEye.Location = new Point(this.Width - 40, (this.Height - 34) / 2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // Nền bo góc
            using (var path = TaoDuongDanBoGoc(rect, BorderRadius))
            {
                using (var brush = new SolidBrush(Color.White))
                {
                    g.FillPath(brush, path);
                }

                // Viền (Focus: #2563EB, Thường: #DCE3ED)
                Color mauVien = _isFocused ? FocusBorderColor : BorderColor;
                float doDay = _isFocused ? 1.8f : 1.2f;

                using (var pen = new Pen(mauVien, doDay))
                {
                    g.DrawPath(pen, path);
                }
            }

            // Vẽ Icon trái nếu có
            if (_iconType != InputIconType.None)
            {
                Color mauIcon = _isFocused ? FocusBorderColor : ColorTranslator.FromHtml("#94A3B8");
                VeIconTrai(g, _iconType, 15, (this.Height - 20) / 2, mauIcon);
            }

            // Vẽ Placeholder nếu trống và không focus
            if (string.IsNullOrEmpty(_textBox.Text) && !_isFocused && !string.IsNullOrEmpty(_placeholderText))
            {
                int placeholderLeft = (_iconType != InputIconType.None) ? 46 : 18;
                int placeholderTop = (this.Height - _textBox.PreferredHeight) / 2 + 1;
                TextRenderer.DrawText(g, _placeholderText, _textBox.Font, new Point(placeholderLeft, placeholderTop), PlaceholderColor, TextFormatFlags.Left);
            }
        }

        private void VeIconTrai(Graphics g, InputIconType icon, int x, int y, Color color)
        {
            using var pen = new Pen(color, 1.6f);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;

            if (icon == InputIconType.User)
            {
                // Đầu người (tròn)
                g.DrawEllipse(pen, x + 4, y + 1, 9, 9);
                // Vai người (cung tròn)
                g.DrawArc(pen, x + 1, y + 10, 15, 10, 200, 140);
            }
            else if (icon == InputIconType.Lock)
            {
                // Thân khóa
                g.DrawRectangle(pen, x + 2, y + 8, 14, 11);
                // Quai khóa
                g.DrawArc(pen, x + 5, y + 1, 8, 9, 180, 180);
                // Lỗ khóa nhỏ
                using var fillBrush = new SolidBrush(color);
                g.FillEllipse(fillBrush, x + 8, y + 12, 2.5f, 2.5f);
            }
        }

        private void VeIconConMat(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Màu icon con mắt sắc nét, tương phản rõ trên nền trắng (#334155 - Slate-700)
            Color mau = _btnToggleEye != null && _btnToggleEye.ClientRectangle.Contains(_btnToggleEye.PointToClient(Cursor.Position))
                ? ColorTranslator.FromHtml("#2563EB")
                : ColorTranslator.FromHtml("#334155");

            using var pen = new Pen(mau, 1.8f);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;

            int w = _btnToggleEye!.Width;
            int h = _btnToggleEye.Height;
            int cx = w / 2;
            int cy = h / 2;

            // Đường cong mi trên và mi dưới
            g.DrawArc(pen, cx - 10, cy - 7, 20, 14, 25, 130);
            g.DrawArc(pen, cx - 10, cy - 7, 20, 14, 205, 130);

            // Con ngươi
            using (var brush = new SolidBrush(mau))
            {
                g.FillEllipse(brush, cx - 3f, cy - 3f, 6, 6);
            }

            // Gạch chéo dứt khoát nếu đang ẩn mật khẩu
            if (!_showPassword)
            {
                using var penSlash = new Pen(mau, 2.0f);
                penSlash.StartCap = LineCap.Round;
                penSlash.EndCap = LineCap.Round;
                g.DrawLine(penSlash, cx - 8, cy - 8, cx + 8, cy + 8);
            }
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

        public new void Focus()
        {
            _textBox.Focus();
        }

        public void SelectAll()
        {
            _textBox.SelectAll();
        }

        public void Clear()
        {
            _textBox.Clear();
        }
    }
}
