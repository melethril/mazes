using Mazes;
using Mazes.Renderers.Bitmap;
using Mazes.Utils;

string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
Console.WriteLine(userFolder);
string basePath = Path.Combine(userFolder, @"Downloads\Mazes");


var gridSize = (rows: 30, cols: 30);
var imageSize = Dimensions.A4Landscape.Scale(7f);

// Generate a maze
var maze = Maze.Random(gridSize, new RecursiveBacktrackerAlgorithm());//, seed: 12345);

// Calculate longest path
var path = Distances.CalculateLongestPath(maze);

// Decorate the maze
maze.ShowPath(path.path, path.start, path.end);
//maze.ShowDistanceHeatMap(path.distances);

// Draw the maze
string filePath = FileUtils.GenerateFullPath(basePath, maze);
maze.RenderAsPng(filePath, imageSize);

Console.WriteLine($"Complete @ {filePath}");
