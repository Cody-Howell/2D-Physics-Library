using FluentAssertions;
using Physics2DLibrary;
namespace Physics_2D_Library.Tests.RotationClass;

// Ints in InlineData are a legacy of the way they used to be handled. It's more tests to check at least. 
public class RotationClassConstraintTests {
    [Fact]
    public void RotationCanBeSet1() {
        Rotation2D r = new Rotation2D();
        r.RotationAngle = 25;
        r.RotationAngle.Should().Be(25);
    }

    [Fact]
    public void RotationCanBeSet2() {
        Rotation2D r = new Rotation2D();
        r.RotationAngle = 25.352;
        r.RotationAngle.Should().Be(25.35);
    }

    [Fact]
    public void RotationCanBeSetInConstructor1() {
        Rotation2D r = new Rotation2D(25);
        r.RotationAngle.Should().Be(25);
    }

    [Fact]
    public void RotationCanBeSetInConstructor2() {
        Rotation2D r1 = new Rotation2D(90.4);
        Rotation2D r2 = new Rotation2D(r1);
        r2.RotationAngle.Should().Be(90.4);
    }

    [Fact]
    public void RotationEnforcesLowerConstraint() {
        Rotation2D r = new Rotation2D(-25.4);
        r.RotationAngle.Should().Be(334.6);
    }

    [Fact]
    public void RotationEnforcesUpperConstraint() {
        Rotation2D r = new Rotation2D(365.23);
        r.RotationAngle.Should().Be(5.23);
    }

    [Theory]
    [InlineData(150, 50, 200)]
    [InlineData(150, -50.5, 99.5)]
    [InlineData(150, -200, 310)]
    public void RotationCanBeAdjusted(
        double startingDegree, double adjustment, double outDegree) {
        Rotation2D r = new Rotation2D(startingDegree);
        r.AdjustBy(adjustment);
        r.RotationAngle.Should().Be(outDegree);
    }

    [Fact]
    public void RotationCanBeCopied() {
        Rotation2D r1 = new Rotation2D(15);
        Rotation2D r2 = new Rotation2D(r1);

        r2.RotationAngle = 25;
        r1.RotationAngle.Should().Be(15);
        r2.RotationAngle.Should().Be(25);
    }
}
public class RotationClassCoordinateTests {
    private double d = Math.Round(Math.Sqrt(2) / 2, 2);

    [Fact]
    public void RotationCanGetCoords1() {
        Rotation2D r = new Rotation2D(0);
        r.X_Coord.Should().Be(1);
        r.Y_Coord.Should().Be(0);
    }

    [Fact]
    public void RotationCanGetCoords2() {
        Rotation2D r = new Rotation2D(45);
        r.X_Coord.Should().Be(d);
        r.Y_Coord.Should().Be(d);
    }

    [Fact]
    public void RotationCanGetCoords3() {
        Rotation2D r = new Rotation2D(90);
        r.X_Coord.Should().Be(0);
        r.Y_Coord.Should().Be(1);
    }

    [Fact]
    public void RotationCanGetCoords4() {
        Rotation2D r = new Rotation2D(135);
        r.X_Coord.Should().Be(-d);
        r.Y_Coord.Should().Be(d);
    }

    [Fact]
    public void RotationCanGetCoords5() {
        Rotation2D r = new Rotation2D(180);
        r.X_Coord.Should().Be(-1);
        r.Y_Coord.Should().Be(0);
    }

    [Fact]
    public void RotationCanGetCoords6() {
        Rotation2D r = new Rotation2D(225);
        r.X_Coord.Should().Be(-d);
        r.Y_Coord.Should().Be(-d);
    }

    [Fact]
    public void RotationCanGetCoords7() {
        Rotation2D r = new Rotation2D(270);
        r.X_Coord.Should().Be(0);
        r.Y_Coord.Should().Be(-1);
    }

    [Fact]
    public void RotationCanGetCoords8() {
        Rotation2D r = new Rotation2D(315);
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
    [InlineData(74.72, 285.28)]
    [InlineData(194.6, 165.4)]
    [InlineData(209.26, 150.74)]
    [InlineData(237.35, 122.65)]
    [InlineData(325.05, 34.95)]
    public void RotationCanBeXFlipped(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
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
    [InlineData(40.89, 139.11)]
    [InlineData(92.87, 87.13)]
    [InlineData(190.89, 349.11)]
    [InlineData(226.24, 313.76)]
    [InlineData(285.47, 254.53)]
    public void RotationCanBeYFlipped(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
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
    [InlineData(99.33, 279.33)]
    [InlineData(195.29, 15.29)]
    [InlineData(216.27, 36.27)]
    [InlineData(314.61, 134.61)]
    public void RotationCanBeDoubleFlipped(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
        r.FlipY();
        r.FlipX();
        r.RotationAngle.Should().Be(outDegree);
    }

    [Theory]
    [InlineData(45, 225)]
    [InlineData(359, 179)]
    [InlineData(0, 180)]
    [InlineData(90, 270)]
    [InlineData(99.33, 279.33)]
    [InlineData(195.29, 15.29)]
    [InlineData(216.27, 36.27)]
    [InlineData(314.61, 134.61)]
    public void RotationCanBeDoubleFlippedPart2(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
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
    [InlineData(77.74, 98.37, 51.68)]
    [InlineData(-28.09, -5.60, 191.27)]
    [InlineData(-46.99, -54.87, 229.42)]
    public void RotationAngleFromCoordinatesWorks(
        double coordX, double coordY, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates(coordX, coordY);
        r.RotationAngle.Should().Be(outAngle);

        r.AssignToCoordinates(new Point2D(coordX, coordY));
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
        double x1, double y1, double x2, double y2, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates(x1, y1, x2, y2);
        r.RotationAngle.Should().Be(outAngle);
    }

    [Theory]
    [InlineData(5, 5, 0, 0, 225)]
    [InlineData(25, 0, -30, 0, 180)]
    [InlineData(-30, 0, 45, -22, 343.65)]
    [InlineData(83, -24, 0, 0, 163.87)]
    public void TwoPointComplexCoordTests(
    double x1, double y1, double x2, double y2, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates(x1, y1, x2, y2);
        r.RotationAngle.Should().Be(outAngle);

        r.AssignToCoordinates(new Point2D(x1, y1), new Point2D(x2, y2));
        r.RotationAngle.Should().Be(outAngle);

        r.AssignToCoordinates(new Line2D(x1, y1, x2, y2));
        r.RotationAngle.Should().Be(outAngle);
    }

    [Theory]
    [InlineData(5, 5, 0, 0, 225)]
    [InlineData(25, 0, -30, 0, 180)]
    [InlineData(-30, 0, 45, -22, 343.65)]
    [InlineData(83, -24, 0, 0, 163.87)]
    public void TuplesCoordTest(
    double x1, double y1, double x2, double y2, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates((x1, y1), (x2, y2));
        r.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassDistanceTests {
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 180, 180)]
    [InlineData(0, 179, 179)]
    [InlineData(0, 181, -179)]
    [InlineData(90, 270, 180)]
    [InlineData(0, 90, 90)]
    [InlineData(0, 270, -90)]
    [InlineData(30, 330, -60)]
    [InlineData(330, 30, 60)]
    [InlineData(270, 0, 90)]
    public void DistanceIntTests(
        double rot1, double rot2, double expDistance) {
        Rotation2D r1 = new Rotation2D(rot1);

        r1.DistanceTo(rot2, true).Should().Be(Math.Abs(expDistance));
        r1.DistanceTo(rot2, false).Should().Be(expDistance);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 180, 180)]
    [InlineData(90, 270, 180)]
    [InlineData(0, 90, 90)]
    [InlineData(0, 270, -90)]
    [InlineData(30, 330, -60)]
    [InlineData(330, 30, 60)]
    [InlineData(270, 0, 90)]
    public void DistanceClassTests(
    int rot1, int rot2, int expDistance) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);

        r1.DistanceTo(r2, true).Should().Be(Math.Abs(expDistance));
        r1.DistanceTo(r2, false).Should().Be(expDistance);
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
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);

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
        Rotation2D r1 = new Rotation2D(rot1);

        r1.AverageTo(rot2, percent);
        r1.RotationAngle.Should().Be(outAngle);
    }

    [Theory]
    [InlineData(0, 90, 0.5, 45)]
    [InlineData(0, 90, 0.2, 18)]
    [InlineData(0, 270, 0.5, 315)]
    [InlineData(0, 270, 0.2, 342)]
    [InlineData(10, 214, 0.15, 346.6)]
    [InlineData(14, 256, 0.7, 291.4)]
    [InlineData(196, 218, 0.28, 202.16)]
    [InlineData(207, 321, 0.83, 301.62)]
    [InlineData(218, 180, 0.59, 195.58)]
    [InlineData(232, 97, 0.42, 175.3)]
    [InlineData(242, 231, 0.24, 239.36)]
    [InlineData(26, 101, 0.08, 32)]
    [InlineData(28, 168, 0.17, 51.8)]
    [InlineData(45, 168, 0.24, 74.52)]
    public void ComplexAverageToTests(
        int rot1, int rot2, double percent, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);

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
        Rotation2D r1 = new Rotation2D();
        Rotation2D r2 = new Rotation2D();
        r1.Invoking(x => x.AverageTo(r2, average)).Should().ThrowExactly<ArgumentOutOfRangeException>()
            .WithMessage("Percent must be between 0 and 1. (Parameter 'percent')");
    }
}
public class RotationClassMoveToTests {
    [Theory]
    [InlineData(0, 90, 5)]
    [InlineData(0, 270, 355)]
    [InlineData(90, 0, 85)]
    [InlineData(90, 180, 95)]
    [InlineData(90, 93, 93)]
    [InlineData(90, 86, 86)]

    public void FiveDistanceTests(
        double start, double end, double expected) {
        Rotation2D r1 = new Rotation2D(start);
        Rotation2D r2 = new Rotation2D(end);

        r1.MoveTo(r2, 5);
        r1.RotationAngle.Should().Be(expected);
    }

    [Theory]
    [InlineData(123.06, 120.90, 5.20, 120.90)]
    [InlineData(192.75, 266.93, 3.39, 196.14)]
    [InlineData(149.43, 7.72, 0.72, 148.71)]
    [InlineData(47.26, 310.59, 1.67, 45.59)]
    [InlineData(282.71, 317.03, 4.38, 287.09)]
    [InlineData(111.51, 1.24, 6.33, 105.18)]
    [InlineData(319.33, 63.56, 8.34, 327.67)]
    [InlineData(63.36, 319.72, 7.82, 55.54)]
    [InlineData(55.40, 330.36, 1.27, 54.13)]
    [InlineData(167.29, 177.47, 7.00, 174.29)]
    public void RandomDistanceTests(
    double start, double end, double movingDistance, double expected) {
        Rotation2D r1 = new Rotation2D(start);

        r1.MoveTo(end, movingDistance);
        r1.RotationAngle.Should().Be(expected);
    }
}
public class RotationClassAddOperatorTests {
    [Theory]
    [InlineData(90, 0, 90)]
    [InlineData(260, 90, 350)]
    [InlineData(90, 45, 135)]
    [InlineData(360, 90, 90)]
    [InlineData(0, 90, 90)]
    [InlineData(0, 15.2, 15.2)]
    public void AdditionTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        r1 += r2;
        r1.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassSubtractOperatorTests {
    [Theory]
    [InlineData(90, 0, 90)]
    [InlineData(260, 90, 170)]
    [InlineData(90, 45, 45)]
    [InlineData(360, 90, 270)]
    [InlineData(0, 90, 270)]
    [InlineData(90, 4.5, 85.5)]
    public void SubtractionTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        r1 -= r2;
        r1.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassAverageOperatorTests {
    [Theory]
    [InlineData(0, 180, 90)]
    [InlineData(0, 90, 45)]
    [InlineData(0, 45, 22.5)]
    [InlineData(350, 10, 0)]
    [InlineData(330, 90, 30)]
    [InlineData(45, 180, 112.5)]
    public void AverageOperatorTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        Rotation2D answer = r1 ^ r2;
        answer.RotationAngle.Should().Be(outAngle);
    }
}
public class RotationClassModuloOperatorTests {
    [Theory]
    [InlineData(360, 60, 0)]
    [InlineData(90, 35, 20)]
    [InlineData(180, 60, 0)]
    [InlineData(180, 50, 30)]
    [InlineData(90, 3.5, 2.5)]
    public void ModuloTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        Rotation2D answer = r1 % r2;
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
    [InlineData(245.25, 245.24, false)]
    [InlineData(245.25, 245.25, true)]
    public void EqualityInterfaceTests(
        double rot1, double rot2, bool equality) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
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
        double rot1, double rot2, int comparison) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        r1.CompareTo(r2).Should().Be(comparison);
    }
}
public class RotationClassSmallTests {
    [Fact]
    public void OneBigTest() {
        Rotation2D rot1 = new Rotation2D(45);
        Rotation2D rot2 = new Rotation2D(185);

        rot1.Coords.X.Should().Be(0.71);
        rot1.Coords.Y.Should().Be(0.71);

        Rotation2D a1 = -rot1;
        rot1.FlipX();
        (a1 == rot1).Should().BeTrue();
        rot1.FlipX();

        a1++;
        a1.RotationAngle.Should().Be(316);

        a1--;
        a1.RotationAngle.Should().Be(315);

        (a1 != rot1).Should().BeTrue();
        (rot2 < a1).Should().BeTrue();
        (rot2 > a1).Should().BeFalse();
        (rot2 <= a1).Should().BeTrue();
        (rot2 >= rot1).Should().BeTrue();
        (rot2 < a1).Should().BeTrue();

        Point2D p = a1 * 2;
        p.X.Should().Be(1.42);
        p.Y.Should().Be(-1.42);

        if (a1) { Assert.True(true); } else { Assert.False(true); }

        a1.RotationAngle = 0;

        if (!a1) { Assert.True(true); } else { Assert.False(true); }
    }

    [Theory]
    [InlineData(6.21, -6.55, 313.47)]
    [InlineData(-7.30, -4.11, 209.38)]
    [InlineData(7.09, -5.11, 324.22)]
    [InlineData(-4.71, -4.77, 225.36)]
    [InlineData(-0.22, 0.35, 122.15)]
    [InlineData(-8.10, 8.44, 133.82)]
    [InlineData(-1.88, 4.98, 110.68)]
    [InlineData(4.02, 5.66, 54.62)]
    [InlineData(5.77, -8.25, 304.97)]
    [InlineData(-1.13, 5.75, 101.12)]
    public void StaticAngleToTests(
        double x, double y, double expAngle) {
        Rotation2D.AngleOf(x, y).Should().Be(expAngle);
    }
}
