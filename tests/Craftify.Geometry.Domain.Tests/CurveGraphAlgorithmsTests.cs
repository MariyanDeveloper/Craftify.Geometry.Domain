using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class CurveGraphAlgorithmsTests
{
    static class GraphData
    {
        public static LineSegment FirstSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(8.88178419700125E-16, 1.22124532708767E-15, 0),
                Point.ByCoordinates(0, 5.00000000000001, 0));
        public static LineSegment SecondSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(1.22124532708768E-15, 5.00000000000001, 0),
                Point.ByCoordinates(-5.00000000000002, 5.00000000000003, 0));
        public static LineSegment ThirdSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(1.22124532708768E-15, 5.00000000000004, 0),
                Point.ByCoordinates(5.00000000000005, 5.00000000000006, 0));
        public static LineSegment FourthSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(5.00000000000007, 5.00000000000008, 0),
                Point.ByCoordinates(5.00000000000009, 10.00000000000001, 0));
        public static LineSegment FifthSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(-5.00000000000002, 5.00000000000003, 0),
                Point.ByCoordinates(-5.00000000000004, 10.00000000000005, 0));
        public static LineSegment SixthSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(5.00000000000006, 10.00000000000007, 0),
                Point.ByCoordinates(0.00000000000008, 10.00000000000009, 0));
        public static LineSegment SeventhSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(-5.00000000000001, 10.00000000000002, 0),
                Point.ByCoordinates(0.00000000000003, 10.00000000000004, 0));
        public static LineSegment EigthSegment => Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(0.00000000000005, 10.00000000000006, 0),
                Point.ByCoordinates(0.00000000000007, 15.00000000000008, 0));
    }

    [Theory]
    [MemberData(nameof(FindShortestPathTestData))]
    public void FindShortestPath_ShouldReturnShortestPath(
        CurveGraph graph,
        Point3D start,
        Point3D end,
        IReadOnlyList<LineSegment> expected)
    {
        var actualPath = graph.FindShortestPath(start, end).ToList();

        if (!expected.Any())
        {
            actualPath.Should().BeEmpty();
            return;
        }

        actualPath
            .Should()
            .HaveSameCount(expected)
            .And
            .AllSatisfy(edge =>
            {
                var index = actualPath.IndexOf(edge);
                edge.Start().AlmostEqualTo(expected[index].Start);
                edge.End().AlmostEqualTo(expected[index].End);
            });
    }

    public static IEnumerable<object[]> FindShortestPathTestData()
    {
        var graph = new CurveGraph(ReadOnlyList.Of(
            [
                GraphData.FirstSegment,
                GraphData.SecondSegment,
                GraphData.ThirdSegment,
                GraphData.FourthSegment,
                GraphData.FifthSegment,
                GraphData.SixthSegment,
                GraphData.SeventhSegment,
                GraphData.EigthSegment
            ]
        ));

        yield return [
            graph,
            Point.ByCoordinates(1, 1, 1),
            Point.Origin(),
            ReadOnlyList.Empty<LineSegment>()
        ];

        yield return [
            graph,
            Point.Origin(),
            Point.ByCoordinates(0, 15, 0),
            ReadOnlyList.Of(
                [
                    GraphData.FirstSegment,
                    GraphData.SecondSegment,
                    GraphData.FourthSegment,
                    GraphData.SeventhSegment,
                    GraphData.EigthSegment
                ]
            )
        ];

        yield return [
            graph,
            Point.ByCoordinates(0, 15, 0),
            Point.Origin(),
            ReadOnlyList.Of(
                [
                    GraphData.EigthSegment,
                    GraphData.SeventhSegment,
                    GraphData.FourthSegment,
                    GraphData.SecondSegment,
                    GraphData.FirstSegment
                ]
            )
        ];
    }
}
