using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class Point3DTests
{
    [Fact]
    public void DistanceTo_WhenExactMatch_ShouldCalculateCorrectDistance()
    {
        var point1 = new Point3D(1, 1, 0);
        var point2 = new Point3D(1, 7, 0);
        var result = point1.DistanceTo(point2);

        result.Should().Be(6);
    }
    
    [Fact]
    public void IsAlmostEqual_ShouldReturnTrueForPointsWithinTolerance()
    {
        var point1 = new Point3D(1, 1, 0);
        var point2 = new Point3D(1, 2, 0);

        var result = point1.AlmostEqualTo(point2, 1.1);

        result.Should().BeTrue();
    }
    

    [Fact]
    public void Translate_ShouldTranslatePointByVector()
    {
        var point = Point.ByCoordinates(1, 2, 3);
        var vector = new Vector3D(4, 5, 6);

        var result = point.Translate(vector);

        result.Should().BeEquivalentTo(new Point3D(5, 7, 9));
    }

    [Fact]
    public void Midpoint_ShouldCalculateCorrectMidpoint()
    {
        var point1 = new Point3D(1, 2, 3);
        var point2 = new Point3D(4, 5, 6);

        var result = point1.Midpoint(point2);

        result.Should().BeEquivalentTo(new Point3D(2.5, 3.5, 4.5));
    }
}