namespace Craftify.Geometry.Domain;

public static class Line
{
    public static LineSegment ByStartPointAndEndPoint(Point3D start, Point3D end) => new(start, end);
}