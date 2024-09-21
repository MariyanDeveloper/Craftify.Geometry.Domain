using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class ArcTests
{
    [Theory]
    [MemberData(nameof(GetPointAtParameterTestData))]
    public void GetPointAtParameter_ShouldReturnExpectedPointAtGivenParameter(
        double parameter,
        Point3D expectedPoint
    )
    {
        var arc = Arc.ByCenterPointRadiusEndAngle(Point.Origin(), 1, 0, 90, Vector.ZAxis());

        var actualPoint = arc.GetPointAtParameter(parameter);

        actualPoint.AlmostEqualTo(expectedPoint, 0.01).Should().BeTrue();
    }

    [Fact]
    public void GetStartEndPoints_ShouldReturnCorrectPoints()
    {
        var arc = Arc.ByCenterPointRadiusEndAngle(
            Point.ByCoordinates(3, 2, 4),
            12,
            90,
            270,
            Vector.ZAxis()
        );

        var startPoint = arc.GetStartPoint();
        var endPoint = arc.GetEndPoint();

        var expectedStartPoint = Point.ByCoordinates(3, 14, 4);
        var expectedEndPoint = Point.ByCoordinates(3, -10, 4);

        startPoint.AlmostEqualTo(expectedStartPoint).Should().BeTrue();
        endPoint.AlmostEqualTo(expectedEndPoint).Should().BeTrue();
    }

    public static IEnumerable<object[]> GetPointAtParameterTestData()
    {
        yield return [0.0, Point.ByCoordinates(1)];
        yield return [0.2, Point.ByCoordinates(0.951, 0.309)];
        yield return [0.5, Point.ByCoordinates(0.707, 0.707, 0)];
        yield return [0.7, Point.ByCoordinates(0.454, 0.891, 0)];
        yield return [1.0, Point.ByCoordinates(0, 1, 0)];
    }
}
