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
        
        decimal fractionOfCell = bounds is CircularBounds ? 0.04m : 0.08m;
        int pillSize = Math.Max((int)(bounds.Height * fractionOfCell), MinPillSize);
        var point = new SKPoint(bounds.Centre.X, bounds.Centre.Y);

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