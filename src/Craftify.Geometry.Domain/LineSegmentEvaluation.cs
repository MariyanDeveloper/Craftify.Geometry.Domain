namespace Craftify.Geometry.Domain;

public static class LineSegmentEvaluation
{
    public static Point3D GetPointAtParameter(this LineSegment lineSegment, double parameter)
    {
        var (start, end) = lineSegment;
        return Point.ByCoordinates(
            start.X + parameter * (end.X - start.X),
            start.Y + parameter * (end.Y - start.Y),
            start.Z + parameter * (end.Z - start.Z)
        );
    }
}
