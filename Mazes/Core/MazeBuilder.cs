namespace Mazes.Core;

public static class MazeBuilder
{
    public static IGrid Build(MazeDescriptor descriptor)
    {
        IGrid grid;
        if (descriptor.Topology.Type == TopologyType.Polar)
        {
            grid = new PolarGrid(descriptor.Size.rows);
        }
        else
        {
            grid = descriptor.Mask != null 
                ? new MaskedGrid(descriptor.Mask) 
                : new SimpleGrid(descriptor.Size.rows, descriptor.Size.cols);
        }

        var random = descriptor.Seed.HasValue 
            ? new Random(descriptor.Seed.Value) 
            : new Random();

        return descriptor.Algorithm.Apply(grid, random);
    }

}