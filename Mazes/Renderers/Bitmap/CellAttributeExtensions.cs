using Mazes.Core;
using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public static class CellAttributeExtensions
    {
        public static SKColor GetBackgroundColour(this CellStyle style) => 
            style.GetColour(StyleProperties.BackgroundColour);

        public static SKColor GetForegroundColour(this CellStyle style) => 
            style.GetColour(StyleProperties.ForegroundColour);
            
        public static SKColor GetOutlineColour(this CellStyle style) => 
            style.GetColour(StyleProperties.OutlineColour);

        private static readonly SKColor lastResortColor = SKColors.LightGray;

        public static SKColor GetColour(this CellStyle style, StyleProperty property)
        {
            if (string.IsNullOrWhiteSpace(property.Name))
                throw new ArgumentException("property has no name");

            SKColor defaultColour = ParseColour(property.DefaultValue, lastResortColor);

            if (!style.Properties.TryGetValue(property.Name, out var value)) 
                return defaultColour;

            return ParseColour(value, defaultColour);
        }

        public static SKColor ParseColour(object value, SKColor? defaultColour = null)
        {
            defaultColour ??= lastResortColor;

            if (value is not string hexValue || string.IsNullOrWhiteSpace(hexValue)) 
                return defaultColour.Value;

            return SKColor.TryParse(hexValue, out var colour) ? colour : defaultColour.Value;

        }

    }
}