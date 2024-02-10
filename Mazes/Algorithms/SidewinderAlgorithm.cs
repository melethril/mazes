using Mazes.Core;
using Mazes.Utils;

namespace Mazes.Algorithms;

public class SidewinderAlgorithm : IMazeAlgorithm
{
    public string Name => "Sidewinder";

    public Grid Apply(Grid grid, Random random)
    {
        foreach (var row in grid.EachRow())
        {
            var run = new List<ICell>();

            foreach (var cell in row.Where(c => c.IsPathable))
            {
                run.Add(cell);
                    
                bool atEasternBoundary = cell.East == null;
                bool atNorthernBoundary = cell.North == null;
                bool shouldEndRun = atEasternBoundary || !atNorthernBoundary && random.Next(2) == 0;

                if (shouldEndRun)
                {
                    ICell? member = random.Sample(run);
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