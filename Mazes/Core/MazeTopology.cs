namespace Mazes.Core;

public class MazeTopology(TopologyType type)
{
    public TopologyType Type { get; set; } = type;

    public static readonly MazeTopology Rectangular = new(TopologyType.Rectangular);
    public static readonly MazeTopology Polar = new(TopologyType.Polar);
}
