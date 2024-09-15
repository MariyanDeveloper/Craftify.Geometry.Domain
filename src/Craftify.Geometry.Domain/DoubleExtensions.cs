namespace Craftify.Geometry.Domain;

public static class DoubleExtensions
{
    public static double Square(this double self) => Math.Pow(self, 2);

    public static double SubtractFrom(this double self, double from) => from - self;

    public static double ToAbsolute(this double self) => Math.Abs(self);

    public static bool IsNegative(this double self) => self < 0;

    public static double NormalizeAngle(this double self)
    {
        var fullCircleDegrees = Constants.Degrees.FullCircle;
        var angleWithFullCircleRange = self % fullCircleDegrees;
        return angleWithFullCircleRange.IsNegative()
            ? angleWithFullCircleRange + fullCircleDegrees
            : angleWithFullCircleRange;
    }

    public static bool AlmostEqualTo(
        this double from,
        double to,
        double tolerance = Defaults.Tolerance
    )
    {
        return to.SubtractFrom(from).ToAbsolute() < tolerance;
    }
}
