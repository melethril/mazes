using Mazes.Core;

namespace Mazes.Algorithms;

public class BinaryTreeAlgorithm : IMazeAlgorithm
{
    public string Name => "Binary Tree";

    public Grid Apply(Grid grid, Random random)
    {
        foreach (var cell in grid.PathableCells)
        {
            var neighbours = new[] { cell.East, cell.North }
                .Where(c => c is not null)
                .Cast<ICell>()
                .ToArray();

            if (neighbours.Any())
            {
                var neighbour = neighbours[random.Next(neighbours.Length)];

                cell.Link(neighbour);
            }
        }

        return grid;
    }
}