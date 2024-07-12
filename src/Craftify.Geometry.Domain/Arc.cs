namespace Craftify.Geometry.Domain;

public record Arc(Point3D Center, double Radius, double StartAngle, double EndAngle) : Curve;