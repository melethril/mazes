using SkiaSharp;

namespace Mazes.Renderers;

public interface IBounds
{
    int Width { get; }
    int Height { get; }
    
    Point Centre { get; }

    int Left { get; }
    int Right { get; }
    int Top { get; }
    int Bottom { get; }

    SKPath ToSKPath();
}
