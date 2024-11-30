using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class Point3DTests
{
    [Fact]
    public void DistanceTo_WhenExactMatch_ShouldCalculateCorrectDistance()
    {
        var point1 = Point.ByCoordinates(1, 1, 0);
        var point2 = Point.ByCoordinates(1, 7, 0);
        var result = point1.DistanceTo(point2);

        result.Should().Be(6);
    }

    [Fact]
    public void IsAlmostEqual_ShouldReturnTrueForPointsWithinTolerance()
    {
        var point1 = Point.ByCoordinates(1, 1, 0);
        var point2 = Point.ByCoordinates(1, 2, 0);

        var result = point1.AlmostEqualTo(point2, 1.1);

        result.Should().BeTrue();
    }

    [Fact]
    public void Translate_ShouldTranslatePointByVector()
    {
        var point = Point.ByCoordinates(1, 2, 3);
        var vector = Vector.ByCoordinates(4, 5, 6);

        var result = point.Translate(vector);

        result.Should().BeEquivalentTo(Point.ByCoordinates(5, 7, 9));
    }

    [Fact]
    public void Midpoint_ShouldCalculateCorrectMidpoint()
    {
        var point1 = Point.ByCoordinates(1, 2, 3);
        var point2 = Point.ByCoordinates(4, 5, 6);

        var result = point1.Midpoint(point2);

        result.Should().BeEquivalentTo(Point.ByCoordinates(2.5, 3.5, 4.5));
    }

    [Theory]
    [MemberData(nameof(MeasureSignedDistanceToPointAlongVectorTestData))]
    public void MeasureSignedDistanceToPointAlongVector_ShouldMeasureNonAbsoluteDistance(
        Point3D source, Point3D destination, Vector3D vector, double expected)
    {
        source
            .MeasureSignedDistanceToPointAlongVector(destination, vector)
            .Should()
            .BeApproximately(expected, Defaults.Tolerance);
    }

    public static IEnumerable<object[]> MeasureSignedDistanceToPointAlongVectorTestData()
    {
        yield return [
            Point.Origin(),
            Point.Origin(),
            Vector.XAxis(),
            0
        ];

        yield return [
            Point.Origin(),
            Point.ByCoordinates(1, 0, 0),
            Vector.XAxis(),
            1
        ];

        yield return [
            Point.Origin(),
            Point.ByCoordinates(1, 0, 0),
            Vector.XAxis().Negate(),
            -1
        ];
    }
}
