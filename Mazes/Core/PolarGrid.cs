namespace Mazes.Core;

public class PolarGrid : Grid
{
    public PolarGrid(int rowCount) : base(rowCount, rowCount, PrepareGrid(rowCount))
    {
        ConfigureCells();
    }

    private static PolarCell[][] PrepareGrid(int rowCount)
    {
        var voidAttribute = new [] {new CellAttribute(CellAttributeType.IsVoid)};
           
        var rows = new PolarCell[rowCount][];
        double rowHeight = 1.0 / rowCount;
        rows[0] = [ new PolarCell(0, 0) ];

        foreach (int rowNum in Enumerable.Range(1, rowCount - 1))
        {
            double radius = (double)rowNum / rowCount;
            double circumference = 2 * Math.PI * radius;
            int previousCount = rows[rowNum - 1].Length;
            double estimatedCellWidth = circumference / previousCount;
            int ratio = (int)Math.Round(estimatedCellWidth / rowHeight, 0);
            int numCells = previousCount * ratio;

            var row = new PolarCell[numCells]; 
            for (int colNum = 0; colNum < numCells; colNum++)
            {
                bool isPathable = true;
                row[colNum] = new PolarCell(rowNum, colNum, isPathable, isPathable ? null : voidAttribute);
            }

            rows[rowNum] = row;
        }

        return rows;
    }

    private void ConfigureCells()
    {
        foreach (var cell in AllCells.Cast<PolarCell>())
        {
            int rowIndex = cell.RowIndex;
            int colIndex = cell.ColumnIndex;

            if (rowIndex > 0)
            {
                cell.Clockwise = IsInBounds(rowIndex, colIndex + 1) ? GetCell(rowIndex, colIndex + 1) : null;
                cell.AntiClockwise = IsInBounds(rowIndex, colIndex - 1) ? GetCell(rowIndex, colIndex - 1) : null;

                int ratio = GetRow(rowIndex).Length / GetRow(rowIndex - 1).Length;
                var parent = (PolarCell)GetCell(rowIndex - 1, colIndex / ratio);
                parent.Outward.Add(cell);
                cell.Inward = parent;
            }
        }
    }

}
    
    