namespace Craftify.Geometry.Domain;

public static class Point
{
    public static Point3D ByCoordinates(double x, double y, double z) => new(x, y, z);
    public static Point3D ByCoordinates(double x, double y) => new(x, y, 0);
    public static Point3D ByCoordinates(double x) => new(x, 0, 0);
}