namespace Craftify.Geometry.Domain;

public static class CoordinateSystemTransformations
{
    public static CoordinateSystem3D Translate(
        this CoordinateSystem3D coordinateSystem,
        Vector3D translation
    ) => coordinateSystem.AsMatrix().Translate(translation);

    public static CoordinateSystem3D Rotate(
        this CoordinateSystem3D coordinateSystem,
        Vector3D rotationAxis,
        double angleInDegrees
    ) => coordinateSystem.AsMatrix().Rotate(rotationAxis, angleInDegrees);

    public static Point3D OfPoint(this CoordinateSystem3D coordinateSystem, Point3D point) =>
        coordinateSystem.AsMatrix().Multiply(point.AsVector()).AsPoint();

    public static Vector3D OfVector(this CoordinateSystem3D coordinateSystem, Vector3D vector)
    {
        return Vector.ByCoordinates(
            vector.X * coordinateSystem.XAxis.X
            + vector.Y * coordinateSystem.YAxis.X
            + vector.Z * coordinateSystem.ZAxis.X,
            vector.X * coordinateSystem.XAxis.Y
            + vector.Y * coordinateSystem.YAxis.Y
            + vector.Z * coordinateSystem.ZAxis.Y,
            vector.X * coordinateSystem.XAxis.Z
            + vector.Y * coordinateSystem.YAxis.Z
            + vector.Z * coordinateSystem.ZAxis.Z
        );
    }

    public static CoordinateSystem3D Inverse(this CoordinateSystem3D coordinateSystem) =>
        coordinateSystem.AsMatrix().Inverse();
}