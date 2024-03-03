namespace Mazes.Core;

public class MazeTopology(TopologyType type)
{
    public TopologyType Type { get; } = type;

    public static readonly MazeTopology Rectangular = new(TopologyType.Rectangular);
    public static readonly MazeTopology Polar = new(TopologyType.Polar);
    public static readonly MazeTopology Sigma = new(TopologyType.Sigma);
    public static readonly MazeTopology Triangular = new(TopologyType.Triangular);

    public static MazeTopology Parse(string? name)
    {
        return name switch
        {
            "rect" => Rectangular,
            "circ" => Polar,
            "polar" => Polar,
            "hex" => Sigma,
            "sigma" => Sigma,
            "tri" => Triangular,
            "triangle" => Triangular,

            _ => throw new ArgumentException("Invalid topology type")
        };
    }
}
