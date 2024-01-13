namespace Mazes.Renderers.Bitmap
{
    public interface ICellAttributeRenderer
    {
        public int Order { get; }
        public void Render(RenderContext context);
    }
}