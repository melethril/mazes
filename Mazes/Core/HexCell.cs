namespace Mazes.Core;

public class HexCell(int row, int column, bool isPathable = true, IList<CellAttribute>? attributes = null)
    : Cell(row, column, isPathable, attributes)
{
    public ICell? North { get; set; }
    public ICell? NorthEast { get; set; }
    public ICell? SouthEast { get; set; }
    public ICell? South { get; set; }
    public ICell? SouthWest { get; set; }
    public ICell? NorthWest { get; set; }

    public override bool IsOnEdge => throw new NotImplementedException();

    public override IEnumerable<ICell> Neighbours =>
        new[] { North, NorthEast, SouthEast, South, SouthWest, NorthWest }
            .Where(c => c != null)
            .Cast<ICell>();
}
