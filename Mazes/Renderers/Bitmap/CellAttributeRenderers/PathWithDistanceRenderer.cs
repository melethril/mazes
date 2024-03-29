using Mazes.Renderers.Bitmap.RenderingContexts;

namespace Mazes.Renderers.Bitmap.CellAttributeRenderers;

internal class PathWithDistanceRenderer : ICellAttributeRenderer
{
    public int Order => 0;

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