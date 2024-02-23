using SkiaSharp;

namespace Mazes.Renderers.Bitmap;

internal static class CanvasExtensions
{
    public static void RenderBackground(this SKCanvas canvas, SKColor colour, IBounds bounds)
    {
        using SKPaint paint = new();
        paint.StrokeWidth = 0;
        paint.Style = SKPaintStyle.Fill;
        paint.Color = colour;
        paint.IsAntialias = true;

        canvas.DrawPath(bounds.ToSKPath(), paint);
    }

    public static void RenderText(this SKCanvas canvas, string text, SKColor colour, IBounds bounds)
    {
        if (string.IsNullOrEmpty(text)) return;
        
        const float fractionOfCell = 0.25f;
        float textSize = ((bounds is ArcBounds ? bounds.Height : bounds.Width) * fractionOfCell);

        using SKPaint pen = new();
        pen.Style = SKPaintStyle.Fill;
        pen.Color = colour;
        pen.StrokeWidth = 1;
        pen.IsAntialias = true;
        pen.TextAlign = SKTextAlign.Center;
        pen.TextSize = textSize;

        using var textPath = new SKPath();
        textPath.MoveTo(bounds.Left, bounds.Centre.Y);
        textPath.LineTo(bounds.Right, bounds.Centre.Y);

        canvas.DrawTextOnPath(text, textPath, new(0, 0), pen);
    }
}