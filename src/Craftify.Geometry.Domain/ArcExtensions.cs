namespace Craftify.Geometry.Domain;

public static class ArcExtensions
{
    public static Point3D Evaluate(this Arc arc, double parameter)
    {
        var angle = arc.StartAngle + parameter * (arc.EndAngle - arc.StartAngle);
        return new Point3D(
            arc.Center.X + arc.Radius * Math.Cos(angle),
            arc.Center.Y + arc.Radius * Math.Sin(angle),
            arc.Center.Z
        );
    }
}