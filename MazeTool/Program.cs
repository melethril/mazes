using Mazes.Algorithms;
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
            var imageSize = Dimensions.Screen1280X1024;

            // Generate a maze
            var descriptor = GetMazeDescriptor(options);
            var grid = MazeBuilder.Build(descriptor);

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

    private static MazeDescriptor GetMazeDescriptor(MazeOptions options)
    {
        Mask? mask = null;
        if (options.MaskFilePath != null)
        {
            using var file = new FileStream(options.MaskFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            mask = Mask.LoadFrom(file).ScaleUp(2, 2);
        }

        var topology = options.Polar ? MazeTopology.Polar : MazeTopology.Rectangular;

        return MazeDescriptor.Specific(
            new RecursiveBacktrackerAlgorithm(),
            topology, (options.Rows, options.Columns), 1234, mask
        );
    }

    private static async Task OutputMaze(MazeOptions options, Dimensions imageSize, IGrid grid)
    {
        string filePath = FileUtils.GenerateFullPath(options.OutputFilePath!, grid);

        var styles = options.StylesFilePath != null
            ? await MazeStyles.Load(options.StylesFilePath)
            : new MazeStyles();

        grid.RenderAsPng(filePath, imageSize, styles);
        //maze.RenderAsText(Console.Out);

        Console.WriteLine($"Complete @ {filePath}");
    }
}