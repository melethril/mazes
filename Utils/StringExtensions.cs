namespace Mazes
{
    public static class StringExtensions
    {
        public static string Truncate(this string text, int cellWidth)
        {
            return text[..Math.Min(cellWidth, text.Length)];
        }

        public static string PadToCentre(this string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;

            return source.PadLeft(padLeft).PadRight(length);
        }

    }
}