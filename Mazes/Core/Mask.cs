using Mazes.Utils;

namespace Mazes.Core;

public class Mask
{
    private readonly bool[][] bits;

    public int Rows { get; }
    public int Columns { get; }

    private Mask(bool[][] bits)
    {
        this.Rows = bits.Length;
        this.Columns = bits[0].Length;
        this.bits = bits;
    }

    public static Mask Empty((int rows, int columns) size)
    {
        return new Mask(PrepareMask(size.rows, size.columns));
    }

    public static Mask LoadFrom(Stream stream)
    {
        using StreamReader sr = new(stream);
        List<string> lines = [];

        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line != null)
                lines.Add(line);
        }

        bool[][] bits = lines
            .Select(l => l.Select(c => c != 'X').ToArray())
            .ToArray();

        return new Mask(bits);
    }

    private static bool[][] PrepareMask(int numRows, int numColumns)
    {
        bool[][] maskRows = new bool[numRows][];

        for (int i = 0; i < numRows; i++)
        {
            bool[] row = new bool[numColumns];

            for (int j = 0; j < numColumns; j++)
            {
                row[j] = true;
            }

            maskRows[i] = row;
        }

        return maskRows;
    }

    public bool this[int row, int column]
    {
        get
        {
            if (row < 0 || row >= Rows) return false;
            if (column < 0 || column >= Columns) return false;
            return bits[row][column];
        }
        set
        {
            if (row < 0 || row >= Rows) throw new ArgumentException("row out of range");
            if (column < 0 || column >= Columns) throw new ArgumentException("column out of range");
            bits[row][column] = value;
        }
    }

    public int Count()
    {
        int count = 0;

        foreach (var row in Enumerable.Range(0, Rows))
        {
            foreach (var col in Enumerable.Range(0, Columns))
            {
                if (bits[row][col])
                    count += 1;
            }
        }

        return count;
    }

    public (int row, int col) GetRandomLocation(Random random)
    {
        if (Count() == 0) throw new InvalidOperationException("Mask covers all cells");

        var maskedLocations = bits
            .SelectMany((row, rowIndex) =>
                row.Select((value, colIndex) => (rowIndex, colIndex, value))
            )
            .Where(o => o.value)
            .ToArray();

        var location = random.Sample(maskedLocations);

        return (location.rowIndex, location.colIndex);
    }

    public Mask ScaleUp(int scaleX, int scaleY)
    {
        bool[][] scaledBits = bits
            .SelectMany(row =>
                {
                    bool[] scaledRow = row
                        .Select(b => Enumerable.Repeat(b, scaleX))
                        .Aggregate((prev, cur) => prev.Concat(cur))
                        .ToArray();

                    return Enumerable.Repeat(scaledRow, scaleY);
                }
            )
            .ToArray();

        return new Mask(scaledBits);
    }
}