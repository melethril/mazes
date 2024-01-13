namespace Mazes.Renderers.Bitmap
{
    using SkiaSharp;

    public class ImageRenderer
    {
        private readonly PageRenderer pageRenderer = new();
        private readonly CellRenderer cellRenderer = new();

        public SKSurface Render(Maze maze, Dimensions imageSize, int margin = 20)
        {
            var pageBounds = CreatePageRect(imageSize, margin);
            int cellSize = CalculateCellSize(maze, pageBounds, numPaddingCells: 1);
            var mazeBounds = CreateMazeRect(maze, pageBounds, cellSize);

            var surface = CreateSurface(imageSize);
            var canvas = surface.Canvas;

            pageRenderer.RenderPage(pageBounds, canvas);

            RenderMaze(canvas, maze, mazeBounds, cellSize);

            return surface;
        }

        private void RenderMaze(SKCanvas canvas, Maze maze, SKRectI mazeBounds, int cellSize)
        {
            var cellsAndBounds = maze.Cells
                .Select(cell => (cell, bounds: GetBoundsForCell(mazeBounds, cellSize, cell)))
                .ToArray();

            foreach (var (cell, bounds) in cellsAndBounds)
            {
                cellRenderer.RenderBackgrounds(canvas, bounds, cell);
            }

            foreach (var (cell, bounds) in cellsAndBounds)
            {
                cellRenderer.RenderForegrounds(canvas, bounds, cell);
            }
        }

        private static SKRectI GetBoundsForCell(SKRectI mazeBounds, int cellSize, Cell cell)
        {
            return new(
                left: mazeBounds.Left + (cell.Column * cellSize),
                top: mazeBounds.Top + (cell.Row * cellSize),
                right: mazeBounds.Left + ((cell.Column + 1) * cellSize),
                bottom: mazeBounds.Top + ((cell.Row + 1) * cellSize)
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
            int mazeWidth = cellSize * maze.Columns;
            int mazeHeight = cellSize * maze.Rows;

            int mazeLeft = page.Left + (page.Width / 2) - (mazeWidth / 2);
            int mazeTop = page.Top + (page.Height / 2) - (mazeHeight / 2);

            return new SKRectI(mazeLeft, mazeTop, mazeLeft + mazeWidth, mazeTop + mazeHeight);
        }

        private static int CalculateCellSize(Maze maze, SKRectI page, int numPaddingCells)
        {
            int cellWidth = page.Width / (maze.Columns + (numPaddingCells * 2));
            int cellHeight = page.Height / (maze.Rows + (numPaddingCells * 2));
            int cellSize = Math.Min(cellHeight, cellWidth);

            return cellSize;
        }
    }
}