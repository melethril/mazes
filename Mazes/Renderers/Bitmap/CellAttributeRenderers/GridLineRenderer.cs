using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class GridLineRenderer : ICellAttributeRenderer
    {
        public int Order => 2;

        public void Render(CellAttributeRenderingContext context)
        {
            using SKPaint brush = new()
            {
                IsStroke = true,
                Color = SKColors.LightGray,
                StrokeWidth = 1,
                PathEffect = SKPathEffect.CreateDash([4, 4], 0)
            };

            context.Canvas.DrawRect(context.Bounds, brush);
        }
    }
}
