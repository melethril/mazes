using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class BreadcrumbRenderer : ICellAttributeRenderer
    {
        public int Order => 0;

        public void Render(RenderContext context)
        {
            var bounds = context.CellBounds;
            int pillSize = bounds.Width / 12;
            int centreX = bounds.Left + (bounds.Width / 2);
            int centreY = bounds.Top + (bounds.Height / 2);

            var fill = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.MediumSeaGreen };
            var stroke = new SKPaint { Style = SKPaintStyle.Stroke, Color = SKColors.DarkGray, StrokeWidth = 1 };

            context.Canvas.DrawCircle(new SKPoint(centreX, centreY), pillSize, fill);
            context.Canvas.DrawCircle(new SKPoint(centreX, centreY), pillSize, stroke);
        }
    }
}
