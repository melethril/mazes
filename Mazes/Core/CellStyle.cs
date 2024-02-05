namespace Mazes.Core
{
    public class CellStyle(string attributeName, Dictionary<string, string> properties)
    {
        public string Name { get; set; } = attributeName;
        public Dictionary<string, string> Properties { get; set; } = properties;


        public CellStyle() : this("", []) {}

        public CellStyle(string attributeName) : this(attributeName, []) {}
    }
}
