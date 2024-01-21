namespace Mazes.Renderers.Bitmap
{
    public interface IHasRenderingProperties
    {
        public IReadOnlyDictionary<string, string> Properties { get; }
    }
}