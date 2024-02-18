using Mazes.Core;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap;

internal class PolarGridRenderer(MazeStyles styles, RendererRegistry rendererRegistry) : IGridRenderer
{
    public void Render(SKCanvas canvas, IGrid polarGrid, SKRectI pageBounds)
    {
        var grid = polarGrid as PolarGrid ?? throw new ArgumentException("grid is not a PolarGrid");

        var centre = new SKPointI(
            x: pageBounds.Left + (pageBounds.Width / 2),
            y: pageBounds.Top + (pageBounds.Height / 2)
        );

        int cellSize = Math.Min(pageBounds.Width, pageBounds.Height) /
                       ((grid.RowCount * 2) + (styles.Page.NumPaddingCells * 2));

        using SKPaint wallBrush = new();
        wallBrush.IsStroke = true;
        wallBrush.Color = SKColor.Parse(styles.Page.WallColour);
        wallBrush.StrokeWidth = styles.Page.WallWidth;
        wallBrush.IsAntialias = true;

        canvas.DrawCircle(centre.X, centre.Y, (grid.RowCount) * cellSize, wallBrush);

        foreach (var cell in grid.AllCells.Cast<PolarCell>())
        {
            double theta = 2 * Math.PI / grid.GetRow(cell.RowIndex).Length;
            double thetaAntiClockwise = (cell.ColumnIndex) * theta;
            double thetaClockwise = (cell.ColumnIndex + 1) * theta;

            int innerRadius = (cell.RowIndex) * cellSize;
            int outerRadius = (cell.RowIndex + 1) * cellSize;

            var a = GetPoint(centre, innerRadius, thetaAntiClockwise);
            var b = GetPoint(centre, outerRadius, thetaAntiClockwise);
            var c = GetPoint(centre, innerRadius, thetaClockwise);
            var d = GetPoint(centre, outerRadius, thetaClockwise);

            var walls = BuildCellWalls(cell, a, b, c, d, innerRadius, outerRadius);
            canvas.DrawPath(walls, wallBrush);
            
            // Define a smaller inner rectangle to account for wall widths
            // int wallWidth = (int)wallBrush.StrokeWidth / 2;
            // SKRectI contentBounds = new(
            //     cellBounds.Left + (cell.IsLinked(cell.West) ? 0 : wallWidth),
            //     cellBounds.Top + (cell.IsLinked(cell.North) ? 0 : wallWidth),
            //     cellBounds.Right - (cell.IsLinked(cell.East) ? 0 : wallWidth),
            //     cellBounds.Bottom - (cell.IsLinked(cell.South) ? 0 : wallWidth)
            // );
            //
            // var cellContext = new CellRenderingContext<Cell>(context.Styles, canvas, cellBounds, cell, contentBounds);
            //
            // // For each attribute that the cell has, render it using the renderer for that attribute
            // var attributesAndRenderers = rendererRegistry.GetRenderers(cell.Attributes);
            // foreach (var (attribute, renderer) in attributesAndRenderers)
            // {
            //     var attrContext = cellContext.ForAttribute(attribute, renderer);
            //     
            //     renderer.Render(attrContext);
            // }
        }
    }

    private static SKPath BuildCellWalls(
        PolarCell cell, SKPointI a, SKPointI b, SKPointI c, SKPointI d,
        int innerRadius, int outerRadius)
    {
        SKPath walls = new();

        if ((cell.HasOutwardEdge || !cell.Outward.Any(cell.IsLinked)) && !cell.IsVoid)
        {
            walls.MoveTo(b);
            walls.ArcTo(new SKPoint(outerRadius, outerRadius),
                0, SKPathArcSize.Small, SKPathDirection.Clockwise, d
            );
        }

        if ((cell.HasAntiClockwiseEdge || !cell.IsLinked(cell.AntiClockwise)) && !cell.IsVoid)
        {
            walls.MoveTo(a);
            walls.LineTo(b);
        }

        if ((cell.HasClockwiseEdge || !cell.IsLinked(cell.Clockwise)) && !cell.IsVoid)
        {
            walls.MoveTo(c);
            walls.LineTo(d);
        }

        if ((cell.HasInwardEdge || !cell.IsLinked(cell.Inward)) && !cell.IsVoid)
        {
            walls.MoveTo(a);
            walls.ArcTo(new SKPoint(innerRadius, innerRadius),
                0, SKPathArcSize.Small, SKPathDirection.Clockwise, c
            );
        }

        return walls;
    }

    private static SKPointI GetPoint(SKPointI centre, int radius, double theta)
    {
        int x = centre.X + (int)(radius * Math.Cos(theta));
        int y = centre.Y + (int)(radius * Math.Sin(theta));

        return new SKPointI(x, y);
    }
}