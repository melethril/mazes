using Mazes.Core;
using Mazes.Renderers.Bitmap;
using Mazes.Renderers.Text;

namespace Mazes.Utils;

public static class MazeExtensions
{
    public static void RenderAsText(this Grid grid, TextWriter writer)
    {
        var renderer = new TextRenderer();
        renderer.Render(grid, writer);
    }

    public static void RenderAsPng(this IGrid grid, string fullPath, Dimensions imageSize, MazeStyles? styles = null)
    {
        var renderer = new ImageRenderer(styles ?? new MazeStyles(), new RendererRegistry());

        using var surface = renderer.Render(grid, imageSize);
        using var image = surface.Snapshot();
        using var data = image.Encode();
        using var file = OpenFile(fullPath);

        file.Write(data.ToArray());
    }

    private static FileStream OpenFile(string fullPath)
    {
        var file = File.Exists(fullPath)
            ? new FileStream(fullPath, FileMode.Truncate, FileAccess.Write)
            : new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write);

        return file;
    }

    public static void ShowIndexes(this IGrid grid)
    {
        foreach (var cell in grid.PathableCells)
        {
            string text = $"{cell.RowIndex},{cell.ColumnIndex}";
            cell.Attributes.Add(new(CellAttributeType.Index, text));
        }
    }

    public static void ShowDistances(this IGrid grid, Distances path)
    {
        foreach (var cell in grid.PathableCells)
        {
            string text = path[cell]?.ToString() ?? string.Empty;
            cell.Attributes.Add(new(CellAttributeType.Distance, text));
        }
    }

    public static void ShowGridLines(this IGrid grid)
    {
        foreach (var cell in grid.PathableCells)
        {
            cell.Attributes.Add(new(CellAttributeType.HasGridLines));
        }
    }

    public static void ShowPath(this IGrid grid, Distances path, ICell start, ICell end)
    {
        foreach (var cell in grid.PathableCells.Except([start, end]))
        {
            string? text = path[cell]?.ToString();
            if (text != null)
            {
                cell.Attributes.Add(new(CellAttributeType.Path, text));
            }
        }

        grid.ShowStartAndEnd(start, end);
    }

    public static void ShowStartAndEnd(this IGrid grid, ICell start, ICell end)
    {
        start.Attributes.Add(new CellAttribute(CellAttributeType.IsStartCell, "START"));
        end.Attributes.Add(new CellAttribute(CellAttributeType.IsTargetCell, "END"));
    }

    public static void ShowDistanceHeatMap(this IGrid grid, Distances distances)
    {
        foreach (var cell in grid.PathableCells)
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

    public static (ICell start, ICell end, Distances path, Distances distances) CalculateLongestPath(this IGrid grid, bool startOnEdge, bool endOnEdge)
    {
        return Distances.CalculateLongestPath(grid, startOnEdge, endOnEdge);
    }

}