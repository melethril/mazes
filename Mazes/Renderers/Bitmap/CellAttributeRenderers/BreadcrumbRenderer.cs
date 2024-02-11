using Mazes.Renderers.Bitmap.RenderingContexts;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap.CellAttributeRenderers;

internal class BreadcrumbRenderer : ICellAttributeRenderer
{
    public int Order => 0;
    private const int MinPillSize = 2;

    public void Render(CellAttributeRenderingContext context)
    {
        var bounds = context.Bounds;
        int pillSize = Math.Max(bounds.Width / 6, MinPillSize);
        int centreX = bounds.Left + (bounds.Width / 2);
        int centreY = bounds.Top + (bounds.Height / 2);
        var point = new SKPoint(centreX, centreY);

        using var fill = new SKPaint();
        fill.Style = SKPaintStyle.Fill;
        fill.Color = context.Style.GetForegroundColour();

        using var stroke = new SKPaint();
        stroke.Style = SKPaintStyle.Stroke;
        stroke.Color = context.Style.GetOutlineColour();
        stroke.StrokeWidth = 1;

        context.Canvas.DrawCircle(point, pillSize, fill);
        context.Canvas.DrawCircle(point, pillSize, stroke);
    }
}