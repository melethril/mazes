using Mazes;
using Mazes.Renderers.Bitmap;
using Mazes.Utils;

const string basePath = @"C:\Users\Martin\Downloads\Mazes\";

var gridSize = (rows: 20, cols: 20);
var imageSize = Dimensions.A4Landscape.Scale(7f);

// Generate a maze
var maze = Maze.Random(gridSize, seed: 12345);

// Calculate longest path
var path = Distances.CalculateLongestPath(maze);

// Decorate the maze
maze.ShowPath(path.path, path.start, path.end);
//maze.ShowDistanceHeatMap(path.distances);

// Draw the maze
string filePath = FileUtils.GenerateFullPath(basePath, maze);
maze.RenderAsPng(filePath, imageSize);

Console.WriteLine($"Complete @ {filePath}");
