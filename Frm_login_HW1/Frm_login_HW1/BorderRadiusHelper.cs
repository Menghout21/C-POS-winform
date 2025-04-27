using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class BorderRadiusHelper
{
    public static void ApplyBorderRadius(Control control, int radius)
    {
        if (control == null)
            throw new ArgumentNullException(nameof(control));

        // Apply rounded region on Paint
        control.Paint += (sender, e) =>
        {
            Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
            control.Region = new Region(GetRoundedPath(bounds, radius));
        };

        // Reapply rounded region when resized
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
