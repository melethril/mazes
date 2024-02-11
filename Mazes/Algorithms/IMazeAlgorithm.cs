using Mazes.Core;

namespace Mazes.Algorithms;

public interface IMazeAlgorithm
{
    string Name { get; }
    IGrid Apply(IGrid grid, Random random);
}