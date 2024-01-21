using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public sealed class CellAttributeRenderingContext(SKCanvas canvas, SKRectI cellBounds, ICell cell, SKRectI contentBounds, CellAttribute attribute)
    {
        public SKCanvas Canvas { get; } = canvas;
        public SKRectI Bounds { get; } = cellBounds;
        public SKRectI ContentBounds { get; } = contentBounds;
        public ICell Cell { get; } = cell;
        public CellAttribute Attribute { get; } = attribute;
    }

}