namespace Mazes.Core;

public class SimpleGrid(int rowCount, int columnCount) 
    : RectangularGrid(rowCount, columnCount, PrepareGrid(rowCount, columnCount));