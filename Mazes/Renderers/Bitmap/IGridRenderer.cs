using Mazes.Core;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap;

public interface IGridRenderer
{
    void Render(SKCanvas canvas, IGrid grid, SKRectI pageBounds);
}