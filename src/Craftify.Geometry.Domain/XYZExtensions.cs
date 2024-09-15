namespace Craftify.Geometry.Domain;

public static class XYZExtensions
{
    public static bool AlmostEqualTo(
        this IXYZ xyz,
        IXYZ other,
        double tolerance = Defaults.Tolerance
    )
    {
        return xyz.X.AlmostEqualTo(other.X, tolerance)
            && xyz.Y.AlmostEqualTo(other.Y, tolerance)
            && xyz.Z.AlmostEqualTo(other.Z, tolerance);
    }
}
