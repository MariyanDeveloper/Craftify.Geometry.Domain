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
}
