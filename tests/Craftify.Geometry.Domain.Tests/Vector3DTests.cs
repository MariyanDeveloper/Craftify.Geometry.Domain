namespace Craftify.Geometry.Domain.Tests;

public class Vector3DTests
{
    [Fact]
    public void A()
    {
        //Arrange
        var vector = new Vector3D(
            -5, -3, 0
        );
        //Act
        var magnitude = vector.Magnitude();
        //Assert
    }

    [Fact]
    public void C()
    {

        var v1 = Vector.ByCoordinates(
            1, 0, 0);
        var v2 = Vector.ByCoordinates(0, -1, 0);
        var r1 = v1.AngleTo(v2);
        var r2 = v2.AngleTo(v1);
        var r3 = v1.AngleAboutAxis(v2, Vector.ZAxis());
        var r4 = v2.AngleAboutAxis(v1, Vector.ZAxis());
        var a = 5;
        // var v1 = new Vector3D(
        //     -1, -1, 0
        // );
        // var v2 = new Vector3D(
        //     2, 0, 0
        // );
        // var angle1 = v1.AngleTo(v2);
        // var a2 = v2.AngleTo(v1);
        // var a3 = v2.AngleAboutAxis(v1, Vector.ZAxis());

    }
}