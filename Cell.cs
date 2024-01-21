namespace Mazes
{
    public class Cell : ICell
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public bool IsPathable { get; }

        public ICell? North { get; set; }
        public ICell? South { get; set; }
        public ICell? East { get; set; }
        public ICell? West { get; set; }

        public bool HasNorthEdge => North == null;
        public bool HasWestEdge => West == null;
        public bool HasEastEdge => East == null;
        public bool HasSouthEdge => South == null;
        public bool IsOnEdge => new ICell?[] { North, East, South, West }.Any(d => d == null);
        public bool IsOnOuterEdge { get; set; }
        public bool IsDeadEnd => links.Count == 1;
        public bool IsVoid  => Attributes.Any(a => a.Type == CellAttributeType.IsVoid);
        public IList<CellAttribute> Attributes { get; } = [];

        private readonly Dictionary<ICell, bool> links = [];

        public Cell(int row, int column, bool isPathable = true, IList<CellAttribute>? attributes = null)
        {
            RowIndex = row;
            ColumnIndex = column;
            IsPathable = isPathable;

            if (attributes != null && attributes.Any())
            {
                foreach( var attr in attributes)
                {
                    this.Attributes.Add(attr);
                }
            }
        }

        public IEnumerable<ICell> Links => links.Keys;
        public bool IsLinked(ICell? cell) => cell != null && links.ContainsKey(cell);

        public ICell Link(ICell cell, bool bidirectional = true)
        {
            links[cell] = true;
            if (bidirectional) cell.Link(this, false);

            return this;
        }

        public ICell Unlink(ICell cell, bool bidirectional = true)
        {
            links.Remove(cell, out _);
            if (bidirectional) cell.Unlink(this, false);

            return this;
        }

        public IEnumerable<ICell> Neighbours
        {
            get
            {
                return new[] { North, South, East, West }
                    .Where(o => o is not null)
                    .Cast<ICell>();
            }
        }
    }
}