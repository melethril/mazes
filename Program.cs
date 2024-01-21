using Mazes;
using Mazes.Renderers.Bitmap;
using Mazes.Utils;

string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
Console.WriteLine(userFolder);
string basePath = Path.Combine(userFolder, @"Downloads\Mazes");


//var gridSize = (rows: 50, cols: 50);
var imageSize = Dimensions.A4Landscape.Scale(7f);

// Create a mask
using var file = new FileStream(@".\fig8.mask", FileMode.Open, FileAccess.Read, FileShare.Read);
var mask = Mask.LoadFrom(file).ScaleUp(3, 5);

// Generate a maze
var maze = MazeBuilder
//    .WithSize(gridSize)
    .WithMask(mask)
    .BuildRandomMaze();

// Calculate longest path
var path = maze.CalculateLongestPath(startOnEdge: true, endOnEdge: true);

// Decorate the maze
//maze.ShowIndexes();
maze.ShowStartAndEnd(path.start, path.end);
maze.ShowPath(path.path, path.start, path.end);
//maze.ShowDistanceHeatMap(path.distances);

// Draw the maze
string filePath = FileUtils.GenerateFullPath(basePath, maze);
maze.RenderAsPng(filePath, imageSize, margin: 0, numPaddingCells: 2);
//maze.RenderAsText(Console.Out);

Console.WriteLine($"Complete @ {filePath}");
