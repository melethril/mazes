namespace Mazes.Core;

public abstract class Grid(int rowCount, int columnCount, IReadOnlyList<ICell[]> grid) : IGrid
{
    public int RowCount { get; } = rowCount;

    public int ColumnCount => columnCount;

    public int CellCount => AllCells.Count();

    protected bool IsInBounds(int row, int column)
    {
        return IsRowInBounds(row) && IsColumnInBounds(GetRow(row), column);
    }
    
    private bool IsRowInBounds(int row)
    {
        return row >= 0 && row < RowCount;
    }

    private static bool IsColumnInBounds(IReadOnlyCollection<ICell> row, int column)
    {
        return column >= 0 && column < row.Count;
    }

    protected ICell GetCell(int row, int column)
    {
        if (!IsInBounds(row, column))
            throw new IndexOutOfRangeException($"Cell index out of bounds: ({row}, {column})");

        return grid[row][column];
    }

    public ICell[] GetRow(int row)
    {
        if (!IsRowInBounds(row))
            throw new IndexOutOfRangeException($"Row index out of bounds: {row}");

        return grid[row];
    }

    public virtual ICell GetRandomCell(Random random)
    {
        int row = random.Next(RowCount);
        int col = random.Next(grid[row].Length);

        return GetCell(row, col);
    }

    public IEnumerable<ICell[]> AllRows
    {
        get { return grid.Select(r => r); }
    }

    public IEnumerable<ICell> PathableCells
    {
        get
        {
            return grid.SelectMany(r => r).Where(c => c.IsPathable);
        }
    }

    public IEnumerable<ICell> AllCells
    {
        get
        {
            return grid.SelectMany(r => r);
        }
    }

}