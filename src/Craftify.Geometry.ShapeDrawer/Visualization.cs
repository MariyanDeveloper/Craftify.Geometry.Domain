using Craftify.Geometry.Domain;

namespace Craftify.Geometry.ShapeDrawer;

public static class Visualization
{
    public static VisualizationType CreatePoint(
        Point3D point,
        CreatePointArguments? pointArguments = default
    )
    {
        pointArguments ??= CreatePointArguments.Default;
        return new PointVisualizationType(point, pointArguments.Paint, pointArguments.PointRadius);
    }

    public static VisualizationType CreatePoints(params Point3D[] points)
    {
        var pointVisualizations = points
            .Select(point => new PointVisualizationType(
                point,
                Style.ForPoint(),
                Defaults.Geometry.PointRadius
            ))
            .ToArray();
        return new CompositeVisualizationType(pointVisualizations);
    }

    public static VisualizationType CreateSmoothLine(LineSegment line)
    {
        return new LineVisualizationType(line, VisualizeLineAs.Line, Style.ForSmoothLine());
    }

    public static VisualizationType CreateSharpLine(LineSegment line)
    {
        return new LineVisualizationType(line, VisualizeLineAs.Line, Style.ForSharpLine());
    }

    public static VisualizationType CreateSmoothLines(params LineSegment[] lines)
    {
        var lineVisualizations = lines
            .Select(line => new LineVisualizationType(
                line,
                VisualizeLineAs.Path,
                Style.ForSmoothLine()
            ))
            .ToArray();
        return new CompositeVisualizationType(lineVisualizations);
    }

    public static VisualizationType CreateComposite(params VisualizationType[] visualizations)
    {
        return new CompositeVisualizationType(visualizations);
    }

    public static VisualizationType CreateArc(ArcSegment arc)
    {
        return new ArcVisualizationType(arc, Style.ForSharpLine());
    }

    public static VisualizationType Compose(this IEnumerable<VisualizationType> visualizations) =>
        new CompositeVisualizationType(visualizations.ToList());
}
