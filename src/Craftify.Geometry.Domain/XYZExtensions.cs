namespace Craftify.Geometry.Domain;

public static class XYZExtensions
{
    public static bool AlmostEqualTo(this IXYZ xyz, IXYZ other, double tolerance = 1e-6)
    {
        return Math.Abs(xyz.X - other.X) < tolerance &&
               Math.Abs(xyz.Y - other.Y) < tolerance &&
               Math.Abs(xyz.Z - other.Z) < tolerance;
    }
}