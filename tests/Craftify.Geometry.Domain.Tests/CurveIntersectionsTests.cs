using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class CurveIntersectionsTests
{
    [Fact]
    public void Intersects_ShouldReturnIntersectionPoint_WhenLinesNotParallelAndHaveSingleIntersectionPoint()
    {
        var firstLine = Line.ByStartPointAndEndPoint(
            Point.Origin(), Point.ByCoordinates(0, 0, 10));
        var secondLine = Line.ByStartPointAndEndPoint(
            Point.ByCoordinates(-5, 0, 5), Point.ByCoordinates(5, 0, 5));

        var intersection = firstLine.Intersects(secondLine);

        intersection.Should().BeEquivalentTo(ReadOnlyList.Of(Point.ByCoordinates(0, 0, 5)));
    }

    [Fact]
    public void Intersects_ShouldReturnEmptyCollection_WhenNoIntersectionPoint()
    {
        var firstLine = Line.ByStartPointAndEndPoint(
            Point.Origin(), Point.ByCoordinates(5, 0, 0));
        var secondLine = Line.ByStartPointAndEndPoint(
            Point.ByCoordinates(2.5, -5, 1), Point.ByCoordinates(2.5, 5, 1));

        var intersection = firstLine.Intersects(secondLine);

        intersection.Should().BeEquivalentTo(ReadOnlyList.Empty<Point3D>());
    }

    [Fact]
    public void Intersects_ShouldReturnCommonEndPoint_WhenHaveCommonEndPoint()
    {
        var firstLine = Line.ByStartPointAndEndPoint(
            Point.Origin(), Point.ByCoordinates(0, 0, 10));
        var secondLine = Line.ByStartPointAndEndPoint(
            Point.ByCoordinates(0, 0, 10), Point.ByCoordinates(5, 5, 12));

        var intersection = firstLine.Intersects(secondLine);

        intersection.Should().BeEquivalentTo(ReadOnlyList.Of(Point.ByCoordinates(0, 0, 10)));
    }

    [Fact]
    public void Intersects_ShouldReturnIntersectionPoint_WhenParallelAndHaveSingleIntersectionPoint()
    {
        var firstLine = Line.ByStartPointAndEndPoint(
            Point.Origin(), Point.ByCoordinates(0, 0, 10));
        var secondLine = Line.ByStartPointAndEndPoint(
            Point.ByCoordinates(0, 0, 10), Point.ByCoordinates(0, 0, 12));

        var intersection = firstLine.Intersects(secondLine);

        intersection.Should().BeEquivalentTo(ReadOnlyList.Of(Point.ByCoordinates(0, 0, 10)));
    }

    [Fact]
    public void Intersects_ShouldReturnOverlappingRange_WhenParallelAndCollinear()
    {
        var firstLine = Line.ByStartPointAndEndPoint(
            Point.Origin(), Point.ByCoordinates(0, 0, 10));
        var secondLine = Line.ByStartPointAndEndPoint(
            Point.ByCoordinates(0, 0, 3), Point.ByCoordinates(0, 0, 12));

        var intersection = firstLine.Intersects(secondLine);

        intersection.Should().BeEquivalentTo(ReadOnlyList.Of(
            [Point.ByCoordinates(0, 0, 3), Point.ByCoordinates(0, 0, 10)]));
    }
}
