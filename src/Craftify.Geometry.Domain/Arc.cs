namespace Craftify.Geometry.Domain;

public record Arc(Point3D Center, double Radius, double StartAngle, double EndAngle) : Curve
{
    public Point3D Evaluate(double t)
    {
        var angle = StartAngle + t * (EndAngle - StartAngle);
        return new Point3D(
            Center.X + Radius * Math.Cos(angle),
            Center.Y + Radius * Math.Sin(angle),
            Center.Z
        );
    }
}