namespace Mazes.Renderers;

public record BoundaryType
{
    private BoundaryType(BoundaryShape Shape, float? Radius, bool IsOuter)
    {
        this.Shape = Shape;
        this.IsOuter = IsOuter;
        this.Radius = Radius ?? 0;
    }

    public static BoundaryType Line => new(BoundaryShape.Line, null, false);
    public static BoundaryType Arc(int radius, bool isOuter) => new(BoundaryShape.Arc, radius, isOuter);
    public static BoundaryType Circle(int radius) => new(BoundaryShape.Circle, radius, IsOuter: true);
    
    public BoundaryShape Shape { get; }
    public bool IsOuter { get; }
    public float Radius { get; }

    public void Deconstruct(out BoundaryShape shape, out float? size)
    {
        shape = this.Shape;
        size = this.Radius;
    }
}
