namespace Mazes
{
    public class RecursiveBacktrackerAlgorithm : IMazeAlgorithm
    {
        public string Name => "Recursive Backtracker";

        public Maze Apply(Maze maze, Random random)
        {
            var startAt = maze.GetRandomCell(random)!;
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

            return maze;
        }
    }
}