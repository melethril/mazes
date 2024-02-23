using SkiaSharp;

namespace Mazes.Renderers;

public class CircularBounds(Point centre, int radius) : IBounds
{
    public int Width => radius * 2;
    public int Height => radius * 2;
    
    public Point Centre => centre;

    public int Left => centre.X - radius;
    public int Right => centre.X + radius;
    public int Top => centre.Y - radius;
    public int Bottom => centre.Y + radius;

    public SKPath ToSKPath()
    {
        var path = new SKPath();
        path.MoveTo(centre);
        path.AddCircle(centre.X, centre.Y, radius);
        path.Close();
        
        return path;
    }
}
