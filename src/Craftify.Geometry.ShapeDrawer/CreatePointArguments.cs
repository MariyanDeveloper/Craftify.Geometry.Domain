using SkiaSharp;

namespace Craftify.Geometry.ShapeDrawer;

public record CreatePointArguments(float PointRadius, SKPaint Paint)
{
    public static CreatePointArguments Default =>
        new(Defaults.Geometry.PointRadius, Style.ForPoint());

    public static CreatePointArguments Create(SKPaint paint)
    {
        return new CreatePointArguments(Defaults.Geometry.PointRadius, paint);
    }

    public static CreatePointArguments Create(float radius, SKPaint paint)
    {
        return new CreatePointArguments(radius, paint);
    }
};