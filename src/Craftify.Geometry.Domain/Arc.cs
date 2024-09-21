namespace Craftify.Geometry.Domain;

public static class Arc
{
    public static ArcSegment ByCenterPointRadiusSweepAngle(
        Point3D center,
        double radius,
        double startAngle,
        double sweepAngle,
        Vector3D normal
    )
    {
        return new(
            center,
            radius,
            startAngle.NormalizeAngle(),
            sweepAngle.NormalizeAngle(),
            normal
        );
    }

    public static ArcSegment ByCenterPointRadiusEndAngle(
        Point3D center,
        double radius,
        double startAngle,
        double endAngle,
        Vector3D normal
    )
    {
        var normalizedStartAngle = startAngle.NormalizeAngle();
        var normalizedEndAngle = endAngle.NormalizeAngle();
        var angleDifference = normalizedEndAngle - normalizedStartAngle;
        var sweepAngle =
            angleDifference < 0 ? angleDifference + Constants.Degrees.FullCircle : angleDifference;
        return ByCenterPointRadiusSweepAngle(
            center,
            radius,
            normalizedStartAngle,
            sweepAngle,
            normal
        );
    }
}
