namespace Mazes.Renderers.Bitmap
{
    using Mazes.Core;
    using SkiaSharp;

    public class ImageRenderer(MazeStyles styles, RendererRegistry rendererRegistry)
    {
        private readonly MazeStyles styles = styles;
        private readonly RendererRegistry rendererRegistry = rendererRegistry;

        public SKSurface Render(Maze maze, Dimensions imageSize)
        {
            var pageBounds = CreatePageRect(imageSize, styles.Page.Margin);
            int cellSize = CalculateCellSize(maze, pageBounds, styles.Page.NumPaddingCells);
            var mazeBounds = CreateMazeRect(maze, pageBounds, cellSize);

            var surface = CreateSurface(imageSize);
            var canvas = surface.Canvas;

            RenderPage(pageBounds, canvas, styles.Page.Margin);
            RenderMaze(canvas, maze, mazeBounds, cellSize);

            return surface;
        }

        public void RenderPage(SKRectI pageBounds, SKCanvas canvas, int margin)
        {
            using SKPaint pageFill = new() 
            { 
                IsStroke = false,
                Color = SKColor.Parse(styles.Page.BackgroundColour),
            };

            canvas.Clear(CellAttributeExtensions.ParseColour(styles.Image.BackgroundColour));
            canvas.DrawRect(pageBounds, pageFill);
   
            if (margin > 0)
            {
                using SKPaint pageOutline = new()
                { 
                    IsStroke = true,
                    StrokeWidth = 2,
                    Color = SKColor.Parse(styles.Page.OutlineColour)
                };

                canvas.DrawRect(pageBounds, pageOutline);
            }
        }

        private void RenderMaze(SKCanvas canvas, Maze maze, SKRectI mazeBounds, int cellSize)
        {
            var cellsAndBounds = maze.AllCells
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

        private static SKRectI CreateMazeRect(Maze maze, SKRectI page, int cellSize)
        {
            int mazeWidth = cellSize * maze.ColumnCount;
            int mazeHeight = cellSize * maze.RowCount;

            int mazeLeft = page.Left + (page.Width / 2) - (mazeWidth / 2);
            int mazeTop = page.Top + (page.Height / 2) - (mazeHeight / 2);

            return new SKRectI(mazeLeft, mazeTop, mazeLeft + mazeWidth, mazeTop + mazeHeight);
        }

        private static int CalculateCellSize(Maze maze, SKRectI page, int numPaddingCells)
        {
            int cellWidth = page.Width / (maze.ColumnCount + (numPaddingCells * 2));
            int cellHeight = page.Height / (maze.RowCount + (numPaddingCells * 2));
            int cellSize = Math.Min(cellHeight, cellWidth);

            return cellSize;
        }
    }
}