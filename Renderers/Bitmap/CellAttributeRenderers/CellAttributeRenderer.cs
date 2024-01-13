namespace Mazes.Renderers.Bitmap
{
    public class CellAttributeRenderer<T>(int order, Action<RenderContext>? render) 
        : ICellAttributeRenderer
    {
        public int Order => order;

        public void Render(RenderContext context)
        {
            if (render == null) return;

            render(context);
        }
    }

    public class CellAttributeRenderer(int order, Action<RenderContext>? render) 
        : CellAttributeRenderer<object>(order, render)
    {
    }
}