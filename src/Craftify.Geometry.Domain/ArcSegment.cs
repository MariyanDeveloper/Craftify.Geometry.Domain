namespace Craftify.Geometry.Domain;

public record ArcSegment(
    Point3D Center,
    double Radius,
    double StartAngle,
    double SweepAngle,
    Vector3D Normal
) : Curve;
