namespace Craftify.Geometry.Domain;

public static class CollinearLinesLookup
{
    public static IEnumerable<LineSegment> GetCollinearAndConnectedLinesTo(
        this IEnumerable<LineSegment> lines,
        LineSegment targetLine,
        double tolerance = Defaults.Tolerance
    )
    {
        return lines.Where(line =>
            line.IsCollinearTo(targetLine, tolerance) && line.IsConnectedTo(targetLine, tolerance)
        );
    }

    public static IEnumerable<LineSegment> GetCollinearAndConnectedEntireChainTo(
        this IEnumerable<LineSegment> lines,
        LineSegment targetLine,
        IReadOnlyList<LineSegment>? currentChain = default,
        double tolerance = Defaults.Tolerance
    )
    {
        currentChain ??= [];
        //TODO: mb we should pass a materialized collection so we don't need to copy it to an array
        var linesAsArray = lines.ToArray();
        var currentCollinearLines = linesAsArray
            .GetCollinearAndConnectedLinesTo(targetLine, tolerance)
            .Where(line => !currentChain.Any(chainLine => chainLine.AlmostEqualTo(line, tolerance)))
            .ToArray();
        var chain = ReadOnlyList.Of(currentChain, currentCollinearLines);
        var childrenSegments = currentCollinearLines.SelectMany(lineSegment =>
            linesAsArray.GetCollinearAndConnectedEntireChainTo(lineSegment, chain, tolerance)
        );
        return [.. currentCollinearLines, .. childrenSegments];
    }
}
