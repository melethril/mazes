using Mazes;
using Mazes.Core;
using Mazes.Renderers.Bitmap;
using Mazes.Utils;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            var options = MazeOptions.Parse(args);
            var imageSize = Dimensions.Screen1280x1024;

            // Generate a maze
            Maze maze = BuildMaze(options);

            // Calculate the longest path through the maze
            var path = maze.CalculateLongestPath(startOnEdge: true, endOnEdge: true);

            // Decorate the maze
            //maze.ShowIndexes();
            maze.ShowStartAndEnd(path.start, path.end);
            maze.ShowPath(path.path, path.start, path.end);
            //maze.ShowDistanceHeatMap(path.distances);

            // Draw and output the maze
            await OutputMaze(options, imageSize, maze);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static Maze BuildMaze(MazeOptions options)
    {
        Maze maze;
        if (options.MaskFilePath != null)
        {
            using var file = new FileStream(options.MaskFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var mask = Mask.LoadFrom(file).ScaleUp(2, 2);

            maze = MazeBuilder
                .WithMask(mask)
                .BuildRandomMaze();
        }
        else
        {
            maze = MazeBuilder
                .WithSize((40, 40))
                .BuildRandomMaze();
        }

        return maze;
    }

    private static async Task OutputMaze(MazeOptions options, Dimensions imageSize, Maze maze)
    {
        string filePath = FileUtils.GenerateFullPath(options.OutputFilePath!, maze);

        MazeStyles styles = options.StylesFilePath != null
            ? await MazeStyles.Load(options.StylesFilePath)
            : new MazeStyles();

        maze.RenderAsPng(filePath, imageSize, styles);
        //maze.RenderAsText(Console.Out);

        Console.WriteLine($"Complete @ {filePath}");
    }
}