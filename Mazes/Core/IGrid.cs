namespace Mazes.Core;

public interface IGrid
{
    int RowCount { get; }
    int ColumnCount { get; }
    int Size { get; }
    IEnumerable<ICell> PathableCells { get; }
    IEnumerable<ICell> AllCells { get; }
    ICell this[int row, int column] { get; }
    ICell GetRandomCell(Random random);
    IEnumerable<ICell[]> EachRow();
}