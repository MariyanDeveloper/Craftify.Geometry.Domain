namespace Craftify.Geometry.Domain;

public static class Vector3DExtensions
{
    public static double GetMagnitude(this Vector3D vector)
    {
        return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
    }

    public static Vector3D Normalize(this Vector3D vector)
    {
        var magnitude = vector.GetMagnitude();
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
}