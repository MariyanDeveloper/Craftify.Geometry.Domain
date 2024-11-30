using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class GetCollinearEntireChainTo
{
    [Fact]
    public void WhenSingleElementIsPassed_ShouldReturnIt()
    {
        var lines = ReadOnlyList.Of(
            [Line.ByStartPointAndEndPoint(Point.Origin(), Point.ByCoordinates(2))]
        );
        var targetLine = Line.ByStartPointAndEndPoint(Point.Origin(), Point.ByCoordinates(2));
        var actualResult = lines.GetCollinearAndConnectedEntireChainTo(targetLine).ToArray();

        var expectedResult = ReadOnlyList.Of(
            [Line.ByStartPointAndEndPoint(Point.Origin(), Point.ByCoordinates(2))]
        );

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void ShouldReturnCollinearChain_WhenChainIsSplitAtMultipleSegments()
    {
        var lines = ReadOnlyList.Of(
            [
                Line.ByStartPointAndEndPoint(Point.Origin(), Point.ByCoordinates(2)),
                Line.ByStartPointAndEndPoint(Point.ByCoordinates(2), Point.ByCoordinates(6)),
                Line.ByStartPointAndEndPoint(Point.ByCoordinates(-5), Point.Origin()),
                Line.ByStartPointAndEndPoint(Point.ByCoordinates(6), Point.ByCoordinates(6, 5)),
                Line.ByStartPointAndEndPoint(Point.ByCoordinates(-5), Point.ByCoordinates(-5, 5)),
            ]
        );
        var targetLine = Line.ByStartPointAndEndPoint(
            Point.ByCoordinates(0),
            Point.ByCoordinates(2)
        );
        var actualResult = lines.GetCollinearAndConnectedEntireChainTo(targetLine).ToArray();

        var expectedResult = ReadOnlyList.Of(
            [
                Line.ByStartPointAndEndPoint(Point.Origin(), Point.ByCoordinates(2)),
                Line.ByStartPointAndEndPoint(Point.ByCoordinates(2), Point.ByCoordinates(6)),
                Line.ByStartPointAndEndPoint(Point.ByCoordinates(-5), Point.Origin()),
            ]
        );
        actualResult.Should().BeEquivalentTo(expectedResult);
    }
}
