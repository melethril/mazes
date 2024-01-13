using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public sealed class RenderContext(SKCanvas canvas, SKRectI cellBounds, SKRectI contentBounds, Cell cell, CellAttribute attribute)
    {
        public SKCanvas Canvas => canvas;
        public SKRectI CellBounds = cellBounds;
        public SKRectI ContentBounds = contentBounds;
        public Cell Cell = cell;
        public CellAttribute Attribute = attribute;
    }
}