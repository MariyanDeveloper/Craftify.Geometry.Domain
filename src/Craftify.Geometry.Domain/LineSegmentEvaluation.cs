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

    public static double GetLength(this LineSegment lineSegment)
    {
        double deltaX = lineSegment.End.X - lineSegment.Start.X;
        double deltaY = lineSegment.End.Y - lineSegment.Start.Y;
        double deltaZ = lineSegment.End.Z - lineSegment.Start.Z;

        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
    }

    public static Vector3D GetDirection(this LineSegment lineSegment)
    {
        double deltaX = lineSegment.End.X - lineSegment.Start.X;
        double deltaY = lineSegment.End.Y - lineSegment.Start.Y;
        double deltaZ = lineSegment.End.Z - lineSegment.Start.Z;

        return new Vector3D(deltaX, deltaY, deltaZ);
    }
}
