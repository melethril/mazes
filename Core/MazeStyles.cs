using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mazes.Core
{
    public class MazeStyles(JsonObject root)
    {
        public Dictionary<string, string> Page = root["Page"]?.Deserialize<Dictionary<string, string>>() ?? [];
        public Dictionary<string, string> DefaultCell = root["DefaultCell"]?.Deserialize<Dictionary<string, string>>() ?? [];
        public JsonArray Attributes = root["Attributes"]?.AsArray() ?? [];

        public static MazeStyles LoadFrom(string path)
        {
            var root = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText(path))?.AsObject()
                ?? throw new FileLoadException("Unable to parse styles");

            return new MazeStyles(root);
        }
    }

}
