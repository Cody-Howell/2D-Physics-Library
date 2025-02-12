using FluentAssertions;
using Physics2DLibrary;
namespace Physics_2D_Library.Tests.PointClass;
public class PointClassBaseTests {
    [Fact]
    public void TestPointClassPair() {
        Point2D p = new Point2D();
        p.Pair = (1.2, 3.4);
        p.X.Should().Be(1.2);
        p.Y.Should().Be(3.4);
    }

    [Fact]
    public void PointClassConstructors() {
        Point2D p1 = new Point2D(1.2, 3.4);
        p1.Pair.Should().Be((1.2, 3.4));

        Point2D p2 = new Point2D((3.4, 5.6));
        p2.Pair.Should().Be((3.4, 5.6));

        Point2D p3 = new Point2D(p1);
        p3.Equals(p1).Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 5, 0, -1)]
    [InlineData(0, 0, 5, -5, -1)]
    [InlineData(0, 0, -5, 5, 1)]
    [InlineData(0, 0, 0, -5, 1)]
    public void PointClassComparison(
        double x1, double y1, double x2, double y2, int comparisonValue) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D p2 = new Point2D(x2, y2);

        p1.CompareTo(p2).Should().Be(comparisonValue);
    }
}
public class PointClassMidpointTests {
    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0)]
    [InlineData(0, 0, 10, 10, 5, 5)]
    [InlineData(0, 0, 10, -5, 5, -2.5)]
    [InlineData(10, 10, 0, 0, 5, 5)]
    [InlineData(10, 10, 10, -5, 10, 2.5)]
    public void PointClassMidpoint(
        double x1, double y1, double x2, double y2, double xOut, double yOut) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D p2 = new Point2D(x2, y2);

        Point2D answer = p1.GetMidpoint(p2);
        answer.X.Should().Be(xOut);
        answer.Y.Should().Be(yOut);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0)]
    [InlineData(0, 0, 10, 10, 5, 5)]
    [InlineData(0, 0, 10, -5, 5, -2.5)]
    [InlineData(10, 10, 0, 0, 5, 5)]
    [InlineData(10, 10, 10, -5, 10, 2.5)]
    public void PointDoubleMidpoint(
    double x1, double y1, double x2, double y2, double xOut, double yOut) {
        Point2D p1 = new Point2D(x1, y1);

        Point2D answer = p1.GetMidpoint(x2, y2);
        answer.X.Should().Be(xOut);
        answer.Y.Should().Be(yOut);
    }
}
public class PointClassDistanceTests {
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 10, 0, 10)]
    [InlineData(0, 0, 0, 10, 10)]
    [InlineData(0, 0, 3, 4, 5)]
    [InlineData(0, 0, -3, -4, 5)]
    [InlineData(0, 0, -3, 4, 5)]
    [InlineData(0, 0, 3, -4, 5)]
    public void PointClassDistance(
        double x1, double y1, double x2, double y2, double expDistance) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D p2 = new Point2D(x2, y2);

        p1.GetDistance(p2).Should().Be(expDistance);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 10, 0, 10)]
    [InlineData(0, 0, 0, 10, 10)]
    [InlineData(0, 0, 3, 4, 5)]
    [InlineData(0, 0, -3, -4, 5)]
    [InlineData(0, 0, -3, 4, 5)]
    [InlineData(0, 0, 3, -4, 5)]
    public void PointDoubleDistance(
    double x1, double y1, double x2, double y2, double expDistance) {
        Point2D p1 = new Point2D(x1, y1);

        p1.GetDistance(x2, y2).Should().Be(expDistance);
    }
}
public class PointClassAssignPointTests {
    [Theory]
    [InlineData(-0.5, -2.6, 1, 2.43, 1.93, -2.5)]
    [InlineData(-0.9, 4.3, 219, 3.40, -3.55, 2.15)]
    [InlineData(1.2, 1.9, 148, 1.47, -0.05, 2.67)]
    [InlineData(-2.4, -0.7, 290, 0.66, -2.17, -1.32)]
    [InlineData(-2.6, 0.7, 204, 3.66, -5.9, -0.8)]
    [InlineData(3.9, 3.3, 232, 0.13, 3.81, 3.2)]
    [InlineData(4.1, -5.0, 45, 2.65, 5.98, -3.1)]
    [InlineData(-4.5, 2.4, 359, 1.59, -2.91, 2.3)]
    public void AssignPointTests(
        double x1, double x2, int angle, double scalar, double pointX, double pointY) {
        Point2D p = new Point2D();
        p.AssignPoint(x1, x2, new Rotation2D(angle), scalar);
        p.X.Should().BeApproximately(pointX, 0.1);
        p.Y.Should().BeApproximately(pointY, 0.1);
    }

    [Theory]
    [InlineData(-0.5, -2.6, 1, 2.43, 1.93, -2.5)]
    [InlineData(-0.9, 4.3, 219, 3.40, -3.55, 2.15)]
    [InlineData(1.2, 1.9, 148, 1.47, -0.05, 2.67)]
    [InlineData(-2.4, -0.7, 290, 0.66, -2.17, -1.32)]
    [InlineData(-2.6, 0.7, 204, 3.66, -5.9, -0.8)]
    [InlineData(3.9, 3.3, 232, 0.13, 3.81, 3.2)]
    [InlineData(4.1, -5.0, 45, 2.65, 5.98, -3.1)]
    [InlineData(-4.5, 2.4, 359, 1.59, -2.91, 2.3)]
    public void AssignPointAsPointTests(
    double x1, double x2, int angle, double scalar, double pointX, double pointY) {
        Point2D p = new Point2D();
        Point2D innerPoint = new Point2D(x1, x2);
        p.AssignPoint(innerPoint, new Rotation2D(angle), scalar);
        p.X.Should().BeApproximately(pointX, 0.1);
        p.Y.Should().BeApproximately(pointY, 0.1);
    }
}
public class PointClassOperatorTests {
    [Fact]
    public void PointCanInvert() {
        Point2D p = new Point2D(3, 5);
        p = -p;
        p.X.Should().Be(-3);
        p.Y.Should().Be(-5);
    }

    [Fact]
    public void PointCanBeAdded() {
        Point2D p1 = new Point2D(3, 5);
        Point2D p2 = new Point2D(5, 8);
        Point2D answer = p1 + p2;

        answer.X.Should().Be(8);
        answer.Y.Should().Be(13);
    }

    [Fact]
    public void PointCanBeSubtracted() {
        Point2D p1 = new Point2D(3, 5);
        Point2D p2 = new Point2D(5, 8);
        Point2D answer = p1 - p2;

        answer.X.Should().Be(-2);
        answer.Y.Should().Be(-3);
    }

    [Fact]
    public void PointCanBeModulod() {
        Point2D p1 = new Point2D(3, 5);
        Point2D p2 = new Point2D(5, 8);
        Point2D answer = p1 % p2;

        answer.X.Should().Be(2);
        answer.Y.Should().Be(3);
    }

    [Theory]
    [InlineData(10, 0, 0)]
    [InlineData(10, 10, 45)]
    [InlineData(0, 10, 90)]
    [InlineData(-10, 10, 135)]
    [InlineData(-10, 0, 180)]
    [InlineData(-10, -10, 225)]
    [InlineData(0, -10, 270)]
    [InlineData(10, -10, 315)]
    public void AngleCanBeGotten(
        double x1, double y1, int expAngle) {
        Point2D p1 = new Point2D(0, 0);
        Point2D p2 = new Point2D(x1, y1);
        Rotation2D answer = p1 ^ p2;

        answer.RotationAngle.Should().Be(expAngle);
    }

    [Fact]
    public void PointsCanCheckEquality() {
        Point2D p1 = new Point2D(2, 3);
        Point2D p2 = new Point2D(3, 5);

        (p1 == p2).Should().BeFalse();
        (p1 != p2).Should().BeTrue();

        p2 = new Point2D(p1);
        (p1 == p2).Should().BeTrue();
    }

    [Theory]
    [InlineData(10, 0, 0, 0, 0)]
    [InlineData(10, 0, 1, 10, 0)]
    [InlineData(10, 10, 0.25, 2.5, 2.5)]
    [InlineData(10, 10, 0.5, 5, 5)]
    public void PointCanScale(
    double x1, double y1, double scalar, double xOut, double yOut) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D answer = p1 * scalar;

        answer.X.Should().Be(xOut);
        answer.Y.Should().Be(yOut);
    }
}


