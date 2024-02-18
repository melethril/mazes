namespace Mazes.Core;

public class Dimensions(int width, int height)
{
    public int Width { get; } = width;
    public int Height { get; } = height;

    public static Dimensions Screen1280X1024 => new(1280, 1024);
    public static Dimensions Screen1920X1080 => new(1920, 1080);
    public static Dimensions A4Portrait => new(210, 297);
    public static Dimensions A4Landscape => A4Portrait.FlipOrientation();

    public static implicit operator Dimensions(ValueTuple<int, int> tuple)
    {
        return new(tuple.Item1, tuple.Item2);
    }

    public Dimensions Scale(float scale)
    {
        return new((int)(Width * scale), (int)(Height * scale));
    }

    public Dimensions FlipOrientation()
    {
        return new(Height, Width);
    }
}