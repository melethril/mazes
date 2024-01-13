using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class PathWithDistanceRenderer : ICellAttributeRenderer
    {
        public int Order => 0;

        public void Render(RenderContext context)
        {
            ICellAttributeRenderer[] renderers = [
                new BackgroundRenderer(SKColors.LightBlue),
                new CellTextRenderer()
            ];

            foreach (var renderer in renderers)
            {
                renderer.Render(context);
            }
        }
    }
}
