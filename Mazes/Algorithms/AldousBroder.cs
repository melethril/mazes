using Mazes.Core;
using Mazes.Utils;

namespace Mazes.Algorithms;

public class AldousBroderAlgorithm : IMazeAlgorithm
{
    public string Name => "Aldous-Broder";

    public Grid Apply(Grid grid, Random random)
    {
        var cell = grid.GetRandomCell(random);
        int unvisited = grid.Size - 1;

        while (unvisited > 0)
        {
            ICell[] neighbours = cell.Neighbours.ToArray();
            ICell neighbour = random.Sample(neighbours)!;

            if (neighbour.Links.Any() == false)
            {
                cell.Link(neighbour);
                unvisited -= 1;
            }

            cell = neighbour;
        }

        return grid;
    }
}