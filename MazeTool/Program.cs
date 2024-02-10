using Mazes.Core;
using Mazes.Utils;

namespace MazeTool;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            var options = MazeOptions.Parse(args);
            var imageSize = Dimensions.Screen1280x1024;

            // Generate a maze
            IGrid grid = BuildMaze(options);

            // Calculate the longest path through the maze
            var path = grid.CalculateLongestPath(startOnEdge: true, endOnEdge: true);

            // Decorate the maze
            grid.ShowStartAndEnd(path.start, path.end);
            grid.ShowPath(path.path, path.start, path.end);
            //grid.ShowDistances(path.distances);
            //grid.ShowDistanceHeatMap(path.distances);

            // Draw and output the maze
            await OutputMaze(options, imageSize, grid);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static IGrid BuildMaze(MazeOptions options)
    {
        IGrid grid;
        if (options.MaskFilePath != null)
        {
            using var file = new FileStream(options.MaskFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var mask = Mask.LoadFrom(file).ScaleUp(2, 2);

            grid = MazeBuilder
                .WithMask(mask)
                .BuildRandomMaze();
        }
        else
        {
            grid = MazeBuilder
                .WithSize((40, 40))
                .BuildRandomMaze();
        }

        return grid;
    }

    private static async Task OutputMaze(MazeOptions options, Dimensions imageSize, IGrid grid)
    {
        string filePath = FileUtils.GenerateFullPath(options.OutputFilePath!, grid);

        MazeStyles styles = options.StylesFilePath != null
            ? await MazeStyles.Load(options.StylesFilePath)
            : new MazeStyles();

        grid.RenderAsPng(filePath, imageSize, styles);
        //maze.RenderAsText(Console.Out);

        Console.WriteLine($"Complete @ {filePath}");
    }
}