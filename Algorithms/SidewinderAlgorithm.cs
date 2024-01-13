namespace Mazes
{
    public class SidewinderAlgorithm : IMazeAlgorithm
    {
        public string Name => "Sidewinder";

        public Maze Apply(Maze maze, Random random)
        {
            foreach (var row in maze.EachRow())
            {
                var run = new List<Cell>();

                foreach (var cell in row)
                {
                    run.Add(cell);
                    
                    bool atEasternBoundary = cell.East == null;
                    bool atNorthernBoundary = cell.North == null;
                    bool shouldEndRun = atEasternBoundary || !atNorthernBoundary && random.Next(2) == 0;

                    if (shouldEndRun)
                    {
                        Cell? member = random.Sample(run);
                        if (member?.North is not null) member.Link(member.North);
                        run.Clear();
                    }
                    else
                    {
                        if (cell.East != null) cell.Link(cell.East);
                    }
                }
            }

            return maze;
        }
    }
}