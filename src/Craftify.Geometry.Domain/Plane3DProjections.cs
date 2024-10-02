namespace Craftify.Geometry.Domain;

public static class Plane3DProjections
{
    public static Point3D ProjectPoint(this Plane3D plane, Point3D point)
    {
        var distanceFromPointToPlane = point.MeasureSignedDistanceToPointAlongVector(
            plane.Origin, plane.Normal);
        var projectionVector = plane.Normal.Normalize().Multiply(distanceFromPointToPlane);
        return point.Translate(projectionVector);
    }
}
