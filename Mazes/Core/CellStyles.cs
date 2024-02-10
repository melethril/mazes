using Mazes.Renderers.Bitmap.CellAttributeRenderers;

namespace Mazes.Core;

public class CellStyles
{
    public List<CellStyle> Attributes { get; set; } = [];

    public CellStyle GetStylesForAttribute(ICellAttributeRenderer renderer)
    {
        string rendererName = renderer.GetType().Name.Replace("Renderer", string.Empty);
        rendererName = char.ToLowerInvariant(rendererName[0]) + rendererName[1..];

        return Attributes.SingleOrDefault(attr => attr.Name == rendererName) ?? new(rendererName);
    }
}