namespace Mazes
{
    public class Maze
    {
        public int Rows { get; }
        public int Columns { get; }
        public int Size => Rows * Columns;

        private readonly Cell[][] grid;

        private Maze(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            grid = PrepareGrid();
            ConfigureCells();
        }

        public static Maze Empty(int rows, int columns)
        {
            return new Maze(rows, columns);
        }

        public static Maze Random(int rows, int columns, IMazeAlgorithm? algorithm = null, int? seed = null)
        {
            algorithm ??= new RecursiveBacktrackerAlgorithm();

            var random = seed == null 
                ? new Random()
                : new Random(seed.Value);

            var grid = Empty(rows, columns);

            return algorithm.Apply(grid, random);
        }

        public static Maze Random((int rows, int columns) size, IMazeAlgorithm? algorithm = null, int? seed = null) => 
            Random(size.rows, size.columns, algorithm, seed);

        protected virtual Cell[][] PrepareGrid()
        {
            var rows = new Cell[Rows][];

            for (int i = 0; i < Rows; i++)
            {
                var row = new Cell[Columns];

                for (int j = 0; j < Columns; j++)
                {
                    row[j] = new Cell(i, j);
                }

                rows[i] = row;
            }

            return rows;
        }

        protected virtual void ConfigureCells()
        {
            foreach (var cell in Cells)
            {
                var (row, col) = (cell.Row, cell.Column);

                cell.North = this[row - 1, col];
                cell.South = this[row + 1, col];
                cell.East = this[row, col + 1];
                cell.West = this[row, col - 1];
            }
        }

        public Cell? this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Rows) return null;
                if (column < 0 || column >= Columns) return null;
                return grid[row][column];
            }
        }

        public Cell? GetRandomCell(Random random)
        {
            int row = random.Next(Rows);
            int col = random.Next(grid[row].Length);

            return this[row, col];
        }

        public IEnumerable<Cell[]> EachRow()
        {
            return grid.Select(r => r);
        }

        public IEnumerable<Cell> Cells
        {
            get
            {
                return grid.SelectMany(r => r).Where(c => c != null);
            }
        }
    }
}