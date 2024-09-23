namespace Craftify.Geometry.Domain;

public static class ArcEvaluation
{
    public static Point3D GetPointAtParameter(this ArcSegment arc, double parameter)
    {
        var angleInDegrees = arc.StartAngle + parameter * arc.SweepAngle;
        var angleInRadians = angleInDegrees.ToRadians();
        return Point.ByCoordinates(
            arc.Center.X + arc.Radius * Math.Cos(angleInRadians),
            arc.Center.Y + arc.Radius * Math.Sin(angleInRadians),
            arc.Center.Z
        );
    }

    public static Point3D GetStartPoint(this ArcSegment arcSegment) =>
        arcSegment.GetPointAtParameter(0);

    public static Point3D GetEndPoint(this ArcSegment arcSegment) =>
        arcSegment.GetPointAtParameter(1);
}
