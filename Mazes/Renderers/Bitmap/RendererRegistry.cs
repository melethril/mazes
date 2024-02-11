using Mazes.Core;
using Mazes.Renderers.Bitmap.CellAttributeRenderers;

namespace Mazes.Renderers.Bitmap;

public class RendererRegistry
{
    private readonly Dictionary<CellAttributeType, Lazy<ICellAttributeRenderer>> list = new()
    {
        { CellAttributeType.Default, new (() => new DefaultCellRenderer()) },
        { CellAttributeType.Index, new (() => new IndexRenderer()) },
        { CellAttributeType.Distance, new (() => new DistanceRenderer()) },
        { CellAttributeType.Path, new (() => new BreadcrumbRenderer()) },
        { CellAttributeType.Intensity, new (() => new IntensityRenderer()) },
        { CellAttributeType.IsStartCell, new (() => new StartCellRenderer()) },
        { CellAttributeType.IsTargetCell, new (() => new TargetCellRenderer()) },
        { CellAttributeType.HasGridLines, new (() => new GridLineRenderer()) },
        { CellAttributeType.IsVoid, new (() => new VoidCellRenderer()) },
    };

    public void AddCustomRenderer(CellAttributeType type, Func<ICellAttributeRenderer> rendererFunc)
    {
        if (list.TryGetValue(type, out var existing))
            throw new ArgumentException($"CellAttributeType {type} already has a renderer registered: {existing.GetType().Name}");

        list.Add(type, new(rendererFunc));
    }

    public bool HasRenderer(CellAttributeType type)
    {
        return list.ContainsKey(type);
    }

    public ICellAttributeRenderer GetRenderer(CellAttributeType type)
    {
        if (!list.TryGetValue(type, out var renderer)) 
            throw new InvalidOperationException($"No renderer for type {type}");

        return renderer.Value;
    }

    public IEnumerable<(CellAttribute attr, ICellAttributeRenderer renderer)> GetRenderers(IEnumerable<CellAttribute> cellAttributes)
    {
        CellAttribute defaultCellAttribute = new(CellAttributeType.Default);

        var attributes = new [] { defaultCellAttribute }
            .Concat(cellAttributes);

        var cellRenderers = attributes
            .Where(attr => HasRenderer(attr.Type))
            .Select(attr => (attr, renderer: GetRenderer(attr.Type)))
            .OrderBy(o => o.renderer.Order);

        return cellRenderers;
    }

}