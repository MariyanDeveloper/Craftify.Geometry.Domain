namespace Craftify.Geometry.Domain;

public record LineSegment(Point3D Start, Point3D End) : Curve;