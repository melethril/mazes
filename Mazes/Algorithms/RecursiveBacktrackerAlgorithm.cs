using Mazes.Core;
using Mazes.Utils;

namespace Mazes.Algorithms;

public class RecursiveBacktrackerAlgorithm : IMazeAlgorithm
{
    public string Name => "Recursive Backtracker";

    public IGrid Apply(IGrid grid, Random random)
    {
        var startAt = grid.GetRandomCell(random);
        var stack = new Stack<ICell>();
        stack.Push(startAt);

        while (stack.Any())
        {
            var current = stack.Peek();
            var neighbours = current.Neighbours.Where(c => !c.Links.Any()).ToArray();
                
            if (!neighbours.Any())
            {
                stack.Pop();
            }
            else
            {
                var neighbour = random.Sample(neighbours)!;
                current.Link(neighbour);
                stack.Push(neighbour);
            }
        }

        return grid;
    }
}