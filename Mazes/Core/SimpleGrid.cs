namespace Mazes.Core;

public class SimpleGrid(int rows, int columns) 
    : Grid(rows, columns, PrepareGrid(rows, columns))
{
    private static ICell[][] PrepareGrid(int numRows, int numColumns)
    {
        var rows = new ICell[numRows][];

        for (int i = 0; i < numRows; i++)
        {
            var row = new ICell[numColumns];

            for (int j = 0; j < numColumns; j++)
            {
                row[j] = new Cell(i, j, isPathable: true);
            }

            rows[i] = row;
        }

        return rows;
    }

    public override ICell GetRandomCell(Random random)
    {
        int row = random.Next(RowCount);
        int col = random.Next(grid[row].Length);

        return this[row, col];
    }
}