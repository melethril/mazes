using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public class CellRenderer
    {
        private readonly RendererRegistry renderers = new();

        public void Render(CellRenderingContext context)
        {
            var cell = context.Cell;
            var canvas = context.Canvas;
            var cellBounds = context.CellBounds;

            using SKPaint wallBrush = new()
            {
                IsStroke = true,
                Color = SKColor.FromHsv(35, 14, 47),
                StrokeWidth = 6,
                IsAntialias = true,
            };

            // Build and draw the cell walls
            var walls = BuildCellWalls(cell, cellBounds);
            canvas.DrawPath(walls, wallBrush);

            // Define a smaller inner rectangle to account for wall widths
            int wallWidth = (int)wallBrush.StrokeWidth / 2;
            SKRectI contentBounds = new(
                cellBounds.Left + (cell.IsLinked(cell.West) ? 0 : wallWidth),
                cellBounds.Top + (cell.IsLinked(cell.North) ? 0 : wallWidth),
                cellBounds.Right - (cell.IsLinked(cell.East) ? 0 : wallWidth),
                cellBounds.Bottom - (cell.IsLinked(cell.South) ? 0 : wallWidth)
            );

            var cellContext = new CellRenderingContext(canvas, cellBounds, cell, contentBounds);

            // Render default cell appearance
            var defaultRenderer = new DefaultCellRenderer();
            defaultRenderer.Render(cellContext);

            // For each attribute that the cell has, render it using the renderer for that attribute
            var attributesAndRenderers = GetAttributesWithRenderers(cell);
            foreach (var (attribute, renderer) in attributesAndRenderers)
            {
                renderer.Render(cellContext.ForAttribute(attribute));
            }
        }

        private static SKPath BuildCellWalls(ICell cell, SKRectI rect)
        {
            SKPath path = new();

            if (cell.HasNorthEdge && !cell.IsVoid)
            {
                path.MoveTo(rect.Left, rect.Top);
                path.LineTo(rect.Right, rect.Top);
            }

            if (cell.HasWestEdge && !cell.IsVoid)
            {
                path.MoveTo(rect.Left, rect.Top);
                path.LineTo(rect.Left, rect.Bottom);
            }

            if ((cell.HasEastEdge || !cell.IsLinked(cell.East)) && !cell.IsVoid)
            {
                path.MoveTo(rect.Right, rect.Top);
                path.LineTo(rect.Right, rect.Bottom);
            }

            if ((cell.HasSouthEdge || !cell.IsLinked(cell.South)) && !cell.IsVoid)
            {
                path.MoveTo(rect.Left, rect.Bottom);
                path.LineTo(rect.Right, rect.Bottom);
            }

            return path;
        }

        private IEnumerable<(CellAttribute attr, ICellAttributeRenderer renderer)> GetAttributesWithRenderers(ICell cell)
        {
            return cell.Attributes
                .Select(attr =>
                {
                    return renderers.List.TryGetValue(attr.Type, out var rendererFunc)
                        ? (attr, renderer: rendererFunc())
                        : (attr, renderer: null);
                })
                .Where(o => o.renderer is not null)
                .Cast<(CellAttribute attr, ICellAttributeRenderer renderer)>()
                .OrderBy(o => o.renderer.Order);
        }

    }
}