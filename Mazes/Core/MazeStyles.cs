using System.Text.Json;

namespace Mazes.Core
{
    public class MazeStyles
    {
        public ImageStyles Image { get; set; } = new();
        public PageStyles Page { get; set; } = new();
        public CellStyles Cells { get; set; } = new();

        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static async Task<MazeStyles> Load(string path)
        {
            using FileStream? stream = File.Exists(path) ? File.OpenRead(path) : null;
            MazeStyles? styles = null;

            if (stream != null)
            {
                styles = await JsonSerializer.DeserializeAsync<MazeStyles>(stream, jsonOptions);
            }

            return styles ?? throw new FileLoadException("Unable to load styles");
        }

        public async Task Save(string path)
        {
            await using FileStream stream = File.Create(path);
            await JsonSerializer.SerializeAsync(stream, this, jsonOptions);
        }
    }

}