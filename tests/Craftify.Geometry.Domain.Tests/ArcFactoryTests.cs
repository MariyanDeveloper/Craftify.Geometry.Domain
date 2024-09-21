using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class ArcFactoryTests
{
    [Theory]
    [InlineData(0.0, 45.0, 0.0, 45.0)]
    [InlineData(-10.0, 90.0, 350.0, 100.0)]
    [InlineData(-370.0, 90.0, 350.0, 100.0)]
    [InlineData(-740.0, -350.0, 340.0, 30.0)]
    [InlineData(370.0, 90.0, 10.0, 80.0)]
    [InlineData(740.0, 350.0, 20.0, 330.0)]
    public void ByCenterPointRadiusEndAngle_ShouldCorrectlyCalculateStartAndSweepAngles(
        double startAngle,
        double endAngle,
        double expectedStartAngle,
        double expectedSweepAngle
    )
    {
        var actualArc = Arc.ByCenterPointRadiusEndAngle(
            Point.Origin(),
            5,
            startAngle,
            endAngle,
            Vector.ZAxis()
        );
        var expectedArc = Arc.ByCenterPointRadiusSweepAngle(
            Point.Origin(),
            5,
            expectedStartAngle,
            expectedSweepAngle,
            Vector.ZAxis()
        );
        actualArc.Should().BeEquivalentTo(expectedArc);
    }
}
