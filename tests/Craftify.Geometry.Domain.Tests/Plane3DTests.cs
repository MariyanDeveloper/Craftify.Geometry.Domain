using FluentAssertions;

namespace Craftify.Geometry.Domain.Tests;

public class Plane3DTests
{
    [Theory]
    [MemberData(nameof(ProjectPointTestData))]
    public void ProjectPoint_ShouldReturnExpectedProjectedPoint(
        Plane3D plane, Point3D input, Point3D expected)
    {
        Point3D projectedPoint = plane.ProjectPoint(input);
        projectedPoint.Should().Be(expected);
    }

    public static IEnumerable<object[]> ProjectPointTestData()
    {
        yield return
            [
                Plane.ByNormalAndOrigin(
                    new Vector3D(0, 0, 1), new Point3D(0, 0, 0)),
                new Point3D(1, 2, 0),
                new Point3D(1, 2, 0)
            ];

        yield return
            [
                Plane.ByNormalAndOrigin(
                    new Vector3D(0, 0, 1), new Point3D(0, 0, 0)),
                new Point3D(1, 2, 3),
                new Point3D(1, 2, 0)
            ];

        yield return
            [
                Plane.ByNormalAndOrigin(
                    new Vector3D(1, 1, 1), new Point3D(1, 1, 1)),
                new Point3D(3, 3, 3),
                new Point3D(1, 1, 1)
            ];

        yield return
            [
                Plane.ByNormalAndOrigin(
                    new Vector3D(0, 1, 0), new Point3D(0, 0, 0)),
                new Point3D(0, 0, 0),
                new Point3D(0, 0, 0)
            ];
    }
}
