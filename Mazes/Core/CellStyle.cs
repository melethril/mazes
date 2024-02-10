namespace Mazes.Core;

public class CellStyle(string attributeName, Dictionary<string, string> properties)
{
    public string Name { get; set; } = attributeName;
    public Dictionary<string, string> Properties { get; set; } = properties;

    // ReSharper disable once UnusedMember.Global
    public CellStyle() : this("", []) {}
    public CellStyle(string attributeName) : this(attributeName, []) {}
}