namespace Craftify.Geometry.Domain;

public static class Plane3DProjections
{
    public static Point3D ProjectPoint(this Plane3D plane, Point3D point)
    {
        var pointVector = Vector.ByCoordinates(point.X, point.Y, point.Z);
        var planeOriginVector = Vector.ByCoordinates(plane.Origin.X, plane.Origin.Y, plane.Origin.Z);

        Vector3D originToPoint = pointVector.Subtract(planeOriginVector);
        double distanceFromPointToPlane = originToPoint.DotProduct(plane.Normal) / plane.Normal.DotProduct(plane.Normal);

        Vector3D projectionVector = plane.Normal.Multiply(distanceFromPointToPlane);
        return point.Translate(projectionVector.Negate());
    }
}
