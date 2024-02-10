using Mazes.Renderers.Bitmap.RenderingContexts;

namespace Mazes.Renderers.Bitmap.CellAttributeRenderers;

public interface ICellAttributeRenderer
{
    public int Order { get; }
    public void Render(CellAttributeRenderingContext context);
}