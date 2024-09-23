namespace Craftify.Geometry.Domain;

public static class Plane
{
    public static Plane3D ByNormalAndOrigin(Vector3D normal, Point3D origin) => 
        new(normal, origin);
}
