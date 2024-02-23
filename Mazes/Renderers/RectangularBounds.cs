using SkiaSharp;

namespace Mazes.Renderers;

public class RectangularBounds(int left, int top, int right, int bottom) : IBounds
{
    private readonly BoundarySegment[] segments =
    [
        new BoundarySegment(new(left, top), new(right, top), BoundaryType.Line),
        new BoundarySegment(new(right, top), new(right, bottom), BoundaryType.Line),
        new BoundarySegment(new(right, bottom), new(left, bottom), BoundaryType.Line),
        new BoundarySegment(new(left, bottom), new(left, top), BoundaryType.Line)
    ];

    public int Width => right - left;
    public int Height => bottom - top;
    
    public Point Centre
    {
        get
        {
            int centreX = Left + (Width / 2);
            int centreY = Top + (Height / 2);
            return new(centreX, centreY);
        }
    }

    public int Left => left;
    public int Right => right;
    public int Top => top;
    public int Bottom => bottom;

    public SKPath ToSKPath()
    {
        var path = new SKPath();

        var a = segments[0].Start;
        path.MoveTo(a);

        foreach (var segment in segments)
        {
           path.LineTo(segment.End);
        }

        path.Close();
        
        return path;
    }
}
