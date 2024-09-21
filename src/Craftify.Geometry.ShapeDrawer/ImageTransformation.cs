using Craftify.Geometry.Domain;

namespace Craftify.Geometry.ShapeDrawer;

public static class ImageTransformation
{
    public static Point3D AdaptGeometryToStartFromLeftBottomCornerOnCanvas(
        this Point3D point,
        double height
    )
    {
        return Point.ByCoordinates(point.X, height - point.Y);
    }

    public static LineSegment AdaptGeometryToStartFromLeftBottomCornerOnCanvas(
        this LineSegment lineSegment,
        double height
    )
    {
        return Line.ByStartPointAndEndPoint(
            lineSegment.Start.AdaptGeometryToStartFromLeftBottomCornerOnCanvas(height),
            lineSegment.End.AdaptGeometryToStartFromLeftBottomCornerOnCanvas(height)
        );
    }

    public static ArcSegment AdaptGeometryToStartFromLeftBottomCornerOnCanvas(
        this ArcSegment arcSegment,
        double height
    )
    {
        return arcSegment with
        {
            Center = arcSegment.Center.AdaptGeometryToStartFromLeftBottomCornerOnCanvas(height),
        };
    }
}