using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public sealed class CellRenderingContext(SKCanvas canvas, SKRectI cellBounds, ICell cell, SKRectI? contentBounds)
    {
        public SKCanvas Canvas { get; } = canvas;
        public SKRectI CellBounds { get; } = cellBounds;
        public SKRectI ContentBounds { get; } = contentBounds ?? cellBounds;
        public ICell Cell { get; } = cell;

        public CellRenderingContext WithContentBounds(SKRectI contentBounds)
        {
            return new CellRenderingContext(Canvas, CellBounds, Cell, contentBounds);
        }

        public CellAttributeRenderingContext ForAttribute(CellAttribute attribute)
        {
            return new CellAttributeRenderingContext(Canvas, CellBounds, Cell, ContentBounds, attribute);
        }

    }


}