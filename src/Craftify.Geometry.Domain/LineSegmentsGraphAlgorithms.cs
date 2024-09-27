namespace Craftify.Geometry.Domain;

public static class LineSegmentsGraphAlgorithms
{
    public static IReadOnlyList<LineSegment> Dijkstra(
        this LineSegmentsGraph graph, Point3D start, Point3D end)
    {
        if (!graph.Exists(start) || !graph.Exists(end))
        {
            return ReadOnlyList.Of<LineSegment>();
        }

        var nodes = graph.DistinctedNodes();
        var neighbors = nodes.Neighbors(graph);
        var costs = nodes.Costs(graph, start);
        var parents = nodes.Parents(graph, start);

        var processed = new List<Point3D>();
        var lowestCostNode = costs.LowestCostNode(processed);

        while (lowestCostNode is not null)
        {
            var cost = costs[lowestCostNode];
            var neighborsCosts = neighbors[lowestCostNode];

            foreach (var neighbor in neighborsCosts.Keys)
            {
                if (!processed.Contains(neighbor) && neighbor.AlmostEqualTo(start))
                {
                    continue;
                }

                var newCost = cost + neighborsCosts[neighbor];

                if (costs[neighbor] > newCost)
                {
                    costs[neighbor] = newCost;
                    parents[neighbor] = lowestCostNode;
                }
            }

            processed.Add(lowestCostNode);
            lowestCostNode = costs.LowestCostNode(processed);
        }

        return parents.BacktrackPath(start, end);
    }

    private static Dictionary<Point3D, Dictionary<Point3D, double>> Neighbors(
        this IEnumerable<Point3D> nodes, LineSegmentsGraph graph)
    {
        return nodes
           .ToDictionary(
               nodeKey => nodeKey,
               nodeValue => nodes
                    .Where(node => graph
                        .CommonEdge(node, nodeValue) is not null
                         && !node.AlmostEqualTo(nodeValue))
                    .ToDictionary(
                        neighborNodeKey => neighborNodeKey,
                        neighborNodeValue => graph
                            .CommonEdge(neighborNodeValue, nodeValue)
                            ?.GetLength()
                            ?? double.PositiveInfinity));
    }

    private static Dictionary<Point3D, double> Costs(
        this IEnumerable<Point3D> nodes, LineSegmentsGraph graph, Point3D start)
    {
        return nodes
            .Where(node => !node.AlmostEqualTo(start))
            .ToDictionary(
                nodeKey => nodeKey,
                nodeValue => graph
                    .CommonEdge(nodeValue, start)
                    ?.GetLength()
                    ?? double.PositiveInfinity);
    }

    private static Dictionary<Point3D, Point3D?> Parents(
        this IEnumerable<Point3D> nodes, LineSegmentsGraph graph, Point3D start)
    {
        return nodes
            .Where(node => !node.AlmostEqualTo(start))
            .ToDictionary(
                nodeKey => nodeKey,
                nodeValue => graph
                    .CommonEdge(nodeValue, start) is not null
                    ? start
                    : null);
    }

    private static Point3D? LowestCostNode(
        this IDictionary<Point3D, double> costs, List<Point3D> processed)
    {
        return costs
            .Where(cost => !processed.Contains(cost.Key))
            .OrderBy(cost => cost.Value)
            .FirstOrDefault()
            .Key;
    }

    private static IReadOnlyList<LineSegment> BacktrackPath(
        this IDictionary<Point3D, Point3D?> parents,
        Point3D start,
        Point3D end,
        List<LineSegment>? nonBacktrackedPath = default)
    {
        nonBacktrackedPath ??= [];
        var current = end;

        foreach (var parentChildPair in parents)
        {
            if (parentChildPair.Value is null)
            {
                continue;
            }

            if (parentChildPair.Key.AlmostEqualTo(current))
            {
                nonBacktrackedPath.Add(new LineSegment(parentChildPair.Value, current));
                current = parentChildPair.Value;
                break;
            }
        }

        if (current.AlmostEqualTo(start))
        {
            nonBacktrackedPath.Reverse();
            return ReadOnlyList.Of<LineSegment>(nonBacktrackedPath);
        }

        return parents.BacktrackPath(start, current, nonBacktrackedPath);
    }
}
