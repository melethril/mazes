using SkiaSharp;

namespace Mazes.Renderers.Bitmap
{
    public class PageRenderer
    {
        private readonly Style image = new(SKColors.White);
        private readonly Style page = new(SKColors.AntiqueWhite, SKColors.LightGray);

        public void RenderPage(SKRectI pageRect, SKCanvas canvas)
        {
            canvas.Clear(image.FillBrush.Color);
            canvas.DrawRect(pageRect, page.FillBrush);
            canvas.DrawRect(pageRect, page.StrokeBrush);
        }
    }
}
