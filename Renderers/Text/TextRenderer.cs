namespace Mazes.Renderers.Text
{
    public class TextRenderer
    {
        public string Render(Maze maze)
        {
            const int cellWidth = 4;
            string horizontalWall = new('-', cellWidth);
            string verticalLink = new(' ', cellWidth);
            string output = "+" + string.Join("", Enumerable.Repeat(horizontalWall + "+", maze.Columns)) + "\n";

            foreach (var row in maze.EachRow())
            {
                string bodyLine = "|";
                string bottomLine = "+";

                foreach (var c in row)
                {
                    Cell cell = c ?? new Cell(-1, -1);

                    string label = cell.Attributes.LastOrDefault(a => 
                        a.Type == CellAttributeType.Index || 
                        a.Type == CellAttributeType.Distance ||
                        a.Type == CellAttributeType.Path
                    )?.GetValueAs<string>() ?? string.Empty;

                    string body = label.Truncate(cellWidth).PadToCentre(cellWidth);
                    string eastBoundary = !cell.IsLinked(cell.East) ? "|" : " ";
                    bodyLine += body + eastBoundary;

                    string southBoundary = !cell.IsLinked(cell.South) ? horizontalWall : verticalLink;
                    string corner = "+";
                    bottomLine += southBoundary + corner;
                }

                output += bodyLine + "\n" + bottomLine + "\n";
            }

            return output;
        }
    }
}