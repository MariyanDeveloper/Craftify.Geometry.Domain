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

    [Theory]
    [MemberData(nameof(IntersectsTestData))]
    public void Intersects_ShouldReturnCorrectIntersectionPoint(
        LineSegment mainLine,
        LineSegment otherLine,
        bool expectedIntersected,
        Point3D? expectedIntersectionPoint)
    {
        bool actualIntersected = mainLine.Intersects(otherLine, out Point3D? actualIntersectionPoint);

        actualIntersected.Should().Be(expectedIntersected);
        actualIntersectionPoint.Should().Be(expectedIntersectionPoint);
    }

    public static IEnumerable<object[]> IntersectsTestData()
    {
        yield return
        [
            new LineSegment(new Point3D(0, 0, 0), new Point3D(1, 1, 0)),
            new LineSegment(new Point3D(0, 1, 0), new Point3D(1, 0, 0)),
            true,
            new Point3D(0.5, 0.5, 0)
        ];

        yield return
        [
            new LineSegment(new Point3D(0, 0, 0), new Point3D(1, 0, 0)),
            new LineSegment(new Point3D(0, 1, 0), new Point3D(1, 1, 0)),
            false,
            null
        ];

        yield return
        [
            new LineSegment(new Point3D(0, 0, 0), new Point3D(1, 1, 0)),
            new LineSegment(new Point3D(2, 2, 0), new Point3D(3, 3, 0)),
            false,
            null
        ];
    }
}
