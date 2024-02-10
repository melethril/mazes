using Mazes.Algorithms;
using Mazes.Core;

namespace Mazes.Utils;

public static class FileUtils
{
    public static string GenerateFullPath(string basePath, IGrid grid, IMazeAlgorithm? algorithm = null, bool withTimestamp = false)
    {
        return Path.Combine(basePath, GenerateFilename(grid, algorithm, withTimestamp));
    }

    public static string GenerateFilename(IGrid grid, IMazeAlgorithm? algorithm = null, bool withTimestamp = false)
    {
        string dims = $"_{grid.RowCount}x{grid.ColumnCount}";
        string algName = algorithm != null ? $"_{algorithm.Name.ToLower().Replace(" ", "_")}" : string.Empty;
        string timestamp = withTimestamp ? $"_{DateTime.UtcNow:yyyyMMdd-HHmmss}.png" : string.Empty;

        return $"maze{dims}{algName}{timestamp}.png";
    }
}