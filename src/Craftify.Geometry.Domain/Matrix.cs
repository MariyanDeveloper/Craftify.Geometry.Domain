namespace Craftify.Geometry.Domain;

public static class Matrix
{
    public static Matrix3x4 Identity()
    {
        var components = ReadOnlyList.OfSequence(
            1.0,
            0.0,
            0.0,
            0.0,
            0.0,
            1.0,
            0.0,
            0.0,
            0.0,
            0.0,
            1.0,
            0.0
        );

        return new Matrix3x4(
            XAxis: Vector.XAxis(),
            YAxis: Vector.YAxis(),
            ZAxis: Vector.ZAxis(),
            Translation: Vector.Zero(),
            Components: components,
            M11: components[0],
            M12: components[1],
            M13: components[2],
            Tx: components[3],
            M21: components[4],
            M22: components[5],
            M23: components[6],
            Ty: components[7],
            M31: components[8],
            M32: components[9],
            M33: components[10],
            Tz: components[11]
        );
    }

    public static Matrix3x4 ByAxes(
        Vector3D xAxis,
        Vector3D yAxis,
        Vector3D zAxis,
        Vector3D translation
    )
    {
        var components = ReadOnlyList.OfSequence(
            xAxis.X,
            xAxis.Y,
            xAxis.Z,
            translation.X,
            yAxis.X,
            yAxis.Y,
            yAxis.Z,
            translation.Y,
            zAxis.X,
            zAxis.Y,
            zAxis.Z,
            translation.Z
        );
        return new Matrix3x4(
            XAxis: xAxis,
            YAxis: yAxis,
            ZAxis: zAxis,
            Translation: translation,
            Components: components,
            M11: components[0],
            M12: components[1],
            M13: components[2],
            M21: components[4],
            M22: components[5],
            M23: components[6],
            M31: components[8],
            M32: components[9],
            M33: components[10],
            Tx: components[3],
            Ty: components[7],
            Tz: components[11]
        );
    }

    public static Matrix3x4 ByCoordinateSystem(CoordinateSystem3D coordinateSystem) =>
        ByAxes(
            xAxis: coordinateSystem.XAxis,
            yAxis: coordinateSystem.YAxis,
            zAxis: coordinateSystem.ZAxis,
            translation: coordinateSystem.Origin.AsVector()
        );

    public static Matrix3x4 AsMatrix(this CoordinateSystem3D coordinateSystem) =>
        ByCoordinateSystem(coordinateSystem);
}
