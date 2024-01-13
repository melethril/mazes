using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public class Style(SKColor? fillColour = null, SKColor? strokeColour = null, float? strokeWidth = null)
    {
        private readonly SKColor fillColour = fillColour ?? SKColors.White;
        private readonly SKColor strokeColour = strokeColour ?? SKColors.Black;
        private readonly float strokeWidth = strokeWidth ?? 2;

        public SKPaint FillBrush => new()
        {
            Style = SKPaintStyle.Fill,
            Color = fillColour,
        };

        public SKPaint StrokeBrush => new()
        {
            Style = SKPaintStyle.Stroke,
            Color = strokeColour,
            StrokeWidth = strokeWidth,
            IsAntialias = true,
        };
    }
}