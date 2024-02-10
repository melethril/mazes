using Mazes.Renderers.Bitmap.RenderingContexts;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap.CellAttributeRenderers;

internal class GridLineRenderer : ICellAttributeRenderer
{
    public int Order => 2;

    public void Render(CellAttributeRenderingContext context)
    {
        using SKPaint brush = new();
        brush.IsStroke = true;
        brush.Color = SKColors.LightGray;
        brush.StrokeWidth = 1;
        brush.PathEffect = SKPathEffect.CreateDash([4, 4], 0);

        context.Canvas.DrawRect(context.Bounds, brush);
    }
}