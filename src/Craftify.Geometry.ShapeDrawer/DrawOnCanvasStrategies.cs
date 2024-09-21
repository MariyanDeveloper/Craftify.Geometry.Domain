using Craftify.Geometry.Domain;
using SkiaSharp;

namespace Craftify.Geometry.ShapeDrawer;

public static class DrawOnCanvasStrategies
{
    public static void DrawOnCanvas(
        this VisualizationType visualizationType,
        SKCanvas canvas,
        DrawingContext drawingContext
    )
    {
        visualizationType.Switch(
            point: pointVisualization => pointVisualization.DrawOnCanvas(canvas, drawingContext),
            line: lineVisualization => lineVisualization.DrawOnCanvas(canvas, drawingContext),
            arc: arcVisualization => arcVisualization.DrawOnCanvas(canvas, drawingContext),
            composite: compositeVisualization =>
                compositeVisualization.DrawOnCanvas(canvas, drawingContext)
        );
    }

    public static void DrawOnCanvas(
        this ArcVisualizationType arcVisualizationType,
        SKCanvas canvas,
        DrawingContext drawingContext
    )
    {
        var arc = arcVisualizationType.ArcSegment;
        var (left, top, right, bottom) = arc.AdaptGeometryToStartFromLeftBottomCornerOnCanvas(
                drawingContext.Height
            )
            .CalculateBoundingRectangle();
        var oval = new SKRect(left, top, right, bottom);
        //we need to negate start and sweep angles because by default they go in a clockwise direction
        canvas.DrawArc(
            oval,
            -(float)arc.StartAngle,
            -(float)arc.SweepAngle,
            false,
            arcVisualizationType.Paint
        );
    }

    public static void DrawOnCanvas(
        this CompositeVisualizationType compositeVisualizationType,
        SKCanvas canvas,
        DrawingContext drawingContext
    )
    {
        foreach (var visualization in compositeVisualizationType.VisualizationTypes)
        {
            visualization.DrawOnCanvas(canvas, drawingContext);
        }
    }

    public static void DrawOnCanvas(
        this PointVisualizationType pointVisualizationType,
        SKCanvas canvas,
        DrawingContext drawingContext
    )
    {
        var (point, paint, radius) = pointVisualizationType;
        var skPoint = point
            .AdaptGeometryToStartFromLeftBottomCornerOnCanvas(drawingContext.Height)
            .MapTo2DSkPoint();
        canvas.DrawCircle(skPoint, radius, paint);
    }

    public static void DrawOnCanvas(
        this LineVisualizationType lineVisualizationType,
        SKCanvas canvas,
        DrawingContext drawingContext
    )
    {
        var (lineSegment, visualizeLineAs, paint) = lineVisualizationType;
        var segment = lineSegment.AdaptGeometryToStartFromLeftBottomCornerOnCanvas(
            drawingContext.Height
        );
        if (visualizeLineAs is VisualizeLineAs.Line)
        {
            canvas.DrawLine(segment.Start.MapTo2DSkPoint(), segment.End.MapTo2DSkPoint(), paint);
            return;
        }

        var pathSegment = segment.MapToSKPath();
        canvas.DrawPath(pathSegment, paint);
    }

    private static (float Left, float Top, float Right, float Bottom) CalculateBoundingRectangle(
        this ArcSegment arc
    )
    {
        var center = arc.Center;
        var radius = arc.Radius;
        var left = (float)(center.X - radius);
        var top = (float)(center.Y - radius);
        var right = (float)(center.X + radius);
        var bottom = (float)(center.Y + radius);
        return (left, top, right, bottom);
    }
}
