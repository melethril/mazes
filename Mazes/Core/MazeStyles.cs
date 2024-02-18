using System.Text.Json;

namespace Mazes.Core;

public class MazeStyles
{
    public ImageStyles Image { get; init; } = new();
    public PageStyles Page { get; init; } = new();
    public CellStyles Cells { get; init; } = new();

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public static async Task<MazeStyles> Load(string path)
    {
        await using var stream = File.Exists(path) ? File.OpenRead(path) : null;
        MazeStyles? styles = null;

        if (stream != null)
        {
            styles = await JsonSerializer.DeserializeAsync<MazeStyles>(stream, JsonOptions);
        }

        return styles ?? throw new FileLoadException("Unable to load styles");
    }

    public async Task Save(string path)
    {
        await using FileStream stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, this, JsonOptions);
    }
}
