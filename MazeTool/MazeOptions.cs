using CommandLine;

namespace MazeTool;

internal class MazeOptions
{
    [Option("outdir", Required = true, HelpText = "Output path.")]
    public string? OutputFilePath { get; set; }

    [Option("maskfile", Required = false, HelpText = "Mask file path.")]
    public string? MaskFilePath { get; set; }

    [Option("stylesfile", Required = false, HelpText = "Styles file path.")]
    public string? StylesFilePath { get; set; }

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

        if (string.IsNullOrWhiteSpace(parserResult.Value.OutputFilePath))
        {
            Console.WriteLine($"Output folder: {parserResult.Value.OutputFilePath}");
        }

        if (string.IsNullOrWhiteSpace(parserResult.Value.StylesFilePath))
        {
            Console.WriteLine($"Styles file: {parserResult.Value.StylesFilePath}");
        }

        if (string.IsNullOrWhiteSpace(parserResult.Value.MaskFilePath))
        {
            Console.WriteLine($"Mask file: {parserResult.Value.MaskFilePath}");
        }

        return parserResult.Value;
    }
}