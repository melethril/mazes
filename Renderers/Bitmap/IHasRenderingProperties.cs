namespace Mazes.Renderers.Bitmap
{
    public interface IHasRenderingProperties
    {
        public IReadOnlyList<RenderingProperty> Properties => [];
    }
}