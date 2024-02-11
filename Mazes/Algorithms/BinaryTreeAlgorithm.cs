using Mazes.Core;

namespace Mazes.Algorithms;

public class BinaryTreeAlgorithm : IMazeAlgorithm
{
    public string Name => "Binary Tree";

    public IGrid Apply(IGrid grid, Random random)
    {
        var rectGrid = grid as RectangularGrid ?? throw new ArgumentException($"{Name} algorithmn can only be applied to rectangular grids");
        
        foreach (var cell in rectGrid.PathableCells.Cast<RectangularCell>())
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