namespace Craftify.Geometry.Domain;

public static class Point3DComputations
{
    public static bool LiesOnCurve(
        this Point3D point,
        Curve curve,
        double tolerance = Defaults.Tolerance)
    {
        return (point, curve) switch
        {
            (Point3D point3D, LineSegment lineSegment)
                => point3D.LiesOnLineSegment(lineSegment, tolerance),
            (Point3D point3D, ArcSegment arcSegment)
                => point3D.LiesOnArcSegment(arcSegment, tolerance),
            _ => false
        };
    }

    private static bool LiesOnLineSegment(
        this Point3D point,
        LineSegment lineSegment,
        double tolerance)
    {
        return point.IsCollinearTo(lineSegment, tolerance) 
            && point.WithinLineSegmentBounds(lineSegment);
    }

    private static bool LiesOnArcSegment(
        this Point3D point,
        ArcSegment arcSegment,
        double tolerance)
    {
        var arcPlane = Plane.ByNormalAndOrigin(arcSegment.Normal, arcSegment.Center);
        var projectedPoint = arcPlane.ProjectPoint(point);

        return point.LiesOnArcPlane(projectedPoint, tolerance)
            && projectedPoint.WithinArcCircle(arcSegment, tolerance)
            && projectedPoint.WithingArcSegmentBounds(arcSegment);
    }

    private static bool LiesOnArcPlane(
        this Point3D point,
        Point3D projectedPoint,
        double tolerance)
    {
        return point.DistanceTo(projectedPoint).AlmostEqualTo(0, tolerance);
    }

    private static bool WithinArcCircle(
        this Point3D projectedPoint,
        ArcSegment arcSegment,
        double tolerance)
    {
        return projectedPoint.DistanceTo(arcSegment.Center).AlmostEqualTo(arcSegment.Radius, tolerance);
    }

    private static bool WithingArcSegmentBounds(this Point3D point, ArcSegment arcSegment)
    {
        var angle = arcSegment.Center.Y
                .SubtractFrom(point.Y)
                .Atan2(arcSegment.Center.X.SubtractFrom(point.X))
                .ToDegrees()
                .NormalizeAngle();
        var endAngle = (arcSegment.StartAngle + arcSegment.SweepAngle) % Constants.Degrees.FullCircle;
        return arcSegment.SweepAngle > 0
            ? angle.WithinArcSegmentBounds(arcSegment.StartAngle, endAngle)
            : angle.WithinArcSegmentBounds(endAngle, arcSegment.StartAngle);
    }

    private static bool WithinArcSegmentBounds(this double angle, double start, double end)
        => start < end ? angle >= start && angle <= end : angle >= end && angle <= start;

    private static bool WithinLineSegmentBounds(this Point3D point, LineSegment lineSegment)
    {
        return point.X.WithinLineSegmentBounds(lineSegment.Start.X, lineSegment.End.X) &&
           point.Y.WithinLineSegmentBounds(lineSegment.Start.Y, lineSegment.End.Y) &&
           point.Z.WithinLineSegmentBounds(lineSegment.Start.Z, lineSegment.End.Z);
    }

    private static bool WithinLineSegmentBounds(this double value, double min, double max)
        => value >= Math.Min(min, max) && value <= Math.Max(min, max);
}
