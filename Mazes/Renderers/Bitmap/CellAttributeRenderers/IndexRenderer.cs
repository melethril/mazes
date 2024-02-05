namespace Mazes.Renderers.Bitmap
{
    internal class IndexRenderer : ICellAttributeRenderer
    {
        public int Order => 0;

        public void Render(CellAttributeRenderingContext context)
        {
            string? text = context.Attribute?.GetValueAs<string>();
            if (text == null) return;

            var fgColour = context.Style.GetForegroundColour();

            context.RenderText(text, fgColour);
        }
    }
}
