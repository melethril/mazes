using Mazes.Algorithms;

namespace Mazes.Core;

public class MazeDescriptor
{
    public IMazeAlgorithm Algorithm { get; }

    public MazeTopology Topology { get; }

    public (int rows, int cols) Size { get; }

    public int? Seed { get; }

    public Mask? Mask { get; }

    private MazeDescriptor(IMazeAlgorithm? algorithm, MazeTopology topology, (int rows, int cols) size, int? seed, Mask? mask)
    {
        Algorithm = algorithm ?? new RecursiveBacktrackerAlgorithm();
        Topology = topology;
        Size = size;
        Seed = seed;
        Mask = mask;
    }
   
    public static MazeDescriptor Specific(IMazeAlgorithm algorithm, MazeTopology topology, (int rows, int cols) size, int seed, Mask? mask)
    {
        return new MazeDescriptor(algorithm, topology, size, seed, mask);
    }
    
    public static MazeDescriptor Random(MazeTopology topology, (int rows, int cols) size, Mask? mask)
    {
        return new MazeDescriptor(null, topology, size, seed: null, mask);
    }


}