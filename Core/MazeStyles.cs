using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mazes.Core
{
    public class MazeStyles(JsonObject root)
    {
        public PageStyles Page = root["Page"]?.Deserialize<PageStyles>() ?? new PageStyles();
        public JsonObject DefaultCell = root["DefaultCell"]?.AsObject() ?? [];
        public JsonArray Attributes = root["Attributes"]?.AsArray() ?? [];

        public static MazeStyles LoadFrom(string path)
        {
            var root = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText(path))?.AsObject()
                ?? throw new FileLoadException("Unable to parse styles");

            return new MazeStyles(root);
        }
    }

    public class PageStyles
    {
        public int Margin { get; set; } = 0;
        public int NumPaddingCells { get; set; } = 1;
    }

    public class CellStyles
    {
        public string Colour { get; set; } = "#000000";

        public int StrokeWidth { get; set; } = 6;
    }
    
    public class CellStyles
    {
        public string Colour { get; set; } = "#000000";

        public int StrokeWidth { get; set; } = 6;
    }
}
