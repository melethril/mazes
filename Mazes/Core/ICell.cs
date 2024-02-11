namespace Mazes.Core;

public interface ICell
{
    int RowIndex { get; }
    int ColumnIndex { get; }

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