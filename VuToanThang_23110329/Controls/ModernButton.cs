using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VuToanThang_23110329.Controls
{
    public class ModernButton : Button
    {
        // Button types with predefined colors
        public enum ButtonType
        {
            Primary,    // Blue - for main actions
            Success,    // Green - for save, add actions
            Warning,    // Orange - for edit, update actions
            Danger,     // Red - for delete actions
            Info,       // Light blue - for info actions
            Secondary,  // Gray - for cancel, secondary actions
            Dark        // Dark gray - for neutral actions
        }

        private ButtonType _buttonType = ButtonType.Primary;
        private int _borderRadius = 8;
        private bool _isHovered = false;
        private bool _isPressed = false;

        public ButtonType Type
        {
            get => _buttonType;
            set
            {
                _buttonType = value;
                UpdateColors();
                Invalidate();
            }
        }

        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = value;
                Invalidate();
            }
        }

        public ModernButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            Size = new Size(100, 36);
            Cursor = Cursors.Hand;
            UpdateColors();
        }

        private void UpdateColors()
        {
            switch (_buttonType)
            {
                case ButtonType.Primary:
                    BackColor = Color.FromArgb(74, 144, 226);
                    ForeColor = Color.White;
                    break;
                case ButtonType.Success:
                    BackColor = Color.FromArgb(40, 167, 69);
                    ForeColor = Color.White;
                    break;
                case ButtonType.Warning:
                    BackColor = Color.FromArgb(255, 193, 7);
                    ForeColor = Color.Black;
                    break;
                case ButtonType.Danger:
                    BackColor = Color.FromArgb(220, 53, 69);
                    ForeColor = Color.White;
                    break;
                case ButtonType.Info:
                    BackColor = Color.FromArgb(23, 162, 184);
                    ForeColor = Color.White;
                    break;
                case ButtonType.Secondary:
                    BackColor = Color.FromArgb(108, 117, 125);
                    ForeColor = Color.White;
                    break;
                case ButtonType.Dark:
                    BackColor = Color.FromArgb(52, 58, 64);
                    ForeColor = Color.White;
                    break;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            _isPressed = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isPressed = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Get the current background color
            Color currentBackColor = GetCurrentBackColor();

            // Create rounded rectangle path
            using (GraphicsPath path = GetRoundedRectanglePath(ClientRectangle, _borderRadius))
            {
                // Fill the button
                using (SolidBrush brush = new SolidBrush(currentBackColor))
                {
                    g.FillPath(brush, path);
                }

                // Add subtle border
                using (Pen pen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
                {
                    g.DrawPath(pen, path);
                }
            }

            // Draw text
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // Draw focus rectangle if focused
            if (Focused)
            {
                using (Pen pen = new Pen(Color.FromArgb(100, ForeColor), 2))
                {
                    pen.DashStyle = DashStyle.Dot;
                    Rectangle focusRect = ClientRectangle;
                    focusRect.Inflate(-3, -3);
                    using (GraphicsPath focusPath = GetRoundedRectanglePath(focusRect, _borderRadius - 2))
                    {
                        g.DrawPath(pen, focusPath);
                    }
                }
            }
        }

        private Color GetCurrentBackColor()
        {
            Color baseColor = BackColor;

            if (!Enabled)
            {
                return Color.FromArgb(120, baseColor.R, baseColor.G, baseColor.B);
            }

            if (_isPressed)
            {
                return DarkenColor(baseColor, 0.2f);
            }

            if (_isHovered)
            {
                return DarkenColor(baseColor, 0.1f);
            }

            return baseColor;
        }

        private Color DarkenColor(Color color, float factor)
        {
            int r = Math.Max(0, (int)(color.R * (1 - factor)));
            int g = Math.Max(0, (int)(color.G * (1 - factor)));
            int b = Math.Max(0, (int)(color.B * (1 - factor)));
            return Color.FromArgb(color.A, r, g, b);
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            // Top-left corner
            path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90);
            // Top-right corner
            path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90);
            // Bottom-right corner
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            // Bottom-left corner
            path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
