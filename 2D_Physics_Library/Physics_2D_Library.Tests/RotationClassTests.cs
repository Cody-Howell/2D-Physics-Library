using FluentAssertions;
using Physics2DLibrary;
namespace Physics_2D_Library.Tests.RotationClass;

public class RotationClassConstraintTests {
    [Fact]
    public void RotationCanBeSet() {
        Rotation r = new Rotation();
        r.RotationAngle = 25;
        r.RotationAngle.Should().Be(25);
    }

    [Fact]
    public void RotationCanBeSetInConstructor1() {
        Rotation r = new Rotation(25);
        r.RotationAngle.Should().Be(25);
    }

    [Fact]
    public void RotationCanBeSetInConstructor2() {
        Rotation r = new Rotation(Math.PI);
        r.RotationAngle.Should().Be(180);
    }

    [Fact]
    public void RotationCanBeSetInConstructor3() {
        Rotation r1 = new Rotation(90);
        Rotation r2 = new Rotation(r1);
        r2.RotationAngle.Should().Be(90);
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

    [Theory]
    [InlineData(45, 225)]
    [InlineData(359, 179)]
    [InlineData(0, 180)]
    [InlineData(90, 270)]
    public void RotationCanBeDoubleFlippedPart2(
    int inDegree, int outDegree) {
        Rotation r = new Rotation(inDegree);
        r.DoubleFlip();
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
    [InlineData(77.74, 98.37, 52)]
    [InlineData(-28.09, -5.60, 191)]
    [InlineData(-46.99, -54.87, 229)]
    public void RotationAngleFromCoordinatesWorks(
        double coordX, double coordY, int outAngle) {
        Rotation r = new Rotation();
        r.AssignToCoordinates(coordX, coordY);
        r.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassAssignsto2PointCoordinateTests {
    [Theory]
    [InlineData(0, 0, 10, 10, 45)]
    [InlineData(0, 0, -10, 10, 135)]
    [InlineData(0, 0, -10, -10, 225)]
    [InlineData(0, 0, 10, -10, 315)]
    public void TwoPointSimpleCoordTests(
        double x1, double y1, double x2, double y2, int outAngle) {
        Rotation r = new Rotation();
        r.AssignToCoordinates(x1, y1, x2, y2);
        r.RotationAngle.Should().Be(outAngle);
    }

    [Theory]
    [InlineData(5, 5, 0, 0, 225)]
    [InlineData(25, 0, -30, 0, 180)]
    [InlineData(-30, 0, 45, -22, 344)]
    [InlineData(83, -24, 0, 0, 164)]
    public void TwoPointComplexCoordTests(
    double x1, double y1, double x2, double y2, int outAngle) {
        Rotation r = new Rotation();
        r.AssignToCoordinates(x1, y1, x2, y2);
        r.RotationAngle.Should().Be(outAngle);
    }

    [Theory]
    [InlineData(5, 5, 0, 0, 225)]
    [InlineData(25, 0, -30, 0, 180)]
    [InlineData(-30, 0, 45, -22, 344)]
    [InlineData(83, -24, 0, 0, 164)]
    public void TuplesCoordTest(
    double x1, double y1, double x2, double y2, int outAngle) {
        Rotation r = new Rotation();
        r.AssignToCoordinates((x1, y1), (x2, y2));
        r.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassDistanceTests {
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 180, 180)]
    [InlineData(0, 90, 90)]
    [InlineData(0, 270, 90)]
    [InlineData(30, 330, 60)]
    [InlineData(330, 30, 60)]
    [InlineData(270, 0, 90)]
    public void DistanceIntTests(
        int rot1, int rot2, int expDistance) {
        Rotation r1 = new Rotation(rot1);

        r1.DistanceTo(rot2).Should().Be(expDistance);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 180, 180)]
    [InlineData(0, 90, 90)]
    [InlineData(0, 270, 90)]
    [InlineData(30, 330, 60)]
    [InlineData(330, 30, 60)]
    [InlineData(270, 0, 90)]
    public void DistanceClassTests(
    int rot1, int rot2, int expDistance) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);

        r1.DistanceTo(r2).Should().Be(expDistance);
    }
}
public class RotationClassAverageToTests {
    [Theory]
    [InlineData(0, 90, 0, 0)]
    [InlineData(0, 90, 1, 90)]
    [InlineData(0, 270, 0, 0)]
    [InlineData(0, 270, 1, 270)]
    public void SimpleAverageToClassTests(
        int rot1, int rot2, double percent, int outAngle) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);

        r1.AverageTo(r2, percent);
        r1.RotationAngle.Should().Be(outAngle);
        r2.RotationAngle.Should().Be(rot2);
    }

    [Theory]
    [InlineData(0, 90, 0, 0)]
    [InlineData(0, 90, 1, 90)]
    [InlineData(0, 270, 0, 0)]
    [InlineData(0, 270, 1, 270)]
    public void SimpleAverageToIntTests(
    int rot1, int rot2, double percent, int outAngle) {
        Rotation r1 = new Rotation(rot1);

        r1.AverageTo(rot2, percent);
        r1.RotationAngle.Should().Be(outAngle);
    }

    [Theory]
    [InlineData(0, 90, 0.5, 45)]
    [InlineData(0, 90, 0.2, 18)]
    [InlineData(0, 270, 0.5, 315)]
    [InlineData(0, 270, 0.2, 342)]
    [InlineData(10, 214, 0.15, 347)]
    [InlineData(14, 256, 0.7, 291)]
    [InlineData(196, 218, 0.28, 202)]
    [InlineData(207, 321, 0.83, 302)]
    [InlineData(218, 180, 0.59, 196)]
    [InlineData(232, 97, 0.42, 175)]
    [InlineData(242, 231, 0.24, 239)]
    [InlineData(26, 101, 0.08, 32)]
    [InlineData(28, 168, 0.17, 52)]
    [InlineData(45, 168, 0.24, 75)]
    public void ComplexAverageToTests(
        int rot1, int rot2, double percent, int outAngle) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);

        r1.AverageTo(r2, percent);
        r1.RotationAngle.Should().Be(outAngle);
        r2.RotationAngle.Should().Be(rot2);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1.001)]
    [InlineData(230)]
    [InlineData(-928735)]
    public void AverageToThrowsErrorsTests(
        double average) {
        Rotation r1 = new Rotation();
        Rotation r2 = new Rotation();
        r1.Invoking(x => x.AverageTo(r2, average)).Should().Throw<ArgumentOutOfRangeException>();
    }
}
public class RotationClassAddOperatorTests {
    [Theory]
    [InlineData(90, 0, 90)]
    [InlineData(260, 90, 350)]
    [InlineData(90, 45, 135)]
    [InlineData(360, 90, 90)]
    [InlineData(0, 90, 90)]
    public void AdditionTests(
        int rot1, int rot2, int outAngle) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);
        Rotation answer = r1 + r2;
        answer.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassSubtractOperatorTests {
    [Theory]
    [InlineData(90, 0, 90)]
    [InlineData(260, 90, 170)]
    [InlineData(90, 45, 45)]
    [InlineData(360, 90, 270)]
    [InlineData(0, 90, 270)]
    public void SubtractionTests(
        int rot1, int rot2, int outAngle) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);
        Rotation answer = r1 - r2;
        answer.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassAverageOperatorTests {
    [Theory]
    [InlineData(0, 180, 90)]
    [InlineData(0, 90, 45)]
    [InlineData(0, 45, 22)]
    [InlineData(350, 10, 0)]
    [InlineData(330, 90, 30)]
    [InlineData(45, 180, 112)]
    public void AverageOperatorTests(
        int rot1, int rot2, int outAngle) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);
        Rotation answer = r1 ^ r2;
        answer.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassModuloOperatorTests {
    [Theory]
    [InlineData(360, 60, 0)]
    [InlineData(90, 35, 20)]
    [InlineData(180, 60, 0)]
    [InlineData(180, 50, 30)]
    public void ModuloTests(
        int rot1, int rot2, int outAngle) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);
        Rotation answer = r1 % r2;
        answer.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassEqualityInterfaceTests {
    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(0, 360, true)]
    [InlineData(245, 245, true)]
    [InlineData(0, 90, false)]
    [InlineData(18, 278, false)]
    [InlineData(245, 94, false)]
    public void EqualityInterfaceTests(
        int rot1, int rot2, bool equality) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);
        r1.Equals(r2).Should().Be(equality);
    }
}
public class RotationClassComparableInterfaceTests {
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(90, 90, 0)]
    [InlineData(90, 45, 1)]
    [InlineData(360, 180, -1)] // tricky!
    [InlineData(34, 247, -1)]
    public void CompareToTests(
        int rot1, int rot2, int comparison) {
        Rotation r1 = new Rotation(rot1);
        Rotation r2 = new Rotation(rot2);
        r1.CompareTo(r2).Should().Be(comparison);
    }
}



