namespace Craftify.Geometry.Domain;

public static class DoubleExtensions
{
    public static double Square(this double value) => Math.Pow(value, 2);

    public static double SubtractFrom(this double self, double from) => from - self;

    public static double ToAbsolute(this double self) => Math.Abs(self);

    public static bool AlmostEqualTo(
        this double from,
        double to,
        double tolerance = Defaults.Tolerance
    )
    {
        return to.SubtractFrom(from).ToAbsolute() < tolerance;
    }

    public static bool IsCloseToZero(this double value) => Math.Abs(value) < Defaults.Tolerance;
}
