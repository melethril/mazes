using Mazes.Core;
using Mazes.Renderers.Bitmap.CellAttributeRenderers;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap.RenderingContexts;

public sealed class CellRenderingContext<T>(MazeStyles styles, SKCanvas canvas, SKRectI cellBounds, T cell, SKRectI? contentBounds) where T : ICell
{
    public MazeStyles Styles { get; } = styles;
    public SKCanvas Canvas { get; } = canvas;
    public SKRectI CellBounds { get; } = cellBounds;
    public SKRectI ContentBounds { get; } = contentBounds ?? cellBounds;
    public T Cell { get; } = cell;

    public CellRenderingContext<T> WithContentBounds(SKRectI contentBounds)
    {
        return new CellRenderingContext<T>(Styles, Canvas, CellBounds, Cell, contentBounds);
    }

    public CellAttributeRenderingContext ForAttribute(CellAttribute attribute, ICellAttributeRenderer renderer)
    {
        var style = Styles.Cells.GetStylesForAttribute(renderer);
        return new CellAttributeRenderingContext(style, Canvas, CellBounds, Cell, ContentBounds, attribute);
    }
}