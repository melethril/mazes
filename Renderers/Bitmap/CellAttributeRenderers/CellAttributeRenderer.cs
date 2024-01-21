namespace Mazes.Renderers.Bitmap
{
    public class CellAttributeRenderer<T>(int order, Action<CellAttributeRenderingContext>? render) 
        : ICellAttributeRenderer
    {
        public int Order => order;

        public void Render(CellAttributeRenderingContext context)
        {
            if (render == null) return;

            render(context);
        }
    }

    public class CellAttributeRenderer(int order, Action<CellAttributeRenderingContext>? render) 
        : CellAttributeRenderer<object>(order, render)
    {
    }
}