using Mazes.Core;
using Mazes.Utils;

namespace Mazes.Algorithms;

public class HuntAndKill : IMazeAlgorithm
{
    public string Name => "Hunt-and-Kill";

    public IGrid Apply(IGrid grid, Random random)
    {
        var current = grid.GetRandomCell(random);

        while (current != null)
        {
            var unvisitedNeighbours = current.Neighbours.Where(n => !n.Links.Any()).ToArray();

            if (unvisitedNeighbours.Any())
            {
                ICell neighbour = random.Sample(unvisitedNeighbours)!;
                current.Link(neighbour);
                current = neighbour;
            }
            else
            {
                current = null;
                foreach (var cell in grid.PathableCells)
                {
                    var visitedNeighbours = cell.Neighbours.Where(n => n.Links.Any()).ToArray();
                    if (!cell.Links.Any() && visitedNeighbours.Any())
                    { 
                        current = cell;
                        var neighbour = random.Sample(visitedNeighbours)!;
                        current.Link(neighbour);
                        break;
                    }
                }
            }
        }

        return grid;
    }
}