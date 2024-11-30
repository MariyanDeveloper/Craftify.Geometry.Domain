namespace Craftify.Geometry.Domain;

public static class MatrixExtensions
{
    public static Matrix3x4 Translate(this Matrix3x4 matrix, Vector3D translation)
    {
        var components = matrix.Components.ToArray();
        components[3] = translation.X;
        components[7] = translation.Y;
        components[11] = translation.Z;

        return matrix with
        {
            Translation = translation,
            Components = components,
            Tx = translation.X,
            Ty = translation.Y,
            Tz = translation.Z,
        };
    }

    //TODO: look into case like matrix.Rotate(xAxis, angle).Rotate(yAxis, angle)
    public static Matrix3x4 Rotate(
        this Matrix3x4 matrix,
        Vector3D rotationAxis,
        double angleInDegrees
    )
    {
        if (!rotationAxis.IsUnit())
        {
            throw new ArgumentException("The provided axis is not of unit length.");
        }
        var angleInRadians = -angleInDegrees.ToRadians();
        var s = Math.Sin(angleInRadians);
        var c = Math.Cos(angleInRadians);

        var a = 1.0 - c;
        var x = rotationAxis.X;
        var y = rotationAxis.Y;
        var z = rotationAxis.Z;

        var r11 = a * x * x + c;
        var r12 = a * x * y - z * s;
        var r13 = a * x * z + y * s;

        var r21 = a * y * x + z * s;
        var r22 = a * y * y + c;
        var r23 = a * y * z - x * s;

        var r31 = a * z * x - y * s;
        var r32 = a * z * y + x * s;
        var r33 = a * z * z + c;

        var newXAxis = Vector.ByCoordinates(
            r11 * matrix.XAxis.X + r12 * matrix.YAxis.X + r13 * matrix.ZAxis.X,
            r11 * matrix.XAxis.Y + r12 * matrix.YAxis.Y + r13 * matrix.ZAxis.Y,
            r11 * matrix.XAxis.Z + r12 * matrix.YAxis.Z + r13 * matrix.ZAxis.Z
        );

        var newYAxis = Vector.ByCoordinates(
            r21 * matrix.XAxis.X + r22 * matrix.YAxis.X + r23 * matrix.ZAxis.X,
            r21 * matrix.XAxis.Y + r22 * matrix.YAxis.Y + r23 * matrix.ZAxis.Y,
            r21 * matrix.XAxis.Z + r22 * matrix.YAxis.Z + r23 * matrix.ZAxis.Z
        );

        var newZAxis = Vector.ByCoordinates(
            r31 * matrix.XAxis.X + r32 * matrix.YAxis.X + r33 * matrix.ZAxis.X,
            r31 * matrix.XAxis.Y + r32 * matrix.YAxis.Y + r33 * matrix.ZAxis.Y,
            r31 * matrix.XAxis.Z + r32 * matrix.YAxis.Z + r33 * matrix.ZAxis.Z
        );

        return Matrix.ByAxes(
            xAxis: newXAxis,
            yAxis: newYAxis,
            zAxis: newZAxis,
            translation: matrix.Translation
        );
    }

    public static Vector3D Multiply(this Matrix3x4 matrix, Vector3D vector)
    {
        return Vector.ByCoordinates(
            vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.Tx,
            vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.Ty,
            vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.Tz
        );
    }

    public static double Determinant(this Matrix3x4 matrix)
    {
        return matrix.M11 * (matrix.M22 * matrix.M33 - matrix.M23 * matrix.M32)
               + matrix.M12 * (matrix.M23 * matrix.M31 - matrix.M21 * matrix.M33)
               + matrix.M13 * (matrix.M21 * matrix.M32 - matrix.M22 * matrix.M31);
    }

    public static Matrix3x4 Inverse(this Matrix3x4 matrix)
    {
        var determinant = matrix.Determinant();
        if (determinant.AlmostEqualTo(0))
        {
            throw new InvalidOperationException(
                "The determinant of the matrix must be greater than 0."
            );
        }
        var inverseDeterminant = 1.0 / determinant;
        var m11 = (matrix.M22 * matrix.M33 - matrix.M23 * matrix.M32) * inverseDeterminant;
        var m12 = (matrix.M13 * matrix.M32 - matrix.M12 * matrix.M33) * inverseDeterminant;
        var m13 = (matrix.M12 * matrix.M23 - matrix.M13 * matrix.M22) * inverseDeterminant;

        var m21 = (matrix.M23 * matrix.M31 - matrix.M21 * matrix.M33) * inverseDeterminant;
        var m22 = (matrix.M11 * matrix.M33 - matrix.M13 * matrix.M31) * inverseDeterminant;
        var m23 = (matrix.M13 * matrix.M21 - matrix.M11 * matrix.M23) * inverseDeterminant;

        var m31 = (matrix.M21 * matrix.M32 - matrix.M22 * matrix.M31) * inverseDeterminant;
        var m32 = (matrix.M12 * matrix.M31 - matrix.M11 * matrix.M32) * inverseDeterminant;
        var m33 = (matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21) * inverseDeterminant;

        var tx = -(matrix.Tx * m11 + matrix.Ty * m21 + matrix.Tz * m31);
        var ty = -(matrix.Tx * m12 + matrix.Ty * m22 + matrix.Tz * m32);
        var tz = -(matrix.Tx * m13 + matrix.Ty * m23 + matrix.Tz * m33);

        var invertedXAxis = new Vector3D(m11, m12, m13);
        var invertedYAxis = new Vector3D(m21, m22, m23);
        var invertedZAxis = new Vector3D(m31, m32, m33);
        var invertedTranslation = new Vector3D(tx, ty, tz);
        return Matrix.ByAxes(
            xAxis: invertedXAxis,
            yAxis: invertedYAxis,
            zAxis: invertedZAxis,
            translation: invertedTranslation
        );
    }
}