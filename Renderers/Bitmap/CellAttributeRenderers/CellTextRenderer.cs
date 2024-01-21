using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public class CellTextRenderer : ICellAttributeRenderer
    {
        public int Order => 2;

        private static SKPaint GetTextPen(int textSize, SKColor? colour = null) => new()
        {
            Style = SKPaintStyle.Fill,
            Color = colour ?? SKColors.Black,
            StrokeWidth = 1,
            IsAntialias = true,
            TextAlign = SKTextAlign.Center,
            TextSize = textSize,
        };

        public void Render(CellAttributeRenderingContext context)
        {
            string? text = context.Attribute?.GetValueAs<string>();
            if (string.IsNullOrEmpty(text)) return;

            int textSize = context.Bounds.Width / 4;
            using var pen = GetTextPen(textSize);

            context.Canvas.DrawCenteredText(context.Bounds, text, textSize, pen);
        }
    }
}