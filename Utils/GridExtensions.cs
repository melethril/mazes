using Mazes.Renderers.Bitmap;
using Mazes.Renderers.Text;
using SkiaSharp;

namespace Mazes
{
    public static class MazeExtensions
    {
        public static string RenderAsText(this Maze maze)
        {
            var renderer = new TextRenderer();
            string text = renderer.Render(maze);

            Console.WriteLine(text);

            return text;
        }

        public static void RenderAsPng(this Maze maze, string fullPath, Dimensions imageSize)
        {
            var renderer = new ImageRenderer();

            using SKSurface surface = renderer.Render(maze, imageSize, 20);
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
            int n = 0;
            foreach (var cell in maze.Cells)
            {
                string text = n++.ToString();
                cell.Attributes.Add(new(CellAttributeType.Index, text));
            }
        }

        public static void ShowDistances(this Maze maze, Distances path)
        {
            foreach (var cell in maze.Cells)
            {
                string text = path[cell]?.ToString() ?? string.Empty;
                cell.Attributes.Add(new(CellAttributeType.Distance, text));
            }
        }

        public static void ShowGridLines(this Maze maze)
        {
            foreach (var cell in maze.Cells)
            {
                cell.Attributes.Add(new(CellAttributeType.HasGridLines));
            }
        }

        public static void ShowPath(this Maze maze, Distances path, Cell start, Cell end)
        {
            foreach (var cell in maze.Cells.Except([start, end]))
            {
                string? text = path[cell]?.ToString();
                if (text != null)
                {
                    cell.Attributes.Add(new(CellAttributeType.Path, text));
                }
            }

            start.Attributes.Add(new CellAttribute(CellAttributeType.IsStartCell, "START"));
            end.Attributes.Add(new CellAttribute(CellAttributeType.IsTargetCell, "END"));
        }

        public static void ShowDistanceHeatMap(this Maze maze, Distances distances)
        {
            foreach (var cell in maze.Cells)
            {
                var intensity = GetIntensity(distances, cell);
                cell.Attributes.Add(new(CellAttributeType.Intensity, intensity));
            }
        }

        private static float? GetIntensity(Distances distances, Cell cell)
        {
            int? distance = distances[cell];
            int maxDistance = distances.Max().distance;
            if (distance == null) return null;

            float intensity = (float)(maxDistance - distance.Value) / maxDistance;

            return intensity;
        }

    }
}
