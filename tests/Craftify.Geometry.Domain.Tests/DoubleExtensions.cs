using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class DoubleExtensionsTests
{
    [Fact]
    public void AlmostEqualTo_ShouldReturnTrueForValuesWithinTolerance()
    {
        var value1 = 1.000001;
        var value2 = 1.000002;
        var tolerance = 1e-5;

        var result = value1.AlmostEqualTo(value2, tolerance);

        result
            .Should()
            .BeTrue(
                because: "the difference between 1.000001 and 1.000002 is within the tolerance of 1e-5"
            );
    }

    [Fact]
    public void AlmostEqualTo_ShouldReturnFalseForValuesOutsideTolerance()
    {
        var value1 = 1.0;
        var value2 = 1.1;
        var tolerance = 1e-5;

        var result = value1.AlmostEqualTo(value2, tolerance);

        result
            .Should()
            .BeFalse(
                because: "the difference between 1.0 and 1.1 is not within the tolerance of 1e-5"
            );
    }
}
