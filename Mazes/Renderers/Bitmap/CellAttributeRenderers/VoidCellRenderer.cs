using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class VoidCellRenderer : ICellAttributeRenderer
    {
        public int Order => 0;

        public void Render(CellAttributeRenderingContext context)
        {
            context.RenderBackground(SKColors.Transparent);
        }
    }    
}