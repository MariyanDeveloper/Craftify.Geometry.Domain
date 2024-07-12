namespace Craftify.Geometry.Domain;

public static class LineExtensions
{
    public static Point3D Evaluate(this Line line, double t) => new Point3D(
        line.Start.X + t * (line.End.X - line.Start.X),
        line.Start.Y + t * (line.End.Y - line.Start.Y),
        line.Start.Z + t * (line.End.Z - line.Start.Z)
    );
}