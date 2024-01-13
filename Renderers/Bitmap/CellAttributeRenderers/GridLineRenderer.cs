using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class GridLineRenderer : ICellAttributeRenderer
    {
        public int Order => 2;

        private static readonly Lazy<SKPaint> gridBrush = new(() => new()
        {
            IsStroke = true,
            Color = SKColors.LightGray,
            StrokeWidth = 1,
            PathEffect = SKPathEffect.CreateDash([4, 4], 0)
        });

        public void Render(RenderContext context)
        {
            context.Canvas.DrawRect(context.CellBounds, gridBrush.Value);
        }
    }
}
