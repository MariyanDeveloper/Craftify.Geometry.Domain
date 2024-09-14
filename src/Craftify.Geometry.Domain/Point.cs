namespace Craftify.Geometry.Domain;

public static class Point
{
    public static Point3D ByCoordinates(double x, double y, double z) => new(x, y, z);
}