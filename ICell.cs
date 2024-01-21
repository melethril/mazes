namespace Mazes
{
    public interface ICell
    {
        int RowIndex { get; }
        int ColumnIndex { get; }
        ICell? North { get; set; }
        ICell? South { get; set; }
        ICell? East { get; set; }
        ICell? West { get; set; }

        bool HasNorthEdge { get; }
        bool HasWestEdge { get; }
        bool HasEastEdge { get; }
        bool HasSouthEdge { get; }
        bool IsOnEdge { get; }
        bool IsOnOuterEdge { get; set; }
        bool IsDeadEnd { get; }

        IList<CellAttribute> Attributes { get; }
        public IEnumerable<ICell> Links { get; }
        public bool IsLinked(ICell? cell);
        public ICell Link(ICell cell, bool bidirectional = true);
        public ICell Unlink(ICell cell, bool bidirectional = true);
        public IEnumerable<ICell> Neighbours { get; }
        bool IsPathable { get; }
        bool IsVoid { get; }
    }
}