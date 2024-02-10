using Mazes.Renderers.Bitmap.RenderingContexts;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap.CellAttributeRenderers;

internal class VoidCellRenderer : ICellAttributeRenderer
{
    public int Order => 0;

    public void Render(CellAttributeRenderingContext context)
    {
        context.RenderBackground(SKColors.Transparent);
    }
}