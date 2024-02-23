using Mazes.Core;
using Mazes.Renderers.Bitmap.RenderingContexts;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap;

internal class PolarGridRenderer(MazeStyles styles, RendererRegistry rendererRegistry) : IGridRenderer
{
    public void Render(SKCanvas canvas, IGrid polarGrid, SKRectI pageBounds)
    {
        var grid = polarGrid as PolarGrid ?? throw new ArgumentException("grid is not a PolarGrid");

        var centre = new Point(
            X: pageBounds.Left + (pageBounds.Width / 2),
            Y: pageBounds.Top + (pageBounds.Height / 2)
        );

        int cellSize = Math.Min(pageBounds.Width, pageBounds.Height) /
                       ((grid.RowCount * 2) + (styles.Page.NumPaddingCells * 2));

        int wallWidth = styles.Page.WallWidth;
        using var wallBrush = GetWallBrush(wallWidth);

        foreach (var cell in grid.AllCells.Cast<PolarCell>())
        {
            var cellBounds = GetCellBounds(grid, cell, cellSize, centre);
            var cellContext = new CellRenderingContext<PolarCell>(styles, canvas, cellBounds, cell, cellBounds);
            
            // For each attribute that the cell has, render it using the renderer for that attribute
            var attributesAndRenderers = rendererRegistry.GetRenderers(cell.Attributes);
            foreach (var (attribute, renderer) in attributesAndRenderers)
            {
                renderer.Render(cellContext.ForAttribute(attribute, renderer));
            }
            
            var walls = BuildCellWalls(cell, cellBounds, wallWidth);
            canvas.DrawPath(walls, wallBrush);
        }
        
        canvas.DrawCircle(centre.X, centre.Y, (grid.RowCount) * cellSize, wallBrush);
    }

    private SKPaint GetWallBrush(int wallWidth)
    {
        SKPaint wallBrush = new();
        wallBrush.Style = SKPaintStyle.Stroke;
        wallBrush.Color = SKColor.Parse(styles.Page.WallColour);
        wallBrush.StrokeWidth = wallWidth;
        wallBrush.StrokeCap = SKStrokeCap.Butt;
        wallBrush.IsAntialias = true;
        
        return wallBrush;
    }

    private static IBounds GetCellBounds(PolarGrid grid, PolarCell cell, int cellSize, Point centre)
    {
        double theta = 2 * Math.PI / grid.GetRow(cell.RowIndex).Length;
        double thetaAntiClockwise = (cell.ColumnIndex) * theta;
        double thetaClockwise = (cell.ColumnIndex + 1) * theta;

        int innerRadius = (cell.RowIndex) * cellSize;
        int outerRadius = (cell.RowIndex + 1) * cellSize;

        return cell.RowIndex == 0
            ? new CircularBounds(new(centre.X, centre.Y), outerRadius)
            : new ArcBounds(thetaClockwise, thetaAntiClockwise, innerRadius, outerRadius, centre);
    }

    private static SKPath BuildCellWalls(PolarCell cell, IBounds bounds, int wallWidth)
    {
        SKPath walls = new();

        if (bounds is ArcBounds arc)
        {
            if ((cell.HasInwardEdge || !cell.IsLinked(cell.Inward)) && !cell.IsVoid)
            {
                var segment = arc.Segments[2];
                walls.MoveTo(segment.Start);

                float radius = segment.Type.Radius;
                walls.ArcTo(new SKPoint(radius, radius),
                    0, SKPathArcSize.Small, SKPathDirection.CounterClockwise, segment.End
                );
            }

            if ((cell.HasAntiClockwiseEdge || !cell.IsLinked(cell.AntiClockwise)) && !cell.IsVoid)
            {
                var segment = arc.Segments[3];
                var adjustedStart = GetAdjustedPoint(cell, segment.Start, wallWidth);
                var adjustedEnd = GetAdjustedPoint(cell, segment.End, wallWidth);
                
                walls.MoveTo(adjustedStart);
                walls.LineTo(adjustedEnd);
            }
        }

        return walls;
    }

    private static Point GetAdjustedPoint(PolarCell cell, Point point, int wallWidth)
    {
        var adjustedStart = cell.ColumnIndex == 0 
            ? point with { Y = point.Y + (wallWidth / 2) }
            : point;

        return adjustedStart;
    }
}
