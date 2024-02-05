namespace Mazes
{
    public interface IMazeAlgorithm
    {
        string Name { get; }
        Maze Apply(Maze maze, Random random);
    }
}