namespace Craftify.Geometry.Domain;

public record Line(Point3D Start, Point3D End)
{
    public Point3D Evaluate(double t) => new Point3D(
        Start.X + t * (End.X - Start.X),
        Start.Y + t * (End.Y - Start.Y),
        Start.Z + t * (End.Z - Start.Z)
    );
}