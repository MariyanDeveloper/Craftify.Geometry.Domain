namespace Craftify.Geometry.Domain;

#pragma warning disable S101
public static class XYZExtensions
#pragma warning restore S101
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
