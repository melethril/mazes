namespace Mazes.Core;

public class PolarCell(int row, int column, bool isPathable = true, IList<CellAttribute>? attributes = null)
    : Cell(row, column, isPathable, attributes)
{
    public ICell? Clockwise { get; set; }
    public ICell? AntiClockwise { get; set; }
    public ICell? Inward { get; set; }

    public IList<Cell> Outward { get; } = [];

    public override bool IsOnEdge => 
        new[] { AntiClockwise, Clockwise, Inward }
        .Concat(Outward)
        .Any(c => c == null);

    public override bool IsOnOuterEdge
    {
        get => !Outward.Any();
        set => throw new InvalidOperationException();
    }

    public override IEnumerable<ICell> Neighbours 
    {
        get
        {
            return new[] { Clockwise, AntiClockwise, Inward }
                .Concat(Outward)
                .Where(cell => cell is not null)
                .Cast<ICell>();
        }
    }

    public bool HasAntiClockwiseEdge => AntiClockwise == null;
    public bool HasClockwiseEdge => Clockwise == null;
    public bool HasInwardEdge => Inward == null;
    public bool HasOutwardEdge => !Outward.Any();
}
