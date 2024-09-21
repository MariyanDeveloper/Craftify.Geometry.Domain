using SkiaSharp;

namespace Craftify.Geometry.ShapeDrawer;

public static class Style
{
    public static SKPaint ForPoint(SKColor? color = null, float? width = null)
    {
        return new SKPaint()
        {
            Color = color ?? Defaults.Geometry.Color,
            StrokeWidth = width ?? Defaults.Geometry.Width,
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
        };
    }

    public static SKPaint ForSmoothLine(SKColor? color = null, float? width = null)
    {
        return new SKPaint()
        {
            Color = color ?? Defaults.Geometry.Color,
            StrokeWidth = width ?? Defaults.Geometry.Width,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round,
        };
    }

    public static SKPaint ForSharpLine(SKColor? color = null, float? width = null)
    {
        return new SKPaint()
        {
            Color = color ?? Defaults.Geometry.Color,
            StrokeWidth = width ?? Defaults.Geometry.Width,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
        };
    }
}