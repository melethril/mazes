using SkiaSharp;

namespace Mazes.Renderers;

public class ArcBounds : IBounds
{
    private readonly int innerRadius;
    private readonly int outerRadius;

    public ArcBounds(
        double thetaClockwise,
        double thetaAntiClockwise,
        int innerRadius,
        int outerRadius,
        Point centre)
    {
        this.innerRadius = innerRadius;
        this.outerRadius = outerRadius;
        
        var innerAntiClockwise = GetPoint(centre, innerRadius, thetaAntiClockwise);
        var outerAntiClockwise = GetPoint(centre, outerRadius, thetaAntiClockwise);
        var innerClockwise = GetPoint(centre, innerRadius, thetaClockwise);
        var outerClockwise = GetPoint(centre, outerRadius, thetaClockwise);
        
        Segments = [
            new BoundarySegment(outerAntiClockwise, outerClockwise, BoundaryType.Arc(outerRadius, isOuter: true)),
            new BoundarySegment(outerClockwise, innerClockwise, BoundaryType.Line),
            new BoundarySegment(innerClockwise, innerAntiClockwise, BoundaryType.Arc(innerRadius, isOuter: false)),
            new BoundarySegment(innerAntiClockwise, outerAntiClockwise, BoundaryType.Line)
        ];
        
        Centre = GetPoint(centre, innerRadius + ((outerRadius - innerRadius) / 2), thetaAntiClockwise + ((thetaClockwise - thetaAntiClockwise) / 2));
    }
    
    private static Point GetPoint(Point centre, int radius, double theta)
    {
        int x = centre.X + (int)(radius * Math.Cos(theta));
        int y = centre.Y + (int)(radius * Math.Sin(theta));

        return new Point(x, y);
    }

    public BoundarySegment[] Segments { get; }

    public int Width => Segments.Select(s => Math.Abs(s.Start.X - s.End.X)).Max();
    public int Height => outerRadius - innerRadius;
    public Point Centre { get; }

    public int Left => Segments.Select(s => Math.Min(s.Start.X, s.End.X)).Min();
    public int Right => Segments.Select(s => Math.Max(s.Start.X, s.End.X)).Max();
    public int Top => Segments.Select(s => Math.Min(s.Start.Y, s.End.Y)).Min();
    public int Bottom => Segments.Select(s => Math.Max(s.Start.Y, s.End.Y)).Max();

    public SKPath ToSKPath()
    {
        var path = new SKPath();

        var start = Segments.First().Start;
        path.MoveTo(start);

        foreach (var segment in Segments)
        {
            var radii = new SKPoint(segment.Type.Radius, segment.Type.Radius);
            var sweep = segment.Type.IsOuter
                ? SKPathDirection.Clockwise
                : SKPathDirection.CounterClockwise;

            path.ArcTo(radii, 0, SKPathArcSize.Small, sweep, segment.End);
        }

        path.Close();
        
        return path;
    }
}
