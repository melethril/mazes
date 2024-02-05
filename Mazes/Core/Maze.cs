namespace Mazes
{
    public abstract class Maze
    {
        public int RowCount { get; }
        public int ColumnCount { get; }
        public virtual int Size => RowCount * ColumnCount;

        protected readonly ICell[][] grid;

        public Maze(int rows, int columns, ICell[][] grid)
        {
            RowCount = rows;
            ColumnCount = columns;
            this.grid = grid;

            ConfigureCells();
        }

        protected void ConfigureCells()
        {
            foreach (var cell in this.AllCells)
            {
                var (rowIndex, colIndex) = (cell.RowIndex, cell.ColumnIndex);

                if (rowIndex == 0 || colIndex == 0 || rowIndex == RowCount - 1 || colIndex == ColumnCount - 1)
                    cell.IsOnOuterEdge = true;

                TryGetCellAt(rowIndex - 1, colIndex, out ICell? north);
                TryGetCellAt(rowIndex + 1, colIndex, out ICell? south);
                TryGetCellAt(rowIndex, colIndex + 1, out ICell? east);
                TryGetCellAt(rowIndex, colIndex - 1, out ICell? west);

                if (cell.IsPathable)
                    SetupPathableCell(cell, north, south, east, west);
                else
                    SetupNonPathableCell(cell, north, south, east, west);

            }
        }

        private static void SetupPathableCell(ICell cell, ICell? north, ICell? south, ICell? east, ICell? west)
        {
            cell.North = north?.IsPathable == true ? north : null;
            cell.South = south?.IsPathable == true ? south : null;
            cell.East = east?.IsPathable == true ? east : null;
            cell.West = west?.IsPathable == true ? west : null;
        }

        private static void SetupNonPathableCell(ICell cell, ICell? north, ICell? south, ICell? east, ICell? west)
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

        private bool IsInBounds(int row, int column)
        {
            if (row < 0 || row >= RowCount) return false;
            if (column < 0 || column >= ColumnCount) return false;

            return true;
        }

        private bool TryGetCellAt(int row, int column, out ICell? cell)
        {
            cell = IsInBounds(row, column) ? this[row, column] : null;

            return cell != null;
        }

        public ICell this[int row, int column]
        {
            get
            {
                if (!IsInBounds(row, column))
                    throw new IndexOutOfRangeException($"Cell index out of bounds: ({row}, {column})");

                return grid[row][column];
            }
        }

        public abstract ICell GetRandomCell(Random random);

        public IEnumerable<ICell[]> EachRow()
        {
            return grid.Select(r => r);
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
}