namespace Mazes.Core;

public class MaskedGrid : Grid
{
    private readonly Mask mask;

    internal MaskedGrid(Mask mask) : base(mask.Rows, mask.Columns, PrepareGrid(mask))
    {
        this.mask = mask;
    }

    private static ICell[][] PrepareGrid(Mask mask)
    {
        int numRows = mask.Rows;
        int numColumns = mask.Columns;
        var voidAttribute = new [] {new CellAttribute(CellAttributeType.IsVoid)};
           
        var rows = new ICell[numRows][];

        for (int i = 0; i < numRows; i++)
        {
            var row = new ICell[numColumns];

            for (int j = 0; j < numColumns; j++)
            {
                var isPathable = mask[i, j];
                row[j] = new Cell(i, j, isPathable, isPathable ? null : voidAttribute);
            }

            rows[i] = row;
        }

        return rows;
    }

    public override ICell GetRandomCell(Random random)
    {
        (int row, int col) = mask.GetRandomLocation(random);

        return this[row, col];
    }

    public override int Size => mask.Count();
}