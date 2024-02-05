namespace Mazes.Renderers.Bitmap
{
    internal class PathWithDistanceRenderer : ICellAttributeRenderer
    {
        public int Order => 0;

        public void Render(CellAttributeRenderingContext context)
        {
            string? text = context.Attribute?.GetValueAs<string>();
            if (text == null) return;

            var fgColour = context.Style.GetForegroundColour();
            var bgColour = context.Style.GetBackgroundColour();

            context.RenderBackground(bgColour);
            context.RenderText(text, fgColour);
        }
    }
}
