namespace Mazes.Utils
{
    public class FileUtils
    {
        public static string GenerateFullPath(string basePath, Maze maze, IMazeAlgorithm? algorithm = null, bool withTimestamp = false)
        {
            return Path.Combine(basePath, GenerateFilename(maze, algorithm, withTimestamp));
        }

        public static string GenerateFilename(Maze maze, IMazeAlgorithm? algorithm = null, bool withTimestamp = false)
        {
            string dims = $"_{maze.RowCount}x{maze.ColumnCount}";
            string? algName = algorithm != null ? $"_{algorithm.Name.ToLower().Replace(" ", "_")}" : string.Empty;
            string? timestamp = withTimestamp ? $"_{DateTime.UtcNow:yyyyMMdd-HHmmss}.png" : string.Empty;

            return $"maze{dims}{algName}{timestamp}.png";
        }
    }
}