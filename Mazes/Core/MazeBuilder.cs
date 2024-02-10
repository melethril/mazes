using Mazes.Algorithms;

namespace Mazes.Core;

public class MazeBuilder
{
    private static IMazeAlgorithm DefaultAlgorithm => new RecursiveBacktrackerAlgorithm();
    private static (int, int) DefaultSize => (10, 10);

    private (int rows, int columns)? size;
    private IMazeAlgorithm? algorithm;
    private Mask? mask;

    private MazeBuilder()
    {
    }

    public static MazeBuilder WithSize((int rows, int columns) size)
    {
        return new MazeBuilder { size = size };
    }

    public static MazeBuilder WithMask(Mask mask)
    {
        return new MazeBuilder { mask = mask };
    }

    public MazeBuilder WithAlgorithm(IMazeAlgorithm mazeAlgorithm)
    {
        this.algorithm = mazeAlgorithm;
        return this;
    }
        
    public IGrid BuildRandomMaze()
    {
        return BuildMaze(seed: null);
    }

    public IGrid BuildMazeFromSeed(int seed)
    {
        return BuildMaze(seed);
    }

    private Grid BuildMaze(int? seed)
    {
        var alg = algorithm ?? DefaultAlgorithm;
        (int rows, int columns) = size ?? DefaultSize;
        var random = seed.HasValue ? new Random(seed.Value) : new Random();

        Grid grid = (mask == null)
            ? new SimpleGrid(rows, columns)
            : new MaskedGrid(mask);

        return alg.Apply(grid, random);
    }

}