namespace Mazes.Core;

public class StyleProperty(string name, object defaultValue)
{
    public string Name { get; } = name;

    public object DefaultValue { get; } = defaultValue;
}