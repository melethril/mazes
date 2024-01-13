namespace Mazes
{
    public class BinaryTreeAlgorithm : IMazeAlgorithm
    {
        public string Name => "Binary Tree";

        public Maze Apply(Maze maze, Random random)
        {
            foreach (var cell in maze.Cells)
            {
                Cell[] neighbours = new[] { cell.East, cell.North }
                    .Where(cell => cell is not null)
                    .Cast<Cell>()
                    .ToArray();

                if (neighbours.Any())
                {
                    var neighbour = neighbours[random.Next(neighbours.Length)];

                    cell.Link(neighbour);
                }
            }

            return maze;
        }
    }
}