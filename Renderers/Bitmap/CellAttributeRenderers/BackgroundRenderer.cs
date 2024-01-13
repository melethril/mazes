using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public class BackgroundRenderer(SKColor? colour = null) : ICellAttributeRenderer
    {
        private readonly SKColor? colour = colour;

        public int Order => 0;

        public void Render(RenderContext context)
        {
            if (colour == null) return;

            context.Canvas.DrawRect(context.ContentBounds, new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = colour.Value,
            });
        }
    }
}
