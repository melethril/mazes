using Mazes.Core;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap.RenderingContexts;

public sealed class CellAttributeRenderingContext(CellStyle style, SKCanvas canvas, SKRectI cellBounds, ICell cell, SKRectI contentBounds, CellAttribute attribute)
{
    public CellStyle Style { get; } = style;
    public SKCanvas Canvas { get; } = canvas;
    public SKRectI Bounds { get; } = cellBounds;
    public SKRectI ContentBounds { get; } = contentBounds;
    public ICell Cell { get; } = cell;
    public CellAttribute Attribute { get; } = attribute;

    public void RenderBackground(SKColor colour)
    {
        using SKPaint paint = new();
        paint.Style = SKPaintStyle.Fill;
        paint.Color = colour;

        Canvas.DrawRect(ContentBounds, paint);
    }

    public void RenderText(string text, SKColor colour)
    {
        if (string.IsNullOrEmpty(text)) return;

        int textSize = Bounds.Width / 4;

        using SKPaint pen = new();
        pen.Style = SKPaintStyle.Fill;
        pen.Color = colour;
        pen.StrokeWidth = 1;
        pen.IsAntialias = true;
        pen.TextAlign = SKTextAlign.Center;
        pen.TextSize = textSize;

        Canvas.DrawCenteredText(Bounds, text, textSize, pen);
    }
}