namespace Craftify.Geometry.Domain;

public static class ArcEvaluation
{
    public static Point3D GetPointAtParameter(this Arc arc, double parameter)
    {
        var angle = arc.StartAngle + parameter * (arc.EndAngle - arc.StartAngle);
        return Point.ByCoordinates(
            arc.Center.X + arc.Radius * Math.Cos(angle),
            arc.Center.Y + arc.Radius * Math.Sin(angle),
            arc.Center.Z
        );
    }
}