namespace Mazes.Renderers.Bitmap
{
    public class RenderingProperty(string name, string type, string value)
    {
        public string Name { get; } = name;
        public string Type { get; } = type;
        public string Value { get; set;} = value;
    }
}