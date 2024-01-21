namespace Mazes
{
    public class MazeBuilder
    {
        private static IMazeAlgorithm DefaultAlgorithm => new RecursiveBacktrackerAlgorithm();
        private static (int, int) DefaultSize => (10, 10);

        private (int rows, int columns)? size;
        private IMazeAlgorithm? algorithm;
        private Mask? mask;

        private MazeBuilder()
        {
        }

        public static MazeBuilder WithSize((int rows, int columns) size)
        {
            return new MazeBuilder { size = size };
        }

        public static MazeBuilder WithMask(Mask mask)
        {
            return new MazeBuilder { mask = mask };
        }

        public MazeBuilder WithAlgorithm(IMazeAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            return this;
        }
        
        public Maze BuildRandomMaze()
        {
            return BuildMaze(seed: null);
        }

        public Maze BuildMazeFromSeed(int seed)
        {
            return BuildMaze(seed);
        }

        private Maze BuildMaze(int? seed)
        {
            var alg = algorithm ?? DefaultAlgorithm;
            var (rows, columns) = size ?? DefaultSize;
            var random = seed.HasValue ? new Random(seed.Value) : new Random();

            Maze maze = (mask == null)
                ? new SimpleMaze(rows, columns)
                : new MaskedMaze(mask);

            return alg.Apply(maze, random);
        }

    }
}