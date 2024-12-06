using FluentAssertions;
namespace Direction_Library.Tests;

public class RotationClassConstraintTests {
    [Fact]
    public void RotationCanBeSet() {
        Rotation r = new Rotation();
        r.RotationAngle = 25;
        r.RotationAngle.Should().Be(25);
    }

    [Fact]
    public void RotationCanBeSetInConstructor() {
        Rotation r = new Rotation(25);
        r.RotationAngle.Should().Be(25);
    }

    [Fact]
    public void RotationEnforcesLowerConstraint() {
        Rotation r = new Rotation(-25);
        r.RotationAngle.Should().Be(335);
    }

    [Fact]
    public void RotationEnforcesUpperConstraint() {
        Rotation r = new Rotation(365);
        r.RotationAngle.Should().Be(5);
    }

    [Theory]
    [InlineData(150, 50, 200)]
    [InlineData(150, -50, 100)]
    [InlineData(150, -200, 310)]
    public void RotationCanBeAdjusted(
        int startingDegree, int adjustment, int outDegree) {
        Rotation r = new Rotation(startingDegree);
        r.AdjustBy(adjustment);
        r.RotationAngle.Should().Be(outDegree);
    }
}
public class RotationClassRadianTests {
    // These would have been Theory tests, but it's difficult to deal with Math.PI and Rounding 
    // in that format. 
    [Fact]
    public void RotationReturnsCorrectRadian1() {
        Rotation r = new Rotation(0);
        r.RotationRadian.Should().Be(0);
        r.RotationAngle = 360;
        r.RotationRadian.Should().Be(0);
    }

    [Fact]
    public void RotationReturnsCorrectRadian2() {
        Rotation r = new Rotation(90);
        r.RotationRadian.Should().Be(Math.Round(Math.PI / 2, 3));
    }

    [Fact]
    public void RotationReturnsCorrectRadian3() {
        Rotation r = new Rotation(180);
        r.RotationRadian.Should().Be(Math.Round(Math.PI, 3));
    }

    [Fact]
    public void RotationReturnsCorrectRadian4() {
        Rotation r = new Rotation(270);
        r.RotationRadian.Should().Be(Math.Round(3 * Math.PI / 2, 3));
    }

    [Fact]
    public void RotationReturnsCorrectRadian5() {
        Rotation r = new Rotation(30);
        r.RotationRadian.Should().Be(Math.Round(Math.PI / 6, 3));
    }

    [Fact]
    public void RotationReturnsCorrectRadian6() {
        Rotation r = new Rotation(240);
        r.RotationRadian.Should().Be(Math.Round(4 * Math.PI / 3, 3));
    }

    [Fact]
    public void RotationSetsCorrectAngleGivenRadian1() {
        Rotation r = new Rotation();
        r.RotationRadian = Math.PI / 2;
        r.RotationAngle.Should().Be(90);
    }

    [Fact]
    public void RotationSetsCorrectAngleGivenRadian2() {
        Rotation r = new Rotation();
        r.RotationRadian = Math.PI / 6;
        r.RotationAngle.Should().Be(30);
    }

    [Fact]
    public void RotationSetsCorrectAngleGivenRadian3() {
        Rotation r = new Rotation();
        r.RotationRadian = 5 * Math.PI / 3;
        r.RotationAngle.Should().Be(300);
    }
}
public class RotationClassCoordinateTests {
    private double d = Math.Round(Math.Sqrt(2) / 2, 2);

    [Fact]
    public void RotationCanGetCoords1() {
        Rotation r = new Rotation(0);
        r.X_Coord.Should().Be(1);
        r.Y_Coord.Should().Be(0);
    }

    [Fact]
    public void RotationCanGetCoords2() {
        Rotation r = new Rotation(45);
        r.X_Coord.Should().Be(d);
        r.Y_Coord.Should().Be(d);
    }

    [Fact]
    public void RotationCanGetCoords3() {
        Rotation r = new Rotation(90);
        r.X_Coord.Should().Be(0);
        r.Y_Coord.Should().Be(1);
    }

    [Fact]
    public void RotationCanGetCoords4() {
        Rotation r = new Rotation(135);
        r.X_Coord.Should().Be(-d);
        r.Y_Coord.Should().Be(d);
    }

    [Fact]
    public void RotationCanGetCoords5() {
        Rotation r = new Rotation(180);
        r.X_Coord.Should().Be(-1);
        r.Y_Coord.Should().Be(0);
    }

    [Fact]
    public void RotationCanGetCoords6() {
        Rotation r = new Rotation(225);
        r.X_Coord.Should().Be(-d);
        r.Y_Coord.Should().Be(-d);
    }

    [Fact]
    public void RotationCanGetCoords7() {
        Rotation r = new Rotation(270);
        r.X_Coord.Should().Be(0);
        r.Y_Coord.Should().Be(-1);
    }

    [Fact]
    public void RotationCanGetCoords8() {
        Rotation r = new Rotation(315);
        r.X_Coord.Should().Be(d);
        r.Y_Coord.Should().Be(-d);
    }
}
public class RotationClassXFlipTests {
    [Theory]
    [InlineData(0, 0)]
    [InlineData(180, 180)]
    [InlineData(30, 330)]
    [InlineData(75, 285)]
    [InlineData(110, 250)]
    [InlineData(170, 190)]
    [InlineData(210, 150)]
    [InlineData(235, 125)]
    [InlineData(271, 89)]
    [InlineData(325, 35)]
    public void RotationCanBeXFlipped(
        int inDegree, int outDegree) {
        Rotation r = new Rotation(inDegree);
        r.FlipX();
        r.RotationAngle.Should().Be(outDegree);
    }
}
public class RotationClassYFlipTests {
    [Theory]
    [InlineData(90, 90)]
    [InlineData(270, 270)]
    [InlineData(30, 150)]
    [InlineData(75, 105)]
    [InlineData(110, 70)]
    [InlineData(170, 10)]
    [InlineData(210, 330)]
    [InlineData(235, 305)]
    [InlineData(271, 269)]
    [InlineData(325, 215)]
    public void RotationCanBeYFlipped(
        int inDegree, int outDegree) {
        Rotation r = new Rotation(inDegree);
        r.FlipY();
        r.RotationAngle.Should().Be(outDegree);
    }
}
public class RotationClassDoubleFlipTests {
    [Theory]
    [InlineData(45, 225)]
    [InlineData(359, 179)]
    [InlineData(0, 180)]
    [InlineData(90, 270)]
    public void RotationCanBeDoubleFlipped(
        int inDegree, int outDegree) {
        Rotation r = new Rotation(inDegree);
        r.FlipY();
        r.FlipX();
        r.RotationAngle.Should().Be(outDegree);
    }
}
public class RotationClassAssignsToCoordinatesTests {
    [Theory]
    [InlineData(10, 0, 0)]
    [InlineData(10, 10, 45)]
    [InlineData(0, 10, 90)]
    [InlineData(-10, 10, 135)]
    [InlineData(-10, 0, 180)]
    [InlineData(-10, -10, 225)]
    [InlineData(0, -10, 270)]
    [InlineData(10, -10, 315)]
    public void RotationAngleFromCoordinatesWorks(
        double coordX, double coordY, int outAngle) {

    }
}