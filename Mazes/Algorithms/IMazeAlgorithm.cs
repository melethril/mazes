using Mazes.Core;

namespace Mazes.Algorithms;

public interface IMazeAlgorithm
{
    string Name { get; }
    Grid Apply(Grid grid, Random random);
}