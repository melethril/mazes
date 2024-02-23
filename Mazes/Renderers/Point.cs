using SkiaSharp;

namespace Mazes.Renderers;

public record Point(int X, int Y)
{
    public static implicit operator SKPoint(Point p) => new(p.X, p.Y);
}