namespace Craftify.Geometry.Domain;

public abstract record Geometry;

public abstract record Curve : Geometry;

public record Plane3D(Vector3D Normal, Point3D Origin) : Geometry;

public record LineSegment(Point3D Start, Point3D End) : Curve;

public record ArcSegment(
    Point3D Center,
    double Radius,
    double StartAngle,
    double SweepAngle,
    Vector3D Normal
) : Curve;

public record Point3D(double X, double Y, double Z) : Geometry, IXYZ;

public record Vector3D(double X, double Y, double Z) : Geometry, IXYZ;

public record Matrix3x4(
    Vector3D XAxis,
    Vector3D YAxis,
    Vector3D ZAxis,
    Vector3D Translation,
    IReadOnlyList<double> Components,
    double M11,
    double M12,
    double M13,
    double M21,
    double M22,
    double M23,
    double M31,
    double M32,
    double M33,
    double Tx,
    double Ty,
    double Tz
);

public record CoordinateSystem3D(Vector3D XAxis, Vector3D YAxis, Vector3D ZAxis, Point3D Origin)
{
    public static implicit operator CoordinateSystem3D(Matrix3x4 matrix) =>
        CoordinateSystem.ByMatrix(matrix);

    public static implicit operator Matrix3x4(CoordinateSystem3D coordinateSystem) =>
        Matrix.ByCoordinateSystem(coordinateSystem);
};
