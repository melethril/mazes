using SkiaSharp;

namespace Mazes.Renderers.Bitmap.RenderingContexts;

public sealed class PageRenderingContext(SKCanvas canvas, SKRectI contentBounds)
{
    public SKCanvas Canvas { get; } = canvas;
    public SKRectI Bounds { get; } = contentBounds;
}