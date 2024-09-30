namespace Craftify.Geometry.Domain;

public static class CoordinateSystem
{
    public static CoordinateSystem3D ByMatrix(Matrix3x4 matrix) =>
        new(
            XAxis: matrix.XAxis,
            YAxis: matrix.YAxis,
            ZAxis: matrix.ZAxis,
            Origin: matrix.Translation.AsPoint()
        );

    public static CoordinateSystem3D Identity() => Matrix.Identity();

    public static CoordinateSystem3D ByOriginAndVectors(
        Point3D origin,
        Vector3D xAxis,
        Vector3D yAxis,
        Vector3D zAxis
    )
    {
        return Matrix.ByAxes(
            translation: origin.AsVector(),
            xAxis: xAxis,
            yAxis: yAxis,
            zAxis: zAxis
        );
    }

    public static CoordinateSystem3D ByOriginAndVectors(
        Point3D origin,
        Vector3D xAxis,
        Vector3D yAxis
    )
    {
        return Matrix.ByAxes(
            translation: origin.AsVector(),
            xAxis: xAxis,
            yAxis: yAxis,
            zAxis: Vector.ZAxis()
        );
    }

    public static CoordinateSystem3D ByOrigin(Point3D origin)
    {
        return ByOriginAndVectors(
            origin: origin,
            xAxis: Vector.XAxis(),
            yAxis: Vector.YAxis(),
            zAxis: Vector.ZAxis()
        );
    }
}