namespace Mazes.Renderers.Text
{
    public class TextRenderer
    {
        public void Render(Maze maze, TextWriter writer)
        {
            const int cellWidth = 4;
            string horizontalWall = new('-', cellWidth);
            string verticalLink = new(' ', cellWidth);
            string output = "+" + string.Join("", Enumerable.Repeat(horizontalWall + "+", maze.ColumnCount)) + "\n";

            foreach (var row in maze.EachRow())
            {
                string bodyLine = "|";
                string bottomLine = "+";

                foreach (var c in row)
                {
                    ICell cell = c ?? new Cell(-1, -1, false);

                    string label = GetCellLabel(cell);

                    string body = label.Truncate(cellWidth).PadToCentre(cellWidth);
                    string eastBoundary = !cell.IsLinked(cell.East) ? "|" : " ";
                    bodyLine += body + eastBoundary;

                    string southBoundary = !cell.IsLinked(cell.South) ? horizontalWall : verticalLink;
                    string corner = "+";
                    bottomLine += southBoundary + corner;
                }

                output += bodyLine + "\n" + bottomLine + "\n";
            }

            writer.WriteLine(output);
        }

        private static string GetCellLabel(ICell cell)
        {
            if (cell.Attributes.Any(a => a.Type == CellAttributeType.IsStartCell))
                return "O";

            if (cell.Attributes.Any(a => a.Type == CellAttributeType.IsTargetCell))
                return "X";

            var cellAttribute = cell.Attributes.LastOrDefault(a =>
                a.Type == CellAttributeType.Index ||
                a.Type == CellAttributeType.Distance ||
                a.Type == CellAttributeType.Path
            );

            return cellAttribute?.GetValueAs<string>() ?? string.Empty;
        }
    }
}