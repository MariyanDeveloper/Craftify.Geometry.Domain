using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class Plane3DTests
{
    [Theory]
    [MemberData(nameof(ProjectPointTestData))]
    public void ProjectPoint_ShouldReturnExpectedProjectedPoint(
        Plane3D plane, Point3D input, Point3D expected)
    {
        plane.ProjectPoint(input).AlmostEqualTo(expected).Should().BeTrue();
    }

    public static IEnumerable<object[]> ProjectPointTestData()
    {
        yield return
            [
                Plane.ByNormalAndOrigin(Vector.ByCoordinates(0, 0, 1), Point.Origin()),
                Point.ByCoordinates(1, 2, 0),
                Point.ByCoordinates(1, 2, 0)
            ];

        yield return
            [
                Plane.ByNormalAndOrigin(Vector.ByCoordinates(0, 0, 1), Point.Origin()),
                Point.ByCoordinates(1, 2, 3),
                Point.ByCoordinates(1, 2, 0)
            ];

        yield return
            [
                Plane.ByNormalAndOrigin(
                    Vector.ByCoordinates(10, 10, 10),
                    Point.ByCoordinates(1, 1, 1)),
                Point.ByCoordinates(3, 3, 3),
                Point.ByCoordinates(1, 1, 1)
            ];

        yield return
            [
                Plane.ByNormalAndOrigin(
                    Vector.ByCoordinates(0, 1, 0), Point.Origin()),
                Point.ByCoordinates(0, 3, 0),
                Point.Origin()
            ];
    }
}
