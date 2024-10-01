using Craftify.Geometry.Domain;

namespace Craftify.Geometry.ShapeDrawer;

public static class Visualization
{
    public static VisualizeFunction CreatePoint(
        Point3D point,
        CreatePointArguments? pointArguments = default
    )
    {
        pointArguments ??= CreatePointArguments.Default;
        var pointVisualizationType = new PointVisualizationType(
            point,
            pointArguments.Paint,
            pointArguments.PointRadius
        );

        return (canvas, context) =>
        {
            pointVisualizationType.DrawOnCanvas(canvas, context);
        };
    }

    public static VisualizeFunction CreatePoints(params Point3D[] points)
    {
        var pointVisualizations = points.Select(point => new PointVisualizationType(
            point,
            Style.ForPoint(),
            Defaults.Geometry.PointRadius
        ));
        return (canvas, context) =>
        {
            foreach (var pointVisualizationType in pointVisualizations)
            {
                pointVisualizationType.DrawOnCanvas(canvas, context);
            }
        };
    }

    public static VisualizeFunction CreateSmoothLine(LineSegment line)
    {
        var lineVisualizationType = new LineVisualizationType(
            line,
            VisualizeLineAs.Line,
            Style.ForSmoothLine()
        );
        return (canvas, context) =>
        {
            lineVisualizationType.DrawOnCanvas(canvas, context);
        };
    }

    public static VisualizeFunction CreateSharpLine(LineSegment line)
    {
        var lineVisualizationType = new LineVisualizationType(
            line,
            VisualizeLineAs.Line,
            Style.ForSharpLine()
        );
        return (canvas, context) =>
        {
            lineVisualizationType.DrawOnCanvas(canvas, context);
        };
    }

    public static VisualizeFunction CreateSmoothLines(params LineSegment[] lines)
    {
        var lineVisualizations = lines.Select(line => new LineVisualizationType(
            line,
            VisualizeLineAs.Path,
            Style.ForSmoothLine()
        ));
        return (canvas, context) =>
        {
            foreach (var lineVisualizationType in lineVisualizations)
            {
                lineVisualizationType.DrawOnCanvas(canvas, context);
            }
        };
    }

    public static VisualizeFunction CreateComposite(params VisualizeFunction[] visualizations)
    {
        return (canvas, context) =>
        {
            foreach (var visualizeFunction in visualizations)
            {
                visualizeFunction(canvas, context);
            }
        };
    }

    public static VisualizeFunction CreateArc(ArcSegment arc)
    {
        var arcVisualizationType = new ArcVisualizationType(arc, Style.ForSharpLine());
        return (canvas, context) =>
        {
            arcVisualizationType.DrawOnCanvas(canvas, context);
        };
    }

    public static VisualizeFunction Compose(
        this IEnumerable<VisualizeFunction> visualizeFunctions
    ) => CreateComposite(visualizeFunctions.ToArray());
}
