using Craftify.Geometry.Domain;
using SkiaSharp;

namespace Craftify.Geometry.ShapeDrawer;

public static class GeometryToSKMapping
{
    public static SKPath MapToSKPath(this LineSegment lineSegment)
    {
        var path = new SKPath();
        path.MoveTo(lineSegment.Start.MapTo2DSkPoint());
        path.LineTo(lineSegment.End.MapTo2DSkPoint());
        return path;
    }

    public static SKPoint MapTo2DSkPoint(this IXYZ pointLike) =>
        new((float)pointLike.X, (float)pointLike.Y);
}