namespace Craftify.Geometry.Domain;

public static class CurveIntersections
{
    public static IReadOnlyList<Point3D> Intersects(
        this Curve curve,
        Curve other,
        double tolerance = Defaults.Tolerance)
    {
        return (curve, other) switch
        {
            (LineSegment lineSegment1, LineSegment lineSegment2)
                => lineSegment1.Intersects(lineSegment2, tolerance),
            _ => throw new NotSupportedException()
        };
    }

    private static IReadOnlyList<Point3D> Intersects(
        this LineSegment main,
        LineSegment other,
        double tolerance)
    {
        if (main.AlmostEqualTo(other))
        {
            return ReadOnlyList.OfSequence(main.Start, main.End);
        }

        if (main.HasEndPoint(other.Start, tolerance))
        {
            return ReadOnlyList.Of(other.Start);
        }

        if (main.HasEndPoint(other.End, tolerance))
        {
            return ReadOnlyList.Of(other.End);
        }

        var startDelta = Vector.ByTwoPoints(main.Start, other.Start);
        var mainDirection = main.GetDirection();
        var otherDirection = other.GetDirection();
        var directionVectorsCrossProduct = mainDirection.CrossProduct(otherDirection);
        var squareDirectionVectorsCrossProduct = directionVectorsCrossProduct
            .DotProduct(directionVectorsCrossProduct);

        var areCoplanar = startDelta.DotProduct(directionVectorsCrossProduct).AlmostEqualTo(0, tolerance);

        if (!areCoplanar)
        {
            return [];
        }

        var areParallel = directionVectorsCrossProduct.AlmostEqualTo(Vector.Zero(), tolerance);

        if (!areParallel)
        {
            double s = startDelta
                        .CrossProduct(otherDirection)
                        .DotProduct(directionVectorsCrossProduct)
                        / squareDirectionVectorsCrossProduct;

            double t = startDelta
                        .CrossProduct(mainDirection)
                        .DotProduct(directionVectorsCrossProduct)
                        / squareDirectionVectorsCrossProduct;

            return (s > 0 && s < 1 && t > 0 && t < 1)
                ? ReadOnlyList.Of(main.Start.AsVector().Add(mainDirection.Multiply(s)).AsPoint())
                : [];
        }

        var startDeltaAndMainDirectionCrossProduct = startDelta
            .CrossProduct(mainDirection);

        var areParallelAndNoOverlapping = startDeltaAndMainDirectionCrossProduct
            .AlmostEqualTo(Vector.Zero(), tolerance);

        if (!areParallelAndNoOverlapping)
        {
            return [];
        }

        var mainDirectionSquareLength = mainDirection.DotProduct(mainDirection);
        var s0 = mainDirection.DotProduct(startDelta) / mainDirectionSquareLength;
        var s1 = s0 + mainDirection.DotProduct(otherDirection) / mainDirectionSquareLength;

        var overlappingRange = GetOverlappingRange(
            0.0d, 1.0d, Math.Min(s0, s1), Math.Max(s0, s1));

        return ReadOnlyList.Of(overlappingRange.Select(
            parameter => main.Start.AsVector().Add(mainDirection.Multiply(parameter)).AsPoint()));
    }

    private static IReadOnlyList<double> GetOverlappingRange(
        double firstIntervalStart,
        double firstIntervalEnd,
        double secondIntervalStart,
        double secondIntervalEnd)
    {
        if (firstIntervalEnd < secondIntervalStart || firstIntervalStart > secondIntervalEnd)
        {
            return [];
        }

        var output = new List<double>();

        if (firstIntervalEnd > secondIntervalStart)
        {
            if (firstIntervalStart < secondIntervalEnd)
            {
                if (firstIntervalStart < secondIntervalStart)
                {
                    output.Add(secondIntervalStart);
                }

                if (firstIntervalStart > secondIntervalStart)
                {
                    output.Add(firstIntervalStart);

                }

                if (firstIntervalEnd > secondIntervalEnd)
                {
                    output.Add(secondIntervalEnd);
                }

                if (firstIntervalEnd < secondIntervalEnd)
                {
                    output.Add(firstIntervalEnd);
                }

                return ReadOnlyList.Of<double>(output);
            }

            output.Add(firstIntervalStart);
            return ReadOnlyList.Of<double>(output);
        }

        output.Add(firstIntervalEnd);
        return ReadOnlyList.Of<double>(output);
    }
}
