namespace Mazes.Core;

internal class RectangularCell(int row, int column, bool isPathable = true, IList<CellAttribute>? attributes = null)
    : Cell(row, column, isPathable, attributes)
{
    public ICell? North { get; set; }
    public ICell? South { get; set; }
    public ICell? East { get; set; }
    public ICell? West { get; set; }

    public bool HasNorthEdge => North == null;
    public bool HasWestEdge => West == null;
    public bool HasEastEdge => East == null;
    public bool HasSouthEdge => South == null;
    
    public override bool IsOnEdge => new[] { North, East, South, West }.Any(d => d == null);
    
    public override IEnumerable<ICell> Neighbours
    {
        get
        {
            return new[] { North, South, East, West }
                .Where(o => o is not null)
                .Cast<ICell>();
        }
    }
}