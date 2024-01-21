using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class DefaultCellRenderer
    {
        public int Order => 0;

        public void Render(CellRenderingContext context)
        {
            if (!context.Cell.IsVoid)
            {
                using SKPaint paint = new() { Style = SKPaintStyle.Fill, Color = SKColors.AntiqueWhite };

                context.Canvas.DrawRect(context.ContentBounds, paint);
            }
        }
    }
}