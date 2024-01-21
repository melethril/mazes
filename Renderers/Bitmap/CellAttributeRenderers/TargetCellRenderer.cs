using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class TargetCellRenderer : ICellAttributeRenderer
    {
        public int Order => 3;

        public void Render(CellAttributeRenderingContext context)
        {
            ICellAttributeRenderer[] renderers = [
                new BackgroundRenderer(SKColors.LightPink),
                new CellTextRenderer()
            ];

            foreach (var renderer in renderers)
            {
                renderer.Render(context);
            }
        }
    }
}