using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class Vector3DTests
{
    [Fact]
    public void Magnitude_ShouldCorrectlyCalculate()
    {
        var vector = Vector.ByCoordinates(4, 3);
        var magnitude = vector.Magnitude();
        magnitude.Should().Be(5);
    }

    [Fact]
    public void AngleTo_ShouldReturn90Degrees_WhenXAxisAndYAxisArePerpendicular()
    {
        var xAxis = Vector.XAxis();
        var yAxis = Vector.YAxis();

        var angle = xAxis.AngleTo(yAxis);

        angle.Should().Be(90);
    }

    [Fact]
    public void AngleTo_ShouldReturn90Degrees_WhenXAxisAndNegativeYAxisArePerpendicular()
    {
        var xAxis = Vector.XAxis();
        var yAxis = Vector.YAxis().Negate();

        var angle = xAxis.AngleTo(yAxis);

        angle.Should().Be(90);
    }

    [Fact]
    public void AngleAboutAxis_ShouldReturn90Degrees_WhenXAxisAndYAxisAreMeasuredAboutZAxis()
    {
        var vector1 = Vector.XAxis();
        var vector2 = Vector.YAxis();
        var rotationAxis = Vector.ZAxis();
        var angle = vector1.AngleAboutAxis(vector2, rotationAxis);
        angle.Should().Be(90);
    }

    [Fact]
    public void AngleAboutAxis_ShouldReturn270Degrees_WhenYAxisAndXAxisAreMeasuredAboutZAxis()
    {
        var vector1 = Vector.XAxis();
        var vector2 = Vector.YAxis();
        var rotationAxis = Vector.ZAxis();
        var angle = vector2.AngleAboutAxis(vector1, rotationAxis);
        angle.Should().Be(270);
    }
}