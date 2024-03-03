using CommandLine;

namespace MazeTool;

// ReSharper disable once ClassNeverInstantiated.Global
internal class MazeOptions
{
    [Option("outdir", Required = true, HelpText = "Output path.")]
    public string? OutputFilePath { get; set; }

    [Option("maskfile", Required = false, HelpText = "Mask file path.")]
    public string? MaskFilePath { get; set; }

    [Option("stylesfile", Required = false, HelpText = "Styles file path.")]
    public string? StylesFilePath { get; set; }
    
    [Option("topology", Required = false, HelpText = "The type of grid to use", Default = "rect")]
    public string? Topology { get; set; }

    [Option("rows", Required = false, HelpText = "Number of rows.", Default = 40)]
    public int Rows { get; set; }
    
    [Option("cols", Required = false, HelpText = "Number of columns.", Default = 40)]
    public int Columns { get; set; }

    internal static MazeOptions Parse(string[] args)
    {
        var parserResult = Parser.Default.ParseArguments<MazeOptions>(args);
        if (parserResult.Errors.Any())
        {
            foreach (var error in parserResult.Errors)
            {
                Console.WriteLine($"Error: {error}");
            }

            throw new ArgumentException("Invalid Arguments");
        }

        return parserResult.Value;
    }
}