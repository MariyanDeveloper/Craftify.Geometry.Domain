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
