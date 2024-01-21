using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class BreadcrumbRenderer : ICellAttributeRenderer
    {
        public int Order => 0;
        private const int MinPillSize = 2;

        public void Render(CellAttributeRenderingContext context)
        {
            SKRectI bounds = context.Bounds;
            int pillSize = Math.Max(bounds.Width / 24, MinPillSize);
            int centreX = bounds.Left + (bounds.Width / 2);
            int centreY = bounds.Top + (bounds.Height / 2);

            var point = new SKPoint(centreX, centreY);
            using var fill = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.DimGray };
            using var stroke = new SKPaint { Style = SKPaintStyle.Stroke, Color = SKColors.DimGray, StrokeWidth = 1 };

            context.Canvas.DrawCircle(point, pillSize, fill);
            context.Canvas.DrawCircle(point, pillSize, stroke);
        }
    }
}
