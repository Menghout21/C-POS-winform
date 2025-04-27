using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class BorderRadiusHelper
{
    public static void ApplyBorderRadius(Control control, int radius, Color borderColor, int borderSize = 1)
    {
        if (control == null)
            throw new ArgumentNullException(nameof(control));

        control.Paint += (sender, e) =>
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new Rectangle(0, 0, control.Width - 1, control.Height - 1);
            GraphicsPath path = GetRoundedPath(bounds, radius);

            // Draw border only
            using (Pen pen = new Pen(borderColor, borderSize))
            {
                graphics.DrawPath(pen, path);
            }

            // Apply rounded region
            control.Region = new Region(path);
        };

        // Redraw when resized
        control.Resize += (sender, e) =>
        {
            Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
            control.Region = new Region(GetRoundedPath(bounds, radius));
            control.Invalidate();
        };
    }

    private static GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        path.StartFigure();
        path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }
}
