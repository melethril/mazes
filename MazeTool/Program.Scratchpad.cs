using SkiaSharp;

namespace MazeTool;

internal static partial class Program
{
    /// <summary>
    /// For testing canvas drawing behaviour
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    private static void Scratchpad()
    {
        var imageInfo = new SKImageInfo(
            width: 800,
            height: 600,
            colorType: SKColorType.Rgba8888,
            alphaType: SKAlphaType.Premul
        );

        var surface = SKSurface.Create(imageInfo);
        var canvas = surface.Canvas;
        var rect = new SKRectI(100, 100, 200, 200);
        var rect2 = new SKRectI(220, 100, 320, 200);
        var rect3 = new SKRectI(340, 100, 440, 200);

        var paint = new SKPaint
        {
            StrokeWidth = 4,
        };

        paint.Style = SKPaintStyle.Fill;
        paint.Color = SKColors.LightCoral;
        canvas.DrawRect(rect, paint);

        paint.Style = SKPaintStyle.Fill;
        paint.Color = SKColors.LightCoral;
        canvas.DrawRect(rect3, paint);

        paint.Color = SKColors.Black;
        paint.Style = SKPaintStyle.Stroke;
        canvas.DrawRect(rect, paint);
        canvas.DrawRect(rect2, paint);


        using var image = surface.Snapshot();
        using var data = image.Encode();
        const string path = "C:/Users/Martin/Downloads/Mazes/test.png";
        using var file = File.Exists(path)
            ? new FileStream(path, FileMode.Truncate, FileAccess.Write)
            : new FileStream(path, FileMode.CreateNew, FileAccess.Write);

        file.Write(data.ToArray());
    }
}