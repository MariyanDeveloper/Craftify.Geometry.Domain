namespace Craftify.Geometry.Domain;

public static class CurveGraphExtensions
{
    public static IReadOnlyList<Point3D> DistinctNodes(this CurveGraph graph)
    {
        return ReadOnlyList.Of(graph.Edges
            .SelectMany(edge => edge.EndPoints())
            .Distinct(new XYZAlmostEqualityComparer())
            .Cast<Point3D>());
    }

    public static Curve? CommonEdge(
       this CurveGraph graph,
       Point3D start,
       Point3D end)
    {
        return graph.Edges
            .FirstOrDefault(edge =>
                edge.HasEndPoint(start)
                && edge.HasEndPoint(end));
    }

    public static bool Exists(this CurveGraph graph, Point3D node)
    {
        return graph.Edges.Any(edge => edge.HasEndPoint(node));
    }
}
