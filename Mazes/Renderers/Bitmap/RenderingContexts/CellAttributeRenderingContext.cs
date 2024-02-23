using Mazes.Core;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap.RenderingContexts;

public sealed class CellAttributeRenderingContext(CellStyle style, SKCanvas canvas, IBounds cellBounds, ICell cell, IBounds contentBounds, CellAttribute attribute)
{
    public CellStyle Style { get; } = style;
    public SKCanvas Canvas { get; } = canvas;
    public IBounds Bounds { get; } = cellBounds;
    public IBounds ContentBounds { get; } = contentBounds;
    public ICell Cell { get; } = cell;
    public CellAttribute Attribute { get; } = attribute;
}