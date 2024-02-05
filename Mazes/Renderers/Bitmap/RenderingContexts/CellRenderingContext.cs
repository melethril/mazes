using Mazes.Core;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public sealed class CellRenderingContext(MazeStyles styles, SKCanvas canvas, SKRectI cellBounds, ICell cell, SKRectI? contentBounds)
    {
        public MazeStyles Styles { get; } = styles;
        public SKCanvas Canvas { get; } = canvas;
        public SKRectI CellBounds { get; } = cellBounds;
        public SKRectI ContentBounds { get; } = contentBounds ?? cellBounds;
        public ICell Cell { get; } = cell;

        public CellRenderingContext WithContentBounds(SKRectI contentBounds)
        {
            return new CellRenderingContext(Styles, Canvas, CellBounds, Cell, contentBounds);
        }

        public CellAttributeRenderingContext ForAttribute(CellAttribute attribute, ICellAttributeRenderer renderer)
        {
            var style = Styles.Cells.GetStylesForAttribute(renderer);
            return new CellAttributeRenderingContext(style, Canvas, CellBounds, Cell, ContentBounds, attribute);
        }
    }


}