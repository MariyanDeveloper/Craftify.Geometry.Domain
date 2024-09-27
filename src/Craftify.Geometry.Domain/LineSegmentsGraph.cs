namespace Craftify.Geometry.Domain;

public record LineSegmentsGraph(IReadOnlyList<LineSegment> Edges) : Graph;
