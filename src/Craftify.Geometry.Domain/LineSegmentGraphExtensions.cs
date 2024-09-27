namespace Craftify.Geometry.Domain;

public static class LineSegmentGraphExtensions
{
    public static IReadOnlyList<Point3D> DistinctedNodes(this LineSegmentsGraph graph)
    {
        return ReadOnlyList.Of(graph.Edges
            .SelectMany(edge => new[] { edge.Start, edge.End })
            .Distinct(new XYZAlmostEqualityComparer())
            .Cast<Point3D>());
    }

    public static LineSegment? CommonEdge(
       this LineSegmentsGraph graph,
       Point3D start,
       Point3D end)
    {
        return graph.Edges
            .FirstOrDefault(edge =>
                edge.HasEndPoint(start)
                && edge.HasEndPoint(end));
    }

    public static bool Exists(this LineSegmentsGraph graph, Point3D node)
    {
        return graph.Edges.Any(edge => edge.HasEndPoint(node));
    }
}
