using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public class CellRenderer
    {
        private readonly RendererRegistry renderers = new();
        
        private static readonly Lazy<SKPaint> backgroundBrush = new(() => new()
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.AntiqueWhite,
        });

        private static readonly Lazy<SKPaint> wallBrush = new(() => new()
        {
            IsStroke = true,
            Color = SKColors.Black,
            StrokeWidth = 6,
            IsAntialias = true,
        });

        public void RenderBackgrounds(SKCanvas canvas, SKRectI cellBounds, Cell cell)
        {
            // Fill whole cell with background brush
            canvas.DrawRect(cellBounds, backgroundBrush.Value);
        }

        public void RenderForegrounds(SKCanvas canvas, SKRectI cellBounds, Cell cell)
        {
            // Build and draw the cell walls
            var walls = BuildCellWalls(cell, cellBounds);
            canvas.DrawPath(walls, wallBrush.Value);

            // Define a smaller inner rectangle to account for wall widths
            int wallWidth = (int)wallBrush.Value.StrokeWidth / 2;
            SKRectI contentBounds = new(
                cellBounds.Left + (cell.IsLinked(cell.West) ? 0 : wallWidth), 
                cellBounds.Top + (cell.IsLinked(cell.North) ? 0 : wallWidth), 
                cellBounds.Right - (cell.IsLinked(cell.East) ? 0 : wallWidth), 
                cellBounds.Bottom - (cell.IsLinked(cell.South) ? 0 : wallWidth)
            );

            // For each attribute that the cell has, render it using the renderer for that attribute
            var attributesAndRenderers = GetAttributesWithRenderers(cell);
            foreach (var (attribute, renderer) in attributesAndRenderers)
            {
                renderer?.Render(new RenderContext(canvas, cellBounds, contentBounds, cell, attribute));
            }
        }

        private static SKPath BuildCellWalls(Cell cell, SKRectI rect)
        {
            SKPath path = new();

            if (cell.IsAtNorthEdge)
            {
                path.MoveTo(rect.Left, rect.Top);
                path.LineTo(rect.Right, rect.Top);
            }

            if (cell.IsAtWestEdge)
            {
                path.MoveTo(rect.Left, rect.Top);
                path.LineTo(rect.Left, rect.Bottom);
            }

            if (cell.IsAtEastEdge || !cell.IsLinked(cell.East))
            {
                path.MoveTo(rect.Right, rect.Top);
                path.LineTo(rect.Right, rect.Bottom);
            }

            if ( cell.IsAtSouthEdge || !cell.IsLinked(cell.South))
            {
                path.MoveTo(rect.Left, rect.Bottom);
                path.LineTo(rect.Right, rect.Bottom);
            }

            return path;
        }

        private IEnumerable<(CellAttribute attr, ICellAttributeRenderer renderer)> GetAttributesWithRenderers(Cell cell)
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