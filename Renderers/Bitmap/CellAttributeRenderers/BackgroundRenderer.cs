using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public class BackgroundRenderer(SKColor? colour = null) : ICellAttributeRenderer
    {
        private readonly SKColor? colour = colour;

        public int Order => 0;

        public void Render(CellAttributeRenderingContext context)
        {
            if (colour == null) return;
            using SKPaint paint = new() { Style = SKPaintStyle.Fill, Color = colour.Value };

            context.Canvas.DrawRect(context.ContentBounds, paint);
        }
    }
}
