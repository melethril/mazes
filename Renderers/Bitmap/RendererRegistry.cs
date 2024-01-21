namespace Mazes.Renderers.Bitmap
{
    public class RendererRegistry
    {
        private readonly Dictionary<CellAttributeType, Func<ICellAttributeRenderer>> list = new()
        {
            { CellAttributeType.Index, () => new CellTextRenderer() },
            { CellAttributeType.Distance, () => new CellTextRenderer() },
            { CellAttributeType.Path, () => new BreadcrumbRenderer() },
            { CellAttributeType.Intensity, () => new IntensityRenderer() },
            { CellAttributeType.IsStartCell, () => new StartCellRenderer() },
            { CellAttributeType.IsTargetCell, () => new TargetCellRenderer() },
            { CellAttributeType.HasGridLines, () => new GridLineRenderer() },
            { CellAttributeType.IsVoid, () => new VoidCellRenderer() },
        };

        public IReadOnlyDictionary<CellAttributeType, Func<ICellAttributeRenderer>> List => list;

        public void RegisterRenderer(CellAttributeType type, Func<ICellAttributeRenderer> rendererFunc)
        {
            if (list.TryGetValue(type, out var existing))
                throw new ArgumentException($"CellAttributeType {type} already has a renderer registered: {existing.GetType().Name}");

            list.Add(type, rendererFunc);
        }
    }
}
