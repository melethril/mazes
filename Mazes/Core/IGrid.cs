namespace Mazes.Core;

public interface IGrid
{
    int RowCount { get; }
    int CellCount { get; }
    IEnumerable<ICell> PathableCells { get; }
    IEnumerable<ICell> AllCells { get; }
    ICell GetRandomCell(Random random);
}