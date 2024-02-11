namespace Mazes.Renderers.Bitmap;

using Core;
using SkiaSharp;

public class ImageRenderer(MazeStyles styles, RendererRegistry rendererRegistry)
{
    public SKSurface Render(IGrid grid, Dimensions imageSize)
    {
        var surface = CreateSurface(imageSize);
        var canvas = surface.Canvas;
        var pageBounds = CreatePageRect(imageSize, styles.Page.Margin);
        
        RenderPage(pageBounds, canvas, styles.Page.Margin);
        
        var renderer = GetRenderer(grid);
        renderer.Render(canvas, grid, pageBounds);

        return surface;
    }

    private IGridRenderer GetRenderer(IGrid grid)
    {
        return grid switch
        {
            SimpleGrid => new RectangularGridRenderer(styles, rendererRegistry),
            MaskedGrid => new RectangularGridRenderer(styles, rendererRegistry),
            PolarGrid => new PolarGridRenderer(styles, rendererRegistry),
            
            _ => throw new ArgumentOutOfRangeException(nameof(grid), grid, null)
        };
    }

    private void RenderPage(SKRectI pageBounds, SKCanvas canvas, int margin)
    {
        using SKPaint pageFill = new();
        pageFill.IsStroke = false;
        pageFill.Color = SKColor.Parse(styles.Page.BackgroundColour);

        canvas.Clear(CellAttributeExtensions.ParseColour(styles.Image.BackgroundColour));
        canvas.DrawRect(pageBounds, pageFill);
   
        if (margin > 0)
        {
            using SKPaint pageOutline = new();
            pageOutline.IsStroke = true;
            pageOutline.StrokeWidth = 2;
            pageOutline.Color = SKColor.Parse(styles.Page.OutlineColour);

            canvas.DrawRect(pageBounds, pageOutline);
        }
    }

    private static SKSurface CreateSurface(Dimensions imageSize)
    {
        var imageInfo = new SKImageInfo(
            width: imageSize.Width,
            height: imageSize.Height,
            colorType: SKColorType.Rgba8888,
            alphaType: SKAlphaType.Premul
        );

        return SKSurface.Create(imageInfo);
    }

    private static SKRectI CreatePageRect(Dimensions imageSize, int margin)
    {
        return new SKRectI(margin, margin, imageSize.Width - margin, imageSize.Height - margin);
    }

}