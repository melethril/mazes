using System.Runtime.Serialization;

namespace Mazes
{
    [Serializable]
    public sealed class Cell : ISerializable
    {
        public int Row { get; }
        public int Column { get; }
        public Cell? North { get; set; }
        public Cell? South { get; set; }
        public Cell? East { get; set; }
        public Cell? West { get; set; }

        public bool IsAtNorthEdge => North == null;
        public bool IsAtWestEdge => West == null;
        public bool IsAtEastEdge => East == null;
        public bool IsAtSouthEdge => South == null;

        public IList<CellAttribute> Attributes { get; } = new List<CellAttribute>();

        private readonly Dictionary<Cell, bool> links = [];

        public IEnumerable<Cell> Links => links.Keys;
        public bool IsLinked(Cell? cell) => cell != null && links.ContainsKey(cell);

        public Cell Link(Cell cell, bool bidirectional = true)
        {
            links[cell] = true;
            if (bidirectional) cell.Link(this, false);

            return this;
        }

        public Cell Unlink(Cell cell, bool bidirectional = true)
        {
            links.Remove(cell, out _);
            if (bidirectional) cell.Unlink(this, false);

            return this;
        }

        public IEnumerable<Cell> Neighbours
        {
            get
            {
                return new[] { North, South, East, West }
                    .Where(o => o is not null)
                    .Cast<Cell>();
            }
        }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        private Cell(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
            // n1 = info.GetInt32("i");
            // n2 = info.GetInt32("j");
            // str = info.GetString("k");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
            // info.AddValue("i", n1);
            // info.AddValue("j", n2);
            // info.AddValue("k", str);
        }

    }
}