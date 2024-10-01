namespace Craftify.Geometry.Domain;

public static class Vector
{
    public static Vector3D XAxis() => new(1, 0, 0);

    public static Vector3D YAxis() => new(0, 1, 0);

    public static Vector3D ZAxis() => new(0, 0, 1);

    public static Vector3D ByCoordinates(double x, double y, double z) => new(x, y, z);

    public static Vector3D ByCoordinates(double x, double y) => new(x, y, 0);

    public static Vector3D ByCoordinates(double x) => new(x, 0, 0);

    public static Vector3D Zero() => new(0, 0, 0);

    public static Vector3D ByTwoPoints(Point3D start, Point3D end)
    {
        return end.AsVector().Subtract(start.AsVector());
    }
}
