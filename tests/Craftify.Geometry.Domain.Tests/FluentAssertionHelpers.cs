using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public static class FluentAssertionHelpers
{
    public static void ShouldBeApproximatelyEquivalentTo<T>(
        this T actual,
        T expected,
        double tolerance = 1e-3
    )
    {
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                options =>
                    options
                        .Using<double>(ctx =>
                            ctx.Subject.Should().BeApproximately(ctx.Expectation, tolerance)
                        )
                        .WhenTypeIs<double>()
            );
    }
}
