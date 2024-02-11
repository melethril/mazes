using Mazes.Core;
using Mazes.Renderers.Bitmap.RenderingContexts;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap;

public class RectangularGridRenderer(MazeStyles styles, RendererRegistry rendererRegistry) : IGridRenderer
{
    public void Render(SKCanvas canvas, IGrid grid, SKRectI pageBounds)
    {
        var rectGrid = grid as RectangularGrid ?? throw new ArgumentException($"grid must be a {nameof(RectangularGrid)}");
        int cellSize = CalculateCellSize(rectGrid, pageBounds, styles.Page.NumPaddingCells);
        var mazeBounds = CreateMazeRect(rectGrid, pageBounds, cellSize);
        
        var cellsAndBounds = grid.AllCells
            .Cast<RectangularCell>()
            .Select(cell => (cell, bounds: GetBoundsForCell(mazeBounds, cellSize, cell)))
            .ToArray();

        foreach (var (cell, bounds) in cellsAndBounds)
        {
            RenderCell(new CellRenderingContext<RectangularCell>(styles, canvas, bounds, cell, contentBounds: null));
        }
    }
    
    private static SKRectI CreateMazeRect(Grid grid, SKRectI page, int cellSize)
    {
        int mazeWidth = cellSize * grid.ColumnCount;
        int mazeHeight = cellSize * grid.RowCount;

        int mazeLeft = page.Left + (page.Width / 2) - (mazeWidth / 2);
        int mazeTop = page.Top + (page.Height / 2) - (mazeHeight / 2);

        return new SKRectI(mazeLeft, mazeTop, mazeLeft + mazeWidth, mazeTop + mazeHeight);
    }

    private static int CalculateCellSize(Grid grid, SKRectI page, int numPaddingCells)
    {
        int cellWidth = page.Width / (grid.ColumnCount + (numPaddingCells * 2));
        int cellHeight = page.Height / (grid.RowCount + (numPaddingCells * 2));
        int cellSize = Math.Min(cellHeight, cellWidth);

        return cellSize;
    }
    
    private static SKRectI GetBoundsForCell(SKRectI mazeBounds, int cellSize, ICell cell)
    {
        return new(
            left: mazeBounds.Left + (cell.ColumnIndex * cellSize),
            top: mazeBounds.Top + (cell.RowIndex * cellSize),
            right: mazeBounds.Left + ((cell.ColumnIndex + 1) * cellSize),
            bottom: mazeBounds.Top + ((cell.RowIndex + 1) * cellSize)
        );
    }

    private void RenderCell(CellRenderingContext<RectangularCell> context)
    {
        var cell = context.Cell;
        var canvas = context.Canvas;
        var cellBounds = context.CellBounds;

        using SKPaint wallBrush = new();
        wallBrush.IsStroke = true;
        wallBrush.Color = SKColor.Parse(context.Styles.Page.WallColour);
        wallBrush.StrokeWidth = context.Styles.Page.WallWidth;
        wallBrush.IsAntialias = true;

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

        var cellContext = new CellRenderingContext<Cell>(context.Styles, canvas, cellBounds, cell, contentBounds);

        // For each attribute that the cell has, render it using the renderer for that attribute
        var attributesAndRenderers = rendererRegistry.GetRenderers(cell.Attributes);
        foreach (var (attribute, renderer) in attributesAndRenderers)
        {
            var attrContext = cellContext.ForAttribute(attribute, renderer);
                
            renderer.Render(attrContext);
        }
    }

    private static SKPath BuildCellWalls(RectangularCell cell, SKRectI rect)
    {
        SKPath path = new();

        if (cell is { HasNorthEdge: true, IsVoid: false })
        {
            path.MoveTo(rect.Left, rect.Top);
            path.LineTo(rect.Right, rect.Top);
        }

        if (cell is { HasWestEdge: true, IsVoid: false })
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

}