using Mazes.Core;
using Mazes.Utils;

namespace Mazes.Algorithms;

public class SidewinderAlgorithm : IMazeAlgorithm
{
    public string Name => "Sidewinder";

    public IGrid Apply(IGrid grid, Random random)
    {
        var rectGrid = grid as RectangularGrid ?? throw new ArgumentException($"{Name} algorithmn can only be applied to rectangular grids");
        
        foreach (var row in rectGrid.AllRows)
        {
            var run = new List<ICell>();

            foreach (var cell in row.Where(c => c.IsPathable).Cast<RectangularCell>())
            {
                run.Add(cell);
                    
                bool atEasternBoundary = cell.East == null;
                bool atNorthernBoundary = cell.North == null;
                bool shouldEndRun = atEasternBoundary || !atNorthernBoundary && random.Next(2) == 0;

                if (shouldEndRun)
                {
                    var member = random.Sample(run) as RectangularCell;
                    if (member?.North is not null) 
                        member.Link(member.North);

                    run.Clear();
                }
                else
                {
                    if (cell.East != null) 
                        cell.Link(cell.East);
                }
            }
        }

        return grid;
    }
}