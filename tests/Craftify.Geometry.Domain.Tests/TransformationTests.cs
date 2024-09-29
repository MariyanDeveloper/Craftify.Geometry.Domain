using Xunit.Abstractions;

namespace Craftify.Geometry.Domain.Tests;

public class TransformationTests
{
    [Theory]
    [MemberData(nameof(GetAnglesTestData))]
    public void Rotate_ShouldCorrectlyRotateCoordinateSystem(
        double angleInDegrees,
        CoordinateSystem3D expectedCoordinateSystem
    )
    {
        var coordinateSystem = CoordinateSystem
            .Identity()
            .Rotate(rotationAxis: Vector.ZAxis(), angleInDegrees: angleInDegrees);
        coordinateSystem.ShouldBeApproximatelyEquivalentTo(expectedCoordinateSystem);
    }

    [Fact]
    public void Rotate_ShouldCorrectlyApplySequencedRotations()
    {
        var rotationAxis = Vector.ZAxis();
        var coordinateSystem = CoordinateSystem
            .Identity()
            .Rotate(rotationAxis, 10)
            .Rotate(rotationAxis, 20)
            .Rotate(rotationAxis, 12)
            .Rotate(rotationAxis, 3);
        var expectedCoordinateSystem = CoordinateSystem.Identity().Rotate(rotationAxis, 45);
        coordinateSystem.ShouldBeApproximatelyEquivalentTo(expectedCoordinateSystem);
    }

    [Fact]
    public void Translate_ShouldCorrectlyTranslateCoordinateSystem()
    {
        var translationVector = Vector.ByCoordinates(4, 5, 2);

        var coordinateSystem = CoordinateSystem.Identity().Translate(translationVector);

        var expectedCoordinateSystem = CoordinateSystem.ByOrigin(Point.ByCoordinates(4, 5, 2));

        coordinateSystem.ShouldBeApproximatelyEquivalentTo(expectedCoordinateSystem);
    }

    [Fact]
    public void OfPoint_ShouldCorrectlyTransformPoint()
    {
        var point = Point.ByCoordinates(-1, 1, -1);
        var coordinateSystem = CoordinateSystem
            .ByOrigin(Point.ByCoordinates(3, 4, 2))
            .Rotate(rotationAxis: Vector.ZAxis(), angleInDegrees: 30);

        var transformedPoint = point.Transform(coordinateSystem);

        var expectedPoint = Point.ByCoordinates(1.634, 4.366, 1);
        transformedPoint.ShouldBeApproximatelyEquivalentTo(expectedPoint);
    }

    [Fact]
    public void OfVector_ShouldIgnoreTranslation()
    {
        var vector = Vector.XAxis();
        var coordinateSystem = CoordinateSystem.ByOrigin(Point.ByCoordinates(3, 4, 2));

        var transformedPoint = coordinateSystem.OfVector(vector);

        var expectedVector = Vector.XAxis();
        transformedPoint.ShouldBeApproximatelyEquivalentTo(expectedVector);
    }

    [Fact]
    public void OfVector_ShouldOnlyApplyRotationAndIgnoreTranslation()
    {
        var vector = Vector.XAxis();
        var coordinateSystem = CoordinateSystem
            .ByOrigin(Point.ByCoordinates(3, 4, 2))
            .Rotate(rotationAxis: Vector.ZAxis(), angleInDegrees: 30);

        var transformedPoint = vector.Transform(coordinateSystem);

        var expectedVector = Vector.ByCoordinates(0.866, 0.5);
        transformedPoint.ShouldBeApproximatelyEquivalentTo(expectedVector);
    }

    [Fact]
    public void Inverse_WhenNoRotationAndTranslationApplied_ShouldReturnTheSame()
    {
        var coordinateSystem = CoordinateSystem.Identity();

        var inversed = coordinateSystem.Inverse();

        var expectedCoordinateSystem = CoordinateSystem.Identity();
        inversed.ShouldBeApproximatelyEquivalentTo(expectedCoordinateSystem);
    }

    [Fact]
    public void Inverse_WhenRotationAndTranslationApplied_ShouldReturnInversed()
    {
        var coordinateSystem = CoordinateSystem
            .ByOrigin(Point.ByCoordinates(3, 4))
            .Rotate(rotationAxis: Vector.ZAxis(), angleInDegrees: 45);

        var inversedCoordinateSystem = coordinateSystem.Inverse();

        var expectedInversedCoordinateSystem = CoordinateSystem.ByOriginAndVectors(
            origin: Point.ByCoordinates(-4.95, -0.707),
            xAxis: Vector.ByCoordinates(0.707, -0.707),
            yAxis: Vector.ByCoordinates(0.707, 0.707)
        );

        inversedCoordinateSystem.ShouldBeApproximatelyEquivalentTo(
            expectedInversedCoordinateSystem
        );
    }

    [Fact]
    public void TransformingPointWithInverse_ShouldReturnToOriginalPosition()
    {
        var originalPoint = Point.ByCoordinates(1, 2);
        var transformationCoordinateSystem = CoordinateSystem
            .ByOrigin(Point.ByCoordinates(3, 4))
            .Rotate(rotationAxis: Vector.ZAxis(), angleInDegrees: 45);

        var transformedPoint = originalPoint.Transform(transformationCoordinateSystem);

        var inverseCoordinateSystem = transformationCoordinateSystem.Inverse();
        var revertedPoint = transformedPoint.Transform(inverseCoordinateSystem);

        revertedPoint.ShouldBeApproximatelyEquivalentTo(originalPoint);
    }

    [Fact]
    public void TransformLine_ShouldReturnExpectedLine()
    {
        var line = Line.ByStartPointAndEndPoint(
            start: Point.ByCoordinates(2, 0),
            end: Point.ByCoordinates(4, 0)
        );
        var coordinateSystem = CoordinateSystem
            .ByOrigin(Point.ByCoordinates(3, 4))
            .Rotate(rotationAxis: Vector.ZAxis(), angleInDegrees: 45);

        var transformedLine = line.Transform(coordinateSystem);

        var expectedLine = Line.ByStartPointAndEndPoint(
            start: Point.ByCoordinates(4.414, 5.414, 0),
            end: Point.ByCoordinates(5.828, 6.828, 0)
        );

        transformedLine.ShouldBeApproximatelyEquivalentTo(expectedLine);
    }

    public static IEnumerable<object[]> GetAnglesTestData()
    {
        yield return [0, CoordinateSystem.Identity()];
        yield return
        [
            30,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.ByCoordinates(0.866, 0.5),
                yAxis: Vector.ByCoordinates(-0.5, 0.866)
            ),
        ];
        yield return
        [
            45,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.ByCoordinates(0.707, 0.707),
                yAxis: Vector.ByCoordinates(-0.707, 0.707)
            ),
        ];
        yield return
        [
            90,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.YAxis(),
                yAxis: Vector.XAxis().Negate()
            ),
        ];
        yield return
        [
            130,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.ByCoordinates(-0.643, 0.766),
                yAxis: Vector.ByCoordinates(-0.766, -0.643)
            ),
        ];
        yield return
        [
            170,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.ByCoordinates(-0.985, 0.174),
                yAxis: Vector.ByCoordinates(-0.174, -0.985)
            ),
        ];
        yield return
        [
            280,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.ByCoordinates(0.174, -0.985),
                yAxis: Vector.ByCoordinates(0.985, 0.174)
            ),
        ];
        yield return
        [
            340,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.ByCoordinates(0.940, -0.342),
                yAxis: Vector.ByCoordinates(0.342, 0.940)
            ),
        ];
        yield return [360, CoordinateSystem.Identity()];
        yield return
        [
            430,
            CoordinateSystem.ByOriginAndVectors(
                origin: Point.Origin(),
                xAxis: Vector.ByCoordinates(0.342, 0.940),
                yAxis: Vector.ByCoordinates(-0.940, 0.342)
            ),
        ];
    }
}
