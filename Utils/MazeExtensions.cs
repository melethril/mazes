using Mazes.Renderers.Bitmap;
using Mazes.Renderers.Text;
using SkiaSharp;

namespace Mazes
{
    public static class MazeExtensions
    {
        public static void RenderAsText(this Maze maze, TextWriter writer)
        {
            var renderer = new TextRenderer();
            renderer.Render(maze, writer);
        }

        public static void RenderAsPng(this Maze maze, string fullPath, Dimensions imageSize, int margin = 0, int numPaddingCells = 1)
        {
            var renderer = new ImageRenderer();

            using SKSurface surface = renderer.Render(maze, imageSize, margin, numPaddingCells);
            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode();
            using FileStream file = OpenFile(fullPath);

            file.Write(data.ToArray());
        }

        private static FileStream OpenFile(string fullPath)
        {
            FileStream file = File.Exists(fullPath)
                ? new FileStream(fullPath, FileMode.Truncate, FileAccess.Write)
                : new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write);

            return file;
        }

        public static void ShowIndexes(this Maze maze)
        {
            foreach (var cell in maze.PathableCells)
            {
                string text = $"{cell.RowIndex},{cell.ColumnIndex}";
                cell.Attributes.Add(new(CellAttributeType.Index, text));
            }
        }

        public static void ShowDistances(this Maze maze, Distances path)
        {
            foreach (var cell in maze.PathableCells)
            {
                string text = path[cell]?.ToString() ?? string.Empty;
                cell.Attributes.Add(new(CellAttributeType.Distance, text));
            }
        }

        public static void ShowGridLines(this Maze maze)
        {
            foreach (var cell in maze.PathableCells)
            {
                cell.Attributes.Add(new(CellAttributeType.HasGridLines));
            }
        }

        public static void ShowPath(this Maze maze, Distances path, ICell start, ICell end)
        {
            foreach (var cell in maze.PathableCells.Except([start, end]))
            {
                string? text = path[cell]?.ToString();
                if (text != null)
                {
                    cell.Attributes.Add(new(CellAttributeType.Path, text));
                }
            }

            maze.ShowStartAndEnd(start, end);
        }

        public static void ShowStartAndEnd(this Maze maze, ICell start, ICell end)
        {
            start.Attributes.Add(new CellAttribute(CellAttributeType.IsStartCell, "START"));
            end.Attributes.Add(new CellAttribute(CellAttributeType.IsTargetCell, "END"));
        }

        public static void ShowDistanceHeatMap(this Maze maze, Distances distances)
        {
            foreach (var cell in maze.PathableCells)
            {
                var intensity = GetIntensity(distances, cell);
                cell.Attributes.Add(new(CellAttributeType.Intensity, intensity));
            }
        }

        private static float? GetIntensity(Distances distances, ICell cell)
        {
            int? distance = distances[cell];
            int maxDistance = distances.Max(onEdge: false).distance;
            if (distance == null) return null;

            float intensity = (float)(maxDistance - distance.Value) / maxDistance;

            return intensity;
        }

        public static (ICell start, ICell end, Distances path, Distances distances) CalculateLongestPath(this Maze maze, bool startOnEdge, bool endOnEdge)
        {
            return Distances.CalculateLongestPath(maze, startOnEdge, endOnEdge);
        }

    }
}
