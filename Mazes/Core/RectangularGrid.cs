namespace Mazes.Core;

public abstract class RectangularGrid : Grid
{
    protected RectangularGrid(int rowCount, int columnCount, ICell[][] grid) : base(rowCount, columnCount, grid)
    {
        ConfigureCells();
    }

    protected static ICell[][] PrepareGrid(int numRows, int numColumns)
    {
        var rows = new ICell[numRows][];

        for (int i = 0; i < numRows; i++)
        {
            var row = new ICell[numColumns];

            for (int j = 0; j < numColumns; j++)
            {
                row[j] = new RectangularCell(i, j, isPathable: true);
            }

            rows[i] = row;
        }

        return rows;
    }

    private void ConfigureCells()
    {
        foreach (var cell in this.AllCells.Cast<RectangularCell>())
        {
            var (rowIndex, colIndex) = (cell.RowIndex, cell.ColumnIndex);

            if (rowIndex == 0 || colIndex == 0 || rowIndex == RowCount - 1 || colIndex == ColumnCount - 1)
                cell.IsOnOuterEdge = true;

            TryGetCellAt(rowIndex - 1, colIndex, out var north);
            TryGetCellAt(rowIndex + 1, colIndex, out var south);
            TryGetCellAt(rowIndex, colIndex + 1, out var east);
            TryGetCellAt(rowIndex, colIndex - 1, out var west);

            if (cell.IsPathable)
                SetupPathableCell(cell, north, south, east, west);
            else
                SetupNonPathableCell(cell, north, south, east, west);

        }
    }

    private static void SetupPathableCell(RectangularCell cell, RectangularCell? north, RectangularCell? south, RectangularCell? east, RectangularCell? west)
    {
        cell.North = north?.IsPathable == true ? north : null;
        cell.South = south?.IsPathable == true ? south : null;
        cell.East = east?.IsPathable == true ? east : null;
        cell.West = west?.IsPathable == true ? west : null;
    }

    private static void SetupNonPathableCell(RectangularCell cell, Cell? north, Cell? south, Cell? east, Cell? west)
    {
        cell.North = north;
        cell.South = south;
        cell.East = east;
        cell.West = west;
        const bool bidirectional = false;

        if (cell.North?.IsPathable == false)
            cell.Link(cell.North, bidirectional);
        if (cell.South?.IsPathable == false)
            cell.Link(cell.South, bidirectional);
        if (cell.East?.IsPathable == false)
            cell.Link(cell.East, bidirectional);
        if (cell.West?.IsPathable == false)
            cell.Link(cell.West, bidirectional);
    }

    // ReSharper disable once UnusedMethodReturnValue.Local
    private bool TryGetCellAt(int row, int column, out RectangularCell? cell)
    {
        cell = IsInBounds(row, column) ? GetCell(row, column) as RectangularCell : null;

        return cell != null;
    }

}