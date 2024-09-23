using SkiaSharp;

namespace Craftify.Geometry.ShapeDrawer;

public static class DrawingBoard
{
    public static void Draw(
        VisualizeFunction visualizeFunction,
        ImageArguments visualizationArguments
    )
    {
        var (width, height, imagePath, background) = visualizationArguments;
        using var bitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(background);
        visualizeFunction(canvas, new DrawingContext(width, height));
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite(imagePath);
        data.SaveTo(stream);
    }
}
