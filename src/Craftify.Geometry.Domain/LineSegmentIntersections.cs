namespace Craftify.Geometry.Domain;

public static class LineSegmentIntersections
{
    public static bool Intersects(this LineSegment main, LineSegment other, out Point3D? intersectionPoint)
    {
        intersectionPoint = null;

        Vector3D mainDirectionVector = main.GetDirection();
        Vector3D otherDirectionVector = other.GetDirection();

        Vector3D directionCrossProduct = mainDirectionVector.CrossProduct(otherDirectionVector);

        bool areDirectionCrossProductValuesCloseToZero = directionCrossProduct.X.IsCloseToZero()
            && directionCrossProduct.Y.IsCloseToZero()
            && directionCrossProduct.Z.IsCloseToZero();

        if (areDirectionCrossProductValuesCloseToZero)
        {
            var vectorBetween = new Vector3D(
                other.Start.X - main.Start.X,
                other.Start.Y - main.Start.Y,
                other.Start.Z - main.Start.Z
            );

            Vector3D crossProductBetween = mainDirectionVector.CrossProduct(vectorBetween);

            bool areCrossProductBetweenCloseToZero = crossProductBetween.X.IsCloseToZero() && crossProductBetween.Y.IsCloseToZero() && crossProductBetween.Z.IsCloseToZero();

            if (areCrossProductBetweenCloseToZero)
            {
                bool areSegmentsOverlapping = AreSegmentsOverlapping(main, other);
                if (areSegmentsOverlapping)
                {
                    intersectionPoint = main.Start;
                    return true;
                }
            }
            return false;
        }

        Vector3D segmentsStartSubtraction = other.Start.SubtractPoint(main.Start);

        double denominator = mainDirectionVector
            .CrossProduct(otherDirectionVector)
            .DotProduct(mainDirectionVector
                .CrossProduct(otherDirectionVector));

        if (denominator.IsCloseToZero())
        {
            return false;
        }

        double t = segmentsStartSubtraction
            .CrossProduct(otherDirectionVector)
            .DotProduct(mainDirectionVector
                .CrossProduct(otherDirectionVector))
            / denominator;

        double u = segmentsStartSubtraction
            .CrossProduct(mainDirectionVector)
            .DotProduct(mainDirectionVector
                .CrossProduct(otherDirectionVector))
            / denominator;

        if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
        {
            intersectionPoint = new Point3D(
                main.Start.X + t * mainDirectionVector.X,
                main.Start.Y + t * mainDirectionVector.Y,
                main.Start.Z + t * mainDirectionVector.Z
            );
            return true;
        }

        return false;
    }

    private static bool AreSegmentsOverlapping(LineSegment main, LineSegment other)
    {
        return (Math.Max(main.Start.X, main.End.X) >= Math.Min(other.Start.X, other.End.X)) &&
               (Math.Min(main.Start.X, main.End.X) <= Math.Max(other.Start.X, other.End.X)) &&
               (Math.Max(main.Start.Y, main.End.Y) >= Math.Min(other.Start.Y, other.End.Y)) &&
               (Math.Min(main.Start.Y, main.End.Y) <= Math.Max(other.Start.Y, other.End.Y)) &&
               (Math.Max(main.Start.Z, main.End.Z) >= Math.Min(other.Start.Z, other.End.Z)) &&
               (Math.Min(main.Start.Z, main.End.Z) <= Math.Max(other.Start.Z, other.End.Z));
    }
}
