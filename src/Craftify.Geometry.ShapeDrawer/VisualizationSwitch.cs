namespace Craftify.Geometry.ShapeDrawer;

public static class VisualizationSwitch
{
    public static void Switch(
        this VisualizationType visualizationType,
        Action<PointVisualizationType> point,
        Action<LineVisualizationType> line,
        Action<ArcVisualizationType> arc,
        Action<CompositeVisualizationType> composite
    )
    {
        _ = visualizationType switch
        {
            LineVisualizationType lineVisualization => line.ToFunc().Invoke(lineVisualization),
            PointVisualizationType pointVisualization => point.ToFunc().Invoke(pointVisualization),
            ArcVisualizationType arcVisualization => arc.ToFunc().Invoke(arcVisualization),
            CompositeVisualizationType compositeVisualizationType => composite
                .ToFunc()
                .Invoke(compositeVisualizationType),
            _ => throw new ArgumentOutOfRangeException(nameof(visualizationType)),
        };
    }

    public static Func<T, ValueTuple> ToFunc<T>(this Action<T> action)
    {
        return (t) =>
        {
            action(t);
            return default(ValueTuple);
        };
    }
}