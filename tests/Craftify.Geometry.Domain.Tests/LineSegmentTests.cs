using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class LineSegmentTests
{
    [Theory]
    [MemberData(nameof(GetPointAtParameterTestData))]
    public void GetPointAtParameter_ShouldReturnExpectedPointAtGivenParameter(
        double parameter,
        Point3D expectedPoint
    )
    {
        var line = Line.ByStartPointAndEndPoint(
            Point.ByCoordinates(2, 0),
            Point.ByCoordinates(10, 0)
        );
        var pointAtParameter = line.GetPointAtParameter(parameter);

        pointAtParameter.Should().Be(expectedPoint);
    }

    public static IEnumerable<object[]> GetPointAtParameterTestData()
    {
        yield return [0, Point.ByCoordinates(2)];
        yield return [0.5, Point.ByCoordinates(6)];
        yield return [1, Point.ByCoordinates(10)];
    }
}
