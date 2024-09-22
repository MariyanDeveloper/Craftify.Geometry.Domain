using Craftify.Geometry.Domain;
using SkiaSharp;

namespace Craftify.Geometry.ShapeDrawer;

public abstract record VisualizationType;

public enum VisualizeLineAs
{
    Path,
    Line,
}

public record LineVisualizationType(
    LineSegment LineSegment,
    VisualizeLineAs VisualizeLineAs,
    SKPaint Paint
) : VisualizationType;

public record ArcVisualizationType(ArcSegment ArcSegment, SKPaint Paint) : VisualizationType;

public record PointVisualizationType(Point3D Point, SKPaint Paint, float Radius)
    : VisualizationType;
