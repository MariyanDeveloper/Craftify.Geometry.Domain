namespace Craftify.Geometry.Domain;

public static class CurveEvaluation
{
    public static IEnumerable<Point3D> EndPoints(this Curve curve)
    {
        yield return curve.Start();
        yield return curve.End();
    }

    public static Point3D Start(this Curve curve)
    {
        return curve switch
        {
            LineSegment lineSegment => lineSegment.Start,
            ArcSegment arcSegment => arcSegment.GetStartPoint(),
            _ => throw new InvalidOperationException()
        };
    }

    public static Point3D End(this Curve curve)
    {
        return curve switch
        {
            LineSegment lineSegment => lineSegment.End,
            ArcSegment arcSegment => arcSegment.GetEndPoint(),
            _ => throw new InvalidOperationException()
        };
    }

    public static bool HasEndPoint(
        this Curve curve,
        Point3D point,
        double tolerance = Defaults.Tolerance)
    {
        return curve.EndPoints().Any(
            endPoint => endPoint.AlmostEqualTo(point, tolerance));
    }

    public static double GetLength(this Curve curve)
    {
        return curve switch
        {
            LineSegment lineSegment => lineSegment.GetLength(),
            ArcSegment arcSegment => arcSegment.GetLength(),
            _ => throw new InvalidOperationException()
        };
    }
}
