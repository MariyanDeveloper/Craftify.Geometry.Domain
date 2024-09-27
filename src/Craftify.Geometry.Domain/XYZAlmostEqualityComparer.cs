namespace Craftify.Geometry.Domain;

#pragma warning disable S101
public class XYZAlmostEqualityComparer(double tolerance = Defaults.Tolerance) : IEqualityComparer<IXYZ>
#pragma warning restore S101
{
    private const int InitialSeedValue = 17;
    private const int PrimeMultiplier = 23;

    private readonly double _tolerance = tolerance;

    public bool Equals(IXYZ x, IXYZ y)
    {
        return x.AlmostEqualTo(y, _tolerance);
    }

    public int GetHashCode(IXYZ xyz)
    {
        int xHash = xyz.X.RoundToTolerance(_tolerance).GetHashCode();
        int yHash = xyz.Y.RoundToTolerance(_tolerance).GetHashCode();
        int zHash = xyz.Z.RoundToTolerance(_tolerance).GetHashCode();

        int hash = InitialSeedValue;
        hash = hash * PrimeMultiplier + xHash;
        hash = hash * PrimeMultiplier + yHash;
        hash = hash * PrimeMultiplier + zHash;

        return hash;
    }
}
