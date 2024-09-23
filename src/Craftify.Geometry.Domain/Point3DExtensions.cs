﻿namespace Craftify.Geometry.Domain;

public static class Point3DExtensions
{
    public static double DistanceTo(this Point3D point, Point3D other)
    {
        var power = 2;
        return Math.Sqrt(
            Math.Pow(other.X - point.X, power)
                + Math.Pow(other.Y - point.Y, power)
                + Math.Pow(other.Z - point.Z, power)
        );
    }

    public static Point3D Translate(this Point3D point, Vector3D vector)
    {
        return new Point3D(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
    }

    public static Point3D Midpoint(this Point3D point, Point3D other)
    {
        var halfDivisionNumber = 2;
        return new Point3D(
            (point.X + other.X) / halfDivisionNumber,
            (point.Y + other.Y) / halfDivisionNumber,
            (point.Z + other.Z) / halfDivisionNumber
        );
    }

    public static Vector3D SubtractPoint(this Point3D main, Point3D other)
        => new(main.X - other.X, main.Y - other.Y, main.Z - other.Z);
}
