using Mazes.Renderers.Bitmap.RenderingContexts;

namespace Mazes.Renderers.Bitmap.CellAttributeRenderers;

internal class StartCellRenderer : ICellAttributeRenderer
{
    public int Order => 3;

    public void Render(CellAttributeRenderingContext context)
    {
        string? text = context.Attribute.GetValueAs<string>();
        if (text == null) return;
            
        var fgColour = context.Style.GetForegroundColour();
        var bgColour = context.Style.GetBackgroundColour();

        context.Canvas.RenderBackground(bgColour, context.ContentBounds);
        context.Canvas.RenderText(text, fgColour, context.Bounds);
    }
}