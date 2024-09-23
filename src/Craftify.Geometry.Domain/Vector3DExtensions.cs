namespace Craftify.Geometry.Domain;

public static class Vector3DExtensions
{
    public static double Magnitude(this Vector3D vector)
    {
        var (x, y, z) = vector;
        return Math.Sqrt(x.Square() + y.Square() + z.Square());
    }

    public static Vector3D Normalize(this Vector3D vector)
    {
        var magnitude = vector.Magnitude();
        return new Vector3D(vector.X / magnitude, vector.Y / magnitude, vector.Z / magnitude);
    }

    public static double DotProduct(this Vector3D vector, Vector3D other)
    {
        return vector.X * other.X + vector.Y * other.Y + vector.Z * other.Z;
    }

    public static Vector3D CrossProduct(this Vector3D vector, Vector3D other)
    {
        return new Vector3D(
            vector.Y * other.Z - vector.Z * other.Y,
            vector.Z * other.X - vector.X * other.Z,
            vector.X * other.Y - vector.Y * other.X
        );
    }

    public static Vector3D Add(this Vector3D vector, Vector3D other)
    {
        return new Vector3D(vector.X + other.X, vector.Y + other.Y, vector.Z + other.Z);
    }

    public static Vector3D Subtract(this Vector3D vector, Vector3D other)
    {
        return new Vector3D(vector.X - other.X, vector.Y - other.Y, vector.Z - other.Z);
    }

    public static Vector3D Multiply(this Vector3D vector, double scalar)
    {
        return new Vector3D(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
    }

    public static Vector3D Divide(this Vector3D vector, double scalar)
    {
        return new Vector3D(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
    }

    public static Vector3D Scale(this Vector3D vector, double scalar)
    {
        return new Vector3D(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
    }

    public static Vector3D Negate(this Vector3D vector)
    {
        return new Vector3D(-vector.X, -vector.Y, -vector.Z);
    }

    public static double AngleTo(this Vector3D vector, Vector3D other)
    {
        var dotProduct = vector.DotProduct(other);
        var magnitudeA = vector.Magnitude();
        var magnitudeB = other.Magnitude();
        var cosTheta = dotProduct / (magnitudeA * magnitudeB);

        return Math.Acos(cosTheta).ToDegrees();
    }

    public static double AngleAboutAxis(this Vector3D vector, Vector3D other, Vector3D rotationAxis)
    {
        var v1 = vector.Subtract(vector.ProjectOnto(rotationAxis));
        var v2 = other.Subtract(other.ProjectOnto(rotationAxis));

        var angle = Math.Atan2(v1.CrossProduct(v2).DotProduct(rotationAxis), v1.DotProduct(v2));
        var angleInDegrees = angle.ToDegrees();

        return angleInDegrees < 0 ? angleInDegrees + 360 : angleInDegrees;
    }

    public static Point3D AsPoint(this Vector3D vector) =>
        Point.ByCoordinates(vector.X, vector.Y, vector.Z);

    public static Vector3D ProjectOnto(this Vector3D vector, Vector3D onto)
    {
        var scalarProjection = vector.DotProduct(onto) / onto.DotProduct(onto);
        return onto.Multiply(scalarProjection);
    }

    public static bool IsParallelTo(
        this Vector3D vector1,
        Vector3D vector2,
        double tolerance = Defaults.Tolerance
    )
    {
        return vector1.CrossProduct(vector2).IsZero(tolerance);
    }

    public static bool IsZero(this Vector3D vector, double tolerance = Defaults.Tolerance)
    {
        return vector.Magnitude().AlmostEqualTo(0, tolerance);
    }
}
