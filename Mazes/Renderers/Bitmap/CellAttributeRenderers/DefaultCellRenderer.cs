namespace Mazes.Renderers.Bitmap.CellAttributeRenderers
{
    internal class DefaultCellRenderer : ICellAttributeRenderer
    {
        public int Order => -1;

        public void Render(CellAttributeRenderingContext context)
        {
            if (!context.Cell.IsVoid)
            {
                context.RenderBackground(context.Style.GetBackgroundColour());
            }
        }
    }
}