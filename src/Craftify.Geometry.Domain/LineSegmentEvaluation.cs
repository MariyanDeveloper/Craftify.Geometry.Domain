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

    public static Vector3D GetNormalizedDirection(this LineSegment line) =>
        Vector.ByTwoPoints(line.Start, line.End).Normalize();

    public static Vector3D GetDirection(this LineSegment line) =>
        Vector.ByTwoPoints(line.Start, line.End);

    public static double GetLength(this LineSegment line) => line.GetDirection().Magnitude();

    public static bool IsParallelTo(
        this LineSegment line1,
        LineSegment line2,
        double tolerance = Defaults.Tolerance
    )
    {
        return line1
            .GetNormalizedDirection()
            .IsParallelTo(line2.GetNormalizedDirection(), tolerance);
    }

    public static IEnumerable<Point3D> GetEndPoints(this LineSegment lineSegment)
    {
        yield return lineSegment.Start;
        yield return lineSegment.End;
    }

    public static bool IsCollinearTo(
        this LineSegment line1,
        LineSegment line2,
        double tolerance = Defaults.Tolerance
    )
    {
        if (!line1.IsParallelTo(line2, tolerance))
        {
            return false;
        }

        return Vector
            .ByTwoPoints(line1.Start, line2.Start)
            .IsParallelTo(line1.GetNormalizedDirection());
    }

    public static bool IsConnectedTo(
        this LineSegment line1,
        LineSegment line2,
        double tolerance = Defaults.Tolerance
    )
    {
        return line1.Start.AlmostEqualTo(line2.Start, tolerance)
            || line1.Start.AlmostEqualTo(line2.End, tolerance)
            || line1.End.AlmostEqualTo(line2.Start, tolerance)
            || line1.End.AlmostEqualTo(line2.End, tolerance);
    }

    public static bool AlmostEqualTo(
        this LineSegment line1,
        LineSegment line2,
        double tolerance = Defaults.Tolerance
    )
    {
        var isFirstPairConnected =
            line1.Start.AlmostEqualTo(line2.Start, tolerance)
            && line1.End.AlmostEqualTo(line2.End, tolerance);
        var isSecondPairConnected =
            line1.Start.AlmostEqualTo(line2.End, tolerance)
            && line1.End.AlmostEqualTo(line2.Start, tolerance);
        return isFirstPairConnected || isSecondPairConnected;
    }

    public static LineSegment Translate(this LineSegment lineSegment, Vector3D translation)
    {
        var newStart = lineSegment.Start.Translate(translation);
        var newEnd = lineSegment.End.Translate(translation);
        return Line.ByStartPointAndEndPoint(newStart, newEnd);
    }

    public static LineSegment ExtendEnd(this LineSegment lineSegment, double distance)
    {
        return lineSegment with
        {
            End = lineSegment.End.Translate(lineSegment.GetNormalizedDirection().Scale(distance)),
        };
    }
}
