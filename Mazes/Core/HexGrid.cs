namespace Mazes.Core;

public class HexGrid : Grid
{
    public HexGrid(int rowCount, int columnCount) : base(rowCount, columnCount, PrepareGrid(rowCount, columnCount))
    {
        ConfigureCells();
    }

    private static ICell[][] PrepareGrid(int rowCount, int columnCount)
    {
        var rows = new ICell[rowCount][];

        for (int i = 0; i < rowCount; i++)
        {
            var row = new ICell[columnCount];

            for (int j = 0; j < columnCount; j++)
            {
                row[j] = new HexCell(i, j, isPathable: true);
            }

            rows[i] = row;
        }

        return rows;
    }

    private void ConfigureCells()
    {
        foreach (var cell in AllCells.Cast<HexCell>())
        {
            int rowIndex = cell.RowIndex;
            int colIndex = cell.ColumnIndex;

            var northDiagonal = int.IsEvenInteger(rowIndex) ? rowIndex - 1 : rowIndex;
            var southDiagonal = int.IsEvenInteger(rowIndex) ? rowIndex : rowIndex + 1;

            cell.NorthWest = GetCell(northDiagonal, colIndex - 1);
            cell.North = GetCell(rowIndex - 1, colIndex);
            cell.NorthEast = GetCell(northDiagonal, colIndex + 1);
            cell.SouthWest = GetCell(southDiagonal, colIndex - 1);
            cell.South = GetCell(rowIndex + 1, colIndex);
            cell.SouthEast = GetCell(southDiagonal, colIndex + 1);
        }
    }

}
    
    