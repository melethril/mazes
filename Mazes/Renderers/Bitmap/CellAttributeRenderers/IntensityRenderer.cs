using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    internal class IntensityRenderer : ICellAttributeRenderer
    {
        public int Order => 0;

        public void Render(CellAttributeRenderingContext context)
        {
            var attrValue = context.Attribute?.GetValueAs<float?>();
            if (attrValue == null) return;

            float intensity = attrValue.Value;
            byte dark = (byte)(255 * intensity);
            byte bright = (byte)(128 + (127 * intensity));
            var colour = new SKColor(dark, bright, dark);

            context.RenderBackground(colour);
        }
    }
}
