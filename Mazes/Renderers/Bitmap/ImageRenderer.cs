using Mazes.Renderers.Bitmap.RenderingContexts;

namespace Mazes.Renderers.Bitmap;

using Core;
using SkiaSharp;

public class ImageRenderer(MazeStyles styles, RendererRegistry rendererRegistry)
{
    public SKSurface Render(IGrid grid, Dimensions imageSize)
    {
        var pageBounds = CreatePageRect(imageSize, styles.Page.Margin);
        int cellSize = CalculateCellSize(grid, pageBounds, styles.Page.NumPaddingCells);
        var mazeBounds = CreateMazeRect(grid, pageBounds, cellSize);

        var surface = CreateSurface(imageSize);
        var canvas = surface.Canvas;

        RenderPage(pageBounds, canvas, styles.Page.Margin);
        RenderMaze(canvas, grid, mazeBounds, cellSize);

        return surface;
    }

    public void RenderPage(SKRectI pageBounds, SKCanvas canvas, int margin)
    {
        using SKPaint pageFill = new();
        pageFill.IsStroke = false;
        pageFill.Color = SKColor.Parse(styles.Page.BackgroundColour);

        canvas.Clear(CellAttributeExtensions.ParseColour(styles.Image.BackgroundColour));
        canvas.DrawRect(pageBounds, pageFill);
   
        if (margin > 0)
        {
            using SKPaint pageOutline = new();
            pageOutline.IsStroke = true;
            pageOutline.StrokeWidth = 2;
            pageOutline.Color = SKColor.Parse(styles.Page.OutlineColour);

            canvas.DrawRect(pageBounds, pageOutline);
        }
    }

    private void RenderMaze(SKCanvas canvas, IGrid grid, SKRectI mazeBounds, int cellSize)
    {
        var cellsAndBounds = grid.AllCells
            .Select(cell => (cell, bounds: GetBoundsForCell(mazeBounds, cellSize, cell)))
            .ToArray();

        CellRenderer cellRenderer = new(rendererRegistry);

        foreach (var (cell, bounds) in cellsAndBounds)
        {
            cellRenderer.Render(new CellRenderingContext(styles, canvas, bounds, cell, contentBounds: null));
        }
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

    private static SKSurface CreateSurface(Dimensions imageSize)
    {
        var imageInfo = new SKImageInfo(
            width: imageSize.Width,
            height: imageSize.Height,
            colorType: SKColorType.Rgba8888,
            alphaType: SKAlphaType.Premul
        );

        return SKSurface.Create(imageInfo);
    }

    private static SKRectI CreatePageRect(Dimensions imageSize, int margin)
    {
        return new SKRectI(margin, margin, imageSize.Width - margin, imageSize.Height - margin);
    }

    private static SKRectI CreateMazeRect(IGrid grid, SKRectI page, int cellSize)
    {
        int mazeWidth = cellSize * grid.ColumnCount;
        int mazeHeight = cellSize * grid.RowCount;

        int mazeLeft = page.Left + (page.Width / 2) - (mazeWidth / 2);
        int mazeTop = page.Top + (page.Height / 2) - (mazeHeight / 2);

        return new SKRectI(mazeLeft, mazeTop, mazeLeft + mazeWidth, mazeTop + mazeHeight);
    }

    private static int CalculateCellSize(IGrid grid, SKRectI page, int numPaddingCells)
    {
        int cellWidth = page.Width / (grid.ColumnCount + (numPaddingCells * 2));
        int cellHeight = page.Height / (grid.RowCount + (numPaddingCells * 2));
        int cellSize = Math.Min(cellHeight, cellWidth);

        return cellSize;
    }
}