using SkiaSharp;

namespace Mazes.Renderers.Bitmap;

internal static class CanvasExtensions
{
    public static void DrawCenteredText(this SKCanvas canvas, SKRectI bounds, string value, int textSize, SKPaint pen)
    {
            int centreY = bounds.Top + (bounds.Width / 2) + (textSize / 2);

            var textPath = new SKPath();
            textPath.MoveTo(bounds.Left, centreY);
            textPath.LineTo(bounds.Right, centreY);

            canvas.DrawTextOnPath(value, textPath, new(0, 0), pen);
        }
}