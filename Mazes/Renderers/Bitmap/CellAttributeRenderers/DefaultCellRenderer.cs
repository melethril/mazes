using Mazes.Renderers.Bitmap.RenderingContexts;

namespace Mazes.Renderers.Bitmap.CellAttributeRenderers;

internal class DefaultCellRenderer : ICellAttributeRenderer
{
    public int Order => -1;

    public void Render(CellAttributeRenderingContext context)
    {
        if (!context.Cell.IsVoid)
        {
            context.Canvas.RenderBackground(context.Style.GetBackgroundColour(), context.ContentBounds);
        }
    }
}