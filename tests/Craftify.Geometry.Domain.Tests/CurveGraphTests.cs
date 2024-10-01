using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class CurveGraphTests
{
    [Theory]
    [MemberData(nameof(DistinctNodesTestData))]
    public void DistinctNodes_ShouldDistinctNodes(
        CurveGraph graph,
        IReadOnlyList<Point3D> expected)
    {
        graph.DistinctNodes().Should().IntersectWith(expected);
    }

    public static IEnumerable<object[]> DistinctNodesTestData()
    {
        var graph = new CurveGraph(ReadOnlyList.Of(
        [
            Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(0.0000000001, 0.0000000001, 0.0000000001),
                Point.ByCoordinates(1, 1, 1)),

            Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(0.0000000002, 0.0000000002, 0.0000000002),
                Point.ByCoordinates(1, 1, 1)),

            Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(0.0000000003, 0.0000000003, 0.0000000003),
                Point.ByCoordinates(1, 1, 1)),

            Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(1.0000000001, 1.0000000001, 1.0000000001),
                Point.ByCoordinates(2, 2, 2)),

            Line.ByStartPointAndEndPoint(
                Point.ByCoordinates(1.0000000002, 1.0000000002, 1.0000000002),
                Point.ByCoordinates(2, 2, 2))
            ]
        ));

        yield return [
            graph,
            ReadOnlyList.Of(
                [
                    Point.ByCoordinates(0.0000000001, 0.0000000001, 0.0000000001),
                    Point.ByCoordinates(1, 1, 1),
                    Point.ByCoordinates(1.0000000001, 1.0000000001, 1.0000000001),
                    Point.ByCoordinates(2, 2, 2),
                ]
            )
        ];
    }
}
