namespace Craftify.Geometry.Domain;

public static class ToDesignScriptTranslation
{
    public static string AsDesignScript(this Point3D point)
    {
        return $"Point.ByCoordinates({point.X}, {point.Y}, {point.Z})";
    }

    public static string AsDesignScript(this LineSegment line)
    {
        return $"Line.ByStartPointEndPoint({line.Start.AsDesignScript()}, {line.End.AsDesignScript()})";
    }

    public static string AsArrayInDesignScript(this IEnumerable<LineSegment> lineSegments)
    {
        var lineSegmentsParsed = lineSegments.Select(AsDesignScript);
        return $"[{string.Join($",{Environment.NewLine}", lineSegmentsParsed)}]";
    }

    public static string AsSeperatedInDesignScript(this IEnumerable<LineSegment> lineSegments)
    {
        var lineSegmentsParsed = lineSegments.Select(AsDesignScript);
        return string.Join($";{Environment.NewLine}", lineSegmentsParsed);
    }

    public static string AsDesignScript(this CoordinateSystem3D coordinateSystem)
    {
        return $"""
            CoordinateSystem.ByOriginVectors(
                 {coordinateSystem.Origin.AsDesignScript()},
                 {coordinateSystem.XAxis.AsDesignScript()},
                 {coordinateSystem.YAxis.AsDesignScript()},
                 {coordinateSystem.ZAxis.AsDesignScript()}
            );
            """;
    }

    public static string AsDesignScript(this Vector3D vector)
    {
        return $"Vector.ByCoordinates({vector.X}, {vector.Y}, {vector.Z})";
    }
}
